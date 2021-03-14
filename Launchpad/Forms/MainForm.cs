using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using Launchpad.Utils;
using Newtonsoft.Json.Linq;
using Oddity.Models.Launches;
using Oddity.Models.Launchpads;
using Oddity.Models.Rockets;
using RestSharp;

namespace Launchpad.Forms
{
    public partial class MainForm : Form
    {
        public const string ApiUrl = "https://api.spacexdata.com/v4";
        private readonly List<LaunchInfo> _launchesData;
        private readonly int _nextFlightNumber;
        private string _missionPatchLink;
        private int _indexOfMission;
        private string _missionName;
        private RocketInfo _rocketInfo;
        private LaunchpadInfo _launchpadInfo;

        private void EnableControls(bool state)
        {
            previousMissionButton.Enabled = state;
            currentMissionButton.Enabled = state;
            nextMissionButton.Enabled = state;
            missionDataButton.Enabled = state;
            missionNameLabel.Focus();
        }

        private void MissionData(int missionNumber)
        {
            EnableControls(false);

            _missionPatchLink = _launchesData[missionNumber].Links.Patch.Large;
            var missionPatchImageLink = _launchesData[missionNumber].Links.Patch.Small;
            _missionName = _launchesData[missionNumber].Name;
            var missionDetails = _launchesData[missionNumber].Details;
            var vehiclesStatus = _launchesData[missionNumber].Upcoming;
            var launchStatus = _launchesData[missionNumber].Success;

            if (string.IsNullOrWhiteSpace(missionPatchImageLink))
            {
                missionPatchImageLabel.Image = (Image) _resources.GetObject("$this.spacexLogo");
            }
            else
            {
                missionPatchImageLabel.Image = Task.Run(() => HttpUtil.StreamUrlToImageAndResize(missionPatchImageLink, 256, 256)).Result;
            }

            missionNameLabel.Text = string.IsNullOrWhiteSpace(_missionName) ? "— No mission name —" : _missionName;
            missionDetailsLabel.Text = string.IsNullOrWhiteSpace(missionDetails) ? "— No mission details —" : missionDetails;
            
            _rocketInfo = new RestClient($"{ApiUrl}/rockets/{_launchesData[missionNumber].RocketId}").Get<RocketInfo>(new RestRequest("/")).Data;
            _launchpadInfo = JObject.Parse(new RestClient($"{ApiUrl}/launchpads/{_launchesData[missionNumber].LaunchpadId}")
                .Get(new RestRequest("/")).Content).ToObject<LaunchpadInfo>();
            vehicleStatusLabel.Text = vehiclesStatus == true ? $"{_rocketInfo.Name} will be launched from {_launchpadInfo.Name}" : $"{_rocketInfo.Name} launched from {_launchpadInfo.Name}";

            switch (launchStatus)
            {
                case true:
                    missionStatusLabel.ForeColor = Color.Green;
                    missionStatusLabel.Text = "SUCCESSFUL";
                    break;
                case false:
                    missionStatusLabel.ForeColor = Color.Red;
                    missionStatusLabel.Text = "FAILED";
                    break;
                default:
                    missionStatusLabel.ForeColor = Color.Blue;
                    missionStatusLabel.Text = $"Will be launched in {_launchesData[missionNumber].DateLocal}";
                    break;
            }            

            EnableControls(true);
        }

        private void previousMissionButton_Click(object sender, EventArgs e)
        {
            if (_indexOfMission == 0)
            {
                currentMissionButton.Select();
            }
            else
            {
                MissionData(--_indexOfMission);
            }
        }

        private void currentMissionButton_Click(object sender, EventArgs e)
        {
            _indexOfMission = _nextFlightNumber;
            MissionData(_indexOfMission);
        }

        private void nextMissionButton_Click(object sender, EventArgs e)
        {
            if (_indexOfMission == (_launchesData.Count - 1))
            {
                MessageBox.Show("Next mission has not been planned, yet ;)", $"—{Application.ProductName}—",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                currentMissionButton.Select();
            }
            else
            {
                MissionData(++_indexOfMission);
            }
        }

        private void missionPatchImageLabel_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(_missionPatchLink))
            {
                missionPatchImageLabel.Enabled = false;
                Enabled = false;
                var missionPathForm = new MissionPatchForm(_missionName, _missionPatchLink, 512, 512)
                {
                    StartPosition = FormStartPosition.CenterParent
                };
                missionPathForm.ShowDialog(this);
                Enabled = true;
                missionPatchImageLabel.Enabled = true;
                missionPatchImageLabel.Select();
            }
            else
            {
                MessageBox.Show("Mission patch cannot be enlarged!", $"—{Application.ProductName}—",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void missionDetailsLabel_Click(object sender, EventArgs e)
        {
            MessageBox.Show(missionDetailsLabel.Text, $"—{Application.ProductName}—", MessageBoxButtons.OK);
        }

        private void missionDataButton_Click(object sender, EventArgs e)
        {
            missionDataButton.Enabled = false;
            Enabled = false;
            
            if (_launchesData[_indexOfMission].PayloadsId.Count > 0)
            {
                var missionDataForm = new MissionDataForm(_launchesData[_indexOfMission], _rocketInfo, _launchpadInfo)
                {
                    StartPosition = FormStartPosition.CenterParent
                };
                missionDataForm.ShowDialog(this);
                missionDataButton.Select();
            }
            else
            {
                MessageBox.Show("No mission data available.", $"—{Application.ProductName}—", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                currentMissionButton.Select();
            }
            
            Enabled = true;
            missionDataButton.Enabled = true;
        }
        
        private void aboutLaunchpadButton_Click(object sender, EventArgs e)
        {
            var logoLabel = new Label
            {
                Location = new Point(5, 40),
                Size = new Size(376, 128),
                ImageAlign = ContentAlignment.MiddleCenter,
                Image = ImageUtil.ResizeImageAndKeepRatio((Image) _resources.GetObject("$this.appLogo"), 128, 128)
            };
            
            var infoLabel = new Label
            {
                Location = new Point(5, 200),
                Size = new Size(376, 230),
                Font = new Font("Segoe UI", 8F, FontStyle.Regular),
                TextAlign = ContentAlignment.TopCenter,
                Text = $"{Application.ProductName} {Application.ProductVersion.Remove(3)}\r\n\r\n" +
                       "Made with love for all by skyffx\r\n" +
                       "github.com/skyffx/Launchpad\r\n\r\n" +
                       "This amazing app is based on:\r\n" +
                       "SpaceX-API — github.com/r-spacex/SpaceX-API\r\n" +
                       "Oddity — github.com/Tearth/Oddity\r\n" +
                       "App icon from flaticon.com\r\n\r\n" +
                       "SpaceX is the rightful owner of presented data.\r\n\r\n\r\n" +
                       "—2021—"
            };
            
            var about = new Form
            {
                Controls = { logoLabel, infoLabel },
                BackColor = Color.White,
                MaximizeBox = false,
                MinimizeBox = false,
                ShowIcon = false,
                ShowInTaskbar = false,
                Text = $"—{Application.ProductName}—",
                FormBorderStyle = FormBorderStyle.FixedDialog,
                Size = new Size(400, 465),
                StartPosition = FormStartPosition.CenterParent
            };
            
            Enabled = false;
            about.ShowDialog(this);
            Enabled = true;
        }
        
        private void AppTips()
        {
            var toolTip = new ToolTip {AutoPopDelay = 2000, InitialDelay = 100, ReshowDelay = 100, ShowAlways = true};
            toolTip.SetToolTip(previousMissionButton, "Go to previous mission");
            toolTip.SetToolTip(currentMissionButton, "Display information about current mission");
            toolTip.SetToolTip(nextMissionButton, "Go to next mission");
            toolTip.SetToolTip(missionPatchImageLabel, "Click to enlarge");
            toolTip.SetToolTip(missionDetailsLabel, "Click to show");
        }
        
        public MainForm(List<LaunchInfo> launchesData, LaunchInfo nextLaunch)
        {
            InitializeComponent();
            CenterToScreen();
            AppTips();
            
            _nextFlightNumber = int.Parse(nextLaunch.FlightNumber.ToString());
            _launchesData = launchesData;
            
            _indexOfMission = _launchesData.FindIndex(mission
                => mission.FlightNumber != null && mission.FlightNumber.Value.ToString()
                    .Contains(_nextFlightNumber.ToString()));
            _nextFlightNumber = _indexOfMission;
            
            MissionData(_nextFlightNumber);
        }
    }
}