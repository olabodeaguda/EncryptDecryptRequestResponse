using EncryptDecryptTest.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EncryptDecryptTest.Middlewares
{
    public class RequestResponseModifierMiddleware
    {
        private readonly RequestDelegate _next;

        public RequestResponseModifierMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            context.Request.EnableBuffering();
            var request = context.Request;

            var stream = request.Body;
            var originalContent = new StreamReader(stream).ReadToEnd();

            if (!string.IsNullOrEmpty(originalContent))
            {
                var modifydata = new TestModel { Name = "vbb", Hobby = "sss" };
                var json = JsonConvert.SerializeObject(modifydata);
                var requestData = Encoding.UTF8.GetBytes(json);
                stream = new MemoryStream(requestData);

                request.Body = stream;
            }
            await _next.Invoke(context);
        }
    }
}
