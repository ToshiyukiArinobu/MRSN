   M S B A M L   `   `   `  ���� X  TKyoeiSystem.Framework.Windows, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null�[clr-namespace:KyoeiSystem.Framework.Windows.ViewBase;assembly=KyoeiSystem.Framework.Windows&KyoeiSystem.Framework.Windows.ViewBase  a ]KyoeiSystem.Framework.Windows.Controls, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null�dclr-namespace:KyoeiSystem.Framework.Windows.Controls;assembly=KyoeiSystem.Framework.Windows.Controls&KyoeiSystem.Framework.Windows.Controls �dclr-namespace:KyoeiSystem.Framework.Windows.Controls;assembly=KyoeiSystem.Framework.Windows.Controls&KyoeiSystem.Framework.Windows.Controls Z VKyoeiSystem.Application.Windows, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]3clr-namespace:KyoeiSystem.Application.Windows.Views%KyoeiSystem.Application.Windows.Views O Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem 4   .KyoeiSystem.Application.Windows.Views.ZIJ02010   bfw[clr-namespace:KyoeiSystem.Framework.Windows.ViewBase;assembly=KyoeiSystem.Framework.Windows  \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      R	 NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml 	    Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006 	    e
 aGrapeCity.WPF.SpreadGrid, Version=1.0.2015.1109, Culture=neutral, PublicKeyToken=5c5c8ff7a6858ccc=sg4http://schemas.grapecity.com/windows/spreadgrid/2012 
 :d2http://schemas.microsoft.com/expression/blend/2008 
 octlsdclr-namespace:KyoeiSystem.Framework.Windows.Controls;assembly=KyoeiSystem.Framework.Windows.Controls 
 sControlsdclr-namespace:KyoeiSystem.Framework.Windows.Controls;assembly=KyoeiSystem.Framework.Windows.Controls 
 <uc3clr-namespace:KyoeiSystem.Application.Windows.Views 
 3System&clr-namespace:System;assembly=mscorlib 
 -     � WindowStyle$  SingleBorderWindow=� � AllowsTransparency .  $	��728��$
��1024��$	��768��$
��1024�� � WindowStartupLocation$ CenterOwner=� � Left$ 0��
 � Top$ 0�� � Title$% 支払明細問合せ(ZIJ02010)��.���  � RowDefinitions �� $
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
��auto �.+�  -   $��purchaseDatePriod��$��0��$
��Left=�$ 仕入日指定��$	 100Q�!   
Label1Text$	! ～��$ 80Q�$ 10��"   Text1ValidationType$
" Date=�#   	Text1Mask$# 
yyyy/MM/dd��$ Center=�$ 80Q�$   Text2MaxLength$$ 10��%   Text2ValidationType$
% Date=�&   	Text2Mask$& 
yyyy/MM/dd��$  Center=�  -   $��depositDatePriod��$y�Hidden=�$��1��$
��Left=�$��50,0,0,0q�$ 入金日指定��$	 100Q�$	! ～��$ 80Q�$ 10��$
" Date=�$# 
yyyy/MM/dd��$ Center=�$ 80Q�$$ 10��$
% Date=�$& 
yyyy/MM/dd��$  Center=�� $��2��$��0,3,0,0q� �� $
��auto ��� $	��150 ��� $
��auto �.+�<  6KyoeiSystem.Framework.Windows.Controls.UcLabelComboBox  -   $��cmbInputType��$��0��$
��Left=�'   Label_Context$' 入力区分��(   Label_Width$	( 100Q�)   Combo_Width$	) 100Q�*   ComboListingParams$/* )随時,支払明細問合せ,入力区分��;  5KyoeiSystem.Application.Windows.Views.M01_TOK_TextBox  -   +  Name$+ 	suppliers��$��2��$
��Left=�,   Label_Context$, 仕入先指定��-   Label_Width$	- 100Q�.   DataAccessName$. M01_TOK��/   DataAccessMode$/ On=�0   LinkItem$	0 1,3��1   Label1Visibility$1 	Collapsed=�2   Label2Visibility$2 	Collapsed=�3   
Text1Width$3 40Q�4   Text1MaxLength$4 3��5   Text1IMEType$	5 Off=�$6   Text1VerticalContentAlignment$6 Center=�7   
Label1Text$	7 －��8   
Text2Width$8 30Q�9   Text2MaxLength$9 2��:   Text2IMEType$	: Off=�$;   Text2VerticalContentAlignment$; Center=�<   
Label2Text$<  ��  -   $��arrivalCompany��$��3��$
��Left=�$��0,3,0,0q�$ M70_JIS��$ On=�$ 入荷先指定��$	 100Q�$ 	Collapsed=�$ 	Collapsed=�$ 40Q�$ 3��$	 Off=�$ Center=�$	 250Q� . 
 ���� $ 0q� .  $  Center=��� -   $��	btnSearch��$��0��$��2��$��50��$��50��$��	670,0,0,0q�$
��Left=�$��Stretch=�$��0q�.���� $)��#../Images/検索実行ボタン.png��$
��Fill=�� $��1��.+�/ 
 )GrapeCity.Windows.SpreadGrid.GcSpreadGrid  -   $��
spGridList��=   AutoGenerateColumns= .  >   
LayoutMode$
> Cell=�?   RowCount$? 0��@   ColumnCount$@ 12��$	��350��A   ColumnDragMode$A Direct=�B   CanUserSortColumnsB .  C   CanUserFilterColumnsC .  $��	10,5,10,1q�D   DocumentUri$KD E/KyoeiSystem.Application.Windows;component/views/ZIJ/ZIJ02010G1.sgxml?�E   ItemsSourceE �� *SearchResult+F �� Mode$F TwoWay=�G �� UpdateSourceTrigger$G PropertyChanged=�H �� NotifyOnSourceUpdatedH . I �� NotifyOnTargetUpdatedI . J   SelectionBorderUnfocusedJ -	 
 'GrapeCity.Windows.SpreadGrid.BorderLine	  K 	  Color$K Orange��L 	  Style$L Thick=�M   SelectionBorderM 	  $K Orange��$L Thick=�N   ColumnsN )
 
 #GrapeCity.Windows.SpreadGrid.Column
  O 
  Locked$
O TrueX�P 
  HorizontalAlignment$P CenterX�Q 
  CellTypeQ / 
 )GrapeCity.Windows.SpreadGrid.TextCellType  R   	MaxLength$R 10��S   MaxLengthUnit$
S Byte=�T   ImeState$	T Off=�U 
  	DataFieldU 3 
 -GrapeCity.Windows.SpreadGrid.BindingDataField  V   BindingV �� *	仕入日+W �� StringFormat$W {0:yyyy/MM/dd}��
  $
O TrueX�$P CenterX�U   V �� *仕入区分+
  $
O TrueX�$P CenterX�U   V �� *入力区分+
  $
O TrueX�$P CenterX�U   V �� *伝票番号+
  $
O TrueX�$P CenterX�U   V �� *元伝票番号+
  $
O TrueX�U   V �� *仕入先名+
  $
O TrueX�$P CenterX�U   V �� *品番コード+
  $
O TrueX�$P CenterX�U   V �� *自社品番+
  $
O TrueX�X 
  	Focusable$
X TrueX�U   V �� *自社品名+
  $
O TrueX�$
X TrueX�U   V �� *	自社色+
  $
O TrueX�$P CenterX�Q   $R 10��$
S Byte=�$	T Off=�U   V �� *賞味期限+$W {0:yyyy/MM/dd}��
  $
O TrueX�$P RightX�Q 1 
 +GrapeCity.Windows.SpreadGrid.NumberCellType  Y   MaxValue$Y 999999999999~�Z   MinValue$Z -999999999999~�[   SpinButtonVisibility$[ NotShow=�\   FieldSetF 
 @GrapeCity.Windows.SpreadGrid.Editors.NumberFieldSetTypeConverter$\ ###,###,##0,,,-, U   V �� *数量+
  $
O TrueX�U   V �� *単位+
  $
O TrueX�$P RightX�Q   $Y 999999999999~�$Z -999999999999~�$[ NotShow=�$\ ###,###,##0,,,-, U   V �� *単価+
  $
O TrueX�$P RightX�Q   $Y 999999999999~�$Z -999999999999~�$[ NotShow=�$\ ###,###,##0,,,-, U   V �� *金額+
  $
O TrueX�U   V �� *摘要+
  $
O TrueX�$P CenterX�U   V �� *発注番号+�� $y�Hidden=�$��2��
���   �$��1q�$��10q�$��2��$ 5,5,5,5q�._�� 
������� �� $��* ��� $��* ��� $��* ��� $��* ��� $��* � �� $��30 ��� $��30 �.+�;  5KyoeiSystem.Framework.Windows.Controls.UcLabelTextBox  -   $��	sumBlank1��$y�Hidden=�$��0��$��0��]   Label_Context$]  ��^   Label_Width$^ 90Q�_   cTextAlignment$_ Right=� `   cVerticalContentAlignment$` Center=�a   cIsReadOnlya . b   cAcceptsTabb .    -   $��	sumBlank2��$y�Hidden=�$��0��$��1��$]  ��$^ 90Q�$_ Right=�$` Center=�a . b .    -   $��
sumSrTotal��$��0��$��2��$] 仕入合計額��$^ 90Q�$_ Right=�$` Center=�a . b .    -   $��sumSrTax��$��0��$��3��$] 仕入消費税額��$^ 90Q�$_ Right=�$` Center=�a . b .    -   $��	sumBlank3��$y�Hidden=�$��1��$��0��$]  ��$^ 90Q�$_ Right=�$` Center=�a . b .    -   $��	sumBlank4��$y�Hidden=�$��1��$��1��$]  ��$^ 90Q�$_ Right=�$` Center=�a . b .    -   $��sumRtnTotal��$��1��$��2��$] 返品合計額��$^ 90Q�$_ Right=�$` Center=�a . b .    -   $��	sumRtnTax��$��1��$��3��$] 返品消費税額��$^ 90Q�$_ Right=�$` Center=�a . b .    -   $��sumTotal��$��1��$��4��$] 総合計額��$^ 90Q�$_ Right=�$` Center=�a . b .  