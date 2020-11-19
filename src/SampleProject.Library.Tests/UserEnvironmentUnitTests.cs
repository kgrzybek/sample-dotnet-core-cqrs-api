using NUnit.Framework;

namespace SampleProject.Library.Tests
{
    public class UserEnvironmentUnitTests
    {
        private UserEnvironment _userEnvironment;
        [SetUp]
        public void Setup()
        {
            _userEnvironment = new UserEnvironment();
        }

        [Test]
        public void GetUserSecretKey_IsNotNull()
        {
            var key = _userEnvironment.GetUserSecretKey();
            Assert.IsNotNull(key);
        }

        [Test]
        public void GetConnectionString_IsNotNull()
        {
            var key = _userEnvironment.GetConnectionString();
            Assert.IsNotNull(key);
        }
    }
}