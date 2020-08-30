using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using Launchpad.Util;
using Oddity.API.Models.Launch;

namespace Launchpad.Forms
{
    public partial class MissionData : Form
    {
        private readonly LaunchInfo _missionData;
        private int _imageNumber;
        private readonly int _imagesInMedia;
        private Image _image;
        
        //

        private void ShowRocketImage(string rocketObject)
        {
            rocketImageLabel.Image = ImageUtil.ResizeImageAndKeepRatio((Image) _resources.GetObject(rocketObject),102, 395);
        }

        private void ShowRocketData()
        {
            rocketNameLabel.Text = _missionData.Rocket.RocketName;
            launchDateLabel.Text = $"{_missionData.LaunchDateLocal} Local\n{_missionData.LaunchDateUtc} UTC\n";
            launchSiteNameLabel.Text = _missionData.LaunchSite.SiteLongName;

            switch (_missionData.Rocket.RocketName)
            {
                case "Falcon Heavy" when _missionData.Rocket.SecondStage.Payloads[0].PayloadType == "Satellite":
                    ShowRocketImage("$this.falconHeavyPayloadImage");
                    break;
                case "Falcon 9" when _missionData.Rocket.SecondStage.Payloads[0].PayloadType == "Satellite":
                    ShowRocketImage("$this.falcon9PayloadImage");
                    break;
                case "Falcon 9" when _missionData.Rocket.SecondStage.Payloads[0].PayloadType == "Crew Dragon":
                    ShowRocketImage("$this.falcon9CrewDragonImage");
                    break;
                case "Falcon 9" when _missionData.Rocket.SecondStage.Payloads[0].PayloadType.Contains("Dragon 1."):
                    ShowRocketImage("$this.falcon9DragonImage");
                    break;
            }

            //
            
            var firstStage = _missionData.Rocket.FirstStage.Cores;
            var coresData = new List<string>();
            if (firstStage.Count > 0)
            {
                var coreNumber = 1;
                foreach (var core in firstStage)
                {
                    coresData.Add($" — Core: {coreNumber} — ");
                    coresData.Add(core.CoreSerial != null ? $"Core serial: {core.CoreSerial}" : "Core serial: -");
                    coresData.Add(core.Flight != null ? $"Flight: {core.Flight}" : "Flight: -");
                    coresData.Add(core.Block != null ? $"Block: {core.Block}" : "Block: -");
                    coresData.Add(core.Reused != null ? $"Reused: {core.Reused}" : "Reused: -");
                    coresData.Add(core.LandSuccess == null ? "Land success: -" : $"Land success: {core.LandSuccess}");
                    coresData.Add(core.LandingType != null ? $"Landing type: {core.LandingType}" : "Landing type: -");
                    coresData.Add(core.LandingVehicle != null ? $"Landing vehicle: {core.LandingVehicle}" : "Landing vehicle: -");
                    coresData.Add(string.Empty);
                    coreNumber++;
                }

                coresData.RemoveAt(coresData.Count - 1);
                firstStageTextBox.Text = string.Join(Environment.NewLine, coresData);
            }
            else
            {
                firstStageTextBox.TextAlign = HorizontalAlignment.Center;
                firstStageTextBox.Text = " — No data — ";
            }

            //

            var secondStage = _missionData.Rocket.SecondStage.Payloads;
            var payloadsData = new List<string>();
            if (secondStage.Count > 0)
            {
                var block = _missionData.Rocket.SecondStage.Block;
                payloadsData.Add(block != null ? $"Block: {block}" : "Block: -");
                payloadsData.Add(string.Empty);
                var payloadNumber = 1;
                foreach (var payload in secondStage)
                {
                    payloadsData.Add($" — Payload: {payloadNumber} — ");
                    payloadsData.Add(payload.PayloadId != null ? $"Name: {payload.PayloadId}" : "Name: -");
                    payloadsData.Add(payload.Customers != null ? $"Customer(s): {string.Join(", ", payload.Customers)}" : "Customer(s): -");
                    payloadsData.Add(payload.Nationality != null ? $"Nationality: {payload.Nationality}" : "Nationality: -");
                    payloadsData.Add(payload.Manufacturer != null ? $"Manufacturer: {payload.Manufacturer}" : "Manufacturer: -");
                    payloadsData.Add(payload.PayloadType != null ? $"Type: {payload.PayloadType}" : "Type: -");
                    payloadsData.Add(payload.Orbit != null ? $"Orbit: {payload.Orbit}" : "Orbit: -");
                    payloadsData.Add(string.Empty);
                    payloadNumber++;
                }

                payloadsData.RemoveAt(payloadsData.Count - 1);
                secondStageTextBox.Text = string.Join(Environment.NewLine, payloadsData);
            }
            else
            {
                secondStageTextBox.TextAlign = HorizontalAlignment.Center;
                secondStageTextBox.Text = " — No data — ";
            }
        }

        private void ShowMediaData(int imageNumber)
        {
            EnableMediaControls(false);

            if (_imagesInMedia != 0)
            {
                ShowMediaControls(true);
                imageNumberLabel.Text = $"{imageNumber + 1}/{_imagesInMedia}";
                _image = Task.Run(() =>
                    HttpUtil.StreamUrlToImage(_missionData.Links.FlickrImages[imageNumber])).Result;
                mediaImageLabel.Image = ImageUtil.ResizeImageAndKeepRatio(_image, 418, 419);
            }
            else
            {
                MessageBox.Show("Images are not available!",
                    $"—{Application.ProductName}—", MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                rocketDetailsButton_Click(null, null);
                rocketDetailsButton.Select();
            }

            EnableMediaControls(true);
        }

        private void ShowRocketControls(bool state)
        {
            rocketNameHeaderLabel.Visible = state;
            rocketNameLabel.Visible = state;
            firstStageHeaderLabel.Visible = state;
            firstStageTextBox.Visible = state;
            secondStageHeaderLabel.Visible = state;
            secondStageTextBox.Visible = state;
            launchDateHeaderLabel.Visible = state;
            launchDateLabel.Visible = state;
            launchSiteHeaderLabel.Visible = state;
            launchSiteNameLabel.Visible = state;
            rocketImageLabel.Visible = state;
            abbreviationsButton.Visible = state;
        }

        private void ShowMediaControls(bool state)
        {
            mediaImageLabel.Visible = state;
            nextImageButton.Visible = state;
            previousImageButton.Visible = state;
            imageNumberLabel.Visible = state;
            saveImageButton.Visible = state;
        }
        
        private void EnableMediaControls(bool state)
        {
            nextImageButton.Enabled = state;
            previousImageButton.Enabled = state;
            saveImageButton.Enabled = state;
            mediaImageLabel.Focus();
        }
        
        //

        private void rocketDetailsButton_Click(object sender, EventArgs e)
        {
            ShowMediaControls(false);
            ShowRocketControls(true);
            ShowRocketData();
        }

        private void missionMediaButton_Click(object sender, EventArgs e)
        {
            ShowRocketControls(false);
            ShowMediaData(_imageNumber);
        }

        private void nextImageButton_Click(object sender, EventArgs e)
        {
            if (_imageNumber == (_imagesInMedia - 1))
            {
                previousImageButton.Focus();
            }
            else
            {
                ShowMediaData(++_imageNumber);
            }
        }

        private void previousImageButton_Click(object sender, EventArgs e)
        {
            if (_imageNumber == 0)
            {
                nextImageButton.Focus();
            }
            else
            {
                ShowMediaData(--_imageNumber);
            }
        }

        private void saveImageButton_Click(object sender, EventArgs e)
        {
            var saveDialog = new SaveFileDialog
            {
                RestoreDirectory = true,
                DefaultExt = "jpg",
                Filter = "JPEG|*.jpg",
                FileName = Regex.Replace($"{_missionData.MissionName}-{_imageNumber + 1}", @"\s+", "_")
            };
            if (saveDialog.ShowDialog() == DialogResult.OK)
            {
                new Bitmap(_image).Save(saveDialog.FileName, ImageFormat.Jpeg);
            }
        }

        private void abbreviationsButton_Click(object sender, EventArgs e)
        {
            MessageBox.Show("LEO - Low Earth Orbit\n" +
                            "GTO - Geostationary Transfer Orbit\n" +
                            "CRS - Commercial Resupply Mission\n" +
                            "ISS - International Space Station\n" +
                            "VLEO - Very low Earth orbit\n" +
                            "MEO - Medium Earth orbit\n" +
                            "SSO - Sun-synchronous orbit\n" +
                            "ASDS - Autonomous spaceport drone ship\n" +
                            "OCISLY - Of Course I Still Love You\n", 
                $"—{Application.ProductName}—", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        
        //
        
        private void ShowTips()
        {
            var toolTip = new ToolTip {AutoPopDelay = 2000, InitialDelay = 100, ReshowDelay = 100, ShowAlways = true};
            toolTip.SetToolTip(abbreviationsButton, "Show common space abbreviations");
        }
        
        public MissionData(LaunchInfo launchInfo)
        {
            _missionData = launchInfo;
            _imagesInMedia = _missionData.Links.FlickrImages.Count;
            InitializeComponent();
            CenterToScreen();
            rocketDetailsButton.Select();
            ShowMediaControls(false);
            ShowRocketData();
            ShowTips();
        }
    }
}