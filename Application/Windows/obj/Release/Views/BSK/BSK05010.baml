   M S B A M L   `   `   `  ���� X  TKyoeiSystem.Framework.Windows, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null�[clr-namespace:KyoeiSystem.Framework.Windows.ViewBase;assembly=KyoeiSystem.Framework.Windows&KyoeiSystem.Framework.Windows.ViewBase  �[clr-namespace:KyoeiSystem.Framework.Windows.ViewBase;assembly=KyoeiSystem.Framework.Windows&KyoeiSystem.Framework.Windows.ViewBase  a ]KyoeiSystem.Framework.Windows.Controls, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null�dclr-namespace:KyoeiSystem.Framework.Windows.Controls;assembly=KyoeiSystem.Framework.Windows.Controls&KyoeiSystem.Framework.Windows.Controls Z VKyoeiSystem.Application.Windows, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]3clr-namespace:KyoeiSystem.Application.Windows.Views%KyoeiSystem.Application.Windows.Views 4   .KyoeiSystem.Application.Windows.Views.BSK05010   bfw[clr-namespace:KyoeiSystem.Framework.Windows.ViewBase;assembly=KyoeiSystem.Framework.Windows  \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     jvm[clr-namespace:KyoeiSystem.Framework.Windows.ViewBase;assembly=KyoeiSystem.Framework.Windows     uctlsdclr-namespace:KyoeiSystem.Framework.Windows.Controls;assembly=KyoeiSystem.Framework.Windows.Controls     Buc3clr-namespace:KyoeiSystem.Application.Windows.Views     e	 aGrapeCity.WPF.SpreadGrid, Version=1.0.2015.1109, Culture=neutral, PublicKeyToken=5c5c8ff7a6858ccc=sg4http://schemas.grapecity.com/windows/spreadgrid/2012 	 -   $��Window��  � WindowStyle$  SingleBorderWindow=� � AllowsTransparency .  $	��728��$
��1024��$	��768��$
��1024�� � WindowStartupLocation$ CenterOwner=� � Language$ ja-JP� � Title$( "年次販社売上調整(BSK05010)�� � 	Resources <  6KyoeiSystem.Framework.Windows.Controls.BooleanInverter   boolInverter��%   &	          .��� $��5q� � RowDefinitions �� $
��auto ��� $
��auto ��� $��* �.+�+  %System.Windows.Controls.Ribbon.Ribbon  $��Stretch=�$��2��$��Stretch=�.@�.  (System.Windows.Controls.Ribbon.RibbonTab  $��	ホーム��   KeyTip$ h��$��75��.@�0  *System.Windows.Controls.Ribbon.RibbonGroup  $��90��  $��F1��   KeyTip$ MASTER��  RibbonFunctionBackground#��[ .@�1  +System.Windows.Controls.Ribbon.RibbonButton  -   	   LargeImageSource$%	 ../Images/15マスタ照会.png��
   ToolTipTitle$
 マスタ照会(F1)��   ToolTipDescription$3 -入力済みのデータを検索します。��   ToolTipImageSource$% ../Images/15マスタ照会.png��   Label$ ﾏｽﾀ照会��   KeyTip$ F1��$��50��  $��F2��$ MASTER��#��[ .@�  $y�Hidden=�$  ��$
  ��$  ��  $��F3��$ MASTER��#��[ .@�  -   $"	 ../Images/25集計開始.png��$
 
計算(F3)��$- '決算調整結果を計算します。��$" ../Images/25集計開始.png��$ 計算��$ F3��$��50��  $��F4��$ MASTER��#��[ .@�  $y�Hidden=�$  ��$
  ��$  ��  $��F5��$ MASTER��#��[ .@�  -   $%	 ../Images/16マスタ入力.png��$
 請求書発行(F5)��$  ��$% ../Images/16マスタ入力.png��$ 	請求書��$ F5��$��50��  $��F6��$ MASTER��#��[ .@�  $y�Hidden=�$  ��$
  ��$  ��  $��F7��$ MASTER��#��[ .@�  $y�Hidden=�$  ��$
  ��$  ��  $��F8��$ MASTER��#��[ .@�  $y�Hidden=�$  ��$
  ��$  ��  $��F9��$ MASTER��#��[ .@�  $y�Hidden=�$  ��$
  ��$  ��  $	��F10��$ MASTER��#��[ .@�  $y�Hidden=�$  ��$
  ��$  ��  $	��F11��$ MASTER��#��[ .@�  -   $	 ../Images/27終了.png��$	 F11��$��50��$ 終了��$
 終了(F11)��$ 終了します��$ ../Images/27終了.png��  $	��F12��$ MASTER��#��[ .@�  $y�Hidden=�$  ��$
  ��$  ��  $��	ヘルプ��
�������.@�  $��サポート��.@�  -   $%	 ../Images/30マニュアル.png��$ MANUAL��$ ﾏﾆｭｱﾙ��$
 取扱説明書��$* $取り扱い説明書を開きます��  -   $(	 "../Images/29遠隔サポート.png��$ SUPPORT��$ お問合せ��$
 お問合せ��$< 6Web上の｢お問合せ｣ページに遷移します��  -   $	 ../Images/QandA.png��$	 FAQ��$ QandA��$
 QandA��$E ?Web上の｢よくあるご質問｣ページに遷移します��  -	   $(	 "../Images/13共栄Webリンク.png��$ KYOEI��$ 共栄ｼｽﾃﾑ��$
 共栄システムHP��$< 6共栄システムのホームページを開きます��� $��0��$��2��$��Bottom=� �� $��74 � � ColumnDefinitions �� $��90 ��� $��* �.+�]� $��0��$��0��$��Stretch=�$��2q����� *MaintenanceModeBackground+.+��� $��メイリオ)�$��24(�$��Medium"�$��Stretch=�$��Center=�$��Center=�$��Center=����� *MaintenanceMode+���� *MaintenanceModeForeground+�� $��0��$��1��$��Bottom=�
��� ���
���  ��$��2,0,2,0q����� *ErrorMessage+y��� *ErrorMessageVisibility+� $��1��
������� �� $
��auto ��� $
��auto ��� $��* �.+��� $��0�����#2792c3$��1q�$��0,5,0,0q�._�� 
�������$��5q� �� $
��auto ��� $
��auto ��� $
��auto ��� $
��auto � �� $	��300 ��� $��* �.+�;  5KyoeiSystem.Framework.Windows.Controls.UcLabelTextBox  -
   $��
FiscalYear��$��0��$
��Left=�   
IsRequired . =  7KyoeiSystem.Framework.Windows.Controls.FrameworkControl   	IsKeyItem . $��5,5,0,0q�   ValidationType$ Integer=�   ImeType$	 Off=�   Label_Context$ 処理年度��   Label_Width$	 100Q�   
cMaxLength$ 4��   cWidth$ 40Q�    cVerticalContentAlignment$ Center=��� $��年度��$��0��$��Center=�$��	150,5,0,0q�?  9KyoeiSystem.Framework.Windows.Controls.UcLabelTwinTextBox  -   $��	MyCompany��$��1��$
��Left=�   
IsRequired .  . $��5,5,0,0q�   DataAccessName$ M70_JIS��   DataAccessMode$ On=�   LinkItem$ 1��   Label_Context$ 対象販社��   Label_Width$	 100Q�   Label1Visibility$ 	Collapsed=�    Label2Visibility$  	Collapsed=�!   
Text1Width$! 40Q�"   Text1MaxLength$" 4��#   Text1IMEType$	# Off=�$   Text1ValidationType$$ Number=�$%   Text1VerticalContentAlignment$% Center=�&   
Text2Width$	& 260Q�'   Text2IsReadOnly' . (   Text2AcceptsTab( .  )   Text2Background
) ���� *   Text2BorderThickness$* 0q�$+   Text2VerticalContentAlignment$+ Center=�  -   $��
AjustRatio��$��2��$
��Left=�$��5,5,0,0q�$ Decimal=�$	 Off=�$ 調整比率��$	 100Q�$ 5��$ 50Q�,   Mask$, ##0.##��"-   cHorizontalContentAlignment$- Right=�$ Center=��� $��%��$��2��$��Center=�$��	160,5,0,0q��� $��1��$��1��$��2��$��	80,25,0,0q�.+��� -   $��SubCalc��$	��100��$��35��$
��Left=�$��Center=����#D2691E
�������$��SemiBold"�$��調整見込計算��� $��1��$
��Left=�$	��Top=�$��
0,10,0,300q� �� $	��860 ��� $��* �.+�/	 	 )GrapeCity.Windows.SpreadGrid.GcSpreadGrid	  -   $��sgSearchResult��$��0��. 	  RowCount$. 12��/ 	  
LayoutMode$
/ Cell=�$	��700��$	��300��$
��Left=�0 	  AutoGenerateColumns0 .  1 	  ColumnDragMode$1 SelectThenDrag=�2 	  DocumentUri$K2 E/KyoeiSystem.Application.Windows;component/views/BSK/BSK05010G1.sgxml?�3 	  ItemsSource3 �� *
SearchList+4 �� Mode$4 TwoWay=�5 �� NotifyOnSourceUpdated5 . 6 �� NotifyOnTargetUpdated6 . 7 �� UpdateSourceTrigger$7 PropertyChanged=�8 	  SelectionBorderUnfocused8 -
 	 'GrapeCity.Windows.SpreadGrid.BorderLine
  9 
  Color$9 Orange��: 
  Style$: Thick=�; 	  SelectionBorder; 
  $9 Orange��$: Thick=�< 	  Columns< ) 	 #GrapeCity.Windows.SpreadGrid.Column  =   HorizontalAlignment$= CenterX�>   Locked$
> TrueX�?   	DataField? 3 	 -GrapeCity.Windows.SpreadGrid.BindingDataField  @   Binding@ �� A �� Path$A 決算対象年月�$4 TwoWay=�5 . 6 . $7 PropertyChanged=�  $
> TrueX�B   CellTypeB 1 	 +GrapeCity.Windows.SpreadGrid.NumberCellType  C   MaxValue$C 
9999999999~�D   MinValue$D -9999999999~�E   SpinButtonVisibility$E NotShow=�F   FieldSetF 	 @GrapeCity.Windows.SpreadGrid.Editors.NumberFieldSetTypeConverter$F ###,###,###,##0,,,-, ?   @ �� $A 決算調整前金額�$4 TwoWay=�5 . 6 . $7 PropertyChanged=�  $
> TrueX�B   $C 
9999999999~�$D -9999999999~�$E NotShow=�$F ###,###,###,##0,,,-, ?   @ �� $A 決算調整見込金額�$4 TwoWay=�5 . 6 . $7 PropertyChanged=�  $
> TrueX�B   $C 
9999999999~�$D -9999999999~�$E NotShow=�$F ###,###,###,##0,,,-, ?   @ �� $A 決算調整後金額�$4 TwoWay=�5 . 6 . $7 PropertyChanged=��� -   $��Confirm��$��1��$	��100��$��35��$��Right=�$��Bottom=����#CD5C5C
�������$��SemiBold"�$��調整確定��