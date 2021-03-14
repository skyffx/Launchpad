using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using Launchpad.Utils;

namespace Launchpad.Forms
{
    public partial class MissionPatchForm : Form
    {
        private readonly Image _image;
        private readonly string _missionName;
        
        private void missionPatchImageLabel_Click(object sender, EventArgs e)
        {
            Close();
        }
        
        private void saveMissionPatchImageButton_Click(object sender, EventArgs e)
        {
            var saveDialog = new SaveFileDialog
            {
                RestoreDirectory = true,
                DefaultExt = "png",
                Filter = "PNG|*.png",
                FileName = Regex.Replace($"{_missionName}-Mission Patch", @"\s+", "_")
            };
            if (saveDialog.ShowDialog() == DialogResult.OK)
            {
                new Bitmap(_image).Save(saveDialog.FileName, ImageFormat.Png);
            }
        }
        
        private void AppTips()
        {
            var toolTip = new ToolTip {AutoPopDelay = 2000, InitialDelay = 100, ReshowDelay = 100, ShowAlways = true};
            toolTip.SetToolTip(missionPatchImageLabel, "Click to close");
            toolTip.SetToolTip(saveMissionPatchImageButton, "Save it");
        }
        
        public MissionPatchForm(string missionName, string missionPatchLink, int imageWidth, int imageHeight)
        {
            InitializeComponent();
            AppTips();
            
            Controls.SetChildIndex(saveMissionPatchImageButton, 0);
            _image = Task.Run(() =>
                HttpUtil.StreamUrlToImage(missionPatchLink)).Result;
            missionPatchImageLabel.Image = ImageUtil.ResizeImageAndKeepRatio(_image, imageWidth, imageHeight);
            _missionName = missionName;
        }
    }
}