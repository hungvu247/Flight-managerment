   M S B A M L   `   `   `  ����G  CAssigmentPRN, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:FlightManagementFlightManagement  5   	   (    "FlightManagement.AirlineManagement   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   0localclr-namespace:FlightManagement     5   	   _ [MaterialDesignThemes.Wpf, Version=4.3.0.3, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5   	     � Title$  AirlineManagement��5	   	   $	��550��6#   $	��850��60      MaterialDesignBody#���   5
   	   $��Regular"�5   	   $��13(�5   	   $��Roboto)�5   	   .��5      �  � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $��* �6   �� 5      $
��Auto �6    � ColumnDefinitions 5   
   �� 5      $��* �6   �� 5      $	��300 �6   .+�5   
   ��  �� Orientation$ 
Horizontal=�6   
�������$��0��6E   $��2��6R   .+�5      �� -   5        $��Add new airline��5      
�������
���   �$��50��6V   $��5q�6b   $
��10,0q�6m     MaterialDesignFlatButton#��[ 5      5      �� 5      -   5        $��Update airline��5      
�������
���   �$��5q�6U   $
��10,0q�6`   #��[ 5      $��50��5    *   �� 5!      -   5        $��Delete airline��5!      
�������
���   �$��5q�6U   $
��10,0q�6`   #��[ 5"      $��50��5#   "   �� 5$      -   5        $	��Bin��5$      
�������
���   �$��5q�6J   $
��10,0q�6U   #��[ 5%      $��50��5&      $	��100��6+   ]� 5'      $
��True��6   $��Right=�6-   .+�5(      �� -   5        $��SearchTextBox��5(      $	��200��6/   $��5q�6;   $��Center=�6F    ! MaterialDesignOutlinedTextBox#��[ 5)      
�������5*      �� 5+      -   5        $��Search airline��5+      $��5q�62   $
��10,0q�6=     MaterialDesignRaisedButton#��[ 5,      
�������
���   �$��50��5.   .   5/      51      &   System.Windows.Controls.DataGrid  54   
   -   5        $��AirlineDataGrid��54      $��1��6*   $��0��67      AutoGenerateColumns$ False��6G   $��5q�6c     MaterialDesignDataGrid#��[ 55         Columns 56      0  *System.Windows.Controls.DataGridTextColumn  57      ,  &System.Windows.Controls.DataGridColumn   Header$ ID��6%      Width5  /System.Windows.Controls.DataGridLengthConverter$ * 6H   1  +System.Windows.Controls.DataGridBoundColumn   Binding 61   �� *Id+6H     58      $
 Code��6%   $ 2* 6L    63   �� *Code+6L     59      $
 Name��6%   $ 3* 6L    63   �� *Name+6L     5:      $ Country��6%   $ 2* 6R    66   �� *	Country+6R   5<      �� 5?   
   $��2��6   $��0��6"   $ 
Horizontal=�62   $��Center=�6K   $��10q�6h   .+�5@      �� -   5        $��PaginationPanel��5@      $ 
Horizontal=�60   $��Center=�6I   $��5q�6f   5A      �� 5D   
   $��1��6   $��1��6   $��2��6.   
���   �$��1q�6S   $��5q�6g   ._�5E      �� $��5q�6   
�������.+�5G      �� -	   5        	 ��Name$	 lblAM��5G      $��Airline Management��6'   $��20(�6D   $
��Left=�6R   $	��200��6m   $��37,0,0,0q�6y   �� 5I      -
   5        $	 lblAirlineId��5I      $��2,2,2,2q�6.   $��
Airline ID��6?   �� 5J      -   5        
 ��Name$
 txtAirlineID��5J      $��4,4,4,4q�60   $�� ��6A   #��[ 5K       �� TextWrapping$
 Wrap=�5L      ${�False��6.   
�������6@   �� 5N      -   5        $	 lblAirlineCode��5N      $��2,2,2,2q�60   $��Airline Code��6A   �� 5O      -   5        $
 txtAirlineCode��5O      $��4,4,4,4q�62   $�� ��6C   #��[ 5P      $
 Wrap=�5Q      
�������6.   �� 5S      -   5        $	 lblAirlineName��5S      $��2,2,2,2q�60   $��Airline Name��6A   �� 5T      -   5        $
 txtAirlineName��5T      $��4,4,4,4q�62   $�� ��6C   #��[ 5U      $
 Wrap=�5V      
�������6.   �� 5X      -   5        $	 lblAirlineCountry��5X      $��2,2,2,2q�63   $��Country��6D   �� 5Y      -   5        $
 txtAirlineCountry��5Y      $��4,4,4,4q�65   $�� ��6F   #��[ 5Z      $
 Wrap=�5[      
�������6.   �� 5\      -   5        $��Clear��5\      $��5q�6)   $��70��64   $
��10,0q�6?   
�������#��[ 5]      
���   �5^   ,   5_      5`      5a      5b      