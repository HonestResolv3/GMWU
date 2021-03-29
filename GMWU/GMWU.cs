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

        private void btnAdd2Queue_Click(object sender, EventArgs e)
        {
            GTask newTask = new GTask();
            switch (tctrlTasks.SelectedIndex)
            {
                case 0:
                    if (textBoxesAreBlank(txtAFLocation, txtGFLocation, txtGMAOutput) 
                        || checkLocationsBeingInvalid(txtAFLocation, txtGFLocation, txtGMAOutput, 0))
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
                    if (textBoxesAreBlank(txtGOLoc, txtGMAFLoc, txtGFLoc)
                        || checkLocationsBeingInvalid(txtGOLoc, txtGMAFLoc, txtGFLoc, 1))
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

        /**
         * Runs the helper method to load my Github page on the users browser
         */
        private void btnGitHub_Click(object sender, EventArgs e)
        {
            runUrlLoad("https://github.com/TruthfullyHonest");
        }

        /**
         * Runs the helper method to load my Steam page on the users browser
         */
        private void btnSteam_Click(object sender, EventArgs e)
        {
            runUrlLoad("https://steamcommunity.com/id/TruthfullyHonest");
        }

        /**
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
                // This area checks the platform the program is running on and then starts a new process with the URL and with different commands depending on the system
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
                    rtbErrors.Text += $"[{DateTime.Now.ToString("HH:mm:ss tt")}] The platform that this application is running on is not able to open up the provided URL: {Url}{Environment.NewLine}{Environment.NewLine}";
            }
        }

        /**
         * A helper method which checks if text boxes for each task are blank (Task Notes and Name are optional)
         */
        private bool textBoxesAreBlank(TextBox T1, TextBox T2, TextBox T3)
        {
            // Checking if each text boxes contents are not empty, null, or just spaces prevents bad input
            if (string.IsNullOrWhiteSpace(T1.Text) || string.IsNullOrWhiteSpace(T2.Text) || string.IsNullOrWhiteSpace(T3.Text))
            {
                MessageBox.Show("Please fill in all required fields before adding it to the task queue!", "Input Error");
                return true;
            }
            return false;
        }

        /**
         * A helper method which checks locations of all the information inside text boxes for each task
         * 
         * A switch statement was used since each task has a unique identifier (0 to 4) to tell the program what to check
         */
        private bool checkLocationsBeingInvalid(TextBox T1, TextBox T2, TextBox T3, int Identifier)
        {
            switch (Identifier)
            {
                case 0:
                    // The first task looks at a directory (Addon folder input), a file (GMad.exe Location), and another directory (.GMA file output)
                    if (!Directory.Exists(T1.Text) || !File.Exists(T2.Text) || !Directory.Exists(T3.Text))
                    {
                        MessageBox.Show("Make sure all locations provided are valid!", "Input Error");
                        return true;
                    }
                    break;
                case 1:
                    // The second task looks at a directory (Addon folder output for extraction), a file (GMad.exe Location), and another file (.GMA file location)
                    if (!Directory.Exists(T1.Text) || !File.Exists(T2.Text) || !File.Exists(T3.Text))
                    {
                        MessageBox.Show("Make sure all locations provided are valid!", "Input Error");
                        return true;
                    }
                    break;
                case 2:
                    // The third task looks at a file (Icon used for addon in the workshop), another file (GMPublish.exe Location), and another file (.GMA file location)
                    if (!File.Exists(T1.Text) || !File.Exists(T2.Text) || !File.Exists(T3.Text))
                    {
                        MessageBox.Show("Make sure all locations provided are valid!", "Input Error");
                        return true;
                    }
                    break;
            }
            // If everything is valid, let the program continue creating the Task object
            return false;
        }

        /**
         * A method that is run whenever the "Run Task in Queue" button is pressed
         * 
         * This method will take the index that is selected in the list box and match it up to the List<GTask> data structure that was made
         * 
         * A process is created with the appropriorate information, if the task has a folder location (for extracting .GMA files), a new directory will be created
         */
        private void btnRunTask_Click(object sender, EventArgs e)
        {
            // Checks if the selected index is valid (if it is not selected, or somehow goes over, it isn't valid)
            if (lbxQueue.SelectedIndex > lbxQueue.Items.Count || lbxQueue.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a valid task from the queue!", "Selection Error");
                return;
            }

            // A reference object to an element in the "list" data structure
            GTask refer = list[lbxQueue.SelectedIndex];

            /*
             * Create a new Process object that has properties defined in the StartInfo area
             * 
             * We do not want to create a new console window and let the output be redirected towards our new console
             */

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

            // This part is specifically meant for extracting .GMA files, if the directory does not exist and the FolderLocation property has content, make a new directory
            if (!string.IsNullOrWhiteSpace(refer.FolderLocation) && !Directory.Exists(refer.FolderLocation))
                Directory.CreateDirectory(Path.Combine(refer.FolderLocation, refer.FileName.Substring(0, refer.FileName.IndexOf("."))));

            // At the moment, I want to make this cross-platform, and in case there are any errors, they are logged to the Errors console window
            try
            {
                process.Start();
                btnRunTask.Enabled = false;
                while (!process.StandardOutput.EndOfStream)
                    rtbConsole.Text += process.StandardOutput.ReadLine() + Environment.NewLine;
                rtbConsole.Text += Environment.NewLine;
                btnRunTask.Enabled = true;
            }
            catch (Exception ex)
            {
                // If an exception occurs after the process is started, log the error and also this prevents the button from not being clickable
                rtbErrors.Text += $"[{DateTime.Now.ToString("HH:mm:ss tt")}] {ex}{Environment.NewLine}{Environment.NewLine}";
                btnRunTask.Enabled = true;
            }
        }
    }
}
