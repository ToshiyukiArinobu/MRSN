   M S B A M L   `   `   `  ���� X  TKyoeiSystem.Framework.Windows, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null�[clr-namespace:KyoeiSystem.Framework.Windows.ViewBase;assembly=KyoeiSystem.Framework.Windows&KyoeiSystem.Framework.Windows.ViewBase  �[clr-namespace:KyoeiSystem.Framework.Windows.ViewBase;assembly=KyoeiSystem.Framework.Windows&KyoeiSystem.Framework.Windows.ViewBase  a ]KyoeiSystem.Framework.Windows.Controls, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null�dclr-namespace:KyoeiSystem.Framework.Windows.Controls;assembly=KyoeiSystem.Framework.Windows.Controls&KyoeiSystem.Framework.Windows.Controls Z VKyoeiSystem.Application.Windows, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]3clr-namespace:KyoeiSystem.Application.Windows.Views%KyoeiSystem.Application.Windows.Views R NSystem.Data, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089>.clr-namespace:System.Data;assembly=System.DataSystem.Data 4   .KyoeiSystem.Application.Windows.Views.DLY03020   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      lfw[clr-namespace:KyoeiSystem.Framework.Windows.ViewBase;assembly=KyoeiSystem.Framework.Windows      R	 NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml 	    Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006 	    @d2http://schemas.microsoft.com/expression/blend/2008 	    jvm[clr-namespace:KyoeiSystem.Framework.Windows.ViewBase;assembly=KyoeiSystem.Framework.Windows 	    uctlsdclr-namespace:KyoeiSystem.Framework.Windows.Controls;assembly=KyoeiSystem.Framework.Windows.Controls 	    Buc3clr-namespace:KyoeiSystem.Application.Windows.Views 	    e
 aGrapeCity.WPF.SpreadGrid, Version=1.0.2015.1109, Culture=neutral, PublicKeyToken=5c5c8ff7a6858ccc=sg4http://schemas.grapecity.com/windows/spreadgrid/2012 
 9Data.clr-namespace:System.Data;assembly=System.Data 
 -   $	��728��$
��1024��$	��768��$
��1024��  � Left$  0��
 � Top$ 0�� � WindowStartupLocation$ Manual=� � AllowsTransparency .   � Language$ ja-JP� � Title$" 売上返品入力(DLY03020)�� � 	Resources    DataGridHeaderBackground �� EndPoint �� 
StartPoint	 � Color
 � Offset  DataGridSummaryBackground��%�   &	        &	 7     �� $	 0,1&�$	 0,0&�.��� $	 #3b5998��$
 0Q��� $	 0,1&�$	 0,0&�.��� $	 #f0e68c��$
 0Q�.���  � RowDefinitions �� $
��auto ��� $
��auto ��� $��* �.+�+  %System.Windows.Controls.Ribbon.Ribbon  $��Stretch=�$��Stretch=�.@�.  (System.Windows.Controls.Ribbon.RibbonTab  $��	ホーム��   KeyTip$ h��$��75��.@�0  *System.Windows.Controls.Ribbon.RibbonGroup  $��90��  $��F1��   KeyTip$ MASTER��  RibbonFunctionBackground#��[ .@�1  +System.Windows.Controls.Ribbon.RibbonButton  -      LargeImageSource$% ../Images/15マスタ照会.png��$��0,0,0,0q�   ToolTipTitle$ マスタ照会(F1)��   ToolTipDescription$3 -入力済みのデータを検索します。��   ToolTipImageSource$% ../Images/15マスタ照会.png��   Label$ ﾏｽﾀ照会��   KeyTip$ F1��$��50��  $��F2��$ MASTER��#��[ .@�  -   $% ../Images/16マスタ入力.png��$��0,0,0,0q�$ ﾏｽﾀ入力��$ F2��$��50��$ マスタ入力(F2)��$- 'マスタの入力画面を開きます��$% ../Images/16マスタ入力.png��  $��F3��$ MASTER��#��[ .@�  $y�Hidden=�$  ��$  ��$  ��  $��F4��$ MASTER��#��[ .@�  $y�Hidden=�$  ��$  ��$  ��  $��F5��$ MASTER��#��[ .@�  $y�Hidden=�$  ��$  ��$  ��  $��F6��$ MASTER��#��[ .@�  -   $ ../Images/28削除.png��$ F6��$��50��$ 	行削除��$ 行削除(F6)��$0 *選択中のリスト行を削除します��$ ../Images/28削除.png��  $��F7��$ MASTER��#��[ .@�  $y�Hidden=�$  ��$  ��$  ��  $��F8��$ MASTER��#��[ .@�  $y�Hidden=�$  ��$  ��$  ��  $��F9��$ MASTER��#��[ .@�  -   $ ../Images/24登録.png��$ F9��$��50��$ 登録��$ データ登録(F9)��$- '入力データの登録を行います��$ ../Images/24登録.png��  $	��F10��$ MASTER��#��[ .@�  -   $ ../Images/26クリア.png��$	 F10��$��50��$ 入力取消��$ 入力取消(F10)��$' !入力の取消しを行います��$ ../Images/26クリア.png��  $	��F11��$ MASTER��#��[ .@�  -   $ ../Images/27終了.png��$	 F11��$��50��$ 終了��$ 終了(F11)��$ 終了します��$ ../Images/27終了.png��  $	��F12��$ MASTER��#��[ .@�  $y�Hidden=�$  ��$  ��$  ��  $��	ヘルプ��
�������.@�  $��サポート��.@�  -   $% ../Images/30マニュアル.png��$ MANUAL��$ ﾏﾆｭｱﾙ��$ 取扱説明書��$* $取り扱い説明書を開きます��  -	   $( "../Images/29遠隔サポート.png��$ SUPPORT��$ お問合せ��$ お問合せ��$< 6Web上の｢お問合せ｣ページに遷移します��  -
   $ ../Images/QandA.png��$	 FAQ��$ QandA��$ QandA��$E ?Web上の｢よくあるご質問｣ページに遷移します��  -   $( "../Images/13共栄Webリンク.png��$ KYOEI��$ 共栄ｼｽﾃﾑ��$ 共栄システムHP��$< 6共栄システムのホームページを開きます��� $��0��$��Bottom=� �� $��74 � � ColumnDefinitions �� $��90 ��� $��* �.+�]� $��0��$��0��$��Stretch=�$��2q����� *MaintenanceModeBackground+.+��� $��メイリオ)�$��24(�$��Medium"�$��Stretch=�$��Center=�$��Center=�$��Center=����� *MaintenanceMode+���� *MaintenanceModeForeground+�� $��0��$��1��$��Bottom=�
��� ���
���  ��$��2,0,2,0q����� *ErrorMessage+y��� *ErrorMessageVisibility+� $��1��
�������$��Stretch=�$��Stretch=� �� $��* �.+�� -    �Name$ FuncDisplay�� �� $
��auto ��� $
��auto ��� $
��auto ��� $
��auto ��� $��30 � �� $
��auto �.+�� -   $��	grdHeader��$��0��$��2��$��	20,10,0,5q� �� $��27 ��� $��10 ��� $
��auto ��� $��* � �� $	��100 ��� $
��auto ��� $
��auto �.+��� -    ��Name$ 	FuncTitle��$��売上入力��$��0��$��0��$��16(�$��30,0,0,0q�?  9KyoeiSystem.Framework.Windows.Controls.UcLabelTwinTextBox  -     Name$ txt自社名��   Label_Context$ 	自社名��$��1��$	��350��   DataAccessName$ M70_JIS��   DataAccessMode$ On=�=  7KyoeiSystem.Framework.Windows.Controls.FrameworkControl   	IsKeyItem .    Label_Width$	 100Q�   Label1Visibility$ 	Collapsed=�   Label2Visibility$ 	Collapsed=�   
Text1Width$ 40Q�    Text1MaxLength$  3��!   Text1IMEType$	! Off=�"   Text1ValidationType$" Integer=�$#   Text1VerticalContentAlignment$# Center=�$   Text2BorderBrush
$ ���� %   Text2Background
% ���� &   Text2IsReadOnly& . $'   Text2VerticalContentAlignment$' Center=�<  6KyoeiSystem.Framework.Windows.Controls.UcLabelComboBox  -   (  Name$( cmb伝票要否��$��0��$��2��)   Label_Context$) 伝票要否��*   Label_Width$	* 100Q�$	��250��+   ComboListingParams$&+  日次,売上入力,伝票要否��,   SelectedValue, �� *SearchHeader[伝票要否]+- �� Mode$- TwoWay=�. �� NotifyOnSourceUpdated. . / �� NotifyOnTargetUpdated/ . 0 �� UpdateSourceTrigger$0 PropertyChanged=�� -   $ gHeader��$��2��$��0��$��3��$��60,0,0,0q� �� $��27 ��� $��27 ��� $��27 ��� $��27 ��� $��27 ��� $��27 �.+�� $��0�� �� $	��100 ��� $��80 ��� $	��210 ��� $	��210 ��� $	��210 ��� $
��auto �.+��� $��返品伝票No.��$��0��1 �� TextAlignment$1 Center=�2 �� Padding$2 0,5,0,0q�
�������#��[  �� $��	返品日��$��1��$1 Center=�$2 0,5,0,0q�
�������#��[  �� $��売上区分��$��2��$1 Center=�$2 0,5,0,0q�
�������#��[  �� $��0��$��1,1,1,1q�
���   ��� $��1��$��0,1,1,1q�
���   ��� $��2��$��0,1,1,1q�
���   ��� $��3��$��0,0,0,0q�
���   ��� $��4��$��0,0,0,0q�
���   �� $��1�� �� $	��100 ��� $��80 ��� $	��210 ��� $	��210 ��� $	��210 ��� $
��auto �.+�6  0KyoeiSystem.Framework.Windows.Controls.UcTextBox  -   3  Name$3 txt返品伝票番号��$��1��$��0��4   ImeType$	4 Off=�5   ValidationType$5 Integer=� . 6   cTextAlignment$6 Center=�7   
cMaxLength$7 7��8   cBorderThickness$8 0q� 9   cVerticalContentAlignment$9 Center=�:   Text: �� *SearchHeader[伝票番号]+$- TwoWay=�. . / . $0 PropertyChanged=�  -   $��txt返品日��$��1�� .  ;   Mask$; 
yyyy/MM/dd��$
5 Date=�$7 10��$6 Center=�$8 0q�$9 Center=�: �� < �� Path$< SearchHeader[売上日]�$- TwoWay=�/ . . . = �� StringFormat$= 
yyyy/MM/dd��$0 	LostFocus=�  -   $( cmb売上区分��$��1��$��2��>   	IsEnabled> .  ?   Label_Visibility$? 	Collapsed=�$,+ &日次,売上返品入力,売上区分��, �� *SearchHeader[売上区分]+$- TwoWay=�. . / . $0 PropertyChanged=��� $��0��$��1,0,1,1q�
���   ��� $��1��$��0,0,1,1q�
���   ��� $��2��$��0,0,1,1q�
���   ��� $��3��$��0,0,0,1q�
���   ��� $��4��$��0,0,0,1q�
���   �� $��2�� �� $	��100 ��� $��80 ��� $	��210 ��� $	��210 ��� $	��210 ��� $
��auto �.+��� $��	伝票No.��$��0��$1 Center=�$2 0,5,0,0q�
�������#��[  �� $��	出荷日��$��1��$1 Center=�$2 0,5,0,0q�
�������#��[  �� $��仕入先名称��$��2��$1 Center=�$2 0,5,0,0q�
�������#��[  �� $��得意先名称��$��3��$1 Center=�$2 0,5,0,0q�
�������#��[  �� $��出荷先名称��$��4��$1 Center=�$2 0,5,0,0q�
�������#��[  �� $��0��$��1,0,1,1q�
���   ��� $��1��$��0,0,1,1q�
���   ��� $��2��$��0,0,1,1q�
���   ��� $��3��$��0,0,1,1q�
���   ��� $��4��$��0,0,1,1q�
���   �� $��3�� �� $	��100 ��� $��80 ��� $	��210 ��� $	��210 ��� $	��210 ��� $
��auto �.+�  -   $3 txt伝票番号��$��0��$	4 Off=�$5 Integer=� . @   	Focusable@ .  $6 Center=�$7 7��$8 0q�$9 Center=�A   cAcceptsReturnA .  B   cAcceptsTabB .  : �� *SearchHeader[元伝票番号]+$- TwoWay=�. . / . $0 PropertyChanged=�  -   $3 txt出荷日��$��1�� .  $; 
yyyy/MM/dd��$
5 Date=�@ .  C   	IsEnabledC .  $7 10��$6 Center=�D   cBackground
D ���� $8 0q�$9 Center=�A .  B .  : �� $< SearchHeader[出荷日]�$- TwoWay=�/ . . . $= 
yyyy/MM/dd��$0 	LostFocus=�;	  5KyoeiSystem.Application.Windows.Views.M01_TOK_TextBox	  -   E 	 Name$E txt仕入先��$��2��F 	  DataAccessName$F M01_TOK��G 	  DataAccessMode$G On=�H 	  LinkItem$	H 1,3��$
��Left=�$��	-30,0,0,0q�|�.  I 	  Label_Visibility$I 	Collapsed=�J 	  Label1Visibility$J 	Collapsed=�K 	  Label2Visibility$K 	Collapsed=�L 	  Text1Visibility$L 	Collapsed=�M 	  
Text1Width$M 0Q�N 	  Text1MaxLength$N 4��O 	  Text1IMEType$	O Off=�P 	  Text1ValidationType$P Integer=�$Q 	  Text1VerticalContentAlignment$Q Center=�R 	  Text1AcceptsTabR .  S 	  Text1AcceptsReturnS .  T 	  Text2Visibility$T 	Collapsed=�U 	  
Text2Width$U 0Q�V 	  Text2MaxLength$V 2��W 	  Text2IMEType$	W Off=�X 	  Text2ValidationType$X Integer=�$Y 	  Text2VerticalContentAlignment$Y Center=�Z 	  Text2AcceptsTabZ .  [ 	  Text2AcceptsReturn[ .  \ 	  
Label2Text$\  ��] 	  Text1] �� *" SearchHeader[仕入先コード]+$- TwoWay=�. . / . $0 PropertyChanged=�^ 	  Text2^ �� *SearchHeader[仕入先枝番]+$- TwoWay=�. . / . $0 PropertyChanged=�	  -   $E txt得意先��;
  5KyoeiSystem.Application.Windows.Views.M01_TOK_TextBox_ 
  	LostFocus$_ txt得意先_LostFocus��` 
  TextAfterChanged$#` txt得意先_TextAfterChanged��$��3��$F M01_TOK��$G On=�$	H 0,3��$
��Left=�$��	-30,0,0,0q�|�.  $I 	Collapsed=�$J 	Collapsed=�$K 	Collapsed=�$L 	Collapsed=�$N 4��$	O Off=�$P Integer=�$Q Center=�R .  S .  $T 	Collapsed=�$V 2��$	W Off=�$X Integer=�$Y Center=�Z .  [ .  $\  ��] �� *" SearchHeader[得意先コード]+$- TwoWay=�. . / . $0 PropertyChanged=�^ �� *SearchHeader[得意先枝番]+$- TwoWay=�. . / . $0 PropertyChanged=�  -   $ txt出荷先��$��4��$ M21_SYUK��$ On=�$
��Left=�$��	-25,0,0,0q� .  |�.  a   Label_Visibility$a 	Collapsed=�$ 	Collapsed=�$ 	Collapsed=�b   Text1Visibility$b 	Collapsed=�$ 0Q�$  15��$	! Off=�$# Center=�c   Text1AcceptsTabc .  d   
Text2Width$	d 210Q�& . 
% ���� 
$ ���� $' Center=�e   Text2Foreground
e �   �f   Text1f �� *" SearchHeader[出荷先コード]+$- TwoWay=�. . / . $0 PropertyChanged=��� $��0��$��1,0,1,1q�
���   ��� $��1��$��0,0,1,1q�
���   ��� $��2��$��0,0,1,1q�
���   ��� $��3��$��0,0,1,1q�
���   ��� $��4��$��0,0,1,1q�
���   �� $��4�� �� $	��100 ��� $��80 ��� $	��210 ��� $	��210 ��� $	��210 ��� $
��auto �.+��� $��納品伝票No.��$��0��$1 Center=�$2 0,5,0,0q�
�������#��[  �� $��	売上日��$��1��$1 Center=�$2 0,5,0,0q�
�������#��[  �� $��	受注No.��$��2��$1 Center=�$2 0,5,0,0q�
�������#��[  �� $��出荷元名称��$��3��$1 Center=�$2 0,5,0,0q�
�������#��[  �� $��在庫倉庫名称��$��4��$1 Center=�$2 0,5,0,0q�
�������#��[  �� $��0��$��1,0,1,1q�
���   ��� $��1��$��0,0,1,1q�
���   ��� $��2��$��0,0,1,1q�
���   ��� $��3��$��0,0,1,1q�
���   ��� $��4��$��0,0,1,1q�
���   �� $��5�� �� $	��100 ��� $��80 ��� $	��210 ��� $	��210 ��� $	��210 ��� $
��auto �.+�  -   $3 txt納品伝票番号��$��0��$	4 Off=�$5 Integer=� .  @ .  C .  $6 Center=�$7 7��$8 0q�
D ���� $9 Center=�g   cIsReadOnlyg . : �� *" SearchHeader[納品伝票番号]+$- TwoWay=�. . / . $0 PropertyChanged=�  -   $��txt売上日��$��1�� .  $; 
yyyy/MM/dd��$
5 Date=�@ .  C .  $7 10��$6 Center=�$8 0q�
D ���� $9 Center=�B .  A .  g . : �� $ < SearchHeader[元売上日]�$- TwoWay=�/ . . . $= 
yyyy/MM/dd��$0 	LostFocus=�  -   $3 txt受注番号��$��2��$	4 Off=�$5 Integer=�@ .   .  C .  $6 Center=�$7 10��$8 0q�
D ���� $9 Center=�g . : �� *SearchHeader[受注番号]+$- TwoWay=�. . / . $0 PropertyChanged=�  -   $ txt出荷元��$��3��$ M70_JIS��$ On=�|�.  $
��Left=�$��	-25,0,0,0q� .  $a 	Collapsed=�$ 	Collapsed=�$ 	Collapsed=�$b 	Collapsed=�$ 0Q�$	! Off=�$" Integer=�$# Center=�$	d 210Q�& . 
% ���� 
$ ���� $' Center=�
e �   �f �� *" SearchHeader[出荷元コード]+$- TwoWay=�. . / . $0 PropertyChanged=�  -   $ txt在庫倉庫��$��4��$ M22_SOUK��$ On=�|�.  $
��Left=�$��	-35,0,0,0q� .  $a 	Collapsed=�$ 	Collapsed=�$ 	Collapsed=�$b 	Collapsed=�$ 40Q�$  3��$	! Off=�$" Integer=�$# Center=�$	d 210Q�& . 
% ���� 
$ ���� $' Center=�
e �   �f �� *%#SearchHeader[在庫倉庫コード]+$- TwoWay=�. . / . $0 PropertyChanged=��� $��0��$��1,0,1,1q�
���   ��� $��1��$��0,0,1,1q�
���   ��� $��2��$��0,0,1,1q�
���   ��� $��3��$��0,0,1,1q�
���   ��� $��4��$��0,0,1,1q�
���   �/ 
 )GrapeCity.Windows.SpreadGrid.GcSpreadGrid  -   $��gcSpreadGrid��$��2��$
��Left=�h   AutoFitType$h CellWithHeader=�i   
LayoutMode$i Template=�j   AutoGenerateColumnsj .  k   HighlightHeader$
k None=�l   RowCount$l 10��m   OperationMode$m Normal=�n   	Protectedn . o   CanUserSortColumnso .  p   ColumnDragMode$p Direct=�$	��950��$	��290��$��80,5,0,0q�q   DocumentUri$Kq E/KyoeiSystem.Application.Windows;component/views/DLY/DLY03020G1.sgxml?�r   ItemsSourcer �� *SearchDetail+$- TwoWay=�$0 PropertyChanged=�. . / . s   SelectionBorderUnfocuseds - 
 'GrapeCity.Windows.SpreadGrid.BorderLine  t   Color$t Orange��u   Style$u Thick=�v   SelectionBorderv   $t Orange��$u Thick=�w   Columnsw ) 
 #GrapeCity.Windows.SpreadGrid.Column  x   Locked$
x TrueX�y   	Focusable$
y TrueX�z   CanUserResize$z FalseX�{   CellType{ / 
 )GrapeCity.Windows.SpreadGrid.TextCellType  |   	MaxLength$| 12��}   MaxLengthUnit$
} Byte=�~   ImeState$	~ Off=�   	DataField 3 
 -GrapeCity.Windows.SpreadGrid.BindingDataField  �   Binding� �� *自社品番+$- TwoWay=�$0 PropertyChanged=�. . / .   $
x TrueX�$
y TrueX�$z FalseX�{   $| 50��$
} Byte=�$	~ Off=�   � �� *得意先品番コード+$- TwoWay=�$0 PropertyChanged=�. . / .   $
x TrueX�$
y TrueX�$z FalseX�{   3 
 -GrapeCity.Windows.SpreadGrid.EditBaseCellType�   
IsReadOnly� .    � �� *自社品名+$- TwoWay=�$0 PropertyChanged=�. . / .   �   HorizontalAlignment$� CenterX�$
x TrueX�$
y TrueX�$z FalseX�{   $| 10��$
} Byte=�$	~ Off=�   � �� *賞味期限+� �� TargetNullValue$�  ��$= 
yyyy/MM/dd��$- TwoWay=�$0 PropertyChanged=�. . / .   $� RightX�$x FalseX�$
y TrueX�$z FalseX�{ 1 
 +GrapeCity.Windows.SpreadGrid.NumberCellType  �   MaxValue$� 	999999.99~��   MinValue$� 
-999999.99~��   SpinButtonVisibility$� NotShow=��   FieldSetF 
 @GrapeCity.Windows.SpreadGrid.Editors.NumberFieldSetTypeConverter$� ###,##0.##,,,-,    � �� *数量+$- TwoWay=�$0 PropertyChanged=�. . / .   $
x TrueX�$y FalseX�$z FalseX�{   $| 2��$
} Byte=�   � �� *単位+$- TwoWay=�$0 PropertyChanged=�. . / .   $� RightX�$x FalseX�$
y TrueX�$z FalseX�{   $� 99999999.99~�$� -99999999.99~�$� NotShow=�$� ##,###,##0.##,,,-,    � �� *単価+$- TwoWay=�$0 PropertyChanged=�. . / .   $� RightX�$
x TrueX�$y FalseX�$z FalseX�{   $� 99999999~�$� 	-99999999~�$� NotShow=�$� ###,###,##0,,,-,    � �� *金額+$- TwoWay=�$0 PropertyChanged=�. . / .   $x FalseX�$
y TrueX�$z FalseX�{   $| 40��$
} Byte=�$~ On=�   � �� *摘要+$- TwoWay=�$0 PropertyChanged=�. . / .   $
x TrueX�$
y TrueX�$z FalseX�$� CenterX�{ 3 
 -GrapeCity.Windows.SpreadGrid.CheckBoxCellType     � �� *マルセン仕入+$- TwoWay=�$0 PropertyChanged=�. . / .   $
x TrueX�$y FalseX��   	IsVisible� .  �   Width$� 0Q�{   � .    � �� *品番コード+  $
x TrueX�$y FalseX�� .  $� 0Q�{   � .    � �� *消費税区分+  $
x TrueX�$y FalseX�� .  $� 0Q�{   � .    � �� *商品分類+� -    $ 
FooterArea��$��3��$��	80,0,0,15q� � �� 
TargetType� �� Property� �� Value��%/   '��       �� "� ��.��� � � ��
� �   � �� $��30 ��� $��30 ��� $��30 ��� $��* � �� $	��465 ��� $��84 ��� $��84 ��� $
��auto �.+�4  .KyoeiSystem.Framework.Windows.Controls.UcLabel  $��備　　考��$��0��$��0��$1 Center=�$��0,6,0,0q�#��[   -!   $ 	txt備考��$��1��$��0��$��2q�$ M11_TEK��$ On=�$a 	Collapsed=�$ 	Collapsed=�$ 	Collapsed=�$ 40Q�$  5���   Text2MaxLength$� 40���   Text2IMEType$� Native=��   Text2� �� *SearchHeader[備考]+$- TwoWay=�. . / .   $��	小　計��$��0��$��1��$1 Center=�$��0,6,0,0q�#��[   $��	消費税��$��1��$��1��$1 Center=�$��0,6,0,0q�#��[   $��	総合計��$��2��$��1��$1 Center=�$��0,6,0,0q�#��[ �� -"   � ��Name$� 	lbl小計��$��0��$��2��$��Right=�$��0,6,5,0q��� -#   $� lbl消費税��$��1��$��2��$��Right=�$��0,6,5,0q��� -$   $� lbl総合計��$��2��$��2��$��Right=�$��0,6,5,0q��� $��0��$��0��$��1,1,0,1q��� $��1��$��0��$��1,1,0,1q��� $��2��$��0��$��1,1,1,1q��� $��0��$��1��$��1,0,0,1q��� $��1��$��1��$��1,0,0,1q��� $��2��$��1��$��1,0,1,1q��� $��1��$��2��$��1,0,0,1q��� $��2��$��2��$��1,0,1,1q�