using System;
using System.Drawing;
using System.ComponentModel;
using System.Windows.Forms;

namespace Launchpad.Forms
{
    partial class MissionPatch
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
            this.missionPatchImageLabel = new Label();
            this.saveMissionPatchImageButton = new Button();
            this.SuspendLayout();
            //
            // missionPatchImageLabel
            //
            this.missionPatchImageLabel.Anchor =
                ((AnchorStyles) ((((AnchorStyles.Top | AnchorStyles.Bottom) | AnchorStyles.Left) | AnchorStyles.Right)));
            this.missionPatchImageLabel.Location = new Point(0, 0);
            this.missionPatchImageLabel.Size = new Size(512, 512);
            this.missionPatchImageLabel.Click += new EventHandler(this.missionPatchImageLabel_Click);
            //
            // saveMissionPatchImageButton
            //
            this.saveMissionPatchImageButton.Location = new Point(470, 478);
            this.saveMissionPatchImageButton.Size = new Size(30, 22);
            this.saveMissionPatchImageButton.Image = (Image) _resources.GetObject("$this.saveImage");
            this.saveMissionPatchImageButton.ImageAlign = ContentAlignment.MiddleCenter;
            this.saveMissionPatchImageButton.UseCompatibleTextRendering = true;
            this.saveMissionPatchImageButton.UseVisualStyleBackColor = true;
            this.saveMissionPatchImageButton.Click += new EventHandler(this.saveMissionPatchImageButton_Click);
            //
            // MissionPatch
            //
            this.components = new Container();
            this.AutoScaleMode = AutoScaleMode.Font;
            this.BackColor = Color.White;
            this.ClientSize = new Size(512, 512);
            this.Controls.Add(this.missionPatchImageLabel);
            this.Controls.Add(this.saveMissionPatchImageButton);
            this.Text = $"—{Application.ProductName}—";
            this.MinimizeBox = false;
            this.MaximizeBox = false;
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.ResumeLayout(false);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.Select();
        }

        #endregion
        
        private Label missionPatchImageLabel;
        private Button saveMissionPatchImageButton;
    }
}