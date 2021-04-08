using System.Threading.Tasks;
using Proserpina.Common;

namespace Proserpina.Auth.Api.Service
{
    public class SimpleAuthorizationService : IAuthorizationService
    {
        private readonly User defaultUser = new User()
        {
            Username = "admin",
            Firstname = "first",
            Lastname = "last",
            Id = 1
        };

        public Task<User> AuthorizeAsync(string username, string password)
        {
            if (username == "admin" && password == "admin")
            {
                return Task.FromResult(defaultUser);
            }

            return Task.FromResult<User>(null);
        }

        public Task<User> DeserializeToken(string token)
        {
            if (token?.Length == System.Guid.NewGuid().ToString().Length)
            {
                return Task.FromResult(defaultUser);
            }

            return Task.FromResult<User>(null);
        }
    }
}