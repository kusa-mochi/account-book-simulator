namespace AccountBookSimu
{
    partial class ProgressForm
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
            this.progressBar_Simulation = new System.Windows.Forms.ProgressBar();
            this.button_Cancel = new System.Windows.Forms.Button();
            this.label_Message = new System.Windows.Forms.Label();
            this.label_Percent = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // progressBar_Simulation
            // 
            this.progressBar_Simulation.Location = new System.Drawing.Point(12, 30);
            this.progressBar_Simulation.Name = "progressBar_Simulation";
            this.progressBar_Simulation.Size = new System.Drawing.Size(501, 23);
            this.progressBar_Simulation.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.progressBar_Simulation.TabIndex = 0;
            this.progressBar_Simulation.Value = 50;
            //this.progressBar_Simulation.MouseDown += new System.Windows.Forms.MouseEventHandler(this.progressBar_Simulation_MouseDown);
            //this.progressBar_Simulation.MouseMove += new System.Windows.Forms.MouseEventHandler(this.progressBar_Simulation_MouseMove);
            // 
            // button_Cancel
            // 
            this.button_Cancel.Location = new System.Drawing.Point(406, 59);
            this.button_Cancel.Name = "button_Cancel";
            this.button_Cancel.Size = new System.Drawing.Size(107, 31);
            this.button_Cancel.TabIndex = 1;
            this.button_Cancel.Text = "キャンセル";
            this.button_Cancel.UseVisualStyleBackColor = true;
            this.button_Cancel.Click += new System.EventHandler(this.button_Cancel_Click);
            // 
            // label_Message
            // 
            this.label_Message.AutoSize = true;
            this.label_Message.Location = new System.Drawing.Point(12, 9);
            this.label_Message.Name = "label_Message";
            this.label_Message.Size = new System.Drawing.Size(52, 18);
            this.label_Message.TabIndex = 2;
            this.label_Message.Text = "label1";
            //this.label_Message.MouseDown += new System.Windows.Forms.MouseEventHandler(this.label_Message_MouseDown);
            //this.label_Message.MouseMove += new System.Windows.Forms.MouseEventHandler(this.label_Message_MouseMove);
            // 
            // label_Percent
            // 
            this.label_Percent.AutoSize = true;
            this.label_Percent.Location = new System.Drawing.Point(403, 9);
            this.label_Percent.Name = "label_Percent";
            this.label_Percent.Size = new System.Drawing.Size(52, 18);
            this.label_Percent.TabIndex = 3;
            this.label_Percent.Text = "label2";
            this.label_Percent.TextAlign = System.Drawing.ContentAlignment.TopRight;
            //this.label_Percent.MouseDown += new System.Windows.Forms.MouseEventHandler(this.label_Percent_MouseDown);
            //this.label_Percent.MouseMove += new System.Windows.Forms.MouseEventHandler(this.label_Percent_MouseMove);
            // 
            // ProgressForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(525, 104);
            this.Controls.Add(this.label_Percent);
            this.Controls.Add(this.label_Message);
            this.Controls.Add(this.button_Cancel);
            this.Controls.Add(this.progressBar_Simulation);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "ProgressForm";
            this.Text = "ProgressForm";
            this.Shown += new System.EventHandler(this.ProgressForm_Shown);
            //this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ProgressForm_MouseDown);
            //this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.ProgressForm_MouseMove);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ProgressBar progressBar_Simulation;
        private System.Windows.Forms.Button button_Cancel;
        private System.Windows.Forms.Label label_Message;
        private System.Windows.Forms.Label label_Percent;
    }
}