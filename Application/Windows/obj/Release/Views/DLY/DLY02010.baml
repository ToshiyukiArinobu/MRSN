   M S B A M L   `   `   `  ���� X  TKyoeiSystem.Framework.Windows, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null�[clr-namespace:KyoeiSystem.Framework.Windows.ViewBase;assembly=KyoeiSystem.Framework.Windows&KyoeiSystem.Framework.Windows.ViewBase  �[clr-namespace:KyoeiSystem.Framework.Windows.ViewBase;assembly=KyoeiSystem.Framework.Windows&KyoeiSystem.Framework.Windows.ViewBase  a ]KyoeiSystem.Framework.Windows.Controls, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null�dclr-namespace:KyoeiSystem.Framework.Windows.Controls;assembly=KyoeiSystem.Framework.Windows.Controls&KyoeiSystem.Framework.Windows.Controls R NSystem.Data, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089>.clr-namespace:System.Data;assembly=System.DataSystem.Data # KyoeiSystem.Application.Windows4   .KyoeiSystem.Application.Windows.Views.DLY02010   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      lfw[clr-namespace:KyoeiSystem.Framework.Windows.ViewBase;assembly=KyoeiSystem.Framework.Windows      R	 NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml 	    Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006 	    @d2http://schemas.microsoft.com/expression/blend/2008 	    jvm[clr-namespace:KyoeiSystem.Framework.Windows.ViewBase;assembly=KyoeiSystem.Framework.Windows 	    uctlsdclr-namespace:KyoeiSystem.Framework.Windows.Controls;assembly=KyoeiSystem.Framework.Windows.Controls 	    e
 aGrapeCity.WPF.SpreadGrid, Version=1.0.2015.1109, Culture=neutral, PublicKeyToken=5c5c8ff7a6858ccc=sg4http://schemas.grapecity.com/windows/spreadgrid/2012 
 9Data.clr-namespace:System.Data;assembly=System.Data 
 -   $	��728��$
��1024��$	��768��$
��1024��  � Left$  0��
 � Top$ 0�� � WindowStartupLocation$ Manual=� � AllowsTransparency .   � Language$ ja-JP� � Title$ 揚り入力(DLY02010)�� � 	Resources    DataGridHeaderBackground �� EndPoint �� 
StartPoint	 � Color
 � Offset  DataGridSummaryBackground��%�   &	        &	 7     �� $	 0,1&�$	 0,0&�.��� $	 #3b5998��$
 0Q��� $	 0,1&�$	 0,0&�.��� $	 #f0e68c��$
 0Q�.���  � RowDefinitions �� $
��auto ��� $
��auto ��� $��* �.+�+  %System.Windows.Controls.Ribbon.Ribbon  $��Stretch=�$��Stretch=�.@�.  (System.Windows.Controls.Ribbon.RibbonTab  $��	ホーム��   KeyTip$ h��$��75��.@�0  *System.Windows.Controls.Ribbon.RibbonGroup  $��90��  $��F1��   KeyTip$ MASTER��  RibbonFunctionBackground#��[ .@�1  +System.Windows.Controls.Ribbon.RibbonButton  -      LargeImageSource$% ../Images/15マスタ照会.png��$��0,0,0,0q�   ToolTipTitle$ マスタ照会(F1)��   ToolTipDescription$3 -入力済みのデータを検索します。��   ToolTipImageSource$% ../Images/15マスタ照会.png��   Label$ ﾏｽﾀ照会��   KeyTip$ F1��$��50��  $��F2��$ MASTER��#��[ .@�  -   $% ../Images/16マスタ入力.png��$��0,0,0,0q�$ ﾏｽﾀ入力��$ F2��$��50��$ マスタ入力(F2)��$- 'マスタの入力画面を開きます��$% ../Images/16マスタ入力.png��  $��F3��$ MASTER��#��[ .@�  $y�Hidden=�$  ��$  ��$  ��  $��F4��$ MASTER��#��[ .@�  $y�Hidden=�$  ��$  ��$  ��  $��F5��$ MASTER��#��[ .@�  -   $ ../Images/行追加.png��$ F5��$��50��$ 	行追加��$ 行追加(F5)��$0 *リストに新規行を追加します。��$ ../Images/行追加.png��  $��F6��$ MASTER��#��[ .@�  -   $ ../Images/28削除.png��$ F6��$��50��$ 	行削除��$ 行削除(F6)��$0 *選択中のリスト行を削除します��$ ../Images/28削除.png��  $��F7��$ MASTER��#��[ .@�  $y�Hidden=�$  ��$  ��$  ��  $��F8��$ MASTER��#��[ .@�  $y�Hidden=�$  ��$  ��$  ��  $��F9��$ MASTER��#��[ .@�  -   $ ../Images/24登録.png��$ F9��$��50��$ 登録��$ データ登録(F9)��$- '入力データの登録を行います��$ ../Images/24登録.png��  $	��F10��$ MASTER��#��[ .@�  -   $ ../Images/26クリア.png��$	 F10��$��50��$ 入力取消��$ 入力取消(F10)��$' !入力の取消しを行います��$ ../Images/26クリア.png��  $	��F11��$ MASTER��#��[ .@�  -   $ ../Images/27終了.png��$	 F11��$��50��$ 終了��$ 終了(F11)��$ 終了します��$ ../Images/27終了.png��  $	��F12��$ MASTER��#��[ .@�  -	   $ ../Images/28削除.png��$	 F12��$��50��$ 削除��$ 削除(F12)��$- '表示中のデータを削除します��$ ../Images/28削除.png��  $��	ヘルプ��
�������.@�  $��サポート��.@�  -
   $% ../Images/30マニュアル.png��$ MANUAL��$ ﾏﾆｭｱﾙ��$ 取扱説明書��$* $取り扱い説明書を開きます��  -   $( "../Images/29遠隔サポート.png��$ SUPPORT��$ お問合せ��$ お問合せ��$< 6Web上の｢お問合せ｣ページに遷移します��  -   $ ../Images/QandA.png��$	 FAQ��$ QandA��$ QandA��$E ?Web上の｢よくあるご質問｣ページに遷移します��  -   $( "../Images/13共栄Webリンク.png��$ KYOEI��$ 共栄ｼｽﾃﾑ��$ 共栄システムHP��$< 6共栄システムのホームページを開きます��� $��0��$��Bottom=� �� $��74 � � ColumnDefinitions �� $��90 ��� $��* �.+�]� $��0��$��0��$��Stretch=�$��2q����� *MaintenanceModeBackground+.+��� $��メイリオ)�$��24(�$��Medium"�$��Stretch=�$��Center=�$��Center=�$��Center=����� *MaintenanceMode+���� *MaintenanceModeForeground+�� $��0��$��1��$��Bottom=�
��� ���
���  ��$��2,0,2,0q����� *ErrorMessage+y��� *ErrorMessageVisibility+� -   $��	grdHeader��$��1��
�������$��Stretch=�$��Stretch=� �� $
��auto ��� $
��auto ��� $
��auto ��� $
��auto ��� $
��auto ��� $
��auto ��� $��30 � �� $
��auto �.+�� $��0��$��2��$��2��$��	20,10,0,5q� �� $
��auto ��� $��10 ��� $
��auto ��� $��* � �� $	��200 ��� $
��auto ��� $
��auto �.+��� -    ��Name$ 	FuncTitle��$��揚り入力��$��0��$��0��$��16(�$��30,0,0,0q�?  9KyoeiSystem.Framework.Windows.Controls.UcLabelTwinTextBox  -     Name$ txt会社名��   Label_Context$ 	会社名��$��1��$	��350��   DataAccessName$ M70_JIS��   DataAccessMode$ On=�=  7KyoeiSystem.Framework.Windows.Controls.FrameworkControl   	IsKeyItem .    Label_Width$	 100Q�   Label1Visibility$ 	Collapsed=�   Label2Visibility$ 	Collapsed=�   
Text1Width$ 40Q�   Text1MaxLength$ 3��    Text1IMEType$	  Off=�!   Text1ValidationType$! Integer=�$"   Text1VerticalContentAlignment$" Center=�#   Text2BorderBrush
# ���� $   Text2Background
$ ���� %   Text2IsReadOnly% . $&   Text2VerticalContentAlignment$& Center=�<  6KyoeiSystem.Framework.Windows.Controls.UcLabelComboBox  -   '  Name$' cmb加工区分��$��0��$��2��(   Label_Context$( 加工区分��)   Label_Width$	) 100Q�$	��250��*   ComboListingParams$&*  日次,揚り入力,加工区分��+   SelectedValue+ �� *SearchHeader[加工区分]+, �� Mode$, TwoWay=�- �� NotifyOnSourceUpdated- . . �� NotifyOnTargetUpdated. . / �� UpdateSourceTrigger$/ PropertyChanged=�� -   0 �Name$0 gHeader��$��2��$��0��$��3��$��	150,0,0,0q� �� $��27 ��� $��27 � �� $��80 ��� $��80 ��� $	��250 ��� $	��200 ��� $
��auto �.+��� $��伝票番号��$��0��$��0��
�������#��[  1 �� TextAlignment$1 Center=�2 �� Padding$2 0,5,0,0q��� -   $��PrevSlip��$��0��$��0��3 K� ZIndex$3 1��$
��Left=�#��[  
�������$��0q�$	��◀��$��16��$��Center=�$y�Hidden=��� -   $��NextSlip��$��0��$��0��$3 1��$��Right=�#��[  
�������$��0q�$	��▶��$��16��$��Center=�$y�Hidden=�6  0KyoeiSystem.Framework.Windows.Controls.UcTextBox  -   4  Name$4 txt伝票番号��$��1��$��0��5   ImeType$	5 Off=�6   ValidationType$6 Integer=� . 7   
cMaxLength$7 7��8   cTextAlignment$8 Center=� 9   cVerticalContentAlignment$9 Center=�:   Text: �� *SearchHeader[伝票番号]+$, TwoWay=�- . . . $/ PropertyChanged=��� $��仕上り日��$��0��$��1��$1 Center=�$2 0,5,0,0q�
�������#��[  ;	  5KyoeiSystem.Framework.Windows.Controls.UcLabelTextBox	  -   $��txt仕上り日��$��1��$��1��; 	  
cMaxLength$; 10��< 	  cTextAlignment$< Center=� = 	  cVerticalContentAlignment$= Center=� .  > 	  Mask$> 
yyyy/MM/dd��? 	  cValidationType$
? Date=�@ 	  Label_Width$@ 0Q�$��-1,-1,-2,-2q�A 	  TextA �� B �� Path$ B SearchHeader[仕上り日]�$, TwoWay=�. . - . C �� StringFormat$C 
yyyy/MM/dd��$/ 	LostFocus=��� $��外注先名称��$��0��$��2��
�������#��[  $1 Center=�$2 0,5,0,0q�  -   $ txt外注先��$��1��$��2��$ M01_TOK��$	 Off=�D   LinkItem$	D 2,3��$
��Left=�$��2,0,0,0q� .  E   Label_Visibility$E 	Collapsed=�$ 	Collapsed=�$ 	Collapsed=�$ 40Q�$ 4��$	  Off=�$! Integer=�$" Center=�F   
Label1Text$	F －��G   
Text2Width$G 30Q�H   Text2MaxLength$H 2��I   Text2IMEType$	I Off=�J   Text2ValidationType$J Integer=�$& Center=�K   
Label2Text$K  ��L   Text1L �� *" SearchHeader[外注先コード]+$, TwoWay=�- . . . $/ PropertyChanged=�M   Text2M �� *SearchHeader[外注先枝番]+$, TwoWay=�- . . . $/ PropertyChanged=��� $��入荷先名称��$��0��$��3��
�������#��[  $1 Center=�$2 0,5,0,0q�  -   $ txt入荷先��$��1��$��3��$ M70_JIS��$ On=�$
��Left=�$��2,0,0,0q� .  $E 	Collapsed=�$ 	Collapsed=�$ 	Collapsed=�$ 30Q�$ 3��$	  Off=�$! Integer=�$" Center=�$	G 150Q�% . 
$ ���� 
# ���� $& Center=�$K  ��L �� *" SearchHeader[入荷先コード]+$, TwoWay=�- . . . $/ PropertyChanged=��� $��0��$��0��$��1,1,0,0q�
���   �$3 9���� $��1��$��0��$��1,1,0,0q�
���   �$3 9���� $��2��$��0��$��1,1,0,0q�
���   �$3 9���� $��3��$��0��$��1,1,1,0q�
���   �$3 9���� $��0��$��1��$��1,1,0,1q�
���   �$3 9���� $��1��$��1��$��1,1,0,1q�
���   �$3 9���� $��2��$��1��$��1,1,0,1q�
���   �$3 9���� $��3��$��1��$��1,1,1,1q�
���   �$3 9��/
 
 )GrapeCity.Windows.SpreadGrid.GcSpreadGrid
  -   $��gcSpreadGrid��$��3��$
��Left=�N 
  AutoFitType$N CellWithHeader=�O 
  
LayoutMode$
O Cell=�P 
  AutoGenerateColumnsP .  Q 
  HighlightHeader$
Q None=�R 
  RowCount$R 10��S 
  OperationMode$S Normal=�T 
  	ProtectedT . U 
  CanUserSortColumnsU .  V 
  ColumnDragMode$V Direct=�$	��150��$	��950��$	��150��$��80,5,0,0q�W 
  DocumentUri$KW E/KyoeiSystem.Application.Windows;component/views/DLY/DLY02010G1.sgxml?�X 
  ItemsSourceX �� *SearchDetail+$, TwoWay=�$/ PropertyChanged=�- . . . Y 
  SelectionBorderUnfocusedY - 
 'GrapeCity.Windows.SpreadGrid.BorderLine  Z   Color$Z Orange��[   Style$[ Thick=�\ 
  SelectionBorder\   $Z Orange��$[ Thick=�] 
  Columns] ) 
 #GrapeCity.Windows.SpreadGrid.Column  ^   Locked$^ FalseX�_   	Focusable$
_ TrueX�`   CanUserResize$` FalseX�a   CellTypea / 
 )GrapeCity.Windows.SpreadGrid.TextCellType  b   	MaxLength$b 12��c   MaxLengthUnit$
c Byte=�d   ImeState$	d Off=�e   	DataFielde 3 
 -GrapeCity.Windows.SpreadGrid.BindingDataField  f   Bindingf �� *自社品番+$, TwoWay=�$/ PropertyChanged=�- . . .   $
^ TrueX�$
_ TrueX�$` FalseX�a   3 
 -GrapeCity.Windows.SpreadGrid.EditBaseCellTypeg   
IsReadOnlyg . e   f �� *自社品名+$, TwoWay=�$/ PropertyChanged=�- . . .   $^ FalseX�$
_ TrueX�$` FalseX�a   $b 10��$
c Byte=�$	d Off=�e   f �� *賞味期限+h �� TargetNullValue$h  ��$C 
yyyy/MM/dd��$, TwoWay=�$/ PropertyChanged=�- . . .   i   HorizontalAlignment$i RightX�$^ FalseX�$
_ TrueX�$` FalseX�a 1 
 +GrapeCity.Windows.SpreadGrid.NumberCellType  j   MaxValue$j 	999999.99~�k   MinValue$k 
-999999.99~�l   SpinButtonVisibility$l NotShow=�m   FieldSetF 
 @GrapeCity.Windows.SpreadGrid.Editors.NumberFieldSetTypeConverter$m ###,##0.##,,,-, e   f �� *数量+$, TwoWay=�$/ PropertyChanged=�- . . .   $
^ TrueX�$_ FalseX�$` FalseX�a   $b 2��$
c Byte=�e   f �� *単位+$, TwoWay=�$/ PropertyChanged=�- . . .   $i RightX�$^ FalseX�$
_ TrueX�$` FalseX�a   $j 99999999.99~�$k -99999999.99~�$l NotShow=�$m ##,###,##0.##,,,-, e   f �� *単価+$, TwoWay=�$/ PropertyChanged=�- . . .   $i RightX�$
^ TrueX�$_ FalseX�$` FalseX�a   $j 99999999~�$k 	-99999999~�$l NotShow=�$m ###,###,##0,,,-, e   f �� *金額+$, TwoWay=�$/ PropertyChanged=�- . . .   $^ FalseX�$
_ TrueX�$` FalseX�a   $b 40��$
c Byte=�$d On=�e   f �� *摘要+$, TwoWay=�$/ PropertyChanged=�- . . .   $
^ TrueX�$_ FalseX�n   	IsVisiblen .  o   Width$o 0Q�a   g . e   f �� *品番コード+  $
^ TrueX�$_ FalseX�$o 0Q�a   g . e   f �� *消費税区分+  $
^ TrueX�$_ FalseX�$o 0Q�a   g . e   f �� *商品分類+� -   $0 
FooterArea��$��4��$��80,0,0,0q� p �� 
TargetTypeq �� Propertyr �� Value��%/   '��       �� "p ��.��� q � ��
r �   � �� $��30 ��� $��30 ��� $��30 ��� $��* � �� $	��477 ��� $��84 ��� $��82 ��� $
��auto �.+�4  .KyoeiSystem.Framework.Windows.Controls.UcLabel  $��備　　考��$��0��$��0��$1 Center=�$��0,6,0,0q�#��[   -   $ 	txt備考��$��1��$��0��$��2q�$ M11_TEK��$ On=�$E 	Collapsed=�$ 	Collapsed=�$ 	Collapsed=�$ 40Q�$ 5��$H 40��$I Native=�M �� *SearchHeader[備考]+$, TwoWay=�- . . .   $��最大10行伝票��$��2��$��0��$��Center=�  $��	小　計��$��0��$��1��$1 Center=�$��0,6,0,0q�#��[   $��	消費税��$��1��$��1��$1 Center=�$��0,6,0,0q�#��[   $��	総合計��$��2��$��1��$1 Center=�$��0,6,0,0q�#��[ �� -   s ��Name$s 	lbl小計��$��0��$��2��$��Right=�$��0,6,5,0q��� -   $s lbl消費税��$��1��$��2��$��Right=�$��0,6,5,0q��� -   $s lbl総合計��$��2��$��2��$��Right=�$��0,6,5,0q��� $��0��$��0��$��1,1,0,1q��� $��1��$��0��$��1,1,0,1q��� $��2��$��0��$��1,1,1,1q��� $��0��$��1��$��1,0,0,1q��� $��1��$��1��$��1,0,0,1q��� $��2��$��1��$��1,0,1,1q��� $��1��$��2��$��1,0,0,1q��� $��2��$��2��$��1,0,1,1q��� $��5��$��	0,1.5,0,0q�
�����w�$	��950��$
��Left=�$��20,5,0,0q��� $��構成品明細��$��5��$��2q�$��40,5,0,0q�$	��850��$	��230��$	��230��.��
  -   $��sgInnerDetail��$
��Left=�$N CellWithHeader=�$O Template=�P .  $
Q None=�$R 5��$S Normal=�t 
  SelectionUnit$	t Row=�T . U .  $V Direct=�$��5q�$KW E/KyoeiSystem.Application.Windows;component/views/DLY/DLY02010G2.sgxml?�X �� *InnerDetail+$, TwoWay=�$/ PropertyChanged=�- . . . Y   $Z Orange��$[ Thick=�\   $Z Orange��$[ Thick=�]   $
^ TrueX�$
_ TrueX�$` FalseX�a   $b 12��$
c Byte=�$	d Off=�e   f �� *自社品番+$, TwoWay=�$/ PropertyChanged=�- . . .   $
^ TrueX�$
_ TrueX�$` FalseX�a   $b 3��$
c Byte=�$	d Off=�e   f �� *	自社色+$, TwoWay=�$/ PropertyChanged=�- . . .   $
^ TrueX�$
_ TrueX�$` FalseX�a   g . e   f �� *自社品名+$, TwoWay=�$/ PropertyChanged=�- . . .   $
^ TrueX�$
_ TrueX�$` FalseX�a   g . e   f �� *自社色名+$, TwoWay=�$/ PropertyChanged=�- . . .   $
^ TrueX�$
_ TrueX�$` FalseX�$i CenterX�a   g . e   f �� *品番コード+$, TwoWay=�$/ PropertyChanged=�- . . .   $
^ TrueX�$
_ TrueX�$` FalseX�$i CenterX�a   $b 10��$
c Byte=�$	d Off=�e   f �� *賞味期限+$h  ��$C 
yyyy/MM/dd��$, TwoWay=�$/ PropertyChanged=�- . . .   $i RightX�$
^ trueX�$
_ TrueX�$` FalseX�a   $j 	999999.99~�$k 
-999999.99~�$l NotShow=�$m ###,##0.##,,,-, e   f �� *数量+$, TwoWay=�$/ PropertyChanged=�- . . .   $
^ TrueX�$
_ TrueX�$` FalseX�a   $b 2��$
c Byte=�e   f �� *単位+$, TwoWay=�$/ PropertyChanged=�- . . .   $
^ TrueX�$
_ TrueX�$` FalseX�a   $j 	999999.99~�$k 
-999999.99~�$l NotShow=�$m ###,##0.##,,,-, e   f �� *必要数量+$, TwoWay=�$/ PropertyChanged=�- . . .   $
^ TrueX�$
_ TrueX�$` FalseX�a   $j 	999999.99~�$k 
-999999.99~�$l NotShow=�$m ###,##0.##,,,-, e   f �� *在庫数量+$, TwoWay=�$/ PropertyChanged=�- . . . 