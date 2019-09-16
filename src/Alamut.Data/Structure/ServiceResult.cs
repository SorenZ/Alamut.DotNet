using System;

namespace Alamut.Data.Structure
{
    /// <summary>
    /// Represent Service result data structure
    /// It can be used as web service result
    /// </summary>
    /// <remarks>result for void Service</remarks>
    [Obsolete("use Result in Alamut.Abstractions package, it will remove in next major version")]
    public class ServiceResult
    {
        public string Message { get; set; }
        public bool Succeed { get; set; }
        public int StatusCode { get; set; }

        /// <summary>
        /// return a successful ServiceResult 
        /// </summary>
        /// <returns>successful ServiceResult</returns>
        public static ServiceResult Okay(string message = "", int statusCode = 200)
        {
            return new ServiceResult
            {
                Succeed = true,
                Message = message,
                StatusCode = statusCode
            };
        }


        /// <summary>
        /// return a ServiceResult with error status and error message
        /// </summary>
        /// <param name="message">error message</param>
        /// <param name="statusCode"></param>
        /// <returns>Error ServiceResult</returns>
        public static ServiceResult Error(string message = "", int statusCode = 500)
        {
            return new ServiceResult
            {
                Succeed = false,
                Message = message,
                StatusCode = statusCode
            };
        }

        /// <summary>
        /// return a ServiceResult from exception
        /// most important exception included in error message
        /// </summary>
        /// <param name="ex">the exception</param>
        /// <param name="statusCode"></param>
        /// <returns>Error ServiceResult</returns>
        public static ServiceResult Exception(Exception ex, int statusCode = 500)
        {
            return new ServiceResult
            {
                Succeed = false,
                Message = ex.ToString(),
                StatusCode = statusCode
            };
        }

        public override bool Equals(object obj)
        {
            var sr = obj as ServiceResult;

            if (sr == null)
            { return false; }


            return this.Succeed == sr.Succeed && this.StatusCode == sr.StatusCode;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return Convert.ToInt32(this.Succeed) + this.StatusCode;
            }
        }
    }
}
