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
        };
        private OpenFileDialog openFileDialog1;
        private FolderBrowserDialog folderBrowserDialog1;

        public MainWindow()
        {
            InitializeComponent();
            txtB_patchName.IsReadOnly = true;
        }

        private void btn_build_Click(object sender, RoutedEventArgs e)
        {
            int checkEverythingOk = everythingIsChecked();
            if (checkEverythingOk > 0)
            {
                // Everything is ok ! We could build the result into the patch name.
                txtB_patchName.IsReadOnly = false;
                txtB_patchName.Text = txtB_IssueID.Text + "[" + txtB_ticketRef.Text + "] " + txtB_description.Text;

                // Build launched
                buildFolder();
            }
            else
            {
                // Display error message. 

                // Clean description.
                txtB_patchName.Text = "Error code : " + checkEverythingOk + ".\n" + ErrorMessage[checkEverythingOk * -1];
                txtB_patchName.IsReadOnly = true;
            }
        }

        private int everythingIsChecked()
        {
            int errorCode = 1;

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

                        }
                        else
                        {
                            // Description is empty
                            errorCode = -4;
                        }
                    }
                    else
                    {
                        // Ticket ref is incorrect
                        errorCode = -3;
                    }
                }
                else
                {
                    // Issue ID is incorrect.
                    errorCode = -2;
                }
            }
            else
            {
                // Check at least one checkbox
                errorCode = -1;
            }

            return errorCode;
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
            string newDirectory = removeChar(txtB_browseDirectory.Text, new char[] { '#' }) + "/" + txtB_patchName.Text + "/";
            try
            {
                // We need to iterate for each type selected.
                
                // Determine whether the directory exists.
                if (Directory.Exists(newDirectory))
                {
                    // Path already existing. 
                    return -5;
                }

                // Try to create the directory.
                DirectoryInfo di = Directory.CreateDirectory(newDirectory);

                // Now we build the subfolders
                return buildSubfolder();
            }
            catch (Exception)
            {
                return -6; // Console.WriteLine("The process failed: {0}", e.ToString());
            }
        }

        private int buildSubfolder()
        {
            string newDirectory = removeChar(txtB_browseDirectory.Text, new char[] { '#' }) + "/" + txtB_patchName.Text + "/";
            int returnValue = 0;

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

            return returnValue;
        }

        private string removeChar(string str, char[] charsToRemove)
        {
            foreach (var c in charsToRemove)
            {
                str = str.Replace(c, '\0');
            }

            return str;
        }
    }
}
