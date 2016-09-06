using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;

namespace Alamut.Utilities.Http
{
    /// <summary>
    /// provide some information about Identity User throughout  domain services
    /// by the help of HttpContextAccessor
    /// </summary>
    /// <remarks>
    /// it can be used when HTTP Context is available
    /// </remarks>
    public class UserResolverService
    {
        private readonly IHttpContextAccessor _context;

        public UserResolverService(IHttpContextAccessor context)
        {
            _context = context;
        }

        /// <summary>
        /// gets current user Username
        /// </summary>
        /// <returns></returns>
        public string GetUserName()
        {
            return _context.HttpContext.User?.Identity?.Name;
        }

        /// <summary>
        /// gets current user friendly name of current user
        /// if it doesn't represented return userName
        /// </summary>
        /// <returns></returns>
        public string GetGivenName()
        {
            return _context.HttpContext.User?.FindFirst(ClaimTypes.GivenName)?.Value ??
                   GetUserName();
        }

        /// <summary>
        /// gets current user UserId
        /// </summary>
        /// <returns></returns>
        public string GetUserId()
        {
            return _context.HttpContext.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        }

        /// <summary>
        /// gets current user Ip Address
        /// </summary>
        /// <returns></returns>
        public string GetUserIpAddress()
        {
            return _context.HttpContext.Features.Get<IHttpConnectionFeature>()?
                .RemoteIpAddress.ToString();
        }
    }
}
