using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Emgu;
using Emgu.CV;
using Emgu.CV.Util;
using Emgu.CV.Structure;
using Emgu.Util;


using DirectShowLib;

namespace WebCam_project
{
    public partial class Form1 : Form
    {
        private VideoCapture capture = null;

        private DsDevice[] webCams = null;

        private int selectedCamerald = 0;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            webCams = DsDevice.GetDevicesOfCat(FilterCategory.VideoInputDevice);
            for (int i = 0; i < webCams.Length; i++)
            {
                toolStripComboBox1.Items.Add(webCams[i].Name);
            }
        }

        private void toolStripComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectedCamerald = toolStripComboBox1.SelectedIndex;
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            try
            {
                if (webCams.Length == 0)
                {
                    throw new Exception("У вас нету доступных камер ");
                }
                else if (toolStripComboBox1.SelectedItem == null)
                {
                    throw new Exception("Выберите одну из доступных камер");
                }
                else if (capture != null)
                {
                    capture.Start();
                }
                else
                {
                    capture = new VideoCapture(selectedCamerald);

                    capture.ImageGrabbed += Capture_ImageGrabbed;

                    capture.Start();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, " у вас ошибка   !", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Capture_ImageGrabbed(object sender, EventArgs e)
        {
            try
            {
                Mat m = new Mat();

                capture.Retrieve(m);

                pictureBox1.Image = m.ToImage<Bgr, Byte>().Flip(Emgu.CV.CvEnum.FlipType.Horizontal).ToBitmap();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, " у вас ошибка !", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }


        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            try
            {
                if (capture != null)
                {
                    capture.Pause();
                    capture.Dispose();
                    capture = null;

                    pictureBox1.Image.Dispose();
                    pictureBox1.Image = null;
                    selectedCamerald = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, " у вас ошибка !", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            try
            {
                if(capture != null)
                {
                    capture.Pause();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, " у вас ошибка !", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void toolStripLabel1_Click(object sender, EventArgs e)
        {

        }
    }
}
