using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VideoBrowser;

namespace VideoBrowser.UI
{
    public partial class MenuTabControl : System.Windows.Forms.TabControl
    {
        public MenuTabControl()
        {
            InitializeComponent();
            TabSet();

        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            this.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.tabMenu_DrawItem);
            this.DrawMode = TabDrawMode.OwnerDrawFixed;
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MainTabControl_MouseDown);
            this.ResumeLayout(false);
        }

        public void AddPage(MouseEventArgs e,TabPage page)
        {
            if (e.Button == MouseButtons.Left)
            {
                int x = e.X, y = e.Y;
                Rectangle r = this.GetTabRect(0);
                int tx = (int)((r.Width * this.TabCount));
                r.Offset(tx, 0);
                r.Width = 30;
                r.Height = 30;
                int longw=((this.TabCount+1) * this.ItemSize.Width + 30);
                if (this.Width < longw)
                {
                    page.Dispose();
                    return;
                }
                bool isAdd = x > r.X && x < r.Right && y > r.Y && y < r.Bottom;
                if (isAdd)
                {
                    this.Controls.Add(page);
                }
                else
                {
                    page.Dispose();
                }
            }
        }

        Color SelectedColor = Color.LightSkyBlue;
        Color MoveColor = Color.White;
        Color FontColor = Color.Black;
        int TextLeft = 10;
        [Browsable(true)]
        [Description("选项卡标题左边距"), Category("TextLeft"), DefaultValue(typeof(Int32), "10")]
        public int TitleTextLeft
        {
            get
            {
                return TextLeft;
            }
            set
            {
                this.TextLeft = value;
            }

        }

        [Browsable(true)]
        [Description("选项卡标题字体颜色"), Category("TitleColor"), DefaultValue(typeof(Color), "Black")]
        public Color TitleFontColor
        {
            get
            {
                return FontColor;
            }
            set
            {
                this.FontColor = value;
            }

        }

        [Browsable(true)]
        [Description("选项卡标题字体选中颜色"), Category("TitleColor"), DefaultValue(typeof(Color), "LightSkyBlue")]
        public Color TitleSelectedColor
        {
            get
            {
                return SelectedColor;
            }
            set
            {
                this.SelectedColor = value;
            }

        }

        [Browsable(true)]
        [Description("选项卡标题字体悬浮颜色"), Category("TitleColor"), DefaultValue(typeof(Color), "White")]
        public Color TitleMoveColor
        {
            get
            {
                return MoveColor;
            }
            set
            {
                this.MoveColor = value;
            }

        }

        /// <summary>
        /// 设定控件绘制模式
        /// </summary>
        private void TabSet()
        {
            this.DrawMode = TabDrawMode.OwnerDrawFixed;
            this.SizeMode = TabSizeMode.Fixed;
            this.Multiline = true;
            this.ItemSize = new Size(100, 28);

        }

        protected override void OnPaint(PaintEventArgs e)
        {
            StringFormat sf = new StringFormat();//封装文本布局信息 
            sf.LineAlignment = StringAlignment.Center;
            sf.Alignment = StringAlignment.Near;
            for (int i = 0; i < this.TabCount; i++)
            {

                Graphics g = e.Graphics;
                if (this.SelectedIndex == i)
                {
                    g.FillRectangle(new SolidBrush(MoveColor), this.GetTabRect(i));
                }
                SolidBrush brush = new SolidBrush(FontColor);
                RectangleF rect = GetTabRect(i);
                rect.X += TextLeft;
                g.DrawString(this.Controls[i].Text, this.Font, brush, rect, sf);
                using (Pen objpen = new Pen(Color.Black))
                {
                    int tx =(int)(rect.X+(rect.Width - 30));
                    rect.X = tx-2;
                    Point p5 = new Point(tx, 8);
                    Font font = new System.Drawing.Font("微软雅黑",12);
                    g.DrawString("〇", font, brush, rect, sf);
                    font = new System.Drawing.Font("微软雅黑", 11);
                    rect.X = tx + 2;
                    rect.Y = rect.Y - 1;
                    g.DrawString("×", font, brush, rect, sf);
                }

            }
           
            //using (Pen objpen = new Pen(Color.Black))
            //{
            //    RectangleF rect = GetTabRect(0);
            //    int tx = (int)(8+(rect.Width*this.TabCount));
            //    Point p5 = new Point(tx, 8);
            //    e.Graphics.DrawRectangle(objpen, tx-6, 2, rect.Height-1, rect.Height-1);
            //    SolidBrush brush = new SolidBrush(Color.Black);
            //    Font font = new System.Drawing.Font("微软雅黑", 16);
            //    p5.X = p5.X - 4;
            //    p5.Y = p5.Y - 8;
            //    e.Graphics.DrawString("+", font, brush, p5);
            //}

        }


        public override Rectangle DisplayRectangle
        {
            get
            {
                Rectangle rect = base.DisplayRectangle;
                return new Rectangle(rect.Left - 2, rect.Top - 2, rect.Width + 4, rect.Height + 5);
            }
        }


        int index = -1;
        protected override void OnMouseMove(MouseEventArgs e)
        {
            int Count = 0;
            try
            {
                Graphics g = this.CreateGraphics();
                SolidBrush brush = new SolidBrush(FontColor);
                StringFormat sf = new StringFormat();//封装文本布局信息 
                sf.LineAlignment = StringAlignment.Center;
                sf.Alignment = StringAlignment.Near;



                for (int i = 0; i < this.TabPages.Count; i++)
                {
                    
                    TabPage tp = this.TabPages[i];
                    
                    if (this.GetTabRect(i).Contains(e.Location) && tp != this.SelectedTab)
                    {
                        if (index != i)
                        {
                            if (Count == 0)
                            {
                                if (index != -1 && this.TabPages[index] != this.SelectedTab)
                                {
                                    g.FillRectangle(new SolidBrush(Color.FromArgb(240, 240, 240)), this.GetTabRect(index));
                                    RectangleF tRectangle = this.GetTabRect(index);
                                    tRectangle.X += TextLeft;
                                    g.DrawString(this.Controls[index].Text, this.Font, brush, tRectangle, sf);

                                }
                                Count = 1;
                            }
                            index = i;
                            g.FillRectangle(new SolidBrush(SelectedColor), this.GetTabRect(i));
                            RectangleF tRectangleF = this.GetTabRect(i);
                            tRectangleF.X += TextLeft;
                            g.DrawString(this.Controls[i].Text, this.Font, brush, tRectangleF, sf);
                            using (Pen objpen = new Pen(Color.Black))
                            {
                                int tx = (int)(tRectangleF.X + (tRectangleF.Width - 30));
                                tRectangleF.X = tx - 2;
                                brush.Color = Color.White;
                                Font font = new System.Drawing.Font("微软雅黑", 12);
                                g.DrawString("〇", font, brush, tRectangleF, sf);
                                font = new System.Drawing.Font("微软雅黑", 11);
                                tRectangleF.X = tx + 2;
                                tRectangleF.Y = tRectangleF.Y - 1;
                                g.DrawString("×", font, brush, tRectangleF, sf);
                            }
                        }
                    }
                    if (this.GetTabRect(i).Contains(e.Location) && tp == this.SelectedTab)
                    {
                        if (index != -1 && index != this.SelectedIndex)
                        {
                            g.FillRectangle(new SolidBrush(Color.FromArgb(240, 240, 240)), this.GetTabRect(index));
                            RectangleF tRectangleF = this.GetTabRect(index);
                            tRectangleF.X += TextLeft;
                            g.DrawString(this.Controls[index].Text, this.Font, brush, tRectangleF, sf);
                            using (Pen objpen = new Pen(Color.Black))
                            {
                                int tx = (int)(tRectangleF.X + (tRectangleF.Width - 30));
                                tRectangleF.X = tx - 2;
                                Font font = new System.Drawing.Font("微软雅黑", 12);
                                g.DrawString("〇", font, brush, tRectangleF, sf);
                                font = new System.Drawing.Font("微软雅黑", 11);
                                tRectangleF.X = tx + 2;
                                tRectangleF.Y = tRectangleF.Y - 1;
                                g.DrawString("×", font, brush, tRectangleF, sf);
                            }
                        }
                        index = -1;

                    }

                    
                }



            }
            catch (Exception)
            {

            }
            Count = 0;
            base.OnMouseMove(e);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            try
            {
                Graphics g = this.CreateGraphics();
                if (index != -1 && this.TabPages[index] != this.SelectedTab)
                {
                    g.FillRectangle(new SolidBrush(Color.FromArgb(240, 240, 240)), this.GetTabRect(index));
                    SolidBrush brush = new SolidBrush(FontColor);
                    RectangleF tRectangleF = this.GetTabRect(index);
                    StringFormat sf = new StringFormat();//封装文本布局信息 
                    sf.LineAlignment = StringAlignment.Center;
                    sf.Alignment = StringAlignment.Near;
                    tRectangleF.X += TextLeft;
                    g.DrawString(this.Controls[index].Text, this.Font, brush, tRectangleF, sf);
                    using (Pen objpen = new Pen(Color.Black))
                    {
                        int tx = (int)(tRectangleF.X + (tRectangleF.Width - 30));
                        tRectangleF.X = tx - 2;
                        Point p5 = new Point(tx, 8);
                        Font font = new System.Drawing.Font("微软雅黑", 12);
                        g.DrawString("〇", font, brush, tRectangleF, sf);
                        font = new System.Drawing.Font("微软雅黑", 11);
                        tRectangleF.X = tx + 2;
                        tRectangleF.Y = tRectangleF.Y - 1;
                        g.DrawString("×", font, brush, tRectangleF, sf);
                    }
                }
            }
            catch (Exception)
            {
            }
            index = -1;
            base.OnMouseLeave(e);
        }
        /// <summary>
        /// 重绘控件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tabMenu_DrawItem(object sender, DrawItemEventArgs e)
        {
            this.SetStyle(
            ControlStyles.UserPaint |                      // 控件将自行绘制，而不是通过操作系统来绘制  
            ControlStyles.OptimizedDoubleBuffer |          // 该控件首先在缓冲区中绘制，而不是直接绘制到屏幕上，这样可以减少闪烁  
            ControlStyles.AllPaintingInWmPaint |           // 控件将忽略 WM_ERASEBKGND 窗口消息以减少闪烁  
            ControlStyles.ResizeRedraw |                   // 在调整控件大小时重绘控件  
            ControlStyles.SupportsTransparentBackColor,    // 控件接受 alpha 组件小于 255 的 BackColor 以模拟透明  
            true);                                         // 设置以上值为 true  
            this.UpdateStyles();
        }

        const int CLOSE_SIZE = 18;

        //关闭按钮功能
        private void MainTabControl_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {


                
                int x = e.X, y = e.Y;
                //计算关闭区域   
                Rectangle myTabRect = this.GetTabRect(this.SelectedIndex);
                myTabRect.Offset(myTabRect.Width - (CLOSE_SIZE + 3), 4);
                myTabRect.Width = CLOSE_SIZE;
                myTabRect.Height = CLOSE_SIZE;


                if (this.TabCount == 1)
                {
                    return;
                }

                TabPage FirstTab = this.TabPages[0]; 
                //如果鼠标在区域内就关闭选项卡   
                bool isClose = x > myTabRect.X && x < myTabRect.Right && y > myTabRect.Y && y < myTabRect.Bottom;
                if (isClose == true)
                {
                    if (this.SelectedTab != FirstTab)
                    {
                        this.TabPages.Remove(this.SelectedTab);
                    }
                }


            }
        }

    }
}