   M S B A M L   `   `   `  ���� X  TKyoeiSystem.Framework.Windows, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null�[clr-namespace:KyoeiSystem.Framework.Windows.ViewBase;assembly=KyoeiSystem.Framework.Windows&KyoeiSystem.Framework.Windows.ViewBase  �[clr-namespace:KyoeiSystem.Framework.Windows.ViewBase;assembly=KyoeiSystem.Framework.Windows&KyoeiSystem.Framework.Windows.ViewBase  a ]KyoeiSystem.Framework.Windows.Controls, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null�dclr-namespace:KyoeiSystem.Framework.Windows.Controls;assembly=KyoeiSystem.Framework.Windows.Controls&KyoeiSystem.Framework.Windows.Controls R NSystem.Data, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089>.clr-namespace:System.Data;assembly=System.DataSystem.Data # KyoeiSystem.Application.Windows4   .KyoeiSystem.Application.Windows.Views.DLY01010   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      lfw[clr-namespace:KyoeiSystem.Framework.Windows.ViewBase;assembly=KyoeiSystem.Framework.Windows      R	 NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml 	    Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006 	    @d2http://schemas.microsoft.com/expression/blend/2008 	    jvm[clr-namespace:KyoeiSystem.Framework.Windows.ViewBase;assembly=KyoeiSystem.Framework.Windows 	    uctlsdclr-namespace:KyoeiSystem.Framework.Windows.Controls;assembly=KyoeiSystem.Framework.Windows.Controls 	    e
 aGrapeCity.WPF.SpreadGrid, Version=1.0.2015.1109, Culture=neutral, PublicKeyToken=5c5c8ff7a6858ccc=sg4http://schemas.grapecity.com/windows/spreadgrid/2012 
 9Data.clr-namespace:System.Data;assembly=System.Data 
 -     � AllowsTransparency  .   � WindowStartupLocation$ Manual=� � Language$ ja-JP�$	��728��$
��1024��$	��768��$
��1024�� � Title$ 仕入入力(DLY01010)�� � 	Resources    DataGridHeaderBackground �� EndPoint �� 
StartPoint � Color � Offset  DataGridSummaryBackground��%�   &	        &	 7     �� $	 0,1&�$	 0,0&�.��� $ #3b5998��$ 0Q��� $	 0,1&�$	 0,0&�.��� $ #f0e68c��$ 0Q�.��� 	 � RowDefinitions	 �� $
��auto ��� $
��auto ��� $
��auto ��� $��* �.+�+  %System.Windows.Controls.Ribbon.Ribbon  $��Stretch=�$��Stretch=�.@�.  (System.Windows.Controls.Ribbon.RibbonTab  $��	ホーム��
   KeyTip$
 h��$��75��.@�0  *System.Windows.Controls.Ribbon.RibbonGroup  $��90��  $��F1��   KeyTip$ MASTER��  RibbonFunctionBackground#��[ .@�1  +System.Windows.Controls.Ribbon.RibbonButton  -      LargeImageSource$% ../Images/15マスタ照会.png��$��0,-1,0,0q�   Label$ ﾏｽﾀ照会��   KeyTip$ F1��$��50��  $��F2��$ MASTER��#��[ .@�  -   $% ../Images/16マスタ入力.png��$��0,-1,0,0q�$ ﾏｽﾀ入力��$ F2��$��50��  $��F3��$ MASTER��#��[ .@�  $y�Hidden=�$  ��   ToolTipTitle$  ��   ToolTipDescription$  ��  $��F4��$ MASTER��#��[ .@�  $y�Hidden=�$  ��$  ��$  ��  $��F5��$ MASTER��#��[ .@�  -   $ ../Images/行追加.png��$��0,-1,0,0q�$ 	行追加��$ F5��$��50��  $��F6��$ MASTER��#��[ .@�  -   $ ../Images/28削除.png��$��0,-1,0,0q�$ 	行削除��$ F6��$��50��  $��F7��$ MASTER��#��[ .@�  $y�Hidden=�$  ��$  ��$  ��  $��F8��$ MASTER��#��[ .@�  $y�Hidden=�$  ��$  ��$  ��  $��F9��$ MASTER��#��[ .@�  -   $ ../Images/24登録.png��$ F9��$��50��$ 登録��$ データ登録(F9)��$- '入力データの登録を行います��   ToolTipImageSource$ ../Images/24登録.png��y���  �� Path$ DataUpdateVisible� �� Mode$ TwoWay=� �� NotifyOnSourceUpdated .  �� NotifyOnTargetUpdated .   $	��F10��$ MASTER��#��[ .@�  -   $ ../Images/26クリア.png��$	 F10��$��50��$ 入力取消��$ 入力取消(F10)��$' !入力の取消しを行います��$ ../Images/26クリア.png��  $	��F11��$ MASTER��#��[ .@�  -   $ ../Images/27終了.png��$	 F11��$��50��$ 終了��$ 終了(F11)��$ 終了します��$ ../Images/27終了.png��  $	��F12��$ MASTER��#��[ .@�  -	   $ ../Images/28削除.png��$	 F12��$��50��$ 削除��$ 削除(F12)��$- '表示中のデータを削除します��$ ../Images/28削除.png��  $��	ヘルプ��
�������.@�  $��サポート��.@�  -
   $% ../Images/30マニュアル.png��$ MANUAL��$ ﾏﾆｭｱﾙ��$ 取扱説明書��$* $取り扱い説明書を開きます��  -   $( "../Images/29遠隔サポート.png��$ SUPPORT��$ お問合せ��$ お問合せ��$< 6Web上の｢お問合せ｣ページに遷移します��  -   $ ../Images/QandA.png��$	 FAQ��$ QandA��$ QandA��$E ?Web上の｢よくあるご質問｣ページに遷移します��  -   $( "../Images/13共栄Webリンク.png��$ KYOEI��$ 共栄ｼｽﾃﾑ��$ 共栄システムHP��$< 6共栄システムのホームページを開きます��� $��0��$��Bottom=�	 �� $��74 � � ColumnDefinitions �� $��90 ��� $��* �.+�]� $��0��$��0��$��Stretch=�$��2q����� *MaintenanceModeBackground+.+��� $��メイリオ)�$��24(�$��Medium"�$��Stretch=�$��Center=�$��Center=�$��Center=����� *MaintenanceMode+���� *MaintenanceModeForeground+�� $��0��$��1��$��Bottom=�
��� ���
���  ��$��2,0,2,0q����� *ErrorMessage+y��� *ErrorMessageVisibility+� $��1��
�������$��2q�	 �� $
��auto ��� $
��auto ��� $
��auto ��� $��30 � �� $��* ��� $
��auto �.+�� $��0��$��5q�	 �� $��* ��� $��* �.+�� $��0��$��5q� �� $	��180 ��� $
��auto �.+��� -    ��Name$ 	FuncTitle��$��仕入入力��$��0��$��16(�?  9KyoeiSystem.Framework.Windows.Controls.UcLabelTwinTextBox  -     Name$ 
c会社名��   Label_Context$ 	会社名��$��1��$	��350��   DataAccessName$ M70_JIS��   DataAccessMode$ On=�=  7KyoeiSystem.Framework.Windows.Controls.FrameworkControl   	IsKeyItem .    Label_Width$	 100Q�   Label1Visibility$ 	Collapsed=�   Label2Visibility$ 	Collapsed=�    
Text1Width$  40Q�!   Text1MaxLength$! 3��"   Text1IMEType$	" Off=�#   Text1ValidationType$# Integer=�$$   Text1VerticalContentAlignment$$ Center=�%   Text2BorderBrush
% ���� &   Text2Background
& ���� '   Text2IsReadOnly' . $(   Text2VerticalContentAlignment$( Center=�� $��1��$��	10,5,10,5q�	 �� $��30 ��� $��27 ��� $��30 ��� $��30 � �� $	��100 ��� $	��100 ��� $	��150 ��� $��* ��� $��* �.+��� $��	伝票No.��$��0��$��0��
�������#��[  ) �� TextAlignment$) Center=�* �� Padding$* 0,7,0,0q��� $��	仕入日��$��0��$��1��
�������#��[  $) Center=�$* 0,7,0,0q��� $��仕入区分��$��0��$��2��
�������#��[  $) Center=�$* 0,7,0,0q��� $��仕入先名称��$��0��$��3��
�������#��[  $) Center=�$* 0,7,0,0q��� $��入荷先名称��$��0��$��4��
�������#��[  $) Center=�$* 0,7,0,0q�6  0KyoeiSystem.Framework.Windows.Controls.UcTextBox  -   +  Name$+ c伝票番号��$��0��$��1��,   cTextAlignment$, Center=�-   
cMaxLength$- 7�� . 
������ .   cBorderThickness$. 0q� /   cVerticalContentAlignment$/ Center=�0   Text0 �� *SearchHeader[伝票番号]+$ TwoWay=� .  . ;  5KyoeiSystem.Framework.Windows.Controls.UcLabelTextBox  -   $��
c仕入日��$��1��$��1��1   
cMaxLength$1 10��2   cTextAlignment$2 Center=� .  3   Mask$3 
yyyy/MM/dd��4   cValidationType$
4 Date=� 5   cVerticalContentAlignment$5 Center=�$��0q�6   Label_Width$6 0Q�$��-1,-1,-2,-1q�7   Text7 �� $ SearchHeader[仕入日]�$ TwoWay=� .  . 8 �� StringFormat$8 
yyyy/MM/dd��9 �� UpdateSourceTrigger$9 	LostFocus=�<	  6KyoeiSystem.Framework.Windows.Controls.UcLabelComboBox	  -   : 	 Name$: c仕入区分��$��2��$��1��; 	  Label_Visibility$; 	Collapsed=� .  < 	  ComboListingParams$&<  日次,仕入入力,仕入区分��= 	  SelectedValue= �� *SearchHeader[仕入区分]+$ TwoWay=� .  . $9 PropertyChanged=�� $��3��$��1�� �� $	��110 ��� $��* �.+�  -   $ 
c仕入先��$��0��$
��Left=�$ M01_TOK��$	 Off=�>   LinkItem$	> 1,3��$��2,0,0,0q�?   LabelVisibility$? 	Collapsed=�$ 	Collapsed=�$ 	Collapsed=�$  40Q�$! 4��$	" Off=�$# Integer=�$$ Center=�@   
Label1Text$	@ －��A   
Text2Width$A 30Q�B   Text2MaxLength$B 2��C   Text2IMEType$	C Off=�D   Text2ValidationType$D Integer=�$( Center=�E   Text1E �� *" SearchHeader[仕入先コード]+$ TwoWay=� .  . $9 PropertyChanged=�F   Text2F �� *SearchHeader[仕入先枝番]+$ TwoWay=� .  . $9 PropertyChanged=��� -   $ c仕入先名��$��1��
�������$��0q�$��0,1,0,1q�  -   $ 
c入荷先��$��4��$��1��$��2,0,0,0q�$ M70_JIS��$ On=�$? 	Collapsed=�$ 	Collapsed=�$ 	Collapsed=�$  40Q�$! 3��$	" Off=�$# Integer=�$$ Center=�' . 
& ���� 
% ���� $( Center=�E �� *" SearchHeader[入荷先コード]+$ TwoWay=� .  . $9 PropertyChanged=��� $��	発注No.��$��2��$��0��
�������#��[  $) Center=�$* 0,7,0,0q�  -   $+ c発注番号��$��3��$��0��$, Center=�$- 7��$. 0q�$/ Center=�0 �� *SearchHeader[発注番号]+$ TwoWay=� .  . $9 PropertyChanged=��� $��0��$��0��$��1,1,0,0q�
���   ��� $��1��$��0��$��1,1,0,0q�
���   ��� $��2��$��0��$��1,1,0,0q�
���   ��� $��3��$��0��$��1,1,0,0q�
���   ��� $��4��$��0��$��1,1,1,0q�
���   ��� $��0��$��1��$��1,1,0,1q�
���   ��� $��1��$��1��$��1,1,0,1q�
���   ��� $��2��$��1��$��1,1,0,1q�
���   ��� $��3��$��1��$��1,1,0,1q�
���   ��� $��4��$��1��$��1,1,1,1q�
���   ��� $��0��$��2��$��1,0,1,0q�
���   ��� $��0��$��3��$��1,1,1,1q�
���   �/
 
 )GrapeCity.Windows.SpreadGrid.GcSpreadGrid
  -   $��
SearchGrid��$��1��G 
  AutoGenerateColumnsG .  H 
  RowCount$H 10��I 
  OperationMode$I Normal=�J 
  
LayoutMode$J Template=�K 
  CanUserSortColumnsK .  L 
  ColumnDragMode$L Default=�$��15,5,0,0q�$
��Left=�$
��1000��M 
  CanUserDragCellM .  N 
  	ProtectedN . $	��Top=�$��50��$	��330��$	��330��O 
  DocumentUri$KO E/KyoeiSystem.Application.Windows;component/views/DLY/DLY01010G1.sgxml?�P 
  ItemsSourceP �� *SearchResult+$ TwoWay=� .  . $9 PropertyChanged=�Q 
  SelectionBorderUnfocusedQ - 
 'GrapeCity.Windows.SpreadGrid.BorderLine  R   Color$R Orange��S   Style$S Thick=�T 
  SelectionBorderT   $R Orange��$S Thick=�U 
  ColumnsU ) 
 #GrapeCity.Windows.SpreadGrid.Column  V   Locked$
V TrueX�W   	IsVisibleW .  X   	Focusable$X FalseX�Y   CanUserResize$Y FalseX�Z   CellTypeZ / 
 )GrapeCity.Windows.SpreadGrid.TextCellType  3 
 -GrapeCity.Windows.SpreadGrid.EditBaseCellType[   
IsReadOnly[ . \   	DataField\ 3 
 -GrapeCity.Windows.SpreadGrid.BindingDataField  ]   Binding] �� *伝票番号+$ TwoWay=� .  . $9 PropertyChanged=�  $
V TrueX�W .  $X FalseX�$Y FalseX�Z   [ . \   ] �� *品番コード+$ TwoWay=� .  . $9 PropertyChanged=�  $V FalseX�$Y FalseX�Z   ^   	MaxLength$^ 12��_   MaxLengthUnit$
_ Byte=�`   ImeState$	` Off=�a   TextWrapping$a NoWrap=�\   ] �� *自社品番+$ TwoWay=� .  . $9 PropertyChanged=�  $
V TrueX�$Y FalseX�Z   [ . \   ] �� *自社品名+$ TwoWay=� .  . $9 PropertyChanged=�  $V FalseX�$Y FalseX�Z   $^ 10��$	` Off=�\   ] �� *賞味期限+b �� TargetNullValue$b  ��$ TwoWay=� .  . $9 PropertyChanged=�  $V FalseX�$Y FalseX�Z 1 
 +GrapeCity.Windows.SpreadGrid.NumberCellType  c   MinValue$c -9999999.99~�d   MaxValue$d 	999999.99~�e   SpinButtonVisibility$e NotShow=�\   ] �� *数量+$ TwoWay=� .  . $9 PropertyChanged=�  $
V TrueX�$X FalseX�$Y FalseX�Z   [ . \   ] �� *単位+$ TwoWay=� .  . $9 PropertyChanged=�  $V FalseX�$Y FalseX�Z   $c -9999999.99~�$d 99999999.99~�$e NotShow=�\   ] �� *単価+$ TwoWay=� .  . $9 PropertyChanged=�  $
V TrueX�$X FalseX�$Y FalseX�Z   [ . $e NotShow=�\   ] �� *金額+$ TwoWay=� .  . $9 PropertyChanged=�  $V FalseX�$Y FalseX�Z   $` On=�$^ 40��\   ] �� *摘要+$ TwoWay=� .  . $9 PropertyChanged=�  $
V TrueX�W .  $Y FalseX�Z   [ . \   ] �� *消費税区分+$ TwoWay=� .  . $9 PropertyChanged=�  $
V TrueX�W .  $Y FalseX�Z   [ . \   ] �� *商品分類+$ TwoWay=� .  . $9 PropertyChanged=�� -   f �Name$f 
FooterArea��$��2��$��15,0,0,0q� g �� 
TargetTypeh �� Propertyi �� Value��%/   '��       �� "g ��.��� h � ��
i �   �	 �� $��30 ��� $��30 ��� $��30 ��� $
��auto � �� $	��464 ��� $��85 ��� $	��100 ��� $
��auto �.+��� $��0��$��0��$��1,1,0,1q��� $��1��$��0��$��1,1,0,1q��� $��2��$��0��$��1,1,1,1q��� $��0��$��1��$��1,0,0,1q��� $��1��$��1��$��1,0,0,1q��� $��2��$��1��$��1,0,1,1q��� $��1��$��2��$��1,0,0,1q��� $��2��$��2��$��1,0,1,1q�4  .KyoeiSystem.Framework.Windows.Controls.UcLabel  $��備　　考��$��0��$��0��$) Center=�$��0,6,0,0q�#��[   -   $ c備考��$��1��$��0��$��2q�$ M11_TEK��$ On=�j   Label_Visibility$j 	Collapsed=�$ 	Collapsed=�$ 	Collapsed=�$  40Q�F �� *SearchHeader[備考]+$ TwoWay=� .  .   $��	小　計��$��0��$��1��$) Center=�$��0,6,0,0q�#��[ �� -   $ c小計��$��0��$��2��$��Right=�$��Center=�  $��	消費税��$��1��$��1��$) Center=�$��0,6,0,0q�#��[ �� -   $ 
c消費税��$��1��$��2��$��Right=�$��Center=�  $��最大10行伝票��$��2��$��0��$��Center=�  $��	総合計��$��2��$��1��$) Center=�$��0,6,0,0q�#��[ �� -   $ 
c総合計��$��2��$��2��$��Right=�$��Center=��� $��0��$��0��$��1,1,0,1q��� $��1��$��0��$��1,1,0,1q��� $��2��$��0��$��1,1,1,1q��� $��0��$��1��$��1,0,0,1q��� $��1��$��1��$��1,0,0,1q��� $��2��$��1��$��1,0,1,1q��� $��1��$��2��$��1,0,0,1q��� $��2��$��2��$��1,0,1,1q�