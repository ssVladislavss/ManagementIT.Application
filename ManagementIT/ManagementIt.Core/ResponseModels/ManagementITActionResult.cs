using ManagementIt.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contracts.Enums;

namespace ManagementIt.Core.ResponseModels
{
    public class ManagementITActionResult
    {
        public bool Success { get; set; }
        public IEnumerable<TypeOfErrors> Errors { get; set; }
        public NotificationType Type { get; set; }
        public string TypeStr { get { return Type.ToString(); } }
        public string ErrorDescription { get; set; }
        public string AspNetException { get; set; }

        protected ManagementITActionResult(bool success, NotificationType type)
        {
            Success = success;
            Type = type;
        }
        protected ManagementITActionResult(bool success, IEnumerable<TypeOfErrors> errors, string e, string errorDescription)
        {
            Success = success;
            Errors = errors;
            ErrorDescription = errorDescription;
            Type = NotificationType.Error;
            AspNetException = e;
        }

        public static ManagementITActionResult IsSuccess() { return new ManagementITActionResult(true, NotificationType.Success); }
        public static ManagementITActionResult Fail(IEnumerable<TypeOfErrors> errors, string errorDescription, string e = null) { return new ManagementITActionResult(false, errors, e, errorDescription); }
    }

    public class ManagementITActionResult<T> : ManagementITActionResult
    {
        public T Data { get; private set; }

        protected ManagementITActionResult(bool success, NotificationType type, T data) : base(success, type) => Data = data;
        protected ManagementITActionResult(bool success, IEnumerable<TypeOfErrors> errors, string errorDescription, T data, string e = null) : base(success, errors, e, errorDescription) => Data = data;
        

        public static ManagementITActionResult<T> IsSuccess(T data) { return new ManagementITActionResult<T>(true, NotificationType.Success, data); }
        public static ManagementITActionResult<T> Fail(IEnumerable<TypeOfErrors> errors, string errorDescription, T data, string e = null) { return new ManagementITActionResult<T>(false, errors, errorDescription, data, e); }
    }
}
