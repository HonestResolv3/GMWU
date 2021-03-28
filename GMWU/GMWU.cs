using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace GMWU
{
    public partial class GMWU : Form
    {
        List<GTask> list = new List<GTask>();

        public GMWU()
        {
            InitializeComponent();
        }

        private void btnAdd2Queue_Click(object sender, System.EventArgs e)
        {
            GTask newTask = new GTask();
            switch (tctrlTasks.SelectedIndex)
            {
                case 0:
                    if (textBoxesAreBlank(txtAFLocation, txtGFLocation, txtGMAOutput))
                        return;

                    if (string.IsNullOrWhiteSpace(tbxTaskName.Text))
                        newTask.TaskName = "Create .GMA";
                    else
                        newTask.TaskName = tbxTaskName.Text;

                    if (string.IsNullOrWhiteSpace(txtGMFileName.Text))
                        txtGMFileName.Text = "newgma";

                    newTask.FileName = txtGFLocation.Text;
                    newTask.Arguments = "create -folder \"" + txtAFLocation.Text + "\" -out \"" + txtGMAOutput.Text + "\\" + txtGMFileName.Text + ".gma" + "\"";
                    break;
                case 1:
                    if (textBoxesAreBlank(txtGOLoc, txtGMAFLoc, txtGFLoc))
                        return;

                    if (string.IsNullOrWhiteSpace(tbxTaskName.Text))
                        newTask.TaskName = "Extract .GMA";
                    else
                        newTask.TaskName = tbxTaskName.Text;

                    newTask.FileName = txtGMAFLoc.Text;
                    newTask.FolderLocation = txtGOLoc.Text;
                    newTask.Arguments = "extract -file \"" + txtGFLoc.Text + "\" -out \"" + txtGOLoc.Text + "\"";
                    break;
                case 2:
                    break;
                default:
                    break;
            }
            newTask.TaskNotes = tbxTaskNotes.Text;
            lbxQueue.Items.Add(newTask);
            list.Add(newTask);
        }

        private void btnGitHub_Click(object sender, System.EventArgs e)
        {
            runUrlLoad("https://github.com/TruthfullyHonest");
        }

        private void btnSteam_Click(object sender, System.EventArgs e)
        {
            runUrlLoad("https://steamcommunity.com/id/TruthfullyHonest");
        }

        /*
         * Credit to Brock Allen for finding this hack how to make URL's load cross-platform
         * URL here: https://brockallen.com/2016/09/24/process-start-for-urls-on-net-core/
         */
        private void runUrlLoad(string Url)
        {
            try
            {
                Process.Start(Url);
            }
            catch
            {
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                {
                    Url = Url.Replace("&", "^&");
                    Process.Start(new ProcessStartInfo("cmd", $"/c start {Url}") { CreateNoWindow = true });
                }
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                    Process.Start("xdg-open", Url);
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
                    Process.Start("open", Url);
                else
                    throw;
            }
        }

        private bool textBoxesAreBlank(TextBox T1, TextBox T2, TextBox T3)
        {
            if (string.IsNullOrWhiteSpace(T1.Text) || string.IsNullOrWhiteSpace(T2.Text) || string.IsNullOrWhiteSpace(T3.Text))
            {
                MessageBox.Show("Please fill in all required fields before adding it to the task queue!", "Input Error");
                return true;
            }
            return false;
        }

        private void btnRunTask_Click(object sender, System.EventArgs e)
        {
            if (lbxQueue.SelectedIndex > lbxQueue.Items.Count || lbxQueue.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a valid task from the queue!", "Selection Error");
                return;
            }

            GTask refer = list[lbxQueue.SelectedIndex];
            Process process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = refer.FileName,
                    Arguments = refer.Arguments,
                    CreateNoWindow = true,
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                }
            };

            if (!string.IsNullOrWhiteSpace(refer.FolderLocation) && !Directory.Exists(refer.FolderLocation))
                Directory.CreateDirectory(Path.Combine(refer.FolderLocation, refer.FileName.Substring(0, refer.FileName.IndexOf("."))));


            process.Start();
            btnRunTask.Enabled = false;
            while (!process.StandardOutput.EndOfStream)
                rtbConsole.Text += process.StandardOutput.ReadLine() + Environment.NewLine;
            rtbConsole.Text += Environment.NewLine;
            btnRunTask.Enabled = true;
        }
    }
}
