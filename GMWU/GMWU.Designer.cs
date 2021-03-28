
namespace GMWU
{
    partial class GMWU
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GMWU));
            this.btnAdd2Queue = new System.Windows.Forms.Button();
            this.gboxTaskInfo = new System.Windows.Forms.GroupBox();
            this.btnRunTask = new System.Windows.Forms.Button();
            this.tbxTaskNotes = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tbxTaskName = new System.Windows.Forms.TextBox();
            this.lblTaskNotes = new System.Windows.Forms.Label();
            this.lblTextName = new System.Windows.Forms.Label();
            this.tctrlMainArea = new System.Windows.Forms.TabControl();
            this.tpgCreatingTasks = new System.Windows.Forms.TabPage();
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.pbxIcon = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tctrlTasks = new System.Windows.Forms.TabControl();
            this.tpgCreateGMA = new System.Windows.Forms.TabPage();
            this.lblGMAFileName = new System.Windows.Forms.Label();
            this.txtGMFileName = new System.Windows.Forms.TextBox();
            this.txtGMAOutput = new System.Windows.Forms.TextBox();
            this.txtGFLocation = new System.Windows.Forms.TextBox();
            this.txtAFLocation = new System.Windows.Forms.TextBox();
            this.btnGMAOutput = new System.Windows.Forms.Button();
            this.btnGFLocation = new System.Windows.Forms.Button();
            this.btnAFLocation = new System.Windows.Forms.Button();
            this.tpgExtractGMA = new System.Windows.Forms.TabPage();
            this.txtGFLoc = new System.Windows.Forms.TextBox();
            this.txtGMAFLoc = new System.Windows.Forms.TextBox();
            this.txtGOLoc = new System.Windows.Forms.TextBox();
            this.btnGFLoc = new System.Windows.Forms.Button();
            this.btnGMAFLoc = new System.Windows.Forms.Button();
            this.btnGOLoc = new System.Windows.Forms.Button();
            this.tpgPublishAddon = new System.Windows.Forms.TabPage();
            this.tpgUpdateAddon = new System.Windows.Forms.TabPage();
            this.tpgUpdateIcon = new System.Windows.Forms.TabPage();
            this.tpgJSCreator = new System.Windows.Forms.TabPage();
            this.tpgPresetCreator = new System.Windows.Forms.TabPage();
            this.tpgSettings = new System.Windows.Forms.TabPage();
            this.tpgAbout = new System.Windows.Forms.TabPage();
            this.btnSteam = new System.Windows.Forms.Button();
            this.btnGitHub = new System.Windows.Forms.Button();
            this.lblCreatedByTitle = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.tpgChangelog = new System.Windows.Forms.TabPage();
            this.tctrlConsole = new System.Windows.Forms.TabControl();
            this.tpgConsole = new System.Windows.Forms.TabPage();
            this.rtbConsole = new System.Windows.Forms.RichTextBox();
            this.tpgErrors = new System.Windows.Forms.TabPage();
            this.rtbErrors = new System.Windows.Forms.RichTextBox();
            this.tctrlTaskArea = new System.Windows.Forms.TabControl();
            this.tpgQueue = new System.Windows.Forms.TabPage();
            this.lbxQueue = new System.Windows.Forms.ListBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.gboxTaskInfo.SuspendLayout();
            this.tctrlMainArea.SuspendLayout();
            this.tpgCreatingTasks.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbxIcon)).BeginInit();
            this.tctrlTasks.SuspendLayout();
            this.tpgCreateGMA.SuspendLayout();
            this.tpgExtractGMA.SuspendLayout();
            this.tpgAbout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.tctrlConsole.SuspendLayout();
            this.tpgConsole.SuspendLayout();
            this.tpgErrors.SuspendLayout();
            this.tctrlTaskArea.SuspendLayout();
            this.tpgQueue.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnAdd2Queue
            // 
            this.btnAdd2Queue.Location = new System.Drawing.Point(110, 97);
            this.btnAdd2Queue.Name = "btnAdd2Queue";
            this.btnAdd2Queue.Size = new System.Drawing.Size(129, 23);
            this.btnAdd2Queue.TabIndex = 6;
            this.btnAdd2Queue.Text = "Add Task to Queue";
            this.btnAdd2Queue.UseVisualStyleBackColor = true;
            this.btnAdd2Queue.Click += new System.EventHandler(this.btnAdd2Queue_Click);
            // 
            // gboxTaskInfo
            // 
            this.gboxTaskInfo.Controls.Add(this.btnRunTask);
            this.gboxTaskInfo.Controls.Add(this.btnAdd2Queue);
            this.gboxTaskInfo.Controls.Add(this.tbxTaskNotes);
            this.gboxTaskInfo.Controls.Add(this.label3);
            this.gboxTaskInfo.Controls.Add(this.tbxTaskName);
            this.gboxTaskInfo.Controls.Add(this.lblTaskNotes);
            this.gboxTaskInfo.Controls.Add(this.lblTextName);
            this.gboxTaskInfo.Location = new System.Drawing.Point(9, 189);
            this.gboxTaskInfo.Name = "gboxTaskInfo";
            this.gboxTaskInfo.Size = new System.Drawing.Size(463, 132);
            this.gboxTaskInfo.TabIndex = 1;
            this.gboxTaskInfo.TabStop = false;
            this.gboxTaskInfo.Text = "Task Information";
            // 
            // btnRunTask
            // 
            this.btnRunTask.Location = new System.Drawing.Point(245, 97);
            this.btnRunTask.Name = "btnRunTask";
            this.btnRunTask.Size = new System.Drawing.Size(129, 23);
            this.btnRunTask.TabIndex = 9;
            this.btnRunTask.Text = "Run Task";
            this.btnRunTask.UseVisualStyleBackColor = true;
            this.btnRunTask.Click += new System.EventHandler(this.btnRunTask_Click);
            // 
            // tbxTaskNotes
            // 
            this.tbxTaskNotes.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.tbxTaskNotes.Location = new System.Drawing.Point(76, 58);
            this.tbxTaskNotes.Name = "tbxTaskNotes";
            this.tbxTaskNotes.Size = new System.Drawing.Size(379, 22);
            this.tbxTaskNotes.TabIndex = 8;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 102);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(0, 13);
            this.label3.TabIndex = 2;
            // 
            // tbxTaskName
            // 
            this.tbxTaskName.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.tbxTaskName.Location = new System.Drawing.Point(76, 22);
            this.tbxTaskName.Name = "tbxTaskName";
            this.tbxTaskName.Size = new System.Drawing.Size(379, 22);
            this.tbxTaskName.TabIndex = 7;
            // 
            // lblTaskNotes
            // 
            this.lblTaskNotes.AutoSize = true;
            this.lblTaskNotes.Location = new System.Drawing.Point(5, 61);
            this.lblTaskNotes.Name = "lblTaskNotes";
            this.lblTaskNotes.Size = new System.Drawing.Size(65, 13);
            this.lblTaskNotes.TabIndex = 1;
            this.lblTaskNotes.Text = "Task Notes:";
            // 
            // lblTextName
            // 
            this.lblTextName.AutoSize = true;
            this.lblTextName.Location = new System.Drawing.Point(5, 25);
            this.lblTextName.Name = "lblTextName";
            this.lblTextName.Size = new System.Drawing.Size(64, 13);
            this.lblTextName.TabIndex = 0;
            this.lblTextName.Text = "Task Name:";
            // 
            // tctrlMainArea
            // 
            this.tctrlMainArea.Controls.Add(this.tpgCreatingTasks);
            this.tctrlMainArea.Controls.Add(this.tpgJSCreator);
            this.tctrlMainArea.Controls.Add(this.tpgPresetCreator);
            this.tctrlMainArea.Controls.Add(this.tpgSettings);
            this.tctrlMainArea.Controls.Add(this.tpgAbout);
            this.tctrlMainArea.Controls.Add(this.tpgChangelog);
            this.tctrlMainArea.Location = new System.Drawing.Point(13, 12);
            this.tctrlMainArea.Name = "tctrlMainArea";
            this.tctrlMainArea.SelectedIndex = 0;
            this.tctrlMainArea.Size = new System.Drawing.Size(686, 358);
            this.tctrlMainArea.TabIndex = 2;
            // 
            // tpgCreatingTasks
            // 
            this.tpgCreatingTasks.Controls.Add(this.button1);
            this.tpgCreatingTasks.Controls.Add(this.groupBox1);
            this.tpgCreatingTasks.Controls.Add(this.tctrlTasks);
            this.tpgCreatingTasks.Controls.Add(this.gboxTaskInfo);
            this.tpgCreatingTasks.Location = new System.Drawing.Point(4, 22);
            this.tpgCreatingTasks.Name = "tpgCreatingTasks";
            this.tpgCreatingTasks.Padding = new System.Windows.Forms.Padding(3);
            this.tpgCreatingTasks.Size = new System.Drawing.Size(678, 332);
            this.tpgCreatingTasks.TabIndex = 0;
            this.tpgCreatingTasks.Text = "Creating Tasks";
            this.tpgCreatingTasks.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(513, 255);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(129, 23);
            this.button1.TabIndex = 9;
            this.button1.Text = "Reset Image";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.pbxIcon);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(483, 20);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(188, 198);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Preview Icon";
            // 
            // pbxIcon
            // 
            this.pbxIcon.Location = new System.Drawing.Point(5, 16);
            this.pbxIcon.Name = "pbxIcon";
            this.pbxIcon.Size = new System.Drawing.Size(177, 177);
            this.pbxIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbxIcon.TabIndex = 3;
            this.pbxIcon.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 102);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 13);
            this.label1.TabIndex = 2;
            // 
            // tctrlTasks
            // 
            this.tctrlTasks.Controls.Add(this.tpgCreateGMA);
            this.tctrlTasks.Controls.Add(this.tpgExtractGMA);
            this.tctrlTasks.Controls.Add(this.tpgPublishAddon);
            this.tctrlTasks.Controls.Add(this.tpgUpdateAddon);
            this.tctrlTasks.Controls.Add(this.tpgUpdateIcon);
            this.tctrlTasks.Location = new System.Drawing.Point(6, 8);
            this.tctrlTasks.Name = "tctrlTasks";
            this.tctrlTasks.SelectedIndex = 0;
            this.tctrlTasks.Size = new System.Drawing.Size(472, 176);
            this.tctrlTasks.TabIndex = 0;
            // 
            // tpgCreateGMA
            // 
            this.tpgCreateGMA.Controls.Add(this.lblGMAFileName);
            this.tpgCreateGMA.Controls.Add(this.txtGMFileName);
            this.tpgCreateGMA.Controls.Add(this.txtGMAOutput);
            this.tpgCreateGMA.Controls.Add(this.txtGFLocation);
            this.tpgCreateGMA.Controls.Add(this.txtAFLocation);
            this.tpgCreateGMA.Controls.Add(this.btnGMAOutput);
            this.tpgCreateGMA.Controls.Add(this.btnGFLocation);
            this.tpgCreateGMA.Controls.Add(this.btnAFLocation);
            this.tpgCreateGMA.Location = new System.Drawing.Point(4, 22);
            this.tpgCreateGMA.Name = "tpgCreateGMA";
            this.tpgCreateGMA.Padding = new System.Windows.Forms.Padding(3);
            this.tpgCreateGMA.Size = new System.Drawing.Size(464, 150);
            this.tpgCreateGMA.TabIndex = 0;
            this.tpgCreateGMA.Text = "Create .GMA File";
            this.tpgCreateGMA.UseVisualStyleBackColor = true;
            // 
            // lblGMAFileName
            // 
            this.lblGMAFileName.AutoSize = true;
            this.lblGMAFileName.Location = new System.Drawing.Point(9, 115);
            this.lblGMAFileName.Name = "lblGMAFileName";
            this.lblGMAFileName.Size = new System.Drawing.Size(91, 13);
            this.lblGMAFileName.TabIndex = 10;
            this.lblGMAFileName.Text = ".GMA File Name:";
            // 
            // txtGMFileName
            // 
            this.txtGMFileName.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtGMFileName.Location = new System.Drawing.Point(109, 111);
            this.txtGMFileName.Name = "txtGMFileName";
            this.txtGMFileName.Size = new System.Drawing.Size(347, 22);
            this.txtGMFileName.TabIndex = 6;
            // 
            // txtGMAOutput
            // 
            this.txtGMAOutput.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtGMAOutput.Location = new System.Drawing.Point(169, 80);
            this.txtGMAOutput.Name = "txtGMAOutput";
            this.txtGMAOutput.Size = new System.Drawing.Size(287, 22);
            this.txtGMAOutput.TabIndex = 5;
            // 
            // txtGFLocation
            // 
            this.txtGFLocation.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtGFLocation.Location = new System.Drawing.Point(169, 49);
            this.txtGFLocation.Name = "txtGFLocation";
            this.txtGFLocation.Size = new System.Drawing.Size(287, 22);
            this.txtGFLocation.TabIndex = 3;
            // 
            // txtAFLocation
            // 
            this.txtAFLocation.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtAFLocation.Location = new System.Drawing.Point(169, 18);
            this.txtAFLocation.Name = "txtAFLocation";
            this.txtAFLocation.Size = new System.Drawing.Size(287, 22);
            this.txtAFLocation.TabIndex = 1;
            // 
            // btnGMAOutput
            // 
            this.btnGMAOutput.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGMAOutput.Location = new System.Drawing.Point(8, 79);
            this.btnGMAOutput.Name = "btnGMAOutput";
            this.btnGMAOutput.Size = new System.Drawing.Size(155, 23);
            this.btnGMAOutput.TabIndex = 4;
            this.btnGMAOutput.Text = ".GMA Output Location";
            this.btnGMAOutput.UseVisualStyleBackColor = true;
            // 
            // btnGFLocation
            // 
            this.btnGFLocation.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGFLocation.Location = new System.Drawing.Point(8, 48);
            this.btnGFLocation.Name = "btnGFLocation";
            this.btnGFLocation.Size = new System.Drawing.Size(155, 23);
            this.btnGFLocation.TabIndex = 2;
            this.btnGFLocation.Text = "GMad.EXE File Location";
            this.btnGFLocation.UseVisualStyleBackColor = true;
            // 
            // btnAFLocation
            // 
            this.btnAFLocation.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAFLocation.Location = new System.Drawing.Point(8, 16);
            this.btnAFLocation.Name = "btnAFLocation";
            this.btnAFLocation.Size = new System.Drawing.Size(155, 23);
            this.btnAFLocation.TabIndex = 0;
            this.btnAFLocation.Text = "Addon Folder Location";
            this.btnAFLocation.UseVisualStyleBackColor = true;
            // 
            // tpgExtractGMA
            // 
            this.tpgExtractGMA.Controls.Add(this.txtGFLoc);
            this.tpgExtractGMA.Controls.Add(this.txtGMAFLoc);
            this.tpgExtractGMA.Controls.Add(this.txtGOLoc);
            this.tpgExtractGMA.Controls.Add(this.btnGFLoc);
            this.tpgExtractGMA.Controls.Add(this.btnGMAFLoc);
            this.tpgExtractGMA.Controls.Add(this.btnGOLoc);
            this.tpgExtractGMA.Location = new System.Drawing.Point(4, 22);
            this.tpgExtractGMA.Name = "tpgExtractGMA";
            this.tpgExtractGMA.Padding = new System.Windows.Forms.Padding(3);
            this.tpgExtractGMA.Size = new System.Drawing.Size(464, 150);
            this.tpgExtractGMA.TabIndex = 1;
            this.tpgExtractGMA.Text = "Extract .GMA File";
            this.tpgExtractGMA.UseVisualStyleBackColor = true;
            // 
            // txtGFLoc
            // 
            this.txtGFLoc.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtGFLoc.Location = new System.Drawing.Point(169, 110);
            this.txtGFLoc.Name = "txtGFLoc";
            this.txtGFLoc.Size = new System.Drawing.Size(287, 22);
            this.txtGFLoc.TabIndex = 11;
            // 
            // txtGMAFLoc
            // 
            this.txtGMAFLoc.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtGMAFLoc.Location = new System.Drawing.Point(169, 62);
            this.txtGMAFLoc.Name = "txtGMAFLoc";
            this.txtGMAFLoc.Size = new System.Drawing.Size(287, 22);
            this.txtGMAFLoc.TabIndex = 9;
            // 
            // txtGOLoc
            // 
            this.txtGOLoc.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtGOLoc.Location = new System.Drawing.Point(169, 16);
            this.txtGOLoc.Name = "txtGOLoc";
            this.txtGOLoc.Size = new System.Drawing.Size(287, 22);
            this.txtGOLoc.TabIndex = 7;
            // 
            // btnGFLoc
            // 
            this.btnGFLoc.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGFLoc.Location = new System.Drawing.Point(8, 109);
            this.btnGFLoc.Name = "btnGFLoc";
            this.btnGFLoc.Size = new System.Drawing.Size(155, 23);
            this.btnGFLoc.TabIndex = 10;
            this.btnGFLoc.Text = ".GMA File Location";
            this.btnGFLoc.UseVisualStyleBackColor = true;
            // 
            // btnGMAFLoc
            // 
            this.btnGMAFLoc.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGMAFLoc.Location = new System.Drawing.Point(8, 61);
            this.btnGMAFLoc.Name = "btnGMAFLoc";
            this.btnGMAFLoc.Size = new System.Drawing.Size(155, 23);
            this.btnGMAFLoc.TabIndex = 8;
            this.btnGMAFLoc.Text = "GMad.EXE File Location";
            this.btnGMAFLoc.UseVisualStyleBackColor = true;
            // 
            // btnGOLoc
            // 
            this.btnGOLoc.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGOLoc.Location = new System.Drawing.Point(8, 15);
            this.btnGOLoc.Name = "btnGOLoc";
            this.btnGOLoc.Size = new System.Drawing.Size(155, 23);
            this.btnGOLoc.TabIndex = 6;
            this.btnGOLoc.Text = ".GMA Output Location";
            this.btnGOLoc.UseVisualStyleBackColor = true;
            // 
            // tpgPublishAddon
            // 
            this.tpgPublishAddon.Location = new System.Drawing.Point(4, 22);
            this.tpgPublishAddon.Name = "tpgPublishAddon";
            this.tpgPublishAddon.Padding = new System.Windows.Forms.Padding(3);
            this.tpgPublishAddon.Size = new System.Drawing.Size(464, 150);
            this.tpgPublishAddon.TabIndex = 2;
            this.tpgPublishAddon.Text = "Publish Addon";
            this.tpgPublishAddon.UseVisualStyleBackColor = true;
            // 
            // tpgUpdateAddon
            // 
            this.tpgUpdateAddon.Location = new System.Drawing.Point(4, 22);
            this.tpgUpdateAddon.Name = "tpgUpdateAddon";
            this.tpgUpdateAddon.Padding = new System.Windows.Forms.Padding(3);
            this.tpgUpdateAddon.Size = new System.Drawing.Size(464, 150);
            this.tpgUpdateAddon.TabIndex = 3;
            this.tpgUpdateAddon.Text = "Update Addon";
            this.tpgUpdateAddon.UseVisualStyleBackColor = true;
            // 
            // tpgUpdateIcon
            // 
            this.tpgUpdateIcon.Location = new System.Drawing.Point(4, 22);
            this.tpgUpdateIcon.Name = "tpgUpdateIcon";
            this.tpgUpdateIcon.Padding = new System.Windows.Forms.Padding(3);
            this.tpgUpdateIcon.Size = new System.Drawing.Size(464, 150);
            this.tpgUpdateIcon.TabIndex = 4;
            this.tpgUpdateIcon.Text = "Update Icon";
            this.tpgUpdateIcon.UseVisualStyleBackColor = true;
            // 
            // tpgJSCreator
            // 
            this.tpgJSCreator.Location = new System.Drawing.Point(4, 22);
            this.tpgJSCreator.Name = "tpgJSCreator";
            this.tpgJSCreator.Padding = new System.Windows.Forms.Padding(3);
            this.tpgJSCreator.Size = new System.Drawing.Size(678, 332);
            this.tpgJSCreator.TabIndex = 1;
            this.tpgJSCreator.Text = ".JSON Creator";
            this.tpgJSCreator.UseVisualStyleBackColor = true;
            // 
            // tpgPresetCreator
            // 
            this.tpgPresetCreator.Location = new System.Drawing.Point(4, 22);
            this.tpgPresetCreator.Name = "tpgPresetCreator";
            this.tpgPresetCreator.Padding = new System.Windows.Forms.Padding(3);
            this.tpgPresetCreator.Size = new System.Drawing.Size(678, 332);
            this.tpgPresetCreator.TabIndex = 2;
            this.tpgPresetCreator.Text = "Preset Creator";
            this.tpgPresetCreator.UseVisualStyleBackColor = true;
            // 
            // tpgSettings
            // 
            this.tpgSettings.Location = new System.Drawing.Point(4, 22);
            this.tpgSettings.Name = "tpgSettings";
            this.tpgSettings.Padding = new System.Windows.Forms.Padding(3);
            this.tpgSettings.Size = new System.Drawing.Size(678, 332);
            this.tpgSettings.TabIndex = 3;
            this.tpgSettings.Text = "Settings";
            this.tpgSettings.UseVisualStyleBackColor = true;
            // 
            // tpgAbout
            // 
            this.tpgAbout.Controls.Add(this.btnSteam);
            this.tpgAbout.Controls.Add(this.btnGitHub);
            this.tpgAbout.Controls.Add(this.lblCreatedByTitle);
            this.tpgAbout.Controls.Add(this.pictureBox1);
            this.tpgAbout.Location = new System.Drawing.Point(4, 22);
            this.tpgAbout.Name = "tpgAbout";
            this.tpgAbout.Padding = new System.Windows.Forms.Padding(3);
            this.tpgAbout.Size = new System.Drawing.Size(678, 332);
            this.tpgAbout.TabIndex = 4;
            this.tpgAbout.Text = "About";
            this.tpgAbout.UseVisualStyleBackColor = true;
            // 
            // btnSteam
            // 
            this.btnSteam.Location = new System.Drawing.Point(346, 277);
            this.btnSteam.Name = "btnSteam";
            this.btnSteam.Size = new System.Drawing.Size(96, 23);
            this.btnSteam.TabIndex = 3;
            this.btnSteam.Text = "Steam Page";
            this.btnSteam.UseVisualStyleBackColor = true;
            this.btnSteam.Click += new System.EventHandler(this.btnSteam_Click);
            // 
            // btnGitHub
            // 
            this.btnGitHub.Location = new System.Drawing.Point(218, 277);
            this.btnGitHub.Name = "btnGitHub";
            this.btnGitHub.Size = new System.Drawing.Size(96, 23);
            this.btnGitHub.TabIndex = 2;
            this.btnGitHub.Text = "GitHub Page";
            this.btnGitHub.UseVisualStyleBackColor = true;
            this.btnGitHub.Click += new System.EventHandler(this.btnGitHub_Click);
            // 
            // lblCreatedByTitle
            // 
            this.lblCreatedByTitle.AutoSize = true;
            this.lblCreatedByTitle.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblCreatedByTitle.Location = new System.Drawing.Point(204, 26);
            this.lblCreatedByTitle.Name = "lblCreatedByTitle";
            this.lblCreatedByTitle.Size = new System.Drawing.Size(250, 25);
            this.lblCreatedByTitle.TabIndex = 1;
            this.lblCreatedByTitle.Text = "Created by: TruthfullyHonest";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::GMWU.Properties.Resources.C_Pi;
            this.pictureBox1.Location = new System.Drawing.Point(244, 72);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(177, 177);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // tpgChangelog
            // 
            this.tpgChangelog.Location = new System.Drawing.Point(4, 22);
            this.tpgChangelog.Name = "tpgChangelog";
            this.tpgChangelog.Padding = new System.Windows.Forms.Padding(3);
            this.tpgChangelog.Size = new System.Drawing.Size(678, 332);
            this.tpgChangelog.TabIndex = 5;
            this.tpgChangelog.Text = "Changelog";
            this.tpgChangelog.UseVisualStyleBackColor = true;
            // 
            // tctrlConsole
            // 
            this.tctrlConsole.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tctrlConsole.Controls.Add(this.tpgConsole);
            this.tctrlConsole.Controls.Add(this.tpgErrors);
            this.tctrlConsole.Location = new System.Drawing.Point(13, 372);
            this.tctrlConsole.Name = "tctrlConsole";
            this.tctrlConsole.SelectedIndex = 0;
            this.tctrlConsole.Size = new System.Drawing.Size(1113, 256);
            this.tctrlConsole.TabIndex = 3;
            // 
            // tpgConsole
            // 
            this.tpgConsole.Controls.Add(this.rtbConsole);
            this.tpgConsole.Location = new System.Drawing.Point(4, 22);
            this.tpgConsole.Name = "tpgConsole";
            this.tpgConsole.Padding = new System.Windows.Forms.Padding(3);
            this.tpgConsole.Size = new System.Drawing.Size(1105, 230);
            this.tpgConsole.TabIndex = 0;
            this.tpgConsole.Text = "Console";
            this.tpgConsole.UseVisualStyleBackColor = true;
            // 
            // rtbConsole
            // 
            this.rtbConsole.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rtbConsole.Cursor = System.Windows.Forms.Cursors.No;
            this.rtbConsole.Location = new System.Drawing.Point(6, 6);
            this.rtbConsole.Name = "rtbConsole";
            this.rtbConsole.ReadOnly = true;
            this.rtbConsole.Size = new System.Drawing.Size(1093, 221);
            this.rtbConsole.TabIndex = 0;
            this.rtbConsole.Text = "";
            // 
            // tpgErrors
            // 
            this.tpgErrors.Controls.Add(this.rtbErrors);
            this.tpgErrors.Location = new System.Drawing.Point(4, 22);
            this.tpgErrors.Name = "tpgErrors";
            this.tpgErrors.Padding = new System.Windows.Forms.Padding(3);
            this.tpgErrors.Size = new System.Drawing.Size(1105, 230);
            this.tpgErrors.TabIndex = 1;
            this.tpgErrors.Text = "Errors";
            this.tpgErrors.UseVisualStyleBackColor = true;
            // 
            // rtbErrors
            // 
            this.rtbErrors.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rtbErrors.Cursor = System.Windows.Forms.Cursors.No;
            this.rtbErrors.Location = new System.Drawing.Point(6, 6);
            this.rtbErrors.Name = "rtbErrors";
            this.rtbErrors.ReadOnly = true;
            this.rtbErrors.Size = new System.Drawing.Size(1093, 221);
            this.rtbErrors.TabIndex = 1;
            this.rtbErrors.Text = "";
            // 
            // tctrlTaskArea
            // 
            this.tctrlTaskArea.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tctrlTaskArea.Controls.Add(this.tpgQueue);
            this.tctrlTaskArea.Controls.Add(this.tabPage2);
            this.tctrlTaskArea.Location = new System.Drawing.Point(703, 12);
            this.tctrlTaskArea.Name = "tctrlTaskArea";
            this.tctrlTaskArea.SelectedIndex = 0;
            this.tctrlTaskArea.Size = new System.Drawing.Size(422, 358);
            this.tctrlTaskArea.TabIndex = 4;
            // 
            // tpgQueue
            // 
            this.tpgQueue.Controls.Add(this.lbxQueue);
            this.tpgQueue.Location = new System.Drawing.Point(4, 22);
            this.tpgQueue.Name = "tpgQueue";
            this.tpgQueue.Padding = new System.Windows.Forms.Padding(3);
            this.tpgQueue.Size = new System.Drawing.Size(414, 332);
            this.tpgQueue.TabIndex = 0;
            this.tpgQueue.Text = "Queue";
            this.tpgQueue.UseVisualStyleBackColor = true;
            // 
            // lbxQueue
            // 
            this.lbxQueue.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbxQueue.FormattingEnabled = true;
            this.lbxQueue.Location = new System.Drawing.Point(6, 7);
            this.lbxQueue.Name = "lbxQueue";
            this.lbxQueue.Size = new System.Drawing.Size(403, 316);
            this.lbxQueue.TabIndex = 0;
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(414, 332);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Addon List";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // GMWU
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1137, 640);
            this.Controls.Add(this.tctrlTaskArea);
            this.Controls.Add(this.tctrlConsole);
            this.Controls.Add(this.tctrlMainArea);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(1153, 679);
            this.Name = "GMWU";
            this.Text = "Garry\'s Mod Workshop Utility";
            this.gboxTaskInfo.ResumeLayout(false);
            this.gboxTaskInfo.PerformLayout();
            this.tctrlMainArea.ResumeLayout(false);
            this.tpgCreatingTasks.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbxIcon)).EndInit();
            this.tctrlTasks.ResumeLayout(false);
            this.tpgCreateGMA.ResumeLayout(false);
            this.tpgCreateGMA.PerformLayout();
            this.tpgExtractGMA.ResumeLayout(false);
            this.tpgExtractGMA.PerformLayout();
            this.tpgAbout.ResumeLayout(false);
            this.tpgAbout.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.tctrlConsole.ResumeLayout(false);
            this.tpgConsole.ResumeLayout(false);
            this.tpgErrors.ResumeLayout(false);
            this.tctrlTaskArea.ResumeLayout(false);
            this.tpgQueue.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnAdd2Queue;
        private System.Windows.Forms.GroupBox gboxTaskInfo;
        private System.Windows.Forms.TextBox tbxTaskNotes;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbxTaskName;
        private System.Windows.Forms.Label lblTaskNotes;
        private System.Windows.Forms.Label lblTextName;
        private System.Windows.Forms.TabControl tctrlMainArea;
        private System.Windows.Forms.TabPage tpgCreatingTasks;
        private System.Windows.Forms.TabPage tpgJSCreator;
        private System.Windows.Forms.TabControl tctrlConsole;
        private System.Windows.Forms.TabPage tpgConsole;
        private System.Windows.Forms.RichTextBox rtbConsole;
        private System.Windows.Forms.TabPage tpgErrors;
        private System.Windows.Forms.RichTextBox rtbErrors;
        private System.Windows.Forms.TabControl tctrlTasks;
        private System.Windows.Forms.TabPage tpgCreateGMA;
        private System.Windows.Forms.TextBox txtGMAOutput;
        private System.Windows.Forms.TextBox txtGFLocation;
        private System.Windows.Forms.TextBox txtAFLocation;
        private System.Windows.Forms.Button btnGMAOutput;
        private System.Windows.Forms.Button btnGFLocation;
        private System.Windows.Forms.Button btnAFLocation;
        private System.Windows.Forms.TabPage tpgExtractGMA;
        private System.Windows.Forms.TextBox txtGFLoc;
        private System.Windows.Forms.TextBox txtGMAFLoc;
        private System.Windows.Forms.TextBox txtGOLoc;
        private System.Windows.Forms.Button btnGFLoc;
        private System.Windows.Forms.Button btnGMAFLoc;
        private System.Windows.Forms.Button btnGOLoc;
        private System.Windows.Forms.TabPage tpgPublishAddon;
        private System.Windows.Forms.TabPage tpgPresetCreator;
        private System.Windows.Forms.TabPage tpgSettings;
        private System.Windows.Forms.TabPage tpgAbout;
        private System.Windows.Forms.TabPage tpgChangelog;
        private System.Windows.Forms.Button btnSteam;
        private System.Windows.Forms.Button btnGitHub;
        private System.Windows.Forms.Label lblCreatedByTitle;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TabPage tpgUpdateAddon;
        private System.Windows.Forms.TabPage tpgUpdateIcon;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.PictureBox pbxIcon;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabControl tctrlTaskArea;
        private System.Windows.Forms.TabPage tpgQueue;
        private System.Windows.Forms.ListBox lbxQueue;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button btnRunTask;
        private System.Windows.Forms.Label lblGMAFileName;
        private System.Windows.Forms.TextBox txtGMFileName;
    }
}

