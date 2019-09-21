using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace VexGuiEdit
{
    class MoveGui
    {
        private Control CurrentControl;  //记录传入的控件
        private Point pPoint; //记录上个鼠标坐标位置
        private Point cPoint; //记录当前鼠标位置
        private GuiFrame fc;
        public MoveGui(Control ctrl)
        {
            CurrentControl = ctrl;
            AddEvents();
        }
        /// <summary>
        /// 挂载事件
        /// </summary>
        private void AddEvents()
        {
            CurrentControl.MouseClick += new MouseEventHandler(MouseClick);
            CurrentControl.MouseDown += new MouseEventHandler(MouseDown);
            CurrentControl.MouseMove += new MouseEventHandler(MouseMove);
            CurrentControl.MouseUp += new MouseEventHandler(MouseUp);
        }
        /// <summary>
        /// 绘制拖拉时黑色边框
        /// </summary>
        /// <param name="ctrl"></param>
        public static void DrawDragBound(Control ctrl)
        {
            //ctrl.Update();
            Graphics g = ctrl.CreateGraphics();
            int width = ctrl.Width;
            int height = ctrl.Height;
            Point[] ps = new Point[5] { new Point(0, 0), new Point(width - 1, 0), new Point(width - 1, height - 1), new Point(0, height - 1), new Point(0, 0) };
            g.DrawLines(new Pen(Color.Black), ps);
        }
        /// <summary>
        /// 鼠标点击事件：用来显示边框
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static string GuiPath = "";
        void MouseClick(object sender, MouseEventArgs e)
        {
            GuiPath = sender.GetType().GetProperty("ImageLocation").GetValue(sender).ToString();
            Methods methods = new Methods();
            BtProperties btPro = new BtProperties();
            //if (methods.BolBtOrIm(GuiPath))
            //{
            //    methods.ReadButton(GuiPath);
            //    btPro.Name = methods.Name_B;
            //    btPro.X = methods.X_B;
            //    btPro.Y = methods.Y_B;
            //    btPro.Width = methods.Width_B;
            //    btPro.High = methods.High_B;
            //    btPro.Url1 = methods.Url1_B;
            //    btPro.Url2 = methods.Url2_B;
            //}
            //Main.Mainfrm.propertyGrid1.SelectedObject = btPro;
            this.CurrentControl.Parent.Refresh();//刷新父容器，清除掉其他控件的边框
            this.CurrentControl.BringToFront();
            fc = new GuiFrame(this.CurrentControl);
            this.CurrentControl.Parent.Controls.Add(fc);
            fc.Visible = true;
            fc.Draw();
        }
        /// <summary>
        /// 鼠标移动事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void MouseMove(object sender, MouseEventArgs e)
        {
            Cursor.Current = Cursors.SizeAll; //当鼠标处于控件内部时，鼠标样式为SizeAll
            if (e.Button == MouseButtons.Left)
            {
                MoveGui.DrawDragBound(this.CurrentControl);
                if (fc != null)
                {
                    fc.Visible = false; //隐藏
                }
                cPoint = Cursor.Position; //记录鼠标当前坐标位置
                int x = cPoint.X - pPoint.X; //得到x坐标的位移值
                int y = cPoint.Y - pPoint.Y; ; //得到y坐标的位移值
                //CurrentControl.Refresh();
                CurrentControl.Location = new Point(CurrentControl.Location.X + x, CurrentControl.Location.Y + y);
                pPoint = cPoint;
            }
        }
        /// <summary>
        /// 鼠标释放事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void MouseUp(object sender, MouseEventArgs e)
        {
            this.CurrentControl.Refresh();
            if (fc != null)
            {
                fc.Visible = true;
                fc.Draw();
            }
        }
        /// <summary>
        /// 鼠标按下事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void MouseDown(object sender, MouseEventArgs e)
        {
            pPoint = Cursor.Position;
        }
    }
}
