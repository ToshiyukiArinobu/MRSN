   M S B A M L   `   `   `  ���� X  TKyoeiSystem.Framework.Windows, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null�[clr-namespace:KyoeiSystem.Framework.Windows.ViewBase;assembly=KyoeiSystem.Framework.Windows&KyoeiSystem.Framework.Windows.ViewBase  �[clr-namespace:KyoeiSystem.Framework.Windows.ViewBase;assembly=KyoeiSystem.Framework.Windows&KyoeiSystem.Framework.Windows.ViewBase  a ]KyoeiSystem.Framework.Windows.Controls, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null�dclr-namespace:KyoeiSystem.Framework.Windows.Controls;assembly=KyoeiSystem.Framework.Windows.Controls&KyoeiSystem.Framework.Windows.Controls # KyoeiSystem.Application.Windows7   1KyoeiSystem.Application.Windows.Views.SCHS03_STOK   bfw[clr-namespace:KyoeiSystem.Framework.Windows.ViewBase;assembly=KyoeiSystem.Framework.Windows  \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     jvm[clr-namespace:KyoeiSystem.Framework.Windows.ViewBase;assembly=KyoeiSystem.Framework.Windows     uctlsdclr-namespace:KyoeiSystem.Framework.Windows.Controls;assembly=KyoeiSystem.Framework.Windows.Controls     -   $��
MainWindow��$	��768��$	��580��  � Left$
  1025��
 � Top$ 0�� � Title$ 在庫参照(SCHS03_STOK)�� � 	Resources    DataGridHeaderBackground �� EndPoint �� 
StartPoint � Color � Offset��%A   &	        �� $	 0,1&�$	 0,0&�.��� $ #3b5998��$ 0Q�.��� #��� ��$��5q� � RowDefinitions �� $
��auto ��� $��* �.+�� $��	15,15,5,5q� �� $��30 ��� $��30 ��� $��30 ��� $��30 ��� $��30 ��� $��30 ��� $
��auto ��� $��* �	 � ColumnDefinitions	 �� $��* ��� $
��auto �.+�;  5KyoeiSystem.Framework.Windows.Controls.UcLabelTextBox  -   
  Name$
 txtProductCode��$��0��$��1q�   Label_Context$ 自社品番��   Label_Width$ 80Q�   ImeType$	 Off=�   ValidationType$ Integer=�   cWidth$	 140Q�   
cMaxLength$ 11��   cHorizontalAlignment$
 Left=�    cVerticalContentAlignment$ Center=�  -   $
 txtProductName��$��1��$��1q�$ 自社品名��$ 80Q�$ Native=�$	 350Q�$	 100��$
 Left=�$ Center=�  -   $
 txtColorName��$��2��$��1q�$ 	自社色��$ 80Q�$ Native=�$	 350Q�$	 100��$
 Left=�$ Center=�  -   $
 txtBrandName��$��3��$��1q�$ ブランド名��$ 80Q�$ Native=�$	 350Q�$	 100��$
 Left=�$ Center=�  -   $
 txtSeriesName��$��4��$��1q�$ シリーズ名��$ 80Q�$ Native=�$	 350Q�$	 100��$
 Left=�$ Center=�  -   $
 	txtHingun��$��5��$��1q�$ 	品群名��$ 80Q�$ Native=�$	 350Q�$	 100��$
 Left=�$ Center=��� -   $��CancelButton��$��3��$��4��$��3��$��61��$��51��$
��Left=�$	��Top=�$��5q� J� RenderTransformOrigin$ 0.5,0.5&�$��3q����	#FF071281 J� OpacityMask# � ������  �� ImageSource$+ %../Images/appbar.app.remove.white.png�� j� Stretch$
 None=� j� 
AlignmentY$ Bottom=�&   System.Windows.Controls.DataGrid  -	   $��
SearchGrid��$��1��   SelectionMode$ Single=�   
IsReadOnly .    CanUserAddRows .     CanUserDeleteRows .     AutoGenerateColumns .     CanUserSortColumns .  ����  �� Path$ SearchResult� �� Mode$ TwoWay=�  �� NotifyOnSourceUpdated  . ! �� NotifyOnTargetUpdated! . "   ColumnHeaderStyle" �� # �� 
TargetType=  7System.Windows.Controls.Primitives.DataGridColumnHeader"#  .��� $ �� Property$ � ��% �� Value$% 25���� $ � ��
% ������� $ � ��$	% 0.5q��� $ � ��
% � ����� $ � ��$% Center=��� GridHeaderStye��$ � ��#% [  &   Columns& 0  *System.Windows.Controls.DataGridTextColumn  ,  &System.Windows.Controls.DataGridColumn'   Header$' 品番��1  +System.Windows.Controls.DataGridBoundColumn(   Binding( �� *品番コード+  $' 自社品番��( �� *自社品番+  $' 自社品名��( �� *自社品名+  $' 自社色名��( �� *自社色名+  $' 賞味期限��( �� *賞味期限+) �� StringFormat$) {0:yyyy/MM/dd}��*   ElementStyle* �� "# ��.��� $ �@��TextAlignment$% Center=�  $' 	在庫数��( �� *	在庫数+$) {0:#,0}��* �� "# ��.��� $ �@��TextAlignment$% Right=�  $' ブランド名��( �� *ブランド名+  $' シリーズ名��( �� *シリーズ名+  $' 	品群名��( �� *	品群名+