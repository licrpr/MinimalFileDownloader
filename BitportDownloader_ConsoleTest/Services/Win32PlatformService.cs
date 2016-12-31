using BitportDownloader.RestApi;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;

namespace BitportDownloader.ConsoleTest
{
    public class Win32PlatformService : IPlatformService
    {
        public static void Initialize()
        {
            PlatformService.Initialize(new Win32PlatformService());
        }

        public IReadOnlyDictionary<string, string> ListenOnLocalPort(int port)
        {
            HttpListener listener = null;
            HttpListenerContext context = null;
            IReadOnlyDictionary<string, string> result = null;
            try
            {
                listener = new HttpListener();
                listener.Prefixes.Add(string.Format("http://127.0.0.1:{0}/", port));
                listener.Start();
                context = listener.GetContext();

                var query = context.Request.QueryString;
                result = query.AllKeys.ToDictionary(o => o, o => query[o]);
            }
            finally
            {
                if (context?.Response != null)
                    context.Response.Close();
                if (listener != null)
                    listener.Stop();
            }
            return result;
        }

        public void OpenBrowser(string url)
        {
            Process.Start(url);
        }
    }
}