using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace VexGuiEdit
{
    public class GuiProperties
    {
        #region pro
        private string _GuiUrl;
        private string _X;
        private string _Y;
        private string _Width;
        private string _High;
        private string _XShow;
        private string _YShow;
        #endregion

        #region Properties
        //设置默认值，特性DefaultValueAttribute("xxxx") Or 给字段赋值
        //Path
        [CategoryAttribute("路径"), DescriptionAttribute("GUI底图路径"), ReadOnlyAttribute(false)]
        public string GuiUrl
        {
            get
            {
                return _GuiUrl;
            }
            set
            {
                _GuiUrl = value;
            }
        }
        //X
        [CategoryAttribute("坐标"), DescriptionAttribute("GUI底图X坐标"), ReadOnlyAttribute(true)]
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
        //Y
        [CategoryAttribute("坐标"), DescriptionAttribute("GUI底图Y坐标"), ReadOnlyAttribute(true)]
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
        //Width
        [CategoryAttribute("实际大小"), DescriptionAttribute("GUI底图实际宽度"), ReadOnlyAttribute(false)]
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
        //Height
        [CategoryAttribute("实际大小"), DescriptionAttribute("GUI底图实际高度"), ReadOnlyAttribute(false)]
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
        [CategoryAttribute("显示大小"), DescriptionAttribute("GUI底图显示宽度"), ReadOnlyAttribute(false)]
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
        [CategoryAttribute("显示大小"), DescriptionAttribute("GUI底图实际高度"), ReadOnlyAttribute(false)]
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
        #endregion 
    }
}
