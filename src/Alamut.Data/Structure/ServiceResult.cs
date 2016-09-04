using System;
using System.Collections.Generic;

namespace Alamut.Data.Structure
{
    /// <summary>
    /// Represent Service result data structure
    /// It can be used as web service result
    /// </summary>
    /// <remarks>result for void Service</remarks>
    public class ServiceResult 
    {
        public int Status { get; set; } 

        public string Message { get; set; }

        public bool Succeed { get; set; }

        /// <summary>
        /// return a successful ServiceResult 
        /// </summary>
        /// <returns>successful ServiceResult</returns>
        public static ServiceResult Okay(string message = "", int status = 200)
        {
            return new ServiceResult
            {
                Succeed = true,
                Message = message,
                Status = status
            };
        }


        /// <summary>
        /// return a ServiceResult with error status and error message
        /// </summary>
        /// <param name="message">error message</param>
        /// <param name="status">result status</param>
        /// <returns>Error ServiceResult</returns>
        public static ServiceResult Error(string message = "", int status = 500)
        {
            return new ServiceResult
            {
                Succeed = false,
                Message = message,
                Status = status
            };
        }

        /// <summary>
        /// return a ServiceResult from exception
        /// most inmportant exception included in error message
        /// </summary>
        /// <param name="ex">the exception</param>
        /// <param name="status">error code</param>
        /// <returns>Error ServiceResult</returns>
        public static ServiceResult Exception(Exception ex, int status = 500 )
        {
            return new ServiceResult
            {
                Succeed = false,
                Message = ex.GetBaseException().Message,
                Status = status
            };
        }

        /// <summary>
        /// return a ServiceResult from validationResult 
        ///     that contains validation message in JSON fomat.
        /// </summary>
        /// <param name="fieldMessages">List of { propertyName, validationMessage }</param>
        /// <returns></returns>
        public static dynamic ValidationFailed(Dictionary<string, string> fieldMessages)
        {
            return new
            {
                Status = ResultStatus.ValidationFailed,
                Message = fieldMessages
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
        /// <param name="status">the result status code </param>
        /// <returns>successful ServiceResult</returns>
        public static ServiceResult<T> Okay(T data, string message = "", int status = 200)
        {
            return new ServiceResult<T>
            {
                Succeed = true,
                Message = message,
                Data = data,
                Status = status
            };
        }

        /// <summary>
        /// returns a typed ServiceResult with an error
        /// </summary>
        /// <param name="message">error message</param>
        /// <param name="status"></param>
        /// <returns>error ServiceResult</returns>
        public new static ServiceResult<T> Error(string message, int status = 200)
        {
            return new ServiceResult<T>
            {
                Succeed = false,
                Message = message,
                Status = status
            };
        }

        /// <summary>
        /// return a ServiceResult from exception
        /// most inmportant exception included in error message
        /// </summary>
        /// <param name="ex">the exception</param>
        /// <param name="status">error code</param>
        /// <returns>Error ServiceResult</returns>
        public new static ServiceResult<T> Exception(Exception ex, int status = 500)
        {
            return new ServiceResult<T>
            {
                Succeed = false,
                Message = ex.GetBaseException().Message,
                Status = status
            };
        }
    }

    /// <summary>
    /// result type status
    /// </summary>
    public enum ResultStatus
    {
        /// <summary>
        /// the service got the message and execute it.
        /// </summary>
        Okay = 0,

        /// <summary>
        /// the Service executed and there is some information about it.
        /// </summary>
        Information = 2,

        /// <summary>
        /// the Service executed and there is a warning.
        /// </summary>
        Warning = 3,

        /// <summary>
        /// the commad execution has been failed with error. 
        /// </summary>
        Error = 4,

        /// <summary>
        /// the Service executed successfully 
        /// </summary>
        Successful = 5,

        /// <summary>
        /// determine that an exception occurred
        /// the exception detail provided in message
        /// </summary>
        Exception = 6,

        /// <summary>
        /// the request for service is not valid.
        /// </summary>
        ValidationFailed = 7,

        /// <summary>
        /// the permission is denied for access service
        /// </summary>
        PermissionDenied = 8
    }
}
