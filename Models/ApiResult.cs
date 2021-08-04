using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LeasedCarService.Models
{
    public class ApiResult<T>
    {
        /// <summary>
        /// 錯誤代碼，(0=成功)
        /// </summary>
        public string ErrorCode { get; set; }
        /// <summary>
        /// 錯誤訊息，(成功'')
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// 錯誤訊息
        /// </summary>
        public T Data { get; set; }


        public ApiResult() { }

        /// <summary>
        /// 建立成功結果
        /// </summary>
        /// <param name="data"></param>
        public ApiResult(T data)
        {
            ErrorCode = "0";
            Message = "";
            Data = data;
        }
    }

    public class ApiError : ApiResult<object>
    {
        /// <summary>
        /// 建立失敗結果
        /// </summary>
        /// <param name="code"></param>
        /// <param name="message"></param>
        public ApiError(string code, string message)
        {
            ErrorCode = code;
            Message = message;
            Data = null;
        }
    }
}
