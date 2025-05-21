using System.ComponentModel;
using System.Diagnostics;
using System.Drawing.Drawing2D;
using System.IO.Ports;
using System.Runtime.InteropServices;
using YamlDotNet.Serialization;


namespace VolumeMaster_Windows;

public partial class Form1 : Form
{
    // For draggable window
    [DllImport("user32.dll")]
    public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
    [DllImport("user32.dll")]
    public static extern bool ReleaseCapture();
    private const int WM_NCLBUTTONDOWN = 0xA1;
    private const int HT_CAPTION = 0x2;

    // For rounded corners
    [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
    private static extern IntPtr CreateRoundRectRgn
    (
        int nLeftRect,
        int nTopRect,
        int nRightRect,
        int nBottomRect,
        int nWidthEllipse,
        int nHeightEllipse
    );

    // UI Theme Colors
    private readonly Color primaryColor = Color.FromArgb(123, 104, 238); // Medium slate blue
    private readonly Color darkBackgroundColor = Color.FromArgb(22, 24, 33);
    private readonly Color panelBackgroundColor = Color.FromArgb(35, 37, 52);
    private readonly Color controlBackgroundColor = Color.FromArgb(50, 53, 70);
    private readonly Color listBackgroundColor = Color.FromArgb(40, 42, 60);

    Dictionary<string, string> appMaps = new();
    Dictionary<string, string> vmMap = new();
    Dictionary<string, string> btMap = new();

    public class Mapper
    {
        public Dictionary<string, AppsString> Mappings { get; set; }

        public Mapper()
        {
            // Initialize the Mappings dictionary in the constructor
            Mappings = new Dictionary<string, AppsString>();
        }

        public class AppsString
        {
            public string Applications { get; set; }
            public string VM { get; set; }
        }
    }

    Process backEndScript;

    static string defaultComport = "COM4";
    static string defaultBaudrate = "9600";
    static string defaultBytesize = "8";
    static string defaultParity = "N";
    static string defaultStopbits = "2";
    static string defaultVm = "N";
    static string defaultVmVersion = "banana";
    public string currentVm = defaultVm;
    public string yamlMap = "";
    public string yamlButtonMap = "";

    public int currentID = 1;
    public int currentView = 0; //0 is appview and 1 is vmview controls what is shown in the process list

    private List<string> selectedProcesses = new List<string>();

    // Add ImageList for process icons
    private ImageList processIcons;

    public Form1()
    {
        InitializeComponent();
        this.hamIcon.Icon = Properties.Resources.iconred;

        // Initialize process icons
        processIcons = new ImageList();
        processIcons.ColorDepth = ColorDepth.Depth32Bit;
        processIcons.ImageSize = new Size(16, 16);
        processListView.SmallImageList = processIcons;

        // Set UI theme colors
        SetThemeColors();
        
        // Start the watchdog thread
        Thread wd = new Thread(new ThreadStart(watchDog)); 
        wd.Start();
        
        // Ensure the form appears on startup
        this.Shown += Form1_Shown;
    }
    
    private void Form1_Shown(object sender, EventArgs e)
    {
        // Apply visual customizations when the form is fully loaded and shown
        try
        {
            ApplyRoundedCorners();
            
            // Style the controls
            StyleControls();
        }
        catch (Exception ex)
        {
            // Log error but don't crash
            Console.WriteLine("Error applying visual customizations: " + ex.Message);
        }
        
        // Make sure the form is visible
        this.Visible = true;
        this.BringToFront();
        this.Activate();
    }

    private void ApplyRoundedCorners()
    {
        try
        {
            // Main form rounded corners
            this.FormBorderStyle = FormBorderStyle.None;
            Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 20, 20));

            // Apply rounded corners to panels if they are created
            if (panelControllerDetails.IsHandleCreated && !panelControllerDetails.IsDisposed)
                panelControllerDetails.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, panelControllerDetails.Width, panelControllerDetails.Height, 15, 15));

            if (processSelectionPanel.IsHandleCreated && !processSelectionPanel.IsDisposed)
                processSelectionPanel.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, processSelectionPanel.Width, processSelectionPanel.Height, 15, 15));
                
            if (panelConfigType.IsHandleCreated && !panelConfigType.IsDisposed)
                panelConfigType.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, panelConfigType.Width, panelConfigType.Height, 15, 15));

            // Groups
            if (groupBoxSerialPort.IsHandleCreated && !groupBoxSerialPort.IsDisposed)
                groupBoxSerialPort.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, groupBoxSerialPort.Width, groupBoxSerialPort.Height, 15, 15));
                
            if (groupBoxVoicemeeter.IsHandleCreated && !groupBoxVoicemeeter.IsDisposed)
                groupBoxVoicemeeter.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, groupBoxVoicemeeter.Width, groupBoxVoicemeeter.Height, 15, 15));

            // Buttons
            ApplyRoundCornersToButtons();
            
            // Lists
            ApplyRoundCornersToLists();
        }
        catch (Exception ex)
        {
            MessageBox.Show("Error applying rounded corners: " + ex.Message);
        }
    }

    private void ApplyRoundCornersToButtons()
    {
        // Only apply if the handles are created and the controls are not disposed
        if (buttonAppSelect.IsHandleCreated && !buttonAppSelect.IsDisposed)
            buttonAppSelect.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, buttonAppSelect.Width, buttonAppSelect.Height, 10, 10));
            
        if (buttonVMSelect.IsHandleCreated && !buttonVMSelect.IsDisposed)
            buttonVMSelect.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, buttonVMSelect.Width, buttonVMSelect.Height, 10, 10));
            
        if (buttonAddProcess.IsHandleCreated && !buttonAddProcess.IsDisposed)
            buttonAddProcess.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, buttonAddProcess.Width, buttonAddProcess.Height, 10, 10));
            
        if (buttonAddController.IsHandleCreated && !buttonAddController.IsDisposed)
            buttonAddController.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, buttonAddController.Width, buttonAddController.Height, 15, 15));
            
        if (saveButton.IsHandleCreated && !saveButton.IsDisposed)
            saveButton.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, saveButton.Width, saveButton.Height, 15, 15));
            
        if (VMbutton.IsHandleCreated && !VMbutton.IsDisposed)
            VMbutton.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, VMbutton.Width, VMbutton.Height, 10, 10));
        
        if (buttonMinimize.IsHandleCreated && !buttonMinimize.IsDisposed)
            buttonMinimize.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, buttonMinimize.Width, buttonMinimize.Height, 8, 8));
            
        if (buttonExit.IsHandleCreated && !buttonExit.IsDisposed)
            buttonExit.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, buttonExit.Width, buttonExit.Height, 8, 8));
    }

    private void ApplyRoundCornersToLists()
    {
        if (processListView.IsHandleCreated && !processListView.IsDisposed)
            processListView.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, processListView.Width, processListView.Height, 15, 15));
            
        if (listBoxSelectedProcesses.IsHandleCreated && !listBoxSelectedProcesses.IsDisposed)
            listBoxSelectedProcesses.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, listBoxSelectedProcesses.Width, listBoxSelectedProcesses.Height, 15, 15));
            
        if (controllerListView.IsHandleCreated && !controllerListView.IsDisposed)
            controllerListView.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, controllerListView.Width, controllerListView.Height, 15, 15));
    }

    private void SetThemeColors()
    {
        // Custom drawing for tab control
        mainTabControl.DrawMode = TabDrawMode.OwnerDrawFixed;
        mainTabControl.DrawItem += MainTabControl_DrawItem;
        
        // Customize textbox and inputs when they're loaded
        this.Load += (s, e) =>
        {
            try
            {
                if (processSearchBox.IsHandleCreated && !processSearchBox.IsDisposed)
                    processSearchBox.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, processSearchBox.Width, processSearchBox.Height, 10, 10));
                
                if (textBoxVersion.IsHandleCreated && !textBoxVersion.IsDisposed)
                    textBoxVersion.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, textBoxVersion.Width, textBoxVersion.Height, 10, 10));
            }
            catch (Exception) 
            {
                // Ignore errors on controls that aren't ready yet
            }
        };
        
        // Add borders to GroupBoxes with safer initialization
        this.Load += (s, e) => 
        {
            try 
            {
                if (groupBoxSerialPort.IsHandleCreated && !groupBoxSerialPort.IsDisposed)
                    groupBoxSerialPort.Paint += GroupBox_Paint;
                
                if (groupBoxVoicemeeter.IsHandleCreated && !groupBoxVoicemeeter.IsDisposed)
                    groupBoxVoicemeeter.Paint += GroupBox_Paint;
            }
            catch (Exception) 
            {
                // Ignore errors on controls that aren't ready yet
            }
        };
    }

    private void GroupBox_Paint(object sender, PaintEventArgs e)
    {
        try
        {
            GroupBox groupBox = sender as GroupBox;
            if (groupBox == null) return;
            
            // Draw a custom rounded border for the GroupBox
            using (GraphicsPath gp = new GraphicsPath())
            {
                Rectangle rect = new Rectangle(0, 10, groupBox.Width - 1, groupBox.Height - 11);
                int radius = 15;
                
                gp.AddArc(rect.X, rect.Y, radius, radius, 180, 90);
                gp.AddArc(rect.X + rect.Width - radius, rect.Y, radius, radius, 270, 90);
                gp.AddArc(rect.X + rect.Width - radius, rect.Y + rect.Height - radius, radius, radius, 0, 90);
                gp.AddArc(rect.X, rect.Y + rect.Height - radius, radius, radius, 90, 90);
                gp.CloseAllFigures();
                
                using (Pen pen = new Pen(primaryColor, 1.5f))
                {
                    e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                    e.Graphics.DrawPath(pen, gp);
                }
                
                // Draw the title
                SizeF textSize = e.Graphics.MeasureString(groupBox.Text, groupBox.Font);
                e.Graphics.FillRectangle(new SolidBrush(groupBox.BackColor), 
                    new Rectangle(10, 0, (int)textSize.Width + 6, (int)textSize.Height));
                e.Graphics.DrawString(groupBox.Text, groupBox.Font, new SolidBrush(groupBox.ForeColor), 
                    new Point(13, 0));
            }
        }
        catch (Exception)
        {
            // Ignore painting errors that might occur during initialization
        }
    }

    private void MainTabControl_DrawItem(object sender, DrawItemEventArgs e)
    {
        try
        {
            // Custom drawing for tab control
            Graphics g = e.Graphics;
            TabPage tabPage = mainTabControl.TabPages[e.Index];
            Rectangle tabBounds = mainTabControl.GetTabRect(e.Index);
            
            // Adjusted radius for tabs
            int radius = 10;
            Rectangle modifiedBounds = new Rectangle(tabBounds.X, tabBounds.Y, tabBounds.Width, tabBounds.Height + radius);
            
            // Create a rounded rectangle path for the tab
            using (GraphicsPath path = new GraphicsPath())
            {
                path.AddArc(modifiedBounds.X, modifiedBounds.Y, radius, radius, 180, 90);
                path.AddArc(modifiedBounds.X + modifiedBounds.Width - radius, modifiedBounds.Y, radius, radius, 270, 90);
                path.AddLine(modifiedBounds.X + modifiedBounds.Width, modifiedBounds.Y + radius, 
                    modifiedBounds.X + modifiedBounds.Width, modifiedBounds.Y + modifiedBounds.Height);
                path.AddLine(modifiedBounds.X, modifiedBounds.Y + modifiedBounds.Height, 
                    modifiedBounds.X, modifiedBounds.Y + radius);
                path.CloseAllFigures();
                
                // Selected tab
                if (e.Index == mainTabControl.SelectedIndex)
                {
                    g.FillPath(new SolidBrush(primaryColor), path);
                }
                // Unselected tab
                else
                {
                    g.FillPath(new SolidBrush(controlBackgroundColor), path);
                }
            }

            // Text formatting
            StringFormat stringFormat = new StringFormat();
            stringFormat.Alignment = StringAlignment.Center;
            stringFormat.LineAlignment = StringAlignment.Center;
            
            // Draw text
            SolidBrush textBrush = new SolidBrush(Color.White);
            g.DrawString(tabPage.Text, tabPage.Font, textBrush, tabBounds, stringFormat);
            textBrush.Dispose();
        }
        catch (Exception)
        {
            // Ignore drawing errors during initialization
        }
    }

    protected override void OnSizeChanged(EventArgs e)
    {
        base.OnSizeChanged(e);
        
        try
        {
            // Reapply rounded corners on resize if the form is already created
            if (this.IsHandleCreated && !this.IsDisposed)
            {
                this.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, this.Width, this.Height, 20, 20));
            }
        }
        catch (Exception)
        {
            // Ignore errors during initialization or resizing
        }
    }

    private const int CP_DISABLE_CLOSE_BUTTON = 0x200;

    protected override CreateParams CreateParams
    {
        get
        {
            var cp = base.CreateParams;
            cp.ClassStyle = cp.ClassStyle | CP_DISABLE_CLOSE_BUTTON;
            return cp;
        }
    }

    private void Form1_Resize(object sender, EventArgs e)
    {
        //if the form is minimized  
        //hide it from the task bar  
        //and show the system tray icon (represented by the NotifyIcon control)  
        if (WindowState != FormWindowState.Minimized) return;
        Hide();
        WindowState = FormWindowState.Normal;
        hamIcon.Visible = true;
    }

    private void notifyIcon1_MouseClick(object sender, MouseEventArgs e)
    {
        if (e.Button == MouseButtons.Left)
        {
            Show();
            Show();
            WindowState = FormWindowState.Normal;
            Activate();
        }
    }

    private async void watchDog()
    {
        int i = 0;
        int d = 0;
        while (true)
        {
            await Task.Delay(3000);

            Process[] processes = Process.GetProcessesByName("VolumeMaster-Headless");
            if (processes.Length == 0)
            {
                this.hamIcon.Icon = Properties.Resources.iconred;
                if (i == 0)
                { hamIcon.ShowBalloonTip(1000, "VolumeMaster Disconnected", "Something's gone wrong - attempting to reconnect", ToolTipIcon.Warning); i = 1; }
                else { BackendControl(1); }

            }
            else
            {
                this.hamIcon.Icon = Properties.Resources.icongreen;
                if (i == 1) { i = 3; hamIcon.ShowBalloonTip(1000, "VolumeMaster Port Connected", "Successfully connected to a COM port, if it's not working check you have the right COM port", ToolTipIcon.None); } else if (i == 3) { i = 0; }
                d = 0;
            }
        }
    }

    private void exitToolStripMenuItem_Click(object sender, EventArgs e)
    {
        Application.Exit();
    }

    private void Form1_Load(object sender, EventArgs e)
    {
        loadConfig(@"config.yaml");
        BackendControl(1);
        RefreshProcessList();
        PopulateControllersListView();
    }

    private void BackendControl(int code)
    {
        if (code == 1)
            backEndScript = Process.Start(@"VolumeMaster-Headless.exe");
        else
            foreach (var process in Process.GetProcessesByName("VolumeMaster-Headless"))
                process.Kill();
    }

    private void comboBox1_Click(object sender, EventArgs e)
    {
        // Display each port name to the console.
        comboBox1.Items.Clear();
        comboBox1.Items.AddRange(SerialPort.GetPortNames());
    }

    private void saveButton_Click(object sender, EventArgs e)
    {
        saveConfig();
        this.hamIcon.Icon = Properties.Resources.icongreen;
        
        // Show a visual confirmation
        ShowSaveConfirmation();
    }
    
    private async void ShowSaveConfirmation()
    {
        saveButton.Text = "Saved!";
        saveButton.BackColor = Color.FromArgb(46, 204, 113); // Green color
        
        await Task.Delay(1000);
        
        saveButton.Text = "Save All";
        saveButton.BackColor = primaryColor;
    }

    private void PopulateControllersListView()
    {
        controllerListView.Items.Clear();
        
        foreach (var key in appMaps.Keys)
        {
            string id = key.Replace("ID", "");
            string apps = GetShortAppsList(appMaps[key]);
            string vm = GetShortVMList(vmMap.ContainsKey(key) ? vmMap[key] : "");
            
            ListViewItem item = new ListViewItem(id);
            item.SubItems.Add(apps);
            item.SubItems.Add(vm);
            item.Tag = key;
            
            controllerListView.Items.Add(item);
        }
    }

    private string GetShortAppsList(string apps)
    {
        if (string.IsNullOrEmpty(apps))
            return "-";
            
        string[] appArray = apps.Split(';');
        if (appArray.Length == 0)
            return "-";
        if (appArray.Length == 1)
            return appArray[0];
            
        return $"{appArray[0]} +{appArray.Length - 1}";
    }
    
    private string GetShortVMList(string vm)
    {
        if (string.IsNullOrEmpty(vm))
            return "-";
            
        string[] vmArray = vm.Split(';');
        if (vmArray.Length == 0)
            return "-";
        if (vmArray.Length == 1)
            return vmArray[0];
            
        return $"{vmArray[0]} +{vmArray.Length - 1}";
    }

    private void loadConfig(string path)
    {
        if (!File.Exists(path))
        {
            var newFile = File.Create(path);
            newFile.Close();
        }
        string text = File.ReadAllText(path); //read file

        //deserialize file
        var deserializer = new DeserializerBuilder().IgnoreUnmatchedProperties().Build();
        Config deserialized = deserializer.Deserialize<Config>(text);

        if (deserialized == null)
        {
            deserialized = new Config();
        }

        Mapper myMapper = new Mapper();

        //map do a dictionary in program
        appMaps.Clear();
        vmMap.Clear();
        if (deserialized.Mappings != null)
        {
            foreach (var (key, value) in deserialized.Mappings)
            {
                appMaps.Add(key, value.Applications);
                vmMap.Add(key, value.VM);

                myMapper.Mappings.Add(key, new Mapper.AppsString { Applications = deserialized.Mappings[key].Applications, VM = deserialized.Mappings[key].VM }); //loads into program for updated yamlstring
            }
        }
        else
        {
            appMaps.Add("ID1", "");
            vmMap.Add("ID1", "");
        }

        //map do a dictionary in program
        btMap.Clear();
        if (deserialized.Buttons != null)
        {
            foreach (var (key, value) in deserialized.Buttons)
            {
                btMap.Add(key, value);
            }
        }
        else
        {
            btMap.Add("B1", "");
        }

        yamlMap = myMapper.Mappings.Aggregate("Mappings:", (acc, kvp) =>
           $"{acc}\n {kvp.Key}:\n  Applications: {kvp.Value.Applications}\n  VM: {kvp.Value.VM}"
       ); // updates new yamlmap

        yamlButtonMap = btMap.Aggregate("Buttons:", (acc, kvp) =>
          $"{acc}\n {kvp.Key}: {kvp.Value}"
        ); // updates new yamlmap

        //set the process list
        UpdateSelectedProcessList();

        //set input boxes to data that was saved in config
        comboBox1.Text = deserialized.comport != null ? deserialized.comport : defaultComport;

        textBoxVersion.Text = deserialized.vmversion != null ? deserialized.vmversion : defaultVmVersion;
        VMbutton.Text = (deserialized.vm == "Y") ? "Enabled" : "Disabled";
        
        // Update button styling based on state
        UpdateVMButtonStyle();
    }
    
    private void UpdateVMButtonStyle()
    {
        if (VMbutton.Text == "Enabled")
        {
            VMbutton.BackColor = Color.FromArgb(46, 204, 113); // Green color when enabled
            VMbutton.ForeColor = Color.White;
        }
        else
        {
            VMbutton.BackColor = controlBackgroundColor;
            VMbutton.ForeColor = Color.White;
        }
    }

    private void UpdateSelectedProcessList()
    {
        listBoxSelectedProcesses.Items.Clear();
        selectedProcesses.Clear();
        
        if (currentView == 0) // Applications
        {
            if (appMaps.ContainsKey($"ID{idSelect.Value}"))
            {
                string appString = appMaps[$"ID{idSelect.Value}"];
                if (!string.IsNullOrEmpty(appString))
                {
                    string[] apps = appString.Split(';');
                    foreach (var app in apps)
                    {
                        if (!string.IsNullOrEmpty(app))
                        {
                            selectedProcesses.Add(app);
                            listBoxSelectedProcesses.Items.Add(app);
                        }
                    }
                }
            }
        }
        else // VoiceMeeter
        {
            if (vmMap.ContainsKey($"ID{idSelect.Value}"))
            {
                string vmString = vmMap[$"ID{idSelect.Value}"];
                if (!string.IsNullOrEmpty(vmString))
                {
                    string[] vms = vmString.Split(';');
                    foreach (var vm in vms)
                    {
                        if (!string.IsNullOrEmpty(vm))
                        {
                            selectedProcesses.Add(vm);
                            listBoxSelectedProcesses.Items.Add(vm);
                        }
                    }
                }
            }
        }
    }

    private void saveConfig()
    {
        BackendControl(0);

        Mapper myMapper = new Mapper();

        foreach (var key in appMaps.Keys) //needs to use try get key
        {
            myMapper.Mappings.Add(key, new Mapper.AppsString { Applications = appMaps[key], VM = vmMap[key] }); //loads into temp map
        }
        yamlMap = myMapper.Mappings.Aggregate("Mappings:", (acc, kvp) =>
           $"{acc}\n {kvp.Key}:\n  Applications: {kvp.Value.Applications}\n  VM: {kvp.Value.VM}"
       ); // updates new yamlmap

        var comport = comboBox1.Text;
        var baudrate = defaultBaudrate;
        var parity = defaultParity;
        var stopbits = defaultStopbits;
        var bytesize = defaultBytesize;
        var vmVersion = textBoxVersion.Text;
        var vm = (VMbutton.Text == "Enabled") ? "Y" : "N";

        var yml = @$"
comport: {comport}
baudrate: {baudrate}
bytesize: {bytesize}
parity: {parity}
stopbits: {stopbits}
VM: {vm}
VM-Version: {vmVersion}

{yamlMap}

{yamlButtonMap}
";

        Console.Write(yml);
        File.WriteAllText("config.yaml", yml);

        loadConfig(@"config.yaml");
        BackendControl(1);
        PopulateControllersListView();
    }

    private void idSelect_ValueChanged(object sender, EventArgs e)
    {
        if (currentView == 0)
        {
            if (!appMaps.TryGetValue($"ID{idSelect.Value}", out _))
            {
                appMaps.TryAdd($"ID{idSelect.Value}", "");
                vmMap.TryAdd($"ID{idSelect.Value}", "");
            }
        }
        else
        {
            if (!vmMap.TryGetValue($"ID{idSelect.Value}", out _))
            {
                appMaps.TryAdd($"ID{idSelect.Value}", "");
                vmMap.TryAdd($"ID{idSelect.Value}", "");
            }
        }

        UpdateSelectedProcessList();
    }

    private void SaveCurrentMap()
    {
        if (idSelect.Value == currentID)
        {
            string processString = string.Join(";", selectedProcesses);
            
            if (currentView == 0) // Applications
            {
                appMaps[$"ID{idSelect.Value}"] = processString;
                if (!vmMap.TryGetValue($"ID{idSelect.Value}", out _))
                {
                    vmMap.Add($"ID{idSelect.Value}", "");
                }
            }
            else // VoiceMeeter
            {
                vmMap[$"ID{idSelect.Value}"] = processString;
                if (!appMaps.TryGetValue($"ID{idSelect.Value}", out _))
                {
                    appMaps.Add($"ID{idSelect.Value}", "");
                }
            }
        }
        else
        {
            currentID = ((int)idSelect.Value);
        }
    }

    private void toolStripMenuItem1_Click(object sender, EventArgs e)
    {
        Process.Start(new ProcessStartInfo
        {
            FileName = "https://getvolumemaster.com/pages/quick-start-guide",
            UseShellExecute = true
        });
    }

    private void loadConfigFileToolStripMenuItem_Click(object sender, EventArgs e)
    {
        if (openConfigDialog.ShowDialog() != DialogResult.OK) return;
        var configPath = openConfigDialog.FileName;
        MessageBox.Show("Loading config file from:  " + configPath);
        loadConfig(configPath);
        saveConfig();
    }

    private void Form1_FormClosing(object sender, FormClosingEventArgs e)
    {
        BackendControl(0);
    }

    private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
    {
        // This is intentionally empty
    }

    private void VMbutton_Click(object sender, EventArgs e)
    {
        VMbutton.Text = (VMbutton.Text == "Enabled") ? "Disabled" : "Enabled";
        UpdateVMButtonStyle();
    }

    private void buttonAppSelect_Click(object sender, EventArgs e)
    {
        currentView = 0;

        buttonVMSelect.BackColor = controlBackgroundColor;
        buttonAppSelect.BackColor = primaryColor;
        
        UpdateSelectedProcessList();
    }

    private void buttonVMSelect_Click(object sender, EventArgs e)
    {
        currentView = 1;

        buttonVMSelect.BackColor = primaryColor;
        buttonAppSelect.BackColor = controlBackgroundColor;
        
        UpdateSelectedProcessList();
    }

    private void hamIcon_MouseDoubleClick(object sender, MouseEventArgs e)
    {
        // This is intentionally empty
    }

    private void panelHeader_MouseDown(object sender, MouseEventArgs e)
    {
        if (e.Button == MouseButtons.Left)
        {
            ReleaseCapture();
            SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
        }
    }

    private void buttonExit_Click(object sender, EventArgs e)
    {
        // Add a fade-out effect with safer error handling
        try
        {
            for (double opacity = 1.0; opacity > 0; opacity -= 0.1)
            {
                this.Opacity = opacity;
                this.Refresh();
                Thread.Sleep(15);
            }
        }
        catch (Exception)
        {
            // Ignore errors during exit animation
        }
        
        Application.Exit();
    }

    private void buttonMinimize_Click(object sender, EventArgs e)
    {
        WindowState = FormWindowState.Minimized;
    }

    private void RefreshProcessList()
    {
        try
        {
            processListView.Items.Clear();
            processIcons.Images.Clear();
            
            // Add default icon
            try
            {
                processIcons.Images.Add("default", SystemIcons.Application.ToBitmap());
            }
            catch
            {
                // If we can't get the system icon, create a simple colored rectangle
                Bitmap defaultIcon = new Bitmap(16, 16);
                using (Graphics g = Graphics.FromImage(defaultIcon))
                {
                    g.Clear(Color.FromArgb(50, 53, 70));
                }
                processIcons.Images.Add("default", defaultIcon);
            }
            
            Dictionary<string, int> iconIndices = new Dictionary<string, int>();
            iconIndices["default"] = 0;
            
            Process[] runningProcesses = Process.GetProcesses();
            foreach (Process proc in runningProcesses)
            {
                try
                {
                    if (!string.IsNullOrEmpty(proc.MainWindowTitle) || !string.IsNullOrEmpty(proc.ProcessName))
                    {
                        string processName = proc.ProcessName + ".exe";
                        ListViewItem item = new ListViewItem(processName);
                        item.SubItems.Add(proc.Id.ToString());
                        item.Tag = processName;
                        
                        // Try to get the process icon
                        try
                        {
                            if (!iconIndices.ContainsKey(processName))
                            {
                                Icon icon = null;
                                string fileName = null;
                                
                                try
                                {
                                    if (proc.MainModule != null)
                                    {
                                        fileName = proc.MainModule.FileName;
                                        if (File.Exists(fileName))
                                        {
                                            icon = Icon.ExtractAssociatedIcon(fileName);
                                        }
                                    }
                                }
                                catch
                                {
                                    // Fall back to default icon
                                }
                                
                                if (icon != null)
                                {
                                    processIcons.Images.Add(processName, icon.ToBitmap());
                                    iconIndices[processName] = processIcons.Images.Count - 1;
                                }
                                else
                                {
                                    iconIndices[processName] = 0; // Default icon
                                }
                            }
                            
                            item.ImageIndex = iconIndices[processName];
                        }
                        catch
                        {
                            item.ImageIndex = 0; // Default icon
                        }
                        
                        processListView.Items.Add(item);
                    }
                }
                catch (Exception) 
                {
                    // Some processes may not allow access - just skip them
                }
            }
            
            // Sort alphabetically
            processListView.Sorting = SortOrder.Ascending;
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error refreshing process list: " + ex.Message);
        }
    }

    private void buttonAddProcess_Click(object sender, EventArgs e)
    {
        if (processListView.SelectedItems.Count > 0)
        {
            string processName = processListView.SelectedItems[0].Tag.ToString();
            
            if (!selectedProcesses.Contains(processName))
            {
                // Add with an animation effect
                selectedProcesses.Add(processName);
                int index = listBoxSelectedProcesses.Items.Add(processName);
                
                // Visual feedback
                buttonAddProcess.BackColor = Color.FromArgb(46, 204, 113); // Green color
                listBoxSelectedProcesses.SelectedIndex = index;
                
                // Reset back to original color after a delay
                Task.Delay(300).ContinueWith(t => 
                {
                    if (this.IsHandleCreated) 
                    {
                        this.Invoke(new Action(() => 
                        {
                            buttonAddProcess.BackColor = primaryColor;
                        }));
                    }
                });
                
                SaveCurrentMap();
            }
        }
    }

    private void removeProcessMenuItem_Click(object sender, EventArgs e)
    {
        if (listBoxSelectedProcesses.SelectedIndex != -1)
        {
            string process = listBoxSelectedProcesses.SelectedItem.ToString();
            selectedProcesses.Remove(process);
            listBoxSelectedProcesses.Items.Remove(process);
            SaveCurrentMap();
        }
    }

    private void listBoxSelectedProcesses_SelectedIndexChanged(object sender, EventArgs e)
    {
        // This is intentionally empty
    }

    private void processSearchBox_TextChanged(object sender, EventArgs e)
    {
        string searchText = processSearchBox.Text.ToLower();
        
        if (string.IsNullOrEmpty(searchText))
        {
            // Show all processes if search is empty
            foreach (ListViewItem item in processListView.Items)
            {
                item.BackColor = listBackgroundColor;
                item.ForeColor = Color.White;
            }
            return;
        }
        
        // Filter process list based on search text
        foreach (ListViewItem item in processListView.Items)
        {
            if (item.Text.ToLower().Contains(searchText))
            {
                item.BackColor = listBackgroundColor;
                item.ForeColor = Color.White;
            }
            else
            {
                item.BackColor = listBackgroundColor;
                item.ForeColor = Color.FromArgb(100, 100, 100); // Dimmed color for non-matching items
            }
        }
    }

    private void buttonAddController_Click(object sender, EventArgs e)
    {
        // Find the highest ID currently in use
        int maxId = 1;
        foreach (var key in appMaps.Keys)
        {
            if (key.StartsWith("ID"))
            {
                if (int.TryParse(key.Substring(2), out int id))
                {
                    maxId = Math.Max(maxId, id);
                }
            }
        }
        
        // Create a new controller with the next ID
        int newId = maxId + 1;
        appMaps[$"ID{newId}"] = "";
        vmMap[$"ID{newId}"] = "";
        
        // Select the new controller
        idSelect.Value = newId;
        
        // Update the UI
        PopulateControllersListView();
        
        // Visual feedback
        buttonAddController.BackColor = Color.FromArgb(46, 204, 113); // Green color
        
        // Reset back to original color after a delay
        Task.Delay(300).ContinueWith(t => 
        {
            if (this.IsHandleCreated) 
            {
                this.Invoke(new Action(() => 
                {
                    buttonAddController.BackColor = primaryColor;
                }));
            }
        });
    }

    private void controllerListView_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (controllerListView.SelectedItems.Count > 0)
        {
            string id = controllerListView.SelectedItems[0].Tag.ToString().Replace("ID", "");
            if (int.TryParse(id, out int controllerId))
            {
                idSelect.Value = controllerId;
            }
        }
    }

    private void PanelLogo_Paint(object sender, PaintEventArgs e)
    {
        try
        {
            // Draw the icon directly using the Icon's ToBitmap method
            if (Properties.Resources.icongreen != null)
            {
                using (Bitmap iconBitmap = Properties.Resources.icongreen.ToBitmap())
                {
                    e.Graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                    e.Graphics.DrawImage(iconBitmap, 0, 0, panelLogo.Width, panelLogo.Height);
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error drawing logo: " + ex.Message);
        }
    }

    private void StyleControls()
    {
        // Style ListView
        processListView.BorderStyle = BorderStyle.None;
        
        // Style ListBox
        listBoxSelectedProcesses.BorderStyle = BorderStyle.None;
        
        // Style controller listview
        controllerListView.BorderStyle = BorderStyle.None;
        
        // Ensure panels have the right colors
        panelControllerDetails.BackColor = Color.FromArgb(35, 37, 52);
        processSelectionPanel.BackColor = Color.FromArgb(35, 37, 52);
        panelConfigType.BackColor = Color.FromArgb(30, 32, 47);
    }
}