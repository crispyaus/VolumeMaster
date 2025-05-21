namespace VolumeMaster_Windows
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            hamIcon = new NotifyIcon(components);
            contextMenuStrip1 = new ContextMenuStrip(components);
            loadConfigFileToolStripMenuItem = new ToolStripMenuItem();
            toolStripMenuItem1 = new ToolStripMenuItem();
            exitToolStripMenuItem = new ToolStripMenuItem();
            openConfigDialog = new OpenFileDialog();
            mainTabControl = new TabControl();
            tabControllers = new TabPage();
            buttonAddController = new Button();
            controllerListView = new ListView();
            columnHeaderID = new ColumnHeader();
            columnHeaderApps = new ColumnHeader();
            columnHeaderVM = new ColumnHeader();
            panelControllerDetails = new Panel();
            idSelectLabel = new Label();
            idSelect = new NumericUpDown();
            panelConfigType = new Panel();
            buttonAppSelect = new Button();
            buttonVMSelect = new Button();
            processSelectionPanel = new Panel();
            processSearchBox = new TextBox();
            processListView = new ListView();
            columnProcessName = new ColumnHeader();
            columnProcessId = new ColumnHeader();
            runningProcessesLabel = new Label();
            buttonAddProcess = new Button();
            listBoxSelectedProcesses = new ListBox();
            processContextMenu = new ContextMenuStrip(components);
            removeProcessMenuItem = new ToolStripMenuItem();
            label1 = new Label();
            tabSettings = new TabPage();
            groupBoxVoicemeeter = new GroupBox();
            textBoxVersion = new TextBox();
            label8 = new Label();
            VMbutton = new Button();
            label7 = new Label();
            groupBoxSerialPort = new GroupBox();
            comboBox1 = new ComboBox();
            labelCOMPort = new Label();
            panelHeader = new Panel();
            panelLogo = new Panel();
            labelTitle = new Label();
            buttonMinimize = new Button();
            buttonExit = new Button();
            saveButton = new Button();
            contextMenuStrip1.SuspendLayout();
            mainTabControl.SuspendLayout();
            tabControllers.SuspendLayout();
            panelControllerDetails.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)idSelect).BeginInit();
            panelConfigType.SuspendLayout();
            processSelectionPanel.SuspendLayout();
            processContextMenu.SuspendLayout();
            tabSettings.SuspendLayout();
            groupBoxVoicemeeter.SuspendLayout();
            groupBoxSerialPort.SuspendLayout();
            panelHeader.SuspendLayout();
            SuspendLayout();
            // 
            // hamIcon
            // 
            hamIcon.ContextMenuStrip = contextMenuStrip1;
            hamIcon.Icon = (Icon)resources.GetObject("hamIcon.Icon");
            hamIcon.Text = "VoiceMaster";
            hamIcon.Visible = true;
            hamIcon.MouseDoubleClick += hamIcon_MouseDoubleClick;
            hamIcon.MouseDown += notifyIcon1_MouseClick;
            // 
            // contextMenuStrip1
            // 
            contextMenuStrip1.ImageScalingSize = new Size(24, 24);
            contextMenuStrip1.Items.AddRange(new ToolStripItem[] { loadConfigFileToolStripMenuItem, toolStripMenuItem1, exitToolStripMenuItem });
            contextMenuStrip1.Name = "contextMenuStrip1";
            contextMenuStrip1.Size = new Size(213, 100);
            // 
            // loadConfigFileToolStripMenuItem
            // 
            loadConfigFileToolStripMenuItem.Name = "loadConfigFileToolStripMenuItem";
            loadConfigFileToolStripMenuItem.Size = new Size(212, 32);
            loadConfigFileToolStripMenuItem.Text = "Load Config File";
            loadConfigFileToolStripMenuItem.Click += loadConfigFileToolStripMenuItem_Click;
            // 
            // toolStripMenuItem1
            // 
            toolStripMenuItem1.Name = "toolStripMenuItem1";
            toolStripMenuItem1.Size = new Size(212, 32);
            toolStripMenuItem1.Text = "Documentation";
            toolStripMenuItem1.Click += toolStripMenuItem1_Click;
            // 
            // exitToolStripMenuItem
            // 
            exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            exitToolStripMenuItem.Size = new Size(212, 32);
            exitToolStripMenuItem.Text = "Exit";
            exitToolStripMenuItem.Click += exitToolStripMenuItem_Click;
            // 
            // openConfigDialog
            // 
            openConfigDialog.FileName = "openConfigDialog";
            // 
            // mainTabControl
            // 
            mainTabControl.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            mainTabControl.Controls.Add(tabControllers);
            mainTabControl.Controls.Add(tabSettings);
            mainTabControl.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            mainTabControl.Location = new Point(20, 70);
            mainTabControl.Name = "mainTabControl";
            mainTabControl.Padding = new Point(12, 6);
            mainTabControl.SelectedIndex = 0;
            mainTabControl.Size = new Size(860, 630);
            mainTabControl.TabIndex = 23;
            // 
            // tabControllers
            // 
            tabControllers.BackColor = Color.FromArgb(30, 32, 47);
            tabControllers.Controls.Add(buttonAddController);
            tabControllers.Controls.Add(controllerListView);
            tabControllers.Controls.Add(panelControllerDetails);
            tabControllers.Location = new Point(4, 37);
            tabControllers.Name = "tabControllers";
            tabControllers.Padding = new Padding(3);
            tabControllers.Size = new Size(852, 589);
            tabControllers.TabIndex = 0;
            tabControllers.Text = "Controllers";
            // 
            // buttonAddController
            // 
            buttonAddController.BackColor = Color.FromArgb(123, 104, 238);
            buttonAddController.FlatAppearance.BorderSize = 0;
            buttonAddController.FlatStyle = FlatStyle.Flat;
            buttonAddController.Font = new Font("Segoe UI Semibold", 9.5F, FontStyle.Bold, GraphicsUnit.Point);
            buttonAddController.ForeColor = Color.White;
            buttonAddController.Location = new Point(15, 533);
            buttonAddController.Name = "buttonAddController";
            buttonAddController.Size = new Size(190, 38);
            buttonAddController.TabIndex = 2;
            buttonAddController.Text = "+ Add Controller";
            buttonAddController.UseVisualStyleBackColor = false;
            buttonAddController.Click += buttonAddController_Click;
            // 
            // controllerListView
            // 
            controllerListView.BackColor = Color.FromArgb(40, 42, 60);
            controllerListView.BorderStyle = BorderStyle.None;
            controllerListView.Columns.AddRange(new ColumnHeader[] { columnHeaderID, columnHeaderApps, columnHeaderVM });
            controllerListView.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            controllerListView.ForeColor = Color.White;
            controllerListView.FullRowSelect = true;
            controllerListView.HeaderStyle = ColumnHeaderStyle.Nonclickable;
            controllerListView.Location = new Point(15, 15);
            controllerListView.Name = "controllerListView";
            controllerListView.Size = new Size(190, 512);
            controllerListView.TabIndex = 1;
            controllerListView.UseCompatibleStateImageBehavior = false;
            controllerListView.View = View.Details;
            controllerListView.SelectedIndexChanged += controllerListView_SelectedIndexChanged;
            // 
            // columnHeaderID
            // 
            columnHeaderID.Text = "ID";
            columnHeaderID.Width = 30;
            // 
            // columnHeaderApps
            // 
            columnHeaderApps.Text = "Applications";
            columnHeaderApps.Width = 80;
            // 
            // columnHeaderVM
            // 
            columnHeaderVM.Text = "VM";
            columnHeaderVM.Width = 80;
            // 
            // panelControllerDetails
            // 
            panelControllerDetails.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            panelControllerDetails.BackColor = Color.FromArgb(35, 37, 52);
            panelControllerDetails.Controls.Add(idSelectLabel);
            panelControllerDetails.Controls.Add(idSelect);
            panelControllerDetails.Controls.Add(panelConfigType);
            panelControllerDetails.Controls.Add(processSelectionPanel);
            panelControllerDetails.Controls.Add(label1);
            panelControllerDetails.Location = new Point(220, 15);
            panelControllerDetails.Name = "panelControllerDetails";
            panelControllerDetails.Size = new Size(617, 559);
            panelControllerDetails.TabIndex = 0;
            // 
            // idSelectLabel
            // 
            idSelectLabel.AutoSize = true;
            idSelectLabel.Font = new Font("Segoe UI Semibold", 11F, FontStyle.Bold, GraphicsUnit.Point);
            idSelectLabel.ForeColor = Color.White;
            idSelectLabel.Location = new Point(17, 15);
            idSelectLabel.Name = "idSelectLabel";
            idSelectLabel.Size = new Size(143, 30);
            idSelectLabel.TabIndex = 24;
            idSelectLabel.Text = "Controller ID:";
            // 
            // idSelect
            // 
            idSelect.BackColor = Color.FromArgb(50, 53, 70);
            idSelect.BorderStyle = BorderStyle.None;
            idSelect.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            idSelect.ForeColor = Color.White;
            idSelect.Location = new Point(166, 15);
            idSelect.Margin = new Padding(4, 5, 4, 5);
            idSelect.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            idSelect.Name = "idSelect";
            idSelect.Size = new Size(77, 35);
            idSelect.TabIndex = 23;
            idSelect.TextAlign = HorizontalAlignment.Center;
            idSelect.Value = new decimal(new int[] { 1, 0, 0, 0 });
            idSelect.ValueChanged += idSelect_ValueChanged;
            // 
            // panelConfigType
            // 
            panelConfigType.BackColor = Color.FromArgb(30, 32, 47);
            panelConfigType.Controls.Add(buttonAppSelect);
            panelConfigType.Controls.Add(buttonVMSelect);
            panelConfigType.Location = new Point(17, 62);
            panelConfigType.Name = "panelConfigType";
            panelConfigType.Size = new Size(367, 56);
            panelConfigType.TabIndex = 22;
            // 
            // buttonAppSelect
            // 
            buttonAppSelect.BackColor = Color.FromArgb(123, 104, 238);
            buttonAppSelect.FlatAppearance.BorderSize = 0;
            buttonAppSelect.FlatStyle = FlatStyle.Flat;
            buttonAppSelect.Font = new Font("Segoe UI Semibold", 9.5F, FontStyle.Bold, GraphicsUnit.Point);
            buttonAppSelect.ForeColor = Color.White;
            buttonAppSelect.Location = new Point(10, 8);
            buttonAppSelect.Margin = new Padding(4, 5, 4, 5);
            buttonAppSelect.Name = "buttonAppSelect";
            buttonAppSelect.Size = new Size(168, 42);
            buttonAppSelect.TabIndex = 0;
            buttonAppSelect.Text = "Applications";
            buttonAppSelect.UseVisualStyleBackColor = false;
            buttonAppSelect.Click += buttonAppSelect_Click;
            // 
            // buttonVMSelect
            // 
            buttonVMSelect.BackColor = Color.FromArgb(50, 53, 70);
            buttonVMSelect.FlatAppearance.BorderSize = 0;
            buttonVMSelect.FlatStyle = FlatStyle.Flat;
            buttonVMSelect.Font = new Font("Segoe UI Semibold", 9.5F, FontStyle.Bold, GraphicsUnit.Point);
            buttonVMSelect.ForeColor = Color.White;
            buttonVMSelect.Location = new Point(188, 8);
            buttonVMSelect.Margin = new Padding(4, 5, 4, 5);
            buttonVMSelect.Name = "buttonVMSelect";
            buttonVMSelect.Size = new Size(168, 42);
            buttonVMSelect.TabIndex = 1;
            buttonVMSelect.Text = "VoiceMeeter";
            buttonVMSelect.UseVisualStyleBackColor = false;
            buttonVMSelect.Click += buttonVMSelect_Click;
            // 
            // processSelectionPanel
            // 
            processSelectionPanel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            processSelectionPanel.BackColor = Color.FromArgb(35, 37, 52);
            processSelectionPanel.Controls.Add(processSearchBox);
            processSelectionPanel.Controls.Add(processListView);
            processSelectionPanel.Controls.Add(runningProcessesLabel);
            processSelectionPanel.Controls.Add(buttonAddProcess);
            processSelectionPanel.Controls.Add(listBoxSelectedProcesses);
            processSelectionPanel.Location = new Point(17, 132);
            processSelectionPanel.Name = "processSelectionPanel";
            processSelectionPanel.Size = new Size(583, 411);
            processSelectionPanel.TabIndex = 21;
            // 
            // processSearchBox
            // 
            processSearchBox.BackColor = Color.FromArgb(50, 53, 70);
            processSearchBox.BorderStyle = BorderStyle.None;
            processSearchBox.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            processSearchBox.ForeColor = Color.White;
            processSearchBox.Location = new Point(10, 45);
            processSearchBox.Name = "processSearchBox";
            processSearchBox.PlaceholderText = "Search processes...";
            processSearchBox.Size = new Size(238, 27);
            processSearchBox.TabIndex = 28;
            processSearchBox.TextChanged += processSearchBox_TextChanged;
            // 
            // processListView
            // 
            processListView.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            processListView.BackColor = Color.FromArgb(40, 42, 60);
            processListView.BorderStyle = BorderStyle.None;
            processListView.Columns.AddRange(new ColumnHeader[] { columnProcessName, columnProcessId });
            processListView.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            processListView.ForeColor = Color.White;
            processListView.FullRowSelect = true;
            processListView.HeaderStyle = ColumnHeaderStyle.None;
            processListView.Location = new Point(10, 77);
            processListView.Name = "processListView";
            processListView.Size = new Size(238, 318);
            processListView.TabIndex = 27;
            processListView.UseCompatibleStateImageBehavior = false;
            processListView.View = View.Details;
            // 
            // columnProcessName
            // 
            columnProcessName.Text = "Process Name";
            columnProcessName.Width = 190;
            // 
            // columnProcessId
            // 
            columnProcessId.Text = "PID";
            columnProcessId.Width = 40;
            // 
            // runningProcessesLabel
            // 
            runningProcessesLabel.AutoSize = true;
            runningProcessesLabel.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold, GraphicsUnit.Point);
            runningProcessesLabel.ForeColor = Color.White;
            runningProcessesLabel.Location = new Point(10, 15);
            runningProcessesLabel.Name = "runningProcessesLabel";
            runningProcessesLabel.Size = new Size(175, 28);
            runningProcessesLabel.TabIndex = 26;
            runningProcessesLabel.Text = "Running Processes";
            // 
            // buttonAddProcess
            // 
            buttonAddProcess.BackColor = Color.FromArgb(123, 104, 238);
            buttonAddProcess.FlatAppearance.BorderSize = 0;
            buttonAddProcess.FlatStyle = FlatStyle.Flat;
            buttonAddProcess.Font = new Font("Segoe UI", 11F, FontStyle.Bold, GraphicsUnit.Point);
            buttonAddProcess.ForeColor = Color.White;
            buttonAddProcess.Location = new Point(256, 174);
            buttonAddProcess.Name = "buttonAddProcess";
            buttonAddProcess.Size = new Size(54, 42);
            buttonAddProcess.TabIndex = 1;
            buttonAddProcess.Text = "→";
            buttonAddProcess.UseVisualStyleBackColor = false;
            buttonAddProcess.Click += buttonAddProcess_Click;
            // 
            // listBoxSelectedProcesses
            // 
            listBoxSelectedProcesses.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            listBoxSelectedProcesses.BackColor = Color.FromArgb(40, 42, 60);
            listBoxSelectedProcesses.BorderStyle = BorderStyle.None;
            listBoxSelectedProcesses.ContextMenuStrip = processContextMenu;
            listBoxSelectedProcesses.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            listBoxSelectedProcesses.ForeColor = Color.White;
            listBoxSelectedProcesses.FormattingEnabled = true;
            listBoxSelectedProcesses.ItemHeight = 25;
            listBoxSelectedProcesses.Location = new Point(316, 45);
            listBoxSelectedProcesses.Name = "listBoxSelectedProcesses";
            listBoxSelectedProcesses.Size = new Size(254, 350);
            listBoxSelectedProcesses.TabIndex = 0;
            listBoxSelectedProcesses.SelectedIndexChanged += listBoxSelectedProcesses_SelectedIndexChanged;
            // 
            // processContextMenu
            // 
            processContextMenu.ImageScalingSize = new Size(24, 24);
            processContextMenu.Items.AddRange(new ToolStripItem[] { removeProcessMenuItem });
            processContextMenu.Name = "processContextMenu";
            processContextMenu.Size = new Size(155, 36);
            // 
            // removeProcessMenuItem
            // 
            removeProcessMenuItem.ForeColor = Color.Red;
            removeProcessMenuItem.Name = "removeProcessMenuItem";
            removeProcessMenuItem.Size = new Size(154, 32);
            removeProcessMenuItem.Text = "Remove";
            removeProcessMenuItem.Click += removeProcessMenuItem_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold, GraphicsUnit.Point);
            label1.ForeColor = Color.White;
            label1.Location = new Point(316, 15);
            label1.Name = "label1";
            label1.Size = new Size(167, 28);
            label1.TabIndex = 20;
            label1.Text = "Selected Process";
            // 
            // tabSettings
            // 
            tabSettings.BackColor = Color.FromArgb(30, 32, 47);
            tabSettings.Controls.Add(groupBoxVoicemeeter);
            tabSettings.Controls.Add(groupBoxSerialPort);
            tabSettings.Location = new Point(4, 37);
            tabSettings.Name = "tabSettings";
            tabSettings.Padding = new Padding(3);
            tabSettings.Size = new Size(852, 589);
            tabSettings.TabIndex = 1;
            tabSettings.Text = "Settings";
            // 
            // groupBoxVoicemeeter
            // 
            groupBoxVoicemeeter.Controls.Add(textBoxVersion);
            groupBoxVoicemeeter.Controls.Add(label8);
            groupBoxVoicemeeter.Controls.Add(VMbutton);
            groupBoxVoicemeeter.Controls.Add(label7);
            groupBoxVoicemeeter.FlatStyle = FlatStyle.Flat;
            groupBoxVoicemeeter.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold, GraphicsUnit.Point);
            groupBoxVoicemeeter.ForeColor = Color.White;
            groupBoxVoicemeeter.Location = new Point(27, 178);
            groupBoxVoicemeeter.Name = "groupBoxVoicemeeter";
            groupBoxVoicemeeter.Size = new Size(463, 147);
            groupBoxVoicemeeter.TabIndex = 1;
            groupBoxVoicemeeter.TabStop = false;
            groupBoxVoicemeeter.Text = "VoiceMeeter Settings";
            // 
            // textBoxVersion
            // 
            textBoxVersion.BackColor = Color.FromArgb(50, 53, 70);
            textBoxVersion.BorderStyle = BorderStyle.None;
            textBoxVersion.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            textBoxVersion.ForeColor = Color.White;
            textBoxVersion.Location = new Point(182, 89);
            textBoxVersion.Margin = new Padding(4, 5, 4, 5);
            textBoxVersion.Name = "textBoxVersion";
            textBoxVersion.Size = new Size(174, 27);
            textBoxVersion.TabIndex = 24;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            label8.ForeColor = Color.White;
            label8.Location = new Point(22, 89);
            label8.Margin = new Padding(4, 0, 4, 0);
            label8.Name = "label8";
            label8.Size = new Size(81, 28);
            label8.TabIndex = 23;
            label8.Text = "Version:";
            // 
            // VMbutton
            // 
            VMbutton.BackColor = Color.FromArgb(50, 53, 70);
            VMbutton.FlatAppearance.BorderSize = 0;
            VMbutton.FlatStyle = FlatStyle.Flat;
            VMbutton.Font = new Font("Segoe UI Semibold", 9.5F, FontStyle.Bold, GraphicsUnit.Point);
            VMbutton.ForeColor = Color.White;
            VMbutton.Location = new Point(182, 37);
            VMbutton.Margin = new Padding(4, 5, 4, 5);
            VMbutton.Name = "VMbutton";
            VMbutton.Size = new Size(171, 42);
            VMbutton.TabIndex = 22;
            VMbutton.Text = "Disabled";
            VMbutton.UseVisualStyleBackColor = false;
            VMbutton.Click += VMbutton_Click;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            label7.ForeColor = Color.White;
            label7.Location = new Point(22, 43);
            label7.Margin = new Padding(4, 0, 4, 0);
            label7.Name = "label7";
            label7.Size = new Size(124, 28);
            label7.TabIndex = 21;
            label7.Text = "VoiceMeeter:";
            // 
            // groupBoxSerialPort
            // 
            groupBoxSerialPort.Controls.Add(comboBox1);
            groupBoxSerialPort.Controls.Add(labelCOMPort);
            groupBoxSerialPort.FlatStyle = FlatStyle.Flat;
            groupBoxSerialPort.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold, GraphicsUnit.Point);
            groupBoxSerialPort.ForeColor = Color.White;
            groupBoxSerialPort.Location = new Point(27, 26);
            groupBoxSerialPort.Name = "groupBoxSerialPort";
            groupBoxSerialPort.Size = new Size(463, 136);
            groupBoxSerialPort.TabIndex = 0;
            groupBoxSerialPort.TabStop = false;
            groupBoxSerialPort.Text = "Connection Settings";
            // 
            // comboBox1
            // 
            comboBox1.AllowDrop = true;
            comboBox1.BackColor = Color.FromArgb(50, 53, 70);
            comboBox1.FlatStyle = FlatStyle.Flat;
            comboBox1.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            comboBox1.ForeColor = Color.White;
            comboBox1.FormattingEnabled = true;
            comboBox1.Location = new Point(182, 55);
            comboBox1.Margin = new Padding(4, 5, 4, 5);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(173, 36);
            comboBox1.TabIndex = 15;
            comboBox1.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
            comboBox1.Click += comboBox1_Click;
            // 
            // labelCOMPort
            // 
            labelCOMPort.AutoSize = true;
            labelCOMPort.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            labelCOMPort.ForeColor = Color.White;
            labelCOMPort.Location = new Point(22, 55);
            labelCOMPort.Margin = new Padding(4, 0, 4, 0);
            labelCOMPort.Name = "labelCOMPort";
            labelCOMPort.Size = new Size(102, 28);
            labelCOMPort.TabIndex = 16;
            labelCOMPort.Text = "COM Port:";
            // 
            // panelHeader
            // 
            panelHeader.BackColor = Color.FromArgb(123, 104, 238);
            panelHeader.Controls.Add(panelLogo);
            panelHeader.Controls.Add(labelTitle);
            panelHeader.Controls.Add(buttonMinimize);
            panelHeader.Controls.Add(buttonExit);
            panelHeader.Dock = DockStyle.Top;
            panelHeader.Location = new Point(0, 0);
            panelHeader.Name = "panelHeader";
            panelHeader.Size = new Size(900, 58);
            panelHeader.TabIndex = 24;
            panelHeader.MouseDown += panelHeader_MouseDown;
            // 
            // panelLogo
            // 
            panelLogo.BackColor = Color.Transparent;
            panelLogo.Location = new Point(20, 12);
            panelLogo.Name = "panelLogo";
            panelLogo.Size = new Size(32, 32);
            panelLogo.TabIndex = 3;
            panelLogo.Paint += PanelLogo_Paint;
            // 
            // labelTitle
            // 
            labelTitle.AutoSize = true;
            labelTitle.Font = new Font("Segoe UI", 14F, FontStyle.Bold, GraphicsUnit.Point);
            labelTitle.ForeColor = Color.White;
            labelTitle.Location = new Point(58, 12);
            labelTitle.Name = "labelTitle";
            labelTitle.Size = new Size(195, 38);
            labelTitle.TabIndex = 2;
            labelTitle.Text = "VolumeMaster";
            // 
            // buttonMinimize
            // 
            buttonMinimize.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            buttonMinimize.BackColor = Color.FromArgb(123, 104, 238);
            buttonMinimize.FlatAppearance.BorderSize = 0;
            buttonMinimize.FlatStyle = FlatStyle.Flat;
            buttonMinimize.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            buttonMinimize.ForeColor = Color.White;
            buttonMinimize.Location = new Point(821, 12);
            buttonMinimize.Name = "buttonMinimize";
            buttonMinimize.Size = new Size(32, 32);
            buttonMinimize.TabIndex = 1;
            buttonMinimize.Text = "_";
            buttonMinimize.UseVisualStyleBackColor = false;
            buttonMinimize.Click += buttonMinimize_Click;
            // 
            // buttonExit
            // 
            buttonExit.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            buttonExit.BackColor = Color.FromArgb(123, 104, 238);
            buttonExit.FlatAppearance.BorderSize = 0;
            buttonExit.FlatStyle = FlatStyle.Flat;
            buttonExit.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            buttonExit.ForeColor = Color.White;
            buttonExit.Location = new Point(857, 12);
            buttonExit.Name = "buttonExit";
            buttonExit.Size = new Size(32, 32);
            buttonExit.TabIndex = 0;
            buttonExit.Text = "X";
            buttonExit.UseVisualStyleBackColor = false;
            buttonExit.Click += buttonExit_Click;
            // 
            // saveButton
            // 
            saveButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            saveButton.BackColor = Color.FromArgb(123, 104, 238);
            saveButton.FlatAppearance.BorderSize = 0;
            saveButton.FlatStyle = FlatStyle.Flat;
            saveButton.Font = new Font("Segoe UI Semibold", 11F, FontStyle.Bold, GraphicsUnit.Point);
            saveButton.ForeColor = Color.White;
            saveButton.Location = new Point(759, 706);
            saveButton.Margin = new Padding(4, 5, 4, 5);
            saveButton.Name = "saveButton";
            saveButton.Size = new Size(121, 50);
            saveButton.TabIndex = 25;
            saveButton.Text = "Save All";
            saveButton.UseVisualStyleBackColor = false;
            saveButton.Click += saveButton_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoValidate = AutoValidate.EnableAllowFocusChange;
            BackColor = Color.FromArgb(22, 24, 33);
            CausesValidation = false;
            ClientSize = new Size(900, 770);
            Controls.Add(saveButton);
            Controls.Add(panelHeader);
            Controls.Add(mainTabControl);
            FormBorderStyle = FormBorderStyle.Sizable;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(4, 5, 4, 5);
            MaximizeBox = false;
            Name = "Form1";
            RightToLeft = RightToLeft.No;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "VolumeMaster";
            FormClosing += Form1_FormClosing;
            Load += Form1_Load;
            DragLeave += Form1_Resize;
            Resize += Form1_Resize;
            contextMenuStrip1.ResumeLayout(false);
            mainTabControl.ResumeLayout(false);
            tabControllers.ResumeLayout(false);
            panelControllerDetails.ResumeLayout(false);
            panelControllerDetails.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)idSelect).EndInit();
            panelConfigType.ResumeLayout(false);
            processSelectionPanel.ResumeLayout(false);
            processSelectionPanel.PerformLayout();
            processContextMenu.ResumeLayout(false);
            tabSettings.ResumeLayout(false);
            groupBoxVoicemeeter.ResumeLayout(false);
            groupBoxVoicemeeter.PerformLayout();
            groupBoxSerialPort.ResumeLayout(false);
            groupBoxSerialPort.PerformLayout();
            panelHeader.ResumeLayout(false);
            panelHeader.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private NotifyIcon hamIcon;
        private ContextMenuStrip contextMenuStrip1;
        private ToolStripMenuItem exitToolStripMenuItem;
        private ComboBox comboBox1;
        private Label label1;
        private Button saveButton;
        private NumericUpDown idSelect;
        private ToolStripMenuItem loadConfigFileToolStripMenuItem;
        private ToolStripMenuItem toolStripMenuItem1;
        private OpenFileDialog openConfigDialog;
        private Label label7;
        private Button VMbutton;
        private Label label8;
        private TextBox textBoxVersion;
        private Button buttonAppSelect;
        private Button buttonVMSelect;
        private TabControl mainTabControl;
        private TabPage tabControllers;
        private TabPage tabSettings;
        private Panel panelHeader;
        private Button buttonMinimize;
        private Button buttonExit;
        private Label labelTitle;
        private GroupBox groupBoxSerialPort;
        private Label labelCOMPort;
        private GroupBox groupBoxVoicemeeter;
        private Panel panelControllerDetails;
        private Panel processSelectionPanel;
        private ListBox listBoxSelectedProcesses;
        private Button buttonAddProcess;
        private Panel panelConfigType;
        private ListView controllerListView;
        private ColumnHeader columnHeaderID;
        private ColumnHeader columnHeaderApps;
        private ColumnHeader columnHeaderVM;
        private Button buttonAddController;
        private Label idSelectLabel;
        private ContextMenuStrip processContextMenu;
        private ToolStripMenuItem removeProcessMenuItem;
        private TextBox processSearchBox;
        private ListView processListView;
        private ColumnHeader columnProcessName;
        private ColumnHeader columnProcessId;
        private Label runningProcessesLabel;
        private Panel panelLogo;
    }
}