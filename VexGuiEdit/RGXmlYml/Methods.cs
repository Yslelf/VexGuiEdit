using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using YamlDotNet.Serialization;
using LitJson;
using System.Windows;

namespace VexGuiEdit
{
    class Methods
    {
        #region text
        public string X_T { get; set; }
        public string Y_T { get; set; }
        public string Scale { get; set; }

        #endregion
        #region button
        public string Id { get; set; } //id
        public string Name_B { get; set; } //name
        public string X_B { get; set; } //x
        public string Y_B { get; set; } //y
        public string Width_B { get; set; } //width
        public string High_B { get; set; } //high
        public string Url1_B { get; set; } //url
        public string Url2_B { get; set; } //url2
        public string Asop { get; set; } //asop
        public string Close { get; set; } //close
        public string To { get; set; } //to

        #endregion
        #region image
        public string Url_I { get; set; } //image
        public string X_I { get; set; } //x
        public string Y_I { get; set; } //y
        public string Width_I { get; set; } //width
        public string High_I { get; set; } //high
        public string XShow_I { get; set; } //xhsow
        public string YShow_I { get; set; } //yshow
        public string HoverText { get; set; } //hovertext
        #endregion
        #region GuiYML
        public string Url { get; set; } //gui
        public string X { get; set; } //x
        public string Y { get; set; } //y
        public string Width { get; set; } //width
        public string High { get; set; } //high
        public string XShow { get; set; } //xshow
        public string YShow { get; set; } //yshow
        #endregion
        public static string YamlToJson(string filepath)
        {
            var b = new StringReader(File.ReadAllText(filepath));
            var deserializer = new DeserializerBuilder().Build();
            var yamlObject = deserializer.Deserialize(b);
            //
            var serializer = new SerializerBuilder()
                .JsonCompatible()
                .Build();
            //
            string json = serializer.Serialize(yamlObject);
            return json;
        }
        public void ReadGuiYML(string filepath)
        {
            try
            {
                YML yml = new YML(filepath);
                if (yml.read("gui", "") != null)
                {
                    Url = yml.read("gui", "").Trim('\'');
                }
                else
                {
                    Url = "";
                }
                if (yml.read("x", "") != null)
                {
                    X = yml.read("x", "").Trim('\'');
                }
                else
                {
                    X = "200";
                }
                if (yml.read("y", "") != null)
                {
                    Y = yml.read("y", "").Trim('\'');
                }
                else
                {
                    Y = "200";
                }
                if (yml.read("width", "") != null)
                {
                    Width = yml.read("width", "").Trim('\'');
                }
                else
                {
                    Width = "1024";
                }
                if (yml.read("high", "") != null)
                {
                    High = yml.read("high", "").Trim('\'');
                }
                else
                {
                    High = "1024";
                }
                if (yml.read("xshow", "") != null)
                {
                    XShow = (double.Parse(yml.read("xshow", "").Trim('\'')) * 2).ToString();
                }
                else
                {
                    XShow = "1024";
                }
                if (yml.read("yshow", "") != null)
                {
                    YShow = (double.Parse(yml.read("yshow", "").Trim('\'')) * 2).ToString();
                }
                else
                {
                    YShow = "1024";
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void ReadButton(string filepath)
        {
            try
            {
                YML yml = new YML(filepath);
                if (yml.read("id", "") != null)
                {
                    Id = yml.read("id", "").Trim('\'');
                }
                else
                {
                    Id = "";
                }
                if (yml.read("name", "") != null)
                {
                    Name_B = yml.read("name", "").Trim('\'');
                }
                else
                {
                    Name_B = "";
                }
                if ((yml.read("x", "") != null))
                {
                    X_B = (double.Parse(yml.read("x", "").Trim('\'')) * 2).ToString();
                }
                else
                {
                    X_B = "300";
                }
                if (yml.read("y", "") != null)
                {
                    Y_B = (double.Parse(yml.read("y", "").Trim('\'')) * 2).ToString();
                }
                else
                {
                    Y_B = "300";
                }
                if (yml.read("width", "") != null)
                {
                    Width_B = (double.Parse(yml.read("width", "").Trim('\'')) * 2).ToString();
                }
                else
                {
                    Width_B = "96";
                }
                if (yml.read("high", "") != null)
                {
                    High_B = (double.Parse(yml.read("high", "").Trim('\'')) * 2).ToString();
                }
                else
                {
                    High_B = "96";
                }
                if (yml.read("url", "") != null)
                {
                    Url1_B = yml.read("url", "").Trim('\'');
                }
                else
                {
                    Url1_B = "";
                }
                if (yml.read("url2", "") != null)
                {
                    Url2_B = yml.read("url2", "").Trim('\'');
                }
                else
                {
                    Url2_B = "";
                }
                if (yml.read("asop", "") != null)
                {
                    Asop = yml.read("asop", "").Trim('\'');
                }
                else
                {
                    Asop = "false";
                }
                if (yml.read("close", "") != null)
                {
                    Close = yml.read("close", "").Trim('\'');
                }
                else
                {
                    Close = "false";
                }
                if (yml.read("to", "") != null)
                {
                    To = yml.read("to", "").Trim('\'');
                }
                else
                {
                    To = "-";
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void ReadImage(string filepath)
        {
            try
            {
                YML yml = new YML(filepath);
                if (yml.read("image", "") != null)
                {
                    Url_I = yml.read("image", "").Trim('\'');
                }
                else
                {
                    Url_I = "";
                }
                if (yml.read("x", "") != null)
                {
                    X_I = (double.Parse(yml.read("x", "").Trim('\'')) * 2).ToString();
                }
                else
                {
                    X_I = "350";
                }
                if (yml.read("y", "") != null)
                {
                    Y_I = (double.Parse(yml.read("y", "").Trim('\'')) * 2).ToString();
                }
                else
                {
                    Y_I = "350";
                }
                if (yml.read("width", "") != null)
                {
                    Width_I = yml.read("width", "").Trim('\'');
                }
                else
                {
                    Width_I = "96";
                }
                if (yml.read("high", "") != null)
                {
                    High_I = yml.read("high", "").Trim('\'');
                }
                else
                {
                    High_I = "96";
                }
                if (yml.read("xshow", "") != null)
                {
                    XShow_I = (double.Parse(yml.read("xshow", "").Trim('\'')) * 2).ToString();
                }
                else
                {
                    XShow_I = "96";
                }
                if (yml.read("yshow", "") != null)
                {
                    YShow_I = (double.Parse(yml.read("yshow", "").Trim('\'')) * 2).ToString();
                }
                else
                {
                    YShow_I = "96";
                }
                if (yml.read("hovertext", "") != null)
                {
                    HoverText = yml.read("hovertext", "").Trim('\'');
                }
                else
                {
                    HoverText = "";
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void ReadText(string filepath)
        {
            try
            {
                YML yml = new YML(filepath);
                if (yml.read("x", "") != null)
                {
                    X_T = (double.Parse(yml.read("x", "")) * 2).ToString();
                }
                else
                {
                    X_T = "250";
                }
                if (yml.read("y", "") != null)
                {
                    Y_T = (double.Parse(yml.read("y", "")) * 2).ToString();
                }
                else
                {
                    Y_T = "250";
                }
                if (yml.read("scale", "") != null)
                {
                    Scale = yml.read("scale", "");
                }
                else
                {
                    Scale = "1.0";
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void SuppleMentGui(string filepath)
        {
            YML yml = new YML(filepath);
            using (FileStream fs = new FileStream(filepath, FileMode.Append, FileAccess.Write, FileShare.Write)) 
            {
                using (StreamWriter sw = new StreamWriter(fs))
                {
                    if (!yml.boolnode("gui"))
                    {
                        sw.WriteLine("\r\ngui: 'https://yun.mc5173.com/images/2019/08/14/mc5173.png'");
                    }
                    if (!yml.boolnode("x"))
                    {
                        sw.WriteLine("\r\nx: -1");
                    }
                    if (!yml.boolnode("y"))
                    {
                        sw.WriteLine("\r\ny: -1");
                    }
                    if (!yml.boolnode("width"))
                    {
                        sw.WriteLine("\r\nwidth: 512");
                    }
                    if (!yml.boolnode("high"))
                    {
                        sw.WriteLine("\r\nhigh: 512");
                    }
                    if (!yml.boolnode("xshow"))
                    {
                        sw.WriteLine("\r\nxshow: 512");
                    }
                    if (!yml.boolnode("yshow"))
                    {
                        sw.WriteLine("\r\nyshow: 512");
                    }
                    if (!yml.boolnode("image"))
                    {
                        sw.WriteLine("\r\nimage: ");
                    }
                    if (!yml.boolnode("text"))
                    {
                        sw.WriteLine("\r\ntext: ");
                    }
                    if (!yml.boolnode("buttons"))
                    {
                        sw.WriteLine("\r\nbuttons: ");
                    }
                    if (!yml.boolnode("slot"))
                    {
                        sw.WriteLine("\r\nslot: ");
                    }
                    sw.Close();
                }
                fs.Close();
            }
        }
        public void SuppleMentIm(string filepath)
        {
            YML yml = new YML(filepath);
            using (FileStream fs = new FileStream(filepath, FileMode.Append, FileAccess.Write, FileShare.Write))
            {
                using (StreamWriter sw = new StreamWriter(fs))
                {
                    if (!yml.boolnode("image"))
                    {
                        sw.WriteLine("\r\nimage: 'https://yun.mc5173.com/images/2019/08/14/mc5173.png'");
                    }
                    if (!yml.boolnode("x"))
                    {
                        sw.WriteLine("\r\nx: 350");
                    }
                    if (!yml.boolnode("y"))
                    {
                        sw.WriteLine("\r\ny: 350");
                    }
                    if (!yml.boolnode("width"))
                    {
                        sw.WriteLine("\r\nwidth: 48");
                    }
                    if (!yml.boolnode("high"))
                    {
                        sw.WriteLine("\r\nhigh: 48");
                    }
                    if (!yml.boolnode("xshow"))
                    {
                        sw.WriteLine("\r\nxshow: 48");
                    }
                    if (!yml.boolnode("yshow"))
                    {
                        sw.WriteLine("\r\nyshow: 48");
                    }
                    if (!yml.boolnode("hovertext"))
                    {
                        sw.WriteLine("\r\nhovertext: ''");
                    }
                    sw.Close();
                }
                fs.Close();
            }
        }
        public void SuppleMentBt(string filepath)
        {
            YML yml = new YML(filepath);
            using (FileStream fs = new FileStream(filepath, FileMode.Append, FileAccess.Write, FileShare.Write))
            {
                using (StreamWriter sw = new StreamWriter(fs))
                {
                    if (!yml.boolnode("id"))
                    {
                        sw.WriteLine("\r\nid: ");
                    }
                    if (!yml.boolnode("name"))
                    {
                        sw.WriteLine("\r\nname: ''");
                    }
                    if (!yml.boolnode("url"))
                    {
                        sw.WriteLine("\r\nurl: 'https://yun.mc5173.com/images/2019/08/14/mc5173.png'");
                    }
                    if (!yml.boolnode("url2"))
                    {
                        sw.WriteLine("\r\nurl2: 'https://yun.mc5173.com/images/2019/08/14/mc5173.png'");
                    }
                    if (!yml.boolnode("x"))
                    {
                        sw.WriteLine("\r\nx: 350");
                    }
                    if (!yml.boolnode("y"))
                    {
                        sw.WriteLine("\r\ny: 350");
                    }
                    if (!yml.boolnode("width"))
                    {
                        sw.WriteLine("\r\nyshow: 48");
                    }
                    if (!yml.boolnode("high"))
                    {
                        sw.WriteLine("\r\nhigh: 48");
                    }
                    if (!yml.boolnode("commands"))
                    {
                        sw.WriteLine("\r\ncommands: ");
                    }
                    if (!yml.boolnode("asop"))
                    {
                        sw.WriteLine("\r\nasop: false");
                    }
                    if (!yml.boolnode("close"))
                    {
                        sw.WriteLine("\r\nclose: false");
                    }
                    if (!yml.boolnode("to"))
                    {
                        sw.WriteLine("\r\nto: '-'");
                    }
                    sw.Close();
                }
                fs.Close();
            }
        }
        public void SuppleMentTxt(string filepath)
        {
            YML yml = new YML(filepath);
            using (FileStream fs = new FileStream(filepath, FileMode.Append, FileAccess.Write, FileShare.Write))
            {
                using (StreamWriter sw = new StreamWriter(fs))
                {
                    if (!yml.boolnode("x"))
                    {
                        sw.WriteLine("\r\nx: 350");
                    }
                    if (!yml.boolnode("y"))
                    {
                        sw.WriteLine("\r\ny: 350");
                    }
                    if (!yml.boolnode("scale"))
                    {
                        sw.WriteLine("\r\nscale: 1.0");
                    }
                    if (!yml.boolnode("text"))
                    {
                        sw.WriteLine("\r\ntext: ");
                    }
                    sw.Close();
                }
                fs.Close();
            }
        }
    }
    class YML
    {
        // 所有行
        private String[] lines;
        // 格式化为节点
        private List<Node> nodeList = new List<Node>();
        // 文件所在地址
        private String path;

        public YML(String path)
        {
            this.path = path;
            this.lines = File.ReadAllLines(path);

            for (int i = 0; i < lines.Length; i++)
            {
                String line = lines[i];
                if (line.Trim() == "")
                {
                    //Console.WriteLine("空白行，行号：" + (i + 1));
                    continue;
                }
                else if (line.Trim().Substring(0, 1) == "#")
                {
                    //Console.WriteLine("注释行，行号：" + (i + 1));
                    continue;
                }

                String[] kv = Regex.Split(line, ":", RegexOptions.IgnoreCase);
                findPreSpace(line);
                Node node = new Node();
                node.space = findPreSpace(line);
                node.name = kv[0].Trim();

                // 去除前后空白符
                String fline = line.Trim();
                int first = fline.IndexOf(':');
                node.value = first == fline.Length - 1 ? null : fline.Substring(first + 2, fline.Length - first - 2);
                node.parent = findParent(node.space);
                nodeList.Add(node);
            }

            this.formatting();
        }

        // 修改值 允许key为多级 例如：spring.datasource.url
        public void modify(String key, String value)
        {
            Node node = findNodeByKey(key);
            if (node != null)
            {
                node.value = value;
            }
        }
        public bool boolnode(string key)
        {
            Node node = findNodeByKey(key);
            if(node!=null)
            {
                return true;
            }
            return false;
        }
        // 读取值
        public String read(String key, String value)
        {
            Node node = findNodeByKey(key);
            if (node != null)
            {
                return node.value;
            }
            return null;
        }

        // 根据key找节点
        private Node findNodeByKey(String key)
        {
            String[] ks = key.Split('.');
            for (int i = 0; i < nodeList.Count; i++)
            {
                Node node = nodeList[i];
                if (node.name == ks[ks.Length - 1])
                {
                    // 判断父级
                    Node tem = node;
                    // 统计匹配到的次数
                    int count = 1;
                    for (int j = ks.Length - 2; j >= 0; j--)
                    {
                        if (tem.parent.name == ks[j])
                        {
                            count++;
                            // 继续检查父级
                            tem = tem.parent;
                        }
                    }

                    if (count == ks.Length)
                    {
                        return node;
                    }
                }
            }
            return null;
        }

        // 保存到文件中
        public void save()
        {
            StreamWriter stream = File.CreateText(this.path);
            for (int i = 0; i < nodeList.Count; i++)
            {
                Node node = nodeList[i];
                StringBuilder sb = new StringBuilder();
                // 放入前置空格
                for (int j = 0; j < node.tier; j++)
                {
                    sb.Append("  ");
                }
                sb.Append(node.name);
                sb.Append(": ");
                if (node.value != null)
                {
                    sb.Append(node.value);
                }
                stream.WriteLine(sb.ToString());
            }
            stream.Flush();
            stream.Close();
        }

        // 格式化
        public void formatting()
        {
            // 先找出根节点
            List<Node> parentNode = new List<Node>();
            for (int i = 0; i < nodeList.Count; i++)
            {
                Node node = nodeList[i];
                if (node.parent == null)
                {
                    parentNode.Add(node);
                }
            }

            List<Node> fNodeList = new List<Node>();
            // 遍历根节点
            for (int i = 0; i < parentNode.Count; i++)
            {
                Node node = parentNode[i];
                fNodeList.Add(node);
                findChildren(node, fNodeList);
            }

            Console.WriteLine("完成");

            // 指针指向格式化后的
            nodeList = fNodeList;
        }


        // 层级
        int tier = 0;
        // 查找孩子并进行分层
        private void findChildren(Node node, List<Node> fNodeList)
        {
            // 当前层 默认第一级，根在外层进行操作
            tier++;

            for (int i = 0; i < nodeList.Count; i++)
            {
                Node item = nodeList[i];
                if (item.parent == node)
                {
                    item.tier = tier;
                    fNodeList.Add(item);
                    findChildren(item, fNodeList);
                }
            }

            // 走出一层
            tier--;
        }

        //查找前缀空格数量
        private int findPreSpace(String str)
        {
            List<char> chars = str.ToList();
            int count = 0;
            foreach (char c in chars)
            {
                if (c == ' ')
                {
                    count++;
                }
                else
                {
                    break;
                }
            }
            return count;
        }

        // 根据缩进找上级
        private Node findParent(int space)
        {

            if (nodeList.Count == 0)
            {
                return null;
            }
            else
            {
                // 倒着找上级
                for (int i = nodeList.Count - 1; i >= 0; i--)
                {
                    Node node = nodeList[i];
                    if (node.space < space)
                    {
                        return node;
                    }
                }
                // 如果没有找到 返回null
                return null;
            }
        }

        // 私有节点类
        private class Node
        {
            // 名称
            public String name { get; set; }
            // 值
            public String value { get; set; }
            // 父级
            public Node parent { get; set; }
            // 前缀空格
            public int space { get; set; }
            // 所属层级
            public int tier { get; set; }
        }
    }
}
