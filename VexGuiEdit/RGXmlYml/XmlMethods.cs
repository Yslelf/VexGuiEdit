using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace VexGuiEdit
{
    class XmlMethods
    {
        #region 创建XML树文件
        public static void CreateGuiTree(string xmlpath, string gui, string x, string y, string width, string high, string xshow, string yshow)
        {
            XElement xElement = new XElement(
                new XElement("Gui",
                    new XElement("GuiUrl", gui),
                    new XElement("X", x),
                    new XElement("Y", y),
                    new XElement("Width", width),
                    new XElement("High", high),
                    new XElement("XShow", xshow),
                    new XElement("YShow", yshow)
               )
          );
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Encoding = new UTF8Encoding(false);
            settings.Indent = true;
            XmlWriter xw = XmlWriter.Create(xmlpath, settings);
            xElement.Save(xw);
            xw.Flush();
            xw.Close();
        }

        public static void CreateImTree(string xmlpath, string imageUrl, string x, string y, string width, string high, string xshow, string yshow, string hovertext)
        {
            XElement xElement = new XElement(
                new XElement("Image",
                    new XElement("ImageUrl", imageUrl),
                    new XElement("X", x),
                    new XElement("Y", y),
                    new XElement("Width", width),
                    new XElement("High", high),
                    new XElement("XShow", xshow),
                    new XElement("YShow", yshow),
                    new XElement("HoverText", hovertext)
               )
          );
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Encoding = new UTF8Encoding(false);
            settings.Indent = true;
            XmlWriter xw = XmlWriter.Create(xmlpath, settings);
            xElement.Save(xw);
            xw.Flush();
            xw.Close();
        }

        public static void CreateBtTree(string xmlpath, string id, string name, string url1, string url2, string x, string y, string width, string high, string commands,string asop, string close, string to)
        {
            XElement xElement = new XElement(
                new XElement("Button",
                    new XElement("Id", id),
                    new XElement("Name", name),
                    new XElement("Url1", url1),
                    new XElement("Url2", url2),
                    new XElement("X", x),
                    new XElement("Y", y),
                    new XElement("Width", width),
                    new XElement("High", high),
                    new XElement("Commands",commands),
                    new XElement("Asop", asop),
                    new XElement("Close", close),
                    new XElement("To", to)
               )
          );
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Encoding = new UTF8Encoding(false);
            settings.Indent = true;
            XmlWriter xw = XmlWriter.Create(xmlpath, settings);
            xElement.Save(xw);
            xw.Flush();
            xw.Close();
        }

        public static void CreateTxtTree(string xmlpath, string x, string y, string scale, string texts)
        {
            XElement xElement = new XElement(
                 new XElement("Text",
                     new XElement("X",x),
                     new XElement("Y",y),
                     new XElement("Scale",scale),
                     new XElement("Texts",texts)
                )
           ); 
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Encoding = new UTF8Encoding(false);
            settings.Indent = true;
            XmlWriter xw = XmlWriter.Create(xmlpath, settings);
            xElement.Save(xw);
            xw.Flush();
            xw.Close();
        }
        #endregion
        #region 修改XML树文件
        public static void ModifyGui(string xmlpath)
        {
            XmlDocument XmlDocument = new XmlDocument();
            XmlDocument.Load(xmlpath);
            XmlNode GuiNode = XmlDocument.SelectSingleNode("Gui");
            XmlNodeList GuiNodeList = GuiNode.ChildNodes;
            GuiNodeList.Item(0).InnerText = FGuiFrame.gui.GuiUrl; //gui
            GuiNodeList.Item(1).InnerText = "-1"; //x
            GuiNodeList.Item(2).InnerText = "-1"; //y
            GuiNodeList.Item(3).InnerText = FGuiFrame.gui.Width; //width
            GuiNodeList.Item(4).InnerText = FGuiFrame.gui.High; //high
            GuiNodeList.Item(5).InnerText = FGuiFrame.gui.XShow; //xshow
            GuiNodeList.Item(6).InnerText = FGuiFrame.gui.YShow; //yshow
            //使用XmlTextWriter进行保存
            XmlTextWriter writer = new XmlTextWriter(xmlpath, Encoding.UTF8);
            writer.Formatting = Formatting.Indented;
            XmlDocument.Save(writer);
            writer.Close();
        }
        public static void ModifyIm(string xmlpath)
        {
            XmlDocument XmlDocument = new XmlDocument();
            XmlDocument.Load(xmlpath);
            XmlNode ImNode = XmlDocument.SelectSingleNode("Image");
            XmlNodeList ImNodeList = ImNode.ChildNodes;
            ImNodeList.Item(0).InnerText = SGuiFrame.Im.ImageUrl; //image
            ImNodeList.Item(1).InnerText = SGuiFrame.Im.X; //x
            ImNodeList.Item(2).InnerText = SGuiFrame.Im.Y; //y
            ImNodeList.Item(3).InnerText = SGuiFrame.Im.Width; //width
            ImNodeList.Item(4).InnerText = SGuiFrame.Im.High; //high
            ImNodeList.Item(5).InnerText = SGuiFrame.Im.XShow; //xshow
            ImNodeList.Item(6).InnerText = SGuiFrame.Im.YShow; //yshow
            ImNodeList.Item(7).InnerText = SGuiFrame.Im.HoverText; //hovertext
            XmlDocument.Save(xmlpath);
            XmlTextWriter writer = new XmlTextWriter(xmlpath, Encoding.UTF8);
            writer.Formatting = Formatting.Indented;
            XmlDocument.Save(writer);
            writer.Close();
        }
        public static void ModifyBt(string xmlpath)
        {
            XmlDocument XmlDocument = new XmlDocument();
            XmlDocument.Load(xmlpath);
            XmlNode BtNode = XmlDocument.SelectSingleNode("Button");
            XmlNodeList BtNodeList = BtNode.ChildNodes;
            BtNodeList.Item(0).InnerText = SGuiFrame.Bt.Id; //id
            BtNodeList.Item(1).InnerText = SGuiFrame.Bt.Name; //name
            BtNodeList.Item(2).InnerText = SGuiFrame.Bt.Url1; //url1
            BtNodeList.Item(3).InnerText = SGuiFrame.Bt.Url2; //url2
            BtNodeList.Item(4).InnerText = SGuiFrame.Bt.X; //x
            BtNodeList.Item(5).InnerText = SGuiFrame.Bt.Y; //y
            BtNodeList.Item(6).InnerText = SGuiFrame.Bt.Width; //width
            BtNodeList.Item(7).InnerText = SGuiFrame.Bt.High; //high
            BtNodeList.Item(8).InnerText = SGuiFrame.Bt.Commands; //commands
            BtNodeList.Item(9).InnerText = Convert.ToString(SGuiFrame.Bt.Asop); //asop
            BtNodeList.Item(10).InnerText = Convert.ToString(SGuiFrame.Bt.Close); //close
            BtNodeList.Item(11).InnerText = SGuiFrame.Bt.To; //to
            XmlDocument.Save(xmlpath);
            XmlTextWriter writer = new XmlTextWriter(xmlpath, Encoding.UTF8);
            writer.Formatting = Formatting.Indented;
            XmlDocument.Save(writer);
            writer.Close();
        }
        public static void ModifyTxt(string xmlpath)
        {
            XmlDocument XmlDocument = new XmlDocument();
            XmlDocument.Load(xmlpath);
            XmlNode TxtNode = XmlDocument.SelectSingleNode("Text");
            XmlNodeList TxtNodeList = TxtNode.ChildNodes;
            TxtNodeList.Item(0).InnerText = SGuiFrame.Txt.X;
            TxtNodeList.Item(1).InnerText = SGuiFrame.Txt.Y;
            TxtNodeList.Item(2).InnerText = SGuiFrame.Txt.Scale;
            TxtNodeList.Item(3).InnerText = SGuiFrame.Txt.Texts;
            //
            XmlTextWriter writer = new XmlTextWriter(xmlpath, Encoding.UTF8);
            writer.Formatting = Formatting.Indented;
            XmlDocument.Save(writer);
            writer.Close();
            //XmlDocument.Save(xmlpath);
        }
        #endregion
        #region 读取XML树文件
        public static void ReadGui(string xmlpath)
        {
            //GuiProperties GuiPro = new GuiProperties();
            XmlDocument XmlDocument = new XmlDocument();
            XmlDocument.Load(xmlpath);
            XmlNode GuiNode = XmlDocument.SelectSingleNode("Gui");
            XmlNodeList GuiNodeList = GuiNode.ChildNodes;
            FGuiFrame.gui.GuiUrl = GuiNodeList.Item(0).InnerText; //guiurl
            FGuiFrame.gui.X = "-1"; //x
            FGuiFrame.gui.Y = "-1"; //y
            FGuiFrame.gui.Width = GuiNodeList.Item(3).InnerText; //width
            FGuiFrame.gui.High = GuiNodeList.Item(4).InnerText; //high
            FGuiFrame.gui.XShow = GuiNodeList.Item(5).InnerText; //xshow
            FGuiFrame.gui.YShow = GuiNodeList.Item(6).InnerText; //yshow
        }
        public static void ReadIm(string xmlpath)
        {
            XmlDocument XmlDocument = new XmlDocument();
            XmlDocument.Load(xmlpath);
            XmlNode ImNode = XmlDocument.SelectSingleNode("Image");
            XmlNodeList ImNodeList = ImNode.ChildNodes;
            SGuiFrame.Im.ImageUrl = ImNodeList.Item(0).InnerText; //imageurl
            SGuiFrame.Im.X = ImNodeList.Item(1).InnerText;
            SGuiFrame.Im.Y = ImNodeList.Item(2).InnerText;
            SGuiFrame.Im.Width = ImNodeList.Item(3).InnerText;
            SGuiFrame.Im.High = ImNodeList.Item(4).InnerText;
            SGuiFrame.Im.XShow = ImNodeList.Item(5).InnerText;
            SGuiFrame.Im.YShow = ImNodeList.Item(6).InnerText;
            SGuiFrame.Im.HoverText = ImNodeList.Item(7).InnerText;
        }
        public static void ReadBt(string xmlpath)
        {
            XmlDocument XmlDocument = new XmlDocument();
            XmlDocument.Load(xmlpath);
            XmlNode BtNode = XmlDocument.SelectSingleNode("Button");
            XmlNodeList BtNodeList = BtNode.ChildNodes;
            SGuiFrame.Bt.Id = BtNodeList.Item(0).InnerText;
            SGuiFrame.Bt.Name = BtNodeList.Item(1).InnerText;
            SGuiFrame.Bt.Url1 = BtNodeList.Item(2).InnerText;
            SGuiFrame.Bt.Url2 = BtNodeList.Item(3).InnerText;
            SGuiFrame.Bt.X = BtNodeList.Item(4).InnerText;
            SGuiFrame.Bt.Y = BtNodeList.Item(5).InnerText;
            SGuiFrame.Bt.Width = BtNodeList.Item(6).InnerText;
            SGuiFrame.Bt.High = BtNodeList.Item(7).InnerText;
            SGuiFrame.Bt.Commands = BtNodeList.Item(8).InnerText;
            SGuiFrame.Bt.Asop = Convert.ToBoolean(BtNodeList.Item(9).InnerText);
            SGuiFrame.Bt.Close = Convert.ToBoolean(BtNodeList.Item(10).InnerText);
            SGuiFrame.Bt.To = BtNodeList.Item(11).InnerText;
        }
        public static void ReadTxt(string xmlpath)
        {
            XmlDocument XmlDocument = new XmlDocument();
            XmlDocument.Load(xmlpath);
            XmlNode TxtNode = XmlDocument.SelectSingleNode("Text");
            XmlNodeList TxtNodeList = TxtNode.ChildNodes;
            SGuiFrame.Txt.X = TxtNodeList.Item(0).InnerText;
            SGuiFrame.Txt.Y = TxtNodeList.Item(1).InnerText;
            SGuiFrame.Txt.Scale = TxtNodeList.Item(2).InnerText;
            SGuiFrame.Txt.Texts = TxtNodeList.Item(3).InnerText;
        }
        #endregion
    }
}
