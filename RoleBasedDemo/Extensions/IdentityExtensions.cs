using RoleBasedDemo.Helpers;
using System.Security.Claims;
using System.Security.Principal;



namespace RoleBasedDemo.Extensions
{
    public static class IdentityExtensions
    {
        public static bool ISLoggedIn(this IIdentity identity)
        {
            ClaimsIdentity? claimsIdentity = identity as ClaimsIdentity;
            Claim? claim = claimsIdentity?.FindFirst(Common.USERNAME);
            return claim != null;
        }

        public static string GetUserName(this IIdentity identity)
        {
            ClaimsIdentity? claimsIdentity = identity as ClaimsIdentity;
            Claim? claim = claimsIdentity?.FindFirst(Common.FULLNAME);
            return claim?.Value;
        }
        public static string GetUserMail(this IIdentity identity)
        {
            ClaimsIdentity? claimsIdentity = identity as ClaimsIdentity;
            Claim? claim = claimsIdentity?.FindFirst(Common.USERMAIL);
            return claim?.Value;
        }


    }

}