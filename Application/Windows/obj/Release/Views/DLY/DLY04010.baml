   M S B A M L   `   `   `  ���� X  TKyoeiSystem.Framework.Windows, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null�[clr-namespace:KyoeiSystem.Framework.Windows.ViewBase;assembly=KyoeiSystem.Framework.Windows&KyoeiSystem.Framework.Windows.ViewBase  �[clr-namespace:KyoeiSystem.Framework.Windows.ViewBase;assembly=KyoeiSystem.Framework.Windows&KyoeiSystem.Framework.Windows.ViewBase  a ]KyoeiSystem.Framework.Windows.Controls, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null�dclr-namespace:KyoeiSystem.Framework.Windows.Controls;assembly=KyoeiSystem.Framework.Windows.Controls&KyoeiSystem.Framework.Windows.Controls Z VKyoeiSystem.Application.Windows, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]3clr-namespace:KyoeiSystem.Application.Windows.Views%KyoeiSystem.Application.Windows.Views R NSystem.Data, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089>.clr-namespace:System.Data;assembly=System.DataSystem.Data 4   .KyoeiSystem.Application.Windows.Views.DLY04010   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      lfw[clr-namespace:KyoeiSystem.Framework.Windows.ViewBase;assembly=KyoeiSystem.Framework.Windows      R	 NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml 	    Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006 	    @d2http://schemas.microsoft.com/expression/blend/2008 	    jvm[clr-namespace:KyoeiSystem.Framework.Windows.ViewBase;assembly=KyoeiSystem.Framework.Windows 	    uctlsdclr-namespace:KyoeiSystem.Framework.Windows.Controls;assembly=KyoeiSystem.Framework.Windows.Controls 	    Buc3clr-namespace:KyoeiSystem.Application.Windows.Views 	    e
 aGrapeCity.WPF.SpreadGrid, Version=1.0.2015.1109, Culture=neutral, PublicKeyToken=5c5c8ff7a6858ccc=sg4http://schemas.grapecity.com/windows/spreadgrid/2012 
 9Data.clr-namespace:System.Data;assembly=System.Data 
 -   $	��728��$
��1024��$	��768��$
��1024��  � Left$  0��
 � Top$ 0�� � WindowStartupLocation$ Manual=� � AllowsTransparency .   � Language$ ja-JP� � Title$% 移動・振替入力(DLY04010)�� � 	Resources    DataGridHeaderBackground �� EndPoint �� 
StartPoint	 � Color
 � Offset  DataGridSummaryBackground��%�   &	        &	 7     �� $	 0,1&�$	 0,0&�.��� $	 #3b5998��$
 0Q��� $	 0,1&�$	 0,0&�.��� $	 #f0e68c��$
 0Q�.���  � RowDefinitions �� $
��auto ��� $
��auto ��� $��* �.+�+  %System.Windows.Controls.Ribbon.Ribbon  $��Stretch=�$��Stretch=�.@�.  (System.Windows.Controls.Ribbon.RibbonTab  $��	ホーム��   KeyTip$ h��$��75��.@�0  *System.Windows.Controls.Ribbon.RibbonGroup  $��90��  $��F1��   KeyTip$ MASTER��  RibbonFunctionBackground#��[ .@�1  +System.Windows.Controls.Ribbon.RibbonButton  -      LargeImageSource$% ../Images/15マスタ照会.png��$��0,0,0,0q�   ToolTipTitle$ マスタ照会(F1)��   ToolTipDescription$3 -入力済みのデータを検索します。��   ToolTipImageSource$% ../Images/15マスタ照会.png��   Label$ ﾏｽﾀ照会��   KeyTip$ F1��$��50��  $��F2��$ MASTER��#��[ .@�  $y�Hidden=�$  ��$  ��$  ��  $��F3��$ MASTER��#��[ .@�  $y�Hidden=�$  ��$  ��$  ��  $��F4��$ MASTER��#��[ .@�  $y�Hidden=�$  ��$  ��$  ��  $��F5��$ MASTER��#��[ .@�  $y�Hidden=�$  ��$  ��$  ��  $��F6��$ MASTER��#��[ .@�  $y�Hidden=�$  ��$  ��$  ��  $��F7��$ MASTER��#��[ .@�  $y�Hidden=�$  ��$  ��$  ��  $��F8��$ MASTER��#��[ .@�  $y�Hidden=�$  ��$  ��$  ��  $��F9��$ MASTER��#��[ .@�  -   $ ../Images/24登録.png��$ F9��$��50��$ 登録��$ データ登録(F9)��$- '入力データの登録を行います��$ ../Images/24登録.png��  $	��F10��$ MASTER��#��[ .@�  -   $ ../Images/26クリア.png��$	 F10��$��50��$ 入力取消��$ 入力取消(F10)��$' !入力の取消しを行います��$ ../Images/26クリア.png��  $	��F11��$ MASTER��#��[ .@�  -   $ ../Images/27終了.png��$	 F11��$��50��$ 終了��$ 終了(F11)��$ 終了します��$ ../Images/27終了.png��  $	��F12��$ MASTER��#��[ .@�  $y�Hidden=�$  ��$  ��$  ��  $��	ヘルプ��
�������.@�  $��サポート��.@�  -   $% ../Images/30マニュアル.png��$ MANUAL��$ ﾏﾆｭｱﾙ��$ 取扱説明書��$* $取り扱い説明書を開きます��  -   $( "../Images/29遠隔サポート.png��$ SUPPORT��$ お問合せ��$ お問合せ��$< 6Web上の｢お問合せ｣ページに遷移します��  -   $ ../Images/QandA.png��$	 FAQ��$ QandA��$ QandA��$E ?Web上の｢よくあるご質問｣ページに遷移します��  -	   $( "../Images/13共栄Webリンク.png��$ KYOEI��$ 共栄ｼｽﾃﾑ��$ 共栄システムHP��$< 6共栄システムのホームページを開きます��� $��0��$��Bottom=� �� $��74 � � ColumnDefinitions �� $��90 ��� $��* �.+�]� $��0��$��0��$��Stretch=�$��2q����� *MaintenanceModeBackground+.+��� $��メイリオ)�$��24(�$��Medium"�$��Stretch=�$��Center=�$��Center=�$��Center=����� *MaintenanceMode+���� *MaintenanceModeForeground+�� $��0��$��1��$��Bottom=�
��� ���
���  ��$��2,0,2,0q����� *ErrorMessage+y��� *ErrorMessageVisibility+� $��1��
�������$��Stretch=�$��Stretch=� �� $��* �.+�� -
    �Name$ FuncDisplay�� �� $
��auto ��� $
��auto ��� $
��auto ��� $
��auto ��� $��60 � �� $
��auto �.+�� -   $��	grdHeader��$��0��$��2��$��	20,10,0,5q� �� $
��auto ��� $��10 ��� $
��auto ��� $��* � �� $	��222 ��� $
��auto �$	��709���� $
��auto �.+��� -    ��Name$ 	FuncTitle��$��移動・振替入力��$��0��$��0��$��16(�$��30,0,0,0q�?  9KyoeiSystem.Framework.Windows.Controls.UcLabelTwinTextBox  -     Name$ txt会社名��   Label_Context$ 	会社名��$��1��   DataAccessName$ M70_JIS��   DataAccessMode$ On=�=  7KyoeiSystem.Framework.Windows.Controls.FrameworkControl   	IsKeyItem . $	��2,0q�   Label_Width$	 100Q�   Label1Visibility$ 	Collapsed=�   Label2Visibility$ 	Collapsed=�   
Text1Width$ 40Q�    Text1MaxLength$  3��!   Text1IMEType$	! Off=�"   Text1ValidationType$" Integer=�$#   Text1VerticalContentAlignment$# Center=�$   Text2BorderBrush
$ ���� %   Text2Background
% ���� &   Text2IsReadOnly& . $'   Text2VerticalContentAlignment$' Center=�� -   $ gHeader��$��2��$��0��$��2��$��60,0,0,0q� �� $��27 ��� $��27 � �� $��80 ��� $��80 ��� $	��100 ��� $	��250 ��� $	��250 ��� $
��auto �.+��� $��	伝票No.��$��0��$��0��
�������#��[  ( �� TextAlignment$( Center=�) �� Padding$) 0,5,0,0q�6  0KyoeiSystem.Framework.Windows.Controls.UcTextBox  -   *  Name$* txt伝票番号��$��1��$��0��+   ImeType$	+ Off=�,   ValidationType$, Integer=� . -   cTextAlignment$- Center=�.   
cMaxLength$. 7��/   cBorderThickness$/ 0q� 0   cVerticalContentAlignment$0 Center=�1   Text1 �� *SearchHeader[伝票番号]+2 �� Mode$2 TwoWay=�3 �� NotifyOnSourceUpdated3 . 4 �� NotifyOnTargetUpdated4 . 5 �� UpdateSourceTrigger$5 PropertyChanged=��� $��	移動日��$��0��$��1��$( Center=�$) 0,5,0,0q�
�������#��[  ;  5KyoeiSystem.Framework.Windows.Controls.UcLabelTextBox  -   $��txt移動日��$��1��$��1��6   
cMaxLength$6 10��7   cTextAlignment$7 Center=�$��0q� .  8   Mask$8 
yyyy/MM/dd��9   cValidationType$
9 Date=�:   Label_Width$: 0Q�$��-1,-1,-2,-2q� ;   cVerticalContentAlignment$; Center=�<   Text< �� = �� Path$= SearchHeader[日付]�$2 TwoWay=�4 . 3 . > �� StringFormat$> 
yyyy/MM/dd��$5 	LostFocus=��� $��移動区分��$��0��$��2��$( Center=�$) 0,5,0,0q�
�������#��[  <	  6KyoeiSystem.Framework.Windows.Controls.UcLabelComboBox	  -   ? 	 Name$? cmb移動区分��$��1��$��2��@ 	  Label_Visibility$@ 	Collapsed=�A 	  ComboListingParams$&A  日次,移動入力,移動区分��B 	  SelectedValueB �� *SearchHeader[移動区分]+$2 TwoWay=�3 . 4 . $5 PropertyChanged=��� $��移動元倉庫��$��0��$��3��$( Center=�$) 0,5,0,0q�
�������#��[    -   $ txt移動元倉庫��$��1��$��3��$��3��$ M22_SOUK��$ On=�$
��Left=�$��2,0,0,0q� .  C   Label_Visibility$C 	Collapsed=�$ 	Collapsed=�$ 	Collapsed=�$ 40Q�$  3��$	! Off=�$" Integer=�$# Center=�D   
Text2Width$	D 210Q�& . 
% ���� 
$ ���� $' Center=�E   Text1E �� *(&SearchHeader[出荷元倉庫コード]+$2 TwoWay=�3 . 4 . $5 PropertyChanged=��� $��移動先倉庫��$��0��$��4��$( Center=�$) 0,5,0,0q�
�������#��[    -   $ txt移動先倉庫��$��1��$��4��$ M22_SOUK��$ On=�$
��Left=�$��2,0,0,0q� .  $C 	Collapsed=�$ 	Collapsed=�$ 	Collapsed=�$ 40Q�$  3��$	! Off=�$" Integer=�$# Center=�$	D 210Q�& . 
% ���� 
$ ���� $' Center=�E �� *(&SearchHeader[出荷先倉庫コード]+$2 TwoWay=�3 . 4 . $5 PropertyChanged=��� $��0��$��0��$��1,1,0,0q�
���   ��� $��1��$��0��$��1,1,0,0q�
���   ��� $��2��$��0��$��1,1,0,0q�
���   ��� $��3��$��0��$��1,1,0,0q�
���   ��� $��4��$��0��$��1,1,1,0q�
���   ��� $��0��$��1��$��1,1,0,1q�
���   ��� $��1��$��1��$��1,1,0,1q�
���   ��� $��2��$��1��$��1,1,0,1q�
���   ��� $��3��$��1��$��1,1,0,1q�
���   ��� $��4��$��1��$��1,1,1,1q�
���   �/
 
 )GrapeCity.Windows.SpreadGrid.GcSpreadGrid
  -   $��gcSpreadGrid��$��2��$
��Left=�F 
  AutoFitType$F CellWithHeader=�G 
  
LayoutMode$G Template=�H 
  AutoGenerateColumnsH .  I 
  HighlightHeader$
I None=�J 
  RowCount$J 10��K 
  OperationMode$K Normal=�L 
  	ProtectedL . M 
  CanUserSortColumnsM .  N 
  ColumnDragMode$N Direct=�$	��950��$	��470��$��80,5,0,0q�O 
  DocumentUri$KO E/KyoeiSystem.Application.Windows;component/views/DLY/DLY04010G1.sgxml?�P 
  ItemsSourceP �� *SearchDetail+$2 TwoWay=�$5 PropertyChanged=�3 . 4 . Q 
  SelectionBorderUnfocusedQ - 
 'GrapeCity.Windows.SpreadGrid.BorderLine  R   Color$R Orange��S   Style$S Thick=�T 
  SelectionBorderT   $R Orange��$S Thick=�U 
  ColumnsU ) 
 #GrapeCity.Windows.SpreadGrid.Column  V   Locked$V FalseX�W   	Focusable$
W TrueX�X   CanUserResize$X FalseX�Y   CellTypeY / 
 )GrapeCity.Windows.SpreadGrid.TextCellType  Z   	MaxLength$Z 12��[   MaxLengthUnit$
[ Byte=�\   ImeState$	\ Off=�]   	DataField] 3 
 -GrapeCity.Windows.SpreadGrid.BindingDataField  ^   Binding^ �� *自社品番+$2 TwoWay=�$5 PropertyChanged=�3 . 4 .   $
V TrueX�$
W TrueX�$X FalseX�Y   3 
 -GrapeCity.Windows.SpreadGrid.EditBaseCellType_   
IsReadOnly_ . ]   ^ �� *自社品名+$2 TwoWay=�$5 PropertyChanged=�3 . 4 .   `   HorizontalAlignment$` CenterX�$V FalseX�$
W TrueX�$X FalseX�Y   $Z 10��$
[ Byte=�$	\ Off=�]   ^ �� *賞味期限+a �� TargetNullValue$a  ��$> 
yyyy/MM/dd��$2 TwoWay=�$5 PropertyChanged=�3 . 4 .   $` RightX�$V FalseX�$
W TrueX�$X FalseX�Y 1 
 +GrapeCity.Windows.SpreadGrid.NumberCellType  b   MaxValue$b 	999999.99~�c   MinValue$c 
-999999.99~�d   SpinButtonVisibility$d NotShow=�e   FieldSetF 
 @GrapeCity.Windows.SpreadGrid.Editors.NumberFieldSetTypeConverter$e ###,##0.##,,,-, ]   ^ �� *数量+$2 TwoWay=�$5 PropertyChanged=�3 . 4 .   $V FalseX�$
W TrueX�$X FalseX�Y   $Z 40��$
[ Byte=�$\ On=�]   ^ �� *摘要+$2 TwoWay=�$5 PropertyChanged=�3 . 4 .   $
V TrueX�$W FalseX�f   	IsVisiblef .  g   Width$g 0Q�Y   _ . ]   ^ �� *品番コード+  $
V TrueX�f .  $X FalseX�Y   _ . ]   ^ �� *消費税区分+$2 TwoWay=�3 . 4 . $5 PropertyChanged=�  $
V TrueX�f .  $X FalseX�Y   _ . ]   ^ �� *商品分類+$2 TwoWay=�3 . 4 . $5 PropertyChanged=�