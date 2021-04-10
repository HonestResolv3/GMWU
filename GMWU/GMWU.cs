using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace GMWU
{
    public partial class GMWU : Form
    {
        IntPtr dialog;
        Image newImg;
        Process process;
        TextReader reader;
        OutputContent output;

        string dialogResult;
        bool addonsLoaded = false;
        bool timerStarted = false;

        readonly List<GTask> list = new List<GTask>();
        readonly List<string> addons = new List<string>();
        readonly List<string> content = new List<string>();
        readonly StringBuilder builder = new StringBuilder();

        public GMWU()
        {
            InitializeComponent();
        }

        // Converts the information from each dialog to a string (such as file location)
        private static string stringFromAnsi(IntPtr ptr)
        {
            return Marshal.PtrToStringAnsi(ptr);
        }

        private void btnAdd2Queue_Click(object sender, EventArgs e)
        {
            GTask newTask = new GTask();
            switch (tctrlTasks.SelectedIndex)
            {
                case 0:
                    if (textBoxesAreBlank(txtAFLocation, txtGMadLoc1, txtGMOutput)
                        || checkLocationsBeingInvalid(txtAFLocation, txtGMadLoc1, txtGMOutput, 0))
                        return;

                    if (string.IsNullOrWhiteSpace(tbxTaskName.Text))
                        newTask.TaskName = "Create .GMA";
                    else
                        newTask.TaskName = tbxTaskName.Text;

                    if (string.IsNullOrWhiteSpace(txtGMFileName.Text))
                        txtGMFileName.Text = "newgma";

                    newTask.FileName = txtGMadLoc1.Text;
                    newTask.Arguments = "create -folder \"" + txtAFLocation.Text + "\" -out \"" + txtGMOutput.Text + "\\" + txtGMFileName.Text + ".gma" + "\"";
                    break;
                // This task is for extrafting a .GMA that will be output to a specific location
                case 1:
                    if (textBoxesAreBlank(txtCOLoc, txtGMadLoc2, txtGMFileLoc1)
                        || checkLocationsBeingInvalid(txtCOLoc, txtGMadLoc2, txtGMFileLoc1, 1))
                        return;

                    if (string.IsNullOrWhiteSpace(tbxTaskName.Text))
                        newTask.TaskName = "Extract .GMA";
                    else
                        newTask.TaskName = tbxTaskName.Text;

                    newTask.FileName = txtGMadLoc2.Text;
                    string gmaName = txtGMFileLoc1.Text.Substring(txtGMFileLoc1.Text.LastIndexOf("\\") + 1);
                    string folderLocation = Path.Combine(txtCOLoc.Text, gmaName.Substring(0, gmaName.IndexOf(".")));
                    newTask.Arguments = "extract -file \"" + txtGMFileLoc1.Text + "\" -out \"" + folderLocation + "\"";
                    break;
                case 2:
                    if (textBoxesAreBlank(txtGMFileLoc2, txtGMPubFileLoc1, txtIconLoc1)
                        || checkLocationsBeingInvalid(txtGMFileLoc2, txtGMPubFileLoc1, txtIconLoc1, 2))
                        return;

                    if (string.IsNullOrWhiteSpace(tbxTaskName.Text))
                        newTask.TaskName = "Publish Addon";
                    else
                        newTask.TaskName = tbxTaskName.Text;

                    newTask.FileName = txtGMPubFileLoc1.Text;
                    newTask.Arguments = "create -addon \"" + txtGMFileLoc2.Text + "\" -icon \"" + txtIconLoc1.Text + "\"";
                    break;
                case 3:
                    if (textBoxesAreBlank(txtGMFileLoc3, txtGMPubFileLoc2, txtAddonID)
                        || checkLocationsBeingInvalid(txtGMFileLoc3, txtGMPubFileLoc2, txtAddonID, 3))
                        return;

                    if (string.IsNullOrWhiteSpace(tbxTaskName.Text))
                        newTask.TaskName = "Update Addon";
                    else
                        newTask.TaskName = tbxTaskName.Text;

                    newTask.FileName = txtGMPubFileLoc2.Text;
                    newTask.Arguments = "update -addon \"" + txtGMFileLoc3.Text + "\" -id \"" + long.Parse(txtAddonID.Text) + "\" -changes \"" + txtChangeNotes.Text + "\"";
                    break;
                case 4:
                    if (textBoxesAreBlank(txtIconLoc2, txtGMPubFileLoc3, txtAddonID2)
                        || checkLocationsBeingInvalid(txtIconLoc2, txtGMPubFileLoc3, txtAddonID2, 3))
                        return;

                    if (string.IsNullOrWhiteSpace(tbxTaskName.Text))
                        newTask.TaskName = "Update Icon";
                    else
                        newTask.TaskName = tbxTaskName.Text;

                    newTask.FileName = txtGMPubFileLoc3.Text;
                    newTask.Arguments = "update -icon \"" + txtIconLoc2.Text + "\" -id \"" + long.Parse(txtAddonID2.Text) + "\"";
                    break;
                default:
                    break;
            }

            newTask.TaskNotes = tbxTaskNotes.Text;
            lbxQueue.Items.Add(newTask);
            list.Add(newTask);
            if (!timerStarted)
            {
                tmrQueueRunner.Start();
                timerStarted = true;
            }
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
                    throw;
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
                TinyFD.tinyfd_messageBox("Input Error", "Please fill in all required fields before adding it to the task queue", "ok", "error", 1);
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
                    // Create .GMA
                    if (!Directory.Exists(T1.Text) || !File.Exists(T2.Text) || !Directory.Exists(T3.Text))
                    {
                        TinyFD.tinyfd_messageBox("Input Error", "Make sure all locations provided are valid", "ok", "error", 1);
                        return true;
                    }
                    break;
                case 1:
                    // Extract .GMA
                    if (!Directory.Exists(T1.Text) || !File.Exists(T2.Text) || !File.Exists(T3.Text))
                    {
                        TinyFD.tinyfd_messageBox("Input Error", "Make sure all locations provided are valid", "ok", "error", 1);
                        return true;
                    }
                    break;
                case 2:
                    // Publish Addon
                    if (!File.Exists(T1.Text) || !File.Exists(T2.Text) || !File.Exists(T3.Text))
                    {
                        TinyFD.tinyfd_messageBox("Input Error", "Make sure all locations provided are valid", "ok", "error", 1);
                        return true;
                    }
                    break;
                case 3:
                    if (!File.Exists(T1.Text) || !File.Exists(T2.Text))
                    {
                        TinyFD.tinyfd_messageBox("Input Error", "Make sure all locations provided are valid", "ok", "error", 1);
                        return true;
                    }
                    else if (!long.TryParse(T3.Text, out long _))
                    {
                        TinyFD.tinyfd_messageBox("Addon ID Input Error", "Enter in a valid addon ID (you can use the addon list as well)", "ok", "error", 1);
                        return true;
                    }
                    break;
                default:
                    break;
            }
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
            runTask(lbxQueue.SelectedIndex);
        }

        private void tmrQueueRunner_Tick(object sender, EventArgs e)
        {
            if (!(lbxQueue.Items.Count > 0))
            {
                tmrQueueRunner.Stop();
                timerStarted = false;
                return;
            }

            runTask(lbxQueue.Items.Count - 1);
        }

        private void btnAFLoc_Click(object sender, EventArgs e)
        {
            loadFolderDialog("Select addon folder", string.Empty, txtAFLocation);
        }

        private void btnGMadLoc1_Click(object sender, EventArgs e)
        {
            loadFileDialog("Select gmad.exe file", string.Empty, 1, new string[] { "gmad.exe" }, "GMad File (gmad.exe)", 0, txtGMadLoc1);
        }

        private void btnGMOutput_Click(object sender, EventArgs e)
        {
            loadFolderDialog("Select folder to put the resulting .gma file in", string.Empty, txtGMOutput);
        }

        private void btnCOLoc_Click(object sender, EventArgs e)
        {
            loadFolderDialog("Select folder for the .gma content output", string.Empty, txtCOLoc);
        }

        private void btnGMadLoc2_Click(object sender, EventArgs e)
        {
            loadFileDialog("Select gmad.exe file", string.Empty, 1, new string[] { "gmad.exe" }, "GMad File (gmad.exe)", 0, txtGMadLoc2);
        }

        private void btnGMFileLoc1_Click(object sender, EventArgs e)
        {
            loadFileDialog("Select .gma file", string.Empty, 1, new string[] { "*.gma" }, "Garrys Mod Addon File (*.gma)", 0, txtGMFileLoc1);
        }

        private void btnGMFileLoc2_Click(object sender, EventArgs e)
        {
            loadFileDialog("Select .gma file", string.Empty, 1, new string[] { "*.gma" }, "Garrys Mod Addon File (*.gma)", 0, txtGMFileLoc2);
        }

        private void btnGMPubFileLoc1_Click(object sender, EventArgs e)
        {
            loadFileDialog("Select gmpublish.exe file", string.Empty, 1, new string[] { "gmpublish.exe" }, "GMPublish File (gmpublish.exe)", 0, txtGMPubFileLoc1);
        }

        private void btnIconLoc1_Click(object sender, EventArgs e)
        {
            loadIconDialog("Select .jpg file", string.Empty, 1, new string[] { "*.jpg" }, "JPG File (*.jpg)", 0, txtIconLoc1);
        }

        private void btnIconLoc2_Click(object sender, EventArgs e)
        {
            loadIconDialog("Select .jpg file", string.Empty, 1, new string[] { "*.jpg" }, "JPG File (*.jpg)", 0, txtIconLoc2);
        }

        private void btnGMPubFileLoc3_Click(object sender, EventArgs e)
        {
            loadFileDialog("Select gmpublish.exe file", string.Empty, 1, new string[] { "gmpublish.exe" }, "GMPublish File (gmpublish.exe)", 0, txtGMPubFileLoc3);
        }

        private void btnGMFileLoc3_Click(object sender, EventArgs e)
        {
            loadFileDialog("Select .gma file", string.Empty, 1, new string[] { "*.gma" }, "Garrys Mod Addon File (*.gma)", 0, txtGMFileLoc1);
        }

        private void btnDefGMPUFile_Click(object sender, EventArgs e)
        {
            loadFileDialog("Select gmpublish.exe file", string.Empty, 1, new string[] { "gmpublish.exe" }, "GMPublish File (gmpublish.exe)", 0, txtDefGMPFile);
        }

        private void btnGMPubFileLoc2_Click(object sender, EventArgs e)
        {
            loadFileDialog("Select gmpublish.exe file", string.Empty, 1, new string[] { "gmpublish.exe" }, "GMPublish File (gmpublish.exe)", 0, txtGMPubFileLoc2);
        }

        private void btnDeleteTask_Click(object sender, EventArgs e)
        {
            if (lbxQueue.SelectedIndex > lbxQueue.Items.Count || lbxQueue.SelectedIndex == -1)
            {
                TinyFD.tinyfd_messageBox("Selection Error", "Please select a valid task from the queue", "ok", "error", 1);
                return;
            }

            list.RemoveAt(lbxQueue.SelectedIndex);
            lbxQueue.Items.RemoveAt(lbxQueue.SelectedIndex);
        }

        private void btnJSOutput_Click(object sender, EventArgs e)
        {
            loadFolderDialog("Select folder for .GMA content output", string.Empty, txtJSOutput);
        }

        private void GMWU_Load(object sender, EventArgs e)
        {
            cbxTag1.SelectedIndex = 0;
            cbxTag2.SelectedIndex = 0;
            cbxAddonType.SelectedIndex = 0;
            lblCurrentQueueTime.Text = $"Current Time Interval: {tmrQueueRunner.Interval / 1000} Seconds";
        }

        private void btnLoadAddons_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtDefGMPFile.Text))
            {
                TinyFD.tinyfd_messageBox("No Location Provided", "You need to enter in the GMPublish.exe file in the Settings area of this program", "ok", "error", 1);
                return;
            }

            try
            {
                Process[] steam = Process.GetProcessesByName("steam");
                if (steam.Length == 0)
                {
                    TinyFD.tinyfd_messageBox("Steam.exe Not Running", "You need to load up Steam to use this part of the program", "ok", "error", 1);
                    return;
                }
            }
            catch (Exception ex)
            {
                tctrlConsole.SelectedIndex = 1;
                rtbErrors.Text += $"[{DateTime.Now.ToString("HH:mm:ss tt")}] {ex}{Environment.NewLine}{Environment.NewLine}";
            }

            Process addonList = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = txtDefGMPFile.Text,
                    Arguments = "list",
                    CreateNoWindow = true,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                }
            };

            try
            {
                addonList.Start();
                content.Clear();
                btnLoadAddons.Enabled = false;

                while (!addonList.StandardOutput.EndOfStream)
                    content.Add(addonList.StandardOutput.ReadLine().Trim());

                for (int i = 0; i < content.Count; i++)
                    if (!(i < 5))
                        lbxAddonList.Items.Add(content[i]);

                lbxAddonList.Items.RemoveAt(lbxAddonList.Items.Count - 1);
                addonsLoaded = true;
                btnLoadAddons.Enabled = true;
            }
            catch (Exception ex)
            {
                btnLoadAddons.Enabled = true;
                tctrlConsole.SelectedIndex = 1;
                rtbErrors.Text += $"[{DateTime.Now.ToString("HH:mm:ss tt")}] {ex}{Environment.NewLine}{Environment.NewLine}";
            }
        }

        private void btnCreateJS_Click(object sender, EventArgs e)
        {
            if (!Directory.Exists(txtJSOutput.Text))
            {
                TinyFD.tinyfd_messageBox("No Location Provided", "You need to enter in a location for the addon.json file that is created", "ok", "error", 1);
                return;
            }
            else if (string.IsNullOrWhiteSpace(txtAddonTitle.Text))
            {
                TinyFD.tinyfd_messageBox("No Addon Title Provided", "You need to enter in a title for your addon", "ok", "error", 1);
                return;
            }
            else if (cbxTag1.SelectedItem.ToString().Equals(cbxTag2.SelectedItem.ToString()))
            {
                TinyFD.tinyfd_messageBox("Duplicate Tags Entered", "You cannot add the same addon tag twice", "ok", "error", 1);
                return;
            }

            string[] tags;
            if (cbxTag2.SelectedIndex > 0)
                tags = new string[] { cbxTag1.SelectedItem.ToString().ToLowerInvariant(), cbxTag2.SelectedItem.ToString().ToLowerInvariant() };
            else
                tags = new string[] { cbxTag1.SelectedItem.ToString().ToLowerInvariant() };

            string[] wildcards;
            if (string.IsNullOrWhiteSpace(txtWildcards.Text))
                wildcards = new string[0];
            else
            {
                try
                {
                    wildcards = Regex.Replace(txtWildcards.Text, @"\s+", string.Empty).Split(',').Where(s => !string.IsNullOrWhiteSpace(s)).ToArray();
                }
                catch (Exception ex)
                {
                    wildcards = new string[0];
                    tctrlConsole.SelectedIndex = 1;
                    rtbErrors.Text += $"[{DateTime.Now.ToString("HH:mm:ss tt")}] {ex}{Environment.NewLine}{Environment.NewLine}";
                }
            }

            try
            {
                AddonJson js = new AddonJson(txtAddonTitle.Text, cbxAddonType.SelectedItem.ToString().ToLowerInvariant(), tags, wildcards);
                string json = JsonConvert.SerializeObject(js, Formatting.Indented);
                File.WriteAllText(Path.Combine(txtJSOutput.Text, "addon.json"), json);
                TinyFD.tinyfd_messageBox("Addon.json Created", $"The addon.json file has been created at: {txtJSOutput.Text}", "ok", "info", 1);
            }
            catch (Exception ex)
            {
                tctrlConsole.SelectedIndex = 1;
                rtbErrors.Text += $"[{DateTime.Now.ToString("HH:mm:ss tt")}] {ex}{Environment.NewLine}{Environment.NewLine}";
            }
        }

        private void btnUseID_Click(object sender, EventArgs e)
        {
            if (!addonsLoaded)
            {
                TinyFD.tinyfd_messageBox("Addons Not Loaded", "Please load your addons before using this function", "ok", "error", 1);
                return;
            }

            try
            {
                Clipboard.SetText(lbxAddonList.SelectedItem.ToString().Substring(0, lbxAddonList.SelectedItem.ToString().IndexOf('\t')));
                TinyFD.tinyfd_messageBox("Addon ID Copied", "Addon ID has been copied to your clipboard", "ok", "info", 1);
            }
            catch (Exception ex)
            {
                tctrlConsole.SelectedIndex = 1;
                rtbErrors.Text += $"[{DateTime.Now.ToString("HH:mm:ss tt")}] {ex}{Environment.NewLine}{Environment.NewLine}";
            }
        }

        private void rtbErrors_TextChanged(object sender, EventArgs e)
        {
            tpgErrors.Text = "Program Errors (New)";
        }

        private void tctrlConsole_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tctrlConsole.SelectedIndex != 0)
                tpgErrors.Text = "Program Errors";
        }

        /**
         * A helper method that loads folder dialogs for addon and gma input/output
         */
        private void loadFolderDialog(string title, string defaultPath, TextBox T1)
        {
            dialog = TinyFD.tinyfd_selectFolderDialog(title, defaultPath);
            dialogResult = stringFromAnsi(dialog);
            if (!string.IsNullOrWhiteSpace(dialogResult))
                T1.Text = dialogResult;
        }

        /**
         * A helper method that loads file dialogs for gmad.exe, gmpublish.exe, and any .gma files, prevents redundancy
         */
        private void loadFileDialog(string title, string defaultPath, int numFilters, string[] filterPatterns, string filterDescription, int allowMultipleSelects, TextBox T1)
        {
            dialog = TinyFD.tinyfd_openFileDialog(title, defaultPath, numFilters, filterPatterns, filterDescription, allowMultipleSelects);
            dialogResult = stringFromAnsi(dialog);
            if (!string.IsNullOrWhiteSpace(dialogResult))
                T1.Text = dialogResult;
        }

        /**
         * A helper method that loads file dialogs for .jpg icons that are 512x512
         */
        private void loadIconDialog(string title, string defaultPath, int numFilters, string[] filterPatterns, string filterDescription, int allowMultipleSelects, TextBox T1)
        {
            dialog = TinyFD.tinyfd_openFileDialog(title, defaultPath, numFilters, filterPatterns, filterDescription, allowMultipleSelects);
            dialogResult = stringFromAnsi(dialog);
            if (!string.IsNullOrWhiteSpace(dialogResult))
            {
                if (!dialogResult.Contains(".jpg"))
                {
                    TinyFD.tinyfd_messageBox("Image Load Error", "This image is not a .jpg", "ok", "error", 1);
                    return;
                }

                try
                {
                    newImg = Image.FromFile(dialogResult);
                    if (newImg.Width != 512 || newImg.Height != 512)
                    {
                        if (pbxIcon.Image != null)
                            pbxIcon.Image.Dispose();
                        TinyFD.tinyfd_messageBox("Image Load Error", "The specified image is not 512x512", "ok", "error", 1);
                        return;
                    }
                    pbxIcon.Image = newImg;
                    T1.Text = dialogResult;
                }
                catch (Exception ex)
                {
                    tctrlConsole.SelectedIndex = 1;
                    rtbErrors.Text += $"[{DateTime.Now.ToString("HH:mm:ss tt")}] {ex}{Environment.NewLine}{Environment.NewLine}";
                }
            }
        }

        private void runTask(int queueItemIndex)
        {
            if (process != null && !process.HasExited)
                return;

            if (queueItemIndex > lbxQueue.Items.Count || queueItemIndex == -1)
            {
                TinyFD.tinyfd_messageBox("Selection Error", "Please select a valid task from the queue", "ok", "error", 1);
                return;
            }

            // A reference object to an element in the "list" data structure
            GTask refer = list[queueItemIndex];

            /*
             * Create a new Process object that has properties defined in the StartInfo area
             * 
             * We do not want to create a new console window and let the output be redirected towards our new console
             */
            process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = refer.FileName,
                    Arguments = refer.Arguments,
                    CreateNoWindow = true,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                }
            };

            // At the moment, I want to make this cross-platform, and in case there are any errors, they are logged to the Errors console window
            try
            {
                process.Start();
                btnRunTask.Enabled = false;
                reader = TextReader.Synchronized(process.StandardOutput);
                bwrConsoleOutput.RunWorkerAsync();

                /*while (!process.StandardOutput.EndOfStream)
                    rtbConsole.Text += process.StandardOutput.ReadLine() + Environment.NewLine;
                rtbConsole.Text += Environment.NewLine;*/

                btnRunTask.Enabled = true;
                list.RemoveAt(queueItemIndex);
                lbxQueue.Items.RemoveAt(queueItemIndex);
            }
            catch (Exception ex)
            {
                tctrlConsole.SelectedIndex = 1;
                rtbErrors.Text += $"[{DateTime.Now.ToString("HH:mm:ss tt")}] {ex}{Environment.NewLine}{Environment.NewLine}";
                btnRunTask.Enabled = true;
            }
        }

        private void bwrConsoleOutput_DoWork(object sender, DoWorkEventArgs e)
        {
            while (!bwrConsoleOutput.CancellationPending)
            {
                bwrConsoleOutput.ReportProgress(0, new OutputContent() { Content = reader.ReadToEnd() });
                bwrConsoleOutput.CancelAsync();
            }
        }

        private void bwrConsoleOutput_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (e.UserState is OutputContent)
            {
                output = e.UserState as OutputContent;
                rtbConsole.Text += $"{output.Content}{Environment.NewLine}{Environment.NewLine}";
            }
        }
    }
}
