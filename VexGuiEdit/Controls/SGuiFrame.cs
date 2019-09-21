using System;
using System.Windows.Forms;
using System.Drawing;
using System.IO;
using System.Text;
using System.Threading;
using System.Xml;

namespace VexGuiEdit
{
	/// <summary>
	/// This class implements sizing and moving functions for
	///	runtime editing of graphic controls
	/// </summary>
	public class SGuiFrame:UserControl
	{
        private const int BOX_SIZE = 8;
        private Color BOX_COLOR = Color.White;
        public static Control CurrentControl;
        public static Label[] Rectangle = new Label[8];
        private int startl;
        private int startt;
        private int startw;
        private int starth;
        private int startx;
        private int starty;
        private bool Dragging;
        private Cursor[] arrArrow = new Cursor[] {Cursors.SizeNWSE, Cursors.SizeNS,
            Cursors.SizeNESW, Cursors.SizeWE, Cursors.SizeNWSE, Cursors.SizeNS,
            Cursors.SizeNESW, Cursors.SizeWE};
        private Cursor oldCursor;
        private const int MIN_SIZE = 20;

        //public static GuiProperties Gui = new GuiProperties();
        public static ImProperties Im = new ImProperties();
        public static BtProperties Bt = new BtProperties();
        public static TxtProperties Txt = new TxtProperties();
        public SGuiFrame()
        {
            //CurrentControl = ctrl;
            for (int i = 0; i < 8; i++)
            {
                Rectangle[i] = new Label();
                Rectangle[i].TabIndex = i;
                Rectangle[i].FlatStyle = 0;
                Rectangle[i].BorderStyle = BorderStyle.FixedSingle;
                Rectangle[i].BackColor = BOX_COLOR;
                Rectangle[i].Cursor = arrArrow[i];
                Rectangle[i].Text = "";
                Rectangle[i].BringToFront();
                Rectangle[i].MouseDown += new MouseEventHandler(this.Rectangle_MouseDown);
                Rectangle[i].MouseMove += new MouseEventHandler(this.Rectangle_MouseMove);
                Rectangle[i].MouseUp += new MouseEventHandler(this.Rectangle_MouseUp);
            }
        }
        public void WireControl(Control ctl)
        {
            ctl.Click += new EventHandler(this.SelectControl);
        }
        public static int ControlW = 0;
        public static int ControlH = 0;
        public void SelectControl(object sender, EventArgs e)
        {
            try
            {
                if (CurrentControl is Control)
                {
                    CurrentControl.Cursor = oldCursor;

                    //Remove event any pre-existing event handlers appended by this class
                    CurrentControl.MouseDown -= new MouseEventHandler(this.SGuiFrame_MouseDown);
                    CurrentControl.MouseMove -= new MouseEventHandler(this.SGuiFrame_MouseMove);
                    CurrentControl.MouseUp -= new MouseEventHandler(this.SGuiFrame_MouseUp);
                    CurrentControl.DoubleClick -= new EventHandler(this.SGuiFrame_DoubleClick);
                    CurrentControl.Resize -= new EventHandler(this.SGuiFrame_Resize);
                    //CurrentControl.LostFocus -= new EventHandler(this.SGuiFrame_LostFocus);
                    //
                    CurrentControl = null;
                }

                CurrentControl = (Control)sender;
                //Add event handlers for moving the selected control around
                CurrentControl.MouseDown += new MouseEventHandler(this.SGuiFrame_MouseDown);
                CurrentControl.MouseMove += new MouseEventHandler(this.SGuiFrame_MouseMove);
                CurrentControl.MouseUp += new MouseEventHandler(this.SGuiFrame_MouseUp);
                CurrentControl.DoubleClick += new EventHandler(this.SGuiFrame_DoubleClick);
                CurrentControl.Resize += new EventHandler(this.SGuiFrame_Resize);
                //CurrentControl.LostFocus += new EventHandler(this.SGuiFrame_LostFocus);
                //Add sizing handles to Control's container (Form or PictureBox)
                for (int i = 0; i < 8; i++)
                {
                    CurrentControl.Parent.Controls.Add(Rectangle[i]);
                    Rectangle[i].BringToFront();
                }
                string Tag = (string)CurrentControl.Tag;
                string YmlName = Tag.Substring(Tag.LastIndexOf(":") + 1);

                if (Tag.StartsWith("image:"))
                {
                    //先改变GuiComboBox的值非常重要，如果等到propertygrid控件选择完对象以后再改变变量值会发生改变，触发guicomboBox IndexChanged的modify就会错误保存。
                    Main.Mainfrm.GuiComboBox.SelectedIndex = Main.Mainfrm.GuiComboBox.Items.IndexOf("图像：" + YmlName + ".yml");
                    XmlMethods.ReadIm(Main.ImagesPath + YmlName + ".xml");
                    ControlW = Convert.ToInt32(SGuiFrame.Im.XShow);
                    ControlH = Convert.ToInt32(SGuiFrame.Im.YShow);
                    Main.Mainfrm.GuiPropertyGrid.SelectedObject = Im;
                }
                else if (Tag.StartsWith("button:"))
                {
                    Main.Mainfrm.GuiComboBox.SelectedIndex = Main.Mainfrm.GuiComboBox.Items.IndexOf("按钮：" + YmlName + ".yml");
                    XmlMethods.ReadBt(Main.ButtonsPath + YmlName + ".xml");
                    ControlW = Convert.ToInt32(SGuiFrame.Bt.Width);
                    ControlH = Convert.ToInt32(SGuiFrame.Bt.High);
                    Main.Mainfrm.GuiPropertyGrid.SelectedObject = Bt;
                }
                else if (Tag.StartsWith("text:"))
                {
                    Main.Mainfrm.GuiComboBox.SelectedIndex = Main.Mainfrm.GuiComboBox.Items.IndexOf("文本：" + YmlName + ".yml");
                    XmlMethods.ReadTxt(Main.TextsPath + YmlName + ".xml");
                    Main.Mainfrm.GuiPropertyGrid.SelectedObject = Txt;
                }
                //Position sizing handles around Control
                MoveHandles();

                //Display sizing handles
                ShowHandles();

                oldCursor = CurrentControl.Cursor;
                CurrentControl.Cursor = Cursors.SizeAll;
                //CurrentControl.BringToFront();
            }
            catch(Exception ex)
            {
                MessageBox.Show("请检查xml文本编辑框内容,缺少了尖括号和标签等请手动填补上去");
            }
        }
        public void Remove()
        {
            HideHandles();
            CurrentControl.Cursor = oldCursor;
        }
        #region 隐藏/显示/移动8个框
        public static void ShowHandles()
        {
            try
            {
                if (CurrentControl != null)
                {
                    for (int i = 0; i < 8; i++)
                    {
                        Rectangle[i].Visible = true;
                    }
                }
                string Tag = (string)CurrentControl.Tag;
                string YmlName = Tag.Substring(Tag.LastIndexOf(":") + 1) + ".xml";
                if (Tag.StartsWith("image:"))
                {
                    SGuiFrame.Im.XShow = ControlW.ToString();
                    SGuiFrame.Im.YShow = ControlH.ToString();
                    SGuiFrame.Im.X = (CurrentControl.Location.X - Main.Mainfrm.Gui_X).ToString();
                    SGuiFrame.Im.Y = (CurrentControl.Location.Y - Main.Mainfrm.Gui_Y).ToString();
                    XmlMethods.ModifyIm(Main.ImagesPath + YmlName);
                    XmlMethods.ReadIm(Main.ImagesPath + YmlName);
                    Main.Mainfrm.GuiPropertyGrid.SelectedObject = Im;
                }
                else if (Tag.StartsWith("button:"))
                {
                    SGuiFrame.Bt.Width = ControlW.ToString();
                    SGuiFrame.Bt.High = ControlH.ToString();
                    SGuiFrame.Bt.X = (CurrentControl.Location.X - Main.Mainfrm.Gui_X).ToString();
                    SGuiFrame.Bt.Y = (CurrentControl.Location.Y - Main.Mainfrm.Gui_Y).ToString();
                    XmlMethods.ModifyBt(Main.ButtonsPath + YmlName);
                    XmlMethods.ReadBt(Main.ButtonsPath + YmlName);
                    Main.Mainfrm.GuiPropertyGrid.SelectedObject = Bt;
                }
                else if (Tag.StartsWith("text:"))
                {
                    SGuiFrame.Txt.X = (CurrentControl.Location.X - Main.Mainfrm.Gui_X).ToString();
                    SGuiFrame.Txt.Y = (CurrentControl.Location.Y - Main.Mainfrm.Gui_Y).ToString();
                    XmlMethods.ModifyTxt(Main.TextsPath + YmlName);
                    XmlMethods.ReadTxt(Main.TextsPath + YmlName);
                    Main.Mainfrm.GuiPropertyGrid.SelectedObject = Txt;
                }
            }
            catch
            {
                //MessageBox.Show("66");
            }
        }
        public void HideHandles()
        {
            try
            {
                for (int i = 0; i < 8; i++)
                {
                    Rectangle[i].Visible = false;
                }
            }
            catch
            {

            }
        }
        public void MoveHandles()
        {
            try
            {
                int sX = CurrentControl.Left - BOX_SIZE;
                int sY = CurrentControl.Top - BOX_SIZE;
                int sW = CurrentControl.Width + BOX_SIZE;
                int sH = CurrentControl.Height + BOX_SIZE;
                int hB = BOX_SIZE / 2;
                int[] arrPosX = new int[] {sX+hB, sX + sW / 2, sX + sW-hB, sX + sW-hB,
            sX + sW-hB, sX + sW / 2, sX+hB, sX+hB};
                int[] arrPosY = new int[] {sY+hB, sY+hB, sY+hB, sY + sH / 2, sY + sH-hB,
            sY + sH-hB, sY + sH-hB, sY + sH / 2};
                for (int i = 0; i < 8; i++)
                {
                    Rectangle[i].SetBounds(arrPosX[i], arrPosY[i], BOX_SIZE, BOX_SIZE);
                }
            }
            catch
            {

            }
        }
        #endregion
        #region 八个框事件
        private void Rectangle_MouseDown(object sender, MouseEventArgs e)
        {
            Dragging = true;
		    startl = CurrentControl.Left;
		    startt = CurrentControl.Top;
            startw = CurrentControl.Width;
            starth = CurrentControl.Height;
		    HideHandles();
	    }
        private void Rectangle_MouseMove(object sender, MouseEventArgs e)
        {
            int l = CurrentControl.Left;
            int w = CurrentControl.Width;
            int t = CurrentControl.Top;
            int h = CurrentControl.Height;
            if (Dragging)
            {
                switch (((Label)sender).TabIndex)
                {
                    case 0: // Dragging top-left sizing box
                        l = startl + e.X < startl + startw - MIN_SIZE ? startl + e.X : startl + startw - MIN_SIZE;
                        t = startt + e.Y < startt + starth - MIN_SIZE ? startt + e.Y : startt + starth - MIN_SIZE;
                        w = startl + startw - CurrentControl.Left;
                        h = startt + starth - CurrentControl.Top;
                        break;
                    case 1: // Dragging top-center sizing box
                        t = startt + e.Y < startt + starth - MIN_SIZE ? startt + e.Y : startt + starth - MIN_SIZE;
                        h = startt + starth - CurrentControl.Top;
                        break;
                    case 2: // Dragging top-right sizing box
                        w = startw + e.X > MIN_SIZE ? startw + e.X : MIN_SIZE;
                        t = startt + e.Y < startt + starth - MIN_SIZE ? startt + e.Y : startt + starth - MIN_SIZE;
                        h = startt + starth - CurrentControl.Top;
                        break;
                    case 3: // Dragging right-middle sizing box
                        w = startw + e.X > MIN_SIZE ? startw + e.X : MIN_SIZE;
                        break;
                    case 4: // Dragging right-bottom sizing box
                        w = startw + e.X > MIN_SIZE ? startw + e.X : MIN_SIZE;
                        h = starth + e.Y > MIN_SIZE ? starth + e.Y : MIN_SIZE;
                        break;
                    case 5: // Dragging center-bottom sizing box
                        h = starth + e.Y > MIN_SIZE ? starth + e.Y : MIN_SIZE;
                        break;
                    case 6: // Dragging left-bottom sizing box
                        l = startl + e.X < startl + startw - MIN_SIZE ? startl + e.X : startl + startw - MIN_SIZE;
                        w = startl + startw - CurrentControl.Left;
                        h = starth + e.Y > MIN_SIZE ? starth + e.Y : MIN_SIZE;
                        break;
                    case 7: // Dragging left-middle sizing box
                        l = startl + e.X < startl + startw - MIN_SIZE ? startl + e.X : startl + startw - MIN_SIZE;
                        w = startl + startw - CurrentControl.Left;
                        break;
                }
                l = (l < 0) ? 0 : l;
                t = (t < 0) ? 0 : t;
                CurrentControl.SetBounds(l, t, w, h);
            }
        }
        private void Rectangle_MouseUp(object sender, MouseEventArgs e)
        {
            Dragging = false;
            MoveHandles();
            ShowHandles();
	    }
        #endregion
        private void SGuiFrame_MouseDown(object sender, MouseEventArgs e)
        {
            Dragging = true;
		    startx = e.X;
		    starty = e.Y;
		    HideHandles();
	    }
        private void SGuiFrame_MouseMove(object sender, MouseEventArgs e)
        {
            try
            {
                Main.Mainfrm.LbX.Text = "X：" + (CurrentControl.Location.X - Main.Mainfrm.Gui_X);
                Main.Mainfrm.LbY.Text = "Y：" + (CurrentControl.Location.Y - Main.Mainfrm.Gui_Y);
                if (Dragging)
                {
                    SGuiFrame.DrawDragBound(CurrentControl);
                    int l = CurrentControl.Left + e.X - startx;
                    int t = CurrentControl.Top + e.Y - starty;
                    int w = CurrentControl.Width;
                    int h = CurrentControl.Height;
                    l = (l < 0) ? 0 : ((l + w > CurrentControl.Parent.ClientRectangle.Width) ?
                    CurrentControl.Parent.ClientRectangle.Width - w : l);
                    t = (t < 0) ? 0 : ((t + h > CurrentControl.Parent.ClientRectangle.Height) ?
                    CurrentControl.Parent.ClientRectangle.Height - h : t);
                    CurrentControl.Left = l;
                    CurrentControl.Top = t;
                }
            }
            catch(Exception ex)
            {
                //MessageBox.Show(ex.Message);
            }
        }
        //ImProperties im = new ImProperties();
	    private void SGuiFrame_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                CurrentControl.Refresh();
                Dragging = false;
                MoveHandles();
                ShowHandles();
            }
            catch
            {
                
            }
        }
        //TxtEditBox txtEditBox = new TxtEditBox();
        private void SGuiFrame_Resize(object sender,EventArgs e)
        {
            ControlW = CurrentControl.Width;
            ControlH = CurrentControl.Height;
            Main.Mainfrm.LbW.Text = "Width：" + CurrentControl.Width;
            Main.Mainfrm.LbH.Text = "Height：" + CurrentControl.Height;
        }
        private void SGuiFrame_DoubleClick(object sender,EventArgs e)
        {
            try
            {
                string Tag = (string)CurrentControl.Tag;  //tag   gui:map
                string Name = CurrentControl.Name.Substring(CurrentControl.Name.LastIndexOf(".") + 1);
                string YmlName = Tag.Substring(Tag.LastIndexOf(":") + 1) + ".xml";  //map.xml
                string TabName = "";
                if (Tag.StartsWith("image:"))
                {
                    TabName = "图像：" + YmlName + " [文档]";
                }
                else if (Tag.StartsWith("button:"))
                {
                    TabName = "按钮：" + YmlName + " [文档]";
                }
                else if (Tag.StartsWith("text:"))
                {
                    TabName = "文本：" + YmlName + " [文档]";
                }
                foreach (TabPage tab in Main.Mainfrm.tabControl1.TabPages)
                {
                    if (tab.Text == TabName)
                    {
                        Main.Mainfrm.tabControl1.SelectedTab = tab;
                        return;
                    }
                }
                TabPage TxtTab = new TabPage();
                if (Tag.StartsWith("image:"))
                {
                    TxtTab.Text = "图像：" + YmlName + " [文档]";
                    TxtTab.Tag = "image";
                    TxtTab.Name = "im";
                }
                else if (Tag.StartsWith("button:"))
                {
                    TxtTab.Text = "按钮：" + YmlName + " [文档]";
                    TxtTab.Tag = "button";
                    TxtTab.Name = "bt";
                }
                else if (Tag.StartsWith("text:"))
                {
                    TxtTab.Text = "文本：" + YmlName + " [文档]";
                    TxtTab.Tag = "text";
                    TxtTab.Name = "txt";
                }
                TxtEditBox txtEditBox = new TxtEditBox();
                txtEditBox.Location = new Point(0, 0);
                txtEditBox.AutoSize = true;
                txtEditBox.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top | AnchorStyles.Bottom;
                //txtEditBox.Size = new Size(854, 674);
                TxtTab.Controls.Add(txtEditBox);
                Main.Mainfrm.tabControl1.TabPages.Add(TxtTab);
                Main.Mainfrm.tabControl1.SelectedTab = TxtTab;
                Main.Mainfrm.窗口WToolStripMenuItem.DropDownItems.Add(calcI() + " " + TxtTab.Text);

                if (Tag.StartsWith("image:"))
                {
                    using (FileStream fs = new FileStream(Main.ImagesPath + YmlName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                    {
                        using (StreamReader reader = new StreamReader(fs, Encoding.UTF8))
                        {
                            txtEditBox.GuiCodeTxt.Text = reader.ReadToEnd();
                            reader.Close();
                        }
                        fs.Close();
                    }
                }
                else if (Tag.StartsWith("button:"))
                {
                    using (FileStream fs = new FileStream(Main.ButtonsPath + YmlName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                    {
                        using (StreamReader reader = new StreamReader(fs, Encoding.UTF8))
                        {
                            txtEditBox.GuiCodeTxt.Text = reader.ReadToEnd();
                            reader.Close();
                        }
                        fs.Close();
                    }
                }
                else if (Tag.StartsWith("text:"))
                {
                    using (FileStream fs = new FileStream(Main.TextsPath + YmlName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                    {
                        using (StreamReader reader = new StreamReader(fs, Encoding.UTF8))
                        {
                            txtEditBox.GuiCodeTxt.Text = reader.ReadToEnd();
                            reader.Close();
                        }
                        fs.Close();
                    }
                }
            }
            catch
            {

            }
        }
        public int calcI()
        {
            if (Main.Mainfrm.i == Main.Mainfrm.i)
            {
                Main.Mainfrm.i += 1;
            }
            return Main.Mainfrm.i;
        }
        public static void DrawDragBound(Control ctrl)
        {
            //ctrl.Update();
            Graphics g = ctrl.CreateGraphics();
            int width = ctrl.Width;
            int height = ctrl.Height;
            Point[] ps = new Point[5] { new Point(0, 0), new Point(width - 1, 0), new Point(width - 1, height - 1), new Point(0, height - 1), new Point(0, 0) };
            g.DrawLines(new Pen(Color.Red), ps);
        }
    }
}

