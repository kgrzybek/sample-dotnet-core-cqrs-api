using System;

namespace SampleProject.Library
{
    public class UserEnvironment
    {
        public string GetUserSecretKey()
        {
            return Environment.GetEnvironmentVariable("UserSecretKey");
        }

        public string GetConnectionString(){
            return Environment.GetEnvironmentVariable("ASPNETCORE_SampleProject_IntegrationTests_ConnectionString");
        }
    }
}
