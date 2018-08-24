namespace OpenMusic.UI
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
            this.contextMenuStripMouseRightClick = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ToolStripMenuItemFullScreen = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.ToolStripMenuItemPageUp = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemPageDown = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.ToolStripMenuItemPreviousMusic = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemNextMusic = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStripMouseRightClick.SuspendLayout();
            this.SuspendLayout();
            // 
            // contextMenuStripMouseRightClick
            // 
            this.contextMenuStripMouseRightClick.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItemFullScreen,
            this.toolStripSeparator1,
            this.ToolStripMenuItemPageUp,
            this.ToolStripMenuItemPageDown,
            this.toolStripSeparator2,
            this.ToolStripMenuItemPreviousMusic,
            this.ToolStripMenuItemNextMusic});
            this.contextMenuStripMouseRightClick.Name = "contextMenuStripMouseRightClick";
            this.contextMenuStripMouseRightClick.Size = new System.Drawing.Size(185, 126);
            // 
            // ToolStripMenuItemFullScreen
            // 
            this.ToolStripMenuItemFullScreen.Name = "ToolStripMenuItemFullScreen";
            this.ToolStripMenuItemFullScreen.ShortcutKeyDisplayString = "";
            this.ToolStripMenuItemFullScreen.Size = new System.Drawing.Size(184, 22);
            this.ToolStripMenuItemFullScreen.Text = "全屏 (F5)";
            this.ToolStripMenuItemFullScreen.Click += new System.EventHandler(this.ToolStripMenuItemFullScreen_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(181, 6);
            // 
            // ToolStripMenuItemPageUp
            // 
            this.ToolStripMenuItemPageUp.Name = "ToolStripMenuItemPageUp";
            this.ToolStripMenuItemPageUp.Size = new System.Drawing.Size(184, 22);
            this.ToolStripMenuItemPageUp.Text = "向前翻页 (←)";
            this.ToolStripMenuItemPageUp.Click += new System.EventHandler(this.ToolStripMenuItemPageUp_Click);
            // 
            // ToolStripMenuItemPageDown
            // 
            this.ToolStripMenuItemPageDown.Name = "ToolStripMenuItemPageDown";
            this.ToolStripMenuItemPageDown.Size = new System.Drawing.Size(184, 22);
            this.ToolStripMenuItemPageDown.Text = "向后翻页 (→)";
            this.ToolStripMenuItemPageDown.Click += new System.EventHandler(this.ToolStripMenuItemPageDown_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(181, 6);
            // 
            // ToolStripMenuItemPreviousMusic
            // 
            this.ToolStripMenuItemPreviousMusic.Name = "ToolStripMenuItemPreviousMusic";
            this.ToolStripMenuItemPreviousMusic.Size = new System.Drawing.Size(184, 22);
            this.ToolStripMenuItemPreviousMusic.Text = "上首音乐 (PageUp)";
            this.ToolStripMenuItemPreviousMusic.Click += new System.EventHandler(this.ToolStripMenuItemPreviousMusic_Click);
            // 
            // ToolStripMenuItemNextMusic
            // 
            this.ToolStripMenuItemNextMusic.Name = "ToolStripMenuItemNextMusic";
            this.ToolStripMenuItemNextMusic.Size = new System.Drawing.Size(184, 22);
            this.ToolStripMenuItemNextMusic.Text = "下首音乐 (PageDown)";
            this.ToolStripMenuItemNextMusic.Click += new System.EventHandler(this.ToolStripMenuItemNextMusic_Click);
            // 
            // FormScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(492, 473);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormScreen";
            this.ShowIcon = false;
            this.Text = "灵歌投影窗口 - 沈阳以利亚科技有限公司 - www.Elijah.com.cn";
            this.Load += new System.EventHandler(this.FormScreen_Load);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.FormScreen_MouseUp);
            this.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.FormScreen_PreviewKeyDown);
            this.Resize += new System.EventHandler(this.FormScreen_Resize);
            this.contextMenuStripMouseRightClick.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip contextMenuStripMouseRightClick;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemFullScreen;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemPageUp;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemPageDown;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemPreviousMusic;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemNextMusic;

    }
}