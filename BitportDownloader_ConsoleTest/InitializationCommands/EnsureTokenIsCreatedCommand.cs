//using BitportDownloader.RestApi;
//using Newtonsoft.Json;
//using RestSharp;
//using RestSharp.Deserializers;
//using RestSharp.Serializers;
//using System;
//using System.Collections.Generic;
//using System.Collections.Specialized;
//using System.Configuration;
//using System.Diagnostics;
//using System.IO;
//using System.Linq;
//using System.Net;
//using System.Net.Http;
//using System.Text;
//using System.Threading.Tasks;
//using System.Web;

//namespace BitportViewer.ConsoleTest
//{
//    class EnsureTokenIsCreatedCommand : IInitializationCommand
//    {
//        public string TokenPath { get; }

//        public int Order
//        {
//            get
//            {
//                return 0;
//            }
//        }

//        public bool CanFail
//        {
//            get
//            {
//                return false;
//            }
//        }

//        public EnsureTokenIsCreatedCommand(string tokenPath)
//        {
//            TokenPath = tokenPath;
//        }

//        public void Run()
//        {
//            var token = File.Exists(TokenPath) ? JsonConvert.DeserializeObject<BitportAppToken>(File.ReadAllText(TokenPath)) : null;
//            if ((token?.access_token).IsNullOrEmpty())
//            {
//                var appSettingsPath = Path.Combine(Environment.CurrentDirectory, "appSettings.json");

//                BitportAppSettings appSettings;
//                if (File.Exists(appSettingsPath))
//                {
//                    appSettings = JsonConvert.DeserializeObject<BitportAppSettings>(File.ReadAllText(appSettingsPath));
//                }
//                else
//                {
//                    throw new FileNotFoundException($"Settings file at \"{appSettingsPath}\" is missing!");
//                }

//                var auth = new BitportAuthenticator(appSettings);

//                token = auth.GetToken();

//                File.WriteAllText(TokenPath, JsonConvert.SerializeObject(token));
//            }
//        }
//    }
//}