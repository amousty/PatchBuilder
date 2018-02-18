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
        // Contains whole error messages potentially displayed
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

        public string folderDirectory { get; set; } = "";
        public string patchNameWithoutSharp { get; set; }
        public int errorStatus { get; set; } = 1;

        public MainWindow()
        {
            InitializeComponent();
            txtB_patchName.IsReadOnly = true;
        }

        /// <summary>
        /// Will build and generate folders
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
                displayErrorStatus();
            }
        }

        /// <summary>
        /// Clean whole data fields.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// Open a dialog in order to select the destination of our folder.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// When the issueID is filled, try to already propose a correct destination
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtB_IssueID_LostFocus(object sender, RoutedEventArgs e)
        {
            if (buildFinalFolderField() < 0) {
                displayErrorStatus();
            }
        }

        /* HELPERS */
        /// <summary>
        /// Check whole data are correct before build.
        /// </summary>
        /// <returns></returns>
        private int everythingIsChecked()
        {
            // Verify at least one checkbox is thicked
            if (chkb_Web.IsChecked.Value ||
                chkb_MVC.IsChecked.Value ||
                chkb_Script.IsChecked.Value ||
                chkb_SP.IsChecked.Value)
            {
                // Check if the issueid is correct
                errorStatus = checkIssueID(txtB_IssueID.Text);
                if (errorStatus > 0)
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
                /*else
                {
                    // Issue ID is incorrect.
                    errorStatus = -2;
                }*/
            }
            else
            {
                // Check at least one checkbox
                errorStatus = -1;
            }

            return errorStatus;
        }

        /// <summary>
        /// Check issue ID, if its correct or not
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        private int checkIssueID(string val)
        {
            errorStatus = -2;
            int tmpI = -1;
            if (val.Count(f => f == '#') == 2)
            {
                string[] testInt = val.Split('#');
                if (int.TryParse(testInt[1], out tmpI))
                {
                    if (tmpI.ToString().Length == 6)
                    {
                        errorStatus = 1; // Ok
                    }
                }
            }

            return errorStatus;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
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

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private int buildFolder()
        {
            string newDirectory = folderDirectory + "\\" + patchNameWithoutSharp + "\\";
            try
            {
                // We need to iterate for each type selected.

                // Determine whether the directory exists.
                if (Directory.Exists(newDirectory))
                {
                    // Path already existing. 
                    errorStatus = -5;
                }

                // Try to create the directory.
                DirectoryInfo di = Directory.CreateDirectory(newDirectory);

                // Now we build the subfolders
                return buildSubfolder();
            }
            catch (Exception)
            {
                errorStatus = -6; // Console.WriteLine("The process failed: {0}", e.ToString());
            }
            return errorStatus;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private int buildSubfolder()
        {
            string newDirectory = folderDirectory + "\\" + patchNameWithoutSharp + "\\";
            errorStatus = 1;

            if (chkb_MVC.IsChecked == true)
            {
                DirectoryInfo di = Directory.CreateDirectory(newDirectory + "\\MVC");
                di = Directory.CreateDirectory(newDirectory + "\\MVC\\MXP_MVC_Application");
            }

            if (chkb_Script.IsChecked == true)
            {
                DirectoryInfo di = Directory.CreateDirectory(newDirectory + "\\SCRIPT");
            }

            if (chkb_SP.IsChecked == true)
            {
                DirectoryInfo di = Directory.CreateDirectory(newDirectory + "\\SP");
            }

            if (chkb_Web.IsChecked == true)
            {
                DirectoryInfo di = Directory.CreateDirectory(newDirectory + "\\WEB");
            }
            return errorStatus;
        }

        /// <summary>
        /// 
        /// </summary>
        private int buildFinalFolderField()
        {
            // When we loose the focus, we try to retrieve the data
            errorStatus = checkIssueID(txtB_IssueID.Text);
            if (errorStatus > 0)
            {
                // If the data is correct, build the folder directory
                folderDirectory =
                   @"C:\SVN\PATCH\To_Dev\" +
                   txtB_IssueID.Text.Substring(1, 3) + "000\\" +
                   txtB_IssueID.Text.Substring(4, 1) + "00\\";
                // we fill accordingly the right field.
                txtB_browseDirectory.Text = folderDirectory;
            }
            return errorStatus;
        }

        /// <summary>
        /// Display the error status according to errorStatus value.
        /// </summary>
        private void displayErrorStatus()
        {
            txtB_patchName.Text = "Error code : " + errorStatus + ".\n" + ErrorMessage[errorStatus * -1];
            txtB_patchName.IsReadOnly = true;
        }
    }
}
