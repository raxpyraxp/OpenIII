using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using OpenIII.GameFiles;

namespace OpenIII.Forms
{
    public partial class TestWindow : OpenIII.Forms.BaseWindow
    {
        private IDEFile CurrentFile;

        public TestWindow()
        {
            InitializeComponent();
        }

        public void OpenFile(IDEFile file)
        {
            file.ParseData();
            CurrentFile = file;
            SetWindowTitle($"{(CurrentFile.Name != null ? CurrentFile.Name : "")} — {Text}");
            textBox1.Text = file.ToString();
        }
    }
}
