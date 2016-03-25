namespace AccountBookSimu
{
    partial class MainForm
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.listBox_PayPattern = new System.Windows.Forms.ListBox();
            this.dateTimePicker_From = new System.Windows.Forms.DateTimePicker();
            this.dateTimePicker_To = new System.Windows.Forms.DateTimePicker();
            this.button_AddPayPattern = new System.Windows.Forms.Button();
            this.button_RemovePayPattern = new System.Windows.Forms.Button();
            this.button_EditPayPattern = new System.Windows.Forms.Button();
            this.label_From = new System.Windows.Forms.Label();
            this.label_To = new System.Windows.Forms.Label();
            this.button_Simulate = new System.Windows.Forms.Button();
            this.label_PayPattern = new System.Windows.Forms.Label();
            this.button_SaveSetting = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.ファイルFToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem_FileOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem_Save = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem_SaveAs = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.ToolStripMenuItem_Quit = new System.Windows.Forms.ToolStripMenuItem();
            this.編集ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem_AddPattern = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem_RemovePattern = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem_EditPattern = new System.Windows.Forms.ToolStripMenuItem();
            this.実行DToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem_Simulate = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // listBox_PayPattern
            // 
            this.listBox_PayPattern.FormattingEnabled = true;
            resources.ApplyResources(this.listBox_PayPattern, "listBox_PayPattern");
            this.listBox_PayPattern.Name = "listBox_PayPattern";
            // 
            // dateTimePicker_From
            // 
            resources.ApplyResources(this.dateTimePicker_From, "dateTimePicker_From");
            this.dateTimePicker_From.Name = "dateTimePicker_From";
            // 
            // dateTimePicker_To
            // 
            resources.ApplyResources(this.dateTimePicker_To, "dateTimePicker_To");
            this.dateTimePicker_To.Name = "dateTimePicker_To";
            // 
            // button_AddPayPattern
            // 
            resources.ApplyResources(this.button_AddPayPattern, "button_AddPayPattern");
            this.button_AddPayPattern.Name = "button_AddPayPattern";
            this.button_AddPayPattern.UseVisualStyleBackColor = true;
            this.button_AddPayPattern.Click += new System.EventHandler(this.button_AddPayPattern_Click);
            // 
            // button_RemovePayPattern
            // 
            resources.ApplyResources(this.button_RemovePayPattern, "button_RemovePayPattern");
            this.button_RemovePayPattern.Name = "button_RemovePayPattern";
            this.button_RemovePayPattern.UseVisualStyleBackColor = true;
            this.button_RemovePayPattern.Click += new System.EventHandler(this.button_RemovePayPattern_Click);
            // 
            // button_EditPayPattern
            // 
            resources.ApplyResources(this.button_EditPayPattern, "button_EditPayPattern");
            this.button_EditPayPattern.Name = "button_EditPayPattern";
            this.button_EditPayPattern.UseVisualStyleBackColor = true;
            this.button_EditPayPattern.Click += new System.EventHandler(this.button_EditPayPattern_Click);
            // 
            // label_From
            // 
            resources.ApplyResources(this.label_From, "label_From");
            this.label_From.Name = "label_From";
            // 
            // label_To
            // 
            resources.ApplyResources(this.label_To, "label_To");
            this.label_To.Name = "label_To";
            // 
            // button_Simulate
            // 
            resources.ApplyResources(this.button_Simulate, "button_Simulate");
            this.button_Simulate.Name = "button_Simulate";
            this.button_Simulate.UseVisualStyleBackColor = true;
            this.button_Simulate.Click += new System.EventHandler(this.button_Simulate_Click);
            // 
            // label_PayPattern
            // 
            resources.ApplyResources(this.label_PayPattern, "label_PayPattern");
            this.label_PayPattern.Name = "label_PayPattern";
            // 
            // button_SaveSetting
            // 
            resources.ApplyResources(this.button_SaveSetting, "button_SaveSetting");
            this.button_SaveSetting.Name = "button_SaveSetting";
            this.button_SaveSetting.UseVisualStyleBackColor = true;
            this.button_SaveSetting.Click += new System.EventHandler(this.button_SaveSetting_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ファイルFToolStripMenuItem,
            this.編集ToolStripMenuItem,
            this.実行DToolStripMenuItem});
            resources.ApplyResources(this.menuStrip1, "menuStrip1");
            this.menuStrip1.Name = "menuStrip1";
            // 
            // ファイルFToolStripMenuItem
            // 
            this.ファイルFToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItem_FileOpen,
            this.ToolStripMenuItem_Save,
            this.ToolStripMenuItem_SaveAs,
            this.toolStripSeparator1,
            this.ToolStripMenuItem_Quit});
            this.ファイルFToolStripMenuItem.Name = "ファイルFToolStripMenuItem";
            resources.ApplyResources(this.ファイルFToolStripMenuItem, "ファイルFToolStripMenuItem");
            // 
            // ToolStripMenuItem_FileOpen
            // 
            this.ToolStripMenuItem_FileOpen.Name = "ToolStripMenuItem_FileOpen";
            resources.ApplyResources(this.ToolStripMenuItem_FileOpen, "ToolStripMenuItem_FileOpen");
            this.ToolStripMenuItem_FileOpen.Click += new System.EventHandler(this.ToolStripMenuItem_FileOpen_Click);
            // 
            // ToolStripMenuItem_Save
            // 
            this.ToolStripMenuItem_Save.Name = "ToolStripMenuItem_Save";
            resources.ApplyResources(this.ToolStripMenuItem_Save, "ToolStripMenuItem_Save");
            this.ToolStripMenuItem_Save.Click += new System.EventHandler(this.ToolStripMenuItem_Save_Click);
            // 
            // ToolStripMenuItem_SaveAs
            // 
            this.ToolStripMenuItem_SaveAs.Name = "ToolStripMenuItem_SaveAs";
            resources.ApplyResources(this.ToolStripMenuItem_SaveAs, "ToolStripMenuItem_SaveAs");
            this.ToolStripMenuItem_SaveAs.Click += new System.EventHandler(this.ToolStripMenuItem_SaveAs_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            resources.ApplyResources(this.toolStripSeparator1, "toolStripSeparator1");
            // 
            // ToolStripMenuItem_Quit
            // 
            this.ToolStripMenuItem_Quit.Name = "ToolStripMenuItem_Quit";
            resources.ApplyResources(this.ToolStripMenuItem_Quit, "ToolStripMenuItem_Quit");
            this.ToolStripMenuItem_Quit.Click += new System.EventHandler(this.ToolStripMenuItem_Quit_Click);
            // 
            // 編集ToolStripMenuItem
            // 
            this.編集ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItem_AddPattern,
            this.ToolStripMenuItem_RemovePattern,
            this.ToolStripMenuItem_EditPattern});
            this.編集ToolStripMenuItem.Name = "編集ToolStripMenuItem";
            resources.ApplyResources(this.編集ToolStripMenuItem, "編集ToolStripMenuItem");
            // 
            // ToolStripMenuItem_AddPattern
            // 
            this.ToolStripMenuItem_AddPattern.Name = "ToolStripMenuItem_AddPattern";
            resources.ApplyResources(this.ToolStripMenuItem_AddPattern, "ToolStripMenuItem_AddPattern");
            this.ToolStripMenuItem_AddPattern.Click += new System.EventHandler(this.ToolStripMenuItem_AddPattern_Click);
            // 
            // ToolStripMenuItem_RemovePattern
            // 
            this.ToolStripMenuItem_RemovePattern.Name = "ToolStripMenuItem_RemovePattern";
            resources.ApplyResources(this.ToolStripMenuItem_RemovePattern, "ToolStripMenuItem_RemovePattern");
            this.ToolStripMenuItem_RemovePattern.Click += new System.EventHandler(this.ToolStripMenuItem_RemovePattern_Click);
            // 
            // ToolStripMenuItem_EditPattern
            // 
            this.ToolStripMenuItem_EditPattern.Name = "ToolStripMenuItem_EditPattern";
            resources.ApplyResources(this.ToolStripMenuItem_EditPattern, "ToolStripMenuItem_EditPattern");
            this.ToolStripMenuItem_EditPattern.Click += new System.EventHandler(this.ToolStripMenuItem_EditPattern_Click);
            // 
            // 実行DToolStripMenuItem
            // 
            this.実行DToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItem_Simulate});
            this.実行DToolStripMenuItem.Name = "実行DToolStripMenuItem";
            resources.ApplyResources(this.実行DToolStripMenuItem, "実行DToolStripMenuItem");
            // 
            // ToolStripMenuItem_Simulate
            // 
            this.ToolStripMenuItem_Simulate.Name = "ToolStripMenuItem_Simulate";
            resources.ApplyResources(this.ToolStripMenuItem_Simulate, "ToolStripMenuItem_Simulate");
            this.ToolStripMenuItem_Simulate.Click += new System.EventHandler(this.ToolStripMenuItem_Simulate_Click);
            // 
            // MainForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.button_SaveSetting);
            this.Controls.Add(this.label_PayPattern);
            this.Controls.Add(this.button_Simulate);
            this.Controls.Add(this.label_To);
            this.Controls.Add(this.label_From);
            this.Controls.Add(this.button_EditPayPattern);
            this.Controls.Add(this.button_RemovePayPattern);
            this.Controls.Add(this.button_AddPayPattern);
            this.Controls.Add(this.dateTimePicker_To);
            this.Controls.Add(this.dateTimePicker_From);
            this.Controls.Add(this.listBox_PayPattern);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox listBox_PayPattern;
        private System.Windows.Forms.DateTimePicker dateTimePicker_From;
        private System.Windows.Forms.DateTimePicker dateTimePicker_To;
        private System.Windows.Forms.Button button_AddPayPattern;
        private System.Windows.Forms.Button button_RemovePayPattern;
        private System.Windows.Forms.Button button_EditPayPattern;
        private System.Windows.Forms.Label label_From;
        private System.Windows.Forms.Label label_To;
        private System.Windows.Forms.Button button_Simulate;
        private System.Windows.Forms.Label label_PayPattern;
        private System.Windows.Forms.Button button_SaveSetting;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem ファイルFToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_FileOpen;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_Save;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_SaveAs;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_Quit;
        private System.Windows.Forms.ToolStripMenuItem 編集ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_AddPattern;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_RemovePattern;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_EditPattern;
        private System.Windows.Forms.ToolStripMenuItem 実行DToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_Simulate;
    }
}

