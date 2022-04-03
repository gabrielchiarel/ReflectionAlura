using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ByteBank.Portal.Infra
{
    public abstract class BaseController
    {
        protected string View([CallerMemberName] string fileName = null)
        {
            //When a abstract class is used the type of this class is of who inherited 
            string className = GetType().Name.Replace("Controller", ""),
            resourceName = $"ByteBank.Portal.View.{className}.{fileName}.html";
            
            var assembly = Assembly.GetExecutingAssembly();

            var streamResource = assembly.GetManifestResourceStream(resourceName);

            var streamReader = new StreamReader(streamResource);
            var textPage = streamReader.ReadToEnd();

            return textPage;
        }
    }
}
