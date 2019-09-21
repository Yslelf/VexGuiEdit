using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VexGuiEdit
{
    public partial class SelectBtImTxt : Form
    {
        public static string SelectedItem = "";
        public static string SelectFileName = "";
        public SelectBtImTxt()
        {
            InitializeComponent();
        }

        private void SelectBtImTxt_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 0;
        }

        private void SelectBtImTxt_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (SelectedItem == "" || SelectFileName == "")  
            {
                MessageBox.Show("你还确定选择或者填写文件名");
                e.Cancel = true;
            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            SelectedItem = comboBox1.SelectedItem.ToString();
            SelectFileName = textBox1.Text;
            this.Close();
        }
    }
}
