   M S B A M L   `   `   `  ���� X  TKyoeiSystem.Framework.Windows, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null�[clr-namespace:KyoeiSystem.Framework.Windows.ViewBase;assembly=KyoeiSystem.Framework.Windows&KyoeiSystem.Framework.Windows.ViewBase  �[clr-namespace:KyoeiSystem.Framework.Windows.ViewBase;assembly=KyoeiSystem.Framework.Windows&KyoeiSystem.Framework.Windows.ViewBase  a ]KyoeiSystem.Framework.Windows.Controls, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null�dclr-namespace:KyoeiSystem.Framework.Windows.Controls;assembly=KyoeiSystem.Framework.Windows.Controls&KyoeiSystem.Framework.Windows.Controls # KyoeiSystem.Application.Windows7   1KyoeiSystem.Application.Windows.Views.SCHM22_SOUK   bfw[clr-namespace:KyoeiSystem.Framework.Windows.ViewBase;assembly=KyoeiSystem.Framework.Windows  \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     jvm[clr-namespace:KyoeiSystem.Framework.Windows.ViewBase;assembly=KyoeiSystem.Framework.Windows     uctlsdclr-namespace:KyoeiSystem.Framework.Windows.Controls;assembly=KyoeiSystem.Framework.Windows.Controls     -   $��
MainWindow��$	��768��$	��362��  � Left$
  1025��
 � Top$ 0�� � Title$( "倉庫マスタ検索(SCHM22_SOUK)�� � 	Resources    DataGridHeaderBackground �� EndPoint �� 
StartPoint � Color � Offset��%A   &	        �� $	 0,1&�$	 0,0&�.��� $ #3b5998��$ 0Q�.��� #��� ��$��5q� � RowDefinitions �� $
��auto ��� $��* �.+�� $��	20,15,5,5q� �� $��30 ��� $��30 ��� $��30 ��� $��30 ��� $��30 ��� $��30 ��� $
��auto ��� $��* �	 � ColumnDefinitions	 �� $��* ��� $
��auto �.+�;  5KyoeiSystem.Framework.Windows.Controls.UcLabelTextBox  -   
  Name$
 txtName��$��0��   ImeType$ Native=�$��1q�   Label_Context$ 	倉庫名��   Label_Width$ 80Q�   cWidth$	 128Q�   cHorizontalAlignment$
 Left=�    cVerticalContentAlignment$ Center=�   Text �� *	倉庫名+ �� Mode$ TwoWay=� �� NotifyOnSourceUpdated .  �� NotifyOnTargetUpdated .   -   $
 txtKana��$��1��$ HankakuKatakana=�$��1q�$ かな読み��$ 80Q�$	 128Q�$
 Left=�$ Center=� �� *かな読み+$ TwoWay=� .  .   -   $
 txtCD��$��2��$ Native=�$��1q�$ 確定ID��$ 80Q�$	 128Q�$
 Left=�$ Center=� �� *確定コード+$ TwoWay=� .  . <  6KyoeiSystem.Framework.Windows.Controls.UcLabelComboBox  -   $��OrderColumn��$��3��$	��208��$
��Left=�$��1q�   Label_Context$ 	表示順��   Label_Width$ 80Q�   ComboListingParams$# 共通,照会画面,表示順��$	��Top=�   SelectedIndex �� *	表示順+$ TwoWay=� .  .  �� UpdateSourceTrigger$ PropertyChanged=��� -   $��CancelButton��$��3��$��4��$��3��$��51��$��61��$
��Left=�$	��Top=�$��5q� J� RenderTransformOrigin$ 0.5,0.5&�$��3q����	#FF071281 J� OpacityMask# � ������  �� ImageSource$+ %../Images/appbar.app.remove.white.png�� j� Stretch$
 None=� j� 
AlignmentY$ Bottom=��� -   $��OkButton��$��0��$��4��$��3��$��61��$��49��$��Right=�$��Bottom=�$��5q�$ 0.5,0.5&�$��3q����	#FF071281# � ������ $5 /../Images/appbar.checkmark.pencil.top.white.png��$
 None=�$ Bottom=�&   System.Windows.Controls.DataGrid  -   $��
SearchGrid��$��1��   SelectionMode$ Single=�    AutoGenerateColumns  . !   
IsReadOnly! . "   CanUserSortColumns" .  #   CanUserAddRows# .  $   CanUserDeleteRows$ .  ���� % �� Path$% SearchResult�$ TwoWay=� .  . &   ColumnHeaderStyle& �� ' �� 
TargetType=  7System.Windows.Controls.Primitives.DataGridColumnHeader"'  .��� ( �� Property( � ��) �� Value$) 25���� ( � ��
) ������� ( � ��$	) 0.5q��� ( � ��
) �   ��� ( � ��$) Center=��� GridHeaderStye��( � ��#) [  