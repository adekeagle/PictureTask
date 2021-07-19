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
        private const string FILENAME = @"last_picture.txt";

        public MainForm()
        {
            InitializeComponent();

            if (File.Exists(FILENAME) && !string.IsNullOrEmpty(readFilePath()))
            {
                picture = Image.FromFile(readFilePath());
                pbPicture.SizeMode = PictureBoxSizeMode.StretchImage;
                pbPicture.Image = picture;

                showListView();
            }

            if (!isPicture())
            {
                btnDelete.Enabled = false;
                lvInfo.Visible = false;
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

                showListView();
            }
        }

        private static void saveFilePath(string path)
        {
            File.WriteAllText(FILENAME, path);
        }

        private static string readFilePath()
        {
            return File.ReadAllText(FILENAME);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            pbPicture.Image.Dispose();
            pbPicture.Image = null;
            saveFilePath("");
            btnDelete.Enabled = false;
            lvInfo.Items.RemoveAt(0);
            lvInfo.Visible = false;
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

        private void showListView()
        {
            lvInfo.Visible = true;
            lvInfo.Columns.Add("Name", 60, HorizontalAlignment.Center);
            lvInfo.Columns.Add("Values", 80, HorizontalAlignment.Center);

            ListViewItem eachRow = new ListViewItem("Name");
            ListViewItem.ListViewSubItem name = new ListViewItem.ListViewSubItem(eachRow, Path.GetFileName(readFilePath()));

            if (lvInfo.Items.Count != 0)
            {
                lvInfo.Items.RemoveAt(0);
            }
            eachRow.SubItems.Add(name);

            lvInfo.Items.Add(eachRow);
        }
    }
}