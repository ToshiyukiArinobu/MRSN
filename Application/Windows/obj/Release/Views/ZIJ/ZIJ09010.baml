   M S B A M L   `   `   `  ���� X  TKyoeiSystem.Framework.Windows, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null�[clr-namespace:KyoeiSystem.Framework.Windows.ViewBase;assembly=KyoeiSystem.Framework.Windows&KyoeiSystem.Framework.Windows.ViewBase  �[clr-namespace:KyoeiSystem.Framework.Windows.ViewBase;assembly=KyoeiSystem.Framework.Windows&KyoeiSystem.Framework.Windows.ViewBase  a ]KyoeiSystem.Framework.Windows.Controls, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null�dclr-namespace:KyoeiSystem.Framework.Windows.Controls;assembly=KyoeiSystem.Framework.Windows.Controls&KyoeiSystem.Framework.Windows.Controls Z VKyoeiSystem.Application.Windows, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]3clr-namespace:KyoeiSystem.Application.Windows.Views%KyoeiSystem.Application.Windows.Views 4   .KyoeiSystem.Application.Windows.Views.ZIJ09010   e aGrapeCity.WPF.SpreadGrid, Version=1.0.2015.1109, Culture=neutral, PublicKeyToken=5c5c8ff7a6858ccc=sg4http://schemas.grapecity.com/windows/spreadgrid/2012  dfw[clr-namespace:KyoeiSystem.Framework.Windows.ViewBase;assembly=KyoeiSystem.Framework.Windows  \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      R	 NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml 	    Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006 	    @d2http://schemas.microsoft.com/expression/blend/2008 	    jvm[clr-namespace:KyoeiSystem.Framework.Windows.ViewBase;assembly=KyoeiSystem.Framework.Windows 	    uctlsdclr-namespace:KyoeiSystem.Framework.Windows.Controls;assembly=KyoeiSystem.Framework.Windows.Controls 	    Buc3clr-namespace:KyoeiSystem.Application.Windows.Views 	    -     � WindowStyle$  SingleBorderWindow=� � AllowsTransparency .   � WindowStartupLocation$ CenterOwner=� � Language$ ja-JP�$	��728��$
��1024��$	��768��$
��1024�� � Left$ 0��
 � Top$ 0�� � Title$% 商品在庫問合せ(ZIJ09010)��.���  � RowDefinitions �� $
��auto ��� $
��auto ��� $��* ��� $
��auto �.+�+  %System.Windows.Controls.Ribbon.Ribbon  $��Stretch=�$��Stretch=�.@�.  (System.Windows.Controls.Ribbon.RibbonTab  $��	ホーム��   KeyTip$ h��$��75��.@�0  *System.Windows.Controls.Ribbon.RibbonGroup  $��90��  $��F1��	   KeyTip$	 MASTER��   RibbonFunctionBackground#��[  .@�1  +System.Windows.Controls.Ribbon.RibbonButton  -   
   LargeImageSource$%
 ../Images/15マスタ照会.png��$��0,0,0,0q�   ToolTipTitle$ マスタ照会(F1)��   ToolTipDescription$3 -入力済みのデータを検索します。��   ToolTipImageSource$% ../Images/15マスタ照会.png��   Label$ ﾏｽﾀ照会��   KeyTip$ F1��$��50��  $��F2��$	 MASTER��#��[  .@�  -   $y�Hidden=�$%
 ../Images/16マスタ入力.png��$��0,0,0,0q�$ ﾏｽﾀ入力��$ F2��$��50��$ マスタ入力(F2)��$- 'マスタの入力画面を開きます��$% ../Images/16マスタ入力.png��  $��F3��$	 MASTER��#��[  .@�  -   $
 ../Images/22リスト.png��$ F3��$y�Hidden=�$ 経費追加��$ 経費行追加(F3)��$' !経費欄に１行追加します��$ ../Images/22リスト.png��  $��F4��$	 MASTER��#��[  .@�  $y�Hidden=�$  ��$  ��$  ��  $��F5��$	 MASTER��#��[  .@�  -   $
 ../Images/19CSV出力.png��$ 	CSV出力��$ F5��$��50��  $��F6��$	 MASTER��#��[  .@�  $y�Hidden=�$  ��$  ��$  ��  $��F7��$	 MASTER��#��[  .@�  -   $
 ../Images/23印刷.png��$ F7��$��50��$y�Hidden=�$ 印刷��$ 
印刷(F7)��$ 印刷します��$ ../Images/23印刷.png��  $��F8��$	 MASTER��#��[  .@�  -   $
 ../Images/23印刷.png��$ F8��$��50��$ 印刷��$ 
印刷(F8)��$ 印刷します��$ ../Images/23印刷.png��  $��F9��$	 MASTER��#��[  .@�  -   $y�Hidden=�$
 ../Images/24登録.png��$ F9��$��50��$ 登録��$ データ登録(F9)��$- '入力データの登録を行います��$ ../Images/24登録.png��  $	��F10��$	 MASTER��#��[  .@�  -	   $y�Hidden=�$
 ../Images/26クリア.png��$	 F10��$��50��$ 入力取消��$ 入力取消(F10)��$' !入力の取消しを行います��$ ../Images/26クリア.png��  $	��F11��$	 MASTER��#��[  .@�  -
   $
 ../Images/27終了.png��$	 F11��$��50��$ 終了��$ 終了(F11)��$ 終了します��$ ../Images/27終了.png��  $	��F12��$	 MASTER��#��[  .@�  -   $y�Hidden=�$
 ../Images/28削除.png��$	 F12��$��50��$ 削除��$ 削除(F12)��$- '表示中のデータを削除します��$ ../Images/28削除.png��  $��	ヘルプ��
�������.@�  $��サポート��.@�  -   $%
 ../Images/30マニュアル.png��$ MANUAL��$ ﾏﾆｭｱﾙ��$ 取扱説明書��$* $取り扱い説明書を開きます��  -   $(
 "../Images/29遠隔サポート.png��$ SUPPORT��$ お問合せ��$ お問合せ��$< 6Web上の｢お問合せ｣ページに遷移します��  -   $
 ../Images/QandA.png��$	 FAQ��$ QandA��$ QandA��$E ?Web上の｢よくあるご質問｣ページに遷移します��  -   $
 ../Images/KyoueiIcon.bmp��$ KYOEI��$ 共栄ｼｽﾃﾑ��$ 共栄システムHP��$< 6共栄システムのホームページを開きます��� $��0��$��Bottom=� �� $��74 � � ColumnDefinitions �� $��90 ��� $��* �.+�]� $��0��$��0��$��Stretch=�$��2q����� *MaintenanceModeBackground+.+��� $��メイリオ)�$��24(�$��Medium"�$��Stretch=�$��Center=�$��Center=�$��Center=����� *MaintenanceMode+���� *MaintenanceModeForeground+�� $��0��$��1��$��Bottom=�
��� ���
���  ��$��2,0,2,0q����� *ErrorMessage+y��� *ErrorMessageVisibility+� -   $��MainForm��$��1��
������� �� $
��auto ��� $
��auto ��� $	��300 �.+��� -   $��Header��$��0��
���   �$��10q�$��1q�._��  �� $
��auto ��� $
��auto ��� $��* �.+�� $��0��.+�� $��0�� �� $��5 ��� $	��900 ��� $
��auto � �� $��30 ��� $
��auto ��� $
��auto �.+�?  9KyoeiSystem.Framework.Windows.Controls.UcLabelTwinTextBox  -   $��	myCompany��$��0��$��1��   DataAccessName$ M70_JIS��   DataAccessMode$ On=�   
IsRequired . $��0,2,0,0q�   Label_Context$ 自社コード��   Label_Width$	 100Q�$
��Left=�   Label1Visibility$ 	Collapsed=�   Label2Visibility$ 	Collapsed=�   
Text1Width$ 40Q�   Text1MaxLength$ 3��   Text1IMEType$	 Off=�   Text1ValidationType$ Integer=�$   Text1VerticalContentAlignment$ Center=�   
Text2Width$	 300Q�   Text2IsReadOnly .    Text2AcceptsTab .      Text2Background
  ���� !   Text2BorderThickness$! 0q�$"   Text2VerticalContentAlignment$" Center=�� $��1��$��1��$��0,2,0,2q� �� $��30 ��� $��30 ��� $��30 � �� $
��auto ��� $	��350 ��� $	��350 �.+�4  .KyoeiSystem.Framework.Windows.Controls.UcLabel  #   cContent$# 商品指定��$	��100��  -   $��Product��$��1��$ 	M09_MYHIN��$ On=�$
��Left=�$��3,0,0,0q�$ 商品コード指定��$	 100Q�$ 	Collapsed=�$ 	Collapsed=�$ 80Q�$	 Off=�$ 12��$ Center=�$	 200Q� .  .  
  ���� $! 0q�$" Center=�<  6KyoeiSystem.Framework.Windows.Controls.UcLabelComboBox  -   $��cmbItemType��$��0��$��2��$
��Left=�$��3,2,0,0q�$   Label_Context$$ 商品分類��%   Label_Width$	% 100Q�&   Combo_Width$	& 100Q�'   ComboListingParams$5' /随時,商品在庫問い合わせ,商品分類��;  5KyoeiSystem.Framework.Windows.Controls.UcLabelTextBox  -   $��ProductName��$��1��$��1��$��2��$
��Left=�$��3,2,0,0q�(   Label_Context$( 商品名指定��)   Label_Width$	) 100Q�*   cWidth$	* 350Q�+   
cMaxLength$+ 50�� ,   cVerticalContentAlignment$, Center=�  -   $��Brand��$��2��$��1��$ 	M14_BRAND��$ On=�$
��Left=�$��3,2,0,0q�$ 	Collapsed=�$ 	Collapsed=�$ ブランド��$	 100Q�$ 50Q�$ 4��$	 Off=�$ Center=�$	 240Q� .  .  
  ���� $! 0q�$" Center=�  -   $��Series��$��2��$��2��$ 
M15_SERIES��$ On=�$
��Left=�$��3,2,0,0q�$ 	Collapsed=�$ 	Collapsed=�$ シリーズ��$	 100Q�$ 50Q�$ 4��$	 Off=�$ Center=�$	 240Q� .  .  
  ���� $! 0q�$" Center=��� -   $��	btnSearch��$��0��$��2��$��2��$��	200,0,0,0q�$��50��$��50��$��Stretch=�$��0q�.���� $)��#../Images/検索実行ボタン.png��$
��Fill=�� -   $��
DetailList��$��1��$��10,5,0,0q�.+�/	  )GrapeCity.Windows.SpreadGrid.GcSpreadGrid	  -   $��
spGridList��- 	  AutoGenerateColumns- .  . 	  HighlightHeader$
. None=�/ 	  
LayoutMode$/ Template=�0 	  OperationMode$0 Normal=�$
��Left=�1 	  	Protected1 . 2 	  CanUserSortColumns2 .  3 	  ColumnDragMode$3 Direct=�$	��450��$
��1020��4 	  DocumentUri$K4 E/KyoeiSystem.Application.Windows;component/views/ZIJ/ZIJ09010G1.sgxml?�5 	  ItemsSource5 �� *SearchResult+6 �� Mode$6 TwoWay=�7 �� UpdateSourceTrigger$7 PropertyChanged=�8 �� NotifyOnSourceUpdated8 . 9 �� NotifyOnTargetUpdated9 . : 	  SelectionBorderUnfocused: -
  'GrapeCity.Windows.SpreadGrid.BorderLine
  ; 
  Color$; Orange��< 
  Style$< Thick=�= 	  SelectionBorder= 
  $; Orange��$< Thick=�> 	  Columns> )  #GrapeCity.Windows.SpreadGrid.Column  ?   Locked$
? TrueX�@   	DataField@ 3  -GrapeCity.Windows.SpreadGrid.BindingDataField  A   BindingA �� *倉庫コード+  $
? TrueX�@   A �� *倉庫名称+  $
? TrueX�@   A �� *品番コード+  $
? TrueX�@   A �� *自社品番コード+  $
? TrueX�@   A �� *自社色コード+  $
? TrueX�@   A �� *品番名称+  $
? TrueX�@   A �� *	色名称+  $
? TrueX�B   VerticalAlignment$B CenterX�@   A �� *賞味期限+  $
? TrueX�C   CellTypeC 1  +GrapeCity.Windows.SpreadGrid.NumberCellType  D   MaxValue$D 99999999~�E   MinValue$E 	-99999999~�F   SpinButtonVisibility$F NotShow=�@   A �� *在庫数量+  $
? TrueX�G   	Focusable$G FalseX�@   A �� *単位+