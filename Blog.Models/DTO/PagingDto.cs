using System;

namespace Blog.Models.DTO
{
    public class ReturnPagingDto<T>
    {
        public T Result { get; set; }
        public int PageLength { get; set; } = 0;
    }

    public class ReturnModelDto<T>
    {
        public ReturnModelDto()
        {
        }

        public bool Success { get; set; }
        public int Statuscode { get; set; }
        public Errors[] Errors { get; set; }
        public T Data { get; set; }
        public string SucccessMessage { get; set; }
    }

    public class Errors
    {
        public string ErrorCode { get; set; }
        public string Message { get; set; }
    }
}