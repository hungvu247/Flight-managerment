   M S B A M L   `   `   `  ����G  CAssigmentPRN, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:AssigmentPRNAssigmentPRN  5   	       AssigmentPRN.BinFlight   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   ,localclr-namespace:AssigmentPRN     5   	   _ [MaterialDesignThemes.Wpf, Version=4.3.0.3, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5	   	     � Title$  	BinFlight��5   	   $	��700��6   $
��1000��6(      MaterialDesignBody#���   5
   
   $��Regular"�5   
   $��13(�5   
   &   System.Windows.Media.TextOptions   TextFormattingMode$ Ideal=�5   
      TextRenderingMode$
 Auto=�5   
     MaterialDesignPaper#���  5   
     MaterialDesignFont#���  5   
   .��5      � .+�5   
   ]� .+�5      ��  �� Orientation$ 
Horizontal=�6   ���#D23F31$	��Top=�6G   .+�5      �� -   5        $��Restore��5      $��5q�6+   $
��10,0q�66   
��� � �
�������6k   �� 5      -   5        $��Restore All��5      $��5q�6/   $
��10,0q�6:   
��� � �
�������6o   ]� 5      $
��True��6   $��Right=�61   .+�5      �� -   5        $��SearchTextBox��5   1   
�������$	��200��6F   $��5q�6R   $��Center=�6]   6x   5      5      �� 5      $ 
Horizontal=�6   ���#D23F31$	��Top=�6G   .+�5      �� $ Vertical=�6   $��10, 0, 0, 0q�64   .+�5       �� $��Airline��6   $��Center=�6.   
�������6I   �� 5!      -   5         ��Name$ cmbAirlineSelect��5!      $	��185��69    �� DisplayMemberPath$
 Name��6E    �� SelectedValuePath$ Id��6^   
�������6�   5"      �� 5#      $ Vertical=�6   $��10, 0, 0, 0q�64   .+�5$      �� $��Departing Airport��6   $��Center=�68   
�������6S   �� 5%      -   5        $ cmbDepartingAirportSelect��5%      $	��185��6B   $
 Name��6N   $ Id��6g   
�������6�   5&      �� 5'      $ Vertical=�6   $��10, 0, 0, 0q�64   .+�5(      �� $��Arriving Airport��6   $��Center=�67   
�������6R   �� 5)      -   5        $ cmbArrivingAirportSelect��5)      $	��185��6A   $
 Name��6M   $ Id��6f   
�������6�   5*      �� 5+      $ Vertical=�6   $��Right=�64   .+�5,      �� -   5        $��Reset filter��5,      $	��185��64   $��35��6@   
�������
���  ��$��40, 10, 0, 0q�6q   6�   5-      5/      � 51      $��1��6   $��2��6     � ColumnDefinitions 52      �� 53      $	��61* �6'   �� 54      $
��314* �6'   �� 55      $
��125* �6'   .+�58      �� $��5,5,5,5q�6   $��2��6.   $��1��6>   .+�59      �� $��Flight Manager��6   $��25(�65   $
��Bold"�6C   
���  ��6h   �� 5;      $��0��6!   $��0��6.   $��5q�6>   .+�5<      �� $��ID��6    $��Center=�6-   �� 5=      -   5        $��txtID��5=   "   $	��185��6/    �� 
IsReadOnly$
 True��6;   5>      �� 5@      $��0��6!   $��1��6.   $��5q�6>   .+�5A      �� $��Airline��6    $��Center=�62   �� 5B      -	   5        $ 
cmbAirline��5B   #   $	��185��67   $
 Name��6C   $ Id��6\   5C      �� 5E      $��0��6!   $��2��6.   $��5q�6>   .+�5F      �� $��Departing Airport��6    $��Center=�6<   �� 5G      -
   5        $ cmbDepartingAirport��5G   #   $
 Name��6@   $ Id��6Y   $	��185��6p   5H      �� 5J      $��0��6!   $��3��6.   $��5q�6>   .+�5K      �� $��Arriving Airport��6    $��Center=�6;   �� 5L      -   5        $ cmbArrivingAirport��5L   #   $
 Name��6?   $ Id��6X   $	��185��6o   5M      �� 5P      $��1��6!   $��0��6.   $��5q�6>   .+�5Q      �� $��Departing Gate��6    $��Center=�69   �� 5R      -   5        $��txtDepartingGate��5R   "   $	��185��6:   5S      �� 5U      $��1��6!   $��1��6.   $��5q�6>   .+�5V      �� $��Arriving Gate��6    $��Center=�68   �� 5W      -   5        $��txtArrivingGate��5W   "   $	��185��69   5X      �� 5Z      $��1��6!   $��2��6.   $��5q�6>   .+�5[      �� $��Departure Time��6    $��Center=�69   (  "System.Windows.Controls.DatePicker  5\      -   5        	  Name$	 dpDepartureDate��5\   %   $	��150��6>   $
��Left=�6J   )  #MaterialDesignThemes.Wpf.TimePicker  5]      -   5        
  Name$
 tpDepartureTime��5^      $	��150��5_      $��10,0,0,0q�5`      )  #MaterialDesignThemes.Wpf.HintAssist   Hint$ With Seconds��5a         WithSeconds$
 True��5d      $��1��5e         	Is24Hours 5c      �� *	IsChecked+ �� ElementName$ Is24HourCheckbox��5e      5f      �� 5h      $��1��6!   $��3��6.   $��5q�6>   .+�5i      �� $��Arrival Time��6    $��Center=�67     5j      -   5        $	 dpArrivalDate��5j   %   $	��150��6<   $
��Left=�6H     5k      -   5        $
 tpArrivalTime��5l      $	��150��5m      $��10,0,0,0q�5n      $ With Seconds��5o      $
 True��5q      $��1��5r       5p      �� *	IsChecked+$ Is24HourCheckbox��5r      5s      5w      &   System.Windows.Controls.DataGrid  5y      -   5        $��FlightDataGrid��5y      $��5,5,5,5q�62   $��0��6v   $��2��6�   5{      5|      5}      5~      