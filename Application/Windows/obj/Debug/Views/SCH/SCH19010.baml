   M S B A M L   `   `   `  ����X  TKyoeiSystem.Framework.Windows, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null�[clr-namespace:KyoeiSystem.Framework.Windows.ViewBase;assembly=KyoeiSystem.Framework.Windows&KyoeiSystem.Framework.Windows.ViewBase  5   	   �[clr-namespace:KyoeiSystem.Framework.Windows.ViewBase;assembly=KyoeiSystem.Framework.Windows&KyoeiSystem.Framework.Windows.ViewBase  5   	   a ]KyoeiSystem.Framework.Windows.Controls, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null�dclr-namespace:KyoeiSystem.Framework.Windows.Controls;assembly=KyoeiSystem.Framework.Windows.Controls&KyoeiSystem.Framework.Windows.Controls 5   	   # KyoeiSystem.Application.Windows4   .KyoeiSystem.Application.Windows.Views.SCH19010   5   	   bfw[clr-namespace:KyoeiSystem.Framework.Windows.ViewBase;assembly=KyoeiSystem.Framework.Windows  \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   jvm[clr-namespace:KyoeiSystem.Framework.Windows.ViewBase;assembly=KyoeiSystem.Framework.Windows     5   	   uctlsdclr-namespace:KyoeiSystem.Framework.Windows.Controls;assembly=KyoeiSystem.Framework.Windows.Controls     5   	   -   5        $��	MinWindow��5      $	��768��5      $	��600��6     � 
ResizeMode$  NoResize=�6    � Left$
 1025��5      
 � Top$ 0��6    � Title$ 取引先検索��5       � 	Resources 5         DataGridHeaderBackground �� EndPoint �� 
StartPoint � Color � Offset��%p   &	        �� 5   
   $	 0,1&�6?   $	 0,0&�6N   .��5      � $ #3b5998��6   $ 0Q�6+   5      5      .��5      � -   5        #��� ��5      $��5q�6t   	 � RowDefinitions	 5   
   �� 5      $
��auto �6   �� 5      $��* �6   .+�5   
   � $��	20,15,5,5q�6   	 5      �� 5      $��30 �6    �� 5      $��30 �6    �� 5      $��30 �6    �� 5       $��30 �6    �� 5!      $��30 �6    �� 5"      $
��auto �6    �� 5#      $��* �6    
 � ColumnDefinitions
 5%      �� 5&      $��* �6#   �� 5'      $
��auto �6#   .+�5-      � $��2��6   
 5.      �� 5/      $
��auto �6'   �� 50      $��* �6'   5=      � 5?      $��3��6   5D      �� 5G      -   5        $��CancelButton��5G      $��4��6)   $��3��69   $��5q�6F   $��3��6Q   $
��Left=�6b   $��61��6}   $	��Top=�6�   $��51��6�    J� RenderTransformOrigin$ 0.5,0.5&�6�    J� OpacityMask# � ��6�   $��3q�6d  ���	#FF071281��5H      �� 5I       �� ImageSource$+ %../Images/appbar.app.remove.white.png��6!    j� Stretch$
 None=�6V    j� 
AlignmentY$ Bottom=�6e   5K      �� 5M      -   5        $��OkButton��5M      $��3��6%   $��4��66   $��0��6G   $��5q�6T   $��Right=�6_   $��61��6{   $��Bottom=�6�   $��49��6�   $ 0.5,0.5&�6�   # � ��6�   $��3q�5N   Q   ���	#FF071281��5O      �� 5P      $5 /../Images/appbar.checkmark.pencil.top.white.png��6!   $
 None=�6_   $ Bottom=�6n   5R      5S      &   System.Windows.Controls.DataGrid  5U   
   -   5        $��
SearchGrid��5U      $��1��6%      CanUserSortColumns .    SelectionMode$ Single=�6L      CanUserAddRows .     CanUserDeleteRows .     
IsReadOnly .    AutoGenerateColumns . ��5V      ��  �� Path$ SearchResult� �� Mode$ TwoWay=� �� NotifyOnSourceUpdated .  �� NotifyOnTargetUpdated .    ColumnHeaderStyle 5Y      �� 5Z       �� 
TargetType=  7System.Windows.Controls.Primitives.DataGridColumnHeader"  6   .�5[      ��  �� Property � �� �� Value$ 25��6/   �� 5\       � ��
 �����63   �� 5]       � ��$	 0.5q�68   �� 5^       � ��
 �   �64   �� 5_       � ��$ Center=�6C   �� 5`      GridHeaderStye�� � ��# [  6K   5a      5e      5l      5m      