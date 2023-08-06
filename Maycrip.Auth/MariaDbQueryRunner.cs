using System.Data;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MySql.Data.MySqlClient;

namespace Maycrip.Auth;

internal class MariaDbQueryRunner: IQueryRunner
{
        private readonly string _connectionString;

        private readonly ILogger _logger;

        public MariaDbQueryRunner(
            IOptions<MariaDBConnectionOptions> options,
            ILogger<MariaDbQueryRunner> logger)
        {
            _connectionString = options.Value?.BuildConnectionString() ?? string.Empty;
            _logger = logger;
        }

        public async Task RunAsync(IDbQuery query)
        {
            IDbConnection connection = null;

            try
            {
                connection = new MySqlConnection(_connectionString);
                connection.Open();

                await query.ExecuteAsync(connection);
            }
            catch (ArgumentException argumentException)
            {
                _logger.LogError(argumentException, "Persistence Error: {Message}", argumentException.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Persistence Error");
            }
            finally
            {
                connection?.Close();
                connection?.Dispose();
            }
        }

        public Task<TResult> RunAsync<TResult>(IDbQuery<TResult> query)
        {
            IDbConnection connection = null;

            try
            {
                connection = new MySqlConnection(_connectionString);
                connection.Open();

                return query.ExecuteAsync(connection);
            }
            catch (ArgumentException argumentException)
            {
                _logger.LogError(argumentException, "Persistence Error: {Message}", argumentException.Message);

                return Task.FromResult(default(TResult));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Persistence Error");

                return Task.FromResult(default(TResult));
            }
            finally
            {
                connection?.Close();
                connection?.Dispose();
            }
        }   
}