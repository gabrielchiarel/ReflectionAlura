using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ByteBank.Portal.Infra
{
    public static class Utilities
    {
        public static bool IsFile(string path)
        {
            var splitedPath = path.Split('/', StringSplitOptions.RemoveEmptyEntries);
            var lastSplit = splitedPath.Length > 0 ? splitedPath[splitedPath.Length - 1] : null;

            return string.IsNullOrWhiteSpace(lastSplit) && lastSplit.Contains('.');
        }
        public static string PathConverterToAssemblyName(string path)
        {
            var prefixAssembly = "ByteBank.Portal";
            
            var namespacePath = path.Replace('/', '.');

            return $"{prefixAssembly}{namespacePath}";
        }

        public static string GetContentType(string path)
        {
            if (path.EndsWith(".css"))
                return "text/css; charset=utf-8";
            else if (path.EndsWith(".js"))
                return "application/js; charset=utf-8";
            else
                return "text/html; charset=utf-8";

            throw new NotImplementedException("Context type not planned");
        }
    }
}
