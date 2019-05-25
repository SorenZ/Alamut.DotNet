using System;
using Microsoft.AspNetCore.Http;

namespace Alamut.Utilities.AspNet.Session
{
    /// <summary>
    /// provide ASP.NET Core session helpers to save and retrieve value types
    /// </summary>
    public static class SessionExtensions 
    {
        public static void Set<T>(this ISession session, string key, T value) where T : struct
        {
            session.SetString(key, value.ToString());
        }

        public static long GetInt64(this ISession session, string key)
        {
            var value = session.GetString(key);
            return value == null
                ? default(long)
                : long.Parse(value);
        }

        public static byte GetByte(this ISession session, string key)
        {
            var value = session.GetString(key);
            return value == null
                ? default(byte)
                : byte.Parse(value);
        }

        public static bool GetBool(this ISession session, string key)
        {
            var value = session.GetString(key);
            return value != null && bool.Parse(value);
        }

        public static DateTime GetDateTime(this ISession session, string key)
        {
            var value = session.GetString(key);
            return value == null
                ? default(DateTime)
                : Convert.ToDateTime(value);
        }

        public static double GetDouble(this ISession session, string key)
        {
            var value = session.GetString(key);
            return value == null
                ? default(double)
                : double.Parse(value);
        }

    }
}