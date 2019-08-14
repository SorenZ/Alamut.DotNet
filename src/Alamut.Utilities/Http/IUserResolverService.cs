using System;

namespace Alamut.Utilities.Http
{
    [Obsolete("use Alamut.Abstractions.Principal.IUserInfo instead")]
    public interface IUserResolverService
    {
        /// <summary>
        /// gets current user Username
        /// </summary>
        string UserName { get; }

        /// <summary>
        /// gets current user full name (first name + family)
        /// </summary>
        string Name { get; }

        /// <summary>
        /// gets current user friendly name (fore name)
        /// if it doesn't represented return userName
        /// </summary>
        string GivenName { get; }

        /// <summary>
        /// gets current user UserId
        /// </summary>
        string UserId { get; }

        /// <summary>
        /// gets current user Ip Address
        /// </summary>
        string UserIpAddress { get; }
    }
}
