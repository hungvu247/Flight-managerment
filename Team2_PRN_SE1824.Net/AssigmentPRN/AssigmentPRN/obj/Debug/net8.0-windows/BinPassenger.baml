   M S B A M L   `   `   `  ����G  CAssigmentPRN, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:FlightManagementFlightManagement  5   	   #    FlightManagement.BinPassenger   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   0localclr-namespace:FlightManagement     5   	   _ [MaterialDesignThemes.Wpf, Version=4.3.0.3, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5   	     � Title$  PassengerManagement��5	   	   $	��850��6%   $
��1100��62      MaterialDesignBody#���   5
   	   $��Regular"�5   	   $��13(�5   	   $��Roboto)�5   	   .��5      �  � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $��* �6   �� 5      $
��Auto �6    � ColumnDefinitions 5   
   �� 5      $��* �6   �� 5      $	��300 �6   .+�5   
   ��  �� Orientation$ 
Horizontal=�6   
�������$��0��6E   $��2��6R   .+�5      �� -   5        $
��Back��5      
�������
���   �$��50��6K   $	��100��6W   $��5q�6c   $
��10,0q�6n     MaterialDesignFlatButton#��[ 5      5      ]� 5      $
��True��6   $��Right=�6-   .+�5      �� -   5        $��SearchTextBox��5      $	��200��6/   $��5q�6;   $��Center=�6F    ! MaterialDesignOutlinedTextBox#��[ 5       
�������5!      �� 5"      -   5        $��Search passenger��5"      $��5q�64   $
��10,0q�6?     MaterialDesignRaisedButton#��[ 5#      
�������
���   �$��50��5$   @   5%      5&      5'      &   System.Windows.Controls.DataGrid  5*   
   -   5        $��PassengerDataGrid��5*      $��1��6,   $��0��69      AutoGenerateColumns$ False��6I   $��5q�6e     MaterialDesignDataGrid#��[ 5+         Columns 5,      0  *System.Windows.Controls.DataGridTextColumn  5-      ,  &System.Windows.Controls.DataGridColumn   Header$ ID��6%      Width5  /System.Windows.Controls.DataGridLengthConverter$ * 6H   1  +System.Windows.Controls.DataGridBoundColumn   Binding 61   �� *Id+6H     5.      $ 
First Name��6%   $ 2* 6W    69   �� *	FirstName+6W     5/      $ 	Last Name��6%   $ 2* 6U    68   �� *
LastName+6U     50      $ Date of Birth��6%   $ 2* 6\    6<   �� *DateOfBirth+6\     51      $ Country��6%   $ 2* 6R    66   �� *	Country+6R     52      $ Email��6%   $ 3* 6N    64   �� *Email+6N     53      $ Gender��6%   $ 2* 6P    65   �� *Gender+6P   55      �� 57   
   $��2��6   $��0��6"   $ 
Horizontal=�62   $��Center=�6K   $��10q�6h   .+�58      �� -   5        $��PaginationPanel��58      $ 
Horizontal=�60   $��Center=�6I   $��5q�6f   59      �� 5<   
   $��1��6   $��1��6   $��2��6.   
���   �$��1q�6S   $��5q�6g   ._�5=      �� $��5q�6   
�������.+�5?      �� -   5        	 ��Name$	 lblPM��5?      $��Passenger Management��6'   $��20(�6F   $
��Left=�6T   $	��200��6o   $��37,0,0,0q�6{   �� 5A      -   5        $	 lblPassengerId��5A      $��2,2,2,2q�60   $��Passenger ID��6A   �� 5B      -   5        
 ��Name$
 txtPassengerID��5B      $��4,4,4,4q�62   $�� ��6C   #��[ 5C       �� TextWrapping$
 Wrap=�5D      ${�False��6.   
�������6@   �� 5F      -	   5        $	 lblFirstName��5F      $��2,2,2,2q�6.   $��
First Name��6?   �� 5G      -
   5        $
 txtFirstName��5G      $��4,4,4,4q�60   $�� ��6A   #��[ 5H      $
 Wrap=�5I      
�������6.   �� 5K      -   5        $	 lblLastName��5K      $��2,2,2,2q�6-   $��	Last Name��6>   �� 5L      -   5        $
 txtLastName��5L      $��4,4,4,4q�6/   $�� ��6@   #��[ 5M      $
 Wrap=�5N      
�������6.   �� 5P      -   5        $	 lblDateOfBirth��5P      $��2,2,2,2q�60   $��Date of Birth��6A   (  "System.Windows.Controls.DatePicker  5Q      -   5          Name$ dpDateOfBirth��5Q      $��4,4,4,4q�64      Text$  ��6E     MaterialDesignDatePicker#��[ 5R      
�������5S      �� 5U      -   5        $	 
lblCountry��5U      $��2,2,2,2q�6,   $��Country��6=   �� 5V      -   5        $
 
txtCountry��5V      $��4,4,4,4q�6.   $�� ��6?   #��[ 5W      $
 Wrap=�5X      
�������6.   �� 5Z      -   5        $	 lblEmail��5Z      $��2,2,2,2q�6*   $��Email��6;   �� 5[      -   5        $
 txtEmail��5[      $��4,4,4,4q�6,   $�� ��6=   #��[ 5\      $
 Wrap=�5]      
�������6.   �� 5_      -   5        $	 	lblGender��5_      $��2,2,2,2q�6+   $��Gender��6<   �� 5`      -   5         ��Name$ cbGender��5`      $��4q�6-     MaterialDesignComboBox#��[ 68   
�������.@�5a      �� $
��Male��6#   
 � Tag$
 Male��62   
�������6=   �� 5b      $��Female��6#   $ Female��64   
�������6A   �� 5c      $��Other��6#   $ Other��63   
�������6?   5d      �� 5e      -   5        $��Restore��5e      $��5q�6+   $��70��66   $
��10,0q�6A   #��[ 5f      
�������
���   �5g   A   5h      5i      5j      5k      