using System;
using ByteBank.Portal.Infra;

namespace ByteBank.Portal
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var prefixes = new string[] { "http://localhost:5010/" };
            WebApplication webApplication = new(prefixes);
            webApplication.Init();
        }
    }
}
