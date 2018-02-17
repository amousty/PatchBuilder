using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PatchBuilder
{

    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string[] ErrorMessage =
        {
                "Unknown error",
                "Please, check at least one component.",
                "Please, provide a correct issue id.",
                "Please, provide a correct ticket reference.",
                "Please, fill the description.",
        };

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
            }
            else {
                // Display error message. 

                // Clean description.
                txtB_patchName.Text = "Error code : " + checkEverythingOk + ".\n" + ErrorMessage[checkEverythingOk*-1];
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
                else {
                    // Issue ID is incorrect.
                    errorCode = -2;
                }
            }
            else {
                // Check at least one checkbox
                errorCode = -1;
            }

            return errorCode;
        }

        private bool checkIssueID(string val) {
            bool isOk = false;
            int tmpI = -1;
            if (val.Count(f => f == '#') == 2)
            {
                string [] testInt= val.Split('#');
                if (int.TryParse(testInt[1], out tmpI)){
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
                if (testInt[0] is string  && int.TryParse(testInt[1], out tmpI))
                {
                    isOk = true;
                }
            }

            return isOk;
        }
    }
}
