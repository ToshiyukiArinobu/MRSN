   M S B A M L   `   `   `  ���� X  TKyoeiSystem.Framework.Windows, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null�[clr-namespace:KyoeiSystem.Framework.Windows.ViewBase;assembly=KyoeiSystem.Framework.Windows&KyoeiSystem.Framework.Windows.ViewBase  �[clr-namespace:KyoeiSystem.Framework.Windows.ViewBase;assembly=KyoeiSystem.Framework.Windows&KyoeiSystem.Framework.Windows.ViewBase  a ]KyoeiSystem.Framework.Windows.Controls, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null�dclr-namespace:KyoeiSystem.Framework.Windows.Controls;assembly=KyoeiSystem.Framework.Windows.Controls&KyoeiSystem.Framework.Windows.Controls Z VKyoeiSystem.Application.Windows, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]3clr-namespace:KyoeiSystem.Application.Windows.Views%KyoeiSystem.Application.Windows.Views 4   .KyoeiSystem.Application.Windows.Views.ZIJ07010   e aGrapeCity.WPF.SpreadGrid, Version=1.0.2015.1109, Culture=neutral, PublicKeyToken=5c5c8ff7a6858ccc=sg4http://schemas.grapecity.com/windows/spreadgrid/2012  dfw[clr-namespace:KyoeiSystem.Framework.Windows.ViewBase;assembly=KyoeiSystem.Framework.Windows  \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      R	 NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml 	    Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006 	    @d2http://schemas.microsoft.com/expression/blend/2008 	    jvm[clr-namespace:KyoeiSystem.Framework.Windows.ViewBase;assembly=KyoeiSystem.Framework.Windows 	    uctlsdclr-namespace:KyoeiSystem.Framework.Windows.Controls;assembly=KyoeiSystem.Framework.Windows.Controls 	    Buc3clr-namespace:KyoeiSystem.Application.Windows.Views 	    -     � WindowStyle$  SingleBorderWindow=� � AllowsTransparency .   � WindowStartupLocation$ CenterOwner=� � Language$ ja-JP�$	��728��$
��1024��$	��768��$
��1024�� � Title$( "商品入出荷問合せ(ZIJ07010)��.���  � RowDefinitions �� $
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
���   �$��10,10,62,10q� �� Padding$ 0,0,0,2q�$��1q�._�� $��5,2,0,2q� �� $��30 ��� $��30 � �� $
��auto ��� $	��350 ��� $
��auto �.+�4  .KyoeiSystem.Framework.Windows.Controls.UcLabel     cContent$ 条件指定��$��0��$��0��$	��100��$
��Left=�$��3,2,0,0q�?  9KyoeiSystem.Framework.Windows.Controls.UcLabelTwinTextBox  -   $��txtMoveDatePriod��$��0��$��1��$
��Left=�$��3,2,0,0q�   
IsRequired .    Label_Context$ 入出荷日��   Label_Width$	 100Q�   Label1Visibility$ 	Collapsed=�   Label2Visibility$ 	Collapsed=�   
Text1Width$ 90Q�   Text1MaxLength$ 10��   	Text1Mask$ 
yyyy/MM/dd��   Text1ValidationType$
 Date=�   Text1IMEType$	 Off=�&   Text1HorizontalContentAlignment$ Center=�$   Text1VerticalContentAlignment$ Center=�   
Label1Text$	 ～��   
Text2Width$ 90Q�   Text2MaxLength$ 10��    	Text2Mask$  
yyyy/MM/dd��!   Text2ValidationType$
! Date=�"   Text2IMEType$	" Off=�&#   Text2HorizontalContentAlignment$# Center=�$$   Text2VerticalContentAlignment$$ Center=�<  6KyoeiSystem.Framework.Windows.Controls.UcLabelComboBox  -   $��
cmbInbound��$��0��$��2��$
��Left=�$��3,2,0,0q�%   Label_Context$% 入出荷区分��&   Label_Width$	& 100Q�'   ComboListingParams$5' /随時,商品入出荷問合せ,入出庫区分��(   Combo_Width$( 90Q�  -   $��txtConsignor��$��1��$��1��$
��Left=�$��3,2,0,0q�)   DataAccessName$) M22_SOUK��*   DataAccessMode$* On=�$ 倉庫指定��$	 100Q�$ 	Collapsed=�$ 	Collapsed=�$ 40Q�$ 3��$	 Off=�$ Integer=�$ Center=�$	 200Q�+   Text2IsReadOnly+ . ,   Text2BorderThickness$, 0q�-   Text2Background
- ���� $$ Center=�  -   $��
txtProduct��$��1��$��2��$
��Left=�$��3,2,0,0q�$) 	M09_MYHIN��$* On=�$ 商品コード指定��$	 100Q�$ 	Collapsed=�$ 	Collapsed=�$ 90Q�$	 Off=�$ 12��$ Center=�$	 200Q�+ . .   Text2AcceptsTab. .  
- ���� $, 0q�$$ Center=��� -   $��	btnSearch��$��0��$��2��$��2��$��	400,0,0,0q�$��50��$��50��$��Stretch=�$��0q�.���� $)��#../Images/検索実行ボタン.png��$
��Fill=�� -   $��
DetailList��$��1��$��10,5,0,0q�.+�/  )GrapeCity.Windows.SpreadGrid.GcSpreadGrid  -   $��
spGridList��/   AutoGenerateColumns/ .  0   HighlightHeader$
0 None=�1   
LayoutMode$
1 Cell=�2   OperationMode$2 Normal=�$
��Left=�3   	Protected3 . 4   CanUserSortColumns4 .  5   ColumnDragMode$5 Direct=�$	��450��$
��1020��6   DocumentUri$K6 E/KyoeiSystem.Application.Windows;component/views/ZIJ/ZIJ07010G1.sgxml?�7   ItemsSource7 �� *SearchResult+8 �� Mode$8 TwoWay=�9 �� UpdateSourceTrigger$9 PropertyChanged=�: �� NotifyOnSourceUpdated: . ; �� NotifyOnTargetUpdated; . <   SelectionBorderUnfocused< -	  'GrapeCity.Windows.SpreadGrid.BorderLine	  = 	  Color$= Orange��> 	  Style$> Thick=�?   SelectionBorder? 	  $= Orange��$> Thick=�@   Columns@ )
  #GrapeCity.Windows.SpreadGrid.Column
  A 
  Locked$
A TrueX�B 
  	Focusable$B FalseX�C 
  	IsVisibleC .  D 
  CellTypeD 1  +GrapeCity.Windows.SpreadGrid.NumberCellType  E   MaxValue$E 99999999~�F   MinValue$F 	-99999999~�G   SpinButtonVisibility$G NotShow=�H 
  	DataFieldH 3  -GrapeCity.Windows.SpreadGrid.BindingDataField  I   BindingI �� *SEQ+
  $
A TrueX�J 
  HorizontalAlignment$J CenterX�H   I �� *入出庫日+
  $
A TrueX�$J CenterX�H   I �� *入出庫区分+
  $
A TrueX�H   I �� *倉庫+
  $
A TrueX�$J CenterX�H   I �� *品番コード+
  $
A TrueX�$J CenterX�H   I �� *自社品番+
  $
A TrueX�H   I �� *自社品名+
  $
A TrueX�H   I �� *	自社色+
  $
A TrueX�$J CenterX�H   I �� *賞味期限+K �� StringFormat$K {0:yyyy/MM/dd}��
  $
A TrueX�D   $E 99999999~�$F 	-99999999~�$G NotShow=�H   I �� *数量+
  $
A TrueX�$B FalseX�H   I �� *単位+
  $
A TrueX�$J CenterX�C .  H   I �� *伝票番号+
  $
A TrueX�$J CenterX�H   I �� *作成機能+