   M S B A M L   `   `   `  ����\  XHuman Resources Management System, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullo<clr-namespace:Human_Resources_Management_System.UserControls.Human_Resources_Management_System.UserControls  5      <    6Human_Resources_Management_System.UserControls.Payroll   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      Nlocal<clr-namespace:Human_Resources_Management_System.UserControls     5      .��5	      �   � RowDefinitions  5
   
   �� 5      $��30 �6   �� 5      $��* �6   �� 5      $
��Auto �6   .+�5   
   � $��0��6   ���#3B3B3B.+�5      �� $��20(�6   $��30,0,0,0q�6&   
�������$��Segoe UI Light)�6O   	Payroll6t   5      � 5   
   $��1��6   ���#3B3B3B.+�5      �� -   5         ��Name$ ListViewUsers��5      $��
10,10,10,9q�6.   ���#3B3B3B
�������$	��400��6j   $	��780��6w    � 	Resources 5       �� 
TargetType �� Property �� Value �� Triggers P� Property P� Value��%*  '��       '�� 3    �� 5      " ��6   .�5      ��  � ��
 ���� 67   �� 5       � ��
 �����67   �� 5        � ��$ Stretch=�6G    5!      P� 5"       �@��
IsSelected$
 True��6<   . �5#   "   ��  � �� �#1414146?   �� 5$   "    � ��
 �����6?   5%      5'      �� 5)      " ��6   .�5*      ��  � ��$ Center=�6G   �� 5+       � ��
 �   �67   �� 5,       � ��
 �����67   �� 5-       � ��$
 Bold"�67   �� 5.       � ��$	 5,3q�64   5/      50      	 �� View	 52      �� 53      
 �� AllowsColumnReorder$
 False��6   .P�54      �� $��ID No��6)    �� Width$	 100��6d    �� DisplayMemberBinding 68   �� *
EmployeeId+6d   �� 55      $��
First Name��6)   $	 150��6h    6=   �� *	FirstName+6h   �� 56      $��	Last Name��6)   $	 150��6e    6<   �� *	Surname+6e   �� 57      $��Email��6)   $	 200��6_    68   �� *Email+6_   �� 58      $��Address��6)   $	 200��6c    6:   �� *	Address+6c   �� 59      $��Address��6)   $	 200��6c    6:   �� *	Address+6c   �� 5:      $	��Pay��6)   $	 100��6[    66   �� *Pay+6[   �� 5;      $��	Paid Date��6)   $	 200��6f    6<   �� *
PaidDate+6f   5<      5>      �� 5?      -   5         ��Name$ SearchButton��5?      $��50��6+   $��50��66   $
��Left=�5@      $	��Top=�6)   $��	85,50,0,0q�6A   $
��🔍��5A   &   5C      �� 5F      -   5         ��Name$ 	SearchBar��5F      $
��Left=�5H      $	��Top=�5I      $��155,10,10,0q�5J      $	��300��5K      $y�	Collapsed=�5L      $��10(�5N      �� 5R       �� Orientation$ Vertical=�6   $	��Top=�60   $��Right=�6H   $��	0,154,0,0q�6d   .+�5S      �� -   5         ��Name$ Total_Employee��5S      $ 
Horizontal=�65   .+�5T      �� -   5         ��Name$ Total_Employee1��5T       
�������$��Segoe UI Light)�6P   $��12(�6l   Total Employee:6�   �� 5U       �� 
IsReadOnly$
 True��6   
������ $��50��6I   $��0q�6T   $|�False��6h   5V      �� 5W      -   5        $ Total_Payroll��5W      $ 
Horizontal=�64   $��0,25,0,0q�6M   .+�5X      �� -   5        $ Total_Payroll1��5X       
�������$��Segoe UI Light)�6O   $��12(�6l   Total Payroll:6�   �� 5Y      $
 True��6   
������ $��50��6I   $��0q�6T   $|�False��6h   5Z      5[      5\      � 5_   
   $��2��6   $��10q�6   .+�5a      �� .+�5b      �� .+�5c      �� $��Payslip System��6    $��Center=�66   $��Center=�6S   $��20(�6n   $
��Bold"�6|   $��0,0,0,0q�6�   
�������$��Segoe UI Light)�6�   5d      �� 5f      $ 
Horizontal=�6   $��Center=�66   .+�5g      �� -   5        $��OpenPayrollInput��5g      $��Calculate Payslip��65   $��10q�6p   $��15(�6|   $��50��6�   $	��150��6�   
���   �
�������$��Segoe UI Light)�6�   �� 5h      -	   5        $��GeneratePayslipButton��5h      $��Generate Payslip��6:   $��10q�6y   $��15(�6�   $��50��6�   $	��150��6�   
���   �
�������$��Segoe UI Light)�6�   �� 5i      -
   5        $��ExportPayslipButton��5i      $��Export Payslip to PDF��68   $��10q�6z   $��15(�6�   $��50��6�   $	��150��6�   
���   �
�������$��Segoe UI Light)�6�   5l      5m      5o      5p      5r      