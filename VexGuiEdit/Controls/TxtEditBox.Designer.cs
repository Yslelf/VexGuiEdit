namespace VexGuiEdit
{
    partial class TxtEditBox
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.LineP = new System.Windows.Forms.Panel();
            this.GuiCodeTxt = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // LineP
            // 
            this.LineP.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(228)))), ((int)(((byte)(228)))), ((int)(((byte)(228)))));
            this.LineP.Location = new System.Drawing.Point(0, 0);
            this.LineP.Name = "LineP";
            this.LineP.Size = new System.Drawing.Size(41, 1180);
            this.LineP.TabIndex = 1;
            this.LineP.Paint += new System.Windows.Forms.PaintEventHandler(this.LineP_Paint);
            // 
            // GuiCodeTxt
            // 
            this.GuiCodeTxt.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GuiCodeTxt.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.GuiCodeTxt.Font = new System.Drawing.Font("楷体", 12F);
            this.GuiCodeTxt.Location = new System.Drawing.Point(41, 0);
            this.GuiCodeTxt.Name = "GuiCodeTxt";
            this.GuiCodeTxt.Size = new System.Drawing.Size(1876, 1177);
            this.GuiCodeTxt.TabIndex = 2;
            this.GuiCodeTxt.Text = "";
            this.GuiCodeTxt.VScroll += new System.EventHandler(this.GuiCodeTxt_VScroll);
            this.GuiCodeTxt.TextChanged += new System.EventHandler(this.GuiCodeTxt_TextChanged);
            // 
            // TxtEditBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.GuiCodeTxt);
            this.Controls.Add(this.LineP);
            this.Name = "TxtEditBox";
            this.Size = new System.Drawing.Size(1920, 1180);
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.Panel LineP;
        public System.Windows.Forms.RichTextBox GuiCodeTxt;
    }
}
