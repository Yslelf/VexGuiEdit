using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VexGuiEdit
{
    public class BtProperties
    {
        private string _Id;
        private string _Name;
        private string _Url1;
        private string _Url2;
        private string _X;
        private string _Y;
        private string _Width;
        private string _High;
        private string _Commands;
        private bool _Asop;
        private bool _Close;
        private string _To;
        [CategoryAttribute("数据"), DescriptionAttribute("按钮编号"), ReadOnlyAttribute(false)]
        public string Id
        {
            get
            {
                return _Id;
            }
            set
            {
                _Id = value;
            }
        }
        [CategoryAttribute("数据"), DescriptionAttribute("按钮名字"), ReadOnlyAttribute(false)]
        public string Name
        {
            get
            {
                return _Name;
            }
            set
            {
                _Name = value;
            }
        }
        [CategoryAttribute("路径"), DescriptionAttribute("按钮路径1"), ReadOnlyAttribute(false)]
        public string Url1
        {
            get
            {
                return _Url1;
            }
            set
            {
                _Url1 = value;
            }
        }
        [CategoryAttribute("路径"), DescriptionAttribute("按钮路径2"), ReadOnlyAttribute(false)]
        public string Url2
        {
            get
            {
                return _Url2;
            }
            set
            {
                _Url2 = value;
            }
        }
        [CategoryAttribute("坐标"), DescriptionAttribute("按钮X坐标"), ReadOnlyAttribute(false)]
        public string X
        {
            get
            {
                return _X;
            }
            set
            {
                _X = value;
            }
        }
        [CategoryAttribute("坐标"), DescriptionAttribute("按钮Y坐标"), ReadOnlyAttribute(false)]
        public string Y
        {
            get
            {
                return _Y;
            }
            set
            {
                _Y = value;
            }
        }
        [CategoryAttribute("大小"), DescriptionAttribute("按钮宽度"), ReadOnlyAttribute(false)]
        public string Width
        {
            get
            {
                return _Width;
            }
            set
            {
                _Width = value;
            }
        }
        [CategoryAttribute("大小"), DescriptionAttribute("按钮高度"), ReadOnlyAttribute(false)]
        public string High
        {
            get
            {
                return _High;
            }
            set
            {
                _High = value;
            }
        }
        [CategoryAttribute("附加"), DescriptionAttribute("按下按钮后执行的命令，可以多个，使用|分割"), ReadOnlyAttribute(false)]
        public string Commands
        {
            get
            {
                return _Commands;
            }
            set
            {
                _Commands = value;
            }
        }
        [CategoryAttribute("附加"), DescriptionAttribute("是否绕过执行指令的权限执行"), ReadOnlyAttribute(false)]
        public bool Asop
        {
            get
            {
                return _Asop;
            }
            set
            {
                _Asop = value;
            }
        }
        [CategoryAttribute("附加"), DescriptionAttribute("按下按钮后是否关闭GUI"), ReadOnlyAttribute(false)]
        public bool Close
        {
            get
            {
                return _Close;
            }
            set
            {
                _Close = value;
            }
        }
        [CategoryAttribute("附加"), DescriptionAttribute("按下按钮后转入另一个GUI界面(设置为 - 则不启用)"), ReadOnlyAttribute(false)]
        public string To
        {
            get
            {
                return _To;
            }
            set
            {
                _To = value;
            }
        }
    }
}
