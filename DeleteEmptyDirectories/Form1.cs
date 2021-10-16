using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Threading;

//written by vprabh01
namespace DeleteEmptyDirectories
{
    public partial class Form1 : Form
    {
        String sourceD;
        Boolean sourceChanged = false;
        public Form1()
        {
            InitializeComponent();
            btnBrowse.Select();
            ToolTip dirInfo = new ToolTip();
            dirInfo.ShowAlways = true;
            dirInfo.SetToolTip(txtFilepath, "Select a directory to clear of empty subdirectories");
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtFilepath.Text.ToString() == "")
                {
                    MessageBox.Show("Please select a directory");
                    return;
                }
                DirectoryInfo di = new DirectoryInfo(sourceD);
                if (MessageBox.Show("Delete all empty directories in '" + sourceD + "'?", "Continue?", MessageBoxButtons.OKCancel) == DialogResult.Cancel)
                    return;
                deleteEmpty(di);
                MessageBox.Show("Complete.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message.ToString());
            }
        }

        private void deleteEmpty(DirectoryInfo sourceInfo)
        {
            try
            {
                DirectoryInfo di = sourceInfo;
                DirectoryInfo[] allSub = di.GetDirectories();
                foreach (DirectoryInfo subDir in allSub)
                    deleteEmpty(subDir);
                allSub = di.GetDirectories();
                FileInfo[] allFiles = di.GetFiles();
                if (allFiles.Length == 0 && allSub.Length == 0)
                    di.Delete();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message.ToString());
            }
        }

        private void txtFilepath_TextChanged(object sender, EventArgs e)
        {
            sourceD = txtFilepath.Text;
            sourceChanged = true;
        }

        private void txtFilepath_Click(object sender, EventArgs e)
        {
            if (!sourceChanged)
                txtFilepath.Clear();
            txtFilepath.ForeColor = Color.Black;
            sourceChanged = true;
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult dr = folderBrowserDialog1.ShowDialog();
                if (dr == System.Windows.Forms.DialogResult.OK)
                {
                    txtFilepath.ForeColor = Color.Black;
                    txtFilepath.Text = folderBrowserDialog1.SelectedPath;
                    sourceChanged = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message.ToString());
            }
        }
    }
}
