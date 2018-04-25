using System;

namespace Alamut.Data.Structure
{
    /// <summary>
    /// Represent Service result data structure
    /// It can be used as web service result
    /// </summary>
    /// <remarks>result for void Service</remarks>
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
        public static ServiceResult Error(string message = "",int statusCode = 500)
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
        /// most inmportant exception included in error message
        /// </summary>
        /// <param name="ex">the exception</param>
        /// <param name="statusCode"></param>
        /// <returns>Error ServiceResult</returns>
        public static ServiceResult Exception(Exception ex,int statusCode = 500)
        {
            return new ServiceResult
            {
                Succeed = false,
                Message = ex.ToString(),
                StatusCode = statusCode
            };
        }
        
    }

    /// <summary>
    /// provides a generic data weapper for Service result 
    /// </summary>
    /// <typeparam name="T">data type of return</typeparam>
    /// <remarks>result for not void result</remarks>
    public class ServiceResult<T> : ServiceResult
    {
        public T Data { get; set; }

        /// <summary>
        /// returns a successful typed ServiceResult
        /// </summary>
        /// <param name="data">return data</param>
        /// <param name="message">the result message</param>
        /// <param name="statusCode"></param>
        /// <returns>successful ServiceResult</returns>
        public static ServiceResult<T> Okay(T data, string message = "", int statusCode = 200)
        {
            return new ServiceResult<T>
            {
                Succeed = true,
                Message = message,
                Data = data,
                StatusCode = statusCode
            };
        }

        /// <summary>
        /// returns a typed ServiceResult with an error
        /// </summary>
        /// <param name="message">error message</param>
        /// <param name="statusCode"></param>
        /// <returns>error ServiceResult</returns>
        public new static ServiceResult<T> Error(string message,int statusCode = 500)
        {
            return new ServiceResult<T>
            {
                Succeed = false,
                Message = message,
                StatusCode = statusCode
            };
        }

        /// <summary>
        /// return a ServiceResult from exception
        /// most inmportant exception included in error message
        /// </summary>
        /// <param name="ex">the exception</param>
        /// <param name="statusCode"></param>
        /// <returns>Error ServiceResult</returns>
        public new static ServiceResult<T> Exception(Exception ex,int statusCode = 500)
        {
            return new ServiceResult<T>
            {
                Succeed = false,
                Message = ex.ToString(),
                StatusCode = statusCode
            };
        }
    }
}
