   M S B A M L   `   `   `  ���� X  TKyoeiSystem.Framework.Windows, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null�[clr-namespace:KyoeiSystem.Framework.Windows.ViewBase;assembly=KyoeiSystem.Framework.Windows&KyoeiSystem.Framework.Windows.ViewBase  �[clr-namespace:KyoeiSystem.Framework.Windows.ViewBase;assembly=KyoeiSystem.Framework.Windows&KyoeiSystem.Framework.Windows.ViewBase  a ]KyoeiSystem.Framework.Windows.Controls, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null�dclr-namespace:KyoeiSystem.Framework.Windows.Controls;assembly=KyoeiSystem.Framework.Windows.Controls&KyoeiSystem.Framework.Windows.Controls Z VKyoeiSystem.Application.Windows, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]3clr-namespace:KyoeiSystem.Application.Windows.Views%KyoeiSystem.Application.Windows.Views 4   .KyoeiSystem.Application.Windows.Views.TKS01020   bfw[clr-namespace:KyoeiSystem.Framework.Windows.ViewBase;assembly=KyoeiSystem.Framework.Windows  \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     jvm[clr-namespace:KyoeiSystem.Framework.Windows.ViewBase;assembly=KyoeiSystem.Framework.Windows     uctlsdclr-namespace:KyoeiSystem.Framework.Windows.Controls;assembly=KyoeiSystem.Framework.Windows.Controls     Buc3clr-namespace:KyoeiSystem.Application.Windows.Views     e	 aGrapeCity.WPF.SpreadGrid, Version=1.0.2015.1109, Culture=neutral, PublicKeyToken=5c5c8ff7a6858ccc=sg4http://schemas.grapecity.com/windows/spreadgrid/2012 	 -   $��Window��  � WindowStartupLocation$  CenterOwner=� � WindowStyle$ SingleBorderWindow=� � AllowsTransparency .   � Language$ ja-JP�$	��728��$
��1024��$	��768��$
��1024�� � Left$ 0��
 � Top$ 0�� � Title$ 請求書発行(TKS01020)�� � 	Resources <  6KyoeiSystem.Framework.Windows.Controls.BooleanInverter   boolInverter��%   &	          .��� $��5q� � RowDefinitions �� $
��auto ��� $
��auto �.+��� $��0��$��1��$��2��$��9�����#2792c3$��1q�+  %System.Windows.Controls.Ribbon.Ribbon  $��Stretch=�$��2��$��Stretch=�.@�.  (System.Windows.Controls.Ribbon.RibbonTab  $��	ホーム��	   KeyTip$	 h��$��75��.@�0  *System.Windows.Controls.Ribbon.RibbonGroup  $��90��  $��F1��
   KeyTip$
 MASTER��  RibbonFunctionBackground#��[ .@�1  +System.Windows.Controls.Ribbon.RibbonButton  -      LargeImageSource$% ../Images/15マスタ照会.png��   ToolTipTitle$ マスタ照会(F1)��   ToolTipDescription$3 -入力済みのデータを検索します。��   ToolTipImageSource$% ../Images/15マスタ照会.png��   Label$ ﾏｽﾀ照会��   KeyTip$ F1��$��50��  $��F2��$
 MASTER��#��[ .@�  $y�Hidden=�$  ��$  ��$  ��  $��F3��$
 MASTER��#��[ .@�  $y�Hidden=�$  ��$  ��$  ��  $��F4��$
 MASTER��#��[ .@�  $y�Hidden=�$  ��$  ��$  ��  $��F5��$
 MASTER��#��[ .@�  $y�Hidden=�$  ��$  ��$  ��  $��F6��$
 MASTER��#��[ .@�  $y�Hidden=�$  ��$  ��$  ��  $��F7��$
 MASTER��#��[ .@�  $y�Hidden=�$  ��$  ��$  ��  $��F8��$
 MASTER��#��[ .@�  -   $ ../Images/23印刷.png��$ F8��$��50��$ 印刷��$ 
印刷(F8)��$ 印刷を開始します��$ ../Images/23印刷.png��  $��F9��$
 MASTER��#��[ .@�  $y�Hidden=�$  ��$  ��$  ��  $	��F10��$
 MASTER��#��[ .@�  $y�Hidden=�$  ��$  ��$  ��  $	��F11��$
 MASTER��#��[ .@�  -   $ ../Images/27終了.png��$	 F11��$��50��$ 終了��$ 終了(F11)��$ 終了します��$ ../Images/27終了.png��  $	��F12��$
 MASTER��#��[ .@�  $y�Hidden=�$  ��$  ��$  ��  $��	ヘルプ��
�������.@�  $��サポート��.@�  -   $% ../Images/30マニュアル.png��$ MANUAL��$ ﾏﾆｭｱﾙ��$ 取扱説明書��$* $取り扱い説明書を開きます��  -   $( "../Images/29遠隔サポート.png��$ SUPPORT��$ お問合せ��$ お問合せ��$< 6Web上の｢お問合せ｣ページに遷移します��  -   $ ../Images/QandA.png��$	 FAQ��$ QandA��$ QandA��$E ?Web上の｢よくあるご質問｣ページに遷移します��  -   $( "../Images/13共栄Webリンク.png��$ KYOEI��$ 共栄ｼｽﾃﾑ��$ 共栄システムHP��$< 6共栄システムのホームページを開きます��� $��0��$��2��$��Bottom=� �� $��74 � � ColumnDefinitions �� $��90 ��� $��* �.+�]� $��0��$��0��$��Stretch=�$��2q����� *MaintenanceModeBackground+.+��� $��メイリオ)�$��24(�$��Medium"�$��Stretch=�$��Center=�$��Center=�$��Center=����� *MaintenanceMode+���� *MaintenanceModeForeground+�� $��0��$��1��$��Bottom=�
��� ���
���  ��$��2,0,2,0q����� *ErrorMessage+y��� *ErrorMessageVisibility+� $��1��
������� �� $
��auto ��� $
��auto ��� $
��auto �.+�� $��1��$��2��$��5,5,0,0q�
������� �� $
��auto ��� $
��auto ��� $
��auto ��� $
��auto ��� $
��auto ��� $
��auto ��� $
��auto � �� $	��670 ��� $��* �.+�?  9KyoeiSystem.Framework.Windows.Controls.UcLabelTwinTextBox  -	   $��	MyCompany��$��0��$
��Left=�   DataAccessName$ M70_JIS��   DataAccessMode$ On=�   
IsRequired . =  7KyoeiSystem.Framework.Windows.Controls.FrameworkControl   	IsKeyItem .    Label_Context$ 自社コード��   Label_Width$	 120Q�   Label1Visibility$ 	Collapsed=�   Label2Visibility$ 	Collapsed=�   
Text1Width$ 40Q�   Text1MaxLength$ 3��   Text1IMEType$	 Off=�   Text1ValidationType$ Integer=�   
Text2Width$	 250Q�   Text2AcceptsTab .      Text2IsReadOnly  . !   Text2Background
! ���� "   Text2BorderThickness$" 0q�;  5KyoeiSystem.Framework.Windows.Controls.UcLabelTextBox  -
   $��	PrintDate��$��1��$
��Left=�#   ValidationType$
# Date=�$   Mask$$ 
yyyy/MM/dd��%   
IsRequired% . $��0,3,0,0q�&   Label_Context$& 出力日付��'   Label_Width$	' 120Q�(   
cMaxLength$( 10��)   cWidth$) 80Q� *   cVerticalContentAlignment$* Center=�  -   $��CreateYearMonth��$��2��$# 
DateYYYYMM=�$$ yyyy/MM�� . % . $
��Left=�$��0,3,0,0q�$& 作成年月��$	' 120Q�$) 60Q�$( 7��$* Center=�� $��3��$��0,3,0,0q� �� $
��auto ��� $��* �.+�  -   $��ClosingDate��$
��Left=�% .  . +   MinValue$+ 0��,   MaxValue$, 31��$# Integer=�$& 作成締日��$	' 120Q�$) 30Q�$( 2��$* Center=�-   Text- �� *締日+. �� Mode$. TwoWay=�/ �� NotifyOnSourceUpdated/ . 0 �� NotifyOnTargetUpdated0 . 1 �� UpdateSourceTrigger$1 PropertyChanged=��� $��2��$��末日は31を設定��$
��Left=�;	  5KyoeiSystem.Application.Windows.Views.M01_TOK_TextBox	  -   2 	 Name$2 Customer��$��4��$��2��3 	  DataAccessMode$3 On=�4 	  DataAccessName$4 M01_TOK��5 	  LinkItem$5 0,3,4�� .  $
��Left=�$��0,3,0,0q�6 	  Label_Context$6 得意先指定��7 	  Label_Width$	7 120Q�8 	  Label1Visibility$8 	Collapsed=�9 	  Label2Visibility$9 	Collapsed=�: 	  
Text1Width$: 40Q�; 	  Text1IMEType$	; Off=�$< 	  Text1VerticalContentAlignment$< Center=�= 	  
Label1Text$	= －��&> 	  Label1VerticalConteintAlignment$> Center=�? 	  
Text2Width$? 30Q�@ 	  Text2IMEType$	@ Off=�$A 	  Text2VerticalContentAlignment$A Center=�B 	  
Label2Text$B  ���� -   $��	btnSearch��$��2��$��2��$��50��$��50��$
��Left=�$��Stretch=�$��0q�.���� $)��#../Images/検索実行ボタン.png��$
��Fill=�� $��2��$��2��$��0,10,0,0q� �� $
��auto ��� $	��420 � �� $	��128 ��� $��* �.+��� $��2��$��10q�.+��� -   $��btnAllOn��$��全てチェック��$��30���� -   $��	btnAllOff��$��全てチェック外す��$��30��$��0,5,0,0q�/
 	 )GrapeCity.Windows.SpreadGrid.GcSpreadGrid
  -   $��sp請求データ一覧��$��0��$��1��$��2��C 
  RowCount$C 0��D 
  AutoGenerateColumnsD .  E 
  
LayoutMode$
E Cell=�F 
  ColumnDragMode$F SelectThenDrag=�G 
  DocumentUri$KG E/KyoeiSystem.Application.Windows;component/views/TKS/TKS01020G1.sgxml?�H 
  ItemsSourceH �� *請求書一覧データ+$. TwoWay=�/ . 0 . $1 PropertyChanged=�I 
  SelectionBorderUnfocusedI - 	 'GrapeCity.Windows.SpreadGrid.BorderLine  J   Color$J Orange��K   Style$K Thick=�L 
  SelectionBorderL   $J Orange��$K Thick=�M 
  ColumnsM ) 	 #GrapeCity.Windows.SpreadGrid.Column  N   HorizontalAlignment$N CenterX�O   CellTypeO 3 	 -GrapeCity.Windows.SpreadGrid.CheckBoxCellType  P   	DataFieldP 3 	 -GrapeCity.Windows.SpreadGrid.BindingDataField  Q   BindingQ �� *印刷区分+  R   Locked$
R TrueX�P   Q �� *得意先コード+  $
R TrueX�P   Q �� *得意先枝番+  $
R TrueX�P   Q �� *得意先名+  $
R TrueX�$N RightX�P   Q �� *回数+  $
R TrueX�P   Q �� *集計期間+  $
R TrueX�$N RightX�P   Q �� *当月請求額+S �� StringFormat$	S #,0��  $
R TrueX�P   Q �� *郵便番号+  $
R TrueX�P   Q �� *	住所１+  $
R TrueX�P   Q �� *	住所２+  $
R TrueX�P   Q �� *電話番号+