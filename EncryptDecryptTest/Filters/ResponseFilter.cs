using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EncryptDecryptTest.Filters
{
    public class ResponseFilter : IAlwaysRunResultFilter
    {
        public void OnResultExecuted(ResultExecutedContext context)
        {
            var str = context.HttpContext.Response.Body;
        }

        public void OnResultExecuting(ResultExecutingContext context)
        {
            var originalBodyStream = context.HttpContext.Response.Body;
            if (context.Result is ObjectResult)
            {
                ObjectResult e = (ObjectResult)context.Result;
                if (e.Value != null)
                    e.Value = Convert.ToBase64String(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(e.Value)));
                context.Result = e;
            }
        }
    }
}
