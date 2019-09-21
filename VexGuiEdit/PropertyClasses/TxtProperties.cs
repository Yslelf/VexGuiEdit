using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VexGuiEdit
{
    public class TxtProperties
    {
        private string _X;
        private string _Y;
        private string _Scale;
        private string _Texts;
        [CategoryAttribute("坐标"), DescriptionAttribute("文本X坐标"), ReadOnlyAttribute(false)]
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
        [CategoryAttribute("坐标"), DescriptionAttribute("文本Y坐标"), ReadOnlyAttribute(false)]
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
        [CategoryAttribute("附加"), DescriptionAttribute("文本倍数"), ReadOnlyAttribute(false)]
        public string Scale
        {
            get
            {
                return _Scale;
            }
            set
            {
                _Scale = value;
            }
        }
        [CategoryAttribute("附加"), DescriptionAttribute("显示文本"), ReadOnlyAttribute(false)]
        public string Texts
        {
            get
            {
                return _Texts;
            }
            set
            {
                _Texts = value;
            }
        }
    }
}
