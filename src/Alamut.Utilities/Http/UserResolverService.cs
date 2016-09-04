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

        public string GetUserName()
        {
            return _context.HttpContext.User?.Identity?.Name;
        }

        public string GetUserId()
        {
            return _context.HttpContext.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        }

        public string GetUserIpAddress()
        {
            return _context.HttpContext.Features.Get<IHttpConnectionFeature>()?
                .RemoteIpAddress.ToString();
        }
    }
}
