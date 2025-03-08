using api.Model;

namespace api.Interface
{
    public interface ITokenService
    {
         public String CreateToken(User user);
    }
}