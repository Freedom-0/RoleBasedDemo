//using Microsoft.AspNetCore.Authorization;
//using RoleBasedDemo.Services;
//using System.Security.Claims;

//namespace RoleBasedDemo.Authorization
//{
//    public class DynamicAuthorizationRequirement : IAuthorizationRequirement
//    {
//        // This class does not require any additional properties or methods
//    }
//    public class DynamicAuthorizationHandler : AuthorizationHandler<IAuthorizationRequirement>
//    {
//        private readonly  IUserInfoService _userPermissionService;

//        public DynamicAuthorizationHandler(IUserInfoService userPermissionService)
//        {
//            _userPermissionService = userPermissionService;
//        }

//        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, IAuthorizationRequirement requirement)
//        {
//            var user = context.User;
//            var requestUrl = context.Resource as string;

//            // Retrieve user's role, location, and other relevant data from claims or elsewhere
//            var roles = user.FindAll(ClaimTypes.Role).Select(c => c.Value);
//            var locations = user.FindAll("Location").Select(c => c.Value);

//            // Call a service method to check user's access to the requested URL based on their role, location, and other criteria
//            var hasAccess = await _userPermissionService.CheckUserAccessAsync(roles, locations, requestUrl);

//            if (hasAccess)
//            {
//                context.Succeed(requirement);
//            }
//            else
//            {
//                context.Fail();
//            }
//        }
//    }


//}
