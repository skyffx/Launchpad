﻿using System;
using System.Drawing;
using System.Net;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Launchpad.Util;
using Oddity;

namespace Launchpad.Forms
{
    public partial class Initial : Form
    {
        private static async Task GetAllMissions()
        {
            await Task.Run(() =>
            {
                var response = HttpUtil.Get("https://api.spacexdata.com/v3/").Result;
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    try
                    {
                        var missionsData = new OddityCore().Launches.GetAll().Execute();
                        var appFormThread = new Thread(() => new Main(missionsData).ShowDialog());
                        appFormThread.SetApartmentState(ApartmentState.STA);
                        appFormThread.Start();
                        Application.Exit();
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("SpaceX, we have a problem!\n\nhttps://api.spacexdata.com/v3/launches\n" +
                                        "=> Request is not completed!\n\nPlease to run app again.",
                            $"—{Application.ProductName}—", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Application.Exit();
                    }
                }
                else
                {
                    MessageBox.Show($"SpaceX, we have a problem!\nHttpStatusCode: {response.StatusCode.ToString()}",
                        $"—{Application.ProductName}—", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Application.Exit();
                }
            });
        }
        
        //
        
        protected override CreateParams CreateParams
        {
            get
            {
                var cp = base.CreateParams;
                cp.ExStyle |= 0x00080000;
                return cp;
            }
        }
        
        private void UpdateFormDisplay(Image backgroundImage)
        {
            var screenDc = API.GetDC(IntPtr.Zero);
            var memDc = API.CreateCompatibleDC(screenDc);
            var hBitmap = IntPtr.Zero;
            var oldBitmap = IntPtr.Zero;

            try
            {
                var bmp = new Bitmap(backgroundImage);
                hBitmap = bmp.GetHbitmap(Color.FromArgb(0));
                oldBitmap = API.SelectObject(memDc, hBitmap);
                
                var size = bmp.Size;
                var pointSource = new Point(0, 0);
                var topPos = new Point(this.Left, this.Top);
                
                var blend = new API.BLENDFUNCTION
                {
                    BlendOp = API.AC_SRC_OVER,
                    BlendFlags = 0,
                    SourceConstantAlpha = 255,
                    AlphaFormat = API.AC_SRC_ALPHA
                };

                API.UpdateLayeredWindow(this.Handle, screenDc, ref topPos, ref size, memDc, ref pointSource, 0, ref blend, API.ULW_ALPHA);
                
                bmp.Dispose();
                API.ReleaseDC(IntPtr.Zero, screenDc);
                if (hBitmap != IntPtr.Zero)
                {
                    API.SelectObject(memDc, oldBitmap);
                    API.DeleteObject(hBitmap);
                }
                API.DeleteDC(memDc);
            }
            catch (Exception)
            {
                // ignored
            }
        }
        
        private void AppLoad(object sender, EventArgs e)
        {
            UpdateFormDisplay(BackgroundImage);
        }

        //
        
        public Initial()
        {
            InitializeComponent();
            HttpUtil.InitHttpClient();
            Task.Run(GetAllMissions);
        }
    }
    
    internal class API
    {
        public const byte AC_SRC_OVER = 0x00;
        public const byte AC_SRC_ALPHA = 0x01;
        public const Int32 ULW_ALPHA = 0x00000002;

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct BLENDFUNCTION
        {
            public byte BlendOp;
            public byte BlendFlags;
            public byte SourceConstantAlpha;
            public byte AlphaFormat;
        }

        [DllImport("user32.dll", ExactSpelling = true, SetLastError = true)]
        public static extern bool UpdateLayeredWindow(IntPtr hwnd, IntPtr hdcDst, ref Point pptDst, ref Size psize,
            IntPtr hdcSrc, ref Point pprSrc, Int32 crKey, ref BLENDFUNCTION pblend, Int32 dwFlags);
        
        [DllImport("user32.dll", ExactSpelling = true, SetLastError = true)]
        public static extern IntPtr GetDC(IntPtr hWnd);

        [DllImport("gdi32.dll", ExactSpelling = true, SetLastError = true)]
        public static extern IntPtr CreateCompatibleDC(IntPtr hDC);
        
        [DllImport("user32.dll", ExactSpelling = true)]
        public static extern int ReleaseDC(IntPtr hWnd, IntPtr hDC);

        [DllImport("gdi32.dll", ExactSpelling = true, SetLastError = true)]
        public static extern bool DeleteDC(IntPtr hdc);
        
        [DllImport("gdi32.dll", ExactSpelling = true)]
        public static extern IntPtr SelectObject(IntPtr hDC, IntPtr hObject);

        [DllImport("gdi32.dll", ExactSpelling = true, SetLastError = true)]
        public static extern bool DeleteObject(IntPtr hObject);
    }
}