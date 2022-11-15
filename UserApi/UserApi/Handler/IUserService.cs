using System.Threading.Tasks;
using UserApi.Model;

namespace UserApi.Handler
{
    internal interface IUserService
    {
        Task<User> Authenticate(string username, string password);

    }
}