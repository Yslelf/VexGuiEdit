using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VexGuiEdit
{
    public partial class TxtControl : UserControl
    {
        public TxtControl()
        {
            InitializeComponent();
        }
        string type = "";
        string filename = "";

        private void TxtControl_MouseClick(object sender, MouseEventArgs e)
        {
            Main.Mainfrm.TextPanel.Refresh();
            Main.Mainfrm.ImagePanel.Refresh();
            Main.Mainfrm.ButtonPanel.Refresh();
            Main.Mainfrm.SlotPanel.Refresh();
            Main.DrawDragBound(this);
            filename = this.label1.Text.Substring(0, this.label1.Text.LastIndexOf("."));
            type = this.label1.Text.Substring(label1.Text.LastIndexOf("(") + 1).Substring(0, this.label1.Text.Substring(label1.Text.LastIndexOf("(") + 1).LastIndexOf(")"));
            if (type.Equals("txt"))
            {
                Main.Mainfrm.GuiComboBox.SelectedIndex = Main.Mainfrm.GuiComboBox.Items.IndexOf("文本：" + filename + ".yml");
                //XmlMethods.ReadTxt(Main.TextsPath + filename + ".xml");
                //Main.Mainfrm.GuiPropertyGrid.SelectedObject = SGuiFrame.Txt;
            }
            type = "";
            filename = "";
        }
    }
}
