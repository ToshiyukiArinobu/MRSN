   M S B A M L   `   `   `  ����X  TKyoeiSystem.Framework.Windows, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null�[clr-namespace:KyoeiSystem.Framework.Windows.ViewBase;assembly=KyoeiSystem.Framework.Windows&KyoeiSystem.Framework.Windows.ViewBase  5   	   �[clr-namespace:KyoeiSystem.Framework.Windows.ViewBase;assembly=KyoeiSystem.Framework.Windows&KyoeiSystem.Framework.Windows.ViewBase  5   	   a ]KyoeiSystem.Framework.Windows.Controls, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null�dclr-namespace:KyoeiSystem.Framework.Windows.Controls;assembly=KyoeiSystem.Framework.Windows.Controls&KyoeiSystem.Framework.Windows.Controls 5   	   # KyoeiSystem.Application.Windows5   /KyoeiSystem.Application.Windows.Views.TKS01010S   5   	   bfw[clr-namespace:KyoeiSystem.Framework.Windows.ViewBase;assembly=KyoeiSystem.Framework.Windows  \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   jvm[clr-namespace:KyoeiSystem.Framework.Windows.ViewBase;assembly=KyoeiSystem.Framework.Windows     5   	   uctlsdclr-namespace:KyoeiSystem.Framework.Windows.Controls;assembly=KyoeiSystem.Framework.Windows.Controls     5   	   -   5        $��	MinWindow��5      $	��768��5      $	��342��6     � 
ResizeMode$  NoResize=�6    � Left$
 1025��5      
 � Top$ 0��6    � Title$ 取引先検索��5       � 	Resources 5         DataGridHeaderBackground �� EndPoint �� 
StartPoint � Color � Offset��%�   &	        �� 5   
   $	 0,1&�6?   $	 0,0&�6N   .��5      � $ #5383c3��6   $ 0Q�6+   � 5      $ #4c6cd3��6   $ .5Q�6+   � 5      $ #3e62ad��6   $ .5Q�6+   � 5      $ #507ea4��6   $ 1Q�6+   5      5      .��5      � -   5        #��� ��5      $��5q�6t   	 � RowDefinitions	 5   
   �� 5      $
��auto �6   �� 5      $��* �6   .+�5   
   � $��	20,15,5,5q�6   	 5      �� 5       $��30 �6    �� 5!      $��30 �6    �� 5"      $��30 �6    �� 5#      $��30 �6    �� 5$      $��30 �6    �� 5%      $
��auto �6    �� 5&      $��* �6    
 � ColumnDefinitions
 5(      �� 5)      $��* �6#   �� 5*      $
��auto �6#   .+�5,      ;  5KyoeiSystem.Framework.Windows.Controls.UcLabelTextBox  -   5          Name$ txtKana��5,   "      Label_Context$ かな読み��63      Label_Width$ 80Q�6H      cWidth$	 128Q�6Y      cHorizontalAlignment$
 Left=�6f   $��1q�6�      ImeType$ Katakana=�6�      Text 5-   "   �� *かな読み+ �� Mode$ TwoWay=� �� NotifyOnSourceUpdated .  �� NotifyOnTargetUpdated . 5.   "     50      -   5        $ txtCD��50   "   $ 確定コード��61   $ 80Q�51   "   $��1��63   $	 128Q�6@   $
 Left=�6M   $��1q�6i   $	 Off=�6t    52   "   �� *確定コード+$ TwoWay=� .  . 53   "   � 56      $��2��6   
 57      �� 58      $
��auto �6'   �� 59      $��* �6'   .+�5;      4  .KyoeiSystem.Framework.Windows.Controls.UcLabel     cContent$ 表示形式��6      cWidth$ 80Q�6/      cHeight$ 26Q�6;      cMargin$ 1q�6H   �� 5=      -   5        $��OrderColumn��5=      $��1��6.   $��25��6>   $	��100��6J   $
��Left=�6V    �� SelectedIndex 5>      �� *表示区分+$ TwoWay=� .  . .@�5A      �� $��	全取引��6#   �� 5B      $��	得意先��6#   �� 5C      $��	支払先��6#   �� 5D      $��	経費先��6#   5F      5G      � 5I      $��3��6   
 5J      �� 5K      $
��auto �6'   �� 5L      $��* �6'   5N      �� 5Q      -   5        $��SearchButton��5Q      $��1��6)   $��3��69   $��67��6J   $��51��6V   $��5q�6a    J� RenderTransformOrigin$ 0.5,0.5&�6l   $��9(�6�   $��3q�6�   ���	#FF071281��5R      �� 5S       �� ImageSource$( "../Images/appbar.magnify.white.png��6!    �� Opacity$ 1Q�6R    j� 
AlignmentY$ Bottom=�6^    j� Stretch$
 None=�6r   5U      �� 5W      -   5        $��CancelButton��5W      $��4��6)   $��3��69   $��5q�6F   $��2��6Q   $
��Left=�6b   $��61��6}   $	��Top=�6�   $��51��6�   $ 0.5,0.5&�6�    J� OpacityMask# � ��6�   $��3q�6d  ���	#FF071281��5X      �� 5Y      $+ %../Images/appbar.app.remove.white.png��6!   $
 None=�6V   $ Bottom=�6e   5[      �� 5]      -   5        $��OkButton��5]      $��3��6%   $��3��67   $��5q�6D   $��Right=�6O   $��61��6k   $	��Top=�6w   $��49��6�   $ 0.5,0.5&�6�   # � ��6�   $��3q�5^   Q   ���	#FF071281��5_      �� 5`      $5 /../Images/appbar.checkmark.pencil.top.white.png��6!   $
 None=�6_   $ Bottom=�6n   5b      5c      &   System.Windows.Controls.DataGrid  5e   
   -	   5        $��
SearchGrid��5e      $��1��6%       SelectionMode$  Extended=�62   !   CanUserAddRows! .  "   CanUserDeleteRows" .  #   
IsReadOnly# . $   AutoGenerateColumns$ .  ��5f      �� % �� Path$% SearchResult�$ TwoWay=� .  . &   ColumnHeaderStyle& 5i      �� 5j      ' �� 
TargetType=  7System.Windows.Controls.Primitives.DataGridColumnHeader"'  6   .�5k      �� ( �� Property( � ��) �� Value$) 25��6/   �� 5l      ( � ��
) �����63   �� 5m      ( � ��$	) 0.5q�68   �� 5n      ( � ��
) �   �64   �� 5o      ( � ��$) Center=�6C   �� 5p      GridHeaderStye��( � ��#) [  6K   5q      *   Columns* 5t      4  .System.Windows.Controls.DataGridTemplateColumn  5v      ,  &System.Windows.Controls.DataGridColumn+   Width5  /System.Windows.Controls.DataGridLengthConverter$+ 60 6)   ,   Header, 5w      �� 5x      $��	コード��6$   
�������
������ - �� TextAlignment$- Center=�6[   $��Center=�6r   $��Center=�6�   $��80��6�   .   CellTemplate. 5{      �� 5|      .R�5}      �� 
������ $��Center=�6Y   $
��Left=�6t   ��6(   �� *取引先ID+6t   5~      5�        5�      $	+ 190 6)   , 5�      �� 5�      $��取引先名��6$   
�������
������ $- Center=�6\   $��Center=�6s   $��Center=�6�   $	��250��6�   . 5�      �� 5�      .R�5�      �� 
������ $��Center=�6Y   $
��Left=�6t   ��6(   �� *取引先名１+6t   5�      5�        5�      $+ 80 6)   , 5�      �� 5�      $��かな��6$   
�������
������ $- Center=�6Z   $��Center=�6q   $��Center=�6�   $	��250��6�   . 5�      �� 5�      .R�5�      �� 
������ $��Center=�6X   $
��Left=�6s   ��6(   �� *かな読み+6s   5�      5�      5�        5�   
   $ 	表示順��6   $ 80Q�6&   $ 26Q�62   $ 1q�6?   $��15,104,224,47q�6K   �� 5�   
   -
   5        / ��Name$/ OrderColumn_Copy��5�      $��25��6-   $	��100��69   $
��Left=�6E   $ 0��6`   $��102,107,0,49q�6�   .@�5�      �� $��	コード��6   �� 5�      $��取引先名��6   �� 5�      $��かな読み��6   5�      5�      5�      