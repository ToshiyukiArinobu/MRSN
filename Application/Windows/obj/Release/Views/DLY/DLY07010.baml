   M S B A M L   `   `   `  ���� X  TKyoeiSystem.Framework.Windows, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null�[clr-namespace:KyoeiSystem.Framework.Windows.ViewBase;assembly=KyoeiSystem.Framework.Windows&KyoeiSystem.Framework.Windows.ViewBase  �[clr-namespace:KyoeiSystem.Framework.Windows.ViewBase;assembly=KyoeiSystem.Framework.Windows&KyoeiSystem.Framework.Windows.ViewBase  a ]KyoeiSystem.Framework.Windows.Controls, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null�dclr-namespace:KyoeiSystem.Framework.Windows.Controls;assembly=KyoeiSystem.Framework.Windows.Controls&KyoeiSystem.Framework.Windows.Controls Z VKyoeiSystem.Application.Windows, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]3clr-namespace:KyoeiSystem.Application.Windows.Views%KyoeiSystem.Application.Windows.Views R NSystem.Data, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089>.clr-namespace:System.Data;assembly=System.DataSystem.Data 4   .KyoeiSystem.Application.Windows.Views.DLY07010   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      lfw[clr-namespace:KyoeiSystem.Framework.Windows.ViewBase;assembly=KyoeiSystem.Framework.Windows      R	 NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml 	    Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006 	    @d2http://schemas.microsoft.com/expression/blend/2008 	    jvm[clr-namespace:KyoeiSystem.Framework.Windows.ViewBase;assembly=KyoeiSystem.Framework.Windows 	    uctlsdclr-namespace:KyoeiSystem.Framework.Windows.Controls;assembly=KyoeiSystem.Framework.Windows.Controls 	    Buc3clr-namespace:KyoeiSystem.Application.Windows.Views 	    e
 aGrapeCity.WPF.SpreadGrid, Version=1.0.2015.1109, Culture=neutral, PublicKeyToken=5c5c8ff7a6858ccc=sg4http://schemas.grapecity.com/windows/spreadgrid/2012 
 9Data.clr-namespace:System.Data;assembly=System.Data 
 -   $	��728��$
��1024��$	��768��$
��1024��  � Left$  0��
 � Top$ 0�� � WindowStartupLocation$ Manual=� � AllowsTransparency .   � Language$ ja-JP� � Title$" 揚り依頼入力(DLY07010)�� � 	Resources    DataGridHeaderBackground �� EndPoint �� 
StartPoint	 � Color
 � Offset  DataGridSummaryBackground��%�   &	        &	 7     �� $	 0,1&�$	 0,0&�.��� $	 #3b5998��$
 0Q��� $	 0,1&�$	 0,0&�.��� $	 #f0e68c��$
 0Q�.���  � RowDefinitions �� $
��auto ��� $
��auto ��� $��* �.+�+  %System.Windows.Controls.Ribbon.Ribbon  $��Stretch=�$��Stretch=�.@�.  (System.Windows.Controls.Ribbon.RibbonTab  $��	ホーム��   KeyTip$ h��$��75��.@�0  *System.Windows.Controls.Ribbon.RibbonGroup  $��90��  $��F1��   KeyTip$ MASTER��  RibbonFunctionBackground#��[ .@�1  +System.Windows.Controls.Ribbon.RibbonButton  -      LargeImageSource$% ../Images/15マスタ照会.png��$��0,0,0,0q�   ToolTipTitle$ マスタ照会(F1)��   ToolTipDescription$3 -入力済みのデータを検索します。��   ToolTipImageSource$% ../Images/15マスタ照会.png��   Label$ ﾏｽﾀ照会��   KeyTip$ F1��$��50��  $��F2��$ MASTER��#��[ .@�  -   $% ../Images/16マスタ入力.png��$��0,0,0,0q�$ ﾏｽﾀ入力��$ F2��$��50��$ マスタ入力(F2)��$- 'マスタの入力画面を開きます��$% ../Images/16マスタ入力.png��  $��F3��$ MASTER��#��[ .@�  -   $% ../Images/16マスタ入力.png��$��0,0,0,0q�$ 揚り入力��$ F3��$��50��$ 揚り入力(F3)��$' !揚り入力画面を開きます��$% ../Images/16マスタ入力.png��  $��F4��$ MASTER��#��[ .@�  $y�Hidden=�$  ��$  ��$  ��  $��F5��$ MASTER��#��[ .@�  -   $ ../Images/行追加.png��$ F5��$��50��$ 	行追加��$ 行追加(F5)��$0 *リストに新規行を追加します。��$ ../Images/行追加.png��  $��F6��$ MASTER��#��[ .@�  -   $ ../Images/28削除.png��$ F6��$��50��$ 	行削除��$ 行削除(F6)��$0 *選択中のリスト行を削除します��$ ../Images/28削除.png��  $��F7��$ MASTER��#��[ .@�  $y�Hidden=�$  ��$  ��$  ��  $��F8��$ MASTER��#��[ .@�  $y�Hidden=�$  ��$  ��$  ��  $��F9��$ MASTER��#��[ .@�  -   $ ../Images/24登録.png��$ F9��$��50��$ 登録��$ データ登録(F9)��$- '入力データの登録を行います��$ ../Images/24登録.png��  $	��F10��$ MASTER��#��[ .@�  -   $ ../Images/26クリア.png��$	 F10��$��50��$ 入力取消��$ 入力取消(F10)��$' !入力の取消しを行います��$ ../Images/26クリア.png��  $	��F11��$ MASTER��#��[ .@�  -	   $ ../Images/27終了.png��$	 F11��$��50��$ 終了��$ 終了(F11)��$ 終了します��$ ../Images/27終了.png��  $	��F12��$ MASTER��#��[ .@�  $y�Hidden=�$  ��$  ��$  ��  $��	ヘルプ��
�������.@�  $��サポート��.@�  -
   $% ../Images/30マニュアル.png��$ MANUAL��$ ﾏﾆｭｱﾙ��$ 取扱説明書��$* $取り扱い説明書を開きます��  -   $( "../Images/29遠隔サポート.png��$ SUPPORT��$ お問合せ��$ お問合せ��$< 6Web上の｢お問合せ｣ページに遷移します��  -   $ ../Images/QandA.png��$	 FAQ��$ QandA��$ QandA��$E ?Web上の｢よくあるご質問｣ページに遷移します��  -   $( "../Images/13共栄Webリンク.png��$ KYOEI��$ 共栄ｼｽﾃﾑ��$ 共栄システムHP��$< 6共栄システムのホームページを開きます��� $��0��$��Bottom=� �� $��74 � � ColumnDefinitions �� $��90 ��� $��* �.+�]� $��0��$��0��$��Stretch=�$��2q����� *MaintenanceModeBackground+.+��� $��メイリオ)�$��24(�$��Medium"�$��Stretch=�$��Center=�$��Center=�$��Center=����� *MaintenanceMode+���� *MaintenanceModeForeground+�� $��0��$��1��$��Bottom=�
��� ���
���  ��$��2,0,2,0q����� *ErrorMessage+y��� *ErrorMessageVisibility+� -   $��	grdHeader��$��1��
�������$��Stretch=�$��Stretch=� �� $
��auto ��� $
��auto �.+�� $��0��$��	20,10,0,5q� �� $
��auto ��� $��* �.+��� -    ��Name$ 	FuncTitle��$��0��$��16(�$��30,0,0,0q�$��揚り依頼入力��?  9KyoeiSystem.Framework.Windows.Controls.UcLabelTwinTextBox  -   $��ProductCode��$��1��$
��Left=�$��	30,15,0,0q�   DataAccessName$ 	M09_MYHIN��   DataAccessMode$ On=�   LinkItem$ 2��   Label_Context$ 自社品番��   Label_Width$	 100Q�   Label1Visibility$ 	Collapsed=�   Label2Visibility$ 	Collapsed=�   
Text1Width$ 90Q�   Text1IMEType$	 Off=�$   Text1VerticalContentAlignment$ Center=�    
Text2Width$	  200Q�!   Text2IsReadOnly! . "   Text2BorderThickness$" 0q�#   Text2Background
# ���� $$   Text2VerticalContentAlignment$$ Center=�;  5KyoeiSystem.Application.Windows.Views.M01_TOK_TextBox  -   %  Name$% Subcontractor��;  5KyoeiSystem.Application.Windows.Views.M01_TOK_TextBox&   	LostFocus$& TwinTextBox_LostFocus��$��1��$
��Left=�$��
500,15,0,0q�'   DataAccessName$' M01_TOK��(   DataAccessMode$( On=�)   LinkItem$	) 2,3��*   Label_Context$* 	依頼先��+   Label_Width$	+ 100Q�,   Label1Visibility$, 	Collapsed=�-   Label2Visibility$- 	Collapsed=�.   
Text1Width$. 40Q�/   Text1MaxLength$/ 4��0   Text1IMEType$	0 Off=�$1   Text1VerticalContentAlignment$1 Center=�2   
Label1Text$	2 －��3   
Text2Width$3 30Q�4   Text2MaxLength$4 2��5   Text2IMEType$	5 Off=�$6   Text2VerticalContentAlignment$6 Center=�/ 
 )GrapeCity.Windows.SpreadGrid.GcSpreadGrid  -   $��gcSpreadGrid��$��2��$
��Left=�7   AutoFitType$7 CellWithHeader=�8   
LayoutMode$
8 Cell=�9   AutoGenerateColumns9 .  :   HighlightHeader$
: None=�;   RowCount$; 10��<   OperationMode$< Normal=�=   	Protected= . >   CanUserSortColumns> .  ?   ColumnDragMode$? Direct=�$	��500��$	��950��$��
50,5,0,100q�@   DocumentUri$K@ E/KyoeiSystem.Application.Windows;component/views/DLY/DLY07010G1.sgxml?�A   ItemsSourceA �� *SearchResult+B �� Mode$B TwoWay=�C �� UpdateSourceTrigger$C PropertyChanged=�D �� NotifyOnSourceUpdatedD . E �� NotifyOnTargetUpdatedE . F   SelectionBorderUnfocusedF -	 
 'GrapeCity.Windows.SpreadGrid.BorderLine	  G 	  Color$G Orange��H 	  Style$H Thick=�I   SelectionBorderI 	  $G Orange��$H Thick=�J   ColumnsJ )
 
 #GrapeCity.Windows.SpreadGrid.Column
  K 
  	Focusable$
K TrueX�L 
  HorizontalAlignment$L CenterX�M 
  CanUserResize$M FalseX�N 
  CellTypeN / 
 )GrapeCity.Windows.SpreadGrid.TextCellType  O   	MaxLength$O 10��P   MaxLengthUnit$
P Byte=�Q   ImeState$	Q Off=�R 
  	DataFieldR 3 
 -GrapeCity.Windows.SpreadGrid.BindingDataField  S   BindingS �� *	依頼日+T �� TargetNullValue$T  ��U �� StringFormat$U 
yyyy/MM/dd��$B TwoWay=�$C PropertyChanged=�D . E . 
  $
K TrueX�$L CenterX�$M FalseX�N   $O 4��$
P Byte=�$	Q Off=�R   S �� *取引先コード+$B TwoWay=�$C PropertyChanged=�D . E . 
  V 
  Locked$
V TrueX�$K FalseX�$L CenterX�$M FalseX�
  $
K TrueX�$L CenterX�$M FalseX�N   $O 2��$
P Byte=�$	Q Off=�R   S �� *枝番+$B TwoWay=�$C PropertyChanged=�D . E . 
  $
V TrueX�$
K TrueX�R   S �� *得意先名+$B TwoWay=�$C PropertyChanged=�D . E . 
  $
K TrueX�$L CenterX�$M FalseX�N   $O 12��$
P Byte=�$	Q Off=�R   S �� *自社品番+$B TwoWay=�$C PropertyChanged=�D . E . 
  $
V TrueX�$
K TrueX�$
M TrueX�N   3 
 -GrapeCity.Windows.SpreadGrid.EditBaseCellTypeW   
IsReadOnlyW . R   S �� *自社品名+$B TwoWay=�$C PropertyChanged=�D . E . 
  $L RightX�$V FalseX�$
K TrueX�$M FalseX�R   S �� *	依頼数+$B TwoWay=�$C PropertyChanged=�D . E . 
  $L RightX�$V FalseX�$
K TrueX�$M FalseX�R   S �� *	仕上数+$B TwoWay=�$C PropertyChanged=�D . E . 
  $K FalseX�X 
  	IsVisibleX .  $M FalseX�R   S �� *品番コード+$B TwoWay=�$C PropertyChanged=�D . E . 