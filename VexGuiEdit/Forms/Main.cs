using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Dynamic;
using LitJson;
using YamlDotNet.Serialization;
using System.Xml;
using System.Xml.Linq;
using System.Net;
using System.Threading;
using Microsoft.VisualBasic;
using System.Windows.Forms.Integration;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace VexGuiEdit
{
    enum BoolGui
    {
        Ok,
        Ok2,
        No
    }
    public partial class Main : Form
    {
        public static string StartClientpath = "";
        public static string StartServerpath = "";
        string BoolServerPath = ""; //用来判断路径
        string BoolClientPath = ""; //用来判断客户端路径
        //private float X, Y;
        public static Main Mainfrm;
        public Main()
        {
            //AppDomain.CurrentDomain.AssemblyResolve += new ResolveEventHandler(CurrentDomain_AssemblyResolve);
            Mainfrm = this;
            InitializeComponent();
            SetStyle(ControlStyles.ResizeRedraw, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
        }
        public static string CurrentDisk = "";
        public static string VgePropertiesPath = "";
        public static string GuisPath = "";
        public static string ImagesPath = ""; //记录图像文件目录
        public static string ButtonsPath = ""; //记录按钮文件目录
        public static string TextsPath = ""; //记录文本文件目录
        public static string guiXmlPath = "";  //记录gui.xml文件路径
        public static string imXmlPath = ""; //记录im.xml文件路径
        public static string btXmlPath = ""; //记录bt.xml文件路径
        public static string txtXmlPath = ""; //记录tx.xml文件路径
        public int i = 1; //标记
        private void Main_Load(object sender, EventArgs e)
        {
            LbX.Text = "";
            LbY.Text = "";
            LbW.Text = "";
            LbH.Text = "";
            窗口WToolStripMenuItem.DropDownItems.Add(i + " " + tabControl1.TabPages[0].Text);
            try
            {
                CurrentDisk = Directory.GetCurrentDirectory().Substring(0, Directory.GetCurrentDirectory().IndexOf("\\") + 1);
                DirectoryInfo pathxml = new DirectoryInfo(CurrentDisk + "VgeXmlPath");
                if (!pathxml.Exists)
                {
                    pathxml.Create();
                    FileInfo filexml = new FileInfo(CurrentDisk + "VgeXmlPath\\vge.xml");
                    {
                        if (!filexml.Exists)
                        {
                            XElement xElement = new XElement(
                                new XElement("Path",
                                new XElement("ClientPath", ""),
                                new XElement("ServerPath", "")
                                    )
                            );
                            XmlWriterSettings settings = new XmlWriterSettings();
                            settings.Encoding = new UTF8Encoding(false);
                            settings.Indent = true;
                            XmlWriter xw = XmlWriter.Create(CurrentDisk + "VgeXmlPath\\vge.xml", settings);
                            xElement.Save(xw);
                            xw.Flush();
                            xw.Close();
                            new Welcome().ShowDialog();
                            XmlDocument doc = new XmlDocument();
                            doc.Load(CurrentDisk + "VgeXmlPath\\vge.xml");
                            XmlNode path = doc.SelectSingleNode("Path");
                            XmlNodeList pathlist = path.ChildNodes;
                            if (pathlist.Item(0).InnerText == "" || pathlist.Item(1).InnerText == "")
                            {
                                new Welcome().ShowDialog();
                            }
                            else
                            {
                                StartClientpath = pathlist.Item(0).InnerText;
                                StartServerpath = pathlist.Item(1).InnerText;
                                BoolServerPath = pathlist.Item(1).InnerText;
                                BoolClientPath = pathlist.Item(0).InnerText;
                            }
                        }
                    }
                }
                else
                {
                    FileInfo filexml = new FileInfo(CurrentDisk + "VgeXmlPath\\vge.xml");
                    {
                        if (!filexml.Exists)
                        {
                            XElement xElement = new XElement(
                                new XElement("Path",
                                new XElement("ClientPath", StartClientpath),
                                new XElement("ServerPath", StartServerpath)
                                    )
                            );
                            XmlWriterSettings settings = new XmlWriterSettings();
                            settings.Encoding = new UTF8Encoding(false);
                            settings.Indent = true;
                            XmlWriter xw = XmlWriter.Create(CurrentDisk + "VgeXmlPath\\vge.xml", settings);
                            xElement.Save(xw);
                            xw.Flush();
                            xw.Close();
                            XmlDocument doc = new XmlDocument();
                            doc.Load(CurrentDisk + "VgeXmlPath\\vge.xml");
                            XmlNode path = doc.SelectSingleNode("Path");
                            XmlNodeList pathlist = path.ChildNodes;
                            if (pathlist.Item(0).InnerText == "" || pathlist.Item(1).InnerText == "")
                            {
                                new Welcome().ShowDialog();
                            }
                            else
                            {
                                StartClientpath = pathlist.Item(0).InnerText;
                                StartServerpath = pathlist.Item(1).InnerText;
                                BoolServerPath = pathlist.Item(1).InnerText;
                                BoolClientPath = pathlist.Item(0).InnerText;
                            }
                        }
                        else
                        {
                            XmlDocument doc = new XmlDocument();
                            doc.Load(CurrentDisk + "VgeXmlPath\\vge.xml");
                            XmlNode path = doc.SelectSingleNode("Path");
                            XmlNodeList pathlist = path.ChildNodes;
                            if (pathlist.Item(0).InnerText == "" || pathlist.Item(1).InnerText == "")
                            {
                                new Welcome().ShowDialog();
                            }
                            else
                            {
                                StartClientpath = pathlist.Item(0).InnerText;
                                StartServerpath = pathlist.Item(1).InnerText;
                                BoolServerPath = pathlist.Item(1).InnerText;
                                BoolClientPath = pathlist.Item(0).InnerText;
                            }
                        }
                    }
                }
            }
            catch(Exception ex)
            {

            }
            #region xml配置文件
            DirectoryInfo vgInfo = new DirectoryInfo(CurrentDisk + "VgeProperties\\Guis");
            GuisPath = CurrentDisk + "VgeProperties\\Guis\\";
            VgePropertiesPath = CurrentDisk + "VgeProperties\\";
            if (!vgInfo.Exists)
            {
                vgInfo.Create();
            }
            DirectoryInfo vgImages = new DirectoryInfo(CurrentDisk + "VgeProperties\\Images");
            ImagesPath = CurrentDisk + "VgeProperties\\Images\\";
            if (!vgImages.Exists)
            {
                vgImages.Create();

            }
            DirectoryInfo vgButtons = new DirectoryInfo(CurrentDisk + "VgeProperties\\Buttons");
            ButtonsPath = CurrentDisk + "VgeProperties\\Buttons\\";
            if (!vgButtons.Exists)
            {
                vgButtons.Create();
            }
            DirectoryInfo vgTexts = new DirectoryInfo(CurrentDisk + "VgeProperties\\Texts");
            TextsPath = CurrentDisk + "VgeProperties\\Texts\\";
            if (!vgTexts.Exists)
            {
                vgTexts.Create();
            }
            Bitmap bitmap = new Bitmap(800, 800);
            Graphics g = Graphics.FromImage(bitmap);
            g.DrawString("请拖入GUI底图或GUI配置文件到程序面板中", new Font("楷体", 25, FontStyle.Regular), new SolidBrush(Color.FromArgb(94, 94, 94)), 100, 430);
            this.GuiArea.BackgroundImage = bitmap;
            //GUIArea.VerticalScroll.Value = GUIArea.VerticalScroll.Minimum;
            //GuiProperty guiProperty = new GuiProperty();
            //propertyGrid1.SelectedObject = guiProperty;
            //Main main = new Main();
            //GuiProperty guiProperty = new GuiProperty();
            //main.propertyGrid1.SelectedObject = guiProperty;
            #endregion
            try
            {
                HttpWebRequest httpReq = null;
                HttpWebResponse httpRes = null;
                httpReq = (HttpWebRequest)WebRequest.Create("http://mcqhyy.asuscomm.com:50006/维护.png");//url是网站的地址
                httpReq.Timeout = 650;
                httpRes = (HttpWebResponse)httpReq.GetResponse();
                if (httpRes.StatusCode == HttpStatusCode.OK)
                {
                    pictureBox1.ImageLocation = "http://mcqhyy.asuscomm.com:50006/维护.png";
                    //GuiArea.BackgroundImage = null;
                    //foreach(Control ctrl in this.Controls)
                    //{
                    //    ctrl.Enabled = false;
                    //}
                }
            }
            catch
            {
            }
        }
        public void LoadXmlPath()
        {
            foreach (string fileGuipath in Directory.GetFiles(GuisPath))
            {
                File.Delete(fileGuipath);
            }
            foreach (string fileImagepath in Directory.GetFiles(ImagesPath))
            {
                File.Delete(fileImagepath);
            }
            foreach(string fileButtonpath in Directory.GetFiles(ButtonsPath))
            {
                File.Delete(fileButtonpath);
            }
            foreach(string fileTextpath in Directory.GetFiles(TextsPath))
            {
                File.Delete(fileTextpath);
            }
            foreach(string fileDirpath in Directory.GetFiles(VgePropertiesPath))
            {
                File.Delete(fileDirpath);
            }
        }
        public static string Vexview = ""; //生成vexview目录地址
        public int Gui_X = 0;
        public int Gui_Y = 0;
        SGuiFrame SGui = new SGuiFrame();
        FGuiFrame FGui = new FGuiFrame();
        //string StartServerpath = ""; //服务端
        //string StartClientpath = ""; //example：D:\精灵梦乡客户端V2.0\精灵梦乡客户端V2.0\.minecraft\vexview\textures 客户端
        string Impath = ""; //example：D:\精灵梦乡服务端V2.0\精灵梦乡服务端V2.0\plugins\VexView\image
        string Btpath = ""; //example：D:\精灵梦乡服务端V2.0\精灵梦乡服务端V2.0\plugins\VexView\button
        string TxtPath = ""; //example：D:\精灵梦乡服务端V2.0\精灵梦乡服务端V2.0\plugins\VexView\text
        BoolGui bolGui = BoolGui.No; //判断是否加入子gui
        private void Main_DragDrop(object sender, DragEventArgs e)
        {
           
            string FilePath = ((System.Array)e.Data.GetData(DataFormats.FileDrop)).GetValue(0).ToString();
            if (FilePath.Substring(FilePath.LastIndexOf("\\") + 1).EndsWith(".yml"))
            {
                StartServerpath = FilePath.Substring(0, FilePath.LastIndexOf("VexView")) + "VexView\\";  //最大BUG!!!!!!!!!!!!
            }
            string clientpath = StartClientpath + "\\";
            #region 注释
            string Extention = FilePath.Substring(FilePath.LastIndexOf('.'));
            if (Extention == ".yml")
            {
                if (FilePath.IndexOf(BoolServerPath) == -1) 
                {
                    LogsTxt.AppendText(">>>该yml文件路径不在第一次绑定服务端VexView目录内\r\n");
                    return;
                }
                Methods method_Gui = new Methods();
                JsonData jsonData = JsonMapper.ToObject(Methods.YamlToJson(FilePath));
                if (!((jsonData.ContainsKey("image")) | (jsonData.ContainsKey("buttons"))))
                {
                    LogsTxt.AppendText(">>>无效yml文件,仅支持GUI配置文件\r\n");
                    return;
                }
                LoadXmlPath(); //删除各目录下文件
                this.GuiArea.BackgroundImage = null;
                this.GuiArea.Controls.Clear();
                TabPage first = tabControl1.TabPages[0];
                foreach(TabPage tab in tabControl1.TabPages)
                {
                    if(tab !=first)
                    {
                        tabControl1.TabPages.Remove(tab);
                    }
                }
                GuiComboBox.Items.Clear();
                ImagePanel.Controls.Clear();
                ButtonPanel.Controls.Clear();
                TextPanel.Controls.Clear();
                GuiComboBox.Items.Clear();
                GuiPropertyGrid.SelectedObject = null;
                LogsTxt.Text = ""; //清空日志
                result = 0; //清零
                j = 0;
                result1 = 0;
                j1 = 0;
                result2 = 0;
                j2 = 0;
                bolGui = BoolGui.Ok2;
                try
                {
                    method_Gui.SuppleMentGui(FilePath);
                    method_Gui.ReadGuiYML(FilePath);
                    LogsTxt.AppendText(">>>解析GUI：" + FilePath.Substring(FilePath.LastIndexOf("\\") + 1) + "\r\n");
                    PictureBox GuiImg = new PictureBox
                    {
                        ImageLocation = method_Gui.Url.Trim('\'').Replace("[local]", clientpath.Replace("\\", "/")),
                        Width = (int)(Convert.ToDouble(method_Gui.XShow)),
                        Height = (int)(Convert.ToDouble(method_Gui.YShow)),
                        Location = new Point(GuiArea.Location.X + 250, GuiArea.Location.Y + 250),
                        SizeMode = PictureBoxSizeMode.StretchImage
                    };
                    string guiname = FilePath.Substring(FilePath.LastIndexOf("\\") + 1).Substring(0, FilePath.Substring(FilePath.LastIndexOf("\\") + 1).LastIndexOf("."));
                    GuiComboBox.Items.Add("GUI：" + FilePath.Substring(FilePath.LastIndexOf("\\") + 1));
                    窗口WToolStripMenuItem.DropDownItems[6].Text = i + " " + guiname + ".yml" + " [设计]";
                    tabControl1.TabPages[0].Text = "GUI：" + guiname + ".yml" + " [设计]";
                    this.GuiArea.Controls.Add(GuiImg);
                    guiXmlPath = GuisPath + guiname + ".xml";
                    XmlMethods.CreateGuiTree(guiXmlPath, method_Gui.Url, method_Gui.X, method_Gui.Y, method_Gui.Width,
                        method_Gui.High, method_Gui.XShow, method_Gui.YShow);
                    GuiImg.Tag = "gui:" + FilePath.Substring(FilePath.LastIndexOf("\\") + 1).Substring(0, FilePath.Substring(FilePath.LastIndexOf("\\") + 1).LastIndexOf(".")); //gui:gui
                    GuiImg.Name = FilePath.Substring(FilePath.LastIndexOf("\\") + 1).Substring(0, FilePath.Substring(FilePath.LastIndexOf("\\") + 1).LastIndexOf(".")) + ".gui"; //gui:gui.yml                                                                                                                                                 
                    Gui_X = GuiImg.Location.X;
                    Gui_Y = GuiImg.Location.Y;
                    GuiImg.SendToBack();
                    FGui.WireControl(GuiImg);
                    GuiComboBox.SelectedIndex = 0;
                    XmlMethods.ReadGui(GuisPath + guiname + ".xml");
                    GuiPropertyGrid.SelectedObject = FGuiFrame.gui;
                }
                catch
                {

                }
                JsonData jsonbt = jsonData["buttons"];
                JsonData jsonim = jsonData["image"];
                JsonData jsontxt = jsonData["text"];
                //JsonData jsonsl = jsonData["slot"];
                Methods method_Pro = new Methods();
                //read the image
                try
                {
                    string images = "";
                    if (jsonim != null)
                    {
                        foreach (JsonData im in jsonim)
                        {
                            if (im == null)
                            {
                                break;
                            }
                            if (im.ToJson().ToString().IndexOf("{") == -1)
                            {
                                images = Regex.Unescape(im.ToJson().ToString().Substring(im.ToJson().ToString().IndexOf("\"") + 1, im.ToJson().ToString().IndexOf("\"", im.ToJson().ToString().IndexOf("\"") + 1) - 1));
                            }
                            else
                            {
                                images = Regex.Unescape(im.ToJson().ToString().Substring(im.ToJson().ToString().IndexOf("\"") + 1, im.ToJson().ToString().IndexOf("\"", im.ToJson().ToString().IndexOf("\"") + 1) - 2));
                            }
                            Impath = StartServerpath + "image\\" + images + ".yml";
                            method_Pro.SuppleMentIm(Impath);
                            using(FileStream fr = new FileStream(Impath,FileMode.Open,FileAccess.Read,FileShare.ReadWrite))
                            {
                                using (StreamReader sr = new StreamReader(fr))
                                {
                                    if(sr.ReadToEnd()!=string.Empty)
                                    {
                                        //MessageBox.Show(Impath);
                                        method_Pro.ReadImage(Impath);
                                        //MessageBox.Show(Impath);
                                        double X, Y = 0;
                                        if (method_Pro.X_I.IndexOf("-") == -1)   //如果X坐标是正数
                                        {
                                            X = double.Parse(method_Pro.X_I) + 250; // +200回归坐标系
                                        }
                                        else //不是正数的话
                                        {
                                            X = Gui_X - Math.Abs(double.Parse(method_Pro.X_I));
                                        }
                                        if (method_Pro.Y_I.IndexOf("-") == -1)  //如果Y坐标是正数
                                        {
                                            Y = double.Parse(method_Pro.Y_I) + 250; // +200回归坐标系
                                        }
                                        else //不是正数的话
                                        {
                                            Y = Gui_Y - Math.Abs(double.Parse(method_Pro.Y_I));
                                        }
                                        PictureBox imimage = new PictureBox
                                        {
                                            ImageLocation = method_Pro.Url_I.Replace("[local]", clientpath.Replace("\\", "/")), //路径 
                                            Location = new Point(Convert.ToInt32(X), Convert.ToInt32(Y)),
                                            SizeMode = PictureBoxSizeMode.StretchImage,
                                            Width = (int)(Convert.ToDouble(method_Pro.XShow_I)),
                                            Height = (int)(Convert.ToDouble(method_Pro.YShow_I))
                                            //BackColor = Color.Transparent
                                        };
                                        this.GuiArea.Controls.Add(imimage);
                                        imXmlPath = ImagesPath + images + ".xml";
                                        XmlMethods.CreateImTree(imXmlPath, method_Pro.Url_I, method_Pro.X_I, method_Pro.Y_I, method_Pro.Width_I,
                                            method_Pro.High_I, method_Pro.XShow_I, method_Pro.YShow_I, method_Pro.HoverText);
                                        imimage.Tag = "image:" + images;
                                        imimage.Name = images + ".im";
                                        GuiComboBox.Items.Add("图像：" + images + ".yml");
                                        ImageControl imctrl = new ImageControl();
                                        imctrl.Name = images; //images下面
                                        imctrl.label1.Text = images + ".png(im)";
                                        if (method_Pro.Url_I.IndexOf("http") == 0 || method_Pro.Url_I.IndexOf("https") == 0)
                                        {
                                            imctrl.pictureBox1.ImageLocation = method_Pro.Url_I;
                                        }
                                        else
                                        {
                                            imctrl.pictureBox1.ImageLocation = clientpath + method_Pro.Url_I.Substring(method_Pro.Url_I.LastIndexOf("]") + 1);
                                        }
                                        imctrl.pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                                        imctrl.Size = new Size(150, 54);
                                        imctrl.Location = new Point(0, Calcim());
                                        ImagePanel.Controls.Add(imctrl);
                                        LogsTxt.AppendText(">>>解析图像：" + images + ".yml" + "\r\n");
                                        imimage.BringToFront();
                                        SGui.WireControl(imimage);
                                        imimage.ContextMenuStrip = GuiMenu;
                                        imimage.MouseEnter += new EventHandler(this.ImageMouseEnter);
                                    }
                                    sr.Close();
                                }
                                fr.Close();
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    //MessageBox.Show(ex.Message);
                    //MessageBox.Show("解析文件一部分失败，请检查你的文件，确保每个yml文件键的值都存在");
                }
                //read the button
                string buttons = "";
                if (jsonbt != null)
                {
                    foreach (JsonData bt in jsonbt)
                    {
                        if (bt == null)
                        {
                            break;
                        }
                        if (bt.ToJson().ToString().IndexOf("{") == -1)
                        {
                            buttons = Regex.Unescape(bt.ToJson().ToString().Substring(bt.ToJson().ToString().IndexOf("\"") + 1, bt.ToJson().ToString().IndexOf("\"", bt.ToJson().ToString().IndexOf("\"") + 1) - 1));
                        }
                        else
                        {
                            buttons = Regex.Unescape(bt.ToJson().ToString().Substring(bt.ToJson().ToString().IndexOf("\"") + 1, bt.ToJson().ToString().IndexOf("\"", bt.ToJson().ToString().IndexOf("\"") + 1) - 2));
                        }
                        Btpath = StartServerpath + "button\\" + buttons + ".yml";
                        method_Pro.SuppleMentBt(Btpath);
                        using(FileStream fr = new FileStream(Btpath,FileMode.Open,FileAccess.Read,FileShare.ReadWrite))
                        {
                            using(StreamReader sr = new StreamReader(fr))
                            {
                                if(sr.ReadToEnd()!=string.Empty)
                                {
                                    method_Pro.ReadButton(Btpath);
                                    JsonData json = JsonMapper.ToObject(Methods.YamlToJson(Btpath));
                                    string cmd = "";
                                    string cmds = ""; //积累命令
                                    if (json["commands"] != null)
                                    {
                                        JsonData jscmd = json["commands"];
                                        //MessageBox.Show(jscmd.ToJson().ToString());
                                        foreach (JsonData jsCMD in jscmd)
                                        {
                                            if (jsCMD.ToJson().ToString().IndexOf("{") == -1)
                                            {
                                                cmd = jsCMD.ToJson().ToString().Substring(jsCMD.ToJson().ToString().IndexOf("\"") + 1, jsCMD.ToJson().ToString().IndexOf("\"", jsCMD.ToJson().ToString().IndexOf("\"") + 1) - 1);
                                            }
                                            else
                                            {
                                                cmd = jsCMD.ToJson().ToString().Substring(jsCMD.ToJson().ToString().IndexOf("\"") + 1, jsCMD.ToJson().ToString().IndexOf("\"", jsCMD.ToJson().ToString().IndexOf("\"") + 1) - 2);
                                            }
                                            cmds += Regex.Unescape(cmd) + "|";
                                        }
                                    }
                                    else
                                    {
                                        cmds = "";
                                    }
                                    //MessageBox.Show(method_BT.X_B);
                                    DcButton btimage = new DcButton();
                                    try
                                    {
                                        //btimage.Parent = GUIArea;
                                        if (method_Pro.Url1_B.IndexOf("https") == 0 || method_Pro.Url1_B.IndexOf("http") == 0)
                                        {
                                            btimage.BackgroundImage = Image.FromStream(WebRequest.Create("https://s2.ax1x.com/2019/08/13/mPJ5yF.png").GetResponse().GetResponseStream());
                                        }
                                        else
                                        {
                                            btimage.BackgroundImage = Image.FromFile(method_Pro.Url1_B.Trim('\'').Replace("[local]", clientpath.Replace("\\", "/"))); //路径                       
                                        }
                                        double X, Y = 0;
                                        if (method_Pro.X_B.IndexOf("-") == -1)   //如果X坐标是正数
                                        {
                                            X = double.Parse(method_Pro.X_B) + 250;
                                        }
                                        else //不是正数的话
                                        {
                                            X = Gui_X - Math.Abs(double.Parse(method_Pro.X_B));
                                        }
                                        if (method_Pro.Y_B.IndexOf("-") == -1)  //如果Y坐标是正数
                                        {
                                            Y = double.Parse(method_Pro.Y_B) + 250;
                                        }
                                        else //不是正数的话
                                        {
                                            Y = Gui_Y - Math.Abs(double.Parse(method_Pro.Y_B));
                                        }
                                        btimage.Location = new Point(Convert.ToInt32(X), Convert.ToInt32(Y));
                                        //btimage.BackColor = Color.Transparent;
                                        btimage.FlatStyle = FlatStyle.Flat;
                                        btimage.FlatAppearance.BorderSize = 0;
                                        btimage.BackgroundImageLayout = ImageLayout.Stretch;   //调整模式为适应图片大小而更改控件大小   //调整模式为适应图片大小而更改控件大小
                                        btimage.Width = (int)(Convert.ToDouble(method_Pro.Width_B));
                                        btimage.Height = (int)(Convert.ToDouble(method_Pro.High_B));
                                        btimage.Text = method_Pro.Name_B;
                                        btimage.Font = new Font("楷体", 12, FontStyle.Bold);
                                        this.GuiArea.Controls.Add(btimage);  //添加到Gui图上
                                        btXmlPath = ButtonsPath + buttons + ".xml";
                                        XmlMethods.CreateBtTree(btXmlPath, method_Pro.Id, method_Pro.Name_B, method_Pro.Url1_B, method_Pro.Url2_B, method_Pro.X_B, method_Pro.Y_B,
                                            method_Pro.Width_B, method_Pro.High_B, cmds, method_Pro.Asop, method_Pro.Close, method_Pro.To);
                                        btimage.Tag = "button:" + buttons;
                                        btimage.Name = buttons + ".bt";
                                        GuiComboBox.Items.Add("按钮：" + buttons + ".yml");
                                        ImageControl btctrl = new ImageControl();
                                        btctrl.Name = buttons;
                                        btctrl.label1.Text = buttons + ".png(bt)";
                                        if (method_Pro.Url1_B.IndexOf("https") == 0 || method_Pro.Url1_B.IndexOf("http") == 0)
                                        {
                                            btctrl.pictureBox1.ImageLocation = method_Pro.Url1_B;
                                        }
                                        else
                                        {
                                            btctrl.pictureBox1.ImageLocation = clientpath + method_Pro.Url1_B.Substring(method_Pro.Url1_B.LastIndexOf("]") + 1);
                                        }
                                        btctrl.pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                                        btctrl.Size = new Size(150, 54);
                                        btctrl.Location = new Point(0, Calcbt());
                                        ButtonPanel.Controls.Add(btctrl);
                                        LogsTxt.AppendText(">>>解析按钮：" + buttons + ".yml" + "\r\n");
                                        btimage.BringToFront();
                                        //new MoveGui(btimage);
                                        SGui.WireControl(btimage);
                                        btimage.ContextMenuStrip = GuiMenu;
                                        btimage.MouseHover += new EventHandler(this.ButtonMouseHover);
                                        btimage.MouseLeave += new EventHandler(this.ButtonMouseLeave);
                                        //MessageBox.Show(method_Pro.High_B);
                                        //MessageBox.Show(btimage.Parent.Location.X.ToString());
                                    }
                                    catch (Exception ex)
                                    {
                                        //MessageBox.Show(ex.Message);
                                        //MessageBox.Show("解析文件一部分失败，请检查你的文件，确保每个yml文件键的值都存在");
                                    }
                                }
                                sr.Close();
                            }
                            fr.Close();
                        }
                    }
                }
                //read the text
                try
                {
                    string tooltxt = "";
                    string texts = "";
                    if (jsontxt != null)
                    {
                        foreach (JsonData txt in jsontxt)
                        {
                            if (txt == null)
                            {
                                break;
                            }
                            if (txt.ToJson().ToString().IndexOf("{") == -1)
                            {
                                texts = Regex.Unescape(txt.ToJson().ToString().Substring(txt.ToJson().ToString().IndexOf("\"") + 1, txt.ToJson().ToString().IndexOf("\"", txt.ToJson().ToString().IndexOf("\"") + 1) - 1));
                            }
                            else
                            {
                                texts = Regex.Unescape(txt.ToJson().ToString().Substring(txt.ToJson().ToString().IndexOf("\"") + 1, txt.ToJson().ToString().IndexOf("\"", txt.ToJson().ToString().IndexOf("\"") + 1) - 2));
                            }
                            TxtPath = StartServerpath + "text\\" + texts + ".yml";
                            method_Pro.SuppleMentTxt(TxtPath);
                            using (FileStream fs = new FileStream(TxtPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite)) 
                            {
                                using (StreamReader sr = new StreamReader(fs))
                                {
                                    if (sr.ReadToEnd() != string.Empty)
                                    {
                                        method_Pro.ReadText(TxtPath);
                                        JsonData json = JsonMapper.ToObject(Methods.YamlToJson(TxtPath)); //读取text
                                        string TXT = "";
                                        string TXTs = ""; //积累命令
                                        if (json["text"] != null)
                                        {
                                            JsonData jsontxt2 = json["text"];
                                            //GuiCodeTxt.Text = jsontxt2.ToJson().ToString();
                                            //MessageBox.Show(jsontxt2.ToJson().ToString());
                                            foreach (JsonData jsTXT in jsontxt2)
                                            {
                                                if (jsTXT.ToJson().ToString().IndexOf("{") == -1)
                                                {
                                                    TXT = jsTXT.ToJson().ToString().Substring(jsTXT.ToJson().ToString().IndexOf("\"") + 1, jsTXT.ToJson().ToString().IndexOf("\"", jsTXT.ToJson().ToString().IndexOf("\"") + 1) - 1);
                                                }
                                                else
                                                {
                                                    TXT = jsTXT.ToJson().ToString().Substring(jsTXT.ToJson().ToString().IndexOf("\"") + 1, jsTXT.ToJson().ToString().IndexOf("\"", jsTXT.ToJson().ToString().IndexOf("\"") + 1) - 2);
                                                }
                                                TXTs += Regex.Unescape(TXT) + "|";
                                            }
                                        }
                                        else
                                        {
                                            TXTs = "";
                                        }
                                        Label Txtlb = new Label();
                                        //Txtlb.Parent = GuiImg;
                                        //Txtlb.BackColor = Color.Transparent;
                                        double X, Y = 0;
                                        if (method_Pro.X_T.IndexOf("-") == -1)   //如果X坐标是正数
                                        {
                                            X = double.Parse(method_Pro.X_T) + 250;
                                        }
                                        else //不是正数的话
                                        {
                                            X = Gui_X - Math.Abs(double.Parse(method_Pro.X_T));
                                        }
                                        if (method_Pro.Y_T.IndexOf("-") == -1)  //如果Y坐标是正数
                                        {
                                            Y = double.Parse(method_Pro.Y_T) + 250;
                                        }
                                        else //不是正数的话
                                        {
                                            Y = Gui_Y - Math.Abs(double.Parse(method_Pro.Y_T));
                                        }
                                        Txtlb.Font = new Font("宋体", 12, FontStyle.Bold);
                                        Txtlb.AutoSize = true;
                                        Txtlb.Location = new Point(Convert.ToInt32(X), Convert.ToInt32(Y));
                                        string[] TXTS = TXTs.Split(new char[] { '|' });
                                        for (int i = 0; i < TXTS.Length; i++)
                                        {
                                            Txtlb.Text += TXTS[i] + "\n";
                                        }
                                        this.GuiArea.Controls.Add(Txtlb);
                                        txtXmlPath = TextsPath + texts + ".xml";
                                        XmlMethods.CreateTxtTree(txtXmlPath, method_Pro.X_T, method_Pro.Y_T, method_Pro.Scale, TXTs);
                                        Txtlb.Tag = "text:" + texts;
                                        Txtlb.Name = texts + ".txt";
                                        GuiComboBox.Items.Add("文本：" + texts + ".yml");
                                        TxtControl txtctrl = new TxtControl();
                                        txtctrl.Name = texts; //texts
                                        txtctrl.label1.Text = texts + ".txt(txt)"; //。
                                        txtctrl.label2.Text = TXTs; //多行文本以一行显示
                                        txtctrl.Size = new Size(150, 54);
                                        txtctrl.Location = new Point(0, Calctxt());
                                        TextPanel.Controls.Add(txtctrl);
                                        LogsTxt.AppendText(">>>解析文本：" + texts + ".yml" + "\r\n");
                                        Txtlb.BringToFront();
                                        SGui.WireControl(Txtlb);
                                        tooltxt = ""; //清空文本
                                        Txtlb.ContextMenuStrip = GuiMenu;
                                    }
                                    sr.Close();
                                }
                                fs.Close();
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    //MessageBox.Show(ex.Message);
                }
            }
            else if (Extention == ".png" && bolGui == BoolGui.No)
            {
                try
                {
                    if (FilePath.IndexOf(BoolClientPath) == -1)
                    {
                        LogsTxt.AppendText(">>>该图片文件路径不在第一次绑定客户端textures的目录内\r\n");
                        return;
                    }
                    GuiPropertyGrid.SelectedObject = null;
                    GuiArea.Controls.Clear();
                    GuiArea.BackgroundImage = null;
                    GuiComboBox.Items.Clear();
                    LoadXmlPath(); //删除各目录下文件
                    string guiName = FilePath.Substring(FilePath.LastIndexOf("\\") + 1).Substring(0, FilePath.Substring(FilePath.LastIndexOf("\\") + 1).LastIndexOf("."));
                    Bitmap bitmap = new Bitmap(FilePath);
                    PictureBox GuiPic = new PictureBox
                    {
                        Width = bitmap.Width,
                        Height = bitmap.Height,
                        ImageLocation = FilePath,
                        SizeMode = PictureBoxSizeMode.StretchImage,
                        Location = new Point(GuiArea.Location.X + 250, GuiArea.Location.Y + 250),
                        Tag = "gui:" + guiName,
                        Name = guiName + ".gui"
                    };
                    this.GuiArea.Controls.Add(GuiPic);
                    Gui_X = GuiPic.Location.X;
                    Gui_Y = GuiPic.Location.Y;
                    GuiComboBox.Items.Add("GUI：" + guiName + ".yml");
                    窗口WToolStripMenuItem.DropDownItems[6].Text = i + " " + guiName + ".yml" + " [设计]";
                    tabControl1.TabPages[0].Text = "GUI：" + guiName + ".yml" + " [设计]";
                    string gui = FilePath.Substring(FilePath.LastIndexOf("textures") + 9);
                    XmlMethods.CreateGuiTree(GuisPath + guiName + ".xml", "[local]" + gui.Replace("\\", "/"), "-1", "-1", GuiPic.Width.ToString(), GuiPic.Height.ToString(), GuiPic.Width.ToString(), GuiPic.Height.ToString());
                    GuiPic.SendToBack();
                    //new FirstGuiFrame(picBox);
                    FGui.WireControl(GuiPic);
                    bolGui = BoolGui.Ok;
                    ImagePanel.Controls.Clear();
                    ButtonPanel.Controls.Clear();
                    TextPanel.Controls.Clear();
                    result = 0; //清零
                    j = 0;
                    result1 = 0;
                    j1 = 0;
                    result2 = 0;
                    j2 = 0;
                }
                catch
                {

                }
            }
            else if (Extention == ".png" && ((bolGui == BoolGui.Ok) || (bolGui == BoolGui.Ok2)))
            {
                try
                {
                    if (FilePath.IndexOf(BoolClientPath) == -1)
                    {
                        LogsTxt.AppendText(">>>该图片文件路径不在第一次绑定客户端textures的目录内\r\n");
                        return;
                    }
                    new SelectBtImTxt().ShowDialog();
                    // picname是文件名
                    string picName = FilePath.Substring(FilePath.LastIndexOf("\\") + 1).Substring(0, FilePath.Substring(FilePath.LastIndexOf("\\") + 1).LastIndexOf("."));
                    Bitmap bitmap = new Bitmap(FilePath);
                    switch (SelectBtImTxt.SelectedItem)
                    {
                        case "图像":
                            {
                                string x = "100", y = "100";
                                double X, Y = 0;
                                if (x.IndexOf("-") == -1)   //如果X坐标是正数
                                {
                                    X = double.Parse(x) + 250; // +250回归坐标系
                                }
                                else //不是正数的话
                                {
                                    X = Gui_X - Math.Abs(double.Parse(x));
                                }
                                if (y.IndexOf("-") == -1)  //如果Y坐标是正数
                                {
                                    Y = double.Parse(x) + 250; // +250回归坐标系
                                }
                                else //不是正数的话
                                {
                                    Y = Gui_Y - Math.Abs(double.Parse(x));
                                }
                                PictureBox ImagePic = new PictureBox
                                {
                                    Width = bitmap.Width,
                                    Height = bitmap.Height,
                                    ImageLocation = FilePath,
                                    SizeMode = PictureBoxSizeMode.StretchImage,
                                    ContextMenuStrip = GuiMenu,
                                    Location = new Point(Convert.ToInt32(X), Convert.ToInt32(Y)),
                                    Tag = "image:" + SelectBtImTxt.SelectFileName,
                                    Name = SelectBtImTxt.SelectFileName + ".im"
                                };
                                this.GuiArea.Controls.Add(ImagePic);
                                GuiComboBox.Items.Add("图像：" + SelectBtImTxt.SelectFileName + ".yml");
                                ImageControl imctrl = new ImageControl();
                                imctrl.Name = SelectBtImTxt.SelectFileName;
                                imctrl.label1.Text = SelectBtImTxt.SelectFileName + ".png(im)";
                                imctrl.pictureBox1.ImageLocation = FilePath;
                                imctrl.pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                                imctrl.Size = new Size(150, 54);
                                imctrl.Location = new Point(0, Calcim());
                                ImagePanel.Controls.Add(imctrl);
                                LogsTxt.AppendText(">>>添加图像：" + SelectBtImTxt.SelectFileName + ".png" + "\r\n");
                                string image = FilePath.Substring(FilePath.LastIndexOf("textures") + 9); //文件里的url
                                XmlMethods.CreateImTree(ImagesPath + SelectBtImTxt.SelectFileName + ".xml", "[local]" + image, "100", "100",
                                    (ImagePic.Width).ToString(), (ImagePic.Height).ToString(), (ImagePic.Width).ToString(), (ImagePic.Height).ToString(), ""); ;
                                ImagePic.BringToFront();
                                SGui.WireControl(ImagePic);
                                ImagePic.MouseEnter += new EventHandler(this.ImageMouseEnter);
                                break;
                            }
                        case "按钮":
                            {
                                string x = "100", y = "100";
                                double X, Y = 0;
                                if (x.IndexOf("-") == -1)   //如果X坐标是正数
                                {
                                    X = double.Parse(x) + 250; // +250回归坐标系
                                }
                                else //不是正数的话
                                {
                                    X = Gui_X - Math.Abs(double.Parse(x));
                                }
                                if (y.IndexOf("-") == -1)  //如果Y坐标是正数
                                {
                                    Y = double.Parse(x) + 250; // +250回归坐标系
                                }
                                else //不是正数的话
                                {
                                    Y = Gui_Y - Math.Abs(double.Parse(x));
                                }
                                DcButton ButtonPic = new DcButton();
                                ButtonPic.Width = bitmap.Width;
                                ButtonPic.Height = bitmap.Height;
                                ButtonPic.BackgroundImage = Image.FromFile(FilePath);
                                ButtonPic.BackgroundImageLayout = ImageLayout.Stretch;
                                ButtonPic.FlatStyle = FlatStyle.Flat;
                                ButtonPic.FlatAppearance.BorderSize = 0;
                                ButtonPic.ContextMenuStrip = GuiMenu;
                                ButtonPic.Location = new Point(Convert.ToInt32(X), Convert.ToInt32(Y));
                                ButtonPic.Text = SelectBtImTxt.SelectFileName;
                                ButtonPic.Tag = "button:" + SelectBtImTxt.SelectFileName;
                                ButtonPic.Name = SelectBtImTxt.SelectFileName + ".bt";
                                ButtonPic.Font = new Font("楷体", 12, FontStyle.Bold);
                                this.GuiArea.Controls.Add(ButtonPic);
                                GuiComboBox.Items.Add("按钮：" + SelectBtImTxt.SelectFileName + ".yml");
                                ImageControl btctrl = new ImageControl();
                                btctrl.Name = SelectBtImTxt.SelectFileName;
                                btctrl.label1.Text = SelectBtImTxt.SelectFileName + ".png(bt)";
                                btctrl.pictureBox1.ImageLocation = FilePath;
                                btctrl.pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                                btctrl.Size = new Size(150, 54);
                                btctrl.Location = new Point(0, Calcbt());
                                ButtonPanel.Controls.Add(btctrl);
                                LogsTxt.AppendText(">>>添加按钮：" + SelectBtImTxt.SelectFileName + ".png" + "\r\n");
                                string button = FilePath.Substring(FilePath.LastIndexOf("textures") + 9);  //button.png
                                XmlMethods.CreateBtTree(ButtonsPath + SelectBtImTxt.SelectFileName + ".xml", "10", SelectBtImTxt.SelectFileName, "[local]" + button, "[local]" + button, "100", "100",
                                    ButtonPic.Width.ToString(), ButtonPic.Height.ToString(), "say Vge's button|", "false", "false", "-") ;
                                ButtonPic.BringToFront();
                                SGui.WireControl(ButtonPic);
                                ButtonPic.MouseHover += new EventHandler(this.ButtonMouseHover);
                                ButtonPic.MouseLeave += new EventHandler(this.ButtonMouseLeave);
                                break;
                            }
                    }
                    SelectBtImTxt.SelectedItem = "";
                }
                catch
                {

                }
            }
            else
            {
                LogsTxt.AppendText("\r\n不支持的文件类型，图片仅可.png类型，配置文件仅可.yml");
            }
            #endregion
        }
        #region 计算
        int result = 0;
        int j = 0;
        public int Calcbt()
        {
            if (j <= 1)
            {
                result = 0;
                j++;
            }
            if (j > 1)
            {
                result += 56;
            }
            return result;
        }
        int result1 = 0;
        int j1 = 0;
        public int Calcim()
        {
            if (j1 <= 1)
            {
                result1 = 0;
                j1++;
            }
            if (j1 > 1)
            {
                result1 += 56;
            }
            return result1;
        }
        int result2 = 0;
        int j2 = 0;
        public int Calctxt()
        {
            if (j2 <= 1)
            {
                result2 = 0;
                j2++;
            }
            if (j2 > 1)
            {
                result2 += 56;
            }
            return result2;
        }
        int result3 = 0;
        int j3 = 0;
        public int Calcsl()
        {
            if (j3 <= 1)
            {
                result3 = 0;
                j3++;
            }
            if (j3 > 1)
            {
                result3 += 56;
            }
            return result3;
        }
        private void Main_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.All;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }
        #endregion
        private void GuiCodeTxt_TextChanged(object sender, EventArgs e)
        {
            //showLineNo();
        }

        private void GuiCodeTxt_VScroll(object sender, EventArgs e)
        {
            //showLineNo();
        }
        ToolTip ImTip = new ToolTip();
        private void ImageMouseEnter(object sender, EventArgs e)
        {
            try
            {
                PictureBox PicHover = (PictureBox)sender;
                string Tag = PicHover.Tag.ToString();
                string filename = Tag.Substring(Tag.LastIndexOf(":") + 1);
                //MessageBox.Show(filename);
                //不用判断图像还是按钮还是文本，只针对于Image的MouseHover事件
                XmlDocument xml = new XmlDocument();
                xml.Load(ImagesPath + filename + ".xml");
                XmlNode ImageNode = xml.SelectSingleNode("Image");
                XmlNodeList ImageNodes = ImageNode.ChildNodes;
                string TextFileName = ImageNodes.Item(7).InnerText;
                if (TextFileName == "")
                {
                    return; //如果为空直接结束
                }
                FileInfo xmlFile = new FileInfo(TextsPath + TextFileName + ".xml");  //判断是否存在，如果不存在直接关闭，防止删除该控件xml文件而导致异常
                if (!xmlFile.Exists)
                {
                    return;
                }
                xml.Load(TextsPath + TextFileName + ".xml");
                XmlNode TextNode = xml.SelectSingleNode("Text");
                XmlNodeList TextNodeList = TextNode.ChildNodes;
                string HoverText = TextNodeList.Item(3).InnerText;
                string[] HoverTexts = HoverText.Split(new char[] { '|' });
                string ReallyHoverText = "";
                for (int i = 0; i < HoverTexts.Length; i++)
                {
                    ReallyHoverText += HoverTexts[i] + "\n";
                }
                ImTip.Hide(PicHover);
                ImTip.AutoPopDelay = 5000; //提示信息的可见时间
                ImTip.InitialDelay = 100; //事件触发后多久出现提示
                ImTip.ReshowDelay = 10;//指针从一个控件移向另一个控件时，经过多久才会显示下一个提示框
                ImTip.ShowAlways = true; //是否显示提示框
                ImTip.BackColor = Color.FromArgb(17, 1, 15);
                ImTip.Show(ReallyHoverText, PicHover);
            }
            catch
            {
                //MessageBox.Show("未在服务端vv配置路径找到相应的悬浮文本");
            }
        }
        private void ButtonMouseHover(object sender, EventArgs e)
        {
            try
            {
                DcButton BtHover = (DcButton)sender;
                string Tag = BtHover.Tag.ToString();
                string filename = Tag.Substring(Tag.LastIndexOf(":") + 1);
                XmlDocument xml = new XmlDocument();
                FileInfo Btxml = new FileInfo(ButtonsPath + filename + ".xml");
                if (!Btxml.Exists)
                {
                    return;
                }
                xml.Load(ButtonsPath + filename + ".xml");
                XmlNode ButtonNode = xml.SelectSingleNode("Button");
                XmlNodeList ButtonNodeList = ButtonNode.ChildNodes;
                string url2 = ButtonNodeList.Item(3).InnerText;
                if (url2.IndexOf("https") == 0 || url2.IndexOf("http") == 0)
                {
                    BtHover.BackgroundImage = Image.FromStream(WebRequest.Create(url2).GetResponse().GetResponseStream());
                }
                else
                {
                    string CompletelyUrlName = url2.Replace("[local]", (StartClientpath + "\\").Replace("\\", "/"));
                    BtHover.BackgroundImage = Image.FromFile(CompletelyUrlName);
                }
            }
            catch
            {
                //MessageBox.Show("没有在客户端vv素材路径在找到悬浮贴图");
            }
        }
        private void ButtonMouseLeave(object sender, EventArgs e)
        {
            try
            {
                DcButton BtLeave = (DcButton)sender;
                string Tag = BtLeave.Tag.ToString();
                string filename = Tag.Substring(Tag.LastIndexOf(":") + 1);
                XmlDocument xml = new XmlDocument();
                FileInfo Btxml = new FileInfo(ButtonsPath + filename + ".xml");
                if (!Btxml.Exists)
                {
                    return;
                }
                //WebRequest.Create("http://x.com/x.png").GetResponse().GetResponseStream()
                xml.Load(ButtonsPath + filename + ".xml");
                XmlNode ButtonNode = xml.SelectSingleNode("Button");
                XmlNodeList ButtonNodeList = ButtonNode.ChildNodes;
                string Url1 = ButtonNodeList.Item(2).InnerText;
                if (Url1.IndexOf("https") == 0 || Url1.IndexOf("http") == 0)
                {
                    BtLeave.BackgroundImage = Image.FromStream(WebRequest.Create(Url1).GetResponse().GetResponseStream());
                }
                else
                {
                    string CompletelyUrlName = Url1.Replace("[local]", (StartClientpath + "\\").Replace("\\", "/"));
                    BtLeave.BackgroundImage = Image.FromFile(CompletelyUrlName);
                }
            }
            catch
            {
                //MessageBox.Show("没有在客户端vv素材路径在找到默认贴图");
            }
        }
        private void Main_Paint(object sender, PaintEventArgs e)
        {
        }

        private void 删除DToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Control control = GuiMenu.SourceControl;
                string Tag = control.Tag.ToString();
                string Name = control.Name.Substring(0, control.Name.LastIndexOf(".")); //map_title
                if (Tag.StartsWith("image:"))
                {
                    for (int i = 0; i < tabControl1.TabPages.Count; i++)
                    {
                        if (tabControl1.TabPages[i].Text == Name + ".xml [文档]")
                        {
                            tabControl1.TabPages.RemoveAt(i);
                        }
                    }
                    GuiComboBox.SelectedIndex = 0;
                    GuiComboBox.Items.RemoveAt(GuiComboBox.Items.IndexOf("图像：" + Name + ".yml"));
                    ImageControl imagePanelControl = ((ImageControl)ImagePanel.Controls.Find(Name, true)[0]); //利用控件name查找imagepanel的控件并删除
                    ImagePanel.Controls.Remove(imagePanelControl);
                    FileInfo xml = new FileInfo(ImagesPath + Name + ".xml");
                    if (xml.Exists)
                    {
                        xml.Delete();
                    }
                }
                else if (Tag.StartsWith("button:"))
                {
                    //GuiComboBox.Items.RemoveAt(GuiComboBox.Items.IndexOf("按钮：" + Name + ".yml"));
                    for (int i = 0; i < tabControl1.TabPages.Count; i++)
                    {
                        if (tabControl1.TabPages[i].Text == Name + ".xml [文档]")
                        {
                            tabControl1.TabPages.RemoveAt(i);
                        }
                    }
                    GuiComboBox.SelectedIndex = 0;
                    GuiComboBox.Items.RemoveAt(GuiComboBox.Items.IndexOf("按钮：" + Name + ".yml"));
                    ImageControl buttonPanelControl = ((ImageControl)ButtonPanel.Controls.Find(Name, true)[0]);
                    ButtonPanel.Controls.Remove(buttonPanelControl);
                    FileInfo xml = new FileInfo(ButtonsPath + Name + ".xml");
                    if (xml.Exists)
                    {
                        xml.Delete();
                    }
                }
                else if (Tag.StartsWith("text:"))
                {
                    for (int i = 0; i < tabControl1.TabPages.Count; i++)
                    {
                        if (tabControl1.TabPages[i].Text == Name + ".xml [文档]")
                        {
                            tabControl1.TabPages.RemoveAt(i);
                        }
                    }
                    GuiComboBox.SelectedIndex = 0;
                    GuiComboBox.Items.RemoveAt(GuiComboBox.Items.IndexOf("文本：" + Name + ".yml"));
                    TxtControl textPanelControl = ((TxtControl)TextPanel.Controls.Find(Name, true)[0]);
                    TextPanel.Controls.Remove(textPanelControl);
                    FileInfo xml = new FileInfo(TextsPath + Name + ".xml");
                    if (xml.Exists)
                    {
                        xml.Delete();
                    }
                }
                GuiArea.Controls.Remove(GuiMenu.SourceControl);
                Main.HideRectangle(); //隐藏编辑框
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void LinkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://jq.qq.com/?_wv=1027&k=5PogXhZ");
        }

        private void Button1_Click(object sender, EventArgs e)
        {
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            //GuiArea.Controls[0].Update();
        }

        private void Main_Scroll(object sender, ScrollEventArgs e)
        {
            //XY = false;
        }

        private void GuiPropertyGrid_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            try
            {
                string TabTypeName = tabControl1.TabPages[tabControl1.SelectedIndex].Name; //比如 bt,txt,im
                string currentComboName = GuiComboBox.SelectedItem.ToString(); //得到当前GuiComboBox选中的项
                string prefix = currentComboName.Substring(0, currentComboName.LastIndexOf("：")); //forexample: 图像，按钮
                string YmlName = currentComboName.Substring(currentComboName.LastIndexOf("：") + 1); //forexample:map_title.yml
                string FileName = YmlName.Substring(0, YmlName.LastIndexOf(".")); //forexample:map_title
                string currentTabName = Main.Mainfrm.tabControl1.TabPages[Main.Mainfrm.tabControl1.SelectedIndex].Text.Substring(Main.Mainfrm.tabControl1.TabPages[Main.Mainfrm.tabControl1.SelectedIndex].Text.IndexOf("：") + 1);  //map_title.yml [文档]
                string CurrentTabName = currentTabName.Substring(0, currentTabName.LastIndexOf("[") - 5);
                switch (prefix)
                {
                    case "GUI":
                        {
                            Main.Mainfrm.GuiPropertyGrid.SelectedObject = FGuiFrame.gui;
                            XmlMethods.ModifyGui(GuisPath + FileName + ".xml");
                            Main.Mainfrm.GuiPropertyGrid.SelectedObject = FGuiFrame.gui;
                            PictureBox PicGui = ((PictureBox)GuiArea.Controls.Find(FileName + ".gui", true)[0]); //根据控件名获得控件实例
                            PicGui.ImageLocation = FGuiFrame.gui.GuiUrl.Replace("[local]", (StartClientpath + "\\").Replace("\\", "/"));
                            PicGui.Width = (int)(double.Parse(FGuiFrame.gui.XShow));
                            PicGui.Height = (int)(double.Parse(FGuiFrame.gui.YShow));
                            Main.HideRectangleGui();
                            break;
                        }
                    case "图像":
                        {
                            Main.Mainfrm.GuiPropertyGrid.SelectedObject = SGuiFrame.Im;
                            XmlMethods.ModifyIm(ImagesPath + FileName + ".xml");
                            Main.Mainfrm.GuiPropertyGrid.SelectedObject = SGuiFrame.Im;
                            PictureBox PicIm = ((PictureBox)GuiArea.Controls.Find(FileName + ".im", true)[0]);
                            PicIm.ImageLocation = SGuiFrame.Im.ImageUrl.Replace("[local]", (StartClientpath + "\\").Replace("\\", "/"));
                            double X, Y = 0;
                            if (SGuiFrame.Im.X.IndexOf("-") == -1)   //如果X坐标是正数
                            {
                                X = double.Parse(SGuiFrame.Im.X) + 250;
                            }
                            else //不是正数的话
                            {
                                X = Gui_X - Math.Abs(double.Parse(SGuiFrame.Im.X));
                            }
                            if (SGuiFrame.Im.Y.IndexOf("-") == -1)  //如果Y坐标是正数
                            {
                                Y = double.Parse(SGuiFrame.Im.Y) + 250;
                            }
                            else //不是正数的话
                            {
                                Y = Gui_Y - Math.Abs(double.Parse(SGuiFrame.Im.Y));
                            }
                            PicIm.Location = new Point(Convert.ToInt32(X), Convert.ToInt32(Y));
                            //MessageBox.Show(SGuiFrame.Im.X.ToString() + " " + SGuiFrame.Im.Y.ToString());
                            PicIm.Width = (int)(double.Parse(SGuiFrame.Im.XShow));
                            PicIm.Height = (int)(double.Parse(SGuiFrame.Im.YShow));
                            Main.HideRectangle();
                            break;
                        }
                    case "按钮":
                        {
                            Main.Mainfrm.GuiPropertyGrid.SelectedObject = SGuiFrame.Bt;
                            XmlMethods.ModifyBt(ButtonsPath + FileName + ".xml");
                            Main.Mainfrm.GuiPropertyGrid.SelectedObject = SGuiFrame.Bt;
                            DcButton PicBt = ((DcButton)GuiArea.Controls.Find(FileName + ".bt", true)[0]);
                            //MessageBox.Show(SGuiFrame.Bt.Url1.Replace("[local]", (StartClientpath + "\\").Replace("\\", "/")));
                            double X, Y = 0;
                            if (SGuiFrame.Bt.X.IndexOf("-") == -1)   //如果X坐标是正数
                            {
                                X = double.Parse(SGuiFrame.Bt.X) + 250;
                            }
                            else //不是正数的话
                            {
                                X = Gui_X - Math.Abs(double.Parse(SGuiFrame.Bt.X));
                            }
                            if (SGuiFrame.Bt.Y.IndexOf("-") == -1)  //如果Y坐标是正数
                            {
                                Y = double.Parse(SGuiFrame.Bt.Y) + 250;
                            }
                            else //不是正数的话
                            {
                                Y = Gui_Y - Math.Abs(double.Parse(SGuiFrame.Bt.Y));
                            }
                            PicBt.BackgroundImage = Image.FromFile(SGuiFrame.Bt.Url1.Replace("[local]", (StartClientpath + "\\").Replace("\\", "/")));
                            PicBt.Location = new Point(Convert.ToInt32(X), Convert.ToInt32(Y));
                            PicBt.BackgroundImageLayout = ImageLayout.Stretch;
                            PicBt.FlatStyle = FlatStyle.Flat;
                            PicBt.FlatAppearance.BorderSize = 0;
                            PicBt.Width = (int)(double.Parse(SGuiFrame.Bt.Width));
                            PicBt.Height = (int)(double.Parse(SGuiFrame.Bt.High));
                            PicBt.Text = SGuiFrame.Bt.Name;
                            PicBt.Font = new Font("楷体", 12, FontStyle.Bold);
                            Main.HideRectangle();
                            break;
                        }
                    case "文本":
                        {
                            Main.Mainfrm.GuiPropertyGrid.SelectedObject = SGuiFrame.Txt;
                            XmlMethods.ModifyTxt(TextsPath + FileName + ".xml");
                            Main.Mainfrm.GuiPropertyGrid.SelectedObject = SGuiFrame.Txt;
                            Label LbTxt = ((Label)GuiArea.Controls.Find(FileName + ".txt", true)[0]);
                            double X, Y = 0;
                            if (SGuiFrame.Txt.X.IndexOf("-") == -1)   //如果X坐标是正数
                            {
                                X = double.Parse(SGuiFrame.Txt.X) + 250;
                            }
                            else //不是正数的话
                            {
                                X = Gui_X - Math.Abs(double.Parse(SGuiFrame.Txt.X));
                            }
                            if (SGuiFrame.Txt.Y.IndexOf("-") == -1)  //如果Y坐标是正数
                            {
                                Y = double.Parse(SGuiFrame.Txt.Y) + 250;
                            }
                            else //不是正数的话
                            {
                                Y = Gui_Y - Math.Abs(double.Parse(SGuiFrame.Txt.Y));
                            }
                            LbTxt.Location = new Point(Convert.ToInt32(X), Convert.ToInt32(Y));
                            string[] texts = SGuiFrame.Txt.Texts.Split(new char[] { '|' });
                            LbTxt.Text = "";
                            for (int i = 0; i < texts.Length; i++)
                            {
                                LbTxt.Text += texts[i] + "\n";
                            }
                            Main.HideRectangle();
                            break;
                        }
                    default:
                        {
                            break;
                        }
                }
                if (tabControl1.TabPages[tabControl1.SelectedIndex].Tag == "image" && TabTypeName == "im")
                {
                    using (FileStream fs = new FileStream(ImagesPath + CurrentTabName + ".xml", FileMode.Open, FileAccess.Read, FileShare.Read))
                    {
                        using (StreamReader sr = new StreamReader(fs))
                        {
                            tabControl1.TabPages[tabControl1.SelectedIndex].Controls[0].Controls[0].Text = sr.ReadToEnd();
                            sr.Close();
                        }
                        fs.Close();
                    }
                }
                else if (tabControl1.TabPages[tabControl1.SelectedIndex].Tag == "button" && TabTypeName == "bt")
                {
                    using (FileStream fs = new FileStream(ButtonsPath + CurrentTabName + ".xml", FileMode.Open, FileAccess.Read, FileShare.Read))
                    {
                        using (StreamReader sr = new StreamReader(fs))
                        {
                            tabControl1.TabPages[tabControl1.SelectedIndex].Controls[0].Controls[0].Text = sr.ReadToEnd();
                            sr.Close();
                        }
                        fs.Close();
                    }
                }
                else if (tabControl1.TabPages[tabControl1.SelectedIndex].Tag == "text" && TabTypeName == "txt")
                {
                    using (FileStream fs = new FileStream(TextsPath + CurrentTabName + ".xml", FileMode.Open, FileAccess.Read, FileShare.Read))
                    {
                        using (StreamReader sr = new StreamReader(fs))
                        {
                            tabControl1.TabPages[tabControl1.SelectedIndex].Controls[0].Controls[0].Text = sr.ReadToEnd();
                            sr.Close();
                        }
                        fs.Close();
                    }
                }
            }
            catch(Exception ex)
            {
                //MessageBox.Show(ex.Message);
            }
        }

        private void 窗口WToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
        int oldindex = -1;
        string Tag = "";
        string Bfstr = "";
        string newTag = "";
        string newstr = "";
        private void GuiComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (oldindex == -1)
                {
                    oldindex = GuiComboBox.SelectedIndex;
                }
                else
                {
                    Tag = GuiComboBox.Items[oldindex].ToString();
                    newTag = GuiComboBox.Items[GuiComboBox.SelectedIndex].ToString();
                    Bfstr = GuiComboBox.Items[oldindex].ToString().Substring(GuiComboBox.Items[oldindex].ToString().LastIndexOf("：") + 1).Substring(0, GuiComboBox.Items[oldindex].ToString().Substring(GuiComboBox.Items[oldindex].ToString().LastIndexOf("：") + 1).LastIndexOf("."));
                    newstr = GuiComboBox.Items[GuiComboBox.SelectedIndex].ToString().Substring(GuiComboBox.Items[GuiComboBox.SelectedIndex].ToString().LastIndexOf("：") + 1).Substring(0, GuiComboBox.Items[GuiComboBox.SelectedIndex].ToString().Substring(GuiComboBox.Items[GuiComboBox.SelectedIndex].ToString().LastIndexOf("：") + 1).LastIndexOf("."));
                    if (Tag.StartsWith("GUI："))
                    {
                        XmlMethods.ModifyGui(GuisPath + Bfstr + ".xml");
                    }
                    else if (Tag.StartsWith("图像："))
                    {
                        XmlMethods.ModifyIm(ImagesPath + Bfstr + ".xml");
                    }
                    else if (Tag.StartsWith("按钮："))
                    {
                        XmlMethods.ModifyBt(ButtonsPath + Bfstr + ".xml");
                    }
                    else if (Tag.StartsWith("文本："))
                    {
                        XmlMethods.ModifyTxt(TextsPath + Bfstr + ".xml");
                    }

                    if (newTag.StartsWith("GUI："))
                    {
                        XmlMethods.ReadGui(GuisPath + newstr + ".xml");
                        GuiPropertyGrid.SelectedObject = FGuiFrame.gui;
                    }
                    else if (newTag.StartsWith("图像："))
                    {
                        XmlMethods.ReadIm(ImagesPath + newstr + ".xml");
                        GuiPropertyGrid.SelectedObject = SGuiFrame.Im;
                    }
                    else if (newTag.StartsWith("按钮："))
                    {
                        XmlMethods.ReadBt(ButtonsPath + newstr + ".xml");
                        GuiPropertyGrid.SelectedObject = SGuiFrame.Bt;
                    }
                    else if (newTag.StartsWith("文本："))
                    {
                        XmlMethods.ReadTxt(TextsPath + newstr + ".xml");
                        GuiPropertyGrid.SelectedObject = SGuiFrame.Txt;
                    }
                    oldindex = GuiComboBox.SelectedIndex;
                }
            }
            catch(Exception ex)
            {
                //MessageBox.Show(ex.Message);
            }
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

        private void 打开GUI配置文件GToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openGui = new OpenFileDialog();
            openGui.InitialDirectory = StartServerpath + "\\gui\\";
            if (openGui.ShowDialog() == DialogResult.OK)
            {
                string FilePath = openGui.FileName;
                if (FilePath.Substring(FilePath.LastIndexOf("\\") + 1).EndsWith(".yml"))
                {
                    StartServerpath = FilePath.Substring(0, FilePath.LastIndexOf("VexView")) + "VexView\\";  //最大BUG!!!!!!!!!!!!
                }
                string clientpath = StartClientpath + "\\";
                #region 注释
                string Extention = FilePath.Substring(FilePath.LastIndexOf('.'));
                if (Extention == ".yml")
                {
                    if (FilePath.IndexOf(BoolServerPath) == -1)
                    {
                        LogsTxt.AppendText(">>>该yml文件路径不在第一次绑定服务端VexView目录内\r\n");
                        return;
                    }
                    Methods method_Gui = new Methods();
                    JsonData jsonData = JsonMapper.ToObject(Methods.YamlToJson(FilePath));
                    if (!((jsonData.ContainsKey("image")) | (jsonData.ContainsKey("buttons"))))
                    {
                        LogsTxt.AppendText(">>>无效yml文件,仅支持GUI配置文件\r\n");
                        return;
                    }
                    LoadXmlPath(); //删除各目录下文件
                    this.GuiArea.BackgroundImage = null;
                    this.GuiArea.Controls.Clear();
                    TabPage first = tabControl1.TabPages[0];
                    foreach (TabPage tab in tabControl1.TabPages)
                    {
                        if (tab != first)
                        {
                            tabControl1.TabPages.Remove(tab);
                        }
                    }
                    GuiComboBox.Items.Clear();
                    ImagePanel.Controls.Clear();
                    ButtonPanel.Controls.Clear();
                    TextPanel.Controls.Clear();
                    GuiComboBox.Items.Clear();
                    GuiPropertyGrid.SelectedObject = null;
                    LogsTxt.Text = ""; //清空日志
                    result = 0; //清零
                    j = 0;
                    result1 = 0;
                    j1 = 0;
                    result2 = 0;
                    j2 = 0;
                    bolGui = BoolGui.Ok2;
                    try
                    {
                        method_Gui.SuppleMentGui(FilePath);
                        method_Gui.ReadGuiYML(FilePath);
                        LogsTxt.AppendText(">>>解析GUI：" + FilePath.Substring(FilePath.LastIndexOf("\\") + 1) + "\r\n");
                        PictureBox GuiImg = new PictureBox
                        {
                            ImageLocation = method_Gui.Url.Trim('\'').Replace("[local]", clientpath.Replace("\\", "/")),
                            Width = (int)(Convert.ToDouble(method_Gui.XShow)),
                            Height = (int)(Convert.ToDouble(method_Gui.YShow)),
                            Location = new Point(GuiArea.Location.X + 250, GuiArea.Location.Y + 250),
                            SizeMode = PictureBoxSizeMode.StretchImage
                        };
                        string guiname = FilePath.Substring(FilePath.LastIndexOf("\\") + 1).Substring(0, FilePath.Substring(FilePath.LastIndexOf("\\") + 1).LastIndexOf("."));
                        GuiComboBox.Items.Add("GUI：" + FilePath.Substring(FilePath.LastIndexOf("\\") + 1));
                        窗口WToolStripMenuItem.DropDownItems[6].Text = i + " " + guiname + ".yml" + " [设计]";
                        tabControl1.TabPages[0].Text = "GUI：" + guiname + ".yml" + " [设计]";
                        this.GuiArea.Controls.Add(GuiImg);
                        guiXmlPath = GuisPath + guiname + ".xml";
                        XmlMethods.CreateGuiTree(guiXmlPath, method_Gui.Url, method_Gui.X, method_Gui.Y, method_Gui.Width,
                            method_Gui.High, method_Gui.XShow, method_Gui.YShow);
                        GuiImg.Tag = "gui:" + FilePath.Substring(FilePath.LastIndexOf("\\") + 1).Substring(0, FilePath.Substring(FilePath.LastIndexOf("\\") + 1).LastIndexOf(".")); //gui:gui
                        GuiImg.Name = FilePath.Substring(FilePath.LastIndexOf("\\") + 1).Substring(0, FilePath.Substring(FilePath.LastIndexOf("\\") + 1).LastIndexOf(".")) + ".gui"; //gui:gui.yml                                                                                                                                                 
                        Gui_X = GuiImg.Location.X;
                        Gui_Y = GuiImg.Location.Y;
                        GuiImg.SendToBack();
                        FGui.WireControl(GuiImg);
                        GuiComboBox.SelectedIndex = 0;
                        XmlMethods.ReadGui(GuisPath + guiname + ".xml");
                        GuiPropertyGrid.SelectedObject = FGuiFrame.gui;
                    }
                    catch
                    {

                    }
                    JsonData jsonbt = jsonData["buttons"];
                    JsonData jsonim = jsonData["image"];
                    JsonData jsontxt = jsonData["text"];
                    //JsonData jsonsl = jsonData["slot"];
                    Methods method_Pro = new Methods();
                    //read the image
                    try
                    {
                        string images = "";
                        if (jsonim != null)
                        {
                            foreach (JsonData im in jsonim)
                            {
                                if (im == null)
                                {
                                    break;
                                }
                                if (im.ToJson().ToString().IndexOf("{") == -1)
                                {
                                    images = Regex.Unescape(im.ToJson().ToString().Substring(im.ToJson().ToString().IndexOf("\"") + 1, im.ToJson().ToString().IndexOf("\"", im.ToJson().ToString().IndexOf("\"") + 1) - 1));
                                }
                                else
                                {
                                    images = Regex.Unescape(im.ToJson().ToString().Substring(im.ToJson().ToString().IndexOf("\"") + 1, im.ToJson().ToString().IndexOf("\"", im.ToJson().ToString().IndexOf("\"") + 1) - 2));
                                }
                                Impath = StartServerpath + "image\\" + images + ".yml";
                                method_Pro.SuppleMentIm(Impath);
                                using (FileStream fr = new FileStream(Impath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                                {
                                    using (StreamReader sr = new StreamReader(fr))
                                    {
                                        if (sr.ReadToEnd() != string.Empty)
                                        {
                                            //MessageBox.Show(Impath);
                                            method_Pro.ReadImage(Impath);
                                            //MessageBox.Show(Impath);
                                            double X, Y = 0;
                                            if (method_Pro.X_I.IndexOf("-") == -1)   //如果X坐标是正数
                                            {
                                                X = double.Parse(method_Pro.X_I) + 250; // +200回归坐标系
                                            }
                                            else //不是正数的话
                                            {
                                                X = Gui_X - Math.Abs(double.Parse(method_Pro.X_I));
                                            }
                                            if (method_Pro.Y_I.IndexOf("-") == -1)  //如果Y坐标是正数
                                            {
                                                Y = double.Parse(method_Pro.Y_I) + 250; // +200回归坐标系
                                            }
                                            else //不是正数的话
                                            {
                                                Y = Gui_Y - Math.Abs(double.Parse(method_Pro.Y_I));
                                            }
                                            PictureBox imimage = new PictureBox
                                            {
                                                ImageLocation = method_Pro.Url_I.Replace("[local]", clientpath.Replace("\\", "/")), //路径 
                                                Location = new Point(Convert.ToInt32(X), Convert.ToInt32(Y)),
                                                SizeMode = PictureBoxSizeMode.StretchImage,
                                                Width = (int)(Convert.ToDouble(method_Pro.XShow_I)),
                                                Height = (int)(Convert.ToDouble(method_Pro.YShow_I))
                                                //BackColor = Color.Transparent
                                            };
                                            this.GuiArea.Controls.Add(imimage);
                                            imXmlPath = ImagesPath + images + ".xml";
                                            XmlMethods.CreateImTree(imXmlPath, method_Pro.Url_I, method_Pro.X_I, method_Pro.Y_I, method_Pro.Width_I,
                                                method_Pro.High_I, method_Pro.XShow_I, method_Pro.YShow_I, method_Pro.HoverText);
                                            imimage.Tag = "image:" + images;
                                            imimage.Name = images + ".im";
                                            GuiComboBox.Items.Add("图像：" + images + ".yml");
                                            ImageControl imctrl = new ImageControl();
                                            imctrl.Name = images; //images下面
                                            imctrl.label1.Text = images + ".png(im)";
                                            if (method_Pro.Url_I.IndexOf("http") == 0 || method_Pro.Url_I.IndexOf("https") == 0)
                                            {
                                                imctrl.pictureBox1.ImageLocation = method_Pro.Url_I;
                                            }
                                            else
                                            {
                                                imctrl.pictureBox1.ImageLocation = clientpath + method_Pro.Url_I.Substring(method_Pro.Url_I.LastIndexOf("]") + 1);
                                            }
                                            imctrl.pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                                            imctrl.Size = new Size(150, 54);
                                            imctrl.Location = new Point(0, Calcim());
                                            ImagePanel.Controls.Add(imctrl);
                                            LogsTxt.AppendText(">>>解析图像：" + images + ".yml" + "\r\n");
                                            imimage.BringToFront();
                                            SGui.WireControl(imimage);
                                            imimage.ContextMenuStrip = GuiMenu;
                                            imimage.MouseEnter += new EventHandler(this.ImageMouseEnter);
                                        }
                                        sr.Close();
                                    }
                                    fr.Close();
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        //MessageBox.Show(ex.Message);
                        //MessageBox.Show("解析文件一部分失败，请检查你的文件，确保每个yml文件键的值都存在");
                    }
                    //read the button
                    string buttons = "";
                    if (jsonbt != null)
                    {
                        foreach (JsonData bt in jsonbt)
                        {
                            if (bt == null)
                            {
                                break;
                            }
                            if (bt.ToJson().ToString().IndexOf("{") == -1)
                            {
                                buttons = Regex.Unescape(bt.ToJson().ToString().Substring(bt.ToJson().ToString().IndexOf("\"") + 1, bt.ToJson().ToString().IndexOf("\"", bt.ToJson().ToString().IndexOf("\"") + 1) - 1));
                            }
                            else
                            {
                                buttons = Regex.Unescape(bt.ToJson().ToString().Substring(bt.ToJson().ToString().IndexOf("\"") + 1, bt.ToJson().ToString().IndexOf("\"", bt.ToJson().ToString().IndexOf("\"") + 1) - 2));
                            }
                            Btpath = StartServerpath + "button\\" + buttons + ".yml";
                            method_Pro.SuppleMentBt(Btpath);
                            using (FileStream fr = new FileStream(Btpath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                            {
                                using (StreamReader sr = new StreamReader(fr))
                                {
                                    if (sr.ReadToEnd() != string.Empty)
                                    {
                                        method_Pro.ReadButton(Btpath);
                                        JsonData json = JsonMapper.ToObject(Methods.YamlToJson(Btpath));
                                        string cmd = "";
                                        string cmds = ""; //积累命令
                                        if (json["commands"] != null)
                                        {
                                            JsonData jscmd = json["commands"];
                                            //MessageBox.Show(jscmd.ToJson().ToString());
                                            foreach (JsonData jsCMD in jscmd)
                                            {
                                                if (jsCMD.ToJson().ToString().IndexOf("{") == -1)
                                                {
                                                    cmd = jsCMD.ToJson().ToString().Substring(jsCMD.ToJson().ToString().IndexOf("\"") + 1, jsCMD.ToJson().ToString().IndexOf("\"", jsCMD.ToJson().ToString().IndexOf("\"") + 1) - 1);
                                                }
                                                else
                                                {
                                                    cmd = jsCMD.ToJson().ToString().Substring(jsCMD.ToJson().ToString().IndexOf("\"") + 1, jsCMD.ToJson().ToString().IndexOf("\"", jsCMD.ToJson().ToString().IndexOf("\"") + 1) - 2);
                                                }
                                                cmds += Regex.Unescape(cmd) + "|";
                                            }
                                        }
                                        else
                                        {
                                            cmds = "";
                                        }
                                        //MessageBox.Show(method_BT.X_B);
                                        DcButton btimage = new DcButton();
                                        try
                                        {
                                            //btimage.Parent = GUIArea;
                                            if (method_Pro.Url1_B.IndexOf("https") == 0 || method_Pro.Url1_B.IndexOf("http") == 0)
                                            {
                                                btimage.BackgroundImage = Image.FromStream(WebRequest.Create("https://s2.ax1x.com/2019/08/13/mPJ5yF.png").GetResponse().GetResponseStream());
                                            }
                                            else
                                            {
                                                btimage.BackgroundImage = Image.FromFile(method_Pro.Url1_B.Trim('\'').Replace("[local]", clientpath.Replace("\\", "/"))); //路径                       
                                            }
                                            double X, Y = 0;
                                            if (method_Pro.X_B.IndexOf("-") == -1)   //如果X坐标是正数
                                            {
                                                X = double.Parse(method_Pro.X_B) + 250;
                                            }
                                            else //不是正数的话
                                            {
                                                X = Gui_X - Math.Abs(double.Parse(method_Pro.X_B));
                                            }
                                            if (method_Pro.Y_B.IndexOf("-") == -1)  //如果Y坐标是正数
                                            {
                                                Y = double.Parse(method_Pro.Y_B) + 250;
                                            }
                                            else //不是正数的话
                                            {
                                                Y = Gui_Y - Math.Abs(double.Parse(method_Pro.Y_B));
                                            }
                                            btimage.Location = new Point(Convert.ToInt32(X), Convert.ToInt32(Y));
                                            //btimage.BackColor = Color.Transparent;
                                            btimage.FlatStyle = FlatStyle.Flat;
                                            btimage.FlatAppearance.BorderSize = 0;
                                            btimage.BackgroundImageLayout = ImageLayout.Stretch;   //调整模式为适应图片大小而更改控件大小   //调整模式为适应图片大小而更改控件大小
                                            btimage.Width = (int)(Convert.ToDouble(method_Pro.Width_B));
                                            btimage.Height = (int)(Convert.ToDouble(method_Pro.High_B));
                                            btimage.Text = method_Pro.Name_B;
                                            btimage.Font = new Font("楷体", 12, FontStyle.Bold);
                                            this.GuiArea.Controls.Add(btimage);  //添加到Gui图上
                                            btXmlPath = ButtonsPath + buttons + ".xml";
                                            XmlMethods.CreateBtTree(btXmlPath, method_Pro.Id, method_Pro.Name_B, method_Pro.Url1_B, method_Pro.Url2_B, method_Pro.X_B, method_Pro.Y_B,
                                                method_Pro.Width_B, method_Pro.High_B, cmds, method_Pro.Asop, method_Pro.Close, method_Pro.To);
                                            btimage.Tag = "button:" + buttons;
                                            btimage.Name = buttons + ".bt";
                                            GuiComboBox.Items.Add("按钮：" + buttons + ".yml");
                                            ImageControl btctrl = new ImageControl();
                                            btctrl.Name = buttons;
                                            btctrl.label1.Text = buttons + ".png(bt)";
                                            if (method_Pro.Url1_B.IndexOf("https") == 0 || method_Pro.Url1_B.IndexOf("http") == 0)
                                            {
                                                btctrl.pictureBox1.ImageLocation = method_Pro.Url1_B;
                                            }
                                            else
                                            {
                                                btctrl.pictureBox1.ImageLocation = clientpath + method_Pro.Url1_B.Substring(method_Pro.Url1_B.LastIndexOf("]") + 1);
                                            }
                                            btctrl.pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                                            btctrl.Size = new Size(150, 54);
                                            btctrl.Location = new Point(0, Calcbt());
                                            ButtonPanel.Controls.Add(btctrl);
                                            LogsTxt.AppendText(">>>解析按钮：" + buttons + ".yml" + "\r\n");
                                            btimage.BringToFront();
                                            //new MoveGui(btimage);
                                            SGui.WireControl(btimage);
                                            btimage.ContextMenuStrip = GuiMenu;
                                            btimage.MouseHover += new EventHandler(this.ButtonMouseHover);
                                            btimage.MouseLeave += new EventHandler(this.ButtonMouseLeave);
                                            //MessageBox.Show(method_Pro.High_B);
                                            //MessageBox.Show(btimage.Parent.Location.X.ToString());
                                        }
                                        catch (Exception ex)
                                        {
                                            //MessageBox.Show(ex.Message);
                                            //MessageBox.Show("解析文件一部分失败，请检查你的文件，确保每个yml文件键的值都存在");
                                        }
                                    }
                                    sr.Close();
                                }
                                fr.Close();
                            }
                        }
                    }
                    //read the text
                    try
                    {
                        string tooltxt = "";
                        string texts = "";
                        if (jsontxt != null)
                        {
                            foreach (JsonData txt in jsontxt)
                            {
                                if (txt == null)
                                {
                                    break;
                                }
                                if (txt.ToJson().ToString().IndexOf("{") == -1)
                                {
                                    texts = Regex.Unescape(txt.ToJson().ToString().Substring(txt.ToJson().ToString().IndexOf("\"") + 1, txt.ToJson().ToString().IndexOf("\"", txt.ToJson().ToString().IndexOf("\"") + 1) - 1));
                                }
                                else
                                {
                                    texts = Regex.Unescape(txt.ToJson().ToString().Substring(txt.ToJson().ToString().IndexOf("\"") + 1, txt.ToJson().ToString().IndexOf("\"", txt.ToJson().ToString().IndexOf("\"") + 1) - 2));
                                }
                                TxtPath = StartServerpath + "text\\" + texts + ".yml";
                                method_Pro.SuppleMentTxt(TxtPath);
                                using (FileStream fs = new FileStream(TxtPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                                {
                                    using (StreamReader sr = new StreamReader(fs))
                                    {
                                        if (sr.ReadToEnd() != string.Empty)
                                        {
                                            method_Pro.ReadText(TxtPath);
                                            JsonData json = JsonMapper.ToObject(Methods.YamlToJson(TxtPath)); //读取text
                                            string TXT = "";
                                            string TXTs = ""; //积累命令
                                            if (json["text"] != null)
                                            {
                                                JsonData jsontxt2 = json["text"];
                                                //GuiCodeTxt.Text = jsontxt2.ToJson().ToString();
                                                //MessageBox.Show(jsontxt2.ToJson().ToString());
                                                foreach (JsonData jsTXT in jsontxt2)
                                                {
                                                    if (jsTXT.ToJson().ToString().IndexOf("{") == -1)
                                                    {
                                                        TXT = jsTXT.ToJson().ToString().Substring(jsTXT.ToJson().ToString().IndexOf("\"") + 1, jsTXT.ToJson().ToString().IndexOf("\"", jsTXT.ToJson().ToString().IndexOf("\"") + 1) - 1);
                                                    }
                                                    else
                                                    {
                                                        TXT = jsTXT.ToJson().ToString().Substring(jsTXT.ToJson().ToString().IndexOf("\"") + 1, jsTXT.ToJson().ToString().IndexOf("\"", jsTXT.ToJson().ToString().IndexOf("\"") + 1) - 2);
                                                    }
                                                    TXTs += Regex.Unescape(TXT) + "|";
                                                }
                                            }
                                            else
                                            {
                                                TXTs = "";
                                            }
                                            Label Txtlb = new Label();
                                            //Txtlb.Parent = GuiImg;
                                            //Txtlb.BackColor = Color.Transparent;
                                            double X, Y = 0;
                                            if (method_Pro.X_T.IndexOf("-") == -1)   //如果X坐标是正数
                                            {
                                                X = double.Parse(method_Pro.X_T) + 250;
                                            }
                                            else //不是正数的话
                                            {
                                                X = Gui_X - Math.Abs(double.Parse(method_Pro.X_T));
                                            }
                                            if (method_Pro.Y_T.IndexOf("-") == -1)  //如果Y坐标是正数
                                            {
                                                Y = double.Parse(method_Pro.Y_T) + 250;
                                            }
                                            else //不是正数的话
                                            {
                                                Y = Gui_Y - Math.Abs(double.Parse(method_Pro.Y_T));
                                            }
                                            Txtlb.Font = new Font("宋体", 12, FontStyle.Bold);
                                            Txtlb.AutoSize = true;
                                            Txtlb.Location = new Point(Convert.ToInt32(X), Convert.ToInt32(Y));
                                            string[] TXTS = TXTs.Split(new char[] { '|' });
                                            for (int i = 0; i < TXTS.Length; i++)
                                            {
                                                Txtlb.Text += TXTS[i] + "\n";
                                            }
                                            this.GuiArea.Controls.Add(Txtlb);
                                            txtXmlPath = TextsPath + texts + ".xml";
                                            XmlMethods.CreateTxtTree(txtXmlPath, method_Pro.X_T, method_Pro.Y_T, method_Pro.Scale, TXTs);
                                            Txtlb.Tag = "text:" + texts;
                                            Txtlb.Name = texts + ".txt";
                                            GuiComboBox.Items.Add("文本：" + texts + ".yml");
                                            TxtControl txtctrl = new TxtControl();
                                            txtctrl.Name = texts; //texts
                                            txtctrl.label1.Text = texts + ".txt(txt)"; //。
                                            txtctrl.label2.Text = TXTs; //多行文本以一行显示
                                            txtctrl.Size = new Size(150, 54);
                                            txtctrl.Location = new Point(0, Calctxt());
                                            TextPanel.Controls.Add(txtctrl);
                                            LogsTxt.AppendText(">>>解析文本：" + texts + ".yml" + "\r\n");
                                            Txtlb.BringToFront();
                                            SGui.WireControl(Txtlb);
                                            tooltxt = ""; //清空文本
                                            Txtlb.ContextMenuStrip = GuiMenu;
                                        }
                                        sr.Close();
                                    }
                                    fs.Close();
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        //MessageBox.Show(ex.Message);
                    }
                }
            }
        }


        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            LoadXmlPath();
        }
        private void 新建文本TToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (bolGui == BoolGui.Ok || bolGui == BoolGui.Ok2)
            {
                new SelectText().ShowDialog();
                string txts = "";
                string[] Texts = SelectText.SelectedText.Split(new char[] { '\r' });
                Label SeLb = new Label();
                for (int i = 0; i < Texts.Length; i++)
                {
                    SeLb.Text += Texts[i] + "\n";
                    txts += Texts[i] + "|"; //用于在textpanel区域显示
                }
                string x = "100", y = "100";
                double X, Y = 0;
                if (x.IndexOf("-") == -1)   //如果X坐标是正数
                {
                    X = double.Parse(x) + 250; // +250回归坐标系
                }
                else //不是正数的话
                {
                    X = Gui_X - Math.Abs(double.Parse(x));
                }
                if (y.IndexOf("-") == -1)  //如果Y坐标是正数
                {
                    Y = double.Parse(x) + 250; // +250回归坐标系
                }
                else //不是正数的话
                {
                    Y = Gui_Y - Math.Abs(double.Parse(x));
                }
                SeLb.Location = new Point(Convert.ToInt32(X), Convert.ToInt32(Y));
                SeLb.Tag = "text:" + SelectText.TextName;
                SeLb.Name = SelectText.TextName + ".txt";
                SeLb.Font = new Font("宋体", 12, FontStyle.Bold);
                SeLb.AutoSize = true;
                this.GuiArea.Controls.Add(SeLb);
                SGui.WireControl(SeLb);
                SeLb.BringToFront();
                GuiComboBox.Items.Add("文本：" + SelectText.TextName + ".yml");
                TxtControl txtctrl = new TxtControl();
                txtctrl.Name = SelectText.TextName;
                txtctrl.label1.Text = SelectText.TextName + ".txt(txt)"; //。
                txtctrl.label2.Text = txts; //多行文本以一行显示
                txtctrl.Size = new Size(150, 54);
                txtctrl.Location = new Point(0, Calctxt());
                TextPanel.Controls.Add(txtctrl);
                LogsTxt.AppendText(">>>添加文本：" + SelectText.TextName + ".txt" + "\r\n");
                XmlMethods.CreateTxtTree(TextsPath + SelectText.TextName + ".xml", (GuiArea.Location.X + 350).ToString(), (GuiArea.Location.Y + 350).ToString(), "1.0", txts);
                SeLb.ContextMenuStrip = GuiMenu;
            }
            else
            {
                MessageBox.Show("还未开始编辑");
            }
        }
        //生成vexview目录
        private void 保存SToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (bolGui == BoolGui.Ok || bolGui == BoolGui.Ok2)
                {
                    string imageFileName = "";
                    string[] imagesFileName;
                    string buttonFileName = "";
                    string[] buttonsFileName;
                    string textFileName = "";
                    string[] textsFileName;
                    FolderBrowserDialog browserVex = new FolderBrowserDialog();
                    if (browserVex.ShowDialog() == DialogResult.OK)
                    {
                        Vexview = browserVex.SelectedPath;
                        if (!Directory.Exists(Vexview + "\\VexView"))
                        {
                            Directory.CreateDirectory(Vexview + "\\VexView\\");
                            Directory.CreateDirectory(Vexview + "\\VexView\\gui");
                            Directory.CreateDirectory(Vexview + "\\VexView\\image");
                            Directory.CreateDirectory(Vexview + "\\VexView\\button");
                            Directory.CreateDirectory(Vexview + "\\VexView\\text");
                            //获取image文件名
                            foreach (string filenameEx in Directory.GetFiles(ImagesPath))
                            {
                                string filename = filenameEx.Substring(filenameEx.LastIndexOf("\\") + 1).Substring(0, filenameEx.Substring(filenameEx.LastIndexOf("\\") + 1).LastIndexOf("."));
                                imageFileName += filename + "|";
                                GenerateYML.GenerateImage(filenameEx, filename);
                            }
                            imagesFileName = imageFileName.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
                            //获取button文件名
                            foreach (string filenameEx in Directory.GetFiles(ButtonsPath))
                            {
                                string filename = filenameEx.Substring(filenameEx.LastIndexOf("\\") + 1).Substring(0, filenameEx.Substring(filenameEx.LastIndexOf("\\") + 1).LastIndexOf("."));
                                buttonFileName += filename + "|";
                                GenerateYML.GenerateButton(filenameEx, filename);
                            }
                            buttonsFileName = buttonFileName.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
                            //获取text文件名
                            foreach (string filenameEx in Directory.GetFiles(TextsPath))
                            {
                                string filename = filenameEx.Substring(filenameEx.LastIndexOf("\\") + 1).Substring(0, filenameEx.Substring(filenameEx.LastIndexOf("\\") + 1).LastIndexOf("."));
                                textFileName += filename + "|";
                                GenerateYML.GenerateText(filenameEx, filename);
                            }
                            textsFileName = textFileName.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
                            //获取gui文件名
                            foreach (string filenameEx in Directory.GetFiles(GuisPath))
                            {
                                string filename = filenameEx.Substring(filenameEx.LastIndexOf("\\") + 1).Substring(0, filenameEx.Substring(filenameEx.LastIndexOf("\\") + 1).LastIndexOf("."));
                                GenerateYML.GenerateGui(filenameEx, imagesFileName, textsFileName, buttonsFileName, filename);
                            }
                        }
                        else
                        {
                            MessageBox.Show("你选择的目录下已有VexView目录，请转移目录或者删除目录");
                        }
                    }
                }
                else
                {
                    MessageBox.Show("还未开始编辑");
                }
                //GenerateYML.GenerateGui(GuisPath + "map.xml", new string[] { "map_title" }, new string[] { "text", "hud" }, new string[] { "map_lobby", "map_Helios", "map_hp", "map_lvl", "map_d" });
            }
            catch
            { }
        }

        private void 新建按钮BToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("请拖入客户端vexview目录图片到程序面板中");
        }

        private void 新建图像IToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //程序位置
            string strAppFileName = System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName;
            System.Diagnostics.Process myNewProcess = new System.Diagnostics.Process();
            //要启动的应用程序
            myNewProcess.StartInfo.FileName = strAppFileName;
            // 设置要启动的进程的初始目录
            myNewProcess.StartInfo.WorkingDirectory = Application.ExecutablePath;
            //启动程序
            myNewProcess.Start();
            //结束该程序
            Application.Exit();
            //结束该所有线程
            Environment.Exit(0);
        }

        private void 新建文本ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("请拖入客户端vexview目录图片到程序面板中");
        }

        private void 新建物品SToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("暂不支持该功能，后面版本会支持");
        }

        private void Main_Resize(object sender, EventArgs e)
        {
            //if(GuiArea.Controls.Count ==1 && GuiArea.Controls[0].Name == "pictureBox1")
            //{
            //    GuiArea.Refresh();
            //    Bitmap bitmap = new Bitmap(1300, 1300);
            //    Graphics g = Graphics.FromImage(bitmap);
            //    g.DrawString("请拖入GUI底图或GUI配置文件到程序面板中", new Font("楷体", 25, FontStyle.Regular), new SolidBrush(Color.FromArgb(94, 94, 94)), GuiArea.Width / 8 + 200, GuiArea.Height / 2 + GuiArea.Height / 5);
            //    GuiArea.BackgroundImage = bitmap;
            //    pictureBox1.Left = GuiArea.Width / 4;
            //    //pictureBox1.Right = GuiArea.Width / 4;
            //}
        }
        public static void HideRectangle()
        {
            foreach(Label CurrentLb in SGuiFrame.Rectangle)
            {
                CurrentLb.Visible = false;
            }
        }
        public static void HideRectangleGui()
        {
            foreach(Label CurrentLb in FGuiFrame.lbl)
            {
                CurrentLb.Visible = false;
            }
        }

        private void ToolStripButton4_Click(object sender, EventArgs e)
        {
            MessageBox.Show("转用菜单栏中的文件功能");
        }

        private void ToolStripButton5_Click(object sender, EventArgs e)
        {
            MessageBox.Show("转用菜单栏中的文件功能");
        }

        private void 计分板配置文件编辑ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void Button1_Click_1(object sender, EventArgs e)
        {
            MessageBox.Show("X:" + SGuiFrame.Im.X.ToString() + " Y:" + SGuiFrame.Im.Y);
        }

        private void GuiArea_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Main.HideRectangle();
                Main.HideRectangleGui();
            }
        }
        int oldi = -1;
        private void TabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (oldi == -1)
                {
                    oldi = tabControl1.SelectedIndex;
                }
                else
                { //newName无前缀，无后缀
                    string ctrlTypeName = "";
                    //string item = ""; //前缀
                    string ComItemName = "";
                    string CurrentTab = tabControl1.TabPages[tabControl1.SelectedIndex].Text.Substring(0, tabControl1.TabPages[tabControl1.SelectedIndex].Text.LastIndexOf("."));  //图像：map_title
                    string currentTabName = tabControl1.TabPages[tabControl1.SelectedIndex].Text.Substring(tabControl1.TabPages[tabControl1.SelectedIndex].Text.IndexOf("：") + 1);  //map_title.yml [文档]
                    string newName = currentTabName.Substring(0, currentTabName.LastIndexOf("[") - 5);  //map_title
                    string currentTabName1 = tabControl1.TabPages[oldi].Text.Substring(tabControl1.TabPages[oldi].Text.IndexOf("：") + 1);  //map_title.yml [文档]
                    string oldName = currentTabName1.Substring(0, currentTabName1.LastIndexOf("[") - 5); //map_title.yml
                    for (int i = 0; i < GuiComboBox.Items.Count; i++)
                    {
                        //item = GuiComboBox.Items[i].ToString().Substring(0, GuiComboBox.Items[i].ToString().LastIndexOf("：") + 1); //截取前缀 forexample:按钮：
                        ComItemName = GuiComboBox.Items[i].ToString(); //forexample:图像：map.yml
                        if (CurrentTab + ".yml" == ComItemName)   //判断选项页名是否存在下拉框里的项
                        {
                            GuiComboBox.SelectedIndex = GuiComboBox.Items.IndexOf(CurrentTab + ".yml");
                            if (tabControl1.TabPages[oldi].Tag == "image")
                            {
                                FileStream stream = File.Open(ImagesPath + oldName + ".xml", FileMode.Open, FileAccess.Write, FileShare.ReadWrite);
                                stream.Seek(0, SeekOrigin.Begin);
                                stream.SetLength(0); //清空流字节
                                stream.Close();
                                File.WriteAllText(ImagesPath + oldName + ".xml", tabControl1.TabPages[oldi].Controls[0].Controls[0].Text);
                                XmlMethods.ReadIm(ImagesPath + oldName + ".xml");
                                string oldCtName = oldName + ".im";
                                PictureBox PicImage = ((PictureBox)GuiArea.Controls.Find(oldCtName, true)[0]);
                                PicImage.ImageLocation = SGuiFrame.Im.ImageUrl.Replace("[local]", (StartClientpath + "\\").Replace("\\", "/"));
                                PicImage.Width = int.Parse(SGuiFrame.Im.XShow);
                                PicImage.Height = int.Parse(SGuiFrame.Im.YShow);
                                double X, Y = 0;
                                if (SGuiFrame.Im.X.IndexOf("-") == -1)   //如果X坐标是正数
                                {
                                    X = double.Parse(SGuiFrame.Im.X) + 250;
                                }
                                else //不是正数的话
                                {
                                    X = Gui_X - Math.Abs(double.Parse(SGuiFrame.Im.X));
                                }
                                if (SGuiFrame.Im.Y.IndexOf("-") == -1)  //如果Y坐标是正数
                                {
                                    Y = double.Parse(SGuiFrame.Im.Y) + 250;
                                }
                                else //不是正数的话
                                {
                                    Y = Gui_Y - Math.Abs(double.Parse(SGuiFrame.Im.Y));
                                }
                                PicImage.Location = new Point(int.Parse(X.ToString()), int.Parse(Y.ToString()));
                                Main.HideRectangle();
                                //Main.Mainfrm.GuiPropertyGrid.SelectedObject = SGuiFrame.Im;
                            }
                            else if (tabControl1.TabPages[oldi].Tag == "button")
                            {
                                FileStream stream = File.Open(ButtonsPath + oldName + ".xml", FileMode.Open, FileAccess.Write, FileShare.ReadWrite);
                                stream.Seek(0, SeekOrigin.Begin);
                                stream.SetLength(0);
                                stream.Close();
                                File.WriteAllText(ButtonsPath + oldName + ".xml", tabControl1.TabPages[oldi].Controls[0].Controls[0].Text);
                                XmlMethods.ReadBt(ButtonsPath + oldName + ".xml");
                                string oldCtName = oldName + ".bt";
                                DcButton DcButton = ((DcButton)GuiArea.Controls.Find(oldCtName, true)[0]);
                                DcButton.BackgroundImage = Image.FromFile(SGuiFrame.Bt.Url1.Replace("[local]", (StartClientpath + "\\").Replace("\\", "/")));
                                DcButton.Width = int.Parse(SGuiFrame.Bt.Width);
                                DcButton.Height = int.Parse(SGuiFrame.Bt.High);
                                double X, Y = 0;
                                if (SGuiFrame.Bt.X.IndexOf("-") == -1)   //如果X坐标是正数
                                {
                                    X = double.Parse(SGuiFrame.Bt.X) + 250;
                                }
                                else //不是正数的话
                                {
                                    X = Gui_X - Math.Abs(double.Parse(SGuiFrame.Bt.X));
                                }
                                if (SGuiFrame.Bt.Y.IndexOf("-") == -1)  //如果Y坐标是正数
                                {
                                    Y = double.Parse(SGuiFrame.Bt.Y) + 250;
                                }
                                else //不是正数的话
                                {
                                    Y = Gui_Y - Math.Abs(double.Parse(SGuiFrame.Bt.Y));
                                }
                                DcButton.Location = new Point(int.Parse(X.ToString()), int.Parse(Y.ToString()));
                                DcButton.Text = SGuiFrame.Bt.Name;
                                Main.HideRectangle();
                                //Main.Mainfrm.GuiPropertyGrid.SelectedObject = SGuiFrame.Bt;
                            }
                            else if (tabControl1.TabPages[oldi].Tag == "text")
                            {
                                Stream stream = File.Open(TextsPath + oldName + ".xml", FileMode.Open, FileAccess.Write, FileShare.ReadWrite);
                                stream.Seek(0, SeekOrigin.Begin);
                                stream.SetLength(0);
                                stream.Close();
                                File.WriteAllText(TextsPath + oldName + ".xml", tabControl1.TabPages[oldi].Controls[0].Controls[0].Text);
                                XmlMethods.ReadTxt(TextsPath + oldName + ".xml");
                                string oldCtName = oldName + ".txt";
                                Label Lb = ((Label)GuiArea.Controls.Find(oldCtName, true)[0]);
                                Lb.Text = ""; //先清空原来的
                                string[] txts = SGuiFrame.Txt.Texts.Split(new char[] { '|' });
                                for (int j = 0; j < txts.Length; j++)
                                {
                                    Lb.Text += txts[j] + "\n";
                                }
                                double X, Y = 0;
                                if (SGuiFrame.Txt.X.IndexOf("-") == -1)   //如果X坐标是正数
                                {
                                    X = double.Parse(SGuiFrame.Txt.X) + 250;
                                }
                                else //不是正数的话
                                {
                                    X = Gui_X - Math.Abs(double.Parse(SGuiFrame.Txt.X));
                                }
                                if (SGuiFrame.Txt.Y.IndexOf("-") == -1)  //如果Y坐标是正数
                                {
                                    Y = double.Parse(SGuiFrame.Txt.Y) + 250;
                                }
                                else //不是正数的话
                                {
                                    Y = Gui_Y - Math.Abs(double.Parse(SGuiFrame.Txt.Y));
                                }
                                Lb.Location = new Point(int.Parse(X.ToString()), int.Parse(Y.ToString()));
                                Main.HideRectangle();
                                //Main.Mainfrm.GuiPropertyGrid.SelectedObject = SGuiFrame.Txt;
                            }
                            if (tabControl1.TabPages[tabControl1.SelectedIndex].Tag == "image")
                            {
                                using (FileStream fs = new FileStream(ImagesPath + newName + ".xml", FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                                {
                                    using (StreamReader sr = new StreamReader(fs))
                                    {
                                        tabControl1.TabPages[tabControl1.SelectedIndex].Controls[0].Controls[0].Text = sr.ReadToEnd();
                                        sr.Close();
                                    }
                                    fs.Close();
                                }

                            }
                            else if (tabControl1.TabPages[tabControl1.SelectedIndex].Tag == "button")
                            {
                                using (FileStream fs = new FileStream(ButtonsPath + newName + ".xml", FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                                {
                                    using (StreamReader sr = new StreamReader(fs))
                                    {
                                        tabControl1.TabPages[tabControl1.SelectedIndex].Controls[0].Controls[0].Text = sr.ReadToEnd();
                                        sr.Close();
                                    }
                                    fs.Close();
                                }
                            }
                            else if (tabControl1.TabPages[tabControl1.SelectedIndex].Tag == "text")
                            {
                                using (FileStream fs = new FileStream(TextsPath + newName + ".xml", FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                                {
                                    using (StreamReader sr = new StreamReader(fs))
                                    {
                                        tabControl1.TabPages[tabControl1.SelectedIndex].Controls[0].Controls[0].Text = sr.ReadToEnd();
                                        sr.Close();
                                    }
                                    fs.Close();
                                }
                            }
                        }
                    }
                    oldi = tabControl1.SelectedIndex;
                }
                //MessageBox.Show(tabControl1.TabPages[1].Controls[0].Controls[0].Text);
            }
            catch (Exception ex)
            {
                //MessageBox.Show("2");
            }
        }
    }
}
#endregion