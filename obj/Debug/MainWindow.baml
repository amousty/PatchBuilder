   M S B A M L   `   `   `  ����G  CPatchBuilder, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:PatchBuilderPatchBuilder  5   	   J FMahApps.Metro, Version=1.6.0.362, Culture=neutral, PublicKeyToken=nullV;clr-namespace:MahApps.Metro.Controls;assembly=MahApps.MetroMahApps.Metro.Controls 5   	       PatchBuilder.MainWindow   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   ,localclr-namespace:PatchBuilder     5   	   PControls;clr-namespace:MahApps.Metro.Controls;assembly=MahApps.Metro     5   	   -   5        (  "MahApps.Metro.Controls.MetroWindow   Name$  Patch_Builder��5       � Title$ MXP Patch builder��5	   	   $	��350��6#   $��455.162��60    � 
ResizeMode$ NoResize=�6@   .��5
      � .+�5   
   �� -   5         ��Name$ lbl_patchName��5      $��
Patch name��6'   $
��Left=�6<   $��
10,200,0,0q�6W   $	��Top=�6k   $��75��6�   $��Segoe UI Semibold)�6�   �� 5   
   -   5         ��Name$ txtB_patchName��5      $
��Left=�6*   $��50��6E   $��112,199,0,0q�6Q    �� TextWrapping$
 Wrap=�6f   $	��Top=�6z   $	��315��6�   ���	#FFDFDFDF6�   �� 5   
   -   5         ��Name$ 	btn_build��5   D      AccentedSquareButtonStyle#��[  6   $��Build��6W   $
��Left=�6g   $��112,254,0,0q�6�   $	��Top=�6�   $��75��6�   
�������6�   �� 5   
   -   5        $ btn_clearAll��5   D   #��[  6   $��
Clear data��6Z   $
��Left=�6o   $��352,254,0,0q�6�   $	��Top=�6�   $��75��6�   
�������6�   �� 5   
   -   5         ��Name$ chkb_MVC��5      $	��MVC��6%   $
��Left=�63   $��112,114,0,0q�6N   $	��Top=�6c   $��60��6{   �� 5   
   -   5        $ lbl_patchType��5      $
��Type��6'   $
��Left=�66   $��
10,110,0,0q�6Q   $	��Top=�6e   $��75��6}   $��Segoe UI Semibold)�6�   �� 5   
   -   5        $ chkb_SP��5      $��SP��6$   $
��Left=�61   $��177,114,0,0q�6L   $	��Top=�6a   $��55��6y   �� 5   
   -	   5        $ chkb_Script��5      $��Script��6(   $
��Left=�69   $��112,142,0,0q�6T   $	��Top=�6i   $��60��6�   �� 5   
   -
   5        $ chkb_Web��5      $	��Web��6%   $
��Left=�63   $��177,142,0,0q�6N   $	��Top=�6c   $��55��6{   �� 5   
   -   5        $ lbl_issueID��5      $��Issue ID��6%   $
��Left=�68   $��	10,10,0,0q�6S   $	��Top=�6f   $��Segoe UI Semibold)�6~   �� 5   
   -   5        $ txtB_IssueID��5   @   *  $MahApps.Metro.Controls.TextBoxHelper   ClearTextButton . $
��Left=�6V   $��26��6q   $��
112,10,0,0q�6}   $
 Wrap=�6�   $��##��6�   $	��Top=�6�   $	��120��6�   6�   �� 5   
   -   5        $ lbl_description��5      $��Description��6)   $
��Left=�6?   $��	10,72,0,0q�6Z   $	��Top=�6m   $��Segoe UI Semibold)�6�   �� 5   
   -   5        $ txtB_description��5   @    . $
��Left=�6Z   $��26��6u   $��
112,72,0,0q�6�   $
 Wrap=�6�   $	��Top=�6�   $	��120��6�   �� 5   
   -   5        $ lbl_ticketRef��5      $��Ticket reference��6'   $
��Left=�6B   $��	10,41,0,0q�6]   $	��Top=�6p   $��Segoe UI Semibold)�6�   �� 5   
   -   5        $ txtB_ticketRef��5   @    . $
��Left=�6X   $��26��6s   $��
112,41,0,0q�6   $
 Wrap=�6�   $
��SUP-��6�   $	��Top=�6�   $	��120��6�   �� 5   
   -   5        	 ��Name$	 img_logo��5      $
��Left=�6"   $	��163��6=   $��	254,0,0,0q�6J   $	��Top=�6]   $	��173��6u   $��Images/logo.jpg��6�   
 J� OpacityMask
 5      �� 5       �� ImageSource$ Images/logo.jpg��6   5      �� 5   
   -   5        $ txtB_browseDirectory��5      $
��Left=�60   $��26��6K   $��112,168,0,0q�6W   $
 Wrap=�6l   $	��Top=�6�   $	��315��6�    �� 
IsReadOnly . ���	#FFDFDFDF6�   �� 5    
   -   5        $ 
btn_browse��5    D   #��[  6   $��Browse��6X   $
��Left=�6i   $��
10,168,0,0q�6�   $	��Top=�6�   $��75��6�   $��26��6�   
�������6�   5"      5#      