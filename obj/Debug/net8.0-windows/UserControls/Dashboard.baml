   M S B A M L   `   `   `  ����\  XHuman Resources Management System, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullo<clr-namespace:Human_Resources_Management_System.UserControls.Human_Resources_Management_System.UserControls  5      >    8Human_Resources_Management_System.UserControls.Dashboard   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      Nlocal<clr-namespace:Human_Resources_Management_System.UserControls     5      Hi:http://schemas.microsoft.com/expression/2010/interactivity     5      .��5
      �   � RowDefinitions  5   
   �� 5      $��30 �6   �� 5      $��1 �6   �� 5      $��2* �6   .+�5   
   � $��0��6   ���#3B3B3B.+�5      �� $��20(�6   $��30,0,0,0q�6&   
�������$��Segoe UI Light)�6O   	Dashboard6v   5      � 5   
   $��1��6   
�������6   � 5   
   $��2��6   ���#3B3B3B.+�5      �� -   5         ��Name$ ListViewUsers��5       �� SelectionMode$ Single=�6`   $��10,10,229,23q�6w   ���#3B3B3B
������� � 	Resources 5       �� 
TargetType �� Property �� Value �� Triggers P� Property	 P� Value��%*  '��       '�� 3    �� 5      " ��6   .�5      ��  � ��
 ���� 67   �� 5       � ��
 �����67   �� 5       � ��$ Stretch=�6G    5      P� 5       �@��
IsSelected$
	 True��6<   . �5   "   ��  � �� �#1414146?   �� 5   "    � ��
 �����6?   5       5"      �� 5%      " ��6   .�5&      ��  � ��$ Center=�6G   �� 5'       � ��
 �   �67   �� 5(       � ��
 �����67   �� 5)       � ��$
 Bold"�67   �� 5*       � ��$	 5,3q�64   5+      5,      
 �� View
 5.      �� 5/       �� AllowsColumnReorder$ False��6   .P�51      �� $��
First Name��6)    �� Width$
 Auto��6h    �� DisplayMemberBinding 6=   �� *	FirstName+6h   �� 52      $��Surname��6)   $
 Auto��6c    6:   �� *	Surname+6c   �� 53      $��Middle Name��6)   $
 Auto��6j    6>   �� *
MiddleName+6j   �� 54      $	��Sex��6)   $
 Auto��6[    66   �� *Sex+6[   �� 55      $��Birthday��6)   $
 Auto��6}    6;   �� *
Birthday+ �� StringFormat$ 
MM-dd-yyyy��6}   �� 56      $	��Age��6)   $
 Auto��6[    66   �� *Age+6[   �� 57      $��Email��6)   $
 Auto��6_    68   �� *Email+6_   �� 58      $��Address��6)   $
 Auto��6c    6:   �� *	Address+6c   �� 59      $��
Contact No��6)   $
 Auto��6h    6=   �� *	ContactNo+6h   �� 5:      $��Requirements��6)   $
 Auto��6m    6?   �� *Requirements+6m   �� 5;      $��
Date Hired��6)   $
 Auto��6�    6=   �� *	DateHired+$ 
MM-dd-yyyy��6�   �� 5<      $��Shuttle Code��6)   $
 Auto��6l    6?   �� *ShuttleCode+6l   �� 5>      $��	Signature��6)   $ 80��6<    �� CellTemplate 5?      �� 5@   "   .R�5A   &   �� $
��Auto��6R   $
��Auto��6_   $��UniformToFill=�6m   ��6,   �� *ApplicantSignature+6m   5B   #   5D      �� 5E      $��Status��6)   $ 60��6a    69   �� *Status+6a   �� 5G      $��Picture��6)   $	 100��6:    5H      �� 5I   "   .R�5J   &   �� $��50��6L   $��50��6W   $��UniformToFill=�6c   ��6,   �� *ProfileImage+6c   5K   #   5M      �� 5N      $�� ��6)    5O      �� 5P   "   .R�5Q   &   �� -   5        $��5,0,0,0q�5Q   H   
������ $��0q�6r    � ToolTip$ Delete��6�   ��5R   *   �� 5S   .   $z��tC:\Users\lenovo\source\repos\Human Resources Management System\Human Resources Management System\Images/TrashBin.png��64   $��16��6�   $��16��6�   5U   '   5V   #   5X      5Y      5[      &   System.Windows.Controls.Calendar  5_      -   5        $��RealTimeCalendar��5_      $	��Top=�5`      $��Right=�6+   $��0,20,0,0q�6G   $	��235��6Y   
�������
���   �6x   � 5a      $	��200��6   $	��350��6   $��Right=�6,   $	��Top=�6H   $��
0,196,15,0q�6`     5b      �� 5c      $��30 �6$   �� 5d      $��1 �6$   �� 5e      $��2* �6$   .+�5h      � $��0��6   ���#141414.+�5i      �� $��20(�6    $��30,0,0,0q�6.   
�������$��Segoe UI Light)�6W   Notes6z   5j      � 5k      $��1��6   
�������6$   � 5l      $��2��6   ���#141414.+�5m      �� -   5         ��Name$ NotesTextBox��5m      
�������
���   � �� TextWrapping$
 Wrap=�5p       �� AcceptsReturn$
 True��5q       �� 
IsReadOnly$
 True��5r      " �� VerticalScrollBarVisibility$
 Auto=�5s      $��5q�5t      5w      5x      5y      � 5|   
   -   5         �Name$ EditFormPopup��5|      $��False��6'    � 	StaysOpen$ False��66   $��Center=�6H    � HorizontalOffset$ 0��5}       � VerticalOffset$ 0��6%   .��5~      >� -   5         >�Name$ EditFormContainer��5~      5�      5�      5�      5�      