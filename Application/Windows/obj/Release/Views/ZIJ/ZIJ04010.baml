   M S B A M L   `   `   `  ���� X  TKyoeiSystem.Framework.Windows, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null�[clr-namespace:KyoeiSystem.Framework.Windows.ViewBase;assembly=KyoeiSystem.Framework.Windows&KyoeiSystem.Framework.Windows.ViewBase  a ]KyoeiSystem.Framework.Windows.Controls, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null�dclr-namespace:KyoeiSystem.Framework.Windows.Controls;assembly=KyoeiSystem.Framework.Windows.Controls&KyoeiSystem.Framework.Windows.Controls �dclr-namespace:KyoeiSystem.Framework.Windows.Controls;assembly=KyoeiSystem.Framework.Windows.Controls&KyoeiSystem.Framework.Windows.Controls Z VKyoeiSystem.Application.Windows, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]3clr-namespace:KyoeiSystem.Application.Windows.Views%KyoeiSystem.Application.Windows.Views O Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem 4   .KyoeiSystem.Application.Windows.Views.ZIJ04010   bfw[clr-namespace:KyoeiSystem.Framework.Windows.ViewBase;assembly=KyoeiSystem.Framework.Windows  \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      R	 NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml 	    Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006 	    e
 aGrapeCity.WPF.SpreadGrid, Version=1.0.2015.1109, Culture=neutral, PublicKeyToken=5c5c8ff7a6858ccc=sg4http://schemas.grapecity.com/windows/spreadgrid/2012 
 :d2http://schemas.microsoft.com/expression/blend/2008 
 octlsdclr-namespace:KyoeiSystem.Framework.Windows.Controls;assembly=KyoeiSystem.Framework.Windows.Controls 
 sControlsdclr-namespace:KyoeiSystem.Framework.Windows.Controls;assembly=KyoeiSystem.Framework.Windows.Controls 
 <uc3clr-namespace:KyoeiSystem.Application.Windows.Views 
 3System&clr-namespace:System;assembly=mscorlib 
 -     � WindowStyle$  SingleBorderWindow=� � AllowsTransparency .  $	��728��$
��1024��$	��768��$
��1024�� � WindowStartupLocation$ CenterOwner=� � Left$ 0��
 � Top$ 0�� � Title$% 出金伝票問合せ(ZIJ04010)��.���  � RowDefinitions �� $
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
10,10,10,5q�._��  �� $��30 ��� $��30 ��� $��30 ��� $��33 �.+�?  9KyoeiSystem.Framework.Windows.Controls.UcLabelTwinTextBox  -   $��	myCompany��$��0��$
��Left=�$��5,3,0,0q�   DataAccessName$ M70_JIS��   DataAccessMode$ On=�   Label1Visibility$ 	Collapsed=�   Label2Visibility$ 	Collapsed=�   Label_Context$ 自社コード��   Label_Width$	 100Q�   
Text1Width$ 40Q�   Text1MaxLength$ 3��   Text1IMEType$	 Off=�$   Text1VerticalContentAlignment$ Center=�   
Text2Width$	 250Q�   Text2IsReadOnly .    Text2Background
 ����    Text2BorderThickness$ 0q�   Text2AcceptsTab .  $   Text2VerticalContentAlignment$ Center=�� $��1��$��5,3,0,0q� �� $
��auto ��� $
��auto ��� $	��150 ��� $
��auto �.+�;  5KyoeiSystem.Framework.Windows.Controls.UcLabelTextBox  -   $��withdrawalDateFrom��$��0��$
��Left=�    Label_Context$  出金日指定��!   Label_Width$	! 100Q�"   cWidth$" 80Q�#   
cMaxLength$# 10��$   ValidationType$
$ Date=�%   Mask$% 
yyyy/MM/dd���� $	��～��$��1��$��Center=�$��Center=�$��5,0,0,0q�  -   $��withdrawalDateTo��$��2��$
��Left=�&   Label_Visibility$& Hidden=�'   Label_cIsVisible' .  $" 80Q�$# 10��$
$ Date=�$% 
yyyy/MM/dd��<  6KyoeiSystem.Framework.Windows.Controls.UcLabelComboBox  -   $��cmbDepositType��$��1��$��3��$
��Left=�$��5,0,0,0q�(   Label_Context$( 金種��)   Label_Width$	) 100Q�*   Combo_Width$	* 100Q�+   ComboListingParams$#+ 随時,出金問合せ,金種��  -   $��depositCompany��$��2��$
��Left=�$��5,3,0,0q�$ M70_JIS��$ On=�$ 出金先販社��$	 100Q�$ 	Collapsed=�$ 	Collapsed=�$ 40Q�$ 3��$	 Off=�$ Center=�$	 250Q� . 
 ���� $ 0q� .  $ Center=�;  5KyoeiSystem.Application.Windows.Views.M01_TOK_TextBox  -   ,  Name$	, TOK��$��3��$
��Left=�$��5,3,0,3q�-   Label_Context$- 得意先指定��.   Label_Width$	. 100Q�/   DataAccessName$/ M01_TOK��0   DataAccessMode$0 On=�1   LinkItem$	1 1,3��2   Label1Visibility$2 	Collapsed=�3   Label2Visibility$3 	Collapsed=�4   
Text1Width$4 40Q�5   Text1MaxLength$5 3��6   Text1IMEType$	6 Off=�$7   Text1VerticalContentAlignment$7 Center=�8   
Label1Text$	8 －��9   
Text2Width$9 30Q�:   Text2MaxLength$: 2��;   Text2IMEType$	; Off=�$<   Text2VerticalContentAlignment$< Center=�=   
Label2Text$=  ���� -   $��	btnSearch��$��2��$��2��$��50��$��50��$��	360,0,0,0q�$
��Left=�$��Stretch=�$��0q�.���� $)��#../Images/検索実行ボタン.png��$
��Fill=�� $��1��.+�/	 
 )GrapeCity.Windows.SpreadGrid.GcSpreadGrid	  -   $��
spGridList��> 	  AutoGenerateColumns> .  ? 	  
LayoutMode$
? Cell=�@ 	  RowCount$@ 0��A 	  ColumnCount$A 8��$	��370��B 	  ColumnDragMode$B Direct=�C 	  CanUserSortColumnsC .  D 	  CanUserFilterColumnsD .  $��	10,5,10,1q�E 	  DocumentUri$KE E/KyoeiSystem.Application.Windows;component/views/ZIJ/ZIJ04010G1.sgxml?�F 	  ItemsSourceF �� *SearchResult+G �� Mode$G TwoWay=�H �� UpdateSourceTrigger$H PropertyChanged=�I �� NotifyOnSourceUpdatedI . J �� NotifyOnTargetUpdatedJ . K 	  SelectionBorderUnfocusedK -
 
 'GrapeCity.Windows.SpreadGrid.BorderLine
  L 
  Color$L Orange��M 
  Style$M Thick=�N 	  SelectionBorderN 
  $L Orange��$M Thick=�O 	  ColumnsO ) 
 #GrapeCity.Windows.SpreadGrid.Column  P   Locked$
P TrueX�Q   	Focusable$
Q TrueX�R   HorizontalAlignment$R CenterX�S   CellTypeS / 
 )GrapeCity.Windows.SpreadGrid.TextCellType  T   	MaxLength$T 10��U   MaxLengthUnit$
U Byte=�V   ImeState$	V Off=�W   	DataFieldW 3 
 -GrapeCity.Windows.SpreadGrid.BindingDataField  X   BindingX �� *	出金日+  $
P TrueX�$
Q TrueX�$R CenterX�W   X �� *伝票番号+  $
P TrueX�$
Q TrueX�W   X �� *得意先名+  $
P TrueX�$
Q TrueX�W   X �� *出金先販社名+  $
P TrueX�$
Q TrueX�$R CenterX�W   X �� *	金種名+  $
P TrueX�$
Q TrueX�$R CenterX�S   $T 10��$
U Byte=�$	V Off=�W   X �� *期日+  $
P TrueX�$R RightX�S 1 
 +GrapeCity.Windows.SpreadGrid.NumberCellType  Y   MaxValue$Y 	999999999~�Z   MinValue$Z 
-999999999~�[   SpinButtonVisibility$[ NotShow=�\   FieldSetF 
 @GrapeCity.Windows.SpreadGrid.Editors.NumberFieldSetTypeConverter$\ ###,###,##0,,,-, W   X �� *金額+  $
P TrueX�S   $T 30��$
U Byte=�$V On=�W   X �� *摘要+�� $��2��
���   �$��1q�$��10q�$��2��] �� Padding$] 5,5,5,5q�._�� 
������� �� $��* ��� $��* ��� $��* ��� $��* ��� $��* � �� $��30 ��� $��30 �.+�  -   $��sumCash��$��0��$��0��$  現金合計��$! 90Q�^   cTextAlignment$^ Right=� _   cVerticalContentAlignment$_ Center=�`   cIsReadOnly` . a   cAcceptsTaba .    -   $��sumTransfer��$��0��$��1��$  振込合計��$! 90Q�$^ Right=�$_ Center=�` . a .    -   $��sumCheck��$��0��$��2��$  小切手合計��$! 90Q�$^ Right=�$_ Center=�` . a .    -   $��sumPromissory��$��0��$��3��$  手形合計��$! 90Q�$^ Right=�$_ Center=�` . a .    -   $��	sumOffset��$��1��$��0��$  相殺合計��$! 90Q�$^ Right=�$_ Center=�` . a .    -   $��sumAjustment��$��1��$��1��$  調整合計��$! 90Q�$^ Right=�$_ Center=�` . a .    -   $��sumOther��$��1��$��2��$  その他合計��$! 90Q�$^ Right=�$_ Center=�` . a .    -   $��sumTotal��$��1��$��4��$  出金合計��$! 90Q�$^ Right=�$_ Center=�` . a .  