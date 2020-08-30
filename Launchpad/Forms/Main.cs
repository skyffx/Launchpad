using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using Launchpad.Util;
using Oddity;
using Oddity.API.Models.Launch;

namespace Launchpad.Forms
{
    public partial class Main : Form
    {
        private readonly OddityCore _oddity = new OddityCore();
        private readonly int _currentFlightNumber;
        private string _missionPatchLink;
        private readonly List<LaunchInfo> _missionsData;
        private int _indexOfMission;
        private string _missionName;

        //

        private void EnableControls(bool state)
        {
            previousMissionButton.Enabled = state;
            currentMissionButton.Enabled = state;
            nextMissionButton.Enabled = state;
            missionDataButton.Enabled = state;
            missionNameLabel.Focus();
        }

        private static void ShowMissionDetails(string content)
        {
            MessageBox.Show(content, $"—{Application.ProductName}—", MessageBoxButtons.OK);
        }

        private int GetCurrentMissionFlightNumber()
        {
            return Convert.ToInt32(_oddity.Launches.GetNext().Execute().FlightNumber);
        }
        
        private void MissionData(int missionNumber)
        {
            EnableControls(false);

            _missionPatchLink = _missionsData[missionNumber].Links.MissionPatch;
            var missionPatchImage = _missionsData[missionNumber].Links.MissionPatchSmall;
            _missionName = _missionsData[missionNumber].MissionName;
            var missionDetails = _missionsData[missionNumber].Details;
            var vehiclesStatus = _missionsData[missionNumber].Upcoming;
            var launchStatus = _missionsData[missionNumber].LaunchSuccess;

            if (string.IsNullOrWhiteSpace(missionPatchImage))
            {
                missionPatchImageLabel.Image = (Image) _resources.GetObject("$this.spacexLogo");
            }
            else
            {
                missionPatchImageLabel.Image = Task.Run(() => HttpUtil.StreamUrlToImageAndResize(missionPatchImage, 256, 256)).Result;
            }

            missionNameLabel.Text = string.IsNullOrWhiteSpace(_missionName) ? "— No mission name —" : _missionName;

            missionDetailsLabel.Text =
                string.IsNullOrWhiteSpace(missionDetails) ? "— No mission details —" : missionDetails;

            vehicleStatusLabel.Text = vehiclesStatus == true
                ? $"{_missionsData[missionNumber].Rocket.RocketName} will be launched from {_missionsData[missionNumber].LaunchSite.SiteName}"
                : $"{_missionsData[missionNumber].Rocket.RocketName} launched from {_missionsData[missionNumber].LaunchSite.SiteName}";

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
                    missionStatusLabel.Text = $"Will be launched in {_missionsData[missionNumber].LaunchDateLocal}";
                    break;
            }            

            EnableControls(true);
        }
        
        //

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
            _indexOfMission = _currentFlightNumber;
            MissionData(_indexOfMission);
        }

        private void nextMissionButton_Click(object sender, EventArgs e)
        {
            if (_indexOfMission == (_missionsData.Count - 1))
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
                new MissionPatch(_missionName, _missionPatchLink, 512, 512).ShowDialog();
                missionPatchImageLabel.Enabled = true;
            }
            else
            {
                MessageBox.Show("Mission patch is not available to enlarge!", $"—{Application.ProductName}—",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void missionDetailsLabel_Click(object sender, EventArgs e)
        {
            ShowMissionDetails(missionDetailsLabel.Text);
        }

        private void missionDataButton_Click(object sender, EventArgs e)
        {
            missionDataButton.Enabled = false;
            new MissionData(_missionsData[_indexOfMission]).ShowDialog();
            missionDataButton.Enabled = true;
            missionDataButton.Select();
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
                       "(Wojciech Piekielniak, wojciech.piekielniak@protonmail.com)\r\n\r\n" +
                       "github.com/skyffx/Launchpad\r\n\r\n" +
                       "This amazing app is based on:\r\n" +
                       "SpaceX-API — github.com/r-spacex/SpaceX-API\r\n" +
                       "Oddity — github.com/Tearth/Oddity\r\n" +
                       "App icon from flaticon.com\r\n\r\n" +
                       "SpaceX is the rightful owner of presented data.\r\n\r\n\r\n" +
                       "—2020—"
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
                Size = new Size(400, 500),
            };
            
            about.ShowDialog();
        }

        //
        
        private void ShowTips()
        {
            var toolTip = new ToolTip {AutoPopDelay = 2000, InitialDelay = 100, ReshowDelay = 100, ShowAlways = true};
            toolTip.SetToolTip(previousMissionButton, "Go to previous mission");
            toolTip.SetToolTip(currentMissionButton, "Display information about current mission");
            toolTip.SetToolTip(nextMissionButton, "Go to next mission");
            toolTip.SetToolTip(missionPatchImageLabel, "Click to enlarge");
            toolTip.SetToolTip(missionDetailsLabel, "Click to show");
        }
        
        public Main(List<LaunchInfo> launchInfo)
        {
            InitializeComponent();
            CenterToScreen();
            ShowTips();

            _currentFlightNumber = GetCurrentMissionFlightNumber();
            _missionsData = launchInfo;
            _indexOfMission = _missionsData.FindIndex(mission
                => mission.FlightNumber != null && mission.FlightNumber.Value.ToString()
                    .Contains(_currentFlightNumber.ToString()));
            _currentFlightNumber = _indexOfMission;
            MissionData(_indexOfMission);
        }
    }
}