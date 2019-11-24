namespace OpenIII
{
    partial class SetGamePathWindow
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
            this.setGtaPathTitleLabel = new System.Windows.Forms.Label();
            this.gtaPathTextBox = new System.Windows.Forms.TextBox();
            this.selectPathButton = new System.Windows.Forms.Button();
            this.nextButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.statusLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // setGtaPathTitleLabel
            // 
            this.setGtaPathTitleLabel.AutoSize = true;
            this.setGtaPathTitleLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.setGtaPathTitleLabel.Location = new System.Drawing.Point(27, 69);
            this.setGtaPathTitleLabel.Name = "setGtaPathTitleLabel";
            this.setGtaPathTitleLabel.Size = new System.Drawing.Size(284, 17);
            this.setGtaPathTitleLabel.TabIndex = 0;
            this.setGtaPathTitleLabel.Text = "To continue, please set your GTA directory:";
            // 
            // gtaPathTextBox
            // 
            this.gtaPathTextBox.Location = new System.Drawing.Point(30, 111);
            this.gtaPathTextBox.Name = "gtaPathTextBox";
            this.gtaPathTextBox.Size = new System.Drawing.Size(572, 22);
            this.gtaPathTextBox.TabIndex = 1;
            this.gtaPathTextBox.TextChanged += new System.EventHandler(this.gtaPathTextBoxTextChanged);
            // 
            // selectPathButton
            // 
            this.selectPathButton.Location = new System.Drawing.Point(608, 108);
            this.selectPathButton.Name = "selectPathButton";
            this.selectPathButton.Size = new System.Drawing.Size(35, 28);
            this.selectPathButton.TabIndex = 2;
            this.selectPathButton.Text = "...";
            this.selectPathButton.UseVisualStyleBackColor = true;
            this.selectPathButton.Click += new System.EventHandler(this.selectPathButtonClick);
            // 
            // nextButton
            // 
            this.nextButton.Enabled = false;
            this.nextButton.Location = new System.Drawing.Point(233, 191);
            this.nextButton.Name = "nextButton";
            this.nextButton.Size = new System.Drawing.Size(86, 29);
            this.nextButton.TabIndex = 3;
            this.nextButton.Text = "Next";
            this.nextButton.UseVisualStyleBackColor = true;
            this.nextButton.Click += new System.EventHandler(this.nextButtonClick);
            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(350, 191);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(84, 29);
            this.cancelButton.TabIndex = 4;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // statusLabel
            // 
            this.statusLabel.AutoSize = true;
            this.statusLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.statusLabel.Location = new System.Drawing.Point(27, 156);
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(0, 17);
            this.statusLabel.TabIndex = 5;
            // 
            // SetGamePathWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(668, 232);
            this.Controls.Add(this.statusLabel);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.nextButton);
            this.Controls.Add(this.selectPathButton);
            this.Controls.Add(this.gtaPathTextBox);
            this.Controls.Add(this.setGtaPathTitleLabel);
            this.MaximumSize = new System.Drawing.Size(686, 279);
            this.MinimumSize = new System.Drawing.Size(686, 279);
            this.Name = "SetGamePathWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SetGamePathWindow";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label setGtaPathTitleLabel;
        private System.Windows.Forms.TextBox gtaPathTextBox;
        private System.Windows.Forms.Button selectPathButton;
        private System.Windows.Forms.Button nextButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Label statusLabel;
    }
}