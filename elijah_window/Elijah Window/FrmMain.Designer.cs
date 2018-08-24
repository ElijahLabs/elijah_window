namespace Elijah_Window
{
    partial class FrmMain
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

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMain));
            this.timerShowHide = new System.Windows.Forms.Timer(this.components);
            this.panTopButton = new System.Windows.Forms.Panel();
            this.picClose = new System.Windows.Forms.PictureBox();
            this.picLogo = new System.Windows.Forms.PictureBox();
            this.panButtons = new System.Windows.Forms.Panel();
            this.button5 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.picButtons = new System.Windows.Forms.PictureBox();
            this.panTab = new System.Windows.Forms.Panel();
            this.picLine = new System.Windows.Forms.PictureBox();
            this.picTab2 = new System.Windows.Forms.PictureBox();
            this.picTab1 = new System.Windows.Forms.PictureBox();
            this.picTabMain = new System.Windows.Forms.PictureBox();
            this.panMain = new System.Windows.Forms.Panel();
            this.m_trayIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextSystem = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.MenuItemClose = new System.Windows.Forms.ToolStripMenuItem();
            this.panTopButton.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picClose)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picLogo)).BeginInit();
            this.panButtons.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picButtons)).BeginInit();
            this.panTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picLine)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picTab2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picTab1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picTabMain)).BeginInit();
            this.contextSystem.SuspendLayout();
            this.SuspendLayout();
            // 
            // timerShowHide
            // 
            this.timerShowHide.Tick += new System.EventHandler(this.timerShowHide_Tick);
            // 
            // panTopButton
            // 
            this.panTopButton.Controls.Add(this.picClose);
            this.panTopButton.Controls.Add(this.picLogo);
            this.panTopButton.Dock = System.Windows.Forms.DockStyle.Top;
            this.panTopButton.Location = new System.Drawing.Point(0, 0);
            this.panTopButton.Name = "panTopButton";
            this.panTopButton.Size = new System.Drawing.Size(300, 30);
            this.panTopButton.TabIndex = 0;
            // 
            // picClose
            // 
            this.picClose.Location = new System.Drawing.Point(274, 7);
            this.picClose.Name = "picClose";
            this.picClose.Size = new System.Drawing.Size(16, 16);
            this.picClose.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picClose.TabIndex = 7;
            this.picClose.TabStop = false;
            this.picClose.Click += new System.EventHandler(this.picClose_Click);
            this.picClose.MouseDown += new System.Windows.Forms.MouseEventHandler(this.picClose_MouseDown);
            this.picClose.MouseLeave += new System.EventHandler(this.picClose_MouseLeave);
            this.picClose.MouseMove += new System.Windows.Forms.MouseEventHandler(this.picClose_MouseMove);
            // 
            // picLogo
            // 
            this.picLogo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.picLogo.Location = new System.Drawing.Point(0, 0);
            this.picLogo.Name = "picLogo";
            this.picLogo.Size = new System.Drawing.Size(300, 30);
            this.picLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picLogo.TabIndex = 6;
            this.picLogo.TabStop = false;
            this.picLogo.MouseDown += new System.Windows.Forms.MouseEventHandler(this.picLogo_MouseDown);
            this.picLogo.MouseMove += new System.Windows.Forms.MouseEventHandler(this.picLogo_MouseMove);
            this.picLogo.MouseUp += new System.Windows.Forms.MouseEventHandler(this.picLogo_MouseUp);
            // 
            // panButtons
            // 
            this.panButtons.Controls.Add(this.button5);
            this.panButtons.Controls.Add(this.button4);
            this.panButtons.Controls.Add(this.button2);
            this.panButtons.Controls.Add(this.button3);
            this.panButtons.Controls.Add(this.button1);
            this.panButtons.Controls.Add(this.picButtons);
            this.panButtons.Location = new System.Drawing.Point(0, 495);
            this.panButtons.Name = "panButtons";
            this.panButtons.Size = new System.Drawing.Size(300, 70);
            this.panButtons.TabIndex = 3;
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(245, 11);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(48, 48);
            this.button5.TabIndex = 18;
            this.button5.UseVisualStyleBackColor = true;
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(186, 11);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(48, 48);
            this.button4.TabIndex = 17;
            this.button4.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(66, 11);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(48, 48);
            this.button2.TabIndex = 16;
            this.button2.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(126, 11);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(48, 48);
            this.button3.TabIndex = 15;
            this.button3.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(6, 11);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(48, 48);
            this.button1.TabIndex = 14;
            this.button1.UseVisualStyleBackColor = true;
            // 
            // picButtons
            // 
            this.picButtons.Dock = System.Windows.Forms.DockStyle.Fill;
            this.picButtons.Location = new System.Drawing.Point(0, 0);
            this.picButtons.Name = "picButtons";
            this.picButtons.Size = new System.Drawing.Size(300, 70);
            this.picButtons.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picButtons.TabIndex = 3;
            this.picButtons.TabStop = false;
            // 
            // panTab
            // 
            this.panTab.Controls.Add(this.picLine);
            this.panTab.Controls.Add(this.picTab2);
            this.panTab.Controls.Add(this.picTab1);
            this.panTab.Controls.Add(this.picTabMain);
            this.panTab.Location = new System.Drawing.Point(1, 30);
            this.panTab.Name = "panTab";
            this.panTab.Size = new System.Drawing.Size(297, 57);
            this.panTab.TabIndex = 4;
            // 
            // picLine
            // 
            this.picLine.Image = ((System.Drawing.Image)(resources.GetObject("picLine.Image")));
            this.picLine.Location = new System.Drawing.Point(2, 53);
            this.picLine.Name = "picLine";
            this.picLine.Size = new System.Drawing.Size(294, 4);
            this.picLine.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picLine.TabIndex = 9;
            this.picLine.TabStop = false;
            // 
            // picTab2
            // 
            this.picTab2.Location = new System.Drawing.Point(149, 0);
            this.picTab2.Name = "picTab2";
            this.picTab2.Size = new System.Drawing.Size(147, 55);
            this.picTab2.TabIndex = 5;
            this.picTab2.TabStop = false;
            
            this.picTab2.MouseDown += new System.Windows.Forms.MouseEventHandler(this.picTab2_MouseDown);
            
            this.picTab2.MouseUp += new System.Windows.Forms.MouseEventHandler(this.picTab2_MouseUp);
            // 
            // picTab1
            // 
            this.picTab1.Location = new System.Drawing.Point(2, 0);
            this.picTab1.Name = "picTab1";
            this.picTab1.Size = new System.Drawing.Size(147, 55);
            this.picTab1.TabIndex = 4;
            this.picTab1.TabStop = false;
            
            this.picTab1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.picTab1_MouseDown);
            
            this.picTab1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.picTab1_MouseUp);
            // 
            // picTabMain
            // 
            this.picTabMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.picTabMain.Location = new System.Drawing.Point(0, 0);
            this.picTabMain.Name = "picTabMain";
            this.picTabMain.Size = new System.Drawing.Size(297, 57);
            this.picTabMain.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picTabMain.TabIndex = 3;
            this.picTabMain.TabStop = false;
            // 
            // panMain
            // 
            this.panMain.BackColor = System.Drawing.Color.White;
            this.panMain.Location = new System.Drawing.Point(1, 87);
            this.panMain.Name = "panMain";
            this.panMain.Size = new System.Drawing.Size(297, 408);
            this.panMain.TabIndex = 5;
            // 
            // m_trayIcon
            // 
            this.m_trayIcon.ContextMenuStrip = this.contextSystem;
            this.m_trayIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("m_trayIcon.Icon")));
            this.m_trayIcon.Visible = true;
            this.m_trayIcon.MouseUp += new System.Windows.Forms.MouseEventHandler(this.m_trayIcon_MouseUp);
            // 
            // contextSystem
            // 
            this.contextSystem.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuItemClose});
            this.contextSystem.Name = "contextSystem";
            this.contextSystem.Size = new System.Drawing.Size(101, 26);
            // 
            // MenuItemClose
            // 
            this.MenuItemClose.Name = "MenuItemClose";
            this.MenuItemClose.Size = new System.Drawing.Size(100, 22);
            this.MenuItemClose.Text = "退出";
            this.MenuItemClose.Click += new System.EventHandler(this.MenuItemClose_Click);
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gainsboro;
            this.ClientSize = new System.Drawing.Size(300, 560);
            this.Controls.Add(this.panMain);
            this.Controls.Add(this.panTab);
            this.Controls.Add(this.panButtons);
            this.Controls.Add(this.panTopButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Resize += new System.EventHandler(this.FrmMain_Resize);
            this.panTopButton.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picClose)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picLogo)).EndInit();
            this.panButtons.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picButtons)).EndInit();
            this.panTab.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picLine)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picTab2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picTab1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picTabMain)).EndInit();
            this.contextSystem.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer timerShowHide;
        private System.Windows.Forms.Panel panTopButton;
        private System.Windows.Forms.PictureBox picLogo;
        private System.Windows.Forms.Panel panButtons;
        private System.Windows.Forms.PictureBox picButtons;
        private System.Windows.Forms.Panel panTab;
        private System.Windows.Forms.PictureBox picTab2;
        private System.Windows.Forms.PictureBox picTab1;
        private System.Windows.Forms.PictureBox picTabMain;
        private System.Windows.Forms.Panel panMain;
        private System.Windows.Forms.NotifyIcon m_trayIcon;
        private System.Windows.Forms.ContextMenuStrip contextSystem;
        private System.Windows.Forms.ToolStripMenuItem MenuItemClose;
        private System.Windows.Forms.PictureBox picClose;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.PictureBox picLine;
    }
}

