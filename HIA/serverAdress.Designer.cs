namespace HIA
{
    partial class serverAdress
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(serverAdress));
            this.inp_ipserver = new System.Windows.Forms.TextBox();
            this.l_address = new System.Windows.Forms.Label();
            this.b_validate = new System.Windows.Forms.Button();
            this.b_cancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // inp_ipserver
            // 
            resources.ApplyResources(this.inp_ipserver, "inp_ipserver");
            this.inp_ipserver.Name = "inp_ipserver";
            this.inp_ipserver.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // l_address
            // 
            resources.ApplyResources(this.l_address, "l_address");
            this.l_address.Name = "l_address";
            // 
            // b_validate
            // 
            this.b_validate.Image = global::HIA.Properties.Resources.check;
            resources.ApplyResources(this.b_validate, "b_validate");
            this.b_validate.Name = "b_validate";
            this.b_validate.UseVisualStyleBackColor = true;
            this.b_validate.Click += new System.EventHandler(this.b_validate_Click);
            // 
            // b_cancel
            // 
            this.b_cancel.Image = global::HIA.Properties.Resources.navigate_cross;
            resources.ApplyResources(this.b_cancel, "b_cancel");
            this.b_cancel.Name = "b_cancel";
            this.b_cancel.UseVisualStyleBackColor = true;
            this.b_cancel.Click += new System.EventHandler(this.b_cancel_Click);
            // 
            // serverAdress
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.b_validate);
            this.Controls.Add(this.l_address);
            this.Controls.Add(this.b_cancel);
            this.Controls.Add(this.inp_ipserver);
            this.Name = "serverAdress";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox inp_ipserver;
        private System.Windows.Forms.Button b_cancel;
        private System.Windows.Forms.Label l_address;
        private System.Windows.Forms.Button b_validate;
    }
}