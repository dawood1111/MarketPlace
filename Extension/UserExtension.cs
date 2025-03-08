using System.Security.Claims;

namespace api.Extension
{
    public static class UserExtension
    {
        public static String GetUserName(this ClaimsPrincipal user00){
           var claim= user00.Claims.SingleOrDefault(s=>s.Type==ClaimTypes.GivenName);
            if(claim==null){
           throw new InvalidOperationException("UserName claim not found.");
            }
            return claim.Value;


        }
           public static String GetEmail(this ClaimsPrincipal user00){
       
           var claim= user00.Claims.SingleOrDefault(s=>s.Type==ClaimTypes.Email);
            if(claim==null){
           throw new InvalidOperationException("Email claim not found.");
            }
            return claim.Value;


        }
        
    }
}