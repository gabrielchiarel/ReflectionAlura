using ByteBank.Portal.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ByteBank.Portal.Infra
{
    public class WebApplication
    {
        private readonly string[] _prefixes;
        public WebApplication(string[] prefixes)
        {
            if (prefixes == null)
                throw new ArgumentNullException(nameof(prefixes));
            _prefixes = prefixes;
        }
        public void Init()
        {
            while (true)
                ManipulateRequest();
        }
    
        private void ManipulateRequest()
        {
            var httpListener = new HttpListener();

            foreach (var prefix in _prefixes)
                httpListener.Prefixes.Add(prefix);

            httpListener.Start();

            //lock application until a request come up in our app
            var context = httpListener.GetContext();
            var request = context.Request;
            var response = context.Response;

            // indicates where the repository  is trying to access.
            var path = request.Url.AbsolutePath;

            if (Utilities.IsFile(path))
            {
                var assembly = Assembly.GetExecutingAssembly();

                var resourceStream = assembly.GetManifestResourceStream(Utilities.PathConverterToAssemblyName(path));

                if (resourceStream == null)
                {
                    response.StatusCode = 404;
                    response.OutputStream.Close();
                }
                else
                {
                    var bytesResource = new byte[resourceStream.Length];

                    resourceStream.Read(bytesResource, 0, (int)resourceStream.Length);

                    response.ContentType = Utilities.GetContentType(path);
                    response.StatusCode = 200;
                    response.ContentLength64 = resourceStream.Length;

                    response.OutputStream.Write(bytesResource, 0, bytesResource.Length);

                    response.Close();
                }
            }
            else if (path == "/Cambio/MXN")
            {
                var controller = new ExchangeController();
                var pageContent = controller.GetMXN();

                var fileBuffer = Encoding.UTF8.GetBytes(pageContent);

                response.StatusCode = 200;
                response.ContentType = "text/html; charset=utf-8";
                response.ContentLength64 = fileBuffer.Length;
                response.OutputStream.Write(fileBuffer, 0, fileBuffer.Length);

                response.OutputStream.Close();
            }
            else if (path == "/Cambio/USD")
            {
                var controller = new ExchangeController();
                var pageContent = controller.GetUSD();

                var fileBuffer = Encoding.UTF8.GetBytes(pageContent);

                response.StatusCode = 200;
                response.ContentType = "text/html; charset=utf-8";
                response.ContentLength64 = fileBuffer.Length;
                response.OutputStream.Write(fileBuffer, 0, fileBuffer.Length);

                response.OutputStream.Close();
            }


            httpListener.Stop();
        }
    }
}
