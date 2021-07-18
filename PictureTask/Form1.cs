using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace PictureTask
{
    public partial class MainForm : Form
    {
        private OpenFileDialog fileDialog;
        private Image picture;
        private const string FILENAME = "last_picture.txt";
        public MainForm()
        {
            InitializeComponent();

            if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + FILENAME) && !string.IsNullOrEmpty(readFilePath()))
            {
                picture = Image.FromFile(readFilePath());
                pbPicture.SizeMode = PictureBoxSizeMode.StretchImage;
                pbPicture.Image = picture;
            }

            if (!isPicture())
            {
                btnDelete.Enabled = false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            fileDialog = new OpenFileDialog();
            fileDialog.Filter = "JPG Files | *.jpg";

            if (!isPicture())
            {
                btnDelete.Enabled = false;
            }

            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                picture = Image.FromFile(fileDialog.FileName);
                pbPicture.SizeMode = PictureBoxSizeMode.StretchImage;
                pbPicture.Image = picture;
                saveFilePath(fileDialog.FileName);
                btnDelete.Enabled = true;
            }
        }

        private static void saveFilePath(string path)
        {
            File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + FILENAME, path);
        }

        private static string readFilePath()
        {
            return File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + FILENAME);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            pbPicture.Image.Dispose();
            pbPicture.Image = null;
            saveFilePath("");
            btnDelete.Enabled = false;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private bool isPicture()
        {
            if (pbPicture.Image == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}