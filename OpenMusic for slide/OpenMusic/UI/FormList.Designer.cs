namespace OpenMusic.UI
{
    partial class FormList
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormList));
            this.listViewMusicianList = new System.Windows.Forms.ListView();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.listViewAlbumList = new System.Windows.Forms.ListView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.buttonSearchMusic = new System.Windows.Forms.Button();
            this.textBoxSearchCondition = new System.Windows.Forms.TextBox();
            this.radioButtonSearchByLyric = new System.Windows.Forms.RadioButton();
            this.radioButtonSearchByName = new System.Windows.Forms.RadioButton();
            this.label4 = new System.Windows.Forms.Label();
            this.buttonInsert = new System.Windows.Forms.Button();
            this.buttonRenameList = new System.Windows.Forms.Button();
            this.buttonCreateList = new System.Windows.Forms.Button();
            this.buttonDeleteList = new System.Windows.Forms.Button();
            this.buttonModifyLyric = new System.Windows.Forms.Button();
            this.buttonCreateLyric = new System.Windows.Forms.Button();
            this.buttonRemoveMusic = new System.Windows.Forms.Button();
            this.buttonMoveUp = new System.Windows.Forms.Button();
            this.buttonMoveDown = new System.Windows.Forms.Button();
            this.buttonRenameMusic = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.listViewUserMusicList = new System.Windows.Forms.ListView();
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.comboBoxMusicianList = new System.Windows.Forms.ComboBox();
            this.comboBoxAlbumList = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.listViewMusicList = new System.Windows.Forms.ListView();
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // listViewMusicianList
            // 
            this.listViewMusicianList.Font = new System.Drawing.Font("宋体", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.listViewMusicianList.FullRowSelect = true;
            this.listViewMusicianList.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.listViewMusicianList.HideSelection = false;
            this.listViewMusicianList.LabelWrap = false;
            this.listViewMusicianList.Location = new System.Drawing.Point(1, 24);
            this.listViewMusicianList.Name = "listViewMusicianList";
            this.listViewMusicianList.Size = new System.Drawing.Size(144, 478);
            this.listViewMusicianList.TabIndex = 0;
            this.listViewMusicianList.UseCompatibleStateImageBehavior = false;
            this.listViewMusicianList.View = System.Windows.Forms.View.SmallIcon;
            this.listViewMusicianList.SelectedIndexChanged += new System.EventHandler(this.listViewMusicianList_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(4, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 14);
            this.label1.TabIndex = 1;
            this.label1.Text = "选择系列";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Bold);
            this.label2.Location = new System.Drawing.Point(148, 7);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 14);
            this.label2.TabIndex = 2;
            this.label2.Text = "选择专辑";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Bold);
            this.label3.Location = new System.Drawing.Point(298, 7);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(67, 14);
            this.label3.TabIndex = 3;
            this.label3.Text = "选择音乐";
            // 
            // listViewAlbumList
            // 
            this.listViewAlbumList.Font = new System.Drawing.Font("宋体", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.listViewAlbumList.FullRowSelect = true;
            this.listViewAlbumList.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.listViewAlbumList.HideSelection = false;
            this.listViewAlbumList.LabelWrap = false;
            this.listViewAlbumList.Location = new System.Drawing.Point(151, 24);
            this.listViewAlbumList.Name = "listViewAlbumList";
            this.listViewAlbumList.Size = new System.Drawing.Size(144, 478);
            this.listViewAlbumList.TabIndex = 4;
            this.listViewAlbumList.UseCompatibleStateImageBehavior = false;
            this.listViewAlbumList.View = System.Windows.Forms.View.SmallIcon;
            this.listViewAlbumList.SelectedIndexChanged += new System.EventHandler(this.listViewAlbumList_SelectedIndexChanged);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.buttonSearchMusic);
            this.panel1.Controls.Add(this.textBoxSearchCondition);
            this.panel1.Controls.Add(this.radioButtonSearchByLyric);
            this.panel1.Controls.Add(this.radioButtonSearchByName);
            this.panel1.Location = new System.Drawing.Point(1, 508);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(631, 60);
            this.panel1.TabIndex = 6;
            // 
            // buttonSearchMusic
            // 
            this.buttonSearchMusic.Font = new System.Drawing.Font("宋体", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.buttonSearchMusic.Location = new System.Drawing.Point(580, 6);
            this.buttonSearchMusic.Name = "buttonSearchMusic";
            this.buttonSearchMusic.Size = new System.Drawing.Size(48, 48);
            this.buttonSearchMusic.TabIndex = 3;
            this.buttonSearchMusic.Text = "搜索";
            this.buttonSearchMusic.UseVisualStyleBackColor = true;
            this.buttonSearchMusic.Click += new System.EventHandler(this.buttonSearchMusic_Click);
            // 
            // textBoxSearchCondition
            // 
            this.textBoxSearchCondition.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBoxSearchCondition.Location = new System.Drawing.Point(112, 17);
            this.textBoxSearchCondition.Name = "textBoxSearchCondition";
            this.textBoxSearchCondition.Size = new System.Drawing.Size(462, 26);
            this.textBoxSearchCondition.TabIndex = 2;
            // 
            // radioButtonSearchByLyric
            // 
            this.radioButtonSearchByLyric.AutoSize = true;
            this.radioButtonSearchByLyric.Font = new System.Drawing.Font("宋体", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.radioButtonSearchByLyric.Location = new System.Drawing.Point(59, 22);
            this.radioButtonSearchByLyric.Name = "radioButtonSearchByLyric";
            this.radioButtonSearchByLyric.Size = new System.Drawing.Size(53, 17);
            this.radioButtonSearchByLyric.TabIndex = 1;
            this.radioButtonSearchByLyric.Text = "歌词";
            this.radioButtonSearchByLyric.UseVisualStyleBackColor = true;
            // 
            // radioButtonSearchByName
            // 
            this.radioButtonSearchByName.AutoSize = true;
            this.radioButtonSearchByName.Checked = true;
            this.radioButtonSearchByName.Font = new System.Drawing.Font("宋体", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.radioButtonSearchByName.Location = new System.Drawing.Point(6, 22);
            this.radioButtonSearchByName.Name = "radioButtonSearchByName";
            this.radioButtonSearchByName.Size = new System.Drawing.Size(53, 17);
            this.radioButtonSearchByName.TabIndex = 0;
            this.radioButtonSearchByName.TabStop = true;
            this.radioButtonSearchByName.Text = "名称";
            this.radioButtonSearchByName.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Bold);
            this.label4.Location = new System.Drawing.Point(635, 7);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(82, 14);
            this.label4.TabIndex = 7;
            this.label4.Text = "自定义列表";
            // 
            // buttonInsert
            // 
            this.buttonInsert.Location = new System.Drawing.Point(6, 74);
            this.buttonInsert.Name = "buttonInsert";
            this.buttonInsert.Size = new System.Drawing.Size(48, 48);
            this.buttonInsert.TabIndex = 9;
            this.buttonInsert.Text = "->";
            this.buttonInsert.UseVisualStyleBackColor = true;
            this.buttonInsert.Click += new System.EventHandler(this.buttonInsert_Click);
            // 
            // buttonRenameList
            // 
            this.buttonRenameList.Location = new System.Drawing.Point(147, 74);
            this.buttonRenameList.Name = "buttonRenameList";
            this.buttonRenameList.Size = new System.Drawing.Size(48, 48);
            this.buttonRenameList.TabIndex = 10;
            this.buttonRenameList.Text = "改名";
            this.buttonRenameList.UseVisualStyleBackColor = true;
            this.buttonRenameList.Click += new System.EventHandler(this.buttonRenameList_Click);
            // 
            // buttonCreateList
            // 
            this.buttonCreateList.Location = new System.Drawing.Point(93, 74);
            this.buttonCreateList.Name = "buttonCreateList";
            this.buttonCreateList.Size = new System.Drawing.Size(48, 48);
            this.buttonCreateList.TabIndex = 11;
            this.buttonCreateList.Text = "新建";
            this.buttonCreateList.UseVisualStyleBackColor = true;
            this.buttonCreateList.Click += new System.EventHandler(this.buttonCreateList_Click);
            // 
            // buttonDeleteList
            // 
            this.buttonDeleteList.Location = new System.Drawing.Point(201, 74);
            this.buttonDeleteList.Name = "buttonDeleteList";
            this.buttonDeleteList.Size = new System.Drawing.Size(48, 48);
            this.buttonDeleteList.TabIndex = 12;
            this.buttonDeleteList.Text = "删除";
            this.buttonDeleteList.UseVisualStyleBackColor = true;
            this.buttonDeleteList.Click += new System.EventHandler(this.buttonDeleteList_Click);
            // 
            // buttonModifyLyric
            // 
            this.buttonModifyLyric.Location = new System.Drawing.Point(201, 248);
            this.buttonModifyLyric.Name = "buttonModifyLyric";
            this.buttonModifyLyric.Size = new System.Drawing.Size(48, 48);
            this.buttonModifyLyric.TabIndex = 14;
            this.buttonModifyLyric.Text = "改词";
            this.buttonModifyLyric.UseVisualStyleBackColor = true;
            this.buttonModifyLyric.Click += new System.EventHandler(this.buttonModifyLyric_Click);
            // 
            // buttonCreateLyric
            // 
            this.buttonCreateLyric.Location = new System.Drawing.Point(201, 356);
            this.buttonCreateLyric.Name = "buttonCreateLyric";
            this.buttonCreateLyric.Size = new System.Drawing.Size(48, 48);
            this.buttonCreateLyric.TabIndex = 15;
            this.buttonCreateLyric.Text = "新建";
            this.buttonCreateLyric.UseVisualStyleBackColor = true;
            this.buttonCreateLyric.Click += new System.EventHandler(this.buttonCreateLyric_Click);
            // 
            // buttonRemoveMusic
            // 
            this.buttonRemoveMusic.Location = new System.Drawing.Point(201, 194);
            this.buttonRemoveMusic.Name = "buttonRemoveMusic";
            this.buttonRemoveMusic.Size = new System.Drawing.Size(48, 48);
            this.buttonRemoveMusic.TabIndex = 16;
            this.buttonRemoveMusic.Text = "移除";
            this.buttonRemoveMusic.UseVisualStyleBackColor = true;
            this.buttonRemoveMusic.Click += new System.EventHandler(this.buttonRemoveMusic_Click);
            // 
            // buttonMoveUp
            // 
            this.buttonMoveUp.Location = new System.Drawing.Point(202, 20);
            this.buttonMoveUp.Name = "buttonMoveUp";
            this.buttonMoveUp.Size = new System.Drawing.Size(48, 48);
            this.buttonMoveUp.TabIndex = 17;
            this.buttonMoveUp.Text = "上移";
            this.buttonMoveUp.UseVisualStyleBackColor = true;
            this.buttonMoveUp.Click += new System.EventHandler(this.buttonMoveUp_Click);
            // 
            // buttonMoveDown
            // 
            this.buttonMoveDown.Location = new System.Drawing.Point(202, 74);
            this.buttonMoveDown.Name = "buttonMoveDown";
            this.buttonMoveDown.Size = new System.Drawing.Size(48, 48);
            this.buttonMoveDown.TabIndex = 18;
            this.buttonMoveDown.Text = "下移";
            this.buttonMoveDown.UseVisualStyleBackColor = true;
            this.buttonMoveDown.Click += new System.EventHandler(this.buttonMoveDown_Click);
            // 
            // buttonRenameMusic
            // 
            this.buttonRenameMusic.Location = new System.Drawing.Point(201, 302);
            this.buttonRenameMusic.Name = "buttonRenameMusic";
            this.buttonRenameMusic.Size = new System.Drawing.Size(48, 48);
            this.buttonRenameMusic.TabIndex = 19;
            this.buttonRenameMusic.Text = "改名";
            this.buttonRenameMusic.UseVisualStyleBackColor = true;
            this.buttonRenameMusic.Click += new System.EventHandler(this.buttonRenameMusic_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.listViewUserMusicList);
            this.groupBox1.Controls.Add(this.buttonRenameMusic);
            this.groupBox1.Controls.Add(this.buttonModifyLyric);
            this.groupBox1.Controls.Add(this.buttonMoveDown);
            this.groupBox1.Controls.Add(this.buttonCreateLyric);
            this.groupBox1.Controls.Add(this.buttonMoveUp);
            this.groupBox1.Controls.Add(this.buttonRemoveMusic);
            this.groupBox1.Font = new System.Drawing.Font("宋体", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox1.Location = new System.Drawing.Point(638, 158);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(255, 410);
            this.groupBox1.TabIndex = 20;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "音乐编辑";
            // 
            // listViewUserMusicList
            // 
            this.listViewUserMusicList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader4});
            this.listViewUserMusicList.FullRowSelect = true;
            this.listViewUserMusicList.GridLines = true;
            this.listViewUserMusicList.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.listViewUserMusicList.HideSelection = false;
            this.listViewUserMusicList.Location = new System.Drawing.Point(6, 18);
            this.listViewUserMusicList.MultiSelect = false;
            this.listViewUserMusicList.Name = "listViewUserMusicList";
            this.listViewUserMusicList.Size = new System.Drawing.Size(190, 386);
            this.listViewUserMusicList.TabIndex = 23;
            this.listViewUserMusicList.TabStop = false;
            this.listViewUserMusicList.UseCompatibleStateImageBehavior = false;
            this.listViewUserMusicList.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "音乐名称";
            this.columnHeader4.Width = 176;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.comboBoxMusicianList);
            this.groupBox2.Controls.Add(this.comboBoxAlbumList);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.buttonInsert);
            this.groupBox2.Controls.Add(this.buttonDeleteList);
            this.groupBox2.Controls.Add(this.buttonRenameList);
            this.groupBox2.Controls.Add(this.buttonCreateList);
            this.groupBox2.Font = new System.Drawing.Font("宋体", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox2.Location = new System.Drawing.Point(638, 24);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(255, 128);
            this.groupBox2.TabIndex = 21;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "列表编辑";
            // 
            // comboBoxMusicianList
            // 
            this.comboBoxMusicianList.DropDownHeight = 300;
            this.comboBoxMusicianList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxMusicianList.DropDownWidth = 160;
            this.comboBoxMusicianList.IntegralHeight = false;
            this.comboBoxMusicianList.Location = new System.Drawing.Point(52, 20);
            this.comboBoxMusicianList.Name = "comboBoxMusicianList";
            this.comboBoxMusicianList.Size = new System.Drawing.Size(198, 21);
            this.comboBoxMusicianList.TabIndex = 17;
            this.comboBoxMusicianList.TabStop = false;
            this.comboBoxMusicianList.SelectedIndexChanged += new System.EventHandler(this.comboBoxMusicianList_SelectedIndexChanged);
            // 
            // comboBoxAlbumList
            // 
            this.comboBoxAlbumList.DropDownHeight = 300;
            this.comboBoxAlbumList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxAlbumList.DropDownWidth = 160;
            this.comboBoxAlbumList.IntegralHeight = false;
            this.comboBoxAlbumList.Location = new System.Drawing.Point(52, 47);
            this.comboBoxAlbumList.Name = "comboBoxAlbumList";
            this.comboBoxAlbumList.Size = new System.Drawing.Size(198, 21);
            this.comboBoxAlbumList.TabIndex = 16;
            this.comboBoxAlbumList.TabStop = false;
            this.comboBoxAlbumList.SelectedIndexChanged += new System.EventHandler(this.comboBoxAlbumList_SelectedIndexChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("宋体", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label7.Location = new System.Drawing.Point(6, 50);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(49, 13);
            this.label7.TabIndex = 15;
            this.label7.Text = "专辑：";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("宋体", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label9.Location = new System.Drawing.Point(6, 23);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(49, 13);
            this.label9.TabIndex = 14;
            this.label9.Text = "系列：";
            // 
            // listViewMusicList
            // 
            this.listViewMusicList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader2});
            this.listViewMusicList.Font = new System.Drawing.Font("宋体", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.listViewMusicList.FullRowSelect = true;
            this.listViewMusicList.GridLines = true;
            this.listViewMusicList.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.listViewMusicList.HideSelection = false;
            this.listViewMusicList.Location = new System.Drawing.Point(301, 24);
            this.listViewMusicList.MultiSelect = false;
            this.listViewMusicList.Name = "listViewMusicList";
            this.listViewMusicList.Size = new System.Drawing.Size(331, 478);
            this.listViewMusicList.TabIndex = 22;
            this.listViewMusicList.TabStop = false;
            this.listViewMusicList.UseCompatibleStateImageBehavior = false;
            this.listViewMusicList.View = System.Windows.Forms.View.Details;
            this.listViewMusicList.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listViewMusicList_MouseDoubleClick);
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "音乐名称";
            this.columnHeader2.Width = 311;
            // 
            // FormList
            // 
            this.AcceptButton = this.buttonSearchMusic;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(896, 573);
            this.Controls.Add(this.listViewMusicList);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.listViewAlbumList);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.listViewMusicianList);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormList";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "音乐列表 - 沈阳以利亚科技有限公司 - www.Elijah.com.cn";
            this.Load += new System.EventHandler(this.FormList_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView listViewMusicianList;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ListView listViewAlbumList;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RadioButton radioButtonSearchByLyric;
        private System.Windows.Forms.RadioButton radioButtonSearchByName;
        private System.Windows.Forms.Button buttonSearchMusic;
        private System.Windows.Forms.TextBox textBoxSearchCondition;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button buttonInsert;
        private System.Windows.Forms.Button buttonRenameList;
        private System.Windows.Forms.Button buttonCreateList;
        private System.Windows.Forms.Button buttonDeleteList;
        private System.Windows.Forms.Button buttonModifyLyric;
        private System.Windows.Forms.Button buttonCreateLyric;
        private System.Windows.Forms.Button buttonRemoveMusic;
        private System.Windows.Forms.Button buttonMoveUp;
        private System.Windows.Forms.Button buttonMoveDown;
        private System.Windows.Forms.Button buttonRenameMusic;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox comboBoxMusicianList;
        private System.Windows.Forms.ComboBox comboBoxAlbumList;
        public System.Windows.Forms.ListView listViewUserMusicList;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        public System.Windows.Forms.ListView listViewMusicList;
        private System.Windows.Forms.ColumnHeader columnHeader2;

    }
}