   M S B A M L   `   `   `  ����\  XHuman Resources Management System, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullo<clr-namespace:Human_Resources_Management_System.UserControls.Human_Resources_Management_System.UserControls  5      A    ;Human_Resources_Management_System.UserControls.UserApproval   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      Nlocal<clr-namespace:Human_Resources_Management_System.UserControls     5      .��5	      �   � RowDefinitions  5
   
   �� 5      $��30 �6   �� 5      $��1 �6   �� 5      $	��220 �6   �� 5      $��1 �6   �� 5      $��2* �6   .+�5   
   � $��0��6   .+�5      �� 
�������$��20(�6+   $��30,0,0,0q�69   $��Segoe UI Light)�6K   
Approval6q   5      � 5   
   $��1��6   5      � 5   
   $��2��6   .+�5      �� -   5         ��Name$ ListViewUsers��5      $��Stretch=�6.   $��Stretch=�6L   ���#3B3B3B � 	Resources 5       �� 
TargetType �� Property �� Value �� Triggers P� Property P� Value��%*  '��       '�� 3    �� 5      " ��6   .�5      ��  � ��
 ���� 67   �� 5       � ��
 �����67   �� 5        � ��$ Stretch=�6G    5!      P� 5"       �@��
IsSelected$
 True��6<   . �5#   "   ��  � �� �#1414146?   �� 5$   "    � ��
 �����6?   5%      5'      �� 5*      " ��6   .�5+      ��  � ��$ Center=�6G   �� 5,       � ��
 �   �67   �� 5-       � ��
 �����67   �� 5.       � ��$
 Bold"�67   �� 5/       � ��$	 5,3q�64   50      51      	 �� View	 52      �� 53      .P�54      �� $��
First Name��6)   
 �� Width$

 Auto��6h    �� DisplayMemberBinding 6=   �� *	FirstName+6h   �� 55      $��	Last Name��6)   $

 Auto��6f    6<   �� *
LastName+6f   �� 56      $��Username��6)   $

 Auto��6e    6;   �� *
Username+6e   �� 57      $��Email��6)   $

 Auto��6_    68   �� *Email+6_   �� 5:      $��Approval��6)   $	
 200��6;    �� CellTemplate 5;      �� 5<   "   .R�5=   &   ��  �� Orientation$ 
Horizontal=�61   $��Center=�6J   .+�5?   *   �� -   5        $��30��5?   1   $��30��6<   $��5q�6H    � ToolTip$ Approve��6S   $	��✔��6�   5A   +   �� 5D   *   -   5        $��30��5D   1   $��30��6<   $��5q�6H   $ Reject��6S   $	��❌��6   5F   +   5G   '   5H   #   5J      5K      5M      5N      � 5O   
   $��3��6   � 5P   
   $��4��6   .+�5Q      �� $��Approved Items��6   $��16(�6.   $
��Bold"�6<   $��0,0,0,10q�6N   �� 5R      -   5        $ ListViewApproved��5R      $��Stretch=�61   ���#3B3B3B$��Stretch=�6d    5S      ��%*  '��       '�� 3    �� 5U      " ��6   .�5V      ��  � ��
 ���� 67   �� 5W       � ��
 �����67   �� 5X       � ��$ Stretch=�6G    5Y      P� 5Z       �@��
IsSelected$
 True��6<   . �5[   "   ��  � �� �#1414146?   �� 5\   "    � ��
 �����6?   5]      5_      �� 5b      " ��6   .�5c      ��  � ��$ Center=�6G   �� 5d       � ��
 �   �67   �� 5e       � ��
 �����67   �� 5f       � ��$
 Bold"�67   �� 5g       � ��$	 5,3q�64   5h      5i      	 5j      �� 5k      .P�5m      �� $��
First Name��6)   $

 Auto��6h    6=   �� *	FirstName+6h   �� 5n      $��	Last Name��6)   $

 Auto��6f    6<   �� *
LastName+6f   �� 5o      $��Email��6)   $

 Auto��6_    68   �� *Email+6_   �� 5p      $��Approval Status��6)   $

 Auto��6r    6B   �� *ApprovalStatus+6r   5r      5t      5u      5v      5w      