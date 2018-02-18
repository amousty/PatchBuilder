using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatchBuilder
{
    public partial class Helpers : MetroWindow
    {
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

        private void buildFinalFolderField()
        {
            // When we loose the focus, we try to retrieve the data
            if (checkIssueID(txtB_IssueID.Text))
            {
                // If the data is correct, build the folder directory
                folderDirectory =
                   @"C:\SVN\PATCH\To_Dev\" +
                   txtB_IssueID.Text.Substring(1, 3) + "000\\" +
                   txtB_IssueID.Text.Substring(4, 1) + "00\\";
                // we fill accordingly the right field.
                txtB_browseDirectory.Text = folderDirectory;
            }
        }


    }
}
