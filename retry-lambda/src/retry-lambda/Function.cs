using System;
using System.Threading.Tasks;
using Amazon.Lambda.Core;
using Amazon.Lambda.APIGatewayEvents;
using StackExchange.Redis;
using Amazon.RDS.Util;
using Npgsql; // For PostgreSQL

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace RetryFunction
{
    public class Function
    {
        private readonly IDatabase _redisCache;
        private readonly string _connectionString;

        public Function()
        {
            // Get environment variables
            string redisConnectionString = Environment.GetEnvironmentVariable("REDIS_CONNECTION_STRING") ?? throw new ArgumentNullException("REDIS_CONNECTION_STRING");
            _connectionString = Environment.GetEnvironmentVariable("RDS_CONNECTION_STRING") ?? throw new ArgumentNullException("RDS_CONNECTION_STRING");

            ConnectionMultiplexer redis = ConnectionMultiplexer.Connect(redisConnectionString);
            _redisCache = redis.GetDatabase();
        }

        public async Task<APIGatewayProxyResponse> FunctionHandler(APIGatewayProxyRequest input, ILambdaContext context)
        {
            context.Logger.LogInformation("Retry Lambda: Received request to perform operation.");

            try
            {
                // Simulate database operation
                context.Logger.LogInformation("Performing database operation...");
                await PerformDatabaseOperation(input.Body);

                // Update cache after successful operation
                context.Logger.LogInformation("Updating cache...");
                await _redisCache.StringSetAsync("cachedData", input.Body);

                context.Logger.LogInformation("Operation completed successfully.");
                return new APIGatewayProxyResponse
                {
                    StatusCode = 200,
                    Body = "Operation completed successfully."
                };
            }
            catch (Exception ex)
            {
                context.Logger.LogError($"Operation failed: {ex.Message}");
                throw; // Rethrow exception to trigger retry/circuit breaker logic
            }
        }

        private async Task PerformDatabaseOperation(string data)
        {
            // Simulate a database write operation
            using var conn = new NpgsqlConnection(_connectionString);
            await conn.OpenAsync();
            using var cmd = new NpgsqlCommand("INSERT INTO DataTable (Data) VALUES (@p)", conn);
            cmd.Parameters.AddWithValue("p", data);
            await cmd.ExecuteNonQueryAsync();
        }
    }
}
