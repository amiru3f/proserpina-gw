using System.Threading.Tasks;
using Proserpina.Common;

namespace Proserpina.Auth.Api.Service
{
    public interface IAuthorizationService
    {
        Task<User> AuthorizeAsync(string username, string password);

        Task<User> DeserializeToken(string token);
    }
}