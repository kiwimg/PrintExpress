using System;
using System.Windows.Forms;
using System.Threading;
using System.Net.NetworkInformation;
using System.Net;

namespace PrintApp
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            bool createNew;
            using (System.Threading.Mutex m = new System.Threading.Mutex(true, Application.ProductName, out createNew))
            {
                if (createNew)
                {
                    if (PortInUse(8088))
                    {
                        MessageBox.Show("系统8088端口被占用", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else {
                        Application.EnableVisualStyles();
                        Application.SetCompatibleTextRenderingDefault(false);
                        HttpServer httpServer = new WebServer(8088);

                        Thread thread = new Thread(new ThreadStart(httpServer.listen));
                        thread.Start();
                        Application.Run(new Form1());
                    }
                } 
                else
                {
                    MessageBox.Show("打印面单程序已经启动","提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        static bool PortInUse(int port)
        {
            bool inUse = false;

            IPGlobalProperties ipProperties = IPGlobalProperties.GetIPGlobalProperties();
            IPEndPoint[] ipEndPoints = ipProperties.GetActiveTcpListeners();

            foreach (IPEndPoint endPoint in ipEndPoints)
            {
                if (endPoint.Port == port)
                {
                    inUse = true;
                    break;
                }
            }
            return inUse;
        }
    }
}