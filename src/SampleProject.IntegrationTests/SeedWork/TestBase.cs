using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using NUnit.Framework;
using SampleProject.Infrastructure;

namespace SampleProject.IntegrationTests.SeedWork
{
    public class TestBase
    {
        protected string ConnectionString;

        [SetUp]
        public async Task BeforeEachTest()
        {
            const string connectionStringEnvironmentVariable =
                "ASPNETCORE_SampleProject_IntegrationTests_ConnectionString";
            ConnectionString = Environment.GetEnvironmentVariable(connectionStringEnvironmentVariable);
            if (ConnectionString == null)
            {
                throw new ApplicationException(
                    $"Define connection string to integration tests database using environment variable: {connectionStringEnvironmentVariable}");
            }

            await using var sqlConnection = new SqlConnection(ConnectionString);

            await ClearDatabase(sqlConnection);

            ApplicationStartup.Initialize(
                new ServiceCollection(),
                ConnectionString, 
                new CacheStore(),
                runQuartz:false);
        }

        private static async Task ClearDatabase(IDbConnection connection)
        {
            const string sql = "DELETE FROM app.InternalCommands " +
                               "DELETE FROM app.OutboxMessages " +
                               "DELETE FROM orders.OrderProducts " +
                               "DELETE FROM orders.Orders " +
                               "DELETE FROM payments.Payments " +
                               "DELETE FROM orders.Customers ";

            await connection.ExecuteScalarAsync(sql);
        }
    }
}