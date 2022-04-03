using ByteBank.Portal.Controller;
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

            var handler = new RequestHandler();

            if (Utilities.IsFile(path))
            {
                handler.FileHandler(response, path);
            }
            else
            {
                handler.ControllerHandler(response, path);
            }

            httpListener.Stop();
        }
    }
}
