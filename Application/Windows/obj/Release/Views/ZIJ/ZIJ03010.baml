   M S B A M L   `   `   `  ���� X  TKyoeiSystem.Framework.Windows, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null�[clr-namespace:KyoeiSystem.Framework.Windows.ViewBase;assembly=KyoeiSystem.Framework.Windows&KyoeiSystem.Framework.Windows.ViewBase  a ]KyoeiSystem.Framework.Windows.Controls, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null�dclr-namespace:KyoeiSystem.Framework.Windows.Controls;assembly=KyoeiSystem.Framework.Windows.Controls&KyoeiSystem.Framework.Windows.Controls �dclr-namespace:KyoeiSystem.Framework.Windows.Controls;assembly=KyoeiSystem.Framework.Windows.Controls&KyoeiSystem.Framework.Windows.Controls Z VKyoeiSystem.Application.Windows, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]3clr-namespace:KyoeiSystem.Application.Windows.Views%KyoeiSystem.Application.Windows.Views O Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem 4   .KyoeiSystem.Application.Windows.Views.ZIJ03010   bfw[clr-namespace:KyoeiSystem.Framework.Windows.ViewBase;assembly=KyoeiSystem.Framework.Windows  \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      R	 NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml 	    Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006 	    e
 aGrapeCity.WPF.SpreadGrid, Version=1.0.2015.1109, Culture=neutral, PublicKeyToken=5c5c8ff7a6858ccc=sg4http://schemas.grapecity.com/windows/spreadgrid/2012 
 :d2http://schemas.microsoft.com/expression/blend/2008 
 octlsdclr-namespace:KyoeiSystem.Framework.Windows.Controls;assembly=KyoeiSystem.Framework.Windows.Controls 
 sControlsdclr-namespace:KyoeiSystem.Framework.Windows.Controls;assembly=KyoeiSystem.Framework.Windows.Controls 
 <uc3clr-namespace:KyoeiSystem.Application.Windows.Views 
 3System&clr-namespace:System;assembly=mscorlib 
 -     � WindowStyle$  SingleBorderWindow=� � AllowsTransparency .  $	��728��$
��1024��$	��768��$
��1024�� � WindowStartupLocation$ CenterOwner=� � Language$ ja-JP� � Title$% 入金伝票問合せ(ZIJ03010)��.���  � RowDefinitions �� $
��auto ��� $
��auto ��� $
��300* ��� $��82 �.+�+  %System.Windows.Controls.Ribbon.Ribbon  $��Stretch=�$��Stretch=�.@�.  (System.Windows.Controls.Ribbon.RibbonTab  $��	ホーム��   KeyTip$ h��$��75��.@�0  *System.Windows.Controls.Ribbon.RibbonGroup  $��90��  $��F1��   KeyTip$ MASTER��   RibbonFunctionBackground#��[  .@�1  +System.Windows.Controls.Ribbon.RibbonButton  -      LargeImageSource$% ../Images/15マスタ照会.png��$��0,0,0,0q�	   ToolTipTitle$	 マスタ照会(F1)��
   ToolTipDescription$3
 -入力済みのデータを検索します。��   ToolTipImageSource$% ../Images/15マスタ照会.png��   Label$ ﾏｽﾀ照会��   KeyTip$ F1��$��50��  $��F2��$ MASTER��#��[  .@�  $y�Hidden=�$  ��$	  ��$
  ��  $��F3��$ MASTER��#��[  .@�  $y�Hidden=�$  ��$	  ��$
  ��  $��F4��$ MASTER��#��[  .@�  $y�Hidden=�$  ��$	  ��$
  ��  $��F5��$ MASTER��#��[  .@�  -   $ ../Images/19CSV出力.png��$ F5��$��50��$ 	CSV出力��$	 CSV出力(F5)��$'
 !CSVファイルに出力します��$ ../Images/19CSV出力.png��  $��F6��$ MASTER��#��[  .@�  $y�Hidden=�$  ��$	  ��$
  ��  $��F7��$ MASTER��#��[  .@�  $y�Hidden=�$  ��$	  ��$
  ��  $��F8��$ MASTER��#��[  .@�  -   $ ../Images/23印刷.png��$ F8��$��50��$ 印刷��$	 
印刷(F8)��$
 印刷します��$ ../Images/23印刷.png��  $��F9��$ MASTER��#��[  .@�  $y�Hidden=�$  ��$	  ��$
  ��  $	��F10��$ MASTER��#��[  .@�  $y�Hidden=�$  ��$	  ��$
  ��  $	��F11��$ MASTER��#��[  .@�  -   $ ../Images/27終了.png��$	 F11��$��50��$ 終了��$	 終了(F11)��$
 終了します��$ ../Images/27終了.png��  $	��F12��$ MASTER��#��[  .@�  $y�Hidden=�$  ��$	  ��$
  ��  $��	ヘルプ��
�������.@�  $��サポート��.@�  -   $% ../Images/30マニュアル.png��$ MANUAL��$ ﾏﾆｭｱﾙ��$	 取扱説明書��$*
 $取り扱い説明書を開きます��  -   $( "../Images/29遠隔サポート.png��$ SUPPORT��$ お問合せ��$	 お問合せ��$<
 6Web上の｢お問合せ｣ページに遷移します��  -   $ ../Images/QandA.png��$	 FAQ��$ QandA��$	 QandA��$E
 ?Web上の｢よくあるご質問｣ページに遷移します��  -	   $( "../Images/13共栄Webリンク.png��$ KYOEI��$ 共栄ｼｽﾃﾑ��$	 共栄システムHP��$<
 6共栄システムのホームページを開きます��� $��0��$��Bottom=� �� $��74 � � ColumnDefinitions �� $��90 ��� $��* �.+�]� $��0��$��0��$��Stretch=�$��2q����� *MaintenanceModeBackground+.+��� $��メイリオ)�$��24(�$��Medium"�$��Stretch=�$��Center=�$��Center=�$��Center=����� *MaintenanceMode+���� *MaintenanceModeForeground+�� $��0��$��1��$��Bottom=�
��� ���
���  ��$��2,0,2,0q����� *ErrorMessage+y��� *ErrorMessageVisibility+� $��1��$��10q�
������� �� $
��auto ��� $
��auto ��� $
��auto ��� $
��auto �.+��� -
   $��Header��$��1��
���   �$��1q�._��  �� $
��auto ��� $
��auto ��� $
��auto ��� $
��auto ��� $
��auto � �� $
��auto ��� $
��auto �.+�?  9KyoeiSystem.Framework.Windows.Controls.UcLabelTwinTextBox  -   $��	myCompany��$��0��$
��Left=�$��5,3,0,0q�   DataAccessName$ M70_JIS��   DataAccessMode$ On=�   Label1Visibility$ 	Collapsed=�   Label2Visibility$ 	Collapsed=�   Label_Context$ 自社コード��   Label_Width$	 100Q�   
Text1Width$ 40Q�   Text1MaxLength$ 3��   Text1IMEType$	 Off=�$   Text1VerticalContentAlignment$ Center=�   
Text2Width$	 250Q�   Text2IsReadOnly .    Text2Background
 ����    Text2BorderThickness$ 0q�   Text2AcceptsTab .  $   Text2VerticalContentAlignment$ Center=�� $��1��$��5,3,0,0q� �� $
��auto ��� $
��auto ��� $
��auto �.+�;  5KyoeiSystem.Framework.Windows.Controls.UcLabelTextBox  -   $��depositDateFrom��$��0��$
��Left=�   Label_Context$ 入金日指定��    Label_Width$	  100Q�!   cWidth$! 80Q�"   
cMaxLength$" 10��#   ValidationType$
# Date=�$   Mask$$ 
yyyy/MM/dd���� $	��～��$��1��$��Center=�$��Center=�$��5,0,0,0q�  -   $��depositDateTo��$��2��$
��Left=�%   Label_Visibility$% Hidden=�&   Label_cIsVisible& .  $! 80Q�$" 10��$
# Date=�$$ 
yyyy/MM/dd��  -   $��depositCompany��$��2��$
��Left=�$��5,3,0,0q�$ M70_JIS��$ On=�$ 入金元販社��$	 100Q�$ 	Collapsed=�$ 	Collapsed=�$ 40Q�$ 3��$	 Off=�$ Center=�$	 250Q� . 
 ���� $ 0q� .  $ Center=�;  5KyoeiSystem.Application.Windows.Views.M01_TOK_TextBox  -   '  Name$	' TOK��$��3��$
��Left=�$��5,3,0,3q�(   Label_Context$( 得意先指定��)   Label_Width$	) 100Q�*   DataAccessName$* M01_TOK��+   DataAccessMode$+ On=�,   LinkItem$	, 0,3��-   Label1Visibility$- 	Collapsed=�.   Label2Visibility$. 	Collapsed=�/   
Text1Width$/ 40Q�0   Text1MaxLength$0 3��1   Text1IMEType$	1 Off=�$2   Text1VerticalContentAlignment$2 Center=�3   
Label1Text$	3 －��4   
Text2Width$4 30Q�5   Text2MaxLength$5 2��6   Text2IMEType$	6 Off=�$7   Text2VerticalContentAlignment$7 Center=�8   
Label2Text$8  ��<  6KyoeiSystem.Framework.Windows.Controls.UcLabelComboBox  -   $��cmbDepositType��$��1��$��1��$
��Left=�$��5,3,0,0q�9   Label_Context$9 金種��:   Label_Width$	: 100Q�;   Combo_Width$	; 100Q�<   ComboListingParams$#< 随時,入金問合せ,金種���� -   $��	btnSearch��$��2��$��1��$��2��$��5,0,0,0q�$
��Left=�$��Stretch=�$��50��$��50��$��0q�.���� $)��#../Images/検索実行ボタン.png��$
��Fill=�� $��2��$��
10,10,10,5q�.+�/	 
 )GrapeCity.Windows.SpreadGrid.GcSpreadGrid	  -   $��
spGridList��$��2��= 	  AutoGenerateColumns= .  > 	  
LayoutMode$
> Cell=�? 	  ColumnDragMode$? Direct=�@ 	  HighlightHeader$
@ None=�A 	  CanUserSortColumnsA .  B 	  ColumnCount$B 8��C 	  RowCount$C 1��D 	  	ProtectedD . $	��380��E 	  DocumentUri$KE E/KyoeiSystem.Application.Windows;component/views/ZIJ/ZIJ03010G1.sgxml?�F 	  ItemsSourceF �� *SearchResult+G �� Mode$G TwoWay=�H �� UpdateSourceTrigger$H PropertyChanged=�I �� NotifyOnSourceUpdatedI . J �� NotifyOnTargetUpdatedJ . K 	  SelectionBorderUnfocusedK -
 
 'GrapeCity.Windows.SpreadGrid.BorderLine
  L 
  Color$L Orange��M 
  Style$M Thick=�N 	  SelectionBorderN 
  $L Orange��$M Thick=�O 	  ColumnsO ) 
 #GrapeCity.Windows.SpreadGrid.Column  P   Locked$
P TrueX�Q   	Focusable$
Q TrueX�R   HorizontalAlignment$R CenterX�S   	DataFieldS 3 
 -GrapeCity.Windows.SpreadGrid.BindingDataField  T   BindingT �� *	入金日+  $
P TrueX�$
Q TrueX�$R CenterX�S   T �� *伝票番号+  $
P TrueX�$
Q TrueX�S   T �� *得意先名+  $
P TrueX�$
Q TrueX�S   T �� *入金元販社名+  $
P TrueX�$
Q TrueX�S   T �� *	金種名+  $
P TrueX�$
Q TrueX�$R CenterX�S   T �� *期日+  $
P TrueX�$R RightX�U   CellTypeU 1 
 +GrapeCity.Windows.SpreadGrid.NumberCellType  V   MaxValue$V 	999999999~�W   MinValue$W 
-999999999~�X   SpinButtonVisibility$X NotShow=�Y   FieldSetF 
 @GrapeCity.Windows.SpreadGrid.Editors.NumberFieldSetTypeConverter$Y ###,###,##0,,,-, S   T �� *金額+  $
P TrueX�U / 
 )GrapeCity.Windows.SpreadGrid.TextCellType  Z   	MaxLength$Z 30��[   MaxLengthUnit$
[ Byte=�\   ImeState$\ On=�S   T �� *摘要+�� $��3��
���   �$��1q�$��0,5,0,50q�._��  �� $��30 ��� $��30 � �� $��* ��� $��* ��� $��* ��� $��* ��� $��* �.+�  -   $��sumCash��$��0��$��0��$ 現金合計��$  90Q�]   cTextAlignment$] Right=� ^   cVerticalContentAlignment$^ Center=�_   cIsReadOnly_ . `   cAcceptsTab` .    -   $��sumTransfer��$��0��$��1��$ 振込合計��$  90Q�$] Right=�$^ Center=�_ . ` .    -   $��sumCheck��$��0��$��2��$ 小切手合計��$  90Q�$] Right=�$^ Center=�_ . ` .    -   $��sumPromissory��$��0��$��3��$ 手形合計��$  90Q�$] Right=�$^ Center=�_ . ` .    -   $��	sumOffset��$��1��$��0��$ 相殺合計��$  90Q�$] Right=�$^ Center=�_ . ` .    -   $��sumAjustment��$��1��$��1��$ 調整合計��$  90Q�$] Right=�$^ Center=�_ . ` .    -   $��sumOther��$��1��$��2��$ その他合計��$  90Q�$] Right=�$^ Center=�_ . ` .    -   $��sumTotal��$��1��$��4��$ 入金合計��$  90Q�$] Right=�$^ Center=�_ . ` .  