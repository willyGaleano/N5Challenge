﻿using System.Collections.Generic;

namespace N5.Challenge.Core.Application.Wrappers
{
    public class Response<T>
    {
        public Response() { }
        public Response(T data, string message = null)
        {
            Succeeded = true;
            Message = message;
            Data = data;
        }

        public Response(T data, bool succeeded, string message = null)
        {
            Succeeded = succeeded;
            Message = message;
            Data = data;
        }

        public Response(string message)
        {
            Succeeded = false;
            Message = message;
        }

        public bool Succeeded { get; set; }
        public string Message { get; set; }
        public List<string> Errors { get; set; }
        public T Data { get; set; }
    }
}