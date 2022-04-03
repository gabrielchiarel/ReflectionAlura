using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ByteBank.Portal.Infra
{
    public class RequestHandler
    {
        public void FileHandler(HttpListenerResponse response, string path)
        {
            var assembly = Assembly.GetExecutingAssembly();

            var resourceStream = assembly.GetManifestResourceStream(Utilities.PathConverterToAssemblyName(path));

            if (resourceStream == null)
            {
                response.StatusCode = 404;
                response.OutputStream.Close();
            }
            else
                using (resourceStream)
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

        public void ControllerHandler(HttpListenerResponse response, string path)
        {
            var sections = path.Split('/', StringSplitOptions.RemoveEmptyEntries);
            string assemblyName = "ByteBank.Portal", controllerName = sections[0], actionName = sections[1],
            controllerFullName = $"{assemblyName}.Controller.{controllerName}Controller";

            var controllerWrapper = Activator.CreateInstance(assemblyName, controllerFullName);

            var controller = controllerWrapper.Unwrap();

            var methodInfo = controller.GetType().GetMethod(actionName);

            var resultAction = (string)methodInfo.Invoke(controller, new object[0]);

            var buffer = Encoding.UTF8.GetBytes(resultAction);

            response.StatusCode = 200;
            response.ContentType = "text/html; charset=utf-8";
            response.ContentLength64 = buffer.Length;
            response.OutputStream.Write(buffer, 0, buffer.Length);

            response.OutputStream.Close();
        }
    }
}
