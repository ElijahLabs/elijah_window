namespace OpenBible.UI
{
    partial class FormScreen
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormScreen));
            this.buttonOldTestament = new System.Windows.Forms.Button();
            this.buttonNewTestament = new System.Windows.Forms.Button();
            this.panelPinyin = new System.Windows.Forms.Panel();
            this.listBoxPinyin = new System.Windows.Forms.ListBox();
            this.labelPinyin = new System.Windows.Forms.Label();
            this.contextMenuStripMouseRightClick = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ToolStripMenuItemFullScreen = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.ToolStripMenuItemPreviousLection = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemNextLection = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.ToolStripMenuItemCopyLectionToClipBoard = new System.Windows.Forms.ToolStripMenuItem();
            this.panelPinyin.SuspendLayout();
            this.contextMenuStripMouseRightClick.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonOldTestament
            // 
            this.buttonOldTestament.AutoSize = true;
            this.buttonOldTestament.BackColor = System.Drawing.Color.White;
            this.buttonOldTestament.Location = new System.Drawing.Point(202, 225);
            this.buttonOldTestament.Name = "buttonOldTestament";
            this.buttonOldTestament.Size = new System.Drawing.Size(39, 22);
            this.buttonOldTestament.TabIndex = 4;
            this.buttonOldTestament.TabStop = false;
            this.buttonOldTestament.Text = "旧约";
            this.buttonOldTestament.UseVisualStyleBackColor = false;
            this.buttonOldTestament.Click += new System.EventHandler(this.buttonOldTestament_Click);
            // 
            // buttonNewTestament
            // 
            this.buttonNewTestament.AutoSize = true;
            this.buttonNewTestament.BackColor = System.Drawing.Color.White;
            this.buttonNewTestament.Location = new System.Drawing.Point(252, 225);
            this.buttonNewTestament.Name = "buttonNewTestament";
            this.buttonNewTestament.Size = new System.Drawing.Size(39, 22);
            this.buttonNewTestament.TabIndex = 3;
            this.buttonNewTestament.TabStop = false;
            this.buttonNewTestament.Text = "新约";
            this.buttonNewTestament.UseVisualStyleBackColor = false;
            this.buttonNewTestament.Click += new System.EventHandler(this.buttonNewTestament_Click);
            // 
            // panelPinyin
            // 
            this.panelPinyin.Controls.Add(this.listBoxPinyin);
            this.panelPinyin.Controls.Add(this.labelPinyin);
            this.panelPinyin.Location = new System.Drawing.Point(137, 167);
            this.panelPinyin.Name = "panelPinyin";
            this.panelPinyin.Size = new System.Drawing.Size(200, 40);
            this.panelPinyin.TabIndex = 5;
            // 
            // listBoxPinyin
            // 
            this.listBoxPinyin.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBoxPinyin.FormattingEnabled = true;
            this.listBoxPinyin.ItemHeight = 12;
            this.listBoxPinyin.Location = new System.Drawing.Point(0, 20);
            this.listBoxPinyin.Name = "listBoxPinyin";
            this.listBoxPinyin.Size = new System.Drawing.Size(200, 20);
            this.listBoxPinyin.TabIndex = 1;
            this.listBoxPinyin.TabStop = false;
            this.listBoxPinyin.SelectedIndexChanged += new System.EventHandler(this.listBoxPinyin_SelectedIndexChanged);
            // 
            // labelPinyin
            // 
            this.labelPinyin.BackColor = System.Drawing.SystemColors.Window;
            this.labelPinyin.Dock = System.Windows.Forms.DockStyle.Top;
            this.labelPinyin.Location = new System.Drawing.Point(0, 0);
            this.labelPinyin.Name = "labelPinyin";
            this.labelPinyin.Padding = new System.Windows.Forms.Padding(3);
            this.labelPinyin.Size = new System.Drawing.Size(200, 20);
            this.labelPinyin.TabIndex = 0;
            // 
            // contextMenuStripMouseRightClick
            // 
            this.contextMenuStripMouseRightClick.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItemFullScreen,
            this.toolStripSeparator1,
            this.ToolStripMenuItemPreviousLection,
            this.ToolStripMenuItemNextLection,
            this.toolStripSeparator2,
            this.ToolStripMenuItemCopyLectionToClipBoard});
            this.contextMenuStripMouseRightClick.Name = "contextMenuStripMouseRightClick";
            this.contextMenuStripMouseRightClick.Size = new System.Drawing.Size(203, 104);
            // 
            // ToolStripMenuItemFullScreen
            // 
            this.ToolStripMenuItemFullScreen.Name = "ToolStripMenuItemFullScreen";
            this.ToolStripMenuItemFullScreen.Size = new System.Drawing.Size(202, 22);
            this.ToolStripMenuItemFullScreen.Text = "全屏 (F5)";
            this.ToolStripMenuItemFullScreen.Click += new System.EventHandler(this.ToolStripMenuItemFullScreen_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(199, 6);
            // 
            // ToolStripMenuItemPreviousLection
            // 
            this.ToolStripMenuItemPreviousLection.Name = "ToolStripMenuItemPreviousLection";
            this.ToolStripMenuItemPreviousLection.Size = new System.Drawing.Size(202, 22);
            this.ToolStripMenuItemPreviousLection.Text = "上处经文 (Page Up)";
            this.ToolStripMenuItemPreviousLection.Click += new System.EventHandler(this.ToolStripMenuItemPreviousLection_Click);
            // 
            // ToolStripMenuItemNextLection
            // 
            this.ToolStripMenuItemNextLection.Name = "ToolStripMenuItemNextLection";
            this.ToolStripMenuItemNextLection.Size = new System.Drawing.Size(202, 22);
            this.ToolStripMenuItemNextLection.Text = "下处经文 (Page Down)";
            this.ToolStripMenuItemNextLection.Click += new System.EventHandler(this.ToolStripMenuItemNextLection_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(199, 6);
            // 
            // ToolStripMenuItemCopyLectionToClipBoard
            // 
            this.ToolStripMenuItemCopyLectionToClipBoard.Name = "ToolStripMenuItemCopyLectionToClipBoard";
            this.ToolStripMenuItemCopyLectionToClipBoard.Size = new System.Drawing.Size(202, 22);
            this.ToolStripMenuItemCopyLectionToClipBoard.Text = "拷贝到剪切板";
            this.ToolStripMenuItemCopyLectionToClipBoard.Click += new System.EventHandler(this.ToolStripMenuItemCopyLectionToClipBoard_Click);
            // 
            // FormScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(492, 473);
            this.ControlBox = false;
            this.Controls.Add(this.panelPinyin);
            this.Controls.Add(this.buttonOldTestament);
            this.Controls.Add(this.buttonNewTestament);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormScreen";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "圣经投影窗口 - 沈阳以利亚科技有限公司 - www.Elijah.com.cn";
            this.Load += new System.EventHandler(this.FormScreen_Load);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.FormScreen_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.FormScreen_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.FormScreen_MouseUp);
            this.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.FormScreen_PreviewKeyDown);
            this.Resize += new System.EventHandler(this.FormScreen_Resize);
            this.panelPinyin.ResumeLayout(false);
            this.contextMenuStripMouseRightClick.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonOldTestament;
        private System.Windows.Forms.Button buttonNewTestament;
        private System.Windows.Forms.Panel panelPinyin;
        private System.Windows.Forms.ListBox listBoxPinyin;
        private System.Windows.Forms.Label labelPinyin;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripMouseRightClick;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemFullScreen;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemPreviousLection;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemNextLection;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemCopyLectionToClipBoard;
    }
}