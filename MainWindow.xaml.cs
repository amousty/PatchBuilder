using System;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Forms;
using MahApps.Metro.Controls;



namespace PatchBuilder
{

    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        string[] ErrorMessage =
        {
                "Unknown error",
                "Please, check at least one component.",
                "Please, provide a correct issue id.",
                "Please, provide a correct ticket reference.",
                "Please, fill the description.",
                "Path specified already existing.",
                "Unable to browse this folder.",
                "Please, browse a folder before building."
        };
        private OpenFileDialog openFileDialog1;
        private FolderBrowserDialog folderBrowserDialog1;

        public string folderDirectory { get; set; }
        public string patchNameWithoutSharp { get; set; }
        public int errorStatus { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            initValues();
            txtB_patchName.IsReadOnly = true;
        }

        private void initValues()
        {
            folderDirectory = "";
            patchNameWithoutSharp = ""; //folderDirectory.Replace('#', '');
            errorStatus = 1;
        }

        private void btn_build_Click(object sender, RoutedEventArgs e)
        {
            errorStatus = everythingIsChecked();
            if (errorStatus > 0)
            {
                // Everything is ok ! We could build the result into the patch name.
                txtB_patchName.IsReadOnly = false;
                txtB_patchName.Text = txtB_IssueID.Text + "[" + txtB_ticketRef.Text + "] " + txtB_description.Text;
                patchNameWithoutSharp = txtB_patchName.Text.Replace("#", "");
                folderDirectory = txtB_browseDirectory.Text;

                // Build launched
                buildFolder();
            }
            else
            {
                // Display error message. 

                // Clean description.
                txtB_patchName.Text = "Error code : " + errorStatus + ".\n" + ErrorMessage[errorStatus * -1];
                txtB_patchName.IsReadOnly = true;
            }
        }

        private int everythingIsChecked()
        {
            // Verify at least one checkbox is thicked
            if (chkb_Web.IsChecked.Value ||
                chkb_MVC.IsChecked.Value ||
                chkb_Script.IsChecked.Value ||
                chkb_SP.IsChecked.Value)
            {
                // Check if the issueid is correct
                if (checkIssueID(txtB_IssueID.Text))
                {
                    // Check if the patchname is correct
                    if (checkPatchType(txtB_ticketRef.Text))
                    {
                        // Check if the issueid is correct
                        if (!txtB_description.Text.Equals(""))
                        {
                            if (txtB_browseDirectory.Text.Equals(""))
                            {
                                // No folder specified
                                errorStatus = -7;
                            }
                        }
                        else
                        {
                            // Description is empty
                            errorStatus = -4;
                        }
                    }
                    else
                    {
                        // Ticket ref is incorrect
                        errorStatus = -3;
                    }
                }
                else
                {
                    // Issue ID is incorrect.
                    errorStatus = -2;
                }
            }
            else
            {
                // Check at least one checkbox
                errorStatus = -1;
            }

            return errorStatus;
        }

        private bool checkIssueID(string val)
        {
            bool isOk = false;
            int tmpI = -1;
            if (val.Count(f => f == '#') == 2)
            {
                string[] testInt = val.Split('#');
                if (int.TryParse(testInt[1], out tmpI))
                {
                    isOk = true;
                }
            }

            return isOk;
        }

        private bool checkPatchType(string val)
        {
            bool isOk = false;
            int tmpI = -1;
            if (val.Count(f => f == '-') == 1)
            {
                string[] testInt = val.Split('-');
                if (testInt[0] is string && int.TryParse(testInt[1], out tmpI))
                {
                    isOk = true;
                }
            }

            return isOk;
        }

        private void btn_clearAll_Click(object sender, RoutedEventArgs e)
        {
            chkb_Web.IsChecked = false;
            chkb_MVC.IsChecked = false;
            chkb_Script.IsChecked = false;
            chkb_SP.IsChecked = false;

            txtB_IssueID.Text = "##";
            txtB_ticketRef.Text = "SUP-";
            txtB_description.Text = "";
            txtB_browseDirectory.Text = "";
            txtB_patchName.Text = "";

            txtB_patchName.IsReadOnly = true;
        }

        private void btn_browse_Click(object sender, RoutedEventArgs e)
        {
            
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();

            // Set the help text description for the FolderBrowserDialog.
            this.folderBrowserDialog1.Description =
                "Select the directory that you want to use as the default.";

            // Do not allow the user to create new files via the FolderBrowserDialog.
            this.folderBrowserDialog1.ShowNewFolderButton = false;

            // Show the FolderBrowserDialog.
            DialogResult result = folderBrowserDialog1.ShowDialog();

            string folderName = @"c:\temp\";
            // No file is opened, bring up openFileDialog in selected path.
            openFileDialog1.InitialDirectory = folderName;
            openFileDialog1.FileName = null;
            txtB_browseDirectory.Text = folderBrowserDialog1.SelectedPath;
        }

        private int buildFolder()
        {
            string newDirectory = folderDirectory + "/" + patchNameWithoutSharp + "/";
            try
            {
                // We need to iterate for each type selected.
                
                // Determine whether the directory exists.
                if (Directory.Exists(newDirectory))
                {
                    // Path already existing. 
                    errorStatus = - 5;
                }

                // Try to create the directory.
                DirectoryInfo di = Directory.CreateDirectory(newDirectory);

                // Now we build the subfolders
                return buildSubfolder();
            }
            catch (Exception)
            {
                errorStatus = - 6; // Console.WriteLine("The process failed: {0}", e.ToString());
            }
            return errorStatus;
        }

        private int buildSubfolder()
        {
            string newDirectory = folderDirectory + "/" + patchNameWithoutSharp + "/";
            errorStatus = 1;

            if (chkb_MVC.IsChecked == true)
            {
                DirectoryInfo di = Directory.CreateDirectory(newDirectory + "/MVC");
                di = Directory.CreateDirectory(newDirectory + "/MVC/MXP_MVC_Application");
            }

            if (chkb_Script.IsChecked == true)
            {
                DirectoryInfo di = Directory.CreateDirectory(newDirectory + "/SCRIPT");
            }

            if (chkb_SP.IsChecked == true)
            {
                DirectoryInfo di = Directory.CreateDirectory(newDirectory + "/SP");
            }

            if (chkb_Web.IsChecked == true)
            {
                DirectoryInfo di = Directory.CreateDirectory(newDirectory + "/WEB");
            }

            return errorStatus;
        }
    }
}
