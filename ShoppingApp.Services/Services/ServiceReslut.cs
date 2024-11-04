using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingApp.Services.Services
{
    public class ServiceResult<T>
    {
        public T Data { get; }
        public bool Success { get; }
        public string Message { get; }
        public string ErrorMessage { get; }

        public ServiceResult() { }

        public ServiceResult(bool success, string message, T data)
        {
            Success = success;
            Message = message;
            Data = data;
        }
    }



}
