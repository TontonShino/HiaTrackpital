namespace HIA_Server
{
    partial class Ihm
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
            this.rtxb_logs = new System.Windows.Forms.RichTextBox();
            this.l_logs = new System.Windows.Forms.Label();
            this.b_launch = new System.Windows.Forms.Button();
            this.b_stopServer = new System.Windows.Forms.Button();
            this.bgw_console = new System.ComponentModel.BackgroundWorker();
            this.SuspendLayout();
            // 
            // rtxb_logs
            // 
            this.rtxb_logs.Location = new System.Drawing.Point(12, 73);
            this.rtxb_logs.Name = "rtxb_logs";
            this.rtxb_logs.Size = new System.Drawing.Size(454, 166);
            this.rtxb_logs.TabIndex = 0;
            this.rtxb_logs.Text = "";
            // 
            // l_logs
            // 
            this.l_logs.AutoSize = true;
            this.l_logs.Location = new System.Drawing.Point(9, 57);
            this.l_logs.Name = "l_logs";
            this.l_logs.Size = new System.Drawing.Size(33, 13);
            this.l_logs.TabIndex = 1;
            this.l_logs.Text = "Logs:";
            // 
            // b_launch
            // 
            this.b_launch.Location = new System.Drawing.Point(34, 22);
            this.b_launch.Name = "b_launch";
            this.b_launch.Size = new System.Drawing.Size(191, 23);
            this.b_launch.TabIndex = 2;
            this.b_launch.Text = "Lancer Serveur";
            this.b_launch.UseVisualStyleBackColor = true;
            this.b_launch.Click += new System.EventHandler(this.button1_Click);
            // 
            // b_stopServer
            // 
            this.b_stopServer.Enabled = false;
            this.b_stopServer.Location = new System.Drawing.Point(261, 21);
            this.b_stopServer.Name = "b_stopServer";
            this.b_stopServer.Size = new System.Drawing.Size(184, 23);
            this.b_stopServer.TabIndex = 3;
            this.b_stopServer.Text = "Stopper Serveur";
            this.b_stopServer.UseCompatibleTextRendering = true;
            this.b_stopServer.UseVisualStyleBackColor = true;
            // 
            // bgw_console
            // 
            this.bgw_console.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgw_console_DoWork);
            // 
            // Ihm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(478, 254);
            this.Controls.Add(this.b_stopServer);
            this.Controls.Add(this.b_launch);
            this.Controls.Add(this.l_logs);
            this.Controls.Add(this.rtxb_logs);
            this.Name = "Ihm";
            this.Text = "Trackpital - Server 1.0";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox rtxb_logs;
        private System.Windows.Forms.Label l_logs;
        private System.Windows.Forms.Button b_launch;
        private System.Windows.Forms.Button b_stopServer;
        private System.ComponentModel.BackgroundWorker bgw_console;
    }
}