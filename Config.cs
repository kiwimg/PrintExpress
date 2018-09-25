using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace pringApp
{
    class Config
    {
        string filePath = Application.StartupPath + "\\Config.ini";
        #region API函数声明

        [DllImport("kernel32")]//返回0表示失败，非0为成功
        private static extern long WritePrivateProfileString(string section, string key,
            string val, string filePath);

        [DllImport("kernel32")]//返回取得字符串缓冲区的长度
        private static extern long GetPrivateProfileString(string section, string key,
            string def, StringBuilder retVal, int size, string filePath);
        #endregion

        public Config() { }

        public string ReadString(string section, string name, string def)
        {
            StringBuilder vRetSb = new StringBuilder(2048);
      

            GetPrivateProfileString(section, name, def, vRetSb, 2048, filePath);
            return vRetSb.ToString();
        }

        public void WriteString(string section, string name, string strVal)
        {
            WritePrivateProfileString(section, name, strVal, filePath);
        }
    }
}
