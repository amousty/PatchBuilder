   M S B A M L   `   `   `  ���� G  CPatchBuilder, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:PatchBuilderPatchBuilder  J FMahApps.Metro, Version=1.6.0.362, Culture=neutral, PublicKeyToken=nullV;clr-namespace:MahApps.Metro.Controls;assembly=MahApps.MetroMahApps.Metro.Controls     PatchBuilder.MainWindow   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     ,localclr-namespace:PatchBuilder     PControls;clr-namespace:MahApps.Metro.Controls;assembly=MahApps.Metro     -   (  "MahApps.Metro.Controls.MetroWindow   Name$  Patch_Builder�� � Title$ MXP Patch builder��$	��350��$��455.162�� � 
ResizeMode$ NoResize=�.��� .+��� -    ��Name$ lbl_patchName��$��
Patch name��$
��Left=�$��
10,200,0,0q�$	��Top=�$��75��$��Segoe UI Semibold)��� -    ��Name$ txtB_patchName��$
��Left=�$��50��$��112,199,0,0q� �� TextWrapping$
 Wrap=�$	��Top=�$	��315�����	#FFDFDFDF�� -    ��Name$ 	btn_build��   AccentedSquareButtonStyle#��[  $��Build��$
��Left=�$��112,254,0,0q�$	��Top=�$��75��
��������� -   $ btn_clearAll��#��[  $��
Clear data��$
��Left=�$��352,254,0,0q�$	��Top=�$��75��
��������� -    ��Name$ chkb_MVC��$	��MVC��$
��Left=�$��112,114,0,0q�$	��Top=�$��60���� -   $ lbl_patchType��$
��Type��$
��Left=�$��
10,110,0,0q�$	��Top=�$��75��$��Segoe UI Semibold)��� -   $ chkb_SP��$��SP��$
��Left=�$��177,114,0,0q�$	��Top=�$��55���� -	   $ chkb_Script��$��Script��$
��Left=�$��112,142,0,0q�$	��Top=�$��60���� -
   $ chkb_Web��$	��Web��$
��Left=�$��177,142,0,0q�$	��Top=�$��55���� -   $ lbl_issueID��$��Issue ID��$
��Left=�$��	10,10,0,0q�$	��Top=�$��Segoe UI Semibold)��� -   $ txtB_IssueID��*  $MahApps.Metro.Controls.TextBoxHelper   ClearTextButton . $
��Left=�$��26��$��
112,10,0,0q�$
 Wrap=�$��##��$	��Top=�$	��120���� -   $ lbl_description��$��Description��$
��Left=�$��	10,72,0,0q�$	��Top=�$��Segoe UI Semibold)��� -   $ txtB_description�� . $
��Left=�$��26��$��
112,72,0,0q�$
 Wrap=�$	��Top=�$	��120���� -   $ lbl_ticketRef��$��Ticket reference��$
��Left=�$��	10,41,0,0q�$	��Top=�$��Segoe UI Semibold)��� -   $ txtB_ticketRef�� . $
��Left=�$��26��$��
112,41,0,0q�$
 Wrap=�$
��SUP-��$	��Top=�$	��120���� -   	 ��Name$	 img_logo��$
��Left=�$	��163��$��	254,0,0,0q�$	��Top=�$	��173��$��Images/logo.jpg��
 J� OpacityMask
 ��  �� ImageSource$ Images/logo.jpg���� -   $ txtB_browseDirectory��$
��Left=�$��26��$��112,168,0,0q�$
 Wrap=�$	��Top=�$	��315�� �� 
IsReadOnly . ���	#FFDFDFDF�� -   $ 
btn_browse��#��[  $��Browse��$
��Left=�$��
10,168,0,0q�$	��Top=�$��75��$��26��
�������