﻿using System;
using System.Windows.Forms;
using System.Drawing;

namespace VexGuiEdit
{
    class FGuiFrame : UserControl
    {
        private const int BOX_SIZE = 8;
        private Color BOX_COLOR = Color.White;
        public static Control m_control;
        public static Label[] lbl = new Label[8];
        private int startl;
        private int startt;
        private int startw;
        private int starth;
        private int startx;
        private int starty;
        private bool dragging;
        private Cursor[] arrArrow = new Cursor[] {Cursors.SizeNWSE, Cursors.SizeNS,
            Cursors.SizeNESW, Cursors.SizeWE, Cursors.SizeNWSE, Cursors.SizeNS,
            Cursors.SizeNESW, Cursors.SizeWE};
        private Cursor oldCursor;
        public static GuiProperties gui = new GuiProperties();
        private const int MIN_SIZE = 20;

        public FGuiFrame()
        {
            for (int i = 0; i < 8; i++)
            {
                lbl[i] = new Label();
                lbl[i].TabIndex = i;
                lbl[i].FlatStyle = 0;
                lbl[i].BorderStyle = BorderStyle.FixedSingle;
                lbl[i].BackColor = BOX_COLOR;
                lbl[i].Cursor = arrArrow[i];
                lbl[i].Text = "";
                lbl[i].BringToFront();
                lbl[i].MouseDown += new MouseEventHandler(this.lbl_MouseDown);
                lbl[i].MouseMove += new MouseEventHandler(this.lbl_MouseMove);
                lbl[i].MouseUp += new MouseEventHandler(this.lbl_MouseUp);
            }
        }


        public void WireControl(Control ctl)
        {
            ctl.Click += new EventHandler(this.SelectControl);

        }


        /////////////////////////////////////////////////////////////////
        // PRIVATE METHODS
        /////////////////////////////////////////////////////////////////

        //
        // Attaches a pick box to the sender Control
        //
        public static string FGuiPath = "";
        private void SelectControl(object sender, EventArgs e)
        {
            FGuiPath = sender.GetType().GetProperty("ImageLocation").GetValue(sender).ToString();
            //Main main = new Main();
            //GuiProperty guiProperty = new GuiProperty();
            //main.propertyGrid1.SelectedObject = guiProperty;
            if (m_control is Control)
            {
                m_control.Cursor = oldCursor;

                //Remove event any pre-existing event handlers appended by this class
                m_control.MouseDown -= new MouseEventHandler(this.ctl_MouseDown);
                m_control.MouseMove -= new MouseEventHandler(this.ctl_MouseMove);
                m_control.MouseUp -= new MouseEventHandler(this.ctl_MouseUp);
                //m_control.MouseClick -= new MouseEventHandler(this.ctl_MouseClick);
                m_control = null;
            }

            m_control = (Control)sender;
            //Add event handlers for moving the selected control around
            m_control.MouseDown += new MouseEventHandler(this.ctl_MouseDown);
            m_control.MouseMove += new MouseEventHandler(this.ctl_MouseMove);
            m_control.MouseUp += new MouseEventHandler(this.ctl_MouseUp);
            m_control.Resize += new EventHandler(this.ctl_Resize);
            //m_control.MouseClick += new MouseEventHandler(this.ctl_MouseClick);

            //Add sizing handles to Control's container (Form or PictureBox)
            for (int i = 0; i < 8; i++)
            {
                m_control.Parent.Controls.Add(lbl[i]);
                lbl[i].BringToFront();
            }
            string Tag = (string)m_control.Tag;
            string YmlName = Tag.Substring(Tag.LastIndexOf(":") + 1);
            try
            {
                if (Tag.StartsWith("gui:"))
                {
                    Main.Mainfrm.GuiComboBox.SelectedIndex = Main.Mainfrm.GuiComboBox.Items.IndexOf("GUI：" + YmlName + ".yml");
                    XmlMethods.ReadGui(Main.GuisPath + YmlName + ".xml");
                    Main.Mainfrm.GuiPropertyGrid.SelectedObject = gui;
                }
            }
            catch { }
            //Position sizing handles around Control
            MoveHandles();

            //Display sizing handles
            ShowHandles();

            oldCursor = m_control.Cursor;
            //m_control.Cursor = Cursors.SizeAll;
            Main.Mainfrm.LbX.Text = "X：0";
            Main.Mainfrm.LbY.Text = "Y：0";
        }

        public void Remove()
        {
            HideHandles();
            m_control.Cursor = oldCursor;
        }

        private void ShowHandles()
        {
            if (m_control != null)
            {
                for (int i = 0; i < 8; i++)
                {
                    lbl[i].Visible = true;
                }
            }
            string Tag = (string)m_control.Tag;
            string YmlName = Tag.Substring(Tag.LastIndexOf(":") + 1) + ".xml";
            //MessageBox.Show(YmlName);
            if (Tag.StartsWith("gui:"))
            {
                FGuiFrame.gui.XShow = m_control.Width.ToString();
                FGuiFrame.gui.YShow = m_control.Height.ToString();
                FGuiFrame.gui.X = (m_control.Location.X - Main.Mainfrm.Gui_X).ToString();
                FGuiFrame.gui.Y = (m_control.Location.Y - Main.Mainfrm.Gui_Y).ToString();
                XmlMethods.ModifyGui(Main.GuisPath + YmlName);
                XmlMethods.ReadGui(Main.GuisPath + YmlName);
                Main.Mainfrm.GuiPropertyGrid.SelectedObject = gui;
            }
        }

        private void HideHandles()
        {
            for (int i = 0; i < 8; i++)
            {
                lbl[i].Visible = false;
            }
        }

        private void MoveHandles()
        {
            int sX = m_control.Left - BOX_SIZE;
            int sY = m_control.Top - BOX_SIZE;
            int sW = m_control.Width + BOX_SIZE;
            int sH = m_control.Height + BOX_SIZE;
            int hB = BOX_SIZE / 2;
            int[] arrPosX = new int[] {sX+hB, sX + sW / 2, sX + sW-hB, sX + sW-hB,
            sX + sW-hB, sX + sW / 2, sX+hB, sX+hB};
            int[] arrPosY = new int[] {sY+hB, sY+hB, sY+hB, sY + sH / 2, sY + sH-hB,
            sY + sH-hB, sY + sH-hB, sY + sH / 2};
            for (int i = 0; i < 8; i++)
                lbl[i].SetBounds(arrPosX[i], arrPosY[i], BOX_SIZE, BOX_SIZE);
        }
        private void lbl_MouseDown(object sender, MouseEventArgs e)
        {
            dragging = true;
            startl = m_control.Left;
            startt = m_control.Top;
            startw = m_control.Width;
            starth = m_control.Height;
            HideHandles();
        }
        private void lbl_MouseMove(object sender, MouseEventArgs e)
        {
            int l = m_control.Left;
            int w = m_control.Width;
            int t = m_control.Top;
            int h = m_control.Height;
            if (dragging)
            {
                switch (((Label)sender).TabIndex)
                {
                    //case 0: // Dragging top-left sizing box
                    //    l = startl + e.X < startl + startw - MIN_SIZE ? startl + e.X : startl + startw - MIN_SIZE;
                    //    t = startt + e.Y < startt + starth - MIN_SIZE ? startt + e.Y : startt + starth - MIN_SIZE;
                    //    w = startl + startw - m_control.Left;
                    //    h = startt + starth - m_control.Top;
                    //    break;
                    //case 1: // Dragging top-center sizing box
                    //    t = startt + e.Y < startt + starth - MIN_SIZE ? startt + e.Y : startt + starth - MIN_SIZE;
                    //    h = startt + starth - m_control.Top;
                    //    break;
                    //case 2: // Dragging top-right sizing box
                    //    w = startw + e.X > MIN_SIZE ? startw + e.X : MIN_SIZE;
                    //    t = startt + e.Y < startt + starth - MIN_SIZE ? startt + e.Y : startt + starth - MIN_SIZE;
                    //    h = startt + starth - m_control.Top;
                    //    break;
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
                        //case 6: // Dragging left-bottom sizing box
                        //    l = startl + e.X < startl + startw - MIN_SIZE ? startl + e.X : startl + startw - MIN_SIZE;
                        //    w = startl + startw - m_control.Left;
                        //    h = starth + e.Y > MIN_SIZE ? starth + e.Y : MIN_SIZE;
                        //    break;
                        //case 7: // Dragging left-middle sizing box
                        //    l = startl + e.X < startl + startw - MIN_SIZE ? startl + e.X : startl + startw - MIN_SIZE;
                        //    w = startl + startw - m_control.Left;
                        //    break;
                }
                l = (l < 0) ? 0 : l;
                t = (t < 0) ? 0 : t;
                m_control.SetBounds(l, t, w, h);
            }
        }
        private void lbl_MouseUp(object sender, MouseEventArgs e)
        {
            dragging = false;
            MoveHandles();
            ShowHandles();
        }

        private void ctl_MouseDown(object sender, MouseEventArgs e)
        {
            dragging = true;
            startx = e.X;
            starty = e.Y;
            HideHandles();
        }
        //private void ctl_MouseClick(object sender,MouseEventArgs e)
        //{
        //    Main main = new Main();
        //    GuiProperty guiProperty = new GuiProperty();
        //    main.propertyGrid1.SelectedObject = guiProperty;
        //}
        private void ctl_MouseMove(object sender, MouseEventArgs e)
        {
            //if (dragging)
            //{
            //    int l = m_control.Left + e.X - startx;
            //    int t = m_control.Top + e.Y - starty;
            //    int w = m_control.Width;
            //    int h = m_control.Height;
            //    l = (l < 0) ? 0 : ((l + w > m_control.Parent.ClientRectangle.Width) ?
            //      m_control.Parent.ClientRectangle.Width - w : l);
            //    t = (t < 0) ? 0 : ((t + h > m_control.Parent.ClientRectangle.Height) ?
            //    m_control.Parent.ClientRectangle.Height - h : t);
            //    m_control.Left = l;
            //    m_control.Top = t;
            //}
        }
        private void ctl_MouseUp(object sender, MouseEventArgs e)
        {
            dragging = false;
            MoveHandles();
            ShowHandles();
        }
        public void ctl_Resize(object sender, EventArgs e)
        {
            Main.Mainfrm.LbW.Text = "Width：" + m_control.Width.ToString();
            Main.Mainfrm.LbH.Text = "Height：" + m_control.Height.ToString();
        }
    }
}
