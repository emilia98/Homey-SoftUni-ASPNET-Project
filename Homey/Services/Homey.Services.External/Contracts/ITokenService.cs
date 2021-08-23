using System.Threading.Tasks;
using Homey.Data.Models;

namespace Homey.Services.External.Contracts
{
    public interface ITokenService
    {
        Task<string> CreateToken(ApplicationUser user);
    }
}
