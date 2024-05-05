﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.Customers.Command
{
    public sealed class CommandResponse<T>
    {

        public HttpStatusCode StatusCode { get; set; }

        public string Message { get; set; }

        public T? Result { get; set; }

        public CommandResponse( HttpStatusCode statusCode, string message, T? result)
        {
            StatusCode = statusCode;
            Message = message;
            Result = result;
        }

        

    }
}
