using System.Windows.Forms;

namespace VolumeMaster_Windows
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            try
            {
                // To customize application configuration such as set high DPI settings or default font,
                // see https://aka.ms/applicationconfiguration.
                ApplicationConfiguration.Initialize();
                Application.SetHighDpiMode(HighDpiMode.SystemAware);
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                
                // Create the main form and run the application
                var mainForm = new Form1();
                Application.Run(mainForm);
            }
            catch (Exception ex)
            {
                // Show any startup errors
                MessageBox.Show("Error starting application: " + ex.Message + "\n\n" + ex.StackTrace, 
                    "VolumeMaster Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}