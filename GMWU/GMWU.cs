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
using System.Threading;
using System.Windows.Forms;

namespace GMWU
{
    public partial class GMWU : Form
    {
        IntPtr dialog;
        Image newImg;
        Process process;
        Process addonList;
        TextReader reader;
        TextReader reader2;
        OutputContent output;
        List<string> addons = new List<string>();

        string dialogResult;
        bool addonsLoaded = false;
        bool timerStarted = false;
        int queueSelection;

        readonly List<GTask> list = new List<GTask>();
        readonly List<string> content = new List<string>();

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
            GTask taskRef = createTask(tctrlTasks.SelectedIndex);
            if (taskRef == null)
            {
                TinyFD.tinyfd_messageBox("Information Incorrect", "Please make sure all fields for the specified task are correct", "ok", "error", 1);
                return;
            }

            lbxQueue.Items.Add(taskRef);
            list.Add(taskRef);
            if (!timerStarted && chkAutoRunTask.Enabled)
            {
                tmrQueueRunner.Start();
                timerStarted = true;
            }
        }

        private void btnOverwriteTask_Click(object sender, EventArgs e)
        {
            if (lbxQueue.Items.Count <= 0)
            {
                TinyFD.tinyfd_messageBox("No Items in Queue", "Please create some tasks for the program to use before using this program", "ok", "error", 1);
                return;
            }
            else if (lbxQueue.SelectedIndex < 0 || lbxQueue.SelectedIndex > lbxQueue.Items.Count - 1)
            {
                TinyFD.tinyfd_messageBox("Invalid Selection Provided", "Please select a valid task from the queue", "ok", "error", 1);
                return;
            }

            GTask taskRef = createTask(tctrlTasks.SelectedIndex);
            if (taskRef == null)
            {
                TinyFD.tinyfd_messageBox("Information Incorrect", "Please make sure all fields for the specified task are correct", "ok", "error", 1);
                return;
            }

            list[lbxQueue.SelectedIndex] = taskRef;
        }

        private GTask createTask(int index)
        {
            GTask newTask = new GTask();
            switch (index)
            {
                case 0:
                    if (textBoxesAreBlank(txtAFLocation, txtGMadLoc1, txtGMOutput)
                        || checkLocationsBeingInvalid(txtAFLocation, txtGMadLoc1, txtGMOutput, 0))
                        return null;

                    if (string.IsNullOrWhiteSpace(txtTaskName.Text))
                        newTask.TaskName = "Create .GMA";
                    else
                        newTask.TaskName = txtTaskName.Text;

                    if (string.IsNullOrWhiteSpace(txtGMFileName.Text))
                        txtGMFileName.Text = "newgma";

                    newTask.FileName = txtGMadLoc1.Text;
                    newTask.Arguments = "create -folder \"" + txtAFLocation.Text + "\" -out \"" + txtGMOutput.Text + "\\" + txtGMFileName.Text + ".gma" + "\"";
                    newTask.TaskType = Enums.TaskType.CreateGMA;
                    break;
                // This task is for extrafting a .GMA that will be output to a specific location
                case 1:
                    if (textBoxesAreBlank(txtCOLoc, txtGMadLoc2, txtGMFileLoc1)
                        || checkLocationsBeingInvalid(txtCOLoc, txtGMadLoc2, txtGMFileLoc1, 1))
                        return null;

                    if (string.IsNullOrWhiteSpace(txtTaskName.Text))
                        newTask.TaskName = "Extract .GMA";
                    else
                        newTask.TaskName = txtTaskName.Text;

                    newTask.FileName = txtGMadLoc2.Text;
                    string gmaName = txtGMFileLoc1.Text.Substring(txtGMFileLoc1.Text.LastIndexOf("\\") + 1);
                    string folderLocation = Path.Combine(txtCOLoc.Text, gmaName.Substring(0, gmaName.IndexOf(".")));
                    newTask.Arguments = "extract -file \"" + txtGMFileLoc1.Text + "\" -out \"" + folderLocation + "\"";
                    newTask.TaskType = Enums.TaskType.ExtractGMA;
                    break;
                case 2:
                    if (textBoxesAreBlank(txtGMFileLoc2, txtGMPubFileLoc1, txtIconLoc1)
                        || checkLocationsBeingInvalid(txtGMFileLoc2, txtGMPubFileLoc1, txtIconLoc1, 2))
                        return null;

                    if (string.IsNullOrWhiteSpace(txtTaskName.Text))
                        newTask.TaskName = "Publish Addon";
                    else
                        newTask.TaskName = txtTaskName.Text;

                    newTask.FileName = txtGMPubFileLoc1.Text;
                    newTask.Arguments = "create -addon \"" + txtGMFileLoc2.Text + "\" -icon \"" + txtIconLoc1.Text + "\"";
                    newTask.TaskType = Enums.TaskType.PublishAddon;
                    break;
                case 3:
                    if (textBoxesAreBlank(txtGMFileLoc3, txtGMPubFileLoc2, txtAddonID)
                        || checkLocationsBeingInvalid(txtGMFileLoc3, txtGMPubFileLoc2, txtAddonID, 3))
                        return null;

                    if (string.IsNullOrWhiteSpace(txtTaskName.Text))
                        newTask.TaskName = "Update Addon";
                    else
                        newTask.TaskName = txtTaskName.Text;

                    newTask.FileName = txtGMPubFileLoc2.Text;
                    newTask.Arguments = "update -addon \"" + txtGMFileLoc3.Text + "\" -id \"" + long.Parse(txtAddonID.Text) + "\" -changes \"" + txtChangeNotes.Text + "\"";
                    newTask.TaskType = Enums.TaskType.UpdateAddon;
                    break;
                case 4:
                    if (textBoxesAreBlank(txtIconLoc2, txtGMPubFileLoc3, txtAddonID2)
                        || checkLocationsBeingInvalid(txtIconLoc2, txtGMPubFileLoc3, txtAddonID2, 3))
                        return null;

                    if (string.IsNullOrWhiteSpace(txtTaskName.Text))
                        newTask.TaskName = "Update Icon";
                    else
                        newTask.TaskName = txtTaskName.Text;

                    newTask.FileName = txtGMPubFileLoc3.Text;
                    newTask.Arguments = "update -icon \"" + txtIconLoc2.Text + "\" -id \"" + long.Parse(txtAddonID2.Text) + "\"";
                    newTask.TaskType = Enums.TaskType.UpdateIcon;
                    break;
                default:
                    break;
            }

            newTask.TaskNotes = txtTaskNotes.Text;
            return newTask;
        }

        /**
         * Runs the helper method to load my Github page on the users browser
         */
        private void btnGitHub_Click(object sender, EventArgs e)
        {
            runUrlLoad("https://github.com/TruthfullyHonest");
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
                    if (!CheckIfFileIsImage(T3.Text))
                        return true;
                    break;
                case 3:
                    // Update Addon
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
                case 4:
                    // Update Icon
                    if (!File.Exists(T1.Text) || !File.Exists(T2.Text))
                    {
                        TinyFD.tinyfd_messageBox("Input Error", "Make sure all locations provided are valid", "ok", "error", 1);
                        return true;
                    }
                    if (!CheckIfFileIsImage(T1.Text))
                        return true;
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
                queueSelection = queueItemIndex;
            }
            catch (Exception ex)
            {
                tctrlConsole.SelectedIndex = 1;
                rtbErrors.Text += $"[{DateTime.Now.ToString("HH:mm:ss tt")}] {ex}{Environment.NewLine}{Environment.NewLine}";
                btnRunTask.Enabled = true;
            }
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
            loadFileDialog("Select .gma file", string.Empty, 1, new string[] { "*.gma" }, "Garrys Mod Addon File (*.gma)", 0, txtGMFileLoc3);
        }

        private void btnDefGMPUFile_Click(object sender, EventArgs e)
        {
            loadFileDialog("Select gmpublish.exe file", string.Empty, 1, new string[] { "gmpublish.exe" }, "GMPublish File (gmpublish.exe)", 0, txtDefGMPFile);
        }

        private void btnGMPubFileLoc2_Click(object sender, EventArgs e)
        {
            loadFileDialog("Select gmpublish.exe file", string.Empty, 1, new string[] { "gmpublish.exe" }, "GMPublish File (gmpublish.exe)", 0, txtGMPubFileLoc2);
        }

        private void btnJSOutput_Click(object sender, EventArgs e)
        {
            loadFolderDialog("Select folder for .GMA content output", string.Empty, txtJSOutput);
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

                if (CheckIfFileIsImage(dialogResult))
                {
                    pbxIcon.Image = newImg;
                    T1.Text = dialogResult;
                }
            }
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

        private void GMWU_Load(object sender, EventArgs e)
        {
            txtDefGMAFile.Text = Properties.Settings.Default.DefaultGMadFilePath;
            txtDefGMPFile.Text = Properties.Settings.Default.DefaultGMPublishFilePath;
            txtAFLocation.Text = Properties.Settings.Default.AddonFolderLocation;
            txtGMadLoc1.Text = Properties.Settings.Default.GMadFile1Location;
            txtGMOutput.Text = Properties.Settings.Default.GMadOutputLocation;
            txtGMFileName.Text = Properties.Settings.Default.GMAFileName;
            txtCOLoc.Text = Properties.Settings.Default.ExtractOutputLocation;
            txtGMadLoc2.Text = Properties.Settings.Default.GMadFile2Location;
            txtGMFileLoc1.Text = Properties.Settings.Default.GMAFile1Location;
            txtGMFileLoc2.Text = Properties.Settings.Default.GMAFile2Location;
            txtGMPubFileLoc1.Text = Properties.Settings.Default.GMPubFile1Location;
            txtIconLoc1.Text = Properties.Settings.Default.IconFile1Location;
            txtGMFileLoc3.Text = Properties.Settings.Default.GMAFile3Location;
            txtGMPubFileLoc2.Text = Properties.Settings.Default.GMPubFile2Location;
            txtAddonID.Text = Properties.Settings.Default.UpdateAddonID.ToString();
            txtChangeNotes.Text = Properties.Settings.Default.UpdateAddonNotes;
            txtIconLoc2.Text = Properties.Settings.Default.IconFile2Location;
            txtGMPubFileLoc3.Text = Properties.Settings.Default.GMPubFile3Location;
            txtAddonID2.Text = Properties.Settings.Default.UpdateIconID.ToString();
            txtTaskName.Text = Properties.Settings.Default.TaskName;
            txtTaskNotes.Text = Properties.Settings.Default.TaskNotes;
            tctrlMainArea.SelectedIndex = Properties.Settings.Default.MainControlIndex;
            tctrlConsole.SelectedIndex = Properties.Settings.Default.ConsoleControlIndex;
            tctrlTaskArea.SelectedIndex = Properties.Settings.Default.TaskControlIndex;
            tctrlTasks.SelectedIndex = Properties.Settings.Default.TaskSelectionIndex;
            txtJSOutput.Text = Properties.Settings.Default.JsonFileOutput;
            txtAddonTitle.Text = Properties.Settings.Default.JsonAddonTitle;
            txtWildcards.Text = Properties.Settings.Default.JsonAddonWildcards;
            cbxTag1.SelectedIndex = Properties.Settings.Default.AddonTag1Index;
            cbxTag2.SelectedIndex = Properties.Settings.Default.AddonTag2Index;
            cbxAddonType.SelectedIndex = Properties.Settings.Default.AddonTypeIndex;
            txtChangeTime.Text = Properties.Settings.Default.ChangeTaskTime.ToString();
            chkUseDefaultExe.Checked = Properties.Settings.Default.UseDefaultLocations;
            chkAutoRunTask.Checked = Properties.Settings.Default.AutoRunTasks;
            tmrQueueRunner.Interval = Properties.Settings.Default.TaskIntervalTime;
            lblCurrentQueueTime.Text = $"Current Time Interval: {tmrQueueRunner.Interval / 1000} Seconds";
        }

        private void btnLoadAddons_Click(object sender, EventArgs e)
        {
            if (addonList != null && !addonList.HasExited)
                return;

            lbxAddonList.Items.Clear();
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

            addonList = new Process
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
                reader2 = TextReader.Synchronized(addonList.StandardOutput);
                bwrAddonList.RunWorkerAsync();

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


        private void bwrConsoleOutput_DoWork(object sender, DoWorkEventArgs e)
        {
            while (!bwrConsoleOutput.CancellationPending)
            {
                bwrConsoleOutput.ReportProgress(0, new OutputContent() { Content = reader.ReadLine() });
            }
        }

        private void bwrConsoleOutput_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (e.UserState is OutputContent)
            {
                output = e.UserState as OutputContent;
                if (string.IsNullOrWhiteSpace(output.Content))
                    bwrConsoleOutput.CancelAsync();
                else
                    rtbConsole.Text += $"{output.Content}{Environment.NewLine}{Environment.NewLine}";
            }
        }

        private void bwrAddonList_DoWork(object sender, DoWorkEventArgs e)
        {
            while (!bwrAddonList.CancellationPending)
            {
                bwrAddonList.ReportProgress(0, new OutputContent() { Content = reader2.ReadLine() });
            }
        }

        private void bwrAddonList_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (e.UserState is OutputContent)
            {
                output = e.UserState as OutputContent;
                if (string.IsNullOrWhiteSpace(output.Content))
                    bwrAddonList.CancelAsync();
                else
                {
                    addons.Add(output.Content.Trim());
                    lbxAddonList.Items.Add($"{output.Content.Trim()}{Environment.NewLine}");
                }
            }
        }

        private void bwrConsoleOutput_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                list.RemoveAt(queueSelection);
                lbxQueue.Items.RemoveAt(queueSelection);
            }
            catch (Exception ex)
            {
                tctrlConsole.SelectedIndex = 1;
                rtbErrors.Text += $"[{DateTime.Now.ToString("HH:mm:ss tt")}] {ex}{Environment.NewLine}";
            }
            finally
            {
                btnRunTask.Enabled = true;
            }
        }

        private bool CheckIfFileIsImage(string path)
        {
            try
            {
                newImg = Image.FromFile(path);
                if (newImg.Width != 512 || newImg.Height != 512)
                {
                    if (pbxIcon.Image != null)
                        pbxIcon.Image.Dispose();
                    TinyFD.tinyfd_messageBox("Image Load Error", "The specified image is not 512x512", "ok", "error", 1);
                    return false;
                }
            }
            catch (Exception ex)
            {
                tctrlConsole.SelectedIndex = 1;
                rtbErrors.Text += $"[{DateTime.Now.ToString("HH:mm:ss tt")}] {ex}{Environment.NewLine}{Environment.NewLine}";
                return false;
            }
            return true;
        }

        private void bwrAddonList_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                lbxAddonList.Items.RemoveAt(0);
                lbxAddonList.Items.RemoveAt(0);
                lbxAddonList.Items.RemoveAt(0);
                lbxAddonList.Items.RemoveAt(0);
                lbxAddonList.Items.RemoveAt(lbxAddonList.Items.Count - 1);
            }
            catch (Exception ex)
            {
                tctrlConsole.SelectedIndex = 1;
                rtbErrors.Text += $"[{DateTime.Now.ToString("HH:mm:ss tt")}] {ex}{Environment.NewLine}{Environment.NewLine}";
            }
            finally
            {
                addonsLoaded = true;
                btnLoadAddons.Enabled = true;
            }
        }

        private void btnDefGMAFile_Click(object sender, EventArgs e)
        {

        }

        private void chkUseDefaultExe_CheckedChanged(object sender, EventArgs e)
        {
            if (chkUseDefaultExe.Checked)
            {
                txtGMadLoc1.Text = txtDefGMAFile.Text;
                txtGMadLoc2.Text = txtDefGMAFile.Text;
                txtGMPubFileLoc1.Text = txtDefGMPFile.Text;
                txtGMPubFileLoc2.Text = txtDefGMPFile.Text;
                txtGMPubFileLoc3.Text = txtDefGMPFile.Text;
            }

            btnGMadLoc1.Enabled = !chkUseDefaultExe.Checked;
            btnGMadLoc2.Enabled = !chkUseDefaultExe.Checked;
            btnGMPubFileLoc1.Enabled = !chkUseDefaultExe.Checked;
            btnGMPubFileLoc2.Enabled = !chkUseDefaultExe.Checked;
            btnGMPubFileLoc3.Enabled = !chkUseDefaultExe.Checked;

            txtGMadLoc1.Enabled = !chkUseDefaultExe.Checked;
            txtGMadLoc2.Enabled = !chkUseDefaultExe.Checked;
            txtGMPubFileLoc1.Enabled = !chkUseDefaultExe.Checked;
            txtGMPubFileLoc2.Enabled = !chkUseDefaultExe.Checked;
            txtGMPubFileLoc3.Enabled = !chkUseDefaultExe.Checked;
        }

        private void btnApplyChanges_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(txtChangeTime.Text, out int time))
            {
                TinyFD.tinyfd_messageBox("Time information Incorrect", "Please enter in a valid time for the tasks to run at", "ok", "error", 1);
                return;
            }
            tmrQueueRunner.Interval = Math.Abs(time * 1000);
            lblCurrentQueueTime.Text = $"Current Time Interval: {tmrQueueRunner.Interval / 1000} Seconds";
        }

        private void GMWU_FormClosing(object sender, FormClosingEventArgs e)
        {
            Properties.Settings.Default.DefaultGMadFilePath = txtDefGMAFile.Text;
            Properties.Settings.Default.DefaultGMPublishFilePath = txtDefGMPFile.Text;
            Properties.Settings.Default.AddonFolderLocation = txtAFLocation.Text;
            Properties.Settings.Default.GMadFile1Location = txtGMadLoc1.Text;
            Properties.Settings.Default.GMadOutputLocation = txtGMOutput.Text;
            Properties.Settings.Default.GMAFileName = txtGMFileName.Text;
            Properties.Settings.Default.ExtractOutputLocation = txtCOLoc.Text;
            Properties.Settings.Default.GMadFile2Location = txtGMadLoc2.Text;
            Properties.Settings.Default.GMAFile1Location = txtGMFileLoc1.Text;
            Properties.Settings.Default.GMAFile2Location = txtGMFileLoc2.Text;
            Properties.Settings.Default.GMPubFile1Location = txtGMPubFileLoc1.Text;
            Properties.Settings.Default.IconFile1Location = txtIconLoc1.Text;
            Properties.Settings.Default.GMAFile3Location = txtGMFileLoc3.Text;
            Properties.Settings.Default.GMPubFile2Location = txtGMPubFileLoc2.Text;

            if (int.TryParse(txtAddonID.Text, out int addonID1))
                Properties.Settings.Default.UpdateAddonID = Math.Abs(addonID1);
            else
                Properties.Settings.Default.UpdateAddonID = 000000000;

            Properties.Settings.Default.UpdateAddonNotes = txtChangeNotes.Text;
            Properties.Settings.Default.IconFile2Location = txtIconLoc2.Text;
            Properties.Settings.Default.GMPubFile3Location = txtGMPubFileLoc3.Text;

            if (int.TryParse(txtAddonID2.Text, out int addonID2))
                Properties.Settings.Default.UpdateIconID = Math.Abs(addonID2);
            else
                Properties.Settings.Default.UpdateIconID = 000000000;

            if (int.TryParse(txtChangeTime.Text, out int time))
                Properties.Settings.Default.ChangeTaskTime = Math.Abs(time);
            else
                Properties.Settings.Default.ChangeTaskTime = 30;

            Properties.Settings.Default.TaskName = txtTaskName.Text;
            Properties.Settings.Default.TaskNotes = txtTaskNotes.Text;
            Properties.Settings.Default.MainControlIndex = tctrlMainArea.SelectedIndex;
            Properties.Settings.Default.ConsoleControlIndex = tctrlConsole.SelectedIndex;
            Properties.Settings.Default.TaskControlIndex = tctrlTaskArea.SelectedIndex;
            Properties.Settings.Default.TaskSelectionIndex = tctrlTasks.SelectedIndex;
            Properties.Settings.Default.JsonFileOutput = txtJSOutput.Text;
            Properties.Settings.Default.JsonAddonTitle = txtAddonTitle.Text;
            Properties.Settings.Default.JsonAddonWildcards = txtWildcards.Text;
            Properties.Settings.Default.AddonTag1Index = cbxTag1.SelectedIndex;
            Properties.Settings.Default.AddonTag2Index = cbxTag2.SelectedIndex;
            Properties.Settings.Default.AddonTypeIndex = cbxAddonType.SelectedIndex;
            Properties.Settings.Default.TaskIntervalTime = tmrQueueRunner.Interval;
            Properties.Settings.Default.AutoRunTasks = chkAutoRunTask.Checked;
            Properties.Settings.Default.UseDefaultLocations = chkUseDefaultExe.Checked;
            Properties.Settings.Default.Save();
        }
    }
}
