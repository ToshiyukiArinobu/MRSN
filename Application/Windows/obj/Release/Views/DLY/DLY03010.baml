   M S B A M L   `   `   `  ���� X  TKyoeiSystem.Framework.Windows, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null�[clr-namespace:KyoeiSystem.Framework.Windows.ViewBase;assembly=KyoeiSystem.Framework.Windows&KyoeiSystem.Framework.Windows.ViewBase  �[clr-namespace:KyoeiSystem.Framework.Windows.ViewBase;assembly=KyoeiSystem.Framework.Windows&KyoeiSystem.Framework.Windows.ViewBase  a ]KyoeiSystem.Framework.Windows.Controls, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null�dclr-namespace:KyoeiSystem.Framework.Windows.Controls;assembly=KyoeiSystem.Framework.Windows.Controls&KyoeiSystem.Framework.Windows.Controls Z VKyoeiSystem.Application.Windows, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]3clr-namespace:KyoeiSystem.Application.Windows.Views%KyoeiSystem.Application.Windows.Views R NSystem.Data, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089>.clr-namespace:System.Data;assembly=System.DataSystem.Data 4   .KyoeiSystem.Application.Windows.Views.DLY03010   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      lfw[clr-namespace:KyoeiSystem.Framework.Windows.ViewBase;assembly=KyoeiSystem.Framework.Windows      R	 NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml 	    Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006 	    @d2http://schemas.microsoft.com/expression/blend/2008 	    jvm[clr-namespace:KyoeiSystem.Framework.Windows.ViewBase;assembly=KyoeiSystem.Framework.Windows 	    uctlsdclr-namespace:KyoeiSystem.Framework.Windows.Controls;assembly=KyoeiSystem.Framework.Windows.Controls 	    Buc3clr-namespace:KyoeiSystem.Application.Windows.Views 	    e
 aGrapeCity.WPF.SpreadGrid, Version=1.0.2015.1109, Culture=neutral, PublicKeyToken=5c5c8ff7a6858ccc=sg4http://schemas.grapecity.com/windows/spreadgrid/2012 
 9Data.clr-namespace:System.Data;assembly=System.Data 
 -   $	��728��$
��1024��$	��768��$
��1024��  � Left$  0��
 � Top$ 0�� � WindowStartupLocation$ Manual=� � AllowsTransparency .   � Language$ ja-JP� � Title$ 売上入力(DLY03010)�� � 	Resources    DataGridHeaderBackground �� EndPoint �� 
StartPoint	 � Color
 � Offset  DataGridSummaryBackground��%�   &	        &	 7     �� $	 0,1&�$	 0,0&�.��� $	 #3b5998��$
 0Q��� $	 0,1&�$	 0,0&�.��� $	 #f0e68c��$
 0Q�.���  � RowDefinitions �� $
��auto ��� $
��auto ��� $��* �.+�+  %System.Windows.Controls.Ribbon.Ribbon  $��Stretch=�$��Stretch=�.@�.  (System.Windows.Controls.Ribbon.RibbonTab  $��	ホーム��   KeyTip$ h��$��75��.@�0  *System.Windows.Controls.Ribbon.RibbonGroup  $��90��  $��F1��   KeyTip$ MASTER��  RibbonFunctionBackground#��[ .@�1  +System.Windows.Controls.Ribbon.RibbonButton  -      LargeImageSource$% ../Images/15マスタ照会.png��$��0,0,0,0q�   ToolTipTitle$ マスタ照会(F1)��   ToolTipDescription$3 -入力済みのデータを検索します。��   ToolTipImageSource$% ../Images/15マスタ照会.png��   Label$ ﾏｽﾀ照会��   KeyTip$ F1��$��50��  $��F2��$ MASTER��#��[ .@�  -   $% ../Images/16マスタ入力.png��$��0,0,0,0q�$ ﾏｽﾀ入力��$ F2��$��50��$ マスタ入力(F2)��$- 'マスタの入力画面を開きます��$% ../Images/16マスタ入力.png��  $��F3��$ MASTER��#��[ .@�  -   $" ../Images/18詳細表示.png��$��0,0,0,0q�$ 在庫参照��$ F3��$��50��$ 在庫参照(F3)��$* $在庫の参照画面を開きます��$" ../Images/18詳細表示.png��  $��F4��$ MASTER��#��[ .@�  $y�Hidden=�$  ��$  ��$  ��  $��F5��$ MASTER��#��[ .@�  -   $ ../Images/行追加.png��$ F5��$��50��$ 	行追加��$ 行追加(F5)��$0 *リストに新規行を追加します。��$ ../Images/行追加.png��  $��F6��$ MASTER��#��[ .@�  -   $ ../Images/28削除.png��$ F6��$��50��$ 	行削除��$ 行削除(F6)��$0 *選択中のリスト行を削除します��$ ../Images/28削除.png��  $��F7��$ MASTER��#��[ .@�  $y�Hidden=�$  ��$  ��$  ��  $��F8��$ MASTER��#��[ .@�  $y�Hidden=�$  ��$  ��$  ��  $��F9��$ MASTER��#��[ .@�  -   $ ../Images/24登録.png��$ F9��$��50��$ 登録��$ データ登録(F9)��$- '入力データの登録を行います��$ ../Images/24登録.png��  $	��F10��$ MASTER��#��[ .@�  -   $ ../Images/26クリア.png��$	 F10��$��50��$ 入力取消��$ 入力取消(F10)��$' !入力の取消しを行います��$ ../Images/26クリア.png��  $	��F11��$ MASTER��#��[ .@�  -	   $ ../Images/27終了.png��$	 F11��$��50��$ 終了��$ 終了(F11)��$ 終了します��$ ../Images/27終了.png��  $	��F12��$ MASTER��#��[ .@�  -
   $ ../Images/28削除.png��$	 F12��$��50��$ 削除��$ 削除(F12)��$- '表示中のデータを削除します��$ ../Images/28削除.png��  $��	ヘルプ��
�������.@�  $��サポート��.@�  -   $% ../Images/30マニュアル.png��$ MANUAL��$ ﾏﾆｭｱﾙ��$ 取扱説明書��$* $取り扱い説明書を開きます��  -   $( "../Images/29遠隔サポート.png��$ SUPPORT��$ お問合せ��$ お問合せ��$< 6Web上の｢お問合せ｣ページに遷移します��  -   $ ../Images/QandA.png��$	 FAQ��$ QandA��$ QandA��$E ?Web上の｢よくあるご質問｣ページに遷移します��  -   $( "../Images/13共栄Webリンク.png��$ KYOEI��$ 共栄ｼｽﾃﾑ��$ 共栄システムHP��$< 6共栄システムのホームページを開きます��� $��0��$��Bottom=� �� $��74 � � ColumnDefinitions �� $��90 ��� $��* �.+�]� $��0��$��0��$��Stretch=�$��2q����� *MaintenanceModeBackground+.+��� $��メイリオ)�$��24(�$��Medium"�$��Stretch=�$��Center=�$��Center=�$��Center=����� *MaintenanceMode+���� *MaintenanceModeForeground+�� $��0��$��1��$��Bottom=�
��� ���
���  ��$��2,0,2,0q����� *ErrorMessage+y��� *ErrorMessageVisibility+� $��1��
�������$��Stretch=�$��Stretch=� �� $��* �.+�� -    �Name$ FuncDisplay�� �� $
��auto ��� $
��auto ��� $
��auto ��� $
��auto ��� $��30 � �� $
��auto �.+�� -   $��	grdHeader��$��0��$��2��$��	20,10,0,5q� �� $��30 ��� $��10 ��� $
��auto ��� $��* � �� $	��100 ��� $
��auto ��� $
��auto �.+��� -    ��Name$ 	FuncTitle��$��売上入力��$��0��$��0��$��16(�$��30,0,0,0q�?  9KyoeiSystem.Framework.Windows.Controls.UcLabelTwinTextBox  -     Name$ txt自社名��   Label_Context$ 	自社名��$��1��$	��350��   DataAccessName$ M70_JIS��   DataAccessMode$ On=�=  7KyoeiSystem.Framework.Windows.Controls.FrameworkControl   	IsKeyItem .    Label_Width$	 100Q�   Label1Visibility$ 	Collapsed=�   Label2Visibility$ 	Collapsed=�   
Text1Width$ 40Q�    Text1MaxLength$  3��!   Text1IMEType$	! Off=�"   Text1ValidationType$" Integer=�$#   Text1VerticalContentAlignment$# Center=�$   Text2BorderBrush
$ ���� %   Text2Background
% ���� &   Text2IsReadOnly& . $'   Text2VerticalContentAlignment$' Center=�<  6KyoeiSystem.Framework.Windows.Controls.UcLabelComboBox  -   (  Name$( cmb伝票要否��$��0��$��2��)   Label_Context$) 伝票要否��*   Label_Width$	* 100Q�$	��250��+   ComboListingParams$&+  日次,売上入力,伝票要否��,   SelectedValue, �� *SearchHeader[伝票要否]+- �� Mode$- TwoWay=�. �� NotifyOnSourceUpdated. . / �� NotifyOnTargetUpdated/ . 0 �� UpdateSourceTrigger$0 PropertyChanged=�� -   $ gHeader��$��2��$��0��$��3��$��60,0,0,0q� �� $��27 ��� $��27 ��� $��27 ��� $��27 ��� $��27 ��� $��27 � �� $��80 ��� $��80 ��� $	��150 ��� $	��250 ��� $	��250 ��� $
��auto �.+��� $��	伝票No.��$��0��$��0��
�������#��[  1 �� TextAlignment$1 Center=�2 �� Padding$2 0,5,0,0q��� -   $��PrevSlip��$��0��$��0��3 K� ZIndex$3 1��$
��Left=�#��[  
�������$��0q�$	��◀��$��16��$��Center=��� -   $��NextSlip��$��0��$��0��$3 1��$��Right=�#��[  
�������$��0q�$	��▶��$��16��$��Center=�6  0KyoeiSystem.Framework.Windows.Controls.UcTextBox  -   4  Name$4 txt伝票番号��$��1��$��0��5   ImeType$	5 Off=�6   ValidationType$6 Integer=� . 7   cTextAlignment$7 Center=�8   
cMaxLength$8 7��9   cBorderThickness$9 0q� :   cVerticalContentAlignment$: Center=�;   Text; �� *SearchHeader[伝票番号]+$- TwoWay=�. . / . $0 PropertyChanged=��� $��	売上日��$��0��$��1��$1 Center=�$2 0,5,0,0q�
�������#��[    -   $��txt売上日��$��1��$��1�� .  $8 10��$7 Center=�$9 0q�$: Center=�<   Mask$< 
yyyy/MM/dd��$
6 Date=�; �� = �� Path$= SearchHeader[売上日]�$- TwoWay=�/ . . . > �� StringFormat$> 
yyyy/MM/dd��$0 	LostFocus=��� $��売上区分��$��0��$��2��$1 Center=�$2 0,5,0,0q�
�������#��[    -   $( cmb売上区分��$��1��$��2��?   Label_Visibility$? 	Collapsed=�$&+  日次,売上入力,売上区分��, �� *SearchHeader[売上区分]+$- TwoWay=�. . / . $0 PropertyChanged=��� $��得意先名称��$��0��$��3��$1 Center=�$2 0,5,0,0q�
�������#��[  ;	  5KyoeiSystem.Application.Windows.Views.M01_TOK_TextBox	  -   @ 	 Name$@ txt得意先��;
  5KyoeiSystem.Application.Windows.Views.M01_TOK_TextBoxA 
  	LostFocus$A txt得意先_LostFocus��B 
  TextAfterChanged$#B txt得意先_TextAfterChanged��$��1��$��3��C 	  DataAccessName$C M01_TOK��D 	  DataAccessMode$D On=�E 	  LinkItem$	E 0,3��$
��Left=�$��2,0,0,0q� .  |�. F 	  Label_Visibility$F 	Collapsed=�G 	  Label1Visibility$G 	Collapsed=�H 	  Label2Visibility$H 	Collapsed=�I 	  
Text1Width$I 40Q�J 	  Text1MaxLength$J 4��K 	  Text1IMEType$	K Off=�L 	  Text1ValidationType$L Integer=�$M 	  Text1VerticalContentAlignment$M Center=�N 	  
Label1Text$	N －��O 	  
Text2Width$O 30Q�P 	  Text2MaxLength$P 2��Q 	  Text2IMEType$	Q Off=�R 	  Text2ValidationType$R Integer=�$S 	  Text2VerticalContentAlignment$S Center=�T 	  
Label2Text$T  ��U 	  Text1U �� *" SearchHeader[得意先コード]+$- TwoWay=�. . / . $0 PropertyChanged=�V 	  Text2V �� *SearchHeader[得意先枝番]+$- TwoWay=�. . / . $0 PropertyChanged=��� $��出荷先名称��$��0��$��4��$1 Center=�$2 0,5,0,0q�
�������#��[    -   $ txt出荷先��$��1��$��4��$ M21_SYUK��$ On=�$
��Left=�$��2,0,0,0q� .  W   Label_Visibility$W 	Collapsed=�$ 	Collapsed=�$ 	Collapsed=�$ 40Q�$  15��$	! Off=�$# Center=�X   
Text2Width$	X 210Q�& . 
% ���� 
$ ���� $' Center=�Y   Text1Y �� *" SearchHeader[出荷先コード]+$- TwoWay=�. . / . $0 PropertyChanged=��� $��納品伝票No.��$��2��$��0��$1 Center=�$2 0,5,0,0q�
�������#��[    -   $4 txt納品伝票番号��$��3��$��0��$	5 Off=�$6 Integer=� .  $7 Center=�$8 7��$9 0q�$: Center=�
�������; �� *" SearchHeader[納品伝票番号]+$- TwoWay=�. . / . $0 PropertyChanged=��� $��	出荷日��$��2��$��1��$1 Center=�$2 0,5,0,0q�
�������#��[    -   $��txt出荷日��$��3��$��1�� .  $8 10��$7 Center=�$9 0q�$: Center=�$< 
yyyy/MM/dd��$
6 Date=�; �� $= SearchHeader[出荷日]�$- TwoWay=�/ . . . $> 
yyyy/MM/dd��$0 	LostFocus=��� $��	受注No.��$��2��$��2��$1 Center=�$2 0,5,0,0q�
�������#��[    -   $4 txt受注番号��$��3��$��2��$	5 Off=�$6 Integer=� .  $7 Center=�$8 10��$9 0q�Z   cBackground
Z �����$: Center=�; �� *SearchHeader[受注番号]+$- TwoWay=�. . / . $0 PropertyChanged=��� $=��7仕入先名称 （※メーカーの場合は必須）��$��2��$��3��$1 Center=�$2 0,5,0,0q�
�������#��[  	  -   $@ txt仕入先��$��3��$��3��$C M01_TOK��$D On=�$	E 1,3��$
��Left=�$��2,0,0,0q� .  |�. $F 	Collapsed=�$G 	Collapsed=�$H 	Collapsed=�$I 40Q�$J 4��$	K Off=�$L Integer=�$M Center=�$	N －��$O 30Q�$P 2��$	Q Off=�$R Integer=�$S Center=�$T  ��U �� *" SearchHeader[仕入先コード]+$- TwoWay=�. . / . $0 PropertyChanged=�V �� *SearchHeader[仕入先枝番]+$- TwoWay=�. . / . $0 PropertyChanged=��� $��出荷元名称��$��2��$��4��$1 Center=�$2 0,5,0,0q�
�������#��[    -    $ txt出荷元��$��3��$��4��$ M70_JIS��$ On=�$
��Left=�$��2,0,0,0q� .  $W 	Collapsed=�$ 	Collapsed=�$ 	Collapsed=�$ 40Q�$	! Off=�$" Integer=�$# Center=�$	X 210Q�& . 
% ���� 
$ ���� $' Center=�Y �� *" SearchHeader[出荷元コード]+$- TwoWay=�. . / . $0 PropertyChanged=��� $��在庫倉庫名称��$��4��$��0��$��3��$1 Center=�$2 0,5,0,0q�
�������#��[    -!   $ txt在庫倉庫��$��5��$��0��$��3��$ M22_SOUK��$ On=�$
��Left=�$��2,0,0,0q� .  $W 	Collapsed=�$ 	Collapsed=�$ 	Collapsed=�$ 40Q�$  3��$	! Off=�$" Integer=�$# Center=�$	X 210Q�& . 
% ���� 
$ ���� $' Center=�Y �� *%#SearchHeader[在庫倉庫コード]+$- TwoWay=�. . / . $0 PropertyChanged=��� $��0��$��0��$��1,1,0,0q�
���   �$3 9���� $��1��$��0��$��1,1,0,0q�
���   �$3 9���� $��2��$��0��$��1,1,0,0q�
���   �$3 9���� $��3��$��0��$��1,1,0,0q�
���   �$3 9���� $��4��$��0��$��1,1,1,0q�
���   �$3 9���� $��0��$��1��$��1,1,0,0q�
���   �$3 9���� $��1��$��1��$��1,1,0,0q�
���   �$3 9���� $��2��$��1��$��1,1,0,0q�
���   �$3 9���� $��3��$��1��$��1,1,0,0q�
���   �$3 9���� $��4��$��1��$��1,1,1,0q�
���   �$3 9���� $��0��$��2��$��1,1,0,0q�
���   �$3 9���� $��1��$��2��$��1,1,0,0q�
���   �$3 9���� $��2��$��2��$��1,1,0,0q�
���   �$3 9���� $��3��$��2��$��1,1,0,0q�
���   �$3 9���� $��4��$��2��$��1,1,1,0q�
���   �$3 9���� $��0��$��3��$��1,1,0,1q�
���   �$3 9���� $��1��$��3��$��1,1,0,1q�
���   �$3 9���� $��2��$��3��$��1,1,0,1q�
���   �$3 9���� $��3��$��3��$��1,1,0,1q�
���   �$3 9���� $��4��$��3��$��1,1,1,1q�
���   �$3 9���� $��0��$��4��$��3��$��1,0,1,1q�
���   �$3 9���� $��0��$��5��$��3��$��1,0,1,1q�
���   �$3 9��/ 
 )GrapeCity.Windows.SpreadGrid.GcSpreadGrid  -"   $��gcSpreadGrid��$��2��$
��Left=�[   AutoFitType$[ CellWithHeader=�\   
LayoutMode$\ Template=�]   AutoGenerateColumns] .  ^   HighlightHeader$
^ None=�_   RowCount$_ 10��`   OperationMode$` Normal=�a   	Protecteda . b   CanUserSortColumnsb .  c   ColumnDragMode$c Direct=�$	��950��$	��290��$��80,5,0,0q�d   DocumentUri$Kd E/KyoeiSystem.Application.Windows;component/views/DLY/DLY03010G1.sgxml?�e   ItemsSourcee �� *SearchDetail+$- TwoWay=�$0 PropertyChanged=�. . / . f   SelectionBorderUnfocusedf - 
 'GrapeCity.Windows.SpreadGrid.BorderLine  g   Color$g Orange��h   Style$h Thick=�i   SelectionBorderi   $g Orange��$h Thick=�j   Columnsj ) 
 #GrapeCity.Windows.SpreadGrid.Column  k   Locked$k FalseX�l   	Focusable$
l TrueX�m   CanUserResize$m FalseX�n   CellTypen / 
 )GrapeCity.Windows.SpreadGrid.TextCellType  o   	MaxLength$o 12��p   MaxLengthUnit$
p Byte=�q   ImeState$	q Off=�r   	DataFieldr 3 
 -GrapeCity.Windows.SpreadGrid.BindingDataField  s   Bindings �� *自社品番+$- TwoWay=�$0 PropertyChanged=�. . / .   $k FalseX�$
l TrueX�$m FalseX�n   $o 50��$
p Byte=�$	q Off=�r   s �� *得意先品番コード+$- TwoWay=�$0 PropertyChanged=�. . / .   $
k TrueX�$
l TrueX�$m FalseX�n   3 
 -GrapeCity.Windows.SpreadGrid.EditBaseCellTypet   
IsReadOnlyt . r   s �� *自社品名+$- TwoWay=�$0 PropertyChanged=�. . / .   u   HorizontalAlignment$u CenterX�$k FalseX�$
l TrueX�$m FalseX�n   $o 10��$
p Byte=�$	q Off=�r   s �� *賞味期限+v �� TargetNullValue$v  ��$> 
yyyy/MM/dd��$- TwoWay=�$0 PropertyChanged=�. . / .   $u RightX�$k FalseX�$
l TrueX�$m FalseX�n 1 
 +GrapeCity.Windows.SpreadGrid.NumberCellType  w   MaxValue$w 	999999.99~�x   MinValue$x 
-999999.99~�y   SpinButtonVisibility$y NotShow=�z   FieldSetF 
 @GrapeCity.Windows.SpreadGrid.Editors.NumberFieldSetTypeConverter$z ###,##0.##,,,-, r   s �� *数量+$- TwoWay=�$0 PropertyChanged=�. . / .   $
k TrueX�$l FalseX�$m FalseX�n   $o 2��$
p Byte=�r   s �� *単位+$- TwoWay=�$0 PropertyChanged=�. . / .   $u RightX�$k FalseX�$
l TrueX�$m FalseX�n   $w 99999999.99~�$x -99999999.99~�$y NotShow=�$z ##,###,##0.##,,,-, r   s �� *単価+$- TwoWay=�$0 PropertyChanged=�. . / .   $u RightX�$
k TrueX�$l FalseX�$m FalseX�n   $w 99999999~�$x 	-99999999~�$y NotShow=�$z ###,###,##0,,,-, r   s �� *金額+$- TwoWay=�$0 PropertyChanged=�. . / .   $k FalseX�$
l TrueX�$m FalseX�n   $o 40��$
p Byte=�$q On=�r   s �� *摘要+$- TwoWay=�$0 PropertyChanged=�. . / .   $k FalseX�$
l TrueX�$m FalseX�$u CenterX�n 3 
 -GrapeCity.Windows.SpreadGrid.CheckBoxCellType  r   s �� *マルセン仕入+$- TwoWay=�$0 PropertyChanged=�. . / .   $
k TrueX�$l FalseX�{   	IsVisible{ .  |   Width$| 0Q�n   t . r   s �� *品番コード+  $
k TrueX�$l FalseX�{ .  $| 0Q�n   t . r   s �� *消費税区分+  $
k TrueX�$l FalseX�{ .  $| 0Q�n   t . r   s �� *商品分類+� -#   $ 
FooterArea��$��3��$��	80,0,0,15q� } �� 
TargetType~ �� Property �� Value��%/   '��       �� "} ��.��� ~ � ��
 �   � �� $��30 ��� $��30 ��� $��30 ��� $��* � �� $	��465 ��� $��84 ��� $��84 ��� $
��auto �.+�4  .KyoeiSystem.Framework.Windows.Controls.UcLabel  $��備　　考��$��0��$��0��$1 Center=�$��0,6,0,0q�#��[   -$   $ 	txt備考��$��1��$��0��$��2q�$ M11_TEK��$ On=�$W 	Collapsed=�$ 	Collapsed=�$ 	Collapsed=�$ 40Q�$  5���   Text2MaxLength$� 40���   Text2IMEType$� Native=��   Text2� �� *SearchHeader[備考]+$- TwoWay=�. . / .   $��	小　計��$��0��$��1��$1 Center=�$��0,6,0,0q�#��[   $��	消費税��$��1��$��1��$1 Center=�$��0,6,0,0q�#��[   $��	総合計��$��2��$��1��$1 Center=�$��0,6,0,0q�#��[ �� -%   � ��Name$� 	lbl小計��$��0��$��2��$��Right=�$��0,6,5,0q��� -&   $� lbl消費税��$��1��$��2��$��Right=�$��0,6,5,0q��� -'   $� lbl総合計��$��2��$��2��$��Right=�$��0,6,5,0q��� $��0��$��0��$��1,1,0,1q�$3 9���� $��1��$��0��$��1,1,0,1q�$3 9���� $��2��$��0��$��1,1,1,1q�$3 9���� $��0��$��1��$��1,0,0,1q�$3 9���� $��1��$��1��$��1,0,0,1q�$3 9���� $��2��$��1��$��1,0,1,1q�$3 9���� $��1��$��2��$��1,0,0,1q�$3 9���� $��2��$��2��$��1,0,1,1q�$3 9��