   M S B A M L   `   `   `  ����G  CAssigmentPRN, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:AsgPRNAsgPRN  5   	   $    AsgPRN.ListAirportDeleteWindow   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   &localclr-namespace:AsgPRN     5   	   _ [MaterialDesignThemes.Wpf, Version=4.3.0.3, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5   	     � Title$  ListAirportDeleteWindow��5	   	   $	��580��6)   $	��910��66      MaterialDesignBody#���   5
   	   $��Regular"�5   	   $��13(�5   	   &   System.Windows.Media.TextOptions   TextFormattingMode$ Ideal=�5   	      TextRenderingMode$
 Auto=�5   	     MaterialDesignPaper#���  5   	     MaterialDesignFont#���  5   	   .��5      � .+�5   
   �� $��Center=�6   $��5q�60   .+�5      ��  �� Orientation$ 
Horizontal=�6   ���#D23F31.+�5      ]� $
��True��6   .+�5      '  !MaterialDesignThemes.Wpf.PackIcon     Kind$ Airplane=�6.   $
��Left=�6>   $��Center=�6T   �� 5      $��Manage airport��6   $��30(�65   $
��Bold)�6C   $��Center=�6U   $
��Left=�6q   $��	0,0,250,0q�5      �� 5      -   5        $��	txtSearch��5      $	��100��6/   $��5q�6;   $��Center=�6F   �� 5      -   5        $��comboBoxSearch��5       �� SelectedIndex$ 1��65   
�������$��5q�6Z   $
��10,0q�6e    " MaterialDesignOutlinedComboBox#��[ 6t   .@�5       ��  �� 
IsSelected$
 True��6'   
 � Tag$
 Name��69   .��6D   Name6J   �� 5!      $ Country��6'   .��65   	Country6>   �� 5"      $
 City��6'   .��62   City68   �� 5#      $
 Code��6'   .��62   Code68   �� 5$      $ State��6'   .��63   State6:   5%      �� 5&      -   5        $��Search airport��5&      $��5q�66   $
��10,0q�6A   
��� d �
�������6z   5)      5*      &   System.Windows.Controls.DataGrid  5+      -   5          Name$ AirportDataGrid��5+      	   AutoGenerateColumns$	 False��60   $��5q�6L   $	��328��6�   $	��870��6�   
   CanUserAddRows$
 False��5,         Columns 5-      0  *System.Windows.Controls.DataGridTextColumn  5.      ,  &System.Windows.Controls.DataGridColumn   Header$ ID��6)      Width5  /System.Windows.Controls.DataGridLengthConverter$ 50 6L   1  +System.Windows.Controls.DataGridBoundColumn   Binding 65   �� *Id+6L     5/      $
 Code��6)   $	 100 6P    67   �� *Code+6P     50      $
 Name��6)   $	 150 6P    67   �� *Name+6P     51      $ Country��6)   $	 100 6V    6:   �� *	Country+6V     52      $ State��6)   $	 100 6R    68   �� *State+6R     53      $
 City��6)   $	 100 6P    67   �� *City+6P   55      �� 5:      $ 
Horizontal=�6   $��Center=�62   $��5q�6M   .+�5;      �� -   5         ��Name$ btnPreviousPage��5;      $��<��62   $
��10,0q�6B   
����ح�
�������$��30��6�   $��15(�6�   $��20��6�   �� 5<      -   5        $ btnNextPage��5<      $��>��6.   $��
10,0,450,0q�6=   $
��10,0q�6Q   
����ح�
�������$��30��5=   C   $��15(�6N   $��20��6\   �� 5>      -   5        $ btnReturnPage��5>      $��Return page��60   $��5q�6F   $
��10,0q�6Q   
��� � �
�������6�   �� 5?      -   5        $ 
btnRestore��5?      $��Restore airport��6-   $��5q�6G   $
��10,0q�6R   
�������
�������$��Right=�6�   6�   5B      5C      5D      5E      