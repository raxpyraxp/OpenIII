using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OpenIII.Forms
{
    public partial class ProgressBarWindow : Form
    {
        protected Action action;

        public ProgressBarWindow()
        {
            InitializeComponent();
        }

        public void StartDialogWithAction(Action action, CancellationTokenSource tokenSource)
        {
            FormClosed += (s, e) =>
            {
                tokenSource.Cancel();
            };

            this.action = action;
            ShowDialog();
        }

        public void SetOperationText(string label)
        {
            operationLabel.Text = label;
        }

        public void SetProgress(int percent)
        {
            progressBar.Value = percent;
            Text = string.Format("Working... {0}%", percent);
        }

        private void OnCancelClick(object sender, EventArgs e)
        {
            Close();
        }

        public void InvokeOnThread(Action action)
        {
            if (!Disposing && InvokeRequired)
            {
                try
                {
                    Invoke(action);
                } catch (ObjectDisposedException) { }
            }
        }

        private void WindowShown(object sender, EventArgs e)
        {
            Task.Factory.StartNew(action);
        }
    }
}
