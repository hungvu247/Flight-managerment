   M S B A M L   `   `   `  ����G  CAssigmentPRN, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullG(clr-namespace:Team2_SE1824_FlightManagerTeam2_SE1824_FlightManager  5   	   .    (Team2_SE1824_FlightManager.BookingWindow   5   	   _ [MaterialDesignThemes.Wpf, Version=4.3.0.3, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   :local(clr-namespace:Team2_SE1824_FlightManager     5   	     � Title$  BookingWindow��5	   	   $	��700��6   $
��1550��6,   .��5
      � .+�5   
   ]� .+�5      ��  �� Orientation$ 
Horizontal=�6   ���#D23F31$	��Top=�6G   .+�5      ]� $
��True��6   $��Right=�61   5      �� 5      $ 
Horizontal=�6   
���  ��$	��Top=�6C   $��65��6X   .+�5      �� $ Vertical=�6   $��10, 0, 0, 0q�64   .+�5      �� $��Search Passenger��6   $��Center=�67   
�������6R   �� 5      -   5         ��Name$ 
txtSearchP��5   5   
�������$	��150��6I   $��0,0, 0,0q�6U   $
��Left=�5      $��24��69   6E   5      �� 5      $ Vertical=�6   $��10, 0, 0, 0q�64   .+�5      �� $��Search Flight��6   $��Center=�64   
�������6O   �� 5      -   5        $ txtSearchFlight��5   5   
�������$	��150��6N   $��0,0, 0,0q�6Z   $
��Left=�5      $��24��69   6E   5      �� 5      $ Vertical=�6   $��10, 0, 0, 0q�64   .+�5      �� $��Search Booking Platform��6   $��Center=�6>   
�������6Y   �� 5      -   5         ��Name$ cboSearchBP��5      $	��185��64   
�������$��24��6S   5       5!      �� 5"      $ Vertical=�6   $��Right=�64   .+�5#      �� -   5         ��Name$ btnResetFilter��5#      $��Reset Filter��65   $	��158��6L   $��46��6X   
�������
���  ��$��20, 10, 0, 0q�5$   A   6W   5%      �� 5&      $ Vertical=�6   $��Right=�64   .+�5'      �� -   5        $ btnExportExcel��5'      $��Export to Excel��65   $	��158��6O   $��46��6[   
�������
���  ��$��20, 10, 0, 0q�5(   A   6W   5)      �� 5*      $ Vertical=�6   $��56��64   $��30, 10, 0, 0q�6?   .+�5+      �� -   5        $ btnBin��5+      $	��Bin��6-   $��5q�6;   $
��10,0q�6F   
��� � �
�������5,      5-      5/      � 51      $��1��6   $��2��6     � ColumnDefinitions 52      �� 53      $	��61* �6'   �� 54      $
��314* �6'   �� 55      $
��125* �6'   .+�5:      �� $��5,5,5,5q�6   $��2��6.   $��1��6>   .+�5;      �� $��Center=�6   $��Booking��6@   $��25(�6R   $
��Bold"�6`   
���  ��6�   �� 5=      $��0��6!   $��0��6.   $��5q�6>   .+�5>      �� $��ID��6    $��Center=�6-   �� 5?      -   5        $��txtID��5?   4    �� 
IsReadOnly$
 True��6"   $	��185��6A   $
��Left=�6M   5@      �� 5B      $��0��6!   $��1��6.   $��5q�6>   .+�5C      �� $��	Passenger��6    $��Center=�64   �� 5D      -   5        $ searchPassenger��5D   "   $	��120��6;   $
��0,10q�6G   $
��Left=�6U   6p   �� 5E      -	   5        $ cboPassenger��5E   #   $	��185��69   $
��Left=�6E   5F      �� 5H      $��0��6!   $��2��6.   $��5q�6>   .+�5I      �� $��Flight��6    $��Center=�61   �� 5J      -
   5        $ 	cboFlight��5J   #   $	��185��66   $
��Left=�6B   5K      �� 5M      $��0��6!   $��3��6.   $��5q�6>   .+�5N      �� $��Booking Platform��6    $��Center=�6;   �� 5O      -   5        $ cboBookingPlat��5O   #   $	��185��6;   $
��Left=�6G   5P      �� 5S      $��1��6!   $��0��6.   $��5q�6>   .+�5T      �� $��Booking Time��6    $��Center=�67   �� 5U      -   5        $��txtBookingTime��5U   4   $
 True��6"   $	��185��6J   $
��Left=�6V   5V      �� 5X      $ 
Horizontal=�6!   $��1��6:   $��0��6G   $��5q�6W   .+�5Y      �� -   5        $ btnAdd��5Y   !   $	��Add��61   $��66��6?   $��30��6J   
�������
���  ��$��0, 10, 0, 0q�5Z   L   6a   �� 5[      -   5        $ 	btnUpdate��5[   !   $��Update��64   $��80��6E   $��30��6P   
�������
���  ��$��20, 10, 0, 0q�5\   L   6b   �� 5]      -   5        $ 	btnDelete��5]   !   $��Right=�64   $��Delete��6P   $��80��6a   $��30��6l   
�������
���  ��$��20, 10, 0, 0q�5^   L   6b   �� 5_      -   5        $ btnClear��5_   !   $��Right=�63   $��Clear��6O   $��70��6_   $��30��6j   
�������
���  ��$��20, 10, 0, 0q�5`   L   6b   5a      5b      &   System.Windows.Controls.DataGrid  5f      -   5        $��dgData��5f         AutoGenerateColumns$ False��6)   $��5,5,4,54q�6E   $��0��6�   $��2��6�      	RowHeight$ 35Q�5g      	   Columns	 5h      0  *System.Windows.Controls.DataGridTextColumn  5i      ,  &System.Windows.Controls.DataGridColumn
   Header$
 ID��6-      Width5  /System.Windows.Controls.DataGridLengthConverter$
 Auto 6P   1  +System.Windows.Controls.DataGridBoundColumn   Binding 69   �� *Id+6P     5j      $
 	Passenger��6-   $
 Auto 6g    6@   �� *Passenger.FullName+6g     5k      $
 Flight��6-   $
 Auto 6d    6=   �� *Flight.InforFlight+6d     5l      $
 Booking Platform��6-   $
 Auto 6p    6G   �� *BookingPlatform.Name+6p     5m      $
 Booking Time��6-   $
 Auto 6c    6C   �� *BookingTime+6c   5o      �� 5p      $ 
Horizontal=�6   $��Bottom=�66   $
��Left=�6Q   $��
300,0,0,10q�6l   $��1��6�   $��44��6�   .+�5q      �� -   5        $ btnPreviousPage��5q      $��Previous��66   $	��100��6g   
�������6s   �� 5r      $
��10,0q�6=   $��Center=�6K   ��6    �� *CurrentPage+6K   �� 5s      -   5        $ btnNextPage��5s      $
��Next��62   $	��100��6[   
�������6g   5t      5u      5v      5w      5x      