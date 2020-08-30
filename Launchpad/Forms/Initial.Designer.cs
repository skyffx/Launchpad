using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Launchpad.Util;

namespace Launchpad.Forms
{
    partial class Initial
    {
        private readonly ComponentResourceManager _resources =
            new ComponentResourceManager(typeof(Main));
        
        // Required designer variable.
        private IContainer components = null;

        // Required designer variable. -> (disposing) true if managed resources should be disposed; otherwise, false.
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }

            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.SuspendLayout();
            this.BackgroundImage = ImageUtil.ResizeImageAndKeepRatio((Image) _resources.GetObject("$this.appLogo"), 200, 200);
            this.ClientSize = new System.Drawing.Size(200, 200);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Icon = ((Icon) (_resources.GetObject("$this.appIcon")));
            this.MaximizeBox = false;
            this.MaximizeBox = false;
            this.Text = Application.ProductName;
            this.Load += new System.EventHandler(this.AppLoad);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.ResumeLayout(false);
        }

        #endregion
    }
}