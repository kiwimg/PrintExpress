using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Drawing.Printing;//添加命名空间
using pringApp;

namespace PrintApp
{
    public partial class Form1 : Form
    {
        AboutBox1 aboutBox1;
        Config config;
        public Form1()
        {
            InitializeComponent();
            PrintDocument print = new PrintDocument();
            foreach (string sPrint in PrinterSettings.InstalledPrinters)//获取所有打印机名称
            {
                comboBox1.Items.Add(sPrint);
               
            }
            config = new Config();
            this.label3.Text = print.PrinterSettings.PrinterName;
            string configPrint = config.ReadString("printer", "print", null);
            if (configPrint != null && configPrint.Length >0) {
                this.label5.Text = configPrint;
            }
          
        }

        private void 退出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
            System.Environment.Exit(0);
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            settingPrinter();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //隐藏窗体
            this.WindowState = FormWindowState.Minimized;
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.Visible = false;
                this.notifyIcon1.Visible = true;
            }
        }
        private void Form1_Close(object sender, EventArgs e)
        {
            
        }
        private void button1_Click_1(object sender, EventArgs e)
        {
        
            Console.WriteLine("got SelectedText" + comboBox1.SelectedItem.ToString());
          

            config.WriteString("printer", "print", comboBox1.SelectedItem.ToString());
            this.Visible = false;
            this.notifyIcon1.Visible = true;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Visible = false;
            this.notifyIcon1.Visible = true;
        }

        private void 设置ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            settingPrinter();
        }


        private void settingPrinter() {
            this.Visible = true;
            this.WindowState = FormWindowState.Normal;

            int xWidth = SystemInformation.PrimaryMonitorSize.Width;//获取显示器屏幕宽度
            int yHeight = SystemInformation.PrimaryMonitorSize.Height;//高度

            this.Location = new Point(xWidth / 2 - this.Width / 2, yHeight / 2 - this.Height / 2);//这里需要再减去窗体本身的宽度和高度的一半
            this.Show();
        }

        private void 关于ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Visible = false;

            aboutBox1 = new AboutBox1();
            aboutBox1.WindowState = FormWindowState.Normal;
            int xWidth = SystemInformation.PrimaryMonitorSize.Width;//获取显示器屏幕宽度
            int yHeight = SystemInformation.PrimaryMonitorSize.Height;//高度

            aboutBox1.Location = new Point(xWidth / 2 - this.Width / 2, yHeight / 2 - this.Height / 2);//这里需要再减去窗体本身的宽度和高度的一半
            aboutBox1.Show();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
