using System;

namespace Transferencia.Infra.CrossCutting.Http.Helper
{
    public static class ApiRoutes
    {
        public static class Account
        { 
            private static string UrlBase = "https://acessoaccount.herokuapp.com/api";
            public static string GetByNumber(string number) => $"{UrlBase}/account/{number}";
            public static string Post => $"{UrlBase}/account";
        }
    }
}
