using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace GMWU
{
    public partial class GMWU : Form
    {
        // Declare variables that will be used
        IntPtr dialog;
        string dialogResult;
        readonly List<GTask> list = new List<GTask>();
        readonly List<string> addons = new List<string>();
        readonly List<string> content = new List<string>();
        Image newImg;

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
                // This task is for creating a new .GMA that will be created through GMad.exe
                case 0:
                    // Checks if the input is blank or the locations of the required content are not valid
                    if (textBoxesAreBlank(txtAFLocation, txtGFLocation, txtGMAOutput)
                        || checkLocationsBeingInvalid(txtAFLocation, txtGFLocation, txtGMAOutput, 0))
                        return;

                    // If the task has no name, give it a default name, else set it to what the user entered
                    if (string.IsNullOrWhiteSpace(tbxTaskName.Text))
                        newTask.TaskName = "Create .GMA";
                    else
                        newTask.TaskName = tbxTaskName.Text;

                    // If no .GMA file name was provided, give it a default name, else set it to what the user entered
                    if (string.IsNullOrWhiteSpace(txtGMFileName.Text))
                        txtGMFileName.Text = "newgma";

                    // Store the location of the file used to create the .GMA in the GTask FileName property
                    newTask.FileName = txtGFLocation.Text;

                    // Store the full name (incluidng .GMA file extension) in the GTask GMAName property
                    newTask.GMAName = txtGMFileName.Text + ".gma";

                    // Stores the arguments needed to create a gma, by creating a .GMA from a folder, and writing the .GMA to an output location)
                    newTask.Arguments = "create -folder \"" + txtAFLocation.Text + "\" -out \"" + txtGMAOutput.Text + "\\" + newTask.GMAName + "\"";
                    break;
                // This task is for extrafting a .GMA that will be output to a specific location
                case 1:
                    // Checks if the input is blank or the locations of the required content are not valid
                    if (textBoxesAreBlank(txtCOLoc, txtGMAFLoc, txtGFLoc)
                        || checkLocationsBeingInvalid(txtCOLoc, txtGMAFLoc, txtGFLoc, 1))
                        return;

                    // If the task has no name, give it a default name, else set it to what the user entered
                    if (string.IsNullOrWhiteSpace(tbxTaskName.Text))
                        newTask.TaskName = "Extract .GMA";
                    else
                        newTask.TaskName = tbxTaskName.Text;

                    // Store the location of the file used to extract the .GMA in the GTask FileName property
                    newTask.FileName = txtGMAFLoc.Text;

                    // Store the full name (incluidng .GMA file extension) in the GTask GMAName property
                    newTask.GMAName = txtGFLoc.Text.Substring(txtGFLoc.Text.LastIndexOf("\\") + 1);

                    // Store the location that will be used to output the contents of the extracted .GMA
                    newTask.FolderLocation = Path.Combine(txtCOLoc.Text, newTask.GMAName.Substring(0, newTask.GMAName.IndexOf(".")));

                    // Store the arguments needed to extract a gma, by seelcting a file, and writing the .GMA's contents to an output location)
                    newTask.Arguments = "extract -file \"" + txtGFLoc.Text + "\" -out \"" + newTask.FolderLocation + "\"";
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
                tinyfd.tinyfd_messageBox("Input Error", "Please fill in all required fields before adding it to the task queue!", "ok", "error", 1);
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
                        tinyfd.tinyfd_messageBox("Input Error", "Make sure all locations provided are valid", "ok", "error", 1);
                        return true;
                    }
                    break;
                case 1:
                    // The second task looks at a directory (Addon folder output for extraction), a file (GMad.exe Location), and another file (.GMA file location)
                    if (!Directory.Exists(T1.Text) || !File.Exists(T2.Text) || !File.Exists(T3.Text))
                    {
                        tinyfd.tinyfd_messageBox("Input Error", "Make sure all locations provided are valid", "ok", "error", 1);
                        return true;
                    }
                    break;
                case 2:
                    // The third task looks at a file (Icon used for addon in the workshop), another file (GMPublish.exe Location), and another file (.GMA file location)
                    if (!File.Exists(T1.Text) || !File.Exists(T2.Text) || !File.Exists(T3.Text))
                    {
                        tinyfd.tinyfd_messageBox("Input Error", "Make sure all locations provided are valid", "ok", "error", 1);
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
                tinyfd.tinyfd_messageBox("Selection Error", "Please select a valid task from the queue", "ok", "error", 1);
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

            // At the moment, I want to make this cross-platform, and in case there are any errors, they are logged to the Errors console window
            try
            {
                // Start the new process
                process.Start();

                // Disable being able to run new tasks until the current one is finished
                btnRunTask.Enabled = false;

                // Check that the process still has work to do
                while (!process.StandardOutput.EndOfStream)

                    // Print what the process is outputting to the console
                    rtbConsole.Text += process.StandardOutput.ReadLine() + Environment.NewLine;

                // Formatting to keep everything spaced
                rtbConsole.Text += Environment.NewLine;

                // Enable being able to run new tasks since the current one is finished
                btnRunTask.Enabled = true;

                // Remove the task from the queue and the "list" data structure
                list.RemoveAt(lbxQueue.SelectedIndex);
                lbxQueue.Items.RemoveAt(lbxQueue.SelectedIndex);
            }
            catch (Exception ex)
            {
                // If an exception occurs after the process is started, log the error and also this prevents the button from not being clickable
                rtbErrors.Text += $"[{DateTime.Now.ToString("HH:mm:ss tt")}] {ex}{Environment.NewLine}{Environment.NewLine}";
                btnRunTask.Enabled = true;
            }
        }

        /**
         * A helper method that loads folder dialogs for addon and gma input/output
         */
        private void loadFolderDialog(string title, string defaultPath, TextBox T1)
        {
            dialog = tinyfd.tinyfd_selectFolderDialog(title, defaultPath);
            dialogResult = stringFromAnsi(dialog);
            if (!string.IsNullOrWhiteSpace(dialogResult))
                T1.Text = dialogResult;
        }

        /**
         * A helper method that loads file dialogs for GMad and GMPublish.exe, prevents redundancy
         */
        private void loadFileDialog(string title, string defaultPath, int numFilters, string[] filterPatterns, string filterDescription, int allowMultipleSelects, TextBox T1)
        {
            dialog = tinyfd.tinyfd_openFileDialog(title, defaultPath, numFilters, filterPatterns, filterDescription, allowMultipleSelects);
            dialogResult = stringFromAnsi(dialog);
            if (!string.IsNullOrWhiteSpace(dialogResult))
                T1.Text = dialogResult;
        }

        private void btnAFLocation_Click(object sender, EventArgs e)
        {
            loadFolderDialog("Select addon folder", string.Empty, txtAFLocation);
        }

        private void btnGFLocation_Click(object sender, EventArgs e)
        {
            loadFileDialog("Select gmad.exe file", string.Empty, 1, new string[] { "gmad.exe" }, "GMad File (gmad.exe)", 0, txtGFLocation);
        }

        private void btnGMAOutput_Click(object sender, EventArgs e)
        {
            loadFolderDialog("Select folder for to put the resulting .GMA in", string.Empty, txtGMAOutput);
        }

        private void btnCOLoc_Click(object sender, EventArgs e)
        {
            loadFolderDialog("Select folder for .GMA content output", string.Empty, txtCOLoc);
        }

        private void btnGMAFLoc_Click(object sender, EventArgs e)
        {
            loadFileDialog("Select gmad.exe file", string.Empty, 1, new string[] { "gmad.exe" }, "GMad File (gmad.exe)", 0, txtGMAFLoc);
        }

        private void btnGFLoc_Click(object sender, EventArgs e)
        {
            loadFileDialog("Select gmad.exe file", string.Empty, 1, new string[] { "gmad.exe" }, "GMad File (gmad.exe)", 0, txtGFLoc);
        }

        private void btnJILoc_Click(object sender, EventArgs e)
        {
            dialog = tinyfd.tinyfd_openFileDialog("Select .jpg file", string.Empty, 1, new string[] { "*.jpg" }, "JPG File (*.jpg)", 0);
            dialogResult = stringFromAnsi(dialog);
            if (!string.IsNullOrWhiteSpace(dialogResult))
            {
                if (!dialogResult.Contains(".jpg"))
                {
                    tinyfd.tinyfd_messageBox("Image Load Error", "This image is not a .jpg", "ok", "error", 1);
                    return;
                }

                try
                {
                    newImg = Image.FromFile(dialogResult);
                    if (newImg.Width != 512 || newImg.Height != 512)
                    {
                        if (pbxIcon.Image != null)
                            pbxIcon.Image.Dispose();
                        tinyfd.tinyfd_messageBox("Image Load Error", "The specified image is not 512x512", "ok", "error", 1);
                        return;
                    }
                    pbxIcon.Image = newImg;
                    txtJILoc.Text = dialogResult;
                }
                catch (Exception ex)
                {
                    rtbErrors.Text += $"[{DateTime.Now.ToString("HH:mm:ss tt")}] {ex}{Environment.NewLine}{Environment.NewLine}";
                }
            }
        }

        private void btnGMPULoc_Click(object sender, EventArgs e)
        {
            loadFileDialog("Select gmpublish.exe file", string.Empty, 1, new string[] { "gmpublish.exe" }, "GMPublish File (gmpublish.exe)", 0, txtGMPULoc);
        }

        private void btnGMAFILoc_Click(object sender, EventArgs e)
        {
            loadFileDialog("Select gmad.exe file", string.Empty, 1, new string[] { "gmad.exe" }, "GMad File (gmad.exe)", 0, txtGMAFILoc);
        }

        private void btnDeleteTask_Click(object sender, EventArgs e)
        {
            if (lbxQueue.SelectedIndex > lbxQueue.Items.Count || lbxQueue.SelectedIndex == -1)
            {
                tinyfd.tinyfd_messageBox("Selection Error", "Please select a valid task from the queue", "ok", "error", 1);
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
        }

        private void btnLoadAddons_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtDefGMPFile.Text))
            {
                tinyfd.tinyfd_messageBox("No Location Provided", "You need to enter in the GMPublish.exe file in the Settings area of this program", "ok", "error", 1);
                return;
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

            addonList.Start();
            content.Clear();

            while (!addonList.StandardOutput.EndOfStream)
                content.Add(addonList.StandardOutput.ReadLine().Trim());

            for (int i = 0; i < content.Count; i++)
                if (!(i < 5))
                    lbxAddonList.Items.Add(content[i]);

            lbxAddonList.Items.RemoveAt(lbxAddonList.Items.Count - 1);
        }

        private void btnDefGMPUFile_Click(object sender, EventArgs e)
        {
            loadFileDialog("Select gmpublish.exe file", string.Empty, 1, new string[] { "gmpublish.exe" }, "GMPublish File (gmpublish.exe)", 0, txtDefGMPFile);
        }
    }

    public class tinyfd
    {
        const string fileDialogDll = "tinyfiledialogs32.dll";

        // Cross-platform file dialogs
        [DllImport(fileDialogDll, CallingConvention = CallingConvention.Cdecl)] public static extern void tinyfd_beep();

        [DllImport(fileDialogDll, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int tinyfd_notifyPopup(string aTitle, string aMessage, string aIconType);

        [DllImport(fileDialogDll, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int tinyfd_messageBox(string aTitle, string aMessage, string aDialogTyle, string aIconType, int aDefaultButton);

        [DllImport(fileDialogDll, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr tinyfd_inputBox(string aTitle, string aMessage, string aDefaultInput);

        [DllImport(fileDialogDll, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr tinyfd_saveFileDialog(string aTitle, string aDefaultPathAndFile, int aNumOfFilterPatterns, string[] aFilterPatterns, string aSingleFilterDescription);

        [DllImport(fileDialogDll, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr tinyfd_openFileDialog(string aTitle, string aDefaultPathAndFile, int aNumOfFilterPatterns, string[] aFilterPatterns, string aSingleFilterDescription, int aAllowMultipleSelects);

        [DllImport(fileDialogDll, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr tinyfd_selectFolderDialog(string aTitle, string aDefaultPathAndFile);

        [DllImport(fileDialogDll, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr tinyfd_colorChooser(string aTitle, string aDefaultHexRGB, byte[] aDefaultRGB, byte[] aoResultRGB);
    }
}
