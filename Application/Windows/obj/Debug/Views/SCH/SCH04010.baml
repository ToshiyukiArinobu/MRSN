   M S B A M L   `   `   `  ����X  TKyoeiSystem.Framework.Windows, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null�[clr-namespace:KyoeiSystem.Framework.Windows.ViewBase;assembly=KyoeiSystem.Framework.Windows&KyoeiSystem.Framework.Windows.ViewBase  5   	   �[clr-namespace:KyoeiSystem.Framework.Windows.ViewBase;assembly=KyoeiSystem.Framework.Windows&KyoeiSystem.Framework.Windows.ViewBase  5   	   a ]KyoeiSystem.Framework.Windows.Controls, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null�dclr-namespace:KyoeiSystem.Framework.Windows.Controls;assembly=KyoeiSystem.Framework.Windows.Controls&KyoeiSystem.Framework.Windows.Controls 5   	   # KyoeiSystem.Application.Windows4   .KyoeiSystem.Application.Windows.Views.SCH04010   5   	   bfw[clr-namespace:KyoeiSystem.Framework.Windows.ViewBase;assembly=KyoeiSystem.Framework.Windows  \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   jvm[clr-namespace:KyoeiSystem.Framework.Windows.ViewBase;assembly=KyoeiSystem.Framework.Windows     5   	   uctlsdclr-namespace:KyoeiSystem.Framework.Windows.Controls;assembly=KyoeiSystem.Framework.Windows.Controls     5   	   -   5        $��	MinWindow��5      $	��768��5      $	��342��6     � 
ResizeMode$  NoResize=�6    � Left$
 1025��5      
 � Top$ 0��6    � 	Resources 5         DataGridHeaderBackground �� EndPoint �� 
StartPoint � Color � Offset��%p   &	        �� 5      $	 0,1&�6;   $	 0,0&�6J   .��5      � $ #3b5998��6   $ 0Q�6+   5      5      .��5      � #��� ��6	   $��5q�6M    � RowDefinitions 5      �� 5      $
��auto �6   �� 5      $��* �6   .+�5      � $��	20,15,5,5q�6    5      �� 5   
   $��30 �6   �� 5   
   $��30 �6   �� 5    
   $��30 �6   �� 5!   
   $��30 �6   �� 5"   
   $��30 �6   �� 5#   
   $
��auto �6   �� 5$   
   $��* �6   	 � ColumnDefinitions	 5&      �� 5'   
   $��* �6   �� 5(   
   $
��auto �6   .+�5*      ;  5KyoeiSystem.Framework.Windows.Controls.UcLabelTextBox  -   5        
  Name$
 
ShimeiKana��5*         Label_Context$ 氏名読み��60      cWidth$	 128Q�6E      cHorizontalAlignment$
 Left=�6S   $��1q�6o      ImeType$ Katakana=�6z      Text 5+      �� *氏名読み+ �� Mode$ TwoWay=� �� NotifyOnTargetUpdated .  �� NotifyOnSourceUpdated .  �� UpdateSourceTrigger$ PropertyChanged=�5,        5-      -   5        $
 	KanaKanji��5-      $ 氏名漢字��6/   $��1��6D   $��1q�6Q   $ Native=�6\    5.      �� *氏名漢字+$ TwoWay=� .  . $ PropertyChanged=�5/      � 51      $��2��6   	 52   
   �� 53      $
��auto �6   �� 54      $��* �6   .+�57   
   <  6KyoeiSystem.Framework.Windows.Controls.UcLabelComboBox  -   5        $��cmb表示順��57      $��1��6-      Label_Context$ 	表示順��58         Label_Width$ 58Q�63   $	��180��6D   $
��Left=�6P      ComboListingParams$# 共通,検索画面,表示順��59      $��30��5;   @   $	��Top=�6L      SelectedIndex 5:      �� *	表示順+$ TwoWay=� .  . $ PropertyChanged=�5=      5>   	   � 5@      $��3��6   	 5A   
   �� 5B      $
��auto �6   �� 5C      $��* �6   .+�5E   
     -   5        $��cmb50音��5E      $	��120��6-   $
��Left=�69   $��1��6T   $ 50音順��5F      $ 58Q�64   $" 共通,検索画面,50音順��5G       5H      �� *音順選択位置+$ TwoWay=� .  . $ PropertyChanged=�5J      5K   	   �� 5N      -   5        $��CancelButton��5N      $��4��6#   $��3��63   $��5q�6@   $��2��6K   $
��Left=�6\   $��61��6w   $	��Top=�6�   $��51��6�    J� RenderTransformOrigin$ 0.5,0.5&�6�    J� OpacityMask# � ��6�   $��3q�6^  ���	#FF071281��5O   
   �� 5P       �� ImageSource$+ %../Images/appbar.app.remove.white.png��6    j� Stretch$
 None=�6L    j� 
AlignmentY$ Bottom=�6[   5R   	   �� 5T      -   5        $��OkButton��5T      $��3��6   $��3��61   $��5q�6>   $��Right=�6I   $��61��6e   $	��Top=�6q   $��49��6�   $ 0.5,0.5&�6�   # � ��6�   $��3q�6D  ���	#FF071281��5U   
   �� 5V      $5 /../Images/appbar.checkmark.pencil.top.white.png��6   $
 None=�6U   $ Bottom=�6d   5X   	   5Y      &   System.Windows.Controls.DataGrid  5[   
   -   5        $��UcGrid��5[      $��2��6!      SelectionMode$ Single=�6.      CanUserAddRows .     CanUserDeleteRows .      
IsReadOnly  . !   AutoGenerateColumns! .  ��5\      �� " �� Path$" SearchResult�$ TwoWay=� .  . # �� SelectedIndex# 5^      �� *データ選択位置+$ TwoWay=� .  . $   ColumnHeaderStyle$ 5`      �� 5a   
   % �� 
TargetType=  7System.Windows.Controls.Primitives.DataGridColumnHeader"%  6   .�5b      �� & �� Property& � ��' �� Value$' 25��6%   �� 5c      & � ��
' �����6)   �� 5d      & � ��$	' 0.5q�6.   �� 5e      & � ��
' �   �6*   �� 5f      & � ��$' Center=�69   �� 5g      GridHeaderStye��& � ��#' [  6A   5h      (   Columns( 5k      4  .System.Windows.Controls.DataGridTemplateColumn  5m   
   ,  &System.Windows.Controls.DataGridColumn)   Width5  /System.Windows.Controls.DataGridLengthConverter$) 60 6!   *   Header* 5n      �� 5o      $��乗務員ID��6   
�������
������ + �� TextAlignment$+ Center=�6Q   $��Center=�6h   $��Center=�6�   $��80��6�   ,   CellTemplate, 5r      �� 5s      .R�5t      �� 
������ $��Center=�6K   $
��Left=�6f   ��6   �� *乗務員ID+6f   5u      5w        5z   
   $	) 190 6!   * 5{      �� 5|      $��乗務員名��6   
�������
������ $+ Center=�6P   $��Center=�6g   $��Center=�6�   $	��250��6�   , 5      �� 5�      .R�5�      �� 
������ $��Center=�6J   $
��Left=�6e   ��6   �� *乗務員名+6e   5�      5�        5�   
   $) 80 6!   * 5�      �� 5�      $��かな��6   
�������
������ $+ Center=�6N   $��Center=�6e   $��Center=�6�   $	��250��6�   , 5�      �� 5�      .R�5�      �� 
������ $��Center=�6J   $
��Left=�6e   ��6   �� *かな読み+6e   5�      5�      5�      5�      5�      