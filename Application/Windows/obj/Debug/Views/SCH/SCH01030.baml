   M S B A M L   `   `   `  ����X  TKyoeiSystem.Framework.Windows, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null�[clr-namespace:KyoeiSystem.Framework.Windows.ViewBase;assembly=KyoeiSystem.Framework.Windows&KyoeiSystem.Framework.Windows.ViewBase  5   	   �[clr-namespace:KyoeiSystem.Framework.Windows.ViewBase;assembly=KyoeiSystem.Framework.Windows&KyoeiSystem.Framework.Windows.ViewBase  5   	   a ]KyoeiSystem.Framework.Windows.Controls, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null�dclr-namespace:KyoeiSystem.Framework.Windows.Controls;assembly=KyoeiSystem.Framework.Windows.Controls&KyoeiSystem.Framework.Windows.Controls 5   	   # KyoeiSystem.Application.Windows4   .KyoeiSystem.Application.Windows.Views.SCH01030   5   	   bfw[clr-namespace:KyoeiSystem.Framework.Windows.ViewBase;assembly=KyoeiSystem.Framework.Windows  \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   jvm[clr-namespace:KyoeiSystem.Framework.Windows.ViewBase;assembly=KyoeiSystem.Framework.Windows     5   	   uctlsdclr-namespace:KyoeiSystem.Framework.Windows.Controls;assembly=KyoeiSystem.Framework.Windows.Controls     5   	   -   5        $��	MinWindow��5      $	��768��5      $	��342��6     � 
ResizeMode$  NoResize=�6    � Left$
 1025��5      
 � Top$ 0��6    � Title$ 得意先検索��5       � 	Resources 5         DataGridHeaderBackground �� EndPoint �� 
StartPoint � Color � Offset��%p   &	        �� 5   
   $	 0,1&�6?   $	 0,0&�6N   .��5      � $ #3b5998��6   $ 0Q�6+   5      5      .��5      � -   5        #��� ��5      $��5q�6t   	 � RowDefinitions	 5   
   �� 5      $
��auto �6   �� 5      $��* �6   .+�5   
   � $��	20,15,5,5q�6   	 5      �� 5      $��30 �6    �� 5      $��30 �6    �� 5      $��30 �6    �� 5       $��30 �6    �� 5!      $��30 �6    �� 5"      $
��auto �6    �� 5#      $��* �6    
 � ColumnDefinitions
 5%      �� 5&      $��* �6#   �� 5'      $
��auto �6#   .+�5)      ;  5KyoeiSystem.Framework.Windows.Controls.UcLabelTextBox  -   5          Name$ txtKana��5)   "      Label_Context$ かな読み��63      Label_Width$ 80Q�6�      cWidth$	 128Q�6�      cHorizontalAlignment$
 Left=�6�   $��1q�6�      ImeType$ HankakuKatakana=�6�      Text 6I   �� *かな読み+ �� Mode$ TwoWay=� �� NotifyOnSourceUpdated .  �� NotifyOnTargetUpdated . 6�     5*      -   5        $ txtCD��5*   "   $ 確定コード��61   $ 80Q�5+   "   $��1��63   $	 128Q�6@   $
 Left=�6M   $��1q�6i   $	 Off=�6t    5*   G   �� *確定コード+$ TwoWay=� .  . 5+   t   � 5-      $��2��6   
 5.      �� 5/      $
��auto �6'   �� 50      $��* �6'   .+�52      4  .KyoeiSystem.Framework.Windows.Controls.UcLabel     cContent$ 	表示順��6      cWidth$ 80Q�6.      cHeight$ 26Q�6:      cMargin$ 1q�6G   �� 54      -   5        $��OrderColumn��54      $��1��6.   $��25��6>   $	��100��6J   $
��Left=�6V    �� SelectedIndex$ 0��6q   .@�55      �� $��	コード��6#   �� 56      $��取引先名��6#   �� 57      $��かな読み��6#   58      59      � 5;      $��3��6   
 5<      �� 5=      $
��auto �6'   �� 5>      $��* �6'   5@      �� 5B      -   5        $��SearchButton��5B      $��1��6)   $��3��69   $��67��6J   $��51��6V   $��5q�6a    J� RenderTransformOrigin$ 0.5,0.5&�6l   $��9(�6�   $��3q�6�   ���	#FF071281��5C      �� 5D       �� ImageSource$( "../Images/appbar.magnify.white.png��6!    �� Opacity$ 1Q�6R    j� 
AlignmentY$ Bottom=�6^    j� Stretch$
 None=�6r   5F      �� 5H      -   5        $��CancelButton��5H      $��4��6)   $��3��69   $��5q�6F   $��2��6Q   $
��Left=�6b   $��61��6}   $	��Top=�6�   $��51��6�   $ 0.5,0.5&�6�    J� OpacityMask# � ��6�   $��3q�6d  ���	#FF071281��5I      �� 5J      $+ %../Images/appbar.app.remove.white.png��6!   $
 None=�6V   $ Bottom=�6e   5L      �� 5N      -   5        $��OkButton��5N      $��3��6%   $��3��67   $��5q�6D   $��Right=�6O   $��61��6k   $	��Top=�6w   $��49��6�   $ 0.5,0.5&�6�   # � ��6�   $��3q�5O   Q   ���	#FF071281��5P      �� 5Q      $5 /../Images/appbar.checkmark.pencil.top.white.png��6!   $
 None=�6_   $ Bottom=�6n   5S      5T      &   System.Windows.Controls.DataGrid  5V   
   -	   5        $��
SearchGrid��5V      $��1��6%       CanUserAddRows  .  !   CanUserDeleteRows! .  "   
IsReadOnly" . #   AutoGenerateColumns# .  ��5W      �� $ �� Path$$ SearchResult�$ TwoWay=� .  . %   ColumnHeaderStyle% 5Z      �� 5[      & �� 
TargetType=  7System.Windows.Controls.Primitives.DataGridColumnHeader"&  6   .�5\      �� ' �� Property' � ��( �� Value$( 25��6/   �� 5]      ' � ��
( �����63   �� 5^      ' � ��$	( 0.5q�68   �� 5_      ' � ��
( �   �64   �� 5`      ' � ��$( Center=�6C   �� 5a      GridHeaderStye��' � ��#( [  6K   5b      )   Columns) 5e      4  .System.Windows.Controls.DataGridTemplateColumn  5g      ,  &System.Windows.Controls.DataGridColumn*   Width5  /System.Windows.Controls.DataGridLengthConverter$* 60 6)   +   Header+ 5h      �� 5i      $��	コード��6$   
�������
������ , �� TextAlignment$, Center=�6[   $��Center=�6r   $��Center=�6�   $��80��6�   -   CellTemplate- 5l      �� 5m      .R�5n      �� 
������ $��Center=�6Y   $
��Left=�6t   ��6(   �� *取引先ID+6t   5o      5q        5t      $	* 190 6)   + 5u      �� 5v      $��経費先項目名��6$   
�������
������ $, Center=�6^   $��Center=�6u   $��Center=�6�   $	��250��6�   - 5y      �� 5z      .R�5{      �� 
������ $��Center=�6Y   $
��Left=�6t   ��6(   �� *取引先名１+6t   5|      5~      5�      5�      5�      