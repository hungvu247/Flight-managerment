   M S B A M L   `   `   `  ����G  CAssigmentPRN, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullG(clr-namespace:Team2_SE1824_FlightManagerTeam2_SE1824_FlightManager  5   	   +    %Team2_SE1824_FlightManager.BinBooking   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   :local(clr-namespace:Team2_SE1824_FlightManager     5   	     � Title$  
BinBooking��5   	   $	��750��6   $
��1200��6)   .��5	      � .+�5
   
   ]� .+�5      ��  �� Orientation$ 
Horizontal=�6   ���#D23F31$	��Top=�6G   .+�5      ]� $
��True��6   $��Right=�61   5      �� 5      $ 
Horizontal=�6   ���#D23F31$	��Top=�6G   $��65��6\   .+�5      �� $ Vertical=�6   $��10, 0, 0, 0q�64   .+�5      �� $��Search Passenger��6   $��Center=�67   
�������6R   �� 5      -   5         ��Name$ 
txtSearchP��5      $	��150��62   $��0,0, 0,0q�6>   
�������$
��Left=�5      $��24��69   6E   5      �� 5      $ Vertical=�6   $��10, 0, 0, 0q�64   .+�5      �� $��Search Flight��6   $��Center=�64   
�������6O   �� 5      -   5        $ txtSearchFlight��5      $	��150��67   $��0,0, 0,0q�6C   
�������$
��Left=�5      $��24��69   6E   5      �� 5      $ Vertical=�6   $��10, 0, 0, 0q�64   .+�5      �� $��Search Booking Platform��6   $��Center=�6>   
�������6Y   �� 5      -   5         ��Name$ cboSearchBP��5      $	��185��64   
�������$��24��6S   5      5       �� 5!      $ Vertical=�6   $��Right=�64   .+�5"      �� -   5         ��Name$ btnResetFilter��5"      $��Reset Filter��65   $	��185��6L   $��46��6X   
�������
���  ��$��40, 10, 0, 0q�5#   A   6W   5$      �� 5%      $ Vertical=�6   $��Right=�64   .+�5&      �� -   5        $ btnRestoreAll��5&      $��Restore All��64   $	��185��6J   $��46��6V   
�������
���  ��$��40, 10, 0, 0q�5'   A   6W   5(      �� 5)      $ Vertical=�6   $��Right=�64   .+�5*      �� -   5        $ btnDeleteAll��5*      $��
Delete All��63   $	��185��6H   $��46��6T   
�������
���  ��$��40, 10, 0, 0q�5+   A   6W   5,      5.      � 50      $��1��6   $��2��6     � ColumnDefinitions 51      �� 52      $	��61* �6'   �� 53      $
��314* �6'   �� 54      $
��125* �6'   .+�59      �� $��5,5,5,5q�6   $��2��6.   $��1��6>   .+�5:      �� $��Center=�6   $��Booking��6@   $��25(�6R   $
��Bold"�6`   
���  ��6�   �� 5<      $��0��6!   $��0��6.   $��5q�6>   .+�5=      �� $��ID��6    $��Center=�6-   �� 5>      -   5        $��txtID��5>   4    �� 
IsReadOnly$
 True��6"   $	��185��6A   $
��Left=�6M   5?      �� 5A      $��0��6!   $��1��6.   $��5q�6>   .+�5B      �� $��	Passenger��6    $��Center=�64   �� 5C      -   5        $ cboPassenger��5C   5   ${�False��6#   $	��185��6K   $
��Left=�6W   5D      �� 5F      $��0��6!   $��2��6.   $��5q�6>   .+�5G      �� $��Flight��6    $��Center=�61   �� 5H      -	   5        $ 	cboFlight��5H   5   ${�False��6#   $	��185��6H   $
��Left=�6T   5I      �� 5K      $��0��6!   $��3��6.   $��5q�6>   .+�5L      �� $��Booking Platform��6    $��Center=�6;   �� 5M      -
   5        $ cboBookingPlat��5M   5   ${�False��6#   $	��185��6M   $
��Left=�6Y   5N      �� 5Q      $��1��6!   $��0��6.   $��5q�6>   .+�5R      �� $��Booking Time��6    $��Center=�67   �� 5S      -   5        $��txtBookingTime��5S   4   $
 True��6"   $	��185��6J   $
��Left=�6V   5T      �� 5V      $ 
Horizontal=�6!   $��1��6:   $��0��6G   $��5q�6W   .+�5W      �� -   5        $ 
btnRestore��5W   !   $
��Left=�65   $��Restore��6P   $��80��6b   $��30��6m   
�������
���  ��$��0, 10, 0, 0q�5X   L   6a   �� 5Y      -   5        $ 	btnDelete��5Y   !   $��Right=�64   $��Delete��6P   $��80��6a   $��30��6l   
�������
���  ��$��20, 10, 0, 0q�5Z   L   6b   �� 5[      -   5        $ btnClear��5[   !   $��Right=�63   $��Clear��6O   $��70��6_   $��30��6j   
�������
���  ��$��20, 10, 0, 0q�5\   L   6b   5]      5^      &   System.Windows.Controls.DataGrid  5b      -   5        $��dgData��5b         AutoGenerateColumns$ False��6)   $��5,5,5,5q�6E   $��0��6�   $��2��6�      Columns 5c      4  .System.Windows.Controls.DataGridCheckBoxColumn  5d      ,  &System.Windows.Controls.DataGridColumn	   Header$	 Choose��61   0  *System.Windows.Controls.DataGridTextColumn  5e      $	 ID��6-   
   Width5  /System.Windows.Controls.DataGridLengthConverter$
 * 6P   1  +System.Windows.Controls.DataGridBoundColumn   Binding 69   �� *Id+6P     5f      $	 	Passenger��6-   $
 3* 6g    6@   �� *Passenger.FullName+6g     5g      $	 Flight��6-   $
 3* 6d    6=   �� *Flight.InforFlight+6d     5h      $	 Booking Platform��6-   $
 3* 6p    6G   �� *BookingPlatform.Name+6p     5i      $	 Booking Time��6-   $
 3* 6c    6C   �� *BookingTime+6c   5k      5l      5m      5n      5o      