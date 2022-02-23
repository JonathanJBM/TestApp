using System;

namespace Test.Models.Communication
{
    public class Response
    {
        public StatusCode Status { get; set; }
        public bool IsSuccess { get; set; }
        public object Message { get; set; }
        public object Content { get; set; }
    }
}
