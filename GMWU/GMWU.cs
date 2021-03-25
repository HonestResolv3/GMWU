using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace GMWU
{
    public partial class GMWU : Form
    {
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

                    newTask.AddonInputLocation = txtAFLocation.Text;
                    newTask.GMadFileLocation = txtGFLocation.Text;
                    newTask.GMAOutputLocation = txtGMAOutput.Text;
                    if (string.IsNullOrWhiteSpace(tbxTaskName.Text))
                        newTask.TaskName = "Create .GMA";
                    else
                        newTask.TaskName = tbxTaskName.Text;
                    break;
                case 1:
                    if (textBoxesAreBlank(txtGOLoc, txtGMAFLoc, txtGFLoc))
                        return;

                    newTask.AddonOutputLocation = txtGOLoc.Text;
                    newTask.GMadFileLocation = txtGMAFLoc.Text;
                    newTask.GMAInputLocation = txtGFLoc.Text;
                    if (string.IsNullOrWhiteSpace(tbxTaskName.Text))
                        newTask.TaskName = "Extract .GMA";
                    else
                        newTask.TaskName = tbxTaskName.Text;
                    break;
                case 2:
                    break;
                default:
                    break;
            }
            newTask.TaskNotes = tbxTaskNotes.Text;
            lbxQueue.Items.Add(newTask);
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
                MessageBox.Show("Please fill in all required fields before adding it to the task queue!", "Error");
                return true;
            }
            return false;
        }
    }
}
