   M S B A M L   `   `   `  ���� X  TKyoeiSystem.Framework.Windows, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null�[clr-namespace:KyoeiSystem.Framework.Windows.ViewBase;assembly=KyoeiSystem.Framework.Windows&KyoeiSystem.Framework.Windows.ViewBase  �[clr-namespace:KyoeiSystem.Framework.Windows.ViewBase;assembly=KyoeiSystem.Framework.Windows&KyoeiSystem.Framework.Windows.ViewBase  a ]KyoeiSystem.Framework.Windows.Controls, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null�dclr-namespace:KyoeiSystem.Framework.Windows.Controls;assembly=KyoeiSystem.Framework.Windows.Controls&KyoeiSystem.Framework.Windows.Controls # KyoeiSystem.Application.Windows9   3KyoeiSystem.Application.Windows.Views.SCHM10_TOKHIN   bfw[clr-namespace:KyoeiSystem.Framework.Windows.ViewBase;assembly=KyoeiSystem.Framework.Windows  \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     jvm[clr-namespace:KyoeiSystem.Framework.Windows.ViewBase;assembly=KyoeiSystem.Framework.Windows     uctlsdclr-namespace:KyoeiSystem.Framework.Windows.Controls;assembly=KyoeiSystem.Framework.Windows.Controls     -   $��
MainWindow��$	��768��$	��420��  � Left$
  1025��
 � Top$ 0�� � Title$* $得意先品番検索(SCHM10_TOKHIN)�� � 	Resources    DataGridHeaderBackground �� EndPoint �� 
StartPoint � Color � Offset��%A   &	        �� $	 0,1&�$	 0,0&�.��� $ #3b5998��$ 0Q�.��� #��� ��$��5q� � RowDefinitions �� $
��auto ��� $��* �.+�� $��	20,15,5,5q� �� $��30 ��� $��30 ��� $��30 ��� $��30 ��� $��30 ��� $��50 �	 � ColumnDefinitions	 �� $��* ��� $
��auto �.+�� $��0��	 �� $
��auto ��� $��* �.+�<  6KyoeiSystem.Framework.Windows.Controls.UcLabelComboBox  -   $��OrderColumn��$��0��$	��270��$
��Left=�$��1q�
   Label_Context$
 	表示順��   Label_Width$ 90Q�   ComboListingParams$, &マスタ検索,品番検索,表示順��;  5KyoeiSystem.Framework.Windows.Controls.UcLabelTextBox  -     Name$ txtCode��$��1��   ImeType$	 Off=�$
��Left=�$��1q�   Label_Context$ 品番��   Label_Width$ 90Q�   cWidth$	 180Q�    cVerticalContentAlignment$ Center=�  -   $ txtName��$��2��$ Native=�$
��Left=�$��1q�$ 品名��$ 90Q�$	 180Q�$ Center=�?  9KyoeiSystem.Framework.Windows.Controls.UcLabelTwinTextBox  -     Name$ txtBrand��$��3��   DataAccessName$ 	M14_BRAND��   DataAccessMode$ On=�$
��Left=�$��1q�   Label_Context$ ブランド��   Label_Width$ 90Q�   Label1Visibility$ 	Collapsed=�   Label2Visibility$ 	Collapsed=�   Text1IMEType$	 Off=�$   Text1VerticalContentAlignment$ Center=�   
Text2Width$	 150Q�   Text2IsReadOnly .    Text2BorderThickness$ 0q�   Text2Background
 ���� $    Text2VerticalContentAlignment$  Center=�  -   $ 	txtSeries��$��4��$ 
M15_SERIES��$ On=�$
��Left=�$��1q�$ シリーズ��$ 90Q�$ 	Collapsed=�$ 	Collapsed=�$	 Off=�$ Center=�$	 150Q� . $ 0q�
 ���� $  Center=�� $��5��$��2��	 �� $��90 ��� $��74 ��� $
��auto � �� $��25 ��� $��25 �.+�  -   $ lblItemClass��$��0��$��0��$��2��$��	0,10,0,10q�$ 商品形態分類��$ 90Q�$ 0Q�7  1KyoeiSystem.Framework.Windows.Controls.UcCheckBox  -   $��chkItemClass_1��!   cContent$! SET品     ��$��0��$��1��"   	IsChecked$
" TrueX�$��5,0,0,0q�  -	   $��chkItemClass_2��$! 単品・材料��$��0��$��2��$
" TrueX�$��0,0,0,0q�  -
   $��chkItemClass_3��$! 雑コード  ��$��1��$��1��$
" TrueX�$��5,0,0,0q�  -   $��chkItemClass_4��$! 副資材    ��$��1��$��2��$
" TrueX�$��0,0,0,0q��� -   $��OkButton��$��0��$��4��$��3��$��3q����	#FF071281# J� RenderTransformOrigin$# 0.5,0.5&�$��5q�$��61��$��49��$��Right=�$��Bottom=�$ J� OpacityMask#$ � ������ % �� ImageSource$5% /../Images/appbar.checkmark.pencil.top.white.png��& j� Stretch$
& None=�' j� 
AlignmentY$' Bottom=��� -   $��CancelButton��$��3��$��4��$��3��$��3q����	#FF071281$# 0.5,0.5&�$��5q�$��61��$��51��$
��Left=�$	��Top=�#$ � ������ $+% %../Images/appbar.app.remove.white.png��$
& None=�$' Bottom=�&   System.Windows.Controls.DataGrid  -   $��
SearchGrid��$��1��(   CanUserSortColumns( .  )   SelectionMode$) Single=�*   CanUserAddRows* .  +   CanUserDeleteRows+ .  ,   
IsReadOnly, . -   AutoGenerateColumns- . ���� . �� Path$. SearchResult�/ �� Mode$/ TwoWay=�0 �� NotifyOnSourceUpdated0 . 1 �� NotifyOnTargetUpdated1 . 2   ColumnHeaderStyle2 �� 3 �� 
TargetType=  7System.Windows.Controls.Primitives.DataGridColumnHeader"3  .��� 4 �� Property4 � ��5 �� Value$5 25���� 4 � ��
5 ������� 4 � ��$	5 0.5q��� 4 � ��
5 �   ��� 4 � ��$5 Center=��� GridHeaderStye��4 � ��#5 [  