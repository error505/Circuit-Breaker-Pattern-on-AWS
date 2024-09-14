using System;
using System.Net.Http;
using System.Threading.Tasks;
using Amazon.Lambda.Core;
using Amazon.Lambda.APIGatewayEvents;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Http.Resilience;
using Microsoft.Extensions.Logging;
using System.Net.Http.Json;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace ApiFunction
{
    public class Function
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly HttpClient _httpClient;
        private readonly ILogger<Function> _logger;

        public Function()
        {
            // Set up a service collection and configure the HTTP client with resilience policies.
            var services = new ServiceCollection();
            
            // Add logging
            services.AddLogging(configure => configure.AddConsole());

            // Configure HttpClient with resilience policies using Microsoft.Extensions.Http.Resilience
            services.AddHttpClient("FunctionClient")
                .AddStandardResilienceHandler()
                .Configure(options =>
                {
                    options.CircuitBreaker.FailureRatio = 0.5; // 50% failure ratio
                    options.CircuitBreaker.MinimumThroughput = 10;
                    options.CircuitBreaker.SamplingDuration = TimeSpan.FromSeconds(30);
                    options.CircuitBreaker.BreakDuration = TimeSpan.FromSeconds(30);

                    options.Retry.BackoffType = Polly.DelayBackoffType.Exponential;
                    options.Retry.MaxRetryAttempts = 3;
                    options.Retry.Delay = TimeSpan.FromSeconds(2);
                });

            _serviceProvider = services.BuildServiceProvider();

            // Get the HttpClient instance from the service provider
            var httpClientFactory = _serviceProvider.GetRequiredService<IHttpClientFactory>();
            _httpClient = httpClientFactory.CreateClient("FunctionClient");

            // Set up logging
            _logger = _serviceProvider.GetRequiredService<ILogger<Function>>();
        }

        public async Task<APIGatewayProxyResponse> FunctionHandler(APIGatewayProxyRequest input, ILambdaContext context)
        {
            string retryFunctionUrl = Environment.GetEnvironmentVariable("RETRY_FUNCTION_URL");
            string fallbackFunctionUrl = Environment.GetEnvironmentVariable("FALLBACK_FUNCTION_URL");

            try
            {
                // Attempt to call the Retry Lambda Function
                _logger.LogInformation("Calling Retry Lambda Function...");
                HttpResponseMessage response = await _httpClient.PostAsJsonAsync($"{retryFunctionUrl}/retry-operation", input.Body);

                if (response.IsSuccessStatusCode)
                {
                    _logger.LogInformation("Retry Lambda Function succeeded.");
                    return new APIGatewayProxyResponse
                    {
                        StatusCode = 200,
                        Body = await response.Content.ReadAsStringAsync()
                    };
                }
                else
                {
                    _logger.LogWarning("Retry Lambda Function failed. Attempting to call Fallback Lambda Function...");
                    return await CallFallbackFunction(fallbackFunctionUrl, input.Body);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception in API Lambda: {ex.Message}. Calling Fallback Lambda Function...");
                return await CallFallbackFunction(fallbackFunctionUrl, input.Body);
            }
        }

        private async Task<APIGatewayProxyResponse> CallFallbackFunction(string fallbackFunctionUrl, string requestData)
        {
            try
            {
                HttpResponseMessage response = await _httpClient.PostAsJsonAsync($"{fallbackFunctionUrl}/fallback-operation", requestData);

                if (response.IsSuccessStatusCode)
                {
                    _logger.LogInformation("Fallback Lambda Function succeeded.");
                    return new APIGatewayProxyResponse
                    {
                        StatusCode = 200,
                        Body = await response.Content.ReadAsStringAsync()
                    };
                }
                else
                {
                    _logger.LogError("Fallback Lambda Function failed.");
                    return new APIGatewayProxyResponse
                    {
                        StatusCode = (int)response.StatusCode,
                        Body = await response.Content.ReadAsStringAsync()
                    };
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception in Fallback Function: {ex.Message}");
                return new APIGatewayProxyResponse
                {
                    StatusCode = 500,
                    Body = "An error occurred while handling the fallback."
                };
            }
        }
    }
}
