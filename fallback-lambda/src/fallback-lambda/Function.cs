using System;
using System.Threading.Tasks;
using Amazon.Lambda.Core;
using Amazon.Lambda.APIGatewayEvents;
using StackExchange.Redis;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace FallbackFunction
{
    public class Function
    {
        private readonly IDatabase _redisCache;

        public Function()
        {
            // Get environment variables
            string redisConnectionString = Environment.GetEnvironmentVariable("REDIS_CONNECTION_STRING") ?? throw new ArgumentNullException("REDIS_CONNECTION_STRING");

            ConnectionMultiplexer redis = ConnectionMultiplexer.Connect(redisConnectionString);
            _redisCache = redis.GetDatabase();
        }

        public async Task<APIGatewayProxyResponse> FunctionHandler(APIGatewayProxyRequest input, ILambdaContext context)
        {
            context.Logger.LogInformation("Fallback Lambda: Received request to perform a fallback operation.");

            try
            {
                // Retrieve cached data
                context.Logger.LogInformation("Retrieving data from cache...");
                string cachedData = await _redisCache.StringGetAsync("cachedData");

                if (!string.IsNullOrEmpty(cachedData))
                {
                    context.Logger.LogInformation("Successfully retrieved data from cache.");
                    return new APIGatewayProxyResponse
                    {
                        StatusCode = 200,
                        Body = cachedData
                    };
                }
                else
                {
                    context.Logger.LogWarning("No cached data available. Returning default fallback response.");
                    return new APIGatewayProxyResponse
                    {
                        StatusCode = 200,
                        Body = "Fallback: No cached data available."
                    };
                }
            }
            catch (Exception ex)
            {
                context.Logger.LogError($"Fallback operation failed: {ex.Message}");
                return new APIGatewayProxyResponse
                {
                    StatusCode = 500,
                    Body = "Fallback operation encountered an error."
                };
            }
        }
    }
}
