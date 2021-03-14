using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using Launchpad.Utils;
using Newtonsoft.Json.Linq;
using Oddity.Models.Cores;
using Oddity.Models.Launches;
using Oddity.Models.Launchpads;
using Oddity.Models.Payloads;
using Oddity.Models.Rockets;
using RestSharp;

namespace Launchpad.Forms
{
    public partial class MissionDataForm : Form
    {
        private readonly LaunchInfo _missionData;
        private readonly RocketInfo _rocketInfo;
        private readonly LaunchpadInfo _launchpadInfo;
        private PayloadInfo _payloadInfo;
        private int _imageNumber;
        private readonly int _imagesInMedia;
        private Image _image;

        private void ShowRocketImage(string rocketObject)
        {
            rocketImageLabel.Image = ImageUtil.ResizeImageAndKeepRatio((Image) _resources.GetObject(rocketObject),102, 395);
        }

        private void ShowRocketData()
        {
            rocketNameLabel.Text = _rocketInfo.Name;
            launchDateLabel.Text = $"{_missionData.DateLocal} Local\n{_missionData.DateUtc} UTC\n";
            launchSiteNameLabel.Text = _launchpadInfo.FullName;

            _payloadInfo = JObject.Parse(new RestClient($"{MainForm.ApiUrl}/payloads/{_missionData.PayloadsId[0]}")
                .Get(new RestRequest("/")).Content).ToObject<PayloadInfo>();

            switch (_rocketInfo.Name)
            {
                case "Falcon Heavy" when _payloadInfo.Type == "Satellite":
                    ShowRocketImage("$this.falconHeavyPayloadImage");
                    break;
                case "Falcon 9" when _payloadInfo.Type == "Satellite":
                    ShowRocketImage("$this.falcon9PayloadImage");
                    break;
                case "Falcon 9" when _payloadInfo.Type == "Crew Dragon":
                    ShowRocketImage("$this.falcon9CrewDragonImage");
                    break;
                case "Falcon 9" when _payloadInfo.Type.Contains("Dragon 1.")
                                     || _payloadInfo.Type.Contains("Dragon 2.")
                                     || _payloadInfo.Type.Contains("Dragon 3.")
                                     || _payloadInfo.Type.Contains("Dragon 4."):
                    ShowRocketImage("$this.falcon9DragonImage");
                    break;
            }

            var cores = _missionData.Cores;
            var coresData = new List<string>();
            if (cores.Count > 0)
            {
                var coreNumber = 1;
                foreach (var core in cores)
                {
                    coresData.Add($" — Core: {coreNumber} — ");
                    if (!string.IsNullOrEmpty(core.CoreId))
                    {
                        var coreInfo = JObject.Parse(new RestClient($"{MainForm.ApiUrl}/cores/{core.CoreId}")
                            .Get(new RestRequest("/")).Content).ToObject<CoreInfo>();
                        coresData.Add(coreInfo.Serial != null ? $"Core serial: {coreInfo.Serial}" : "Core serial: -");
                        coresData.Add(coreInfo.Block != null ? $"Block: {coreInfo.Block}" : "Block: -");
                        coresData.Add($"Launches: {coreInfo.Launches.Count}");
                        coresData.Add(coreInfo.ReuseCount != null ? $"Reuses: {coreInfo.ReuseCount}" : "Reuses: -");
                        coresData.Add($"Status: {coreInfo.Status}");
                        coresData.Add(coreInfo.AsdsLandings != null ? $"ASDS Landings: {coreInfo.AsdsLandings}" : "ASDS Landings: -");
                        coresData.Add(coreInfo.RtlsLandings != null ? $"RTLS Landings: {coreInfo.RtlsLandings}" : "RTLS Landings: -");
                    }
                    else
                    {
                        coresData.Add("!No details!");
                    }
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
            
            var payloads = _missionData.PayloadsId;
            var payloadsData = new List<string>();
            if (payloads.Count > 0)
            {
                var payloadNumber = 1;
                foreach (var payload in payloads)
                {
                    _payloadInfo = JObject.Parse(new RestClient($"{MainForm.ApiUrl}/payloads/{payload}")
                        .Get(new RestRequest("/")).Content).ToObject<PayloadInfo>();

                    payloadsData.Add($" — Payload: {payloadNumber} — ");
                    if (!string.IsNullOrEmpty(_payloadInfo.Id))
                    {
                        payloadsData.Add(_payloadInfo.Name != null ? $"Name: {_payloadInfo.Name}" : "Name: -");
                        payloadsData.Add(_payloadInfo.Customers != null ? $"Customers: {string.Join(", ", _payloadInfo.Customers)}" : "Customers: -");
                        payloadsData.Add(_payloadInfo.Nationalities != null ? $"Nationalities: {string.Join(", ", _payloadInfo.Nationalities)}" : "Nationalities: -");
                        payloadsData.Add(_payloadInfo.Manufacturers != null ? $"Manufacturers: {string.Join(", ", _payloadInfo.Manufacturers)}" : "Manufacturers: -");
                        payloadsData.Add(_payloadInfo.MassKilograms != null ? $"Mass (kg): {_payloadInfo.MassKilograms}" : "Mass (kg): -");
                        payloadsData.Add(_payloadInfo.MassPounds != null ? $"Mass (lbs): {_payloadInfo.MassPounds}" : "Mass (lbs): -");
                        payloadsData.Add(_payloadInfo.Type != null ? $"Type: {_payloadInfo.Type}" : "Type: -");
                        payloadsData.Add(_payloadInfo.Orbit != null ? $"Orbit: {_payloadInfo.Orbit}" : "Orbit: -");
                    }
                    else
                    {
                        coresData.Add("!No details!");
                    }
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
                    HttpUtil.StreamUrlToImage(_missionData.Links.Flickr.Original[imageNumber])).Result;
                mediaImageLabel.Image = ImageUtil.ResizeImageAndKeepRatio(_image, 418, 419);
            }
            else
            {
                MessageBox.Show("Images from mission are not available yet!",
                    $"—{Application.ProductName}—", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                FileName = Regex.Replace($"{_missionData.Name}-{_imageNumber + 1}", @"\s+", "_")
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
                            "ASDS - Autonomous spaceport drone ship (landing type)\n" +
                            "RTLS - Return to launch landing site (landing type)\n" +
                            "OCISLY - Of Course I Still Love You (landing type)\n", 
                $"—{Application.ProductName}—", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void AppTips()
        {
            var toolTip = new ToolTip {AutoPopDelay = 2000, InitialDelay = 100, ReshowDelay = 100, ShowAlways = true};
            toolTip.SetToolTip(abbreviationsButton, "Show common space abbreviations");
        }
        
        public MissionDataForm(LaunchInfo launchInfo, RocketInfo rocketInfo, LaunchpadInfo launchpadInfo)
        {
            _missionData = launchInfo;
            _rocketInfo = rocketInfo;
            _launchpadInfo = launchpadInfo;
            _imagesInMedia = _missionData.Links.Flickr.Original.Count;
            
            InitializeComponent();
            CenterToScreen();
            rocketDetailsButton.Select();
            ShowMediaControls(false);
            ShowRocketData();
            AppTips();
        }
    }
}