using System;
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
    [Obsolete("use Alamut.AspNet.Principal.UserInfoByHttpContext instead")]
    public class UserResolverServiceByHttpContext : IUserResolverService
    {
        private readonly IHttpContextAccessor _context;

        public UserResolverServiceByHttpContext(IHttpContextAccessor context)
        {
            _context = context;
        }

        public string UserName => _context.HttpContext.User?.Identity?.Name;
        public string Name => _context.HttpContext.User.FindFirst(ClaimTypes.Name)?.Value;
        public string GivenName => _context.HttpContext.User.FindFirst(ClaimTypes.GivenName)?.Value;
        public string UserId => _context.HttpContext.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        public string UserIpAddress => _context.HttpContext
            .Features.Get<IHttpConnectionFeature>()?
            .RemoteIpAddress.ToString();
    }
}
