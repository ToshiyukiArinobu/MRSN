   M S B A M L   `   `   `  ���� X  TKyoeiSystem.Framework.Windows, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null�[clr-namespace:KyoeiSystem.Framework.Windows.ViewBase;assembly=KyoeiSystem.Framework.Windows&KyoeiSystem.Framework.Windows.ViewBase  a ]KyoeiSystem.Framework.Windows.Controls, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null�dclr-namespace:KyoeiSystem.Framework.Windows.Controls;assembly=KyoeiSystem.Framework.Windows.Controls&KyoeiSystem.Framework.Windows.Controls �dclr-namespace:KyoeiSystem.Framework.Windows.Controls;assembly=KyoeiSystem.Framework.Windows.Controls&KyoeiSystem.Framework.Windows.Controls Z VKyoeiSystem.Application.Windows, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]3clr-namespace:KyoeiSystem.Application.Windows.Views%KyoeiSystem.Application.Windows.Views O Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem 4   .KyoeiSystem.Application.Windows.Views.ZIJ01010   bfw[clr-namespace:KyoeiSystem.Framework.Windows.ViewBase;assembly=KyoeiSystem.Framework.Windows  \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      R	 NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml 	    Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006 	    e
 aGrapeCity.WPF.SpreadGrid, Version=1.0.2015.1109, Culture=neutral, PublicKeyToken=5c5c8ff7a6858ccc=sg4http://schemas.grapecity.com/windows/spreadgrid/2012 
 :d2http://schemas.microsoft.com/expression/blend/2008 
 octlsdclr-namespace:KyoeiSystem.Framework.Windows.Controls;assembly=KyoeiSystem.Framework.Windows.Controls 
 sControlsdclr-namespace:KyoeiSystem.Framework.Windows.Controls;assembly=KyoeiSystem.Framework.Windows.Controls 
 <uc3clr-namespace:KyoeiSystem.Application.Windows.Views 
 3System&clr-namespace:System;assembly=mscorlib 
 -     � WindowStyle$  SingleBorderWindow=� � AllowsTransparency .  $	��728��$
��1024��$	��768��$
��1024�� � WindowStartupLocation$ CenterOwner=� � Left$ 0��
 � Top$ 0�� � Title$( "仕入データ問合せ(ZIJ01010)��.���  � RowDefinitions �� $
��auto ��� $
��auto ��� $
��300* ��� $��82 �.+�+  %System.Windows.Controls.Ribbon.Ribbon  $��Stretch=�$��Stretch=�.@�.  (System.Windows.Controls.Ribbon.RibbonTab  $��	ホーム��   KeyTip$ h��$��75��.@�0  *System.Windows.Controls.Ribbon.RibbonGroup  $��90��  $��F1��   KeyTip$ MASTER��   RibbonFunctionBackground#��[  .@�1  +System.Windows.Controls.Ribbon.RibbonButton  -   	   LargeImageSource$%	 ../Images/15マスタ照会.png��$��0,0,0,0q�
   ToolTipTitle$
 マスタ照会(F1)��   ToolTipDescription$3 -入力済みのデータを検索します。��   ToolTipImageSource$% ../Images/15マスタ照会.png��   Label$ ﾏｽﾀ照会��   KeyTip$ F1��$��50��  $��F2��$ MASTER��#��[  .@�  $y�Hidden=�$  ��$
  ��$  ��  $��F3��$ MASTER��#��[  .@�  $y�Hidden=�$  ��$
  ��$  ��  $��F4��$ MASTER��#��[  .@�  $y�Hidden=�$  ��$
  ��$  ��  $��F5��$ MASTER��#��[  .@�  -   $	 ../Images/19CSV出力.png��$ F5��$��50��$ 	CSV出力��$
 CSV出力(F5)��$' !CSVファイルに出力します��$ ../Images/19CSV出力.png��  $��F6��$ MASTER��#��[  .@�  $y�Hidden=�$  ��$
  ��$  ��  $��F7��$ MASTER��#��[  .@�  $y�Hidden=�$  ��$
  ��$  ��  $��F8��$ MASTER��#��[  .@�  -   $	 ../Images/23印刷.png��$ F8��$��50��$ 印刷��$
 
印刷(F8)��$ 印刷します��$ ../Images/23印刷.png��  $��F9��$ MASTER��#��[  .@�  $y�Hidden=�$  ��$
  ��$  ��  $	��F10��$ MASTER��#��[  .@�  $y�Hidden=�$  ��$
  ��$  ��  $	��F11��$ MASTER��#��[  .@�  -   $	 ../Images/27終了.png��$	 F11��$��50��$ 終了��$
 終了(F11)��$ 終了します��$ ../Images/27終了.png��  $	��F12��$ MASTER��#��[  .@�  $y�Hidden=�$  ��$
  ��$  ��  $��	ヘルプ��
�������.@�  $��サポート��.@�  -   $%	 ../Images/30マニュアル.png��$ MANUAL��$ ﾏﾆｭｱﾙ��$
 取扱説明書��$* $取り扱い説明書を開きます��  -   $(	 "../Images/29遠隔サポート.png��$ SUPPORT��$ お問合せ��$
 お問合せ��$< 6Web上の｢お問合せ｣ページに遷移します��  -   $	 ../Images/QandA.png��$	 FAQ��$ QandA��$
 QandA��$E ?Web上の｢よくあるご質問｣ページに遷移します��  -	   $(	 "../Images/13共栄Webリンク.png��$ KYOEI��$ 共栄ｼｽﾃﾑ��$
 共栄システムHP��$< 6共栄システムのホームページを開きます��� $��0��$��Bottom=� �� $��74 � � ColumnDefinitions �� $��90 ��� $��* �.+�]� $��0��$��0��$��Stretch=�$��2q����� *MaintenanceModeBackground+.+��� $��メイリオ)�$��24(�$��Medium"�$��Stretch=�$��Center=�$��Center=�$��Center=����� *MaintenanceMode+���� *MaintenanceModeForeground+�� $��0��$��1��$��Bottom=�
��� ���
���  ��$��2,0,2,0q����� *ErrorMessage+y��� *ErrorMessageVisibility+� -
   $
��Func��$��1��
������� �� $
��auto ��� $
��auto ��� $
��auto �.+��� -   $��Header��
���   �$��0��$��1q�$��
10,10,10,5q� �� Padding$ 5q�._��  �� $��30 ��� $��30 ��� $��30 ��� $��33 �.+�?  9KyoeiSystem.Framework.Windows.Controls.UcLabelTwinTextBox  -   $��	myCompany��$��0��$
��Left=�   DataAccessName$ M70_JIS��   DataAccessMode$ On=�   Label1Visibility$ 	Collapsed=�   Label2Visibility$ 	Collapsed=�   Label_Context$ 自社コード��   Label_Width$	 100Q�   
Text1Width$ 40Q�   Text1MaxLength$ 3��   Text1IMEType$	 Off=�$   Text1VerticalContentAlignment$ Center=�   
Text2Width$	 250Q�   Text2IsReadOnly .    Text2Background
 ����    Text2BorderThickness$ 0q�   Text2AcceptsTab .  $    Text2VerticalContentAlignment$  Center=�� $��1��$��0,3,0,0q� �� $
��auto ��� $
��auto ��� $	��150 ��� $
��auto ��� $
��auto ��� $
��auto �.+�;  5KyoeiSystem.Framework.Windows.Controls.UcLabelTextBox  -   $��purchaseDateFrom��$��0��$
��Left=�!   Label_Context$! 仕入日指定��"   Label_Width$	" 100Q�#   cWidth$# 80Q�$   
cMaxLength$$ 10��%   ValidationType$
% Date=�&   Mask$& 
yyyy/MM/dd���� $	��～��$��1��$��Center=�$��Center=�$��5,0,0,0q�  -   $��purchaseDateTo��$��2��$
��Left=�'   Label_Visibility$' Hidden=�(   Label_cIsVisible( .  $# 80Q�$$ 10��$
% Date=�$& 
yyyy/MM/dd��� $��2��$��0,3,0,0q� �� $
��auto ��� $	��150 ��� $
��auto �.+�<  6KyoeiSystem.Framework.Windows.Controls.UcLabelComboBox  -   $��cmbInputType��$��0��$
��Left=�)   Label_Context$) 入力区分��*   Label_Width$	* 100Q�+   Combo_Width$	+ 100Q�,   ComboListingParams$/, )随時,支払明細問合せ,入力区分��;  5KyoeiSystem.Application.Windows.Views.M01_TOK_TextBox  -   -  Name$- 	suppliers��$��2��$
��Left=�.   Label_Context$. 仕入先指定��/   Label_Width$	/ 100Q�0   DataAccessName$0 M01_TOK��1   DataAccessMode$1 On=�2   LinkItem$	2 1,3��3   Label1Visibility$3 	Collapsed=�4   Label2Visibility$4 	Collapsed=�5   
Text1Width$5 40Q�6   Text1MaxLength$6 3��7   Text1IMEType$	7 Off=�$8   Text1VerticalContentAlignment$8 Center=�9   
Label1Text$	9 －��:   
Text2Width$: 30Q�;   Text2MaxLength$; 2��<   Text2IMEType$	< Off=�$=   Text2VerticalContentAlignment$= Center=�>   
Label2Text$>  ��  -   $��arrivalCompany��$��3��$
��Left=�$��0,3,0,0q�$ M70_JIS��$ On=�$ 入荷先指定��$	 100Q�$ 	Collapsed=�$ 	Collapsed=�$ 40Q�$ 3��$	 Off=�$ Center=�$	 250Q� . 
 ���� $ 0q� .  $  Center=��� -   $��	btnSearch��$��0��$��2��$��50��$��50��$��	670,0,0,0q�$
��Left=�$��Stretch=�$��0q�.���� $)��#../Images/検索実行ボタン.png��$
��Fill=�� $��1��.+�/	 
 )GrapeCity.Windows.SpreadGrid.GcSpreadGrid	  -   $��
spGridList��? 	  AutoGenerateColumns? .  @ 	  
LayoutMode$
@ Cell=�A 	  RowCount$A 0��B 	  ColumnCount$B 12��$	��350��C 	  ColumnDragMode$C Direct=�D 	  CanUserSortColumnsD .  E 	  CanUserFilterColumnsE .  $��	10,5,10,1q�F 	  DocumentUri$KF E/KyoeiSystem.Application.Windows;component/views/ZIJ/ZIJ01010G1.sgxml?�G 	  ItemsSourceG �� *SearchResult+H �� Mode$H TwoWay=�I �� UpdateSourceTrigger$I PropertyChanged=�J �� NotifyOnSourceUpdatedJ . K �� NotifyOnTargetUpdatedK . L 	  SelectionBorderUnfocusedL -
 
 'GrapeCity.Windows.SpreadGrid.BorderLine
  M 
  Color$M Orange��N 
  Style$N Thick=�O 	  SelectionBorderO 
  $M Orange��$N Thick=�P 	  ColumnsP ) 
 #GrapeCity.Windows.SpreadGrid.Column  Q   Locked$
Q TrueX�R   	Focusable$
R TrueX�S   HorizontalAlignment$S CenterX�T   	DataFieldT 3 
 -GrapeCity.Windows.SpreadGrid.BindingDataField  U   BindingU �� *入力区分名+  $
Q TrueX�$
R TrueX�$S CenterX�V   CellTypeV / 
 )GrapeCity.Windows.SpreadGrid.TextCellType  W   	MaxLength$W 10��X   MaxLengthUnit$
X Byte=�Y   ImeState$	Y Off=�T   U �� *	仕入日+  $
Q TrueX�$
R TrueX�$S CenterX�T   U �� *伝票番号+  $
Q TrueX�$
R TrueX�$S CenterX�T   U �� *返品伝票番号+  $
Q TrueX�$
R TrueX�T   U �� *仕入先名+  $
Q TrueX�$S RightX�V 1 
 +GrapeCity.Windows.SpreadGrid.NumberCellType  Z   MaxValue$Z 999999999999~�[   MinValue$[ -999999999999~�\   SpinButtonVisibility$\ NotShow=�]   FieldSetF 
 @GrapeCity.Windows.SpreadGrid.Editors.NumberFieldSetTypeConverter$] ###,###,##0,,,-, T   U �� *合計金額+  $
Q TrueX�$S RightX�V   $Z 999999999999~�$[ -999999999999~�$\ NotShow=�$] ###,###,##0,,,-, T   U �� *返品合計金額+  $
Q TrueX�$S RightX�V   $Z 	999999999~�$[ 
-999999999~�$\ NotShow=�$] ###,###,##0,,,-, T   U �� *	消費税+  $
Q TrueX�$S RightX�V   $Z 	999999999~�$[ 
-999999999~�$\ NotShow=�$] ###,###,##0,,,-, T   U �� *返品消費税+  $
Q TrueX�$
R TrueX�T   U �� *入荷先名+  $
Q TrueX�V   $W 30��$
X Byte=�$Y On=�T   U �� *備考+  $
Q TrueX�$
R TrueX�$S CenterX�T   U �� *発注番号+�� $��2��
���   �$��1q�$��10q�$��2��$ 5,5,5,5q�._�� 
������� �� $��* ��� $��* ��� $��* ��� $��* ��� $��* � �� $��30 ��� $��30 �.+�  -   $��	sumBlank1��$y�Hidden=�$��0��$��0��$!  ��$" 90Q�^   cTextAlignment$^ Right=� _   cVerticalContentAlignment$_ Center=�`   cIsReadOnly` . a   cAcceptsTaba .    -   $��	sumBlank2��$y�Hidden=�$��0��$��1��$!  ��$" 90Q�$^ Right=�$_ Center=�` . a .    -   $��
sumSrTotal��$��0��$��2��$! 仕入合計額��$" 90Q�$^ Right=�$_ Center=�` . a .    -   $��sumSrTax��$��0��$��3��$! 仕入消費税額��$" 90Q�$^ Right=�$_ Center=�` . a .    -   $��	sumBlank3��$y�Hidden=�$��1��$��0��$!  ��$" 90Q�$^ Right=�$_ Center=�` . a .    -   $��	sumBlank4��$y�Hidden=�$��1��$��1��$!  ��$" 90Q�$^ Right=�$_ Center=�` . a .    -   $��sumRtnTotal��$��1��$��2��$! 返品合計額��$" 90Q�$^ Right=�$_ Center=�` . a .    -   $��	sumRtnTax��$��1��$��3��$! 返品消費税額��$" 90Q�$^ Right=�$_ Center=�` . a .    -   $��sumTotal��$��1��$��4��$! 総合計額��$" 90Q�$^ Right=�$_ Center=�` . a .  