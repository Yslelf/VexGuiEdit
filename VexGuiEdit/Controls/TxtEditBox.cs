using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace VexGuiEdit
{
    public partial class TxtEditBox : UserControl
    {
        public TxtEditBox()
        {
            InitializeComponent();
        }
        #region Code行数显示
        public void showLineNo()
        {
            //获得当前坐标信息
            Point p = this.GuiCodeTxt.Location;
            int crntFirstIndex = this.GuiCodeTxt.GetCharIndexFromPosition(p);

            int crntFirstLine = this.GuiCodeTxt.GetLineFromCharIndex(crntFirstIndex);

            Point crntFirstPos = this.GuiCodeTxt.GetPositionFromCharIndex(crntFirstIndex);

            p.Y += this.GuiCodeTxt.Height;

            int crntLastIndex = this.GuiCodeTxt.GetCharIndexFromPosition(p);

            int crntLastLine = this.GuiCodeTxt.GetLineFromCharIndex(crntLastIndex);
            Point crntLastPos = this.GuiCodeTxt.GetPositionFromCharIndex(crntLastIndex);

            //准备画图
            Graphics g = this.LineP.CreateGraphics();

            Font font = new Font(this.GuiCodeTxt.Font, this.GuiCodeTxt.Font.Style);

            SolidBrush brush = new SolidBrush(Color.Green);

            //画图开始

            //刷新画布

            Rectangle rect = this.LineP.ClientRectangle;
            brush.Color = this.LineP.BackColor;

            g.FillRectangle(brush, 0, 0, this.LineP.ClientRectangle.Width, this.LineP.ClientRectangle.Height);

            brush.Color = Color.Black;//重置画笔颜色

            //绘制行号

            int lineSpace = 0;

            if (crntFirstLine != crntLastLine)
            {
                lineSpace = (crntLastPos.Y - crntFirstPos.Y) / (crntLastLine - crntFirstLine);

            }

            else
            {
                lineSpace = Convert.ToInt32(this.GuiCodeTxt.Font.Size);

            }

            int brushX = this.LineP.ClientRectangle.Width - Convert.ToInt32(font.Size * 3);

            int brushY = crntLastPos.Y + Convert.ToInt32(font.Size * 0.21f);//惊人的算法啊！！
            for (int i = crntLastLine; i >= crntFirstLine; i--)
            {

                g.DrawString((i + 1).ToString(), font, brush, brushX, brushY);

                brushY -= lineSpace;
            }

            g.Dispose();

            font.Dispose();

            brush.Dispose();
        }
        #endregion

        public void GuiCodeTxt_TextChanged(object sender, EventArgs e)
        {
            try
            {
                LineP.Invalidate();
                //比如 map_title    ok
                string currentTabName = Main.Mainfrm.tabControl1.TabPages[Main.Mainfrm.tabControl1.SelectedIndex].Text.Substring(Main.Mainfrm.tabControl1.TabPages[Main.Mainfrm.tabControl1.SelectedIndex].Text.IndexOf("：") + 1);  //map_title.yml [文档]
                string newName = currentTabName.Substring(0, currentTabName.LastIndexOf("[") - 5);
                if (Main.Mainfrm.tabControl1.TabPages[Main.Mainfrm.tabControl1.SelectedIndex].Tag == "image")
                {
                    Stream stream = File.Open(Main.ImagesPath + newName + ".xml", FileMode.Open, FileAccess.Write, FileShare.ReadWrite);
                    stream.Seek(0, SeekOrigin.Begin);
                    stream.SetLength(0);
                    stream.Close();
                    File.WriteAllText(Main.ImagesPath + newName + ".xml", Main.Mainfrm.tabControl1.TabPages[Main.Mainfrm.tabControl1.SelectedIndex].Controls[0].Controls[0].Text);
                    XmlMethods.ReadIm(Main.ImagesPath + newName + ".xml");
                    Main.Mainfrm.GuiPropertyGrid.SelectedObject = SGuiFrame.Im;
                }
                else if (Main.Mainfrm.tabControl1.TabPages[Main.Mainfrm.tabControl1.SelectedIndex].Tag == "button")
                {
                    Stream stream = File.Open(Main.ButtonsPath + newName + ".xml", FileMode.Open, FileAccess.Write, FileShare.ReadWrite);
                    stream.Seek(0, SeekOrigin.Begin);
                    stream.SetLength(0);
                    stream.Close();
                    File.WriteAllText(Main.ButtonsPath + newName + ".xml", Main.Mainfrm.tabControl1.TabPages[Main.Mainfrm.tabControl1.SelectedIndex].Controls[0].Controls[0].Text);
                    XmlMethods.ReadBt(Main.ButtonsPath + newName + ".xml");
                    Main.Mainfrm.GuiPropertyGrid.SelectedObject = SGuiFrame.Bt;
                }
                else if (Main.Mainfrm.tabControl1.TabPages[Main.Mainfrm.tabControl1.SelectedIndex].Tag == "text")
                {
                    Stream stream = File.Open(Main.TextsPath + newName + ".xml", FileMode.Open, FileAccess.Write, FileShare.ReadWrite);
                    stream.Seek(0, SeekOrigin.Begin);
                    stream.SetLength(0);
                    stream.Close();
                    File.WriteAllText(Main.TextsPath + newName + ".xml", Main.Mainfrm.tabControl1.TabPages[Main.Mainfrm.tabControl1.SelectedIndex].Controls[0].Controls[0].Text);
                    XmlMethods.ReadTxt(Main.TextsPath + newName + ".xml");
                    Main.Mainfrm.GuiPropertyGrid.SelectedObject = SGuiFrame.Txt;
                }
            }
            catch
            {

            }
        }

        public void GuiCodeTxt_VScroll(object sender, EventArgs e)
        {
            LineP.Invalidate();
        }

        private void LineP_Paint(object sender, PaintEventArgs e)
        {
            showLineNo();
        }
    }
}
