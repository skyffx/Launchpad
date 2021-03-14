using System;
using System.Drawing;
using System.IO;
using System.ComponentModel;
using System.Windows.Forms;

namespace Launchpad.Forms
{
    partial class MainForm
    {
	    private readonly ComponentResourceManager _resources =
		    new ComponentResourceManager(typeof(MainForm));
	    
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
	        this.currentMissionButton = new Button();
	        this.previousMissionButton = new Button();
	        this.nextMissionButton = new Button();
	        this.missionPatchImageLabel = new Label();
	        this.missionNameLabel = new Label();
	        this.missionDetailsLabel = new Label();
	        this.vehicleStatusLabel = new Label();
	        this.missionStatusHeaderLabel = new Label();
	        this.missionStatusLabel = new Label();
	        this.missionDataButton = new Button();
	        this.aboutLaunchpadButton = new Button();
	        this.SuspendLayout();
	        //
	        // currentMissionButton
	        //
	        this.currentMissionButton.Font = new Font("Segoe UI", 8F, FontStyle.Bold);
	        this.currentMissionButton.Location = new Point(85, 12);
	        this.currentMissionButton.Size = new Size(180, 22);
	        this.currentMissionButton.TabIndex = 1;
	        this.currentMissionButton.Text = "CURRENT MISSION";
	        this.currentMissionButton.TextAlign = ContentAlignment.TopCenter;
	        this.currentMissionButton.UseCompatibleTextRendering = true;
	        this.currentMissionButton.UseVisualStyleBackColor = true;
	        this.currentMissionButton.Click += new EventHandler(this.currentMissionButton_Click);
	        //
	        // previousMissionButton
	        //
	        this.previousMissionButton.Location = new Point(37, 12);
	        this.previousMissionButton.Size = new Size(50, 22);
	        this.previousMissionButton.TabIndex = 0;
	        this.previousMissionButton.Image = (Image) _resources.GetObject("$this.leftArrow");
	        this.previousMissionButton.ImageAlign = ContentAlignment.MiddleCenter;
	        this.previousMissionButton.UseCompatibleTextRendering = true;
	        this.previousMissionButton.UseVisualStyleBackColor = true;
	        this.previousMissionButton.Click += new EventHandler(this.previousMissionButton_Click);
	        //
	        // nextMissionButton
	        //
	        this.nextMissionButton.Location = new Point(263, 12);
	        this.nextMissionButton.Size = new Size(50, 22);
	        this.nextMissionButton.TabIndex = 2;
	        this.nextMissionButton.Image = (Image) _resources.GetObject("$this.rightArrow");
	        this.nextMissionButton.ImageAlign = ContentAlignment.MiddleCenter;
	        this.nextMissionButton.UseCompatibleTextRendering = true;
	        this.nextMissionButton.UseVisualStyleBackColor = true;
	        this.nextMissionButton.Click += new EventHandler(this.nextMissionButton_Click);
	        //
	        // missionPatchImageLabel
	        //
	        this.missionPatchImageLabel.Anchor =
		        ((AnchorStyles) ((((AnchorStyles.Top | AnchorStyles.Bottom) | AnchorStyles.Left) | AnchorStyles.Right)));
	        this.missionPatchImageLabel.Location = new Point(47, 47);
	        this.missionPatchImageLabel.Size = new Size(256, 256);
	        this.missionPatchImageLabel.Click += new EventHandler(this.missionPatchImageLabel_Click);
	        //
	        // missionNameLabel
	        //
	        this.missionNameLabel.Anchor =
		        ((AnchorStyles) ((((AnchorStyles.Top | AnchorStyles.Bottom) | AnchorStyles.Left) | AnchorStyles.Right)));
	        this.missionNameLabel.AutoEllipsis = true;
	        this.missionNameLabel.Font = new Font("Segoe UI", 12F);
	        this.missionNameLabel.Location = new Point(12, 315);
	        this.missionNameLabel.Size = new Size(326, 22);
	        this.missionNameLabel.TextAlign = ContentAlignment.TopCenter;
	        //
	        // missionDetailsLabel
	        //
	        this.missionDetailsLabel.Anchor =
		        ((AnchorStyles) ((((AnchorStyles.Top | AnchorStyles.Bottom) | AnchorStyles.Left) | AnchorStyles.Right)));
	        this.missionDetailsLabel.AutoEllipsis = true;
	        this.missionDetailsLabel.Font = new Font("Segoe UI", 9F);
	        this.missionDetailsLabel.Location = new Point(12, 344);
	        this.missionDetailsLabel.Size = new Size(326, 120);
	        this.missionDetailsLabel.TextAlign = ContentAlignment.TopCenter;
	        this.missionDetailsLabel.Click += new EventHandler(this.missionDetailsLabel_Click);
	        //
	        // vehicleStatusLabel
	        //
	        this.vehicleStatusLabel.Anchor =
		        ((AnchorStyles) ((((AnchorStyles.Top | AnchorStyles.Bottom) | AnchorStyles.Left) | AnchorStyles.Right)));
	        this.vehicleStatusLabel.AutoEllipsis = true;
	        this.vehicleStatusLabel.Font = new Font("Verdana", 8F, FontStyle.Bold);
	        this.vehicleStatusLabel.Location = new Point(12, 475);
	        this.vehicleStatusLabel.Size = new Size(326, 14);
	        this.vehicleStatusLabel.TextAlign = ContentAlignment.TopCenter;
	        //
	        // missionStatusHeaderLabel
	        //
	        this.missionStatusHeaderLabel.Anchor =
		        ((AnchorStyles) ((((AnchorStyles.Top | AnchorStyles.Bottom) | AnchorStyles.Left) | AnchorStyles.Right)));
	        this.missionStatusHeaderLabel.AutoEllipsis = true;
	        this.missionStatusHeaderLabel.Font = new Font("Segoe UI", 8F, FontStyle.Underline);
	        this.missionStatusHeaderLabel.Location = new Point(12, 498);
	        this.missionStatusHeaderLabel.Size = new Size(326, 14);
	        this.missionStatusHeaderLabel.Text = "__MISSION STATUS__";
	        this.missionStatusHeaderLabel.TextAlign = ContentAlignment.TopCenter;
	        //
	        // missionStatusLabel
	        //
	        this.missionStatusLabel.Anchor =
		        ((AnchorStyles) ((((AnchorStyles.Top | AnchorStyles.Bottom) | AnchorStyles.Left) | AnchorStyles.Right)));
	        this.missionStatusLabel.AutoEllipsis = true;
	        this.missionStatusLabel.Font = new Font("Verdana", 8F, FontStyle.Bold);
	        this.missionStatusLabel.Location = new Point(12, 517);
	        this.missionStatusLabel.Size = new Size(326, 14);
	        this.missionStatusLabel.TextAlign = ContentAlignment.TopCenter;
	        //
	        // missionDataButton
	        //
	        this.missionDataButton.Font = new Font("Segoe UI", 8F, FontStyle.Bold);
	        this.missionDataButton.Location = new Point(75, 545);
	        this.missionDataButton.Size = new Size(200, 22);
	        this.missionDataButton.TabIndex = 3;
	        this.missionDataButton.Text = "MISSION DATA";
	        this.missionDataButton.TextAlign = ContentAlignment.TopCenter;
	        this.missionDataButton.UseCompatibleTextRendering = true;
	        this.missionDataButton.UseVisualStyleBackColor = true;
	        this.missionDataButton.Click += new EventHandler(this.missionDataButton_Click);
	        //
	        // aboutLaunchpadButton
	        //
	        this.aboutLaunchpadButton.Font = new Font("Segoe UI", 8F);
	        this.aboutLaunchpadButton.Location = new Point(75, 572);
	        this.aboutLaunchpadButton.Size = new Size(200, 22);
	        this.aboutLaunchpadButton.TabIndex = 4;
	        this.aboutLaunchpadButton.Text = "About Launchpad";
	        this.aboutLaunchpadButton.UseCompatibleTextRendering = true;
	        this.aboutLaunchpadButton.UseVisualStyleBackColor = true;
	        this.aboutLaunchpadButton.Click += new EventHandler(this.aboutLaunchpadButton_Click);
	        //
	        // MainForm
	        //
	        this.components = new Container();
	        this.AutoScaleMode = AutoScaleMode.Font;
	        this.BackColor = Color.White;
	        this.ClientSize = new Size(350, 612);
	        this.Controls.Add(this.currentMissionButton);
	        this.Controls.Add(this.previousMissionButton);
	        this.Controls.Add(this.nextMissionButton);
	        this.Controls.Add(this.missionPatchImageLabel);
	        this.Controls.Add(this.missionNameLabel);
	        this.Controls.Add(this.missionDetailsLabel);
	        this.Controls.Add(this.vehicleStatusLabel);
	        this.Controls.Add(this.missionStatusHeaderLabel);
	        this.Controls.Add(this.missionStatusLabel);
	        this.Controls.Add(this.missionDataButton);
	        this.Controls.Add(this.aboutLaunchpadButton);
	        this.Icon = ((Icon) (_resources.GetObject("$this.appIcon")));
	        this.MaximizeBox = false;
	        this.Text = Application.ProductName;
	        this.ResumeLayout(false);
	        this.FormBorderStyle = FormBorderStyle.FixedDialog;
	        this.currentMissionButton.Select();
        }

        #endregion

        private Button aboutLaunchpadButton;
        private Button missionDataButton;
        private Label missionStatusLabel;
        private Label missionStatusHeaderLabel;
        private Label vehicleStatusLabel;
        private Label missionDetailsLabel;
        private Label missionNameLabel;
        private Label missionPatchImageLabel;
        private Button nextMissionButton;
        private Button previousMissionButton;
        private Button currentMissionButton;
    }
}