   M S B A M L   `   `   `  ����G  CAssigmentPRN, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:FlightManagementFlightManagement  5   	   -    'FlightManagement.BookingPlatformHistory   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   0localclr-namespace:FlightManagement     5   	     � Title$  BookingPlatformHistory��5   	   $	��450��6(   $	��800��65      MaterialDesignBody#���   5	   
   $��Regular"�5
      $��13(�5      &   System.Windows.Media.TextOptions   TextFormattingMode$ Ideal=�5         TextRenderingMode$
 Auto=�5        MaterialDesignPaper#���  5        MaterialDesignFont#���  5      .��5      � .+�5   
   ]� .+�5      ��  �� Orientation$ 
Horizontal=�6   ���#D23F31$	��Top=�6G   .+�5      �� -   5        $��Restore platform��5      $��5q�64   $
��10,0q�6?   
��� � �
�������6t   ]� 5      $
��True��6   $��Right=�61   .+�5      �� -   5        $��	txtSearch��5      $��Center=�6/   
�������$	��200��6]   $��5q�6i    % !MaterialDesignFloatingHintTextBox#��[ 6t   �� 5      -   5        $��Search��5      $��5q�6.   $
��10,0q�69   
��� � �
�������6n   5      5      �� 5      .+�5      �� -   5         ��Name$ lvBookingPlatform��5      $	��450��66   $��5q�6B    �� View 5      �� 5      -   5        gvBookingPlatform��.P�5       ��  �� Width$	 100��6-   $��Platform name��69    �� DisplayMemberBinding 6P   ��  �� Path$
 Name��� 5!      $	 300��6-   $	��Url��69    6F   �� $	 Url�5"      5&      �� 5'      $��1��6   $ 
Horizontal=�6*   $��Stretch=�6C   $��5q�6a   .+�5(      �� -   5        $
��Auto��5(      $��Stretch=�6*   $��<<��6H   $��5q�6[   6f   �� 5)      -   5        $
��Auto��5)      $��Stretch=�6*   $��>>��6H   $��5q�6[   6f   5*      5+      �� 5/      
�������$��1q�6(   $��5q�6<   ._�50      �� $��5q�6   .+�52      �� -   5        	 ��Name$	 lblAM��52      $��Manage booking platform��6+   $��3��6M   $��0��6]   $��32(�6j   $
��Left=�6x   $	��466��53      $��37,0,0,0q�6   �� 54      $��Flight Code��6   $��2,2,2,2q�62   $��1��6C   $��0��6P   $��2��6`   �� 55      -	   5        
 ��Name$
 txtBookingPlatformID��55      ${�False��6<   $��4,4,4,4q�6N   $��1��6_   $��2��6l   $�� ��6|    �� TextWrapping$
 Wrap=�6�   $��2��6�   �� 56      $��Booking platform name��6   $��2,2,2,2q�6<   $��2��6M   $��0��6Z   $��2��6j   �� 57      -
   5        $
 txtBookingPlatformName��57      $��4,4,4,4q�6>   $��2��6O   $��2��6\   $�� ��6l   $
 Wrap=�6t   $��2��6�   �� 58      $	��Url��6   $��2,2,2,2q�6*   $��3��6;   $��0��6H   $��2��6X   �� 59      -   5        $
 txtBookingPlatformURL��59      ${�False��6=   $��4,4,4,4q�6O   $��3��6`   $��2��6m   $�� ��6}   $
 Wrap=�6�   $��2��6�   �� 5:      -   5         ��Name$ 
btnRefresh��5:      $��Refresh��61   $��5q�6C   $
��10,0q�6N   
��� � �
�������$	��150��6�   6�   5;      5<      5=      5>      5?      