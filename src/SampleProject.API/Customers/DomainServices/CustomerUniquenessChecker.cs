using Dapper;
using SampleProject.Domain.Customers;
using SampleProject.Infrastructure;

namespace SampleProject.API.Customers.DomainServices
{
    public class CustomerUniquenessChecker : ICustomerUniquenessChecker
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        public CustomerUniquenessChecker(ISqlConnectionFactory sqlConnectionFactory)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
        }

        public bool IsUnique(Customer customer)
        {
            using (var connection = this._sqlConnectionFactory.GetOpenConnection())
            {
                const string sql = "SELECT TOP 1 1" +
                                   "FROM [orders].[Customers] AS [Customer] " +
                                   "WHERE [Customer].[Email] = @Email";
                var customersNumber = connection.QuerySingle<int?>(sql,
                                new
                                {
                                    customer.Email
                                });

                return !customersNumber.HasValue;
            }
        }
    }
}