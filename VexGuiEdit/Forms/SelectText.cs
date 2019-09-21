using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VexGuiEdit
{
    public partial class SelectText : Form
    {
        public static string SelectedText = "";
        public static string TextName = "";
        public SelectText()
        {
            InitializeComponent();
            this.ControlBox = false;
        }

        private void SelectText_FormClosing(object sender, FormClosingEventArgs e)
        {
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            if (richTextBox1.Text == "" || textBox1.Text == "")
            {
                MessageBox.Show("请填写完整！");
            }
            if (richTextBox1.Text != "" || textBox1.Text != "")
            {
                SelectedText = richTextBox1.Text;
                TextName = textBox1.Text;
                this.Close();
            }
        }
    }
}
