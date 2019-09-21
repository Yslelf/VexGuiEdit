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
    public partial class ImageControl : UserControl
    {
        public ImageControl()
        {
            InitializeComponent();
            this.pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
        }
        string type = "";
        string filename = "";
        private void ImageControl_MouseClick(object sender, MouseEventArgs e)
        {
            Main.Mainfrm.ImagePanel.Refresh();
            Main.Mainfrm.ButtonPanel.Refresh();
            Main.Mainfrm.TextPanel.Refresh();
            Main.Mainfrm.SlotPanel.Refresh();
            Main.DrawDragBound(this);
            filename = this.label1.Text.Substring(0, this.label1.Text.LastIndexOf("."));
            type = this.label1.Text.Substring(label1.Text.LastIndexOf("(") + 1).Substring(0, this.label1.Text.Substring(label1.Text.LastIndexOf("(") + 1).LastIndexOf(")"));
            if(type.Equals("im"))
            {
                Main.Mainfrm.GuiComboBox.SelectedIndex = Main.Mainfrm.GuiComboBox.Items.IndexOf("图像：" + filename + ".yml");
                //XmlMethods.ReadIm(Main.ImagesPath + filename + ".xml");  //不用再读，becauseGuiComboBox有indexchanged事件3
                //Main.Mainfrm.GuiPropertyGrid.SelectedObject = SGuiFrame.Im;
            }
            else if(type.Equals("bt"))
            {
                Main.Mainfrm.GuiComboBox.SelectedIndex = Main.Mainfrm.GuiComboBox.Items.IndexOf("按钮：" + filename + ".yml");
                //XmlMethods.ReadBt(Main.ButtonsPath + filename + ".xml");
                //Main.Mainfrm.GuiPropertyGrid.SelectedObject = SGuiFrame.Bt;
            }
            type = "";
            filename = "";
        }
    }
}
