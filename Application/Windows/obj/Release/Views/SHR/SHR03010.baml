   M S B A M L   `   `   `  ���� X  TKyoeiSystem.Framework.Windows, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null�[clr-namespace:KyoeiSystem.Framework.Windows.ViewBase;assembly=KyoeiSystem.Framework.Windows&KyoeiSystem.Framework.Windows.ViewBase  �[clr-namespace:KyoeiSystem.Framework.Windows.ViewBase;assembly=KyoeiSystem.Framework.Windows&KyoeiSystem.Framework.Windows.ViewBase  a ]KyoeiSystem.Framework.Windows.Controls, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null�dclr-namespace:KyoeiSystem.Framework.Windows.Controls;assembly=KyoeiSystem.Framework.Windows.Controls&KyoeiSystem.Framework.Windows.Controls Z VKyoeiSystem.Application.Windows, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]3clr-namespace:KyoeiSystem.Application.Windows.Views%KyoeiSystem.Application.Windows.Views 4   .KyoeiSystem.Application.Windows.Views.SHR03010   bfw[clr-namespace:KyoeiSystem.Framework.Windows.ViewBase;assembly=KyoeiSystem.Framework.Windows  \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     jvm[clr-namespace:KyoeiSystem.Framework.Windows.ViewBase;assembly=KyoeiSystem.Framework.Windows     uctlsdclr-namespace:KyoeiSystem.Framework.Windows.Controls;assembly=KyoeiSystem.Framework.Windows.Controls     Buc3clr-namespace:KyoeiSystem.Application.Windows.Views     e	 aGrapeCity.WPF.SpreadGrid, Version=1.0.2015.1109, Culture=neutral, PublicKeyToken=5c5c8ff7a6858ccc=sg4http://schemas.grapecity.com/windows/spreadgrid/2012 	 -   $��Window��  � WindowStyle$  SingleBorderWindow=� � AllowsTransparency .  $	��728��$
��1024��$	��768��$
��1024�� � WindowStartupLocation$ CenterOwner=� � Language$ ja-JP� � Title$ 支払締集計(SHR03010)�� � 	Resources <  6KyoeiSystem.Framework.Windows.Controls.BooleanInverter   boolInverter��%   &	          .��� $��5q� � RowDefinitions �� $
��auto ��� $
��auto ��� $��* �.+�+  %System.Windows.Controls.Ribbon.Ribbon  $��Stretch=�$��2��$��Stretch=�.@�.  (System.Windows.Controls.Ribbon.RibbonTab  $��	ホーム��   KeyTip$ h��$��75��.@�0  *System.Windows.Controls.Ribbon.RibbonGroup  $��90��  $��F1��   KeyTip$ MASTER��  RibbonFunctionBackground#��[ .@�1  +System.Windows.Controls.Ribbon.RibbonButton  -   	   LargeImageSource$%	 ../Images/15マスタ照会.png��
   ToolTipTitle$
 マスタ照会(F1)��   ToolTipDescription$3 -入力済みのデータを検索します。��   ToolTipImageSource$% ../Images/15マスタ照会.png��   Label$ ﾏｽﾀ照会��   KeyTip$ F1��$��50��  $��F2��$ MASTER��#��[ .@�  $y�Hidden=�$  ��$
  ��$  ��  $��F3��$ MASTER��#��[ .@�  $y�Hidden=�$  ��$
  ��$  ��  $��F4��$ MASTER��#��[ .@�  $y�Hidden=�$  ��$
  ��$  ��  $��F5��$ MASTER��#��[ .@�  $y�Hidden=�$  ��$
  ��$  ��  $��F6��$ MASTER��#��[ .@�  $y�Hidden=�$  ��$
  ��$  ��  $��F7��$ MASTER��#��[ .@�  $y�Hidden=�$  ��$
  ��$  ��  $��F8��$ MASTER��#��[ .@�  -   $y�Hidden=�$	 ../Images/23印刷.png��$ F8��$ 印刷��$
 
印刷(F8)��$ 印刷を開始します��$ ../Images/23印刷.png��  $��F9��$ MASTER��#��[ .@�  -   $"	 ../Images/25集計開始.png��$ F9��$��50��$ 集計開始��$
 集計開始(F9)��$! 集計処理を行います��$" ../Images/25集計開始.png��  $	��F10��$ MASTER��#��[ .@�  $y�Hidden=�$  ��$
  ��$  ��  $	��F11��$ MASTER��#��[ .@�  -   $	 ../Images/27終了.png��$	 F11��$��50��$ 終了��$
 終了(F11)��$ 終了します��$ ../Images/27終了.png��  $	��F12��$ MASTER��#��[ .@�  $y�Hidden=�$  ��$
  ��$  ��  $��	ヘルプ��
�������.@�  $��サポート��.@�  -   $%	 ../Images/30マニュアル.png��$ MANUAL��$ ﾏﾆｭｱﾙ��$
 取扱説明書��$* $取り扱い説明書を開きます��  -   $(	 "../Images/29遠隔サポート.png��$ SUPPORT��$ お問合せ��$
 お問合せ��$< 6Web上の｢お問合せ｣ページに遷移します��  -   $	 ../Images/QandA.png��$	 FAQ��$ QandA��$
 QandA��$E ?Web上の｢よくあるご質問｣ページに遷移します��  -	   $(	 "../Images/13共栄Webリンク.png��$ KYOEI��$ 共栄ｼｽﾃﾑ��$
 共栄システムHP��$< 6共栄システムのホームページを開きます��� $��0��$��2��$��Bottom=� �� $��74 � � ColumnDefinitions �� $��90 ��� $��* �.+�]� $��0��$��0��$��Stretch=�$��2q����� *MaintenanceModeBackground+.+��� $��メイリオ)�$��24(�$��Medium"�$��Stretch=�$��Center=�$��Center=�$��Center=����� *MaintenanceMode+���� *MaintenanceModeForeground+�� $��0��$��1��$��Bottom=�
��� ���
���  ��$��2,0,2,0q����� *ErrorMessage+y��� *ErrorMessageVisibility+�� $��0��$��1�����#2792c3$��1q�$��0,5,0,0q�._�� 
�������$��5q� �� $
��auto ��� $
��auto ��� $
��auto ��� $
��auto � �� $	��520 ��� $��* �.+�?  9KyoeiSystem.Framework.Windows.Controls.UcLabelTwinTextBox  -
   $��	MyCompany��$��0��   DataAccessName$ M70_JIS��   DataAccessMode$ On=�$
��Left=�=  7KyoeiSystem.Framework.Windows.Controls.FrameworkControl   	IsKeyItem . $��5,5,0,0q�   Label_Context$ 自社コード��   Label_Width$	 100Q�   Label1Visibility$ 	Collapsed=�   Label2Visibility$ 	Collapsed=�   
Text1Width$ 60Q�   Text1IMEType$	 Off=�$   Text1VerticalContentAlignment$ Center=�   Text1ValidationType$ Number=�   
Text2Width$	 250Q�   Text2AcceptsTab .     Text2Background
 ����    Text2BorderThickness$ 0q�   Text2IsReadOnly . $    Text2VerticalContentAlignment$  Center=�;  5KyoeiSystem.Framework.Windows.Controls.UcLabelTextBox  -   $��CreateYearMonth��$��1��$
��Left=�!   ValidationType$! 
DateYYYYMM=�"   Mask$" yyyy/MM��#   ImeType$	# Off=�$   
IsRequired$ . $��5,5,0,0q�%   Label_Context$% 作成支払年月��&   Label_Width$	& 100Q�'   
cMaxLength$' 7��(   cWidth$( 60Q� )   cVerticalContentAlignment$) Center=�� $��2��$��5,5,0,0q� �� $
��auto ��� $��* �.+�  -   $��ClosingDate��$��0��$
��Left=�$! Integer=�$	# Off=�$% 作成支払締日��$	& 100Q�*   MinValue$* 1��+   MaxValue$+ 31��$( 60Q�$' 2��$) Center=��� $��1��$0��*末日は31 ※空白で全締日集計※��$
��Left=�;	  5KyoeiSystem.Application.Windows.Views.M01_TOK_TextBox	  -   , 	 Name$, Customer��$��3��$
��Left=� .  $��5,5,0,0q�- 	  DataAccessName$- M01_TOK��. 	  DataAccessMode$. On=�/ 	  LinkItem$	/ 1,3��0 	  Label_Context$0 支払先指定��1 	  Label_Width$	1 100Q�2 	  Label1Visibility$2 	Collapsed=�3 	  Label2Visibility$3 	Collapsed=�4 	  
Text1Width$4 60Q�5 	  Text1MaxLength$5 4��6 	  Text1IMEType$	6 Off=�$7 	  Text1VerticalContentAlignment$7 Center=�8 	  
Label1Text$	8 －��&9 	  Label1VerticalConteintAlignment$9 Center=�: 	  
Text2Width$: 30Q�; 	  Text2MaxLength$; 2��< 	  Text2IMEType$	< Off=�$= 	  Text2VerticalContentAlignment$= Center=�> 	  
Label2Text$>  ��� $��4��$��	105,5,0,0q�$y�Hidden=�.+��� -   $��Kotei��$	��150��$��15(�|�.  $��計算期間の再計算���� $��1��$��1��$��2��? �� Orientation$? 
Horizontal=�$��5,5,0,0q�.+��� -   $��50��$��50��$
��Left=�$��Stretch=�$��0q�.���� $)��#../Images/検索実行ボタン.png��$
��Fill=��� -   $y�Hidden=�$%��表示項目
位置ﾘｾｯﾄ��$
��Left=�|�.  $��76��$��50��$��20,0,0,0q�� $��6��$��2��$��0,10,0,0q�.+�/
 	 )GrapeCity.Windows.SpreadGrid.GcSpreadGrid
  -   $��sp支払データ一覧��$��1��$��0��@ 
  AutoGenerateColumns@ .  $
��1000��A 
  
LayoutMode$
A Cell=�B 
  ColumnDragMode$B SelectThenDrag=�C 
  DocumentUri$KC E/KyoeiSystem.Application.Windows;component/views/SHR/SHR03010G1.sgxml?�D 
  RowCount$D 0��E 
  ItemsSourceE �� *
SearchList+F �� Mode$F TwoWay=�G �� NotifyOnSourceUpdatedG . H �� NotifyOnTargetUpdatedH . I �� UpdateSourceTrigger$I PropertyChanged=�J 
  SelectionBorderUnfocusedJ - 	 'GrapeCity.Windows.SpreadGrid.BorderLine  K   Color$K Orange��L   Style$L Thick=�M 
  SelectionBorderM   $K Orange��$L Thick=�N 
  ColumnsN ) 	 #GrapeCity.Windows.SpreadGrid.Column  O   HorizontalAlignment$O CenterX�P   Locked$
P TrueX�Q   	DataFieldQ 3 	 -GrapeCity.Windows.SpreadGrid.BindingDataField  R   BindingR �� S �� Path$S ID�$F TwoWay=�G . H . $I PropertyChanged=�  $
P TrueX�Q   R �� $S 支払先名�$F TwoWay=�G . H . $I PropertyChanged=�  $O CenterX�$
P TrueX�Q   R �� $S 締日�$F TwoWay=�G . H . $I PropertyChanged=�  T   	Focusable$T FalseX�U   CellTypeU 1 	 +GrapeCity.Windows.SpreadGrid.ButtonCellType  V   Content$V 	クリア��  $O CenterX�$
P TrueX�Q   R �� $S 区分�$F TwoWay=�G . H . $I PropertyChanged=�  $O CenterX�U / 	 )GrapeCity.Windows.SpreadGrid.TextCellType  W   	MaxLength$W 10��X   MaxLengthUnit$
X Byte=�Y   ImeState$	Y Off=�Q   R �� $S 開始日付1�$F TwoWay=�G . H . $I PropertyChanged=�Z �� StringFormat$Z 
yyyy/MM/dd��  $O CenterX�U   $W 10��$
X Byte=�$	Y Off=�Q   R �� $S 終了日付1�$F TwoWay=�G . H . $I PropertyChanged=�$Z 
yyyy/MM/dd��  $O CenterX�U   $W 10��$
X Byte=�$	Y Off=�Q   R �� $S 開始日付2�$F TwoWay=�G . H . $I PropertyChanged=�$Z 
yyyy/MM/dd��  $O CenterX�U   $W 10��$
X Byte=�$	Y Off=�Q   R �� $S 終了日付2�$F TwoWay=�G . H . $I PropertyChanged=�$Z 
yyyy/MM/dd��  $O CenterX�U   $W 10��$
X Byte=�$	Y Off=�Q   R �� $S 開始日付3�$F TwoWay=�G . H . $I PropertyChanged=�$Z 
yyyy/MM/dd��  $O CenterX�U   $W 10��$
X Byte=�$	Y Off=�Q   R �� $S 終了日付3�$F TwoWay=�G . H . $I PropertyChanged=�$Z 
yyyy/MM/dd��  $
P TrueX�[   	IsVisible[ .  Q   R �� $S クリア開始日付�$F TwoWay=�G . H . $I PropertyChanged=�$Z 
yyyy/MM/dd��  $
P TrueX�[ .  Q   R �� $S クリア終了日付�$F TwoWay=�G . H . $I PropertyChanged=�$Z 
yyyy/MM/dd��