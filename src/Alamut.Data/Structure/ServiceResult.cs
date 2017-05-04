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
        public string Message { get; set; }

        public bool Succeed { get; set; }

        /// <summary>
        /// return a successful ServiceResult 
        /// </summary>
        /// <returns>successful ServiceResult</returns>
        public static ServiceResult Okay(string message = "")
        {
            return new ServiceResult
            {
                Succeed = true,
                Message = message,
            };
        }


        /// <summary>
        /// return a ServiceResult with error status and error message
        /// </summary>
        /// <param name="message">error message</param>
        /// <returns>Error ServiceResult</returns>
        public static ServiceResult Error(string message = "")
        {
            return new ServiceResult
            {
                Succeed = false,
                Message = message,
            };
        }

        /// <summary>
        /// return a ServiceResult from exception
        /// most inmportant exception included in error message
        /// </summary>
        /// <param name="ex">the exception</param>
        /// <returns>Error ServiceResult</returns>
        public static ServiceResult Exception(Exception ex)
        {
            return new ServiceResult
            {
                Succeed = false,
                Message = ex.GetBaseException().Message,
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
                Succeed = false,
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
        /// <returns>successful ServiceResult</returns>
        public static ServiceResult<T> Okay(T data, string message = "")
        {
            return new ServiceResult<T>
            {
                Succeed = true,
                Message = message,
                Data = data,
            };
        }

        /// <summary>
        /// returns a typed ServiceResult with an error
        /// </summary>
        /// <param name="message">error message</param>
        /// <returns>error ServiceResult</returns>
        public new static ServiceResult<T> Error(string message)
        {
            return new ServiceResult<T>
            {
                Succeed = false,
                Message = message,
            };
        }

        /// <summary>
        /// return a ServiceResult from exception
        /// most inmportant exception included in error message
        /// </summary>
        /// <param name="ex">the exception</param>
        /// <returns>Error ServiceResult</returns>
        public new static ServiceResult<T> Exception(Exception ex)
        {
            return new ServiceResult<T>
            {
                Succeed = false,
                Message = ex.GetBaseException().Message
            };
        }
    }
}
