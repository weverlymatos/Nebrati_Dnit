﻿using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace Dnit.Core.Services.RequestProvider
{
    public class HttpRequestExceptionEx : HttpRequestException
    {
        public System.Net.HttpStatusCode HttpCode { get; }
        public HttpRequestExceptionEx(System.Net.HttpStatusCode code) : this(code, null, null)
        {
        }

        public HttpRequestExceptionEx(System.Net.HttpStatusCode code, string message) : this(code, message, null)
        {
        }

        public HttpRequestExceptionEx(System.Net.HttpStatusCode code, string message, Exception inner) : base(message,
            inner)
        {
            HttpCode = code;
        }

    }
}