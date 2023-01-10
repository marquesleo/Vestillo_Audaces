using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Vestillo.Lib
{
    public partial class InformacoesApi
    {
        public const string url = "http://fsutils.azurewebsites.net/api/"; 
        // Public Const url = "http://192.168.0.10:57697/api/"

        public static string ChamarAPI(string Controller, string json)
        {
            if (Debugger.IsAttached)
            {
                return FSUtils.WebApi.WebApiClient.ChamarAPIPorPostEnviandoJSON(Controller, json, url);
            }
            else
            {
                return FSUtils.WebApi.WebApiClient.ChamarAPIPorPostEnviandoJSON(Controller, json);
            }
        }
    }
}