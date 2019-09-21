using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using YamlDotNet.Serialization;

namespace VexGuiEdit
{
    public class GenerateYML
    {
        public static void GenerateGui(string xmlpath, string[] images, string[] texts, string[] buttons, string filename)
        {
            XmlDocument XmlGui = new XmlDocument();
            XmlGui.Load(xmlpath);
            XmlNode GuiNode = XmlGui.SelectSingleNode("Gui");
            XmlNodeList GuiNodeList = GuiNode.ChildNodes;
            GuiYml gui = new GuiYml();
            gui.gui = GuiNodeList.Item(0).InnerText;
            gui.x = double.Parse(GuiNodeList.Item(1).InnerText);
            gui.y = double.Parse(GuiNodeList.Item(2).InnerText);
            gui.width = double.Parse(GuiNodeList.Item(3).InnerText) / 2;
            gui.high = double.Parse(GuiNodeList.Item(4).InnerText) / 2;
            gui.xshow = double.Parse(GuiNodeList.Item(5).InnerText) / 2;
            gui.yshow = double.Parse(GuiNodeList.Item(6).InnerText) / 2;
            gui.image = new List<string>();
            for (int i = 0; i < images.Length; i++)
            {
                gui.image.Add(images[i]);
            }
            gui.text = new List<string>();
            for (int i = 0; i < texts.Length; i++)
            {
                gui.text.Add(texts[i]);
            }
            gui.buttons = new List<string>();
            for (int i = 0; i < buttons.Length; i++)
            {
                gui.buttons.Add(buttons[i]);
            }
            gui.slot = "";
            Serializer serializer = new Serializer();
            StringWriter strwriter = new StringWriter();
            serializer.Serialize(strwriter, gui);
            using (TextWriter writer = File.CreateText(Main.Vexview + "\\VexView\\gui\\" + filename + ".yml"))  
            {
                writer.Write(strwriter.ToString());
            }
        }
        public static void GenerateImage(string xmlpath, string filename)
        {
            XmlDocument XmlImage = new XmlDocument();
            XmlImage.Load(xmlpath);
            XmlNode ImageNode = XmlImage.SelectSingleNode("Image");
            XmlNodeList ImageNodeList = ImageNode.ChildNodes;
            ImageYml image = new ImageYml();
            image.image = ImageNodeList.Item(0).InnerText;
            image.x = double.Parse(ImageNodeList.Item(1).InnerText) / 2;
            image.y = double.Parse(ImageNodeList.Item(2).InnerText) / 2;
            image.width = double.Parse(ImageNodeList.Item(3).InnerText) / 2;
            image.high = double.Parse(ImageNodeList.Item(4).InnerText) / 2;
            image.xshow = double.Parse(ImageNodeList.Item(5).InnerText) / 2;
            image.yshow = double.Parse(ImageNodeList.Item(6).InnerText) / 2;
            image.hovertext = ImageNodeList.Item(7).InnerText;
            Serializer serializer = new Serializer();
            StringWriter strwriter = new StringWriter();
            serializer.Serialize(strwriter, image);
            using (TextWriter writer = File.CreateText(Main.Vexview + "\\VexView\\image\\" + filename + ".yml")) 
            {
                writer.Write(strwriter.ToString());
            }
            
        }
        public static void GenerateButton(string xmlpath,string filename)
        {
            XmlDocument XmlButton = new XmlDocument();
            XmlButton.Load(xmlpath);
            XmlNode ButtonNode = XmlButton.SelectSingleNode("Button");
            XmlNodeList ButtonNodeList = ButtonNode.ChildNodes;
            ButtonYml button = new ButtonYml();
            button.id = int.Parse(ButtonNodeList.Item(0).InnerText);
            button.name = ButtonNodeList.Item(1).InnerText;
            button.url = ButtonNodeList.Item(2).InnerText;
            button.url2 = ButtonNodeList.Item(3).InnerText;
            button.x = double.Parse(ButtonNodeList.Item(4).InnerText) / 2;
            button.y = double.Parse(ButtonNodeList.Item(5).InnerText) / 2;
            button.width = double.Parse(ButtonNodeList.Item(6).InnerText) / 2;
            button.high = double.Parse(ButtonNodeList.Item(7).InnerText) / 2;
            button.commands = new List<string>();
            string[] commands = ButtonNodeList.Item(8).InnerText.Split(new char[] { '|' },StringSplitOptions.RemoveEmptyEntries);
            for (int i =0;i<commands.Length;i++)
            {
                button.commands.Add(commands[i]);
            }
            button.asop = Boolean.Parse(ButtonNodeList.Item(9).InnerText);
            button.close = Boolean.Parse(ButtonNodeList.Item(10).InnerText);
            button.to = ButtonNodeList.Item(11).InnerText;
            Serializer serializer = new Serializer();
            StringWriter strwriter = new StringWriter();
            serializer.Serialize(strwriter, button);
            using (TextWriter writer = File.CreateText(Main.Vexview + "\\VexView\\button\\" + filename + ".yml")) 
            {
                writer.Write(strwriter.ToString());
            }
        }
        public static void GenerateText(string xmlpath, string filename)
        {
            XmlDocument XmlText = new XmlDocument();
            XmlText.Load(xmlpath);
            XmlNode TextNode = XmlText.SelectSingleNode("Text");
            XmlNodeList TextNodeList = TextNode.ChildNodes;
            TextYml text = new TextYml();
            text.x = double.Parse(TextNodeList.Item(0).InnerText) / 2;
            text.y = double.Parse(TextNodeList.Item(1).InnerText) / 2;
            //text.scale = Convert.ToDouble(TextNodeList.Item(2).InnerText); //文件里没有这个键
            text.text = new List<string>();
            string[] texts = TextNodeList.Item(3).InnerText.Split(new char[] { '|' },StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < texts.Length; i++)
            {
                text.text.Add(texts[i]);
            }
            Serializer serializer = new Serializer();
            StringWriter strwriter = new StringWriter();
            serializer.Serialize(strwriter, text);
            using (TextWriter writer = File.CreateText(Main.Vexview + "\\VexView\\text\\" + filename + ".yml")) 
            {
                writer.Write(strwriter.ToString());
            }
        }
    }
    public class GuiYml
    {
        public string gui { get; set; }
        public double x { get; set; }
        public double y { get; set; }
        public double width { get; set; }
        public double high { get; set; }
        public double xshow { get; set; }
        public double yshow { get; set; }
        public List<string> image { get; set; }
        public List<string> text { get; set; }
        public List<string> buttons { get; set; }
        public string slot { get; set; }
    }
    public class ImageYml
    {
        public string image { get; set; }
        public double x { get; set; }
        public double y { get; set; }
        public double width { get; set; }
        public double high { get; set; }
        public double xshow { get; set; }
        public double yshow { get; set; }
        public string hovertext { get; set; }
    }
    public class ButtonYml
    {
        public int id { get; set; }
        public string name { get; set; }
        public string url { get; set; }
        public string url2 { get; set; }
        public double x { get; set; }
        public double y { get; set; }
        public double width { get; set; }
        public double high { get; set; }
        public List<string> commands { get; set; }
        public bool asop { get; set; }
        public bool close { get; set; }
        public string to { get; set; }

    }
    public class TextYml
    {
        public double x { get; set; }
        public double y { get; set; }
        public double scale { get; set; }
        public List<string> text { get; set; }
    }
}
