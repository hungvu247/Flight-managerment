   M S B A M L   `   `   `  ����G  CAssigmentPRN, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:AssigmentPRNAssigmentPRN  5   	   &     AssigmentPRN.ManageAirportWindow   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   ,localclr-namespace:AssigmentPRN     5   	   _ [MaterialDesignThemes.Wpf, Version=4.3.0.3, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5	   	     � Title$  ManageAirportWindow��5   	   $	��610��6%   $	��920��62      MaterialDesignBody#���   5
   	   $��Regular"�5   	   $��13(�5   	   &   System.Windows.Media.TextOptions   TextFormattingMode$ Ideal=�5   	      TextRenderingMode$
 Auto=�5   	     MaterialDesignPaper#���  5   	     MaterialDesignFont#���  5   	   .��5      � .+�5   
   ]� .+�5      ��  �� Orientation$ 
Horizontal=�6   ���#D23F31$	��Top=�6G   .+�5      ]� $
��True��6   .+�5      '  !MaterialDesignThemes.Wpf.PackIcon     Kind$ Airplane=�6.   $
��Left=�6>   $��Center=�6T   �� 5      $��Manage airport��6   $��30(�65   $
��Bold)�6C   $��Center=�6U   $
��Left=�6p   $��	0,0,270,0q�6�   �� 5      -   5        $��	txtSearch��5      $	��100��6/   $��5q�6;   $��Center=�6F   �� 5      -   5        $��comboBoxSearch��5       �� SelectedIndex$ 1��65   
�������$��5q�6Z   $
��10,0q�6e    " MaterialDesignOutlinedComboBox#��[ 6t   .@�5      ��  �� 
IsSelected$
 True��6'   
 � Tag$
 Name��69   .��6D   Name6J   �� 5       $ Country��6'   .��65   	Country6>   �� 5!      $
 City��6'   .��62   City68   �� 5"      $
 Code��6'   .��62   Code68   �� 5#      $ State��6'   .��63   State6:   5$      �� 5%      -   5        $��Search airport��5%      $��5q�66   $
��10,0q�6A   
��� d �
�������6z   �� 5&      -   5        $��Export excel��5&      $��5q�64   $
��10,0q�6?   
����ح�
�������6x   5'      5(      ]� 5*      $
��True��6   .+�5,      &   System.Windows.Controls.DataGrid  -   5        $��AirportDataGrid��5,         AutoGenerateColumns$ False��62   $��5q�6N   	   CanUserAddRows$	 False��6�   $	��Top=�6�   $
��Auto��6�   
   Columns
 5-      0  *System.Windows.Controls.DataGridTextColumn  5.      ,  &System.Windows.Controls.DataGridColumn   Header$ ID��6-      Width5  /System.Windows.Controls.DataGridLengthConverter$ 50 6P   1  +System.Windows.Controls.DataGridBoundColumn   Binding 69   �� *Id+6P     5/      $
 Code��6-   $	 100 6T    6;   �� *Code+6T     50      $
 Name��6-   $	 150 6T    6;   �� *Name+6T     51      $ Country��6-   $	 100 6Z    6>   �� *	Country+6Z     52      $ State��6-   $	 100 6V    6<   �� *State+6V     53      $
 City��6-   $	 100 6T    6;   �� *City+6T   55      �� 58      $ 
Horizontal=�6   $��Bottom=�66   $
��Left=�6Q   $��5q�6l   .+�59      �� -   5         ��Name$ btnPreviousPage��59      $��<��66   $
��10,0q�6E   
����ح�
�������$��30��6�   $��15(�6�   $��20��6�   �� 5:      -   5        $ btnNextPage��5:      $��>��62   $��
10,0,450,0q�6A   $
��10,0q�6U   
����ح�
�������$��30��6�   $��15(�6�   $��20��6�   5;      5<      �� 5?      
�������$��1q�6(   $��5q�6<   $
��Auto��6G   ._�5@      �� $��5q�6   .+�5A      �� -   5         ��Name$ lblAM��5A      $��Airport Detail��6+   $��15(�6D   $	��466��6R   $
��Bold)�6^   �� 5B      -	   5        $ lblAirportId��5B      $��
Airport ID��62   $��2q�6G   �� 5C      -
   5         ��Name$ txtAirportID��5C      $��4q�64   $�� ��6?    �� 
IsReadOnly$
 True��6G   �� 5E      -   5        $ lblAirportCode��5E      $��Airport Code��64   $��2q�6K   �� 5F      -   5        $ txtAirportCode��5F      $��4q�66   $�� ��6A   �� 5H      -   5        $ lblAirportName��5H      $��Airport Name��64   $��2q�6K   �� 5I      -   5        $ txtAirportName��5I      $��4q�66   $�� ��6A   �� 5K      -   5        $ lblAirportCountry��5K      $��Country��67   $��2q�6I   �� 5L      -   5        $ txtAirportCountry��5L      $��4q�69   $�� ��6D   �� 5N      -   5        $ lblAirportState��5N      $��State��65   $��2q�6E   �� 5O      -   5        $ txtAirportState��5O      $��4q�67   $�� ��6B   �� 5Q      -   5        $ lblAirportCity��5Q      $
��City��64   $��2q�6C   �� 5R      -   5        $ txtAirportCity��5R      $��4q�66   $�� ��6A   �� 5T      $ 
Horizontal=�6!   .+�5U      �� -   5        $��Add airport��5U   !   
��� � �  MaterialDesignRaisedButton#��[ 6�    � ToolTip$/ )Resource name: MaterialDesignRaisedButton��6�   $��2q�6'  $	��132��6D  {�6J   �� *DataContext.ControlsEnabled+ �� RelativeSource �� *FindAncestor+ �� AncestorType
 Window6D  �� 5V      -   5        $��Update airport��5V   !   
����ح�#��[ 6�   $/ )Resource name: MaterialDesignRaisedButton��6�   $��2q�6.  $	��122��6N  {�6Q   �� *DataContext.ControlsEnabled+ �� *FindAncestor+
 Window6N  5W      �� 5X      $ 
Horizontal=�6!   .+�5Y      �� -   5        $��Reset��5Y   !   
���  ��#��[ 6�   $/ )Resource name: MaterialDesignRaisedButton��6�   $��2q�6  $	��132��68  {�6B   �� *DataContext.ControlsEnabled+ �� *FindAncestor+
 Window68  �� 5Z      -   5        $��Delete airport��5Z   !   #��[ 6�   $/ )Resource name: MaterialDesignRaisedButton��6�   $��2q�6  $	��122��67  {�6:   �� *DataContext.ControlsEnabled+ �� *FindAncestor+
 Window67  5[      �� 5\      -   5        $	��Bin��5\      #��[ 6�   $/ )Resource name: MaterialDesignRaisedButton��6�   $��2q�6  $	��122��6,  {�6+   �� *DataContext.ControlsEnabled+ �� *FindAncestor+
 Window6,  5]      5^      5_      5`      5b      