using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VexGuiEdit
{
    public class ImProperties
    {
        private string _ImageUrl;
        private string _X;
        private string _Y;
        private string _Width;
        private string _High;
        private string _XShow;
        private string _YShow;
        private string _HoverText;
        [CategoryAttribute("数据"), DescriptionAttribute("图像路径"), ReadOnlyAttribute(false)]
        public string ImageUrl
        {
            get
            {
                return _ImageUrl;
            }
            set
            {
                _ImageUrl = value;
            }
        }
        [CategoryAttribute("坐标"), DescriptionAttribute("图像X坐标"), ReadOnlyAttribute(false)]
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
        [CategoryAttribute("坐标"), DescriptionAttribute("图像Y坐标"), ReadOnlyAttribute(false)]
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
        [CategoryAttribute("实际大小"), DescriptionAttribute("图像实际宽度"), ReadOnlyAttribute(false)]
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
        [CategoryAttribute("实际大小"), DescriptionAttribute("图像实际高度"), ReadOnlyAttribute(false)]
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
        [CategoryAttribute("显示大小"), DescriptionAttribute("图像显示宽度"), ReadOnlyAttribute(false)]
        public string XShow
        {
            get
            {
                return _XShow;
            }
            set
            {
                _XShow = value;
            }
        }
        [CategoryAttribute("显示大小"), DescriptionAttribute("图像显示高度"), ReadOnlyAttribute(false)]
        public string YShow
        {
            get
            {
                return _YShow;
            }
            set
            {
                _YShow = value;
            }
        }
        [CategoryAttribute("附加"), DescriptionAttribute("悬浮字显示，请填写text目录的文件名"), ReadOnlyAttribute(false)]
        public string HoverText
        {
            get
            {
                return _HoverText;
            }
            set
            {
                _HoverText = value;
            }
        }
    }
}
