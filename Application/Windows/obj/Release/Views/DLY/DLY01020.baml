   M S B A M L   `   `   `  ���� X  TKyoeiSystem.Framework.Windows, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null�[clr-namespace:KyoeiSystem.Framework.Windows.ViewBase;assembly=KyoeiSystem.Framework.Windows&KyoeiSystem.Framework.Windows.ViewBase  �[clr-namespace:KyoeiSystem.Framework.Windows.ViewBase;assembly=KyoeiSystem.Framework.Windows&KyoeiSystem.Framework.Windows.ViewBase  a ]KyoeiSystem.Framework.Windows.Controls, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null�dclr-namespace:KyoeiSystem.Framework.Windows.Controls;assembly=KyoeiSystem.Framework.Windows.Controls&KyoeiSystem.Framework.Windows.Controls Z VKyoeiSystem.Application.Windows, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]3clr-namespace:KyoeiSystem.Application.Windows.Views%KyoeiSystem.Application.Windows.Views R NSystem.Data, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089>.clr-namespace:System.Data;assembly=System.DataSystem.Data 4   .KyoeiSystem.Application.Windows.Views.DLY01020   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      lfw[clr-namespace:KyoeiSystem.Framework.Windows.ViewBase;assembly=KyoeiSystem.Framework.Windows      R	 NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml 	    Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006 	    @d2http://schemas.microsoft.com/expression/blend/2008 	    jvm[clr-namespace:KyoeiSystem.Framework.Windows.ViewBase;assembly=KyoeiSystem.Framework.Windows 	    uctlsdclr-namespace:KyoeiSystem.Framework.Windows.Controls;assembly=KyoeiSystem.Framework.Windows.Controls 	    Buc3clr-namespace:KyoeiSystem.Application.Windows.Views 	    e
 aGrapeCity.WPF.SpreadGrid, Version=1.0.2015.1109, Culture=neutral, PublicKeyToken=5c5c8ff7a6858ccc=sg4http://schemas.grapecity.com/windows/spreadgrid/2012 
 9Data.clr-namespace:System.Data;assembly=System.Data 
 -     � AllowsTransparency  .   � WindowStartupLocation$ Manual=� � Language$ ja-JP�$	��728��$
��1024��$	��768��$
��1024�� � Title$" 仕入返品入力(DLY01020)�� � 	Resources    DataGridHeaderBackground �� EndPoint �� 
StartPoint � Color � Offset  DataGridSummaryBackground��%�   &	        &	 7     �� $	 0,1&�$	 0,0&�.��� $ #3b5998��$ 0Q��� $	 0,1&�$	 0,0&�.��� $ #f0e68c��$ 0Q�.��� 	 � RowDefinitions	 �� $
��auto ��� $
��auto ��� $
��auto ��� $��* �.+�+  %System.Windows.Controls.Ribbon.Ribbon  $��Stretch=�$��Stretch=�.@�.  (System.Windows.Controls.Ribbon.RibbonTab  $��	ホーム��
   KeyTip$
 h��$��75��.@�0  *System.Windows.Controls.Ribbon.RibbonGroup  $��90��  $��F1��   KeyTip$ MASTER��  RibbonFunctionBackground#��[ .@�1  +System.Windows.Controls.Ribbon.RibbonButton  -      LargeImageSource$% ../Images/15マスタ照会.png��$��0,-1,0,0q�   Label$ ﾏｽﾀ照会��   KeyTip$ F1��$��50��  $��F2��$ MASTER��#��[ .@�  -   $% ../Images/16マスタ入力.png��$��0,-1,0,0q�$ ﾏｽﾀ入力��$ F2��$��50��  $��F3��$ MASTER��#��[ .@�  $y�Hidden=�$  ��   ToolTipTitle$  ��   ToolTipDescription$  ��  $��F4��$ MASTER��#��[ .@�  $y�Hidden=�$  ��$  ��$  ��  $��F5��$ MASTER��#��[ .@�  $y�Hidden=�$  ��$  ��$  ��  $��F6��$ MASTER��#��[ .@�  -   $ ../Images/28削除.png��$��0,-1,0,0q�$ 	行削除��$ F6��$��50��  $��F7��$ MASTER��#��[ .@�  $y�Hidden=�$  ��$  ��$  ��  $��F8��$ MASTER��#��[ .@�  $y�Hidden=�$  ��$  ��$  ��  $��F9��$ MASTER��#��[ .@�  -   $ ../Images/24登録.png��$ F9��$��50��$ 登録��$ データ登録(F9)��$- '入力データの登録を行います��   ToolTipImageSource$ ../Images/24登録.png��y���  �� Path$ DataUpdateVisible� �� Mode$ TwoWay=� �� NotifyOnSourceUpdated .  �� NotifyOnTargetUpdated .   $	��F10��$ MASTER��#��[ .@�  -   $ ../Images/26クリア.png��$	 F10��$��50��$ 入力取消��$ 入力取消(F10)��$' !入力の取消しを行います��$ ../Images/26クリア.png��  $	��F11��$ MASTER��#��[ .@�  -   $ ../Images/27終了.png��$	 F11��$��50��$ 終了��$ 終了(F11)��$ 終了します��$ ../Images/27終了.png��  $	��F12��$ MASTER��#��[ .@�  $y�Hidden=�$  ��$  ��$  ��  $��	ヘルプ��
�������.@�  $��サポート��.@�  -   $% ../Images/30マニュアル.png��$ MANUAL��$ ﾏﾆｭｱﾙ��$ 取扱説明書��$* $取り扱い説明書を開きます��  -	   $( "../Images/29遠隔サポート.png��$ SUPPORT��$ お問合せ��$ お問合せ��$< 6Web上の｢お問合せ｣ページに遷移します��  -
   $ ../Images/QandA.png��$	 FAQ��$ QandA��$ QandA��$E ?Web上の｢よくあるご質問｣ページに遷移します��  -   $( "../Images/13共栄Webリンク.png��$ KYOEI��$ 共栄ｼｽﾃﾑ��$ 共栄システムHP��$< 6共栄システムのホームページを開きます��� $��0��$��Bottom=�	 �� $��74 � � ColumnDefinitions �� $��90 ��� $��* �.+�]� $��0��$��0��$��Stretch=�$��2q����� *MaintenanceModeBackground+.+��� $��メイリオ)�$��24(�$��Medium"�$��Stretch=�$��Center=�$��Center=�$��Center=����� *MaintenanceMode+���� *MaintenanceModeForeground+�� $��0��$��1��$��Bottom=�
��� ���
���  ��$��2,0,2,0q����� *ErrorMessage+y��� *ErrorMessageVisibility+� $��1��
�������$��2q�	 �� $
��auto ��� $
��auto ��� $
��auto ��� $��30 � �� $��* ��� $
��auto �.+�� $��0��$��5q�	 �� $��* ��� $��* �.+�� $��0��$��5q� �� $	��180 ��� $
��auto ��� $	��100 ��� $	��100 �.+��� -    ��Name$ 	FuncTitle��$��仕入返品入力��$��0��$��16(�?  9KyoeiSystem.Framework.Windows.Controls.UcLabelTwinTextBox  -     Name$ 
c会社名��   Label_Context$ 	会社名��$��1��$	��350��   DataAccessName$ M70_JIS��   DataAccessMode$ On=�=  7KyoeiSystem.Framework.Windows.Controls.FrameworkControl   	IsKeyItem .    Label_Width$	 100Q�   Label1Visibility$ 	Collapsed=�   Label2Visibility$ 	Collapsed=�    
Text1Width$  40Q�!   Text1MaxLength$! 3��"   Text1IMEType$	" Off=�#   Text1ValidationType$# Integer=�$$   Text1VerticalContentAlignment$$ Center=�%   Text2BorderBrush
% ���� &   Text2Background
& ���� '   Text2IsReadOnly' . $(   Text2VerticalContentAlignment$( Center=�)   Text1) �� *" SearchHeader[会社名コード]+$ TwoWay=� .  . ;  5KyoeiSystem.Framework.Windows.Controls.UcLabelTextBox  $��2��*   Label_Context$* 仕入区分��+   Label_Width$	+ 100Q�$
��Left=�,   cWidth$, 0Q�<  6KyoeiSystem.Framework.Windows.Controls.UcLabelComboBox  -   -  Name$- c仕入区分��$��4��.   Label_Visibility$. 	Collapsed=� .  /   ComboListingParams$,/ &日次,仕入返品入力,仕入区分��0   SelectedIndex$0 1��1   SelectedValue1 �� *SearchHeader[仕入区分]+$ TwoWay=� .  . 2 �� UpdateSourceTrigger$2 PropertyChanged=�� $��1��$��	10,5,10,5q�	 �� $��30 ��� $��27 ��� $��30 ��� $��30 �.+�� $��0�� �� $	��100 ��� $	��100 ��� $	��250 ��� $	��250 ��� $	��100 �.+��� $��返品伝票No.��$��0��$��0��
�������#��[  3 �� TextAlignment$3 Center=�4 �� Padding$4 0,7,0,0q��� $��	返品日��$��0��$��1��
�������#��[  $3 Center=�$4 0,7,0,0q��� $y�Hidden=�$�� ��$��0��$��2��
�������#��[  $3 Center=�$4 0,7,0,0q��� $y�Hidden=�$�� ��$��0��$��3��
�������#��[  $3 Center=�$4 0,7,0,0q��� $y�Hidden=�$�� ��$��0��$��4��
�������#��[  $3 Center=�$4 0,7,0,0q��� $��0��$��0��$��1,1,1,1q�
���   ��� $��1��$��0��$��0,1,1,1q�
���   ��� $��2��$��0��$��0,0,0,0q�
���   ��� $��3��$��0��$��0,0,0,0q�
���   ��� $��4��$��0��$��0,0,0,0q�
���   �� $��1�� �� $	��100 ��� $	��100 ��� $	��250 ��� $	��250 ��� $	��100 �.+�6	  0KyoeiSystem.Framework.Windows.Controls.UcTextBox	  -   5 	 Name$5 c返品伝票番号��$��0��$��1��6 	  cTextAlignment$6 Center=�7 	  
cMaxLength$7 7�� . 
������ 8 	  cBorderThickness$8 0q� 9 	  cVerticalContentAlignment$9 Center=�: 	  cToolTip$Z: T返品伝票No.を入力する事で返品伝票を呼び出す事ができます。��; 	  Text; �� *SearchHeader[伝票番号]+$ TwoWay=� .  . 	  -   $��
c返品日��$��1��$��1��$7 10��$6 Center=� .  < 	  Mask$< 
yyyy/MM/dd��= 	  ValidationType$
= Date=�$8 0q�$9 Center=�; �� $ SearchHeader[仕入日]�$ TwoWay=� .  . > �� StringFormat$> 
yyyy/MM/dd��$2 	LostFocus=��� $��0��$��0��$��1,0,1,1q�
���   ��� $��1��$��0��$��0,0,1,1q�
���   ��� $��2��$��0��$��0,0,0,1q�
���   ��� $��3��$��0��$��0,0,0,1q�
���   ��� $��4��$��0��$��0,0,0,1q�
���   �� $��2�� �� $	��100 ��� $	��100 ��� $	��250 ��� $	��250 ��� $	��100 �.+��� $��	伝票No.��$��2��$��0��
�������#��[  $3 Center=�$4 0,7,0,0q��� $��	仕入日��$��2��$��1��
�������#��[  $3 Center=�$4 0,7,0,0q��� $��仕入先名称��$��2��$��2��
�������#��[  $3 Center=�$4 0,7,0,0q��� $��入荷先名称��$��2��$��3��
�������#��[  $3 Center=�$4 0,7,0,0q��� $��	発注No.��$��2��$��4��
�������#��[  $3 Center=�$4 0,7,0,0q��� $��0��$��2��$��1,0,1,1q�
���   ��� $��1��$��2��$��0,0,1,1q�
���   ��� $��2��$��2��$��0,0,1,1q�
���   ��� $��3��$��2��$��0,0,1,1q�
���   ��� $��4��$��2��$��0,0,1,1q�
���   �� $��3�� �� $	��100 ��� $	��100 ��� $	��250 ��� $	��250 ��� $	��100 �.+�	  -   $5 c伝票番号��$��0��$��3��$6 Center=�$7 7�� . ? 	  cAcceptsTab? .  @ 	  cAcceptsReturn@ .  A 	  	FocusableA .  B 	  cBorderBrush
B ���� $8 0q�$9 Center=�$T: N伝票No.を入力する事で仕入伝票を呼び出す事ができます。��; �� *SearchHeader[元伝票番号]+$ TwoWay=� .  . 	  -   $��
c仕入日��$��3��$��1��$6 Center=�C 	  cIsReadOnlyC . ? .  @ .  
B ���� $8 0q�D 	  cBackground
D ���� $9 Center=�A .  $< 
yyyy/MM/dd��$
= Date=�; �� $  SearchHeader[元仕入日]�$ TwoWay=� .  . $> 
yyyy/MM/dd��$2 	LostFocus=�;
  5KyoeiSystem.Application.Windows.Views.M01_TOK_TextBox
  -   E 
 Name$E 
c仕入先��;  5KyoeiSystem.Application.Windows.Views.M01_TOK_TextBoxF   TextAfterChanged$!F c仕入先_TextAfterChanged��$��3��$��2��G 
  DataAccessName$G M01_TOK��H 
  DataAccessMode$H On=�I 
  LinkItem$	I 1,3��$
��Left=�$��2,-1,0,0q� .  |�.  J 
  Label_Visibility$J 	Collapsed=�K 
  Label1Visibility$K 	Collapsed=�L 
  Label2Visibility$L 	Collapsed=�M 
  
Text1Width$M 40Q�N 
  Text1IsReadOnlyN . O 
  Text1Background
O ���� P 
  Text1BorderThickness$P 0q�$Q 
  Text1VerticalContentAlignment$Q Center=�R 
  Text1AcceptsTabR .  S 
  Text1AcceptsReturnS .  T 
  
Label1Text$	T －��&U 
  Label1VerticalConteintAlignment$U Center=�V 
  
Text2Width$V 30Q�W 
  Text2IsReadOnlyW . X 
  Text2Background
X ���� Y 
  Text2BorderThickness$Y 0q�$Z 
  Text2VerticalContentAlignment$Z Center=�[ 
  Text2AcceptsTab[ .  \ 
  Text2AcceptsReturn\ .  ] 
  
Label2Text$]  ��^ 
  Text1^ �� *" SearchHeader[仕入先コード]+$ TwoWay=� .  . $2 PropertyChanged=�_ 
  Text2_ �� *SearchHeader[仕入先枝番]+$ TwoWay=� .  . $2 PropertyChanged=�  -   $ 
c入荷先��$��3��$��3��$��2,-1,0,0q�$ M70_JIS��$ On=�$��27��`   LabelVisibility$` 	Collapsed=�$ 	Collapsed=�$ 	Collapsed=�|�.  $  40Q�a   Text1IsReadOnlya . b   Text1Background
b ���� c   Text1BorderThickness$c 0q�d   Text1VerticalAlignment$d Center=�e   Text1AcceptsTabe .  f   Text1AcceptsReturnf .  ' . 
& ���� 
% ���� g   Text2VerticalAlignment$g Center=�h   Text2AcceptsTabh .  i   Text2AcceptsReturni .  ) �� *" SearchHeader[入荷先コード]+$ TwoWay=� .  . $2 PropertyChanged=�	  -   $5 c発注番号��$��3��$��4��$��2,0,0,0q�A .  $6 Center=�$8 0q�
D ���� C . $9 Center=�? .  @ .  ; �� *SearchHeader[発注番号]+$ TwoWay=� .  . $2 PropertyChanged=��� $��0��$��2��$��1,0,1,1q�
���   ��� $��1��$��2��$��0,0,1,1q�
���   ��� $��2��$��2��$��0,0,1,1q�
���   ��� $��3��$��2��$��0,0,1,1q�
���   ��� $��4��$��2��$��0,0,1,1q�
���   �/ 
 )GrapeCity.Windows.SpreadGrid.GcSpreadGrid  -   $��gcSpreadGrid��$��1��j   AutoGenerateColumnsj .  k   RowCount$k 10��l   OperationMode$l Normal=�m   
LayoutMode$m Template=�n   CanUserSortColumnsn .  o   ColumnDragMode$o Default=�$��15,5,0,0q�$
��Left=�$
��1000��p   CanUserDragCellp .  q   	Protectedq . $	��Top=�$��50��$	��330��$	��330��r   DocumentUri$Kr E/KyoeiSystem.Application.Windows;component/views/DLY/DLY01020G1.sgxml?�s   ItemsSources �� *SearchDetail+$ TwoWay=� .  . $2 PropertyChanged=�t   SelectionBorderUnfocusedt - 
 'GrapeCity.Windows.SpreadGrid.BorderLine  u   Color$u Orange��v   Style$v Thick=�w   SelectionBorderw   $u Orange��$v Thick=�x   Columnsx ) 
 #GrapeCity.Windows.SpreadGrid.Column  y   Locked$
y TrueX�z   	IsVisiblez .  {   	Focusable${ FalseX�|   CanUserResize$| FalseX�}   CellType} / 
 )GrapeCity.Windows.SpreadGrid.TextCellType  3 
 -GrapeCity.Windows.SpreadGrid.EditBaseCellType~   
IsReadOnly~ .    	DataField 3 
 -GrapeCity.Windows.SpreadGrid.BindingDataField  �   Binding� �� *伝票番号+$ TwoWay=� .  . $2 PropertyChanged=�  $
y TrueX�z .  ${ FalseX�$| FalseX�}   ~ .    � �� *品番コード+$ TwoWay=� .  . $2 PropertyChanged=�  $
y TrueX�$| FalseX�}   �   	MaxLength$� 12���   MaxLengthUnit$
� Byte=��   ImeState$	� Off=��   TextWrapping$� NoWrap=�   � �� *自社品番+$ TwoWay=� .  . $2 PropertyChanged=�  $
y TrueX�$| FalseX�}   ~ .    � �� *自社品名+$ TwoWay=� .  . $2 PropertyChanged=�  $
y TrueX�$| FalseX��   HorizontalAlignment$� CenterX�}   $� 10��$	� Off=�   � �� *賞味期限+� �� TargetNullValue$�  ��$ TwoWay=� .  . $2 PropertyChanged=�  $y FalseX�$| FalseX�} 1 
 +GrapeCity.Windows.SpreadGrid.NumberCellType  �   MinValue$� -9999999.99~��   MaxValue$� 	999999.99~��   SpinButtonVisibility$� NotShow=�   � �� *数量+$ TwoWay=� .  . $2 PropertyChanged=�  $
y TrueX�${ FalseX�$| FalseX�}   ~ .    � �� *単位+$ TwoWay=� .  . $2 PropertyChanged=�  $y FalseX�$| FalseX�}   $� -9999999.99~�$� 99999999.99~�$� NotShow=�   � �� *単価+$ TwoWay=� .  . $2 PropertyChanged=�  $
y TrueX�${ FalseX�$| FalseX�}   ~ . $� NotShow=�   � �� *金額+$ TwoWay=� .  . $2 PropertyChanged=�  $y FalseX�$| FalseX�}   $� On=�$� 40��   � �� *摘要+$ TwoWay=� .  . $2 PropertyChanged=�  $
y TrueX�z .  $| FalseX�}   ~ .    � �� *消費税区分+$ TwoWay=� .  . $2 PropertyChanged=�  $
y TrueX�z .  $| FalseX�}   ~ .    � �� *商品分類+$ TwoWay=� .  . $2 PropertyChanged=�� -   � �Name$� 
FooterArea��$��2��$��15,0,0,0q� � �� 
TargetType� �� Property� �� Value��%/   '��       �� "� ��.��� � � ��
� �   �	 �� $��30 ��� $��30 ��� $��30 ��� $
��auto � �� $	��464 ��� $��85 ��� $	��100 ��� $
��auto �.+��� $��0��$��0��$��1,1,0,1q��� $��1��$��0��$��1,1,0,1q��� $��2��$��0��$��1,1,1,1q��� $��0��$��1��$��1,0,0,1q��� $��1��$��1��$��1,0,0,1q��� $��2��$��1��$��1,0,1,1q��� $��1��$��2��$��1,0,0,1q��� $��2��$��2��$��1,0,1,1q�4  .KyoeiSystem.Framework.Windows.Controls.UcLabel  $��備　　考��$��0��$��0��$3 Center=�$��0,6,0,0q�#��[   -   $ c備考��$��1��$��0��$��2q�$ M11_TEK��$ On=��   Label_Visibility$� 	Collapsed=�$ 	Collapsed=�$ 	Collapsed=�$  40Q��   Text2� �� *SearchHeader[備考]+$ TwoWay=� .  .   $��	小　計��$��0��$��1��$3 Center=�$��0,6,0,0q�#��[ �� -   $ c小計��$��0��$��2��$��Right=�$��Center=�  $��	消費税��$��1��$��1��$3 Center=�$��0,6,0,0q�#��[ �� -   $ 
c消費税��$��1��$��2��$��Right=�$��Center=�  $��最大10行伝票��$��2��$��0��$��Center=�  $��	総合計��$��2��$��1��$3 Center=�$��0,6,0,0q�#��[ �� -   $ 
c総合計��$��2��$��2��$��Right=�$��Center=��� $��0��$��0��$��1,1,0,1q��� $��1��$��0��$��1,1,0,1q��� $��2��$��0��$��1,1,1,1q��� $��0��$��1��$��1,0,0,1q��� $��1��$��1��$��1,0,0,1q��� $��2��$��1��$��1,0,1,1q��� $��1��$��2��$��1,0,0,1q��� $��2��$��2��$��1,0,1,1q�