using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Infrastructure
{
    public class ServiceResult<T> where T:class
    {
        public T Result { get; }
        public List<string> Errors { get; }
        public bool Succeded { get; }
        public ServiceResult(T res)
        {
            Succeded = true;
            Errors = new List<string>();
            Result = res;
        }
        public ServiceResult(List<string> errors)
        {
            Succeded = false;
            Errors = errors;
            Result = null;
        }
    }
}
