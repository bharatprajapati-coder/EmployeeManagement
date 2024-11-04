using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace EmpManagement.Data
{
    public static class EmployeeHelper
    {
        private static Database db;

        public static Database ConfigureDb(IConfiguration configuration)
        {
            if (db == null)
            {
                string ConnectionString = configuration.GetConnectionString("Defaultconnection");


                if (!string.IsNullOrEmpty(ConnectionString))
                {
                    var providerFactory = SqlClientFactory.Instance;
                    DatabaseFactory.SetDatabaseProviderFactory(new DatabaseProviderFactory());
                    db = new GenericDatabase(ConnectionString, providerFactory);

                }
            }
            return db;

        }

    }
}
