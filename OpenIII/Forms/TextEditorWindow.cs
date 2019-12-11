using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace OpenIII
{
    public partial class TextEditorWindow : Form
    {
        private static TextEditorWindow instance;
        public TextEditorWindow()
        {
            InitializeComponent();
        }

        public void SetTextArea(string text)
        {
            FileContent.Text = text;
        }

        public static TextEditorWindow GetInstance()
        {
            if (instance == null)
            {
                instance = new TextEditorWindow();
            }

            return instance;
        }
    }
}
