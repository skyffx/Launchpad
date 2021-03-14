using System;
using System.Drawing;
using System.ComponentModel;
using System.Windows.Forms;

namespace Launchpad.Forms
{
    partial class MissionDataForm
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
            this.rocketDetailsButton = new Button();
            this.missionMediaButton = new Button();
            this.rocketNameHeaderLabel = new Label();
            this.rocketNameLabel = new Label();
            this.firstStageHeaderLabel = new Label();
            this.firstStageTextBox = new TextBox();
            this.secondStageHeaderLabel = new Label();
            this.secondStageTextBox = new TextBox();
            this.launchDateHeaderLabel = new Label();
            this.launchDateLabel = new Label();
            this.launchSiteHeaderLabel = new Label();
            this.launchSiteNameLabel = new Label();
            this.rocketImageLabel = new Label();
            this.mediaImageLabel = new Label();
            this.nextImageButton = new Button();
            this.previousImageButton = new Button();
            this.imageNumberLabel = new Label();
            this.saveImageButton = new Button();
            this.abbreviationsButton = new Button();
            this.SuspendLayout();
            //
            // rocketDetailsButton
            //
            this.rocketDetailsButton.Font = new Font("Tahoma", 8F);
            this.rocketDetailsButton.Location = new Point(20, 12);
            this.rocketDetailsButton.Size = new Size(202, 22);
            this.rocketDetailsButton.TabIndex = 0;
            this.rocketDetailsButton.Text = "ROCKET";
            this.rocketDetailsButton.UseCompatibleTextRendering = true;
            this.rocketDetailsButton.UseVisualStyleBackColor = true;
            this.rocketDetailsButton.Click += new EventHandler(this.rocketDetailsButton_Click);
            //
            // missionMediaButton
            //
            this.missionMediaButton.Font = new Font("Tahoma", 8F);
            this.missionMediaButton.Location = new Point(220, 12);
            this.missionMediaButton.Size = new Size(202, 22);
            this.missionMediaButton.TabIndex = 1;
            this.missionMediaButton.Text = "MEDIA";
            this.missionMediaButton.UseCompatibleTextRendering = true;
            this.missionMediaButton.UseVisualStyleBackColor = true;
            this.missionMediaButton.Click += new EventHandler(this.missionMediaButton_Click);
            //
            // rocketNameHeaderLabel
            //
            this.rocketNameHeaderLabel.Anchor =
                ((AnchorStyles) ((((AnchorStyles.Top | AnchorStyles.Bottom) | AnchorStyles.Left) | AnchorStyles.Right)));
            this.rocketNameHeaderLabel.AutoEllipsis = true;
            this.rocketNameHeaderLabel.Font = new Font("Tahoma", 8F, FontStyle.Underline);
            this.rocketNameHeaderLabel.Location = new Point(12, 46);
            this.rocketNameHeaderLabel.Size = new Size(300, 14);
            this.rocketNameHeaderLabel.Text = "__NAME__";
            this.rocketNameHeaderLabel.TextAlign = ContentAlignment.TopCenter;
            //
            // rocketNameLabel
            //
            this.rocketNameLabel.Anchor =
                ((AnchorStyles) ((((AnchorStyles.Top | AnchorStyles.Bottom) | AnchorStyles.Left) | AnchorStyles.Right)));
            this.rocketNameLabel.AutoEllipsis = true;
            this.rocketNameLabel.Font = new Font("Verdana", 8F, FontStyle.Bold);
            this.rocketNameLabel.Location = new Point(12, 65);
            this.rocketNameLabel.Size = new Size(300, 14);
            this.rocketNameLabel.TextAlign = ContentAlignment.TopCenter;
            //
            // firstStageHeaderLabel
            //
            this.firstStageHeaderLabel.Anchor =
                ((AnchorStyles) ((((AnchorStyles.Top | AnchorStyles.Bottom) | AnchorStyles.Left) | AnchorStyles.Right)));
            this.firstStageHeaderLabel.AutoEllipsis = true;
            this.firstStageHeaderLabel.Font = new Font("Tahoma", 8F, FontStyle.Underline);
            this.firstStageHeaderLabel.Location = new Point(12, 243);
            this.firstStageHeaderLabel.Size = new Size(300, 14);
            this.firstStageHeaderLabel.Text = "__FIRST STAGE__";
            this.firstStageHeaderLabel.TextAlign = ContentAlignment.TopCenter;
            //
            // firstStageTextBox
            //
            this.firstStageTextBox.Font = new Font("Verdana", 8F);
            this.firstStageTextBox.Location = new Point(12, 262);
            this.firstStageTextBox.Size = new Size(300, 107);
            this.firstStageTextBox.Multiline = true;
            this.firstStageTextBox.ScrollBars = ScrollBars.Both;
            this.firstStageTextBox.ReadOnly = true;
            this.firstStageTextBox.BorderStyle = BorderStyle.None;
            //
            // secondStageHeaderLabel
            //
            this.secondStageHeaderLabel.Anchor =
                ((AnchorStyles) ((((AnchorStyles.Top | AnchorStyles.Bottom) | AnchorStyles.Left) | AnchorStyles.Right)));
            this.secondStageHeaderLabel.AutoEllipsis = true;
            this.secondStageHeaderLabel.Font = new Font("Tahoma", 8F, FontStyle.Underline);
            this.secondStageHeaderLabel.Location = new Point(12, 90);
            this.secondStageHeaderLabel.Size = new Size(300, 14);
            this.secondStageHeaderLabel.Text = "__SECOND STAGE__";
            this.secondStageHeaderLabel.TextAlign = ContentAlignment.TopCenter;
            //
            // secondStageTextBox
            //
            this.secondStageTextBox.Font = new Font("Verdana", 8F);
            this.secondStageTextBox.Location = new Point(12, 109);
            this.secondStageTextBox.Size = new Size(300, 119);
            this.secondStageTextBox.Multiline = true;
            this.secondStageTextBox.ScrollBars = ScrollBars.Both;
            this.secondStageTextBox.ReadOnly = true;
            this.secondStageTextBox.BorderStyle = BorderStyle.None;
            //
            // launchDateHeaderLabel
            //
            this.launchDateHeaderLabel.Anchor =
                ((AnchorStyles) ((((AnchorStyles.Top | AnchorStyles.Bottom) | AnchorStyles.Left) | AnchorStyles.Right)));
            this.launchDateHeaderLabel.AutoEllipsis = true;
            this.launchDateHeaderLabel.Font = new Font("Tahoma", 8F, FontStyle.Underline);
            this.launchDateHeaderLabel.Location = new Point(12, 404);
            this.launchDateHeaderLabel.Size = new Size(300, 14);
            this.launchDateHeaderLabel.Text = "__LAUNCH DATE__";
            this.launchDateHeaderLabel.TextAlign = ContentAlignment.TopCenter;
            //
            // launchDateLabel
            //
            this.launchDateLabel.Anchor =
                ((AnchorStyles) ((((AnchorStyles.Top | AnchorStyles.Bottom) | AnchorStyles.Left) | AnchorStyles.Right)));
            this.launchDateLabel.AutoEllipsis = true;
            this.launchDateLabel.Font = new Font("Verdana", 8F, FontStyle.Bold);
            this.launchDateLabel.Location = new Point(12, 423);
            this.launchDateLabel.Size = new Size(300, 30);
            this.launchDateLabel.TextAlign = ContentAlignment.TopCenter;
            //
            // launchSiteHeaderLabel
            //
            this.launchSiteHeaderLabel.Anchor =
                ((AnchorStyles) ((((AnchorStyles.Top | AnchorStyles.Bottom) | AnchorStyles.Left) | AnchorStyles.Right)));
            this.launchSiteHeaderLabel.AutoEllipsis = true;
            this.launchSiteHeaderLabel.Font = new Font("Tahoma", 8F, FontStyle.Underline);
            this.launchSiteHeaderLabel.Location = new Point(12, 462);
            this.launchSiteHeaderLabel.Size = new Size(300, 14);
            this.launchSiteHeaderLabel.Text = "__LAUNCH SITE__";
            this.launchSiteHeaderLabel.TextAlign = ContentAlignment.TopCenter;
            //
            // launchSiteNameLabel
            //
            this.launchSiteNameLabel.Anchor =
                ((AnchorStyles) ((((AnchorStyles.Top | AnchorStyles.Bottom) | AnchorStyles.Left) | AnchorStyles.Right)));
            this.launchSiteNameLabel.AutoEllipsis = true;
            this.launchSiteNameLabel.Font = new Font("Verdana", 8F, FontStyle.Bold);
            this.launchSiteNameLabel.Location = new Point(12, 481);
            this.launchSiteNameLabel.Size = new Size(418, 14);
            this.launchSiteNameLabel.TextAlign = ContentAlignment.TopLeft;
            //
            // rocketImageLabel
            //
            this.rocketImageLabel.Anchor =
                ((AnchorStyles) ((((AnchorStyles.Top | AnchorStyles.Bottom) | AnchorStyles.Left) | AnchorStyles.Right)));
            this.rocketImageLabel.Location = new Point(324, 52);
            this.rocketImageLabel.Size = new Size(102, 395);
            //
            // mediaImageLabel
            //
            this.mediaImageLabel.Anchor =
                ((AnchorStyles) ((((AnchorStyles.Top | AnchorStyles.Bottom) | AnchorStyles.Left) | AnchorStyles.Right)));
            this.mediaImageLabel.Location = new Point(12, 45);
            this.mediaImageLabel.Size = new Size(418, 419);
            //
            // nextImageButton
            //
            this.nextImageButton.Location = new Point(255, 476);
            this.nextImageButton.Size = new Size(42, 22);
            this.nextImageButton.TabIndex = 2;
            this.nextImageButton.Image = (Image) _resources.GetObject("$this.rightArrow");
            this.nextImageButton.ImageAlign = ContentAlignment.MiddleCenter;
            this.nextImageButton.UseCompatibleTextRendering = true;
            this.nextImageButton.UseVisualStyleBackColor = true;
            this.nextImageButton.Click += new EventHandler(this.nextImageButton_Click);
            //
            // previousImageButton
            //
            this.previousImageButton.Location = new Point(145, 476);
            this.previousImageButton.Size = new Size(42, 22);
            this.previousImageButton.TabIndex = 3;
            this.previousImageButton.Image = (Image) _resources.GetObject("$this.leftArrow");
            this.previousImageButton.ImageAlign = ContentAlignment.MiddleCenter;
            this.previousImageButton.UseCompatibleTextRendering = true;
            this.previousImageButton.UseVisualStyleBackColor = true;
            this.previousImageButton.Click += new EventHandler(this.previousImageButton_Click);
            //
            // imageNumberLabel
            //
            this.imageNumberLabel.Anchor =
                ((AnchorStyles) ((((AnchorStyles.Top | AnchorStyles.Bottom) | AnchorStyles.Left) | AnchorStyles.Right)));
            this.imageNumberLabel.Location = new Point(197, 480);
            this.imageNumberLabel.Size = new Size(50, 14);
            this.imageNumberLabel.Font = new Font("Verdana", 8F, FontStyle.Bold);
            this.imageNumberLabel.TextAlign = ContentAlignment.MiddleCenter;
            //
            // saveImageButton
            //
            this.saveImageButton.Location = new Point(398, 476);
            this.saveImageButton.Size = new Size(30, 22);
            this.saveImageButton.TabIndex = 4;
            this.saveImageButton.Image = (Image) _resources.GetObject("$this.saveImage");
            this.saveImageButton.ImageAlign = ContentAlignment.MiddleCenter;
            this.saveImageButton.UseCompatibleTextRendering = true;
            this.saveImageButton.UseVisualStyleBackColor = true;
            this.saveImageButton.Click += new EventHandler(this.saveImageButton_Click);
            //
            // abbreviationsButton
            //
            this.abbreviationsButton.Font = new Font("Verdana", 9F, FontStyle.Bold);
            this.abbreviationsButton.Location = new Point(282, 369);
            this.abbreviationsButton.Size = new Size(30, 22);
            this.abbreviationsButton.Text = "i";
            this.abbreviationsButton.UseCompatibleTextRendering = true;
            this.abbreviationsButton.UseVisualStyleBackColor = true;
            this.abbreviationsButton.TextAlign = ContentAlignment.TopCenter;
            this.abbreviationsButton.Click += new EventHandler(this.abbreviationsButton_Click);
            //
            // MissionDataForm
            //
            this.components = new Container();
            this.AutoScaleMode = AutoScaleMode.Font;
            this.BackColor = Color.White;
            this.ClientSize = new Size(442, 510);
            this.Controls.Add(this.rocketDetailsButton);
            this.Controls.Add(this.missionMediaButton);
            this.Controls.Add(this.rocketNameHeaderLabel);
            this.Controls.Add(this.rocketNameLabel);
            this.Controls.Add(this.firstStageHeaderLabel);
            this.Controls.Add(this.firstStageTextBox);
            this.Controls.Add(this.secondStageHeaderLabel);
            this.Controls.Add(this.secondStageTextBox);
            this.Controls.Add(this.launchDateHeaderLabel);
            this.Controls.Add(this.launchDateLabel);
            this.Controls.Add(this.launchSiteHeaderLabel);
            this.Controls.Add(this.launchSiteNameLabel);
            this.Controls.Add(this.rocketImageLabel);
            this.Controls.Add(this.mediaImageLabel);
            this.Controls.Add(this.nextImageButton);
            this.Controls.Add(this.previousImageButton);
            this.Controls.Add(this.imageNumberLabel);
            this.Controls.Add(this.saveImageButton);
            this.Controls.Add(this.abbreviationsButton);
            this.Text = $"—{Application.ProductName}—";
            this.MinimizeBox = false;
            this.MaximizeBox = false;
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.ResumeLayout(false);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
        }

        #endregion
        
        private Button rocketDetailsButton;
        private Button missionMediaButton;
        private Label rocketNameHeaderLabel;
        private Label rocketNameLabel;
        private Label firstStageHeaderLabel;
        private TextBox firstStageTextBox;
        private Label secondStageHeaderLabel;
        private TextBox secondStageTextBox;
        private Label launchDateHeaderLabel;
        private Label launchDateLabel;
        private Label launchSiteHeaderLabel;
        private Label launchSiteNameLabel;
        private Label rocketImageLabel;
        private Label mediaImageLabel;
        private Button nextImageButton;
        private Button previousImageButton;
        private Label imageNumberLabel;
        private Button saveImageButton;
        private Button abbreviationsButton;
    }
}