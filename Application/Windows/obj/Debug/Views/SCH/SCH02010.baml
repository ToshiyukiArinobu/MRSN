   M S B A M L   `   `   `  ����X  TKyoeiSystem.Framework.Windows, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null�[clr-namespace:KyoeiSystem.Framework.Windows.ViewBase;assembly=KyoeiSystem.Framework.Windows&KyoeiSystem.Framework.Windows.ViewBase  5   	   �[clr-namespace:KyoeiSystem.Framework.Windows.ViewBase;assembly=KyoeiSystem.Framework.Windows&KyoeiSystem.Framework.Windows.ViewBase  5   	   a ]KyoeiSystem.Framework.Windows.Controls, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null�dclr-namespace:KyoeiSystem.Framework.Windows.Controls;assembly=KyoeiSystem.Framework.Windows.Controls&KyoeiSystem.Framework.Windows.Controls 5   	   # KyoeiSystem.Application.Windows4   .KyoeiSystem.Application.Windows.Views.SCH02010   5   	   bfw[clr-namespace:KyoeiSystem.Framework.Windows.ViewBase;assembly=KyoeiSystem.Framework.Windows  \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   jvm[clr-namespace:KyoeiSystem.Framework.Windows.ViewBase;assembly=KyoeiSystem.Framework.Windows     5   	   uctlsdclr-namespace:KyoeiSystem.Framework.Windows.Controls;assembly=KyoeiSystem.Framework.Windows.Controls     5   	   -   5        $��	MinWindow��5      $	��768��5      $	��342��6     � 
ResizeMode$  NoResize=�6    � Left$
 1025��5      
 � Top$ 0��6    � Title$ 得意先(内訳)検索��5       � 	Resources 5         DataGridHeaderBackground �� EndPoint �� 
StartPoint � Color � Offset��%p   &	        �� 5   
   $	 0,1&�6?   $	 0,0&�6N   .��5      � $ #3b5998��6   $ 0Q�6+   5      5      .��5      � -   5        #��� ��5      $��5q�6t   	 � RowDefinitions	 5   
   �� 5      $
��auto �6   �� 5      $��* �6   .+�5   
   � $��	20,15,5,5q�6   	 5      �� 5      $��30 �6    �� 5      $��30 �6    �� 5      $��30 �6    �� 5       $��30 �6    �� 5!      $��30 �6    �� 5"      $
��auto �6    �� 5#      $��* �6    
 � ColumnDefinitions
 5%      �� 5&      $��* �6#   �� 5'      $
��auto �6#   .+�5)      ;  5KyoeiSystem.Framework.Windows.Controls.UcLabelTextBox  -   5          Name$ txtKana��5)   "      Label_Context$ かな読み��63      Label_Width$ 80Q�6�      cWidth$	 128Q�6�      cHorizontalAlignment$
 Left=�6�   $��1q�6�      ImeType$ HankakuKatakana=�6�      Text 6I   �� *かな読み+ �� Mode$ TwoWay=� �� NotifyOnSourceUpdated .  �� NotifyOnTargetUpdated . 6�     5*      -   5        $ txtCD��5*   "   $ 確定ID��61   $ 80Q�5+   "   $��1��63   $	 128Q�6@   $
 Left=�6M   $��1q�6i   $	 Off=�6t    5*   F   �� *確定コード+$ TwoWay=� .  . 5+   t   � 5-      $��2��6   
 5.      �� 5/      $
��auto �6'   �� 50      $��* �6'   .+�52      4  .KyoeiSystem.Framework.Windows.Controls.UcLabel     cContent$ 	表示順��6      cWidth$ 80Q�6.      cHeight$ 26Q�6:      cMargin$ 1q�6G   �� 54      -   5        $��OrderColumn��54      $��1��6.   $��25��6>   $	��100��6J   $
��Left=�6V    �� SelectedIndex$ 0��6q   .@�55      �� $��	コード��6#   �� 56      $��取引先名��6#   �� 57      $��かな読み��6#   58      59      � 5;      $��3��6   
 5<      �� 5=      $
��auto �6'   �� 5>      $��* �6'   5@      �� 5B      -   5        $��CancelButton��5B      $��4��6)   $��3��69   $��5q�6F   $��3��6Q   $
��Left=�6b   $��61��6}   $	��Top=�6�   $��51��6�    J� RenderTransformOrigin$ 0.5,0.5&�6�    J� OpacityMask# � ��6�   $��3q�6d  ���	#FF071281��5C      �� 5D       �� ImageSource$+ %../Images/appbar.app.remove.white.png��6!    j� Stretch$
 None=�6V    j� 
AlignmentY$ Bottom=�6e   5F      �� 5H      -   5        $��OkButton��5H      $��3��6%   $��4��66   $��0��6G   $��5q�6T   $��Right=�6_   $��61��6{   $��Bottom=�6�   $��49��6�   $ 0.5,0.5&�6�   # � ��6�   $��3q�5I   Q   ���	#FF071281��5J      �� 5K      $5 /../Images/appbar.checkmark.pencil.top.white.png��6!   $
 None=�6_   $ Bottom=�6n   5M      5N      &   System.Windows.Controls.DataGrid  5P   
   -   5        $��
SearchGrid��5P      $��1��6%      CanUserAddRows .      CanUserDeleteRows  .  !   
IsReadOnly! . "   AutoGenerateColumns" .  ��5Q      �� # �� Path$# SearchResult�$ TwoWay=� .  . $   ColumnHeaderStyle$ 5T      �� 5U      % �� 
TargetType=  7System.Windows.Controls.Primitives.DataGridColumnHeader"%  6   .�5V      �� & �� Property& � ��' �� Value$' 25��6/   �� 5W      & � ��
' �����63   �� 5X      & � ��$	' 0.5q�68   �� 5Y      & � ��
' �   �64   �� 5Z      & � ��$' Center=�6C   �� 5[      GridHeaderStye��& � ��#' [  6K   5\      (   Columns( 5_      4  .System.Windows.Controls.DataGridTemplateColumn  5a      ,  &System.Windows.Controls.DataGridColumn)   Width5  /System.Windows.Controls.DataGridLengthConverter$) 60 6)   *   Header* 5b      �� 5c      $��	コード��6$   
�������
������ + �� TextAlignment$+ Center=�6[   $��Center=�6r   $��Center=�6�   $��80��6�   ,   CellTemplate, 5f      �� 5g      .R�5h      �� 
������ $��Center=�6Y   $
��Left=�6t   ��6(   �� *取引先ID+6t   5i      5k        5n      $	) 190 6)   * 5o      �� 5p      $��取引先名��6$   
�������
������ $+ Center=�6\   $��Center=�6s   $��Center=�6�   $	��250��6�   , 5s      �� 5t      .R�5u      �� 
������ $��Center=�6Y   $
��Left=�6t   ��6(   �� *取引先名１+6t   5v      5x        5{      $) 80 6)   * 5|      �� 5}      $��かな��6$   
�������
������ $+ Center=�6Z   $��Center=�6q   $��Center=�6�   $	��250��6�   , 5�      �� 5�      .R�5�      �� 
������ $��Center=�6X   $
��Left=�6s   ��6(   �� *かな読み+6s   5�      5�      5�      5�      5�      