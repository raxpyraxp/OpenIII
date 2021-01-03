/*
 *  This file is a part of OpenIII project, the GTA modding tool.
 *  
 *  Copyright (C) 2019-2020 Savelii Morozov (Prographer)
 *  Email: morozov.salevii@gmail.com
 *  
 *  Copyright (C) 2019-2020 Sergey Filatov (raxp)
 *  Email: raxp.worm202@gmail.com
 *  
 *  This program is free software: you can redistribute it and/or modify
 *  it under the terms of the GNU General Public License as published by
 *  the Free Software Foundation, either version 3 of the License, or
 *  (at your option) any later version.
 *
 *  This program is distributed in the hope that it will be useful,
 *  but WITHOUT ANY WARRANTY; without even the implied warranty of
 *  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 *  GNU General Public License for more details.
 *
 *  You should have received a copy of the GNU General Public License
 *  along with this program.  If not, see <https://www.gnu.org/licenses/>.
 */

namespace OpenIII.Forms
{
    partial class AboutWindow
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
            this.prographerCopyrightLabel = new System.Windows.Forms.Label();
            this.raxpCopyrightLabel = new System.Windows.Forms.Label();
            this.warrantyNoticeLabel = new System.Windows.Forms.Label();
            this.prographerEmailLinkLabel = new System.Windows.Forms.LinkLabel();
            this.raxpEmailLinkLabel = new System.Windows.Forms.LinkLabel();
            this.freeSoftwareLabel = new System.Windows.Forms.Label();
            this.warrantyDetailsLinkLabel = new System.Windows.Forms.LinkLabel();
            this.freeSoftwareDetailsLinkLabel = new System.Windows.Forms.LinkLabel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.versionLabel = new System.Windows.Forms.Label();
            this.githubNoticeLabel = new System.Windows.Forms.Label();
            this.githubLink = new System.Windows.Forms.LinkLabel();
            this.gplNoticeLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // prographerCopyrightLabel
            // 
            this.prographerCopyrightLabel.AutoSize = true;
            this.prographerCopyrightLabel.Location = new System.Drawing.Point(12, 396);
            this.prographerCopyrightLabel.Name = "prographerCopyrightLabel";
            this.prographerCopyrightLabel.Size = new System.Drawing.Size(353, 17);
            this.prographerCopyrightLabel.TabIndex = 2;
            this.prographerCopyrightLabel.Text = "Copyright (C) 2019-2020 Savelii Morozov (Prographer)";
            // 
            // raxpCopyrightLabel
            // 
            this.raxpCopyrightLabel.AutoSize = true;
            this.raxpCopyrightLabel.Location = new System.Drawing.Point(12, 432);
            this.raxpCopyrightLabel.Name = "raxpCopyrightLabel";
            this.raxpCopyrightLabel.Size = new System.Drawing.Size(299, 17);
            this.raxpCopyrightLabel.TabIndex = 3;
            this.raxpCopyrightLabel.Text = "Copyright (C) 2019-2020 Sergey Filatov (raxp)\r\n";
            // 
            // warrantyNoticeLabel
            // 
            this.warrantyNoticeLabel.AutoSize = true;
            this.warrantyNoticeLabel.Location = new System.Drawing.Point(12, 542);
            this.warrantyNoticeLabel.Name = "warrantyNoticeLabel";
            this.warrantyNoticeLabel.Size = new System.Drawing.Size(372, 17);
            this.warrantyNoticeLabel.TabIndex = 4;
            this.warrantyNoticeLabel.Text = "This program comes with ABSOLUTELY NO WARRANTY.";
            // 
            // prographerEmailLinkLabel
            // 
            this.prographerEmailLinkLabel.AutoSize = true;
            this.prographerEmailLinkLabel.Location = new System.Drawing.Point(371, 396);
            this.prographerEmailLinkLabel.Name = "prographerEmailLinkLabel";
            this.prographerEmailLinkLabel.Size = new System.Drawing.Size(182, 17);
            this.prographerEmailLinkLabel.TabIndex = 5;
            this.prographerEmailLinkLabel.TabStop = true;
            this.prographerEmailLinkLabel.Text = "morozov.salevii@gmail.com";
            this.prographerEmailLinkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.emailLinkLabelClicked);
            // 
            // raxpEmailLinkLabel
            // 
            this.raxpEmailLinkLabel.AutoSize = true;
            this.raxpEmailLinkLabel.Location = new System.Drawing.Point(317, 432);
            this.raxpEmailLinkLabel.Name = "raxpEmailLinkLabel";
            this.raxpEmailLinkLabel.Size = new System.Drawing.Size(173, 17);
            this.raxpEmailLinkLabel.TabIndex = 6;
            this.raxpEmailLinkLabel.TabStop = true;
            this.raxpEmailLinkLabel.Text = "raxp.worm202@gmail.com";
            this.raxpEmailLinkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.emailLinkLabelClicked);
            // 
            // freeSoftwareLabel
            // 
            this.freeSoftwareLabel.AutoSize = true;
            this.freeSoftwareLabel.Location = new System.Drawing.Point(12, 571);
            this.freeSoftwareLabel.Name = "freeSoftwareLabel";
            this.freeSoftwareLabel.Size = new System.Drawing.Size(539, 17);
            this.freeSoftwareLabel.TabIndex = 7;
            this.freeSoftwareLabel.Text = "This is free software, and you are welcome to redistribute it under certain condi" +
    "tions.";
            // 
            // warrantyDetailsLinkLabel
            // 
            this.warrantyDetailsLinkLabel.AutoSize = true;
            this.warrantyDetailsLinkLabel.Location = new System.Drawing.Point(390, 542);
            this.warrantyDetailsLinkLabel.Name = "warrantyDetailsLinkLabel";
            this.warrantyDetailsLinkLabel.Size = new System.Drawing.Size(78, 17);
            this.warrantyDetailsLinkLabel.TabIndex = 8;
            this.warrantyDetailsLinkLabel.TabStop = true;
            this.warrantyDetailsLinkLabel.Text = "See details";
            this.warrantyDetailsLinkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.warrantyLinkLabelClicked);
            // 
            // freeSoftwareDetailsLinkLabel
            // 
            this.freeSoftwareDetailsLinkLabel.AutoSize = true;
            this.freeSoftwareDetailsLinkLabel.Location = new System.Drawing.Point(557, 571);
            this.freeSoftwareDetailsLinkLabel.Name = "freeSoftwareDetailsLinkLabel";
            this.freeSoftwareDetailsLinkLabel.Size = new System.Drawing.Size(78, 17);
            this.freeSoftwareDetailsLinkLabel.TabIndex = 9;
            this.freeSoftwareDetailsLinkLabel.TabStop = true;
            this.freeSoftwareDetailsLinkLabel.Text = "See details";
            this.freeSoftwareDetailsLinkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.licenseLinkLabelClicked);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::OpenIII.Properties.Resources.cover;
            this.pictureBox1.Location = new System.Drawing.Point(15, 13);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(872, 329);
            this.pictureBox1.TabIndex = 10;
            this.pictureBox1.TabStop = false;
            // 
            // versionLabel
            // 
            this.versionLabel.AutoSize = true;
            this.versionLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.versionLabel.Location = new System.Drawing.Point(384, 352);
            this.versionLabel.Name = "versionLabel";
            this.versionLabel.Size = new System.Drawing.Size(133, 25);
            this.versionLabel.TabIndex = 11;
            this.versionLabel.Text = "Version: 0.0.0";
            // 
            // githubNoticeLabel
            // 
            this.githubNoticeLabel.AutoSize = true;
            this.githubNoticeLabel.Location = new System.Drawing.Point(12, 468);
            this.githubNoticeLabel.Name = "githubNoticeLabel";
            this.githubNoticeLabel.Size = new System.Drawing.Size(333, 17);
            this.githubNoticeLabel.TabIndex = 12;
            this.githubNoticeLabel.Text = "You can contribute to this program using our official";
            // 
            // githubLink
            // 
            this.githubLink.AutoSize = true;
            this.githubLink.Location = new System.Drawing.Point(349, 468);
            this.githubLink.Name = "githubLink";
            this.githubLink.Size = new System.Drawing.Size(119, 17);
            this.githubLink.TabIndex = 13;
            this.githubLink.TabStop = true;
            this.githubLink.Text = "GitHub repository";
            this.githubLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.githubLinkClicked);
            // 
            // gplNoticeLabel
            // 
            this.gplNoticeLabel.AutoSize = true;
            this.gplNoticeLabel.Location = new System.Drawing.Point(12, 512);
            this.gplNoticeLabel.Name = "gplNoticeLabel";
            this.gplNoticeLabel.Size = new System.Drawing.Size(503, 17);
            this.gplNoticeLabel.TabIndex = 14;
            this.gplNoticeLabel.Text = "This program is licensed under GNU General Public License version 3 or later.";
            // 
            // AboutWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(901, 601);
            this.Controls.Add(this.gplNoticeLabel);
            this.Controls.Add(this.githubLink);
            this.Controls.Add(this.githubNoticeLabel);
            this.Controls.Add(this.versionLabel);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.freeSoftwareDetailsLinkLabel);
            this.Controls.Add(this.warrantyDetailsLinkLabel);
            this.Controls.Add(this.freeSoftwareLabel);
            this.Controls.Add(this.raxpEmailLinkLabel);
            this.Controls.Add(this.prographerEmailLinkLabel);
            this.Controls.Add(this.warrantyNoticeLabel);
            this.Controls.Add(this.raxpCopyrightLabel);
            this.Controls.Add(this.prographerCopyrightLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AboutWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "About";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label prographerCopyrightLabel;
        private System.Windows.Forms.Label raxpCopyrightLabel;
        private System.Windows.Forms.Label warrantyNoticeLabel;
        private System.Windows.Forms.LinkLabel prographerEmailLinkLabel;
        private System.Windows.Forms.LinkLabel raxpEmailLinkLabel;
        private System.Windows.Forms.Label freeSoftwareLabel;
        private System.Windows.Forms.LinkLabel warrantyDetailsLinkLabel;
        private System.Windows.Forms.LinkLabel freeSoftwareDetailsLinkLabel;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label versionLabel;
        private System.Windows.Forms.Label githubNoticeLabel;
        private System.Windows.Forms.LinkLabel githubLink;
        private System.Windows.Forms.Label gplNoticeLabel;
    }
}