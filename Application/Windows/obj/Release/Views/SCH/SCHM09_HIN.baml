   M S B A M L   `   `   `  ���� X  TKyoeiSystem.Framework.Windows, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null�[clr-namespace:KyoeiSystem.Framework.Windows.ViewBase;assembly=KyoeiSystem.Framework.Windows&KyoeiSystem.Framework.Windows.ViewBase  �[clr-namespace:KyoeiSystem.Framework.Windows.ViewBase;assembly=KyoeiSystem.Framework.Windows&KyoeiSystem.Framework.Windows.ViewBase  a ]KyoeiSystem.Framework.Windows.Controls, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null�dclr-namespace:KyoeiSystem.Framework.Windows.Controls;assembly=KyoeiSystem.Framework.Windows.Controls&KyoeiSystem.Framework.Windows.Controls # KyoeiSystem.Application.Windows6   0KyoeiSystem.Application.Windows.Views.SCHM09_HIN   bfw[clr-namespace:KyoeiSystem.Framework.Windows.ViewBase;assembly=KyoeiSystem.Framework.Windows  \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     jvm[clr-namespace:KyoeiSystem.Framework.Windows.ViewBase;assembly=KyoeiSystem.Framework.Windows     uctlsdclr-namespace:KyoeiSystem.Framework.Windows.Controls;assembly=KyoeiSystem.Framework.Windows.Controls     -   $��
MainWindow��$	��768��$	��342��  � Left$
  1025��
 � Top$ 0�� � Title$ 品番検索(SCHM09_HIN)�� � 	Resources    DataGridHeaderBackground �� EndPoint �� 
StartPoint � Color � Offset��%A   &	        �� $	 0,1&�$	 0,0&�.��� $ #3b5998��$ 0Q�.��� #��� ��$��5q� � RowDefinitions �� $
��auto ��� $��* �.+�� $��	20,15,5,5q� �� $��30 ��� $��30 ��� $��30 ��� $��30 ��� $��30 ��� $��50 �	 � ColumnDefinitions	 �� $��* ��� $
��auto �.+�� $��0��	 �� $
��auto ��� $��* �.+�<  6KyoeiSystem.Framework.Windows.Controls.UcLabelComboBox  -   $��OrderColumn��
   Label_Context$
 	表示順��$��0��   Label_Width$ 90Q�$	��220��$��1q�$
��Left=�   ComboListingParams$, &マスタ検索,品番検索,表示順��;  5KyoeiSystem.Framework.Windows.Controls.UcLabelTextBox  -     Name$ txtCode��   Label_Context$ 品番��$��1��   Label_Width$ 90Q�   cWidth$	 130Q�$��1q�   ImeType$	 Off=�$
��Left=�  -   $ txtName��$ 品名��$��2��$ 90Q�$	 130Q�$��1q�$ Native=�$
��Left=�?  9KyoeiSystem.Framework.Windows.Controls.UcLabelTwinTextBox  -     Name$ txtBrand��   Label_Context$ ブランド��$��3��   Label_Width$ 90Q�   
Text2Width$	 100Q�$��1q�   DataAccessName$ 	M14_BRAND��   DataAccessMode$ On=�$
��Left=�   Label1Visibility$ 	Collapsed=�   Label2Visibility$ 	Collapsed=�   Text1IMEType$	 Off=�   Text2IsReadOnly .   -   $ 	txtSeries��$ シリーズ��$��4��$ 90Q�$	 100Q�$��1q�$ 
M15_SERIES��$ On=�$
��Left=�$ 	Collapsed=�$ 	Collapsed=�$	 Off=� . � $��5��$��2��	 �� $��90 ��� $��74 ��� $
��auto � �� $��25 ��� $��25 �.+�  -   $ lblItemClass��$ 商品形態分類��$��0��$��0��$��2��$ 90Q�$ 0Q�$��	0,10,0,10q�7  1KyoeiSystem.Framework.Windows.Controls.UcCheckBox  -   $��chkItemClass_1��   cContent$ SET品��$��1��$��0��   	IsChecked$
 TrueX�$��5,0,0,0q�  -	   $��chkItemClass_2��$ 単品・材料��$��2��$��0��$
 TrueX�  -
   $��chkItemClass_3��$ 雑コード��$��1��$��1��$
 TrueX�$��5,0,0,0q�  -   $��chkItemClass_4��$ 	副資材��$��2��$��1��$
 TrueX��� -   $��OkButton��$��4��$��0��$��3��$��3q����	#FF071281 J� RenderTransformOrigin$ 0.5,0.5&�$��5q�$��61��$��49��$��Right=�$��Bottom=� J� OpacityMask# � ������   �� ImageSource$5  /../Images/appbar.checkmark.pencil.top.white.png��! j� Stretch$
! None=�" j� 
AlignmentY$" Bottom=��� -   $��CancelButton��$��4��$��3��$��3��$��3q����	#FF071281$ 0.5,0.5&�$��5q�$��61��$��51��$
��Left=�$	��Top=�# � ������ $+  %../Images/appbar.app.remove.white.png��$
! None=�$" Bottom=�&   System.Windows.Controls.DataGrid  -   $��
SearchGrid��$��1��#   CanUserSortColumns# .  $   SelectionMode$$ Single=�%   CanUserAddRows% .  &   CanUserDeleteRows& .  '   
IsReadOnly' . (   AutoGenerateColumns( . ���� ) �� Path$) SearchResult�* �� Mode$* TwoWay=�+ �� NotifyOnSourceUpdated+ . , �� NotifyOnTargetUpdated, . -   ColumnHeaderStyle- �� . �� 
TargetType=  7System.Windows.Controls.Primitives.DataGridColumnHeader".  .��� / �� Property/ � ��0 �� Value$0 25���� / � ��
0 ������� / � ��$	0 0.5q��� / � ��
0 �   ��� / � ��$0 Center=��� GridHeaderStye��/ � ��#0 [  