using System;
using System.IO;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using pringApp;

namespace PrintApp
{

    class WebServer : HttpServer
    {
        public WebServer(int port) : base(port)
        {
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override void handleGETRequest(HttpProcessor p)
        {
            p.writeSuccess();
            Response res = new Response();

            if (p.http_url.Equals("/print.html"))
            {
                p.outputStream.WriteLine("<!DOCTYPE html>");
                p.outputStream.WriteLine("<html lang='zh-CN'><head>");
                p.outputStream.WriteLine("<meta charset='UTF-8'>");
                p.outputStream.WriteLine("<script src='http://ad.ddyunf.com/resources/printer/printer.js'></script> ");
                p.outputStream.WriteLine("</head>");
                p.outputStream.WriteLine("<body>");
                p.outputStream.WriteLine("<script type=text/javascript>");
                p.outputStream.WriteLine("window.addEventListener('message', function(e){");
                p.outputStream.WriteLine("postData(e);");
                p.outputStream.WriteLine("}, false)");
                p.outputStream.WriteLine("</script>");
                p.outputStream.WriteLine("</body></html>");
            }else {
                res.stauts = ResStatusCode.NoGet.ToString();
                res.desc = "不支持get方式";
                string json = JsonConvert.SerializeObject(res);
                p.outputStream.WriteLine(json);
            }
        }

        public override void handlePOSTRequest(HttpProcessor p, StreamReader inputData)
        {
            Config config = new Config();
            string priterNameConfig = config.ReadString("printer", "print", null);
            if (priterNameConfig == null || priterNameConfig.Length == 0)
            {
                p.writeSuccess("application/json");
                Response res = new Response();
                res.stauts = ResStatusCode.NO_Printer.ToString();
                res.desc = "请先设置打印面单机器";
                string json = JsonConvert.SerializeObject(res);
                p.outputStream.WriteLine(json);
            }
            else {
                HandleDelegate handleDelegate = asynHandle;
                string data = inputData.ReadToEnd();
                try
                {
                    Console.WriteLine("======data===========================" + data);
                    JArray item = (JArray)JsonConvert.DeserializeObject(data);
                    IAsyncResult result = handleDelegate.BeginInvoke(item, null, null);
                    p.writeSuccess("application/json");
                    Response res = new Response();
                    res.stauts = ResStatusCode.Success.ToString();
                    string json = JsonConvert.SerializeObject(res);
                    p.outputStream.WriteLine(json);
                }
                catch (Newtonsoft.Json.JsonReaderException e)
                {
                    p.writeSuccess("application/json");
                    Response res = new Response();
                    res.stauts = ResStatusCode.Forma_Error.ToString();
                    res.desc = "数据格式错误";
                    string json = JsonConvert.SerializeObject(res);
                    p.outputStream.WriteLine(json);
                }
            }

           
          
        }
        public delegate void HandleDelegate(JArray item );

        public static void asynHandle(JArray item )
        {
            Config config = new Config();

            PrintSetting printSetting = new PrintSetting();
            string priterNameConfig = config.ReadString("printer", "print", null);
            if (priterNameConfig != null && priterNameConfig.Length > 0)
            {
                printSetting.priterName = priterNameConfig;
            }
           

            PrintService printService = new PrintService(printSetting, item);
            printService.Print();
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
