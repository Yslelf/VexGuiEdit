using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;

namespace VexGuiEdit
{
    public partial class Welcome : Form
    {
        public Welcome()
        {
            InitializeComponent();
        }
        private void Button2_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog browserClient = new FolderBrowserDialog();
            if (browserClient.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = browserClient.SelectedPath;
            }
        }

        private void Welcome_Load(object sender, EventArgs e)
        {
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog browserServer = new FolderBrowserDialog();
            if (browserServer.ShowDialog() == DialogResult.OK)
            {
                textBox2.Text = browserServer.SelectedPath;
            }
        }
        int i = 0;
        private void Button1_Click(object sender, EventArgs e)
        {
            i = 1;  //set 1就不会触发FormClosing事件
            if (textBox1.Text != "" && textBox2.Text != "")
            {
                if (textBox1.Text.IndexOf("textures") != -1 && textBox2.Text.IndexOf("VexView") != -1) 
                {
                    XmlDocument xml = new XmlDocument();
                    xml.Load(Main.CurrentDisk + "VgeXmlPath\\vge.xml");
                    XmlNode xmlnode = xml.SelectSingleNode("Path");
                    XmlNodeList xmlnodeList = xmlnode.ChildNodes;
                    xmlnodeList.Item(0).InnerText = textBox1.Text;
                    xmlnodeList.Item(1).InnerText = textBox2.Text;
                    xml.Save(Main.CurrentDisk + "VgeXmlPath\\vge.xml");
                    this.Close();
                }
                else
                {
                    MessageBox.Show("请检查路径，将客户端vv路径选到textures目录下，将服务端vv路径选到VexView目录下");
                }
            }
            else
            {
                MessageBox.Show("路径为空，请选择好客户端，服务端VexView路径");
            }
        }

        private void Welcome_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (i != 1)
            {
                if (MessageBox.Show("是否关闭程序", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    Process.GetCurrentProcess().Kill();
                }
                else
                {
                    e.Cancel = true;
                }
            }
        }
    }
}
