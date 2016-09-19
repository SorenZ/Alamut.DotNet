﻿using System;
using System.Security.Claims;

namespace Alamut.Helpers.Identity
{
    public static class ClaimsPrincipalExtensions 
    {
        /// <summary>
        /// get user Id for current user from calims 
        /// </summary>
        /// <param name="principal"></param>
        /// <returns>if user id provided in claim return that, otherwise return false</returns>
        public static string GetUserId(this ClaimsPrincipal principal)
        {
            if (principal == null)
                throw new ArgumentNullException(nameof(principal));

            return principal.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        }
    }
}