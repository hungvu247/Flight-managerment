   M S B A M L   `   `   `  ����G  CAssigmentPRN, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:FlightManagementFlightManagement  5   	   *    $FlightManagement.BaggageUpdateWindow   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   0localclr-namespace:FlightManagement     5   	     � Title$  BaggageUpdateWindow��5   	   $	��450��6%   $	��800��62      MaterialDesignBody#���   5	   	   $��Regular"�5
      $��13(�5      &   System.Windows.Media.TextOptions   TextFormattingMode$ Ideal=�5         TextRenderingMode$
 Auto=�5        MaterialDesignPaper#���  5        MaterialDesignFont#���  5      .��5      � .+�5   
   ]� .+�5      ��  �� Orientation$ 
Horizontal=�6   ���#D23F31$	��Top=�6G   .+�5      ]� $
��True��6   $��Right=�61   .+�5      �� -   5        $��	txtSearch��5      $��Center=�6/   
�������$	��200��6]   $��5q�6i    % !MaterialDesignFloatingHintTextBox#��[ 6t   �� 5      -   5        
��� � �$��Search��5   1   $��5q�6B   $
��10,0q�6M   
�������  MaterialDesignRaisedButton#��[ 6�    � ToolTip$/ )Resource name: MaterialDesignRaisedButton��6.  {�6�   �� *DataContext.ControlsEnabled+ �� RelativeSource �� *FindAncestor+ �� AncestorType
 Window6.  5      5      �� 5      .+�5      �� -   5         ��Name$ lvBookingBaggage��5      $
��1100��62   $��5q�6?    �� View 5      �� 5       -   5        gvBookingPlatform��.P�5!      �� 	 �� Width$	 90��6)   $��
FlightCode��64   
 �� DisplayMemberBinding
 6H   ��  �� Path$ 
FlightCode��� 5"      $
	 Auto��6)   $��Passenger Name��66   
 6N   �� $ PassengerName��� 5#      $
	 Auto��6)   $��Email��66   
 6E   �� $ Email��� 5$      $	 90��6)   $��Date of birth��64   
 6K   �� $	 Dob��� 5%      $	 60��6)   $��Weight��64   
 6D   �� $ Weight��� 5&      $	 90��6)   $��Platform��64   
 6F   �� $ PlatformName��� 5'      $		 240��6)   $��Departing Airport��65   
 6P   �� $ DepartingAirport��� 5(      $		 250��6-   $��Arriving Airport��69   
 6S   �� $ ArrivingAirport��� 5)      $
	 Auto��6)   $��Departing time��66   
 6N   �� $ DepartingTime��� 5*      $
	 Auto��6)   $��Arriving time��66   
 6M   �� $ ArrivingTime�5,      5.      �� 5/      $��1��6   $ 
Horizontal=�6&   $��Stretch=�6?   $��5q�6]   .+�50      �� -   5        $
��Auto��50      $��Stretch=�6&   $��<<��6D   $��5q�6W   6b   �� 51      -   5        $
��Auto��51      $��Stretch=�6&   $��>>��6D   $��5q�6W   6b   52      53      �� 56      
�������$��1q�6(   $��5q�6<   ._�57      �� $��5q�6   .+�59      �� -   5         ��Name$ lblAM��59      $��Manage baggage��6+   $��3��6D   $��0��6T   $��32(�6a   $
��Left=�6o   $	��466��5:      $��37,0,0,0q�6   �� 5;      $��Flight Code��6   $��2,2,2,2q�62   $��1��6C   $��0��6P   $��2��6`   �� 5<      -   5         ��Name$ txtFlightID��5<      ${�False��63   $��4,4,4,4q�6E   $��1��6V   $��2��6c   $�� ��6s    �� TextWrapping$
 Wrap=�6{   $��2��6�   �� 5=      $��Passenger Name��6   $��2,2,2,2q�65   $��2��6F   $��0��6S   $��2��6c   �� 5>      -	   5        $ txtPassengerName��5>      ${�False��68   $��4,4,4,4q�6J   $��2��6[   $��2��6h   $�� ��6x   $
 Wrap=�6�   $��2��6�   �� 5?      $��Platform booking name��6   $��2,2,2,2q�6<   $��2��6M   $��0��6Z   $��2��6j   �� 5@      -
   5        $ txtBookingPlatform��5@      ${�False��6:   $��4,4,4,4q�6L   $��2��6]   $��2��6j   $�� ��6z   $
 Wrap=�6�   $��2��6�   �� 5A      $��Departing airport��6   $��2,2,2,2q�68   $��2��6I   $��0��6V   $��2��6f   �� 5B      -   5        $ txtDepartingAirport��5B      ${�False��6;   $��4,4,4,4q�6M   $��2��6^   $��2��6k   $�� ��6{   $
 Wrap=�6�   $��2��6�   �� 5C      $��Arriving airport��6   $��2,2,2,2q�67   $��2��6H   $��0��6U   $��2��6e   �� 5D      -   5        $ txtArrivingAirport��5D      ${�False��6:   $��4,4,4,4q�6L   $��2��6]   $��2��6j   $�� ��6z   $
 Wrap=�6�   $��2��6�   �� 5E      $��Departing time��6   $��2,2,2,2q�65   $��2��6F   $��0��6S   $��2��6c   �� 5F      -   5        $ txtDepartingTime��5F      ${�False��68   $��4,4,4,4q�6J   $��2��6[   $��2��6h   $�� ��6x   $
 Wrap=�6�   $��2��6�   �� 5G      $��Arriving time��6   $��2,2,2,2q�64   $��2��6E   $��0��6R   $��2��6b   �� 5H      -   5        $ txtArrivingTime��5H      ${�False��67   $��4,4,4,4q�6I   $��2��6Z   $��2��6g   $�� ��6w   $
 Wrap=�6   $��2��6�   �� 5I      $��Weight (kg)��6   $��2,2,2,2q�62   $��3��6C   $��0��6P   $��2��6`   �� 5J      -   5        $ 	txtWeight��5J      $��4,4,4,4q�61   $��3��6B   $��2��6O   $�� ��6_   $
 Wrap=�6g   $��2��6{   �� 5L      $ 
Horizontal=�6!   $��Center=�6:   $��40,0,0,0q�6U   .+�5M      �� -   5        $��Change baggage��5M   !   $��5q�6:   $
��10,0q�6E   
��� � �$	��150��6g   $��30��6s   
�������6�   �� 5N      -   5        $��Delete baggage��5N   !   $��5q�6:   $
��10,0q�6E   
��� � �$	��150��6g   $��30��6s   
�������6�   5O      �� 5P      $ 
Horizontal=�6!   $��0,20,5,0q�6;   .+�5Q      �� $��Platform booking name��6    $��2,2,2,2q�6@   $��3��6Q   $��0��6^   $��2��6n   �� 5R      -   5        $��cbBoxBookingPlatform��5R   H   $	��160��6e   $��30��6q   5S      �� 5T      $ 
Horizontal=�6!   $��0,20,0,0q�6:   .+�5U      �� $��Airport��6    $��2,2,2,2q�62   $��3��6C   $��0��6P   $��2��6`   �� 5V      -   5        $��cbBoxDepartAirport��5V   F   $��2,2,2,2q�6`   $	��150��6q   $��30��6}   �� 5W      -   5        $��cbBoxArriveAirport��5W   F   $	��150��6a   $��30��6m   5X      �� 5Y      -   5         ��Name$ 
btnRefresh��5Y      $��Refresh��61   $��5q�6C   $
��10,0q�6N   
��� � �
�������$��30��6�   $	��150��6�   6�   �� 5Z      $ 
Horizontal=�6!   $��0,0,0,0q�6:   .+�5\      �� -   5        $ txtBookingId��5\   "   $y�Hidden=�68   $��4,4,4,4q�6L   $��3��6]   $��2��6j   $�� ��6z   $
 Wrap=�6�   $��2��6�   �� 5]      -   5        $ txtEmail��5]   "   $y�Hidden=�64   $��3��6H   $��2��6U   $�� ��6e   $
 Wrap=�6m   $��2��6�   �� 5^      -   5        $ txtGid��5^   "   $y�Hidden=�62   $��3��6G   $��2��6T   $�� ��6d   $
 Wrap=�6l   $��2��6�   5_      5a      5c      5d      5e      5f      