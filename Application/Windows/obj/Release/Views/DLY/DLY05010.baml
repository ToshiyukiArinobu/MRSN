   M S B A M L   `   `   `  ���� X  TKyoeiSystem.Framework.Windows, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null�[clr-namespace:KyoeiSystem.Framework.Windows.ViewBase;assembly=KyoeiSystem.Framework.Windows&KyoeiSystem.Framework.Windows.ViewBase  �[clr-namespace:KyoeiSystem.Framework.Windows.ViewBase;assembly=KyoeiSystem.Framework.Windows&KyoeiSystem.Framework.Windows.ViewBase  a ]KyoeiSystem.Framework.Windows.Controls, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null�dclr-namespace:KyoeiSystem.Framework.Windows.Controls;assembly=KyoeiSystem.Framework.Windows.Controls&KyoeiSystem.Framework.Windows.Controls Z VKyoeiSystem.Application.Windows, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]3clr-namespace:KyoeiSystem.Application.Windows.Views%KyoeiSystem.Application.Windows.Views R NSystem.Data, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089>.clr-namespace:System.Data;assembly=System.DataSystem.Data 4   .KyoeiSystem.Application.Windows.Views.DLY05010   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      lfw[clr-namespace:KyoeiSystem.Framework.Windows.ViewBase;assembly=KyoeiSystem.Framework.Windows      R	 NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml 	    Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006 	    @d2http://schemas.microsoft.com/expression/blend/2008 	    jvm[clr-namespace:KyoeiSystem.Framework.Windows.ViewBase;assembly=KyoeiSystem.Framework.Windows 	    uctlsdclr-namespace:KyoeiSystem.Framework.Windows.Controls;assembly=KyoeiSystem.Framework.Windows.Controls 	    Buc3clr-namespace:KyoeiSystem.Application.Windows.Views 	    e
 aGrapeCity.WPF.SpreadGrid, Version=1.0.2015.1109, Culture=neutral, PublicKeyToken=5c5c8ff7a6858ccc=sg4http://schemas.grapecity.com/windows/spreadgrid/2012 
 9Data.clr-namespace:System.Data;assembly=System.Data 
 -   $	��728��$
��1024��$	��768��$
��1024��  � Left$  0��
 � Top$ 0�� � WindowStartupLocation$ Manual=� � AllowsTransparency .   � Language$ ja-JP� � Title$ 入金入力(DLY05010)�� � 	Resources    DataGridHeaderBackground �� EndPoint �� 
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
��auto ��� $	��350 � �� $
��auto �.+�� -   $��	grdHeader��$��0��$��2��$��	20,10,0,5q� �� $
��auto ��� $��10 ��� $
��auto ��� $��* � �� $	��222 ��� $
��auto �$	��810���� $
��auto �.+��� -    ��Name$ 	FuncTitle��$��入金入力��$��0��$��0��$��16(�$��30,0,0,0q�?  9KyoeiSystem.Framework.Windows.Controls.UcLabelTwinTextBox  -     Name$ txt会社名��   Label_Context$ 	会社名��$��1��$
��Left=�   DataAccessName$ M70_JIS��   DataAccessMode$ On=�=  7KyoeiSystem.Framework.Windows.Controls.FrameworkControl   	IsKeyItem . $	��2,0q�   Label_Width$	 100Q�   Label1Visibility$ 	Collapsed=�   Label2Visibility$ 	Collapsed=�   
Text1Width$ 40Q�    Text1MaxLength$  3��!   Text1IMEType$	! Off=�"   Text1ValidationType$" Integer=�$#   Text1VerticalContentAlignment$# Center=�$   Text2BorderBrush
$ ���� %   Text2Background
% ���� &   Text2IsReadOnly& . $'   Text2VerticalContentAlignment$' Center=�(   Text1( �� *(&SearchHeader[入金先自社コード]+) �� Mode$) TwoWay=�* �� NotifyOnSourceUpdated* . + �� NotifyOnTargetUpdated+ . , �� UpdateSourceTrigger$, PropertyChanged=�� -   $ gHeader��$��2��$��0��$��2��$��60,0,0,0q� �� $��27 ��� $��27 � �� $��80 ��� $��80 ��� $	��250 ��� $	��250 ��� $
��auto �.+��� $��	伝票No.��$��0��$��0��
�������#��[  - �� TextAlignment$- Center=�. �� Padding$. 0,5,0,0q��� -   $��PrevSlip��$��0��$��0��/ K� ZIndex$/ 1��$
��Left=�#��[  
�������$��0q�$	��◀��$��16��$��Center=�$y�Hidden=��� -   $��NextSlip��$��0��$��0��$/ 1��$��Right=�#��[  
�������$��0q�$	��▶��$��16��$��Center=�$y�Hidden=�6  0KyoeiSystem.Framework.Windows.Controls.UcTextBox  -   0  Name$0 txt伝票番号��$��1��$��0��1   ImeType$	1 Off=�2   ValidationType$2 Integer=� . 3   cTextAlignment$3 Center=�4   
cMaxLength$4 7��5   cBorderThickness$5 0q� 6   cVerticalContentAlignment$6 Center=�7   Text7 �� *SearchHeader[伝票番号]+$) TwoWay=�* . + . $, PropertyChanged=��� $��	入金日��$��0��$��1��$- Center=�$. 0,5,0,0q�
�������#��[  ;  5KyoeiSystem.Framework.Windows.Controls.UcLabelTextBox  -   $��txt入金日��$��1��$��1��$��0q�8   cBorderThickness$8 0q� .  9   
cMaxLength$9 10��:   cTextAlignment$: Center=� ;   cVerticalContentAlignment$; Center=�<   Mask$< 
yyyy/MM/dd��=   cValidationType$
= Date=�>   Label_Width$> 0Q�$��-1,-1,-2,-2q�?   Text? �� @ �� Path$@ SearchHeader[入金日]�$) TwoWay=�+ . * . A �� StringFormat$A 
yyyy/MM/dd��$, 	LostFocus=��� $��	得意先��$��0��$��2��$- Center=�$. 0,5,0,0q�
�������#��[  ;	  5KyoeiSystem.Application.Windows.Views.M01_TOK_TextBox	  -   B 	 Name$B txt得意先��$��1��$��2��C 	  DataAccessName$C M01_TOK��D 	  DataAccessMode$D On=�E 	  LinkItem$	E 0,3��$
��Left=�$��2,0,0,0q� .  |�. F 	  Label_Visibility$F 	Collapsed=�G 	  Label1Visibility$G 	Collapsed=�H 	  Label2Visibility$H 	Collapsed=�I 	  
Text1Width$I 40Q�J 	  Text1MaxLength$J 4��K 	  Text1IMEType$	K Off=�L 	  Text1ValidationType$L Integer=�$M 	  Text1VerticalContentAlignment$M Center=�N 	  
Label1Text$	N －��O 	  
Text2Width$O 30Q�P 	  Text2MaxLength$P 2��Q 	  Text2IMEType$	Q Off=�R 	  Text2ValidationType$R Integer=�$S 	  Text2VerticalContentAlignment$S Center=�T 	  
Label2Text$T  ��U 	  Text1U �� *" SearchHeader[得意先コード]+$) TwoWay=�* . + . $, PropertyChanged=�V 	  Text2V �� *SearchHeader[得意先枝番]+$) TwoWay=�* . + . $, PropertyChanged=��� $��入金元販社��$��0��$��3��$- Center=�$. 0,5,0,0q�
�������#��[    -   $ txt入金元販社��$��1��$��3��$ M70_JIS��$ On=�$
��Left=�$��2,0,0,0q� .  W   Label_Visibility$W 	Collapsed=�$ 	Collapsed=�$ 	Collapsed=�$ 40Q�$  3��$	! Off=�$" Integer=�$# Center=�X   
Text2Width$	X 210Q�& . 
% ���� 
$ ���� $' Center=�( �� *(&SearchHeader[入金元販社コード]+$) TwoWay=�* . + . $, PropertyChanged=��� $��0��$��0��$��1,1,1,1q�
���   �$/ 9���� $��1��$��0��$��0,1,1,1q�
���   �$/ 9���� $��2��$��0��$��0,1,1,1q�
���   �$/ 9���� $��3��$��0��$��0,1,1,1q�
���   �$/ 9���� $��0��$��1��$��1,0,1,1q�
���   �$/ 9���� $��1��$��1��$��0,0,1,1q�
���   �$/ 9���� $��2��$��1��$��0,0,1,1q�
���   �$/ 9���� $��3��$��1��$��0,0,1,1q�
���   �$/ 9��/
 
 )GrapeCity.Windows.SpreadGrid.GcSpreadGrid
  -   $��gcSpreadGrid��$��2��$
��Left=�Y 
  AutoFitType$Y CellWithHeader=�Z 
  
LayoutMode$
Z Cell=�[ 
  AutoGenerateColumns[ .  \ 
  HighlightHeader$
\ None=�] 
  RowCount$] 10��^ 
  OperationMode$^ Normal=�_ 
  	Protected_ . ` 
  CanUserSortColumns` .  a 
  ColumnDragMode$a Direct=�$	��950��$	��470��$��80,5,0,0q�b 
  DocumentUri$Kb E/KyoeiSystem.Application.Windows;component/views/DLY/DLY05010G1.sgxml?�c 
  ItemsSourcec �� *SearchDetail+$) TwoWay=�$, PropertyChanged=�* . + . d 
  SelectionBorderUnfocusedd - 
 'GrapeCity.Windows.SpreadGrid.BorderLine  e   Color$e Orange��f   Style$f Thick=�g 
  SelectionBorderg   $e Orange��$f Thick=�h 
  Columnsh ) 
 #GrapeCity.Windows.SpreadGrid.Column  i   Locked$i FalseX�j   	Focusable$
j TrueX�k   CanUserResize$k FalseX�l   HorizontalAlignment$
l LeftX�m   CellTypem 3 
 -GrapeCity.Windows.SpreadGrid.ComboBoxCellType  n   ImeState$	n Off=�o   	DataFieldo 3 
 -GrapeCity.Windows.SpreadGrid.BindingDataField  p   Bindingp �� *金種コード+$) TwoWay=�$, PropertyChanged=�* . + .   $
i TrueX�$
j TrueX�$k FalseX�m / 
 )GrapeCity.Windows.SpreadGrid.TextCellType  3 
 -GrapeCity.Windows.SpreadGrid.EditBaseCellTypeq   
IsReadOnlyq .   $l RightX�$i FalseX�$
j TrueX�$k FalseX�m   r   	MaxLength$r 8��s   MaxLengthUnit$
s Byte=�t   ImeState$	t Off=�o   p �� *金額+$) TwoWay=�$, PropertyChanged=�* . + . $	A #,#��  $l CenterX�$i FalseX�$
j TrueX�$k FalseX�m   $r 10��$
s Byte=�$	t Off=�o   p �� *期日+u �� TargetNullValue$u  ��$A 
yyyy/MM/dd��$) TwoWay=�$, PropertyChanged=�* . + .   $i FalseX�$
j TrueX�$k FalseX�m   $r 40��$
s Byte=�$t On=�o   p �� *摘要+$) TwoWay=�$, PropertyChanged=�* . + . 