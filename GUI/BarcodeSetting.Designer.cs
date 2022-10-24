
namespace AISIN_WFA.GUI
{
    partial class BarcodeSetting
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
            this.chk_holdSmema = new System.Windows.Forms.CheckBox();
            this.chk_autoChange = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // chk_holdSmema
            // 
            this.chk_holdSmema.AutoSize = true;
            this.chk_holdSmema.Location = new System.Drawing.Point(14, 35);
            this.chk_holdSmema.Name = "chk_holdSmema";
            this.chk_holdSmema.Size = new System.Drawing.Size(195, 24);
            this.chk_holdSmema.TabIndex = 0;
            this.chk_holdSmema.Text = "Hold smema until scan";
            this.chk_holdSmema.UseVisualStyleBackColor = true;
            this.chk_holdSmema.CheckedChanged += new System.EventHandler(this.chk_holdSmema_CheckedChanged);
            // 
            // chk_autoChange
            // 
            this.chk_autoChange.AutoSize = true;
            this.chk_autoChange.Location = new System.Drawing.Point(14, 99);
            this.chk_autoChange.Name = "chk_autoChange";
            this.chk_autoChange.Size = new System.Drawing.Size(466, 24);
            this.chk_autoChange.TabIndex = 1;
            this.chk_autoChange.Text = "Change Recipe, rail width or belt speed on barcode mismatch";
            this.chk_autoChange.UseVisualStyleBackColor = true;
            this.chk_autoChange.CheckedChanged += new System.EventHandler(this.chk_autoChange_CheckedChanged);
            // 
            // BarcodeSetting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(497, 153);
            this.Controls.Add(this.chk_autoChange);
            this.Controls.Add(this.chk_holdSmema);
            this.Name = "BarcodeSetting";
            this.Text = "BarcodeSetting";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox chk_holdSmema;
        private System.Windows.Forms.CheckBox chk_autoChange;
    }
}