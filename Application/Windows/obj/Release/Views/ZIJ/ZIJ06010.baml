   M S B A M L   `   `   `  ���� X  TKyoeiSystem.Framework.Windows, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null�[clr-namespace:KyoeiSystem.Framework.Windows.ViewBase;assembly=KyoeiSystem.Framework.Windows&KyoeiSystem.Framework.Windows.ViewBase  �[clr-namespace:KyoeiSystem.Framework.Windows.ViewBase;assembly=KyoeiSystem.Framework.Windows&KyoeiSystem.Framework.Windows.ViewBase  a ]KyoeiSystem.Framework.Windows.Controls, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null�dclr-namespace:KyoeiSystem.Framework.Windows.Controls;assembly=KyoeiSystem.Framework.Windows.Controls&KyoeiSystem.Framework.Windows.Controls Z VKyoeiSystem.Application.Windows, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]3clr-namespace:KyoeiSystem.Application.Windows.Views%KyoeiSystem.Application.Windows.Views 4   .KyoeiSystem.Application.Windows.Views.ZIJ06010   e aGrapeCity.WPF.SpreadGrid, Version=1.0.2015.1109, Culture=neutral, PublicKeyToken=5c5c8ff7a6858ccc=sg4http://schemas.grapecity.com/windows/spreadgrid/2012  dfw[clr-namespace:KyoeiSystem.Framework.Windows.ViewBase;assembly=KyoeiSystem.Framework.Windows  \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      R	 NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml 	    Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006 	    @d2http://schemas.microsoft.com/expression/blend/2008 	    jvm[clr-namespace:KyoeiSystem.Framework.Windows.ViewBase;assembly=KyoeiSystem.Framework.Windows 	    uctlsdclr-namespace:KyoeiSystem.Framework.Windows.Controls;assembly=KyoeiSystem.Framework.Windows.Controls 	    Buc3clr-namespace:KyoeiSystem.Application.Windows.Views 	    -     � WindowStyle$  SingleBorderWindow=� � AllowsTransparency .   � WindowStartupLocation$ CenterOwner=� � Language$ ja-JP�$	��728��$
��1024��$	��768��$
��1024�� � Title$2 ,商品移動/振替入力問合せ(ZIJ06010)��.���  � RowDefinitions �� $
��auto ��� $
��auto ��� $��* ��� $
��auto �.+�+  %System.Windows.Controls.Ribbon.Ribbon  $��Stretch=�$��Stretch=�.@�.  (System.Windows.Controls.Ribbon.RibbonTab  $��	ホーム��   KeyTip$ h��$��75��.@�0  *System.Windows.Controls.Ribbon.RibbonGroup  $��90��  $��F1��   KeyTip$ MASTER��   RibbonFunctionBackground#��[  .@�1  +System.Windows.Controls.Ribbon.RibbonButton  -      LargeImageSource$% ../Images/15マスタ照会.png��$��0,0,0,0q�	   ToolTipTitle$	 マスタ照会(F1)��
   ToolTipDescription$3
 -入力済みのデータを検索します。��   ToolTipImageSource$% ../Images/15マスタ照会.png��   Label$ ﾏｽﾀ照会��   KeyTip$ F1��$��50��  $��F2��$ MASTER��#��[  .@�  -   $y�Hidden=�$% ../Images/16マスタ入力.png��$��0,0,0,0q�$ ﾏｽﾀ入力��$ F2��$��50��$	 マスタ入力(F2)��$-
 'マスタの入力画面を開きます��$% ../Images/16マスタ入力.png��  $��F3��$ MASTER��#��[  .@�  -   $ ../Images/22リスト.png��$ F3��$y�Hidden=�$ 経費追加��$	 経費行追加(F3)��$'
 !経費欄に１行追加します��$ ../Images/22リスト.png��  $��F4��$ MASTER��#��[  .@�  $y�Hidden=�$  ��$	  ��$
  ��  $��F5��$ MASTER��#��[  .@�  -   $ ../Images/19CSV出力.png��$ 	CSV出力��$ F5��$��50��  $��F6��$ MASTER��#��[  .@�  $y�Hidden=�$  ��$	  ��$
  ��  $��F7��$ MASTER��#��[  .@�  -   $ ../Images/23印刷.png��$ F7��$��50��$y�Hidden=�$ 印刷��$	 
印刷(F7)��$
 印刷します��$ ../Images/23印刷.png��  $��F8��$ MASTER��#��[  .@�  -   $ ../Images/23印刷.png��$ F8��$��50��$ 印刷��$	 
印刷(F8)��$
 印刷します��$ ../Images/23印刷.png��  $��F9��$ MASTER��#��[  .@�  -   $y�Hidden=�$ ../Images/24登録.png��$ F9��$��50��$ 登録��$	 データ登録(F9)��$-
 '入力データの登録を行います��$ ../Images/24登録.png��  $	��F10��$ MASTER��#��[  .@�  -	   $y�Hidden=�$ ../Images/26クリア.png��$	 F10��$��50��$ 入力取消��$	 入力取消(F10)��$'
 !入力の取消しを行います��$ ../Images/26クリア.png��  $	��F11��$ MASTER��#��[  .@�  -
   $ ../Images/27終了.png��$	 F11��$��50��$ 終了��$	 終了(F11)��$
 終了します��$ ../Images/27終了.png��  $	��F12��$ MASTER��#��[  .@�  -   $y�Hidden=�$ ../Images/28削除.png��$	 F12��$��50��$ 削除��$	 削除(F12)��$-
 '表示中のデータを削除します��$ ../Images/28削除.png��  $��	ヘルプ��
�������.@�  $��サポート��.@�  -   $% ../Images/30マニュアル.png��$ MANUAL��$ ﾏﾆｭｱﾙ��$	 取扱説明書��$*
 $取り扱い説明書を開きます��  -   $( "../Images/29遠隔サポート.png��$ SUPPORT��$ お問合せ��$	 お問合せ��$<
 6Web上の｢お問合せ｣ページに遷移します��  -   $ ../Images/QandA.png��$	 FAQ��$ QandA��$	 QandA��$E
 ?Web上の｢よくあるご質問｣ページに遷移します��  -   $ ../Images/KyoueiIcon.bmp��$ KYOEI��$ 共栄ｼｽﾃﾑ��$	 共栄システムHP��$<
 6共栄システムのホームページを開きます��� $��0��$��Bottom=� �� $��74 � � ColumnDefinitions �� $��90 ��� $��* �.+�]� $��0��$��0��$��Stretch=�$��2q����� *MaintenanceModeBackground+.+��� $��メイリオ)�$��24(�$��Medium"�$��Stretch=�$��Center=�$��Center=�$��Center=����� *MaintenanceMode+���� *MaintenanceModeForeground+�� $��0��$��1��$��Bottom=�
��� ���
���  ��$��2,0,2,0q����� *ErrorMessage+y��� *ErrorMessageVisibility+� -   $��MainForm��$��1��
������� �� $
��auto ��� $
��auto ��� $	��300 �.+��� -   $��Header��$��0��
���   �$��10,10,62,10q� �� Padding$ 0,0,0,2q�$��1q�._�� $��5,2,0,2q� �� $��30 ��� $��30 ��� $��30 � �� $
��auto ��� $	��350 ��� $	��350 �.+�4  .KyoeiSystem.Framework.Windows.Controls.UcLabel     cContent$ 条件指定��$��0��$��0��$	��100��$
��Left=�$��3,2,0,0q�?  9KyoeiSystem.Framework.Windows.Controls.UcLabelTwinTextBox  -   $��txtMoveDatePriod��$��0��$��1��$
��Left=�$��3,2,0,0q�   
IsRequired .    Label_Context$ 	移動日��   Label_Width$	 100Q�   Label1Visibility$ 	Collapsed=�   Label2Visibility$ 	Collapsed=�   
Text1Width$ 90Q�   Text1MaxLength$ 10��   	Text1Mask$ 
yyyy/MM/dd��   Text1ValidationType$
 Date=�   Text1IMEType$	 Off=�&   Text1HorizontalContentAlignment$ Center=�$   Text1VerticalContentAlignment$ Center=�   
Label1Text$	 ～��   
Text2Width$ 90Q�   Text2MaxLength$ 10��    	Text2Mask$  
yyyy/MM/dd��!   Text2ValidationType$
! Date=�"   Text2IMEType$	" Off=�&#   Text2HorizontalContentAlignment$# Center=�$$   Text2VerticalContentAlignment$$ Center=�  -   $��
txtProduct��$��1��$��1��$
��Left=�$��3,2,0,0q�%   DataAccessName$% 	M09_MYHIN��&   DataAccessMode$& On=�$ 商品コード指定��$	 100Q�$ 	Collapsed=�$ 	Collapsed=�$ 90Q�$	 Off=�$ 12��$ Center=�$	 200Q�'   Text2IsReadOnly' . (   Text2AcceptsTab( .  )   Text2Background
) ���� *   Text2BorderThickness$* 0q�$$ Center=�  -   $��txtConsignor��$��2��$��1��$
��Left=�$��3,2,0,0q�$% M22_SOUK��$& On=�$ 出荷元倉庫��$	 100Q�$ 	Collapsed=�$ 	Collapsed=�$ 40Q�$ 3��$	 Off=�$ Integer=�$ Center=�$	 200Q�' . $* 0q�
) ���� $$ Center=�  -   $��txtDistination��$��2��$��2��$
��Left=�$��3,2,0,0q�$% M22_SOUK��$& On=�$ 出荷先倉庫��$	 100Q�$ 	Collapsed=�$ 	Collapsed=�$ 40Q�$ 3��$	 Off=�$ Integer=�$ Center=�$	 200Q�' . $* 0q�
) ���� $$ Center=��� -   $��	btnSearch��$��0��$��2��$��2��$��0,0,0,0q�$��50��$��50��$��Stretch=�$��0q�.���� $)��#../Images/検索実行ボタン.png��$
��Fill=�� -   $��
DetailList��$��1��$��10,5,0,0q�.+�/  )GrapeCity.Windows.SpreadGrid.GcSpreadGrid  -   $��
spGridList��+   AutoGenerateColumns+ .  ,   HighlightHeader$
, None=�-   
LayoutMode$- Template=�.   OperationMode$. Normal=�$
��Left=�/   	Protected/ . 0   CanUserSortColumns0 .  1   ColumnDragMode$1 Direct=�$	��450��$
��1020��2   DocumentUri$K2 E/KyoeiSystem.Application.Windows;component/views/ZIJ/ZIJ06010G1.sgxml?�3   ItemsSource3 �� *SearchResult+4 �� Mode$4 TwoWay=�5 �� UpdateSourceTrigger$5 PropertyChanged=�6 �� NotifyOnSourceUpdated6 . 7 �� NotifyOnTargetUpdated7 . 8   SelectionBorderUnfocused8 -  'GrapeCity.Windows.SpreadGrid.BorderLine  9   Color$9 Orange��:   Style$: Thick=�;   SelectionBorder;   $9 Orange��$: Thick=�<   Columns< )	  #GrapeCity.Windows.SpreadGrid.Column	  = 	  Locked$
= TrueX�> 	  HorizontalAlignment$> CenterX�? 	  	DataField? 3
  -GrapeCity.Windows.SpreadGrid.BindingDataField
  @ 
  Binding@ �� *	移動日+A �� StringFormat$A {0:yyyy/MM/dd}��	  $
= TrueX�$> CenterX�? 
  @ �� *伝票番号+	  $
= TrueX�? 
  @ �� *	自社名+	  $
= TrueX�? 
  @ �� *移動区分+	  $
= TrueX�? 
  @ �� *移動元倉庫+	  $
= TrueX�? 
  @ �� *移動先倉庫+	  $
= TrueX�$> CenterX�? 
  @ �� *品番コード+	  $
= TrueX�$> CenterX�? 
  @ �� *自社品番+	  $
= TrueX�? 
  @ �� *	商品名+	  $
= TrueX�? 
  @ �� *賞味期限+$A {0:yyyy/MM/dd}��	  $
= TrueX�B 	  CellTypeB 1  +GrapeCity.Windows.SpreadGrid.NumberCellType  C   MaxValue$C 99999999~�D   MinValue$D 	-99999999~�E   SpinButtonVisibility$E NotShow=�? 
  @ �� *数量+	  $
= TrueX�F 	  	Focusable$F FalseX�? 
  @ �� *単位+