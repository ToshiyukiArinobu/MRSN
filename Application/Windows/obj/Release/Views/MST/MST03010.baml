   M S B A M L   `   `   `  ���� X  TKyoeiSystem.Framework.Windows, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null�[clr-namespace:KyoeiSystem.Framework.Windows.ViewBase;assembly=KyoeiSystem.Framework.Windows&KyoeiSystem.Framework.Windows.ViewBase  �[clr-namespace:KyoeiSystem.Framework.Windows.ViewBase;assembly=KyoeiSystem.Framework.Windows&KyoeiSystem.Framework.Windows.ViewBase  a ]KyoeiSystem.Framework.Windows.Controls, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null�dclr-namespace:KyoeiSystem.Framework.Windows.Controls;assembly=KyoeiSystem.Framework.Windows.Controls&KyoeiSystem.Framework.Windows.Controls O Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem # KyoeiSystem.Application.Windows4   .KyoeiSystem.Application.Windows.Views.MST03010   bfw[clr-namespace:KyoeiSystem.Framework.Windows.ViewBase;assembly=KyoeiSystem.Framework.Windows  \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      R	 NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml 	    Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006 	    e
 aGrapeCity.WPF.SpreadGrid, Version=1.0.2015.1109, Culture=neutral, PublicKeyToken=5c5c8ff7a6858ccc=sg4http://schemas.grapecity.com/windows/spreadgrid/2012 
 :d2http://schemas.microsoft.com/expression/blend/2008 
 dvm[clr-namespace:KyoeiSystem.Framework.Windows.ViewBase;assembly=KyoeiSystem.Framework.Windows 
 octlsdclr-namespace:KyoeiSystem.Framework.Windows.Controls;assembly=KyoeiSystem.Framework.Windows.Controls 
 3System&clr-namespace:System;assembly=mscorlib 
 -   $��
MainWindow��  � AllowsTransparency  .  $	��728��$
��1024��$	��768��$
��1024�� � WindowStartupLocation$ CenterOwner=� � Left$ 0��
 � Top$ 0�� � Title$( "セット品番マスタ(MST03010)��.��(� $
��auto��$
��auto�� (� Stretch$
 Fill=� (� StretchDirection$
 Both=�.���  � RowDefinitions �� $
��auto ��� $	��657 �.+�+  %System.Windows.Controls.Ribbon.Ribbon  $��Stretch=�$��2��$��Stretch=�.@�.  (System.Windows.Controls.Ribbon.RibbonTab  $��	ホーム��   KeyTip$ h��$��75��.@�0  *System.Windows.Controls.Ribbon.RibbonGroup  $��90��  $��F1��	   KeyTip$	 MASTER��   RibbonFunctionBackground#��[  .@�1  +System.Windows.Controls.Ribbon.RibbonButton  -   
   LargeImageSource$%
 ../Images/15マスタ照会.png��$��0,-1,0,0q�   ToolTipTitle$ マスタ照会(F1)��   ToolTipDescription$3 -入力済みのデータを検索します。��   ToolTipImageSource$% ../Images/15マスタ照会.png��   Label$ ﾏｽﾀ照会��   KeyTip$ F1��$��50��  $��F2��$	 MASTER��#��[  .@�  $y�Hidden=�$  ��$  ��$  ��  $��F3��$	 MASTER��#��[  .@�  $y�Hidden=�$  ��$  ��$  ��  $��F4��$	 MASTER��#��[  .@�  $y�Hidden=�$  ��$  ��$  ��  $��F5��$	 MASTER��#��[  .@�  $y�Hidden=�$  ��$  ��$  ��  $��F6��$	 MASTER��#��[  .@�  $y�Hidden=�$  ��$  ��$  ��  $��F7��$	 MASTER��#��[  .@�  $y�Hidden=�$  ��$  ��$  ��  $��F8��$	 MASTER��#��[  .@�  -   $
 ../Images/22リスト.png��$ F8��$��50��$ 	リスト��$ リスト一覧(F8)��$! リストを表示します��$ ../Images/22リスト.png��  $��F9��$	 MASTER��#��[  .@�  -   $
 ../Images/24登録.png��$ F9��$��50��$ 登録��$ データ登録(F9)��$- '入力データの登録を行います��$ ../Images/24登録.png��y���  �� Path$ DataUpdateVisible� �� Mode$ TwoWay=� �� NotifyOnSourceUpdated .  �� NotifyOnTargetUpdated .   $	��F10��$	 MASTER��#��[  .@�  -   $
 ../Images/26クリア.png��$	 F10��$��50��$ 入力取消��$ 入力取消(F10)��$' !入力の取消しを行います��$ ../Images/26クリア.png��  -   $��delete_button��$	��F11��$	 MASTER��#��[  .@�  -   $
 ../Images/27終了.png��$	 F11��$��50��$ 終了��$ 終了(F11)��$ 終了します��$ ../Images/27終了.png��  $	��F12��$	 MASTER��#��[  .@�  -   $
 ../Images/28削除.png��$	 F12��$��50��$ 削除��$ 削除(F12)��$ 入力を削除します��$ ../Images/28削除.png��y��� $ DataUpdateVisible�$ TwoWay=� .  .   $��	ヘルプ��
�������.@�  $��サポート��.@�  -	   $%
 ../Images/30マニュアル.png��$ MANUAL��$ ﾏﾆｭｱﾙ��$ 取扱説明書��$* $取り扱い説明書を開きます��  -
   $(
 "../Images/29遠隔サポート.png��$ SUPPORT��$ お問合せ��$ お問合せ��$< 6Web上の｢お問合せ｣ページに遷移します��  -   $
 ../Images/QandA.png��$	 FAQ��$ QandA��$ QandA��$E ?Web上の｢よくあるご質問｣ページに遷移します��  -   $(
 "../Images/13共栄Webリンク.png��$ KYOEI��$ 共栄ｼｽﾃﾑ��$ 共栄システムHP��$< 6共栄システムのホームページを開きます��  $��実施状況入力��
�������   ContextualTabGroupHeader$ 入力補助��.@�  $'��!従来ファンクションキー��.@�  $%
 ../Images/16マスタ入力.png��$ d��$ 	行追加��$ 行追加(F5)��$' !売上の履歴を表示します��$% ../Images/16マスタ入力.png��  -   $
 ../Images/24登録.png��$ e��$ マスタ登録��$ 伝票登録(F9)��$$ 伝票の登録を行います��$ ../Images/24登録.png��  -   $
 ../Images/26クリア.png��$ f��$ 入力取消し��$ 入力取消(F12)��$' !入力の取消しを行います��$ ../Images/26クリア.png��  -   $
 ../Images/27終了.png��$ g��$ 終了��$ 入力取消(F11)��$ 終了します��$ ../Images/27終了.png��  $��一括入力��
�������$ 入力補助��.@�  $'��!従来ファンクションキー��.@�  $
 ../Images/22リスト.png��$ d��$ プレビュー��$ プレビュー(F5)��$' !売上の履歴を表示します��$ ../Images/22リスト.png��  -   $
 ../Images/24登録.png��$ e��$ 印刷��$ 
印刷(F9)��$$ 伝票の登録を行います��$ ../Images/24登録.png��  -   $
 ../Images/24登録.png��$ e��$ マスタ登録��$ マスタ登録(F9)��$$ 伝票の登録を行います��$ ../Images/24登録.png��  -   $
 ../Images/26クリア.png��$ f��$ 入力取消し��$ 入力取消(F12)��$' !入力の取消しを行います��$ ../Images/26クリア.png��  -   $
 ../Images/27終了.png��$ g��$ 終了��$ 入力取消(F11)��$ 終了します��$ ../Images/27終了.png��� $��0��$��Bottom=� �� $��74 � � ColumnDefinitions �� $��90 ��� $��* �.+�]� $��0��$��0��$��Stretch=�$��2q����� *MaintenanceModeBackground+.+��� $��メイリオ)�$��24(�$��Medium"�$��Stretch=�$��Center=�$��Center=�$��Center=����� *MaintenanceMode+���� *MaintenanceModeForeground+�� $��0��$��1��$��Bottom=�
��� ���
���  ��$��2,0,2,0q����� *ErrorMessage+y��� *ErrorMessageVisibility+�� $��1�����#2792c3$��1q�$��5,5,5,52q�
�������._�� $��15,15,15,40q� �� $
��auto ��� $��3 ��� $��* �.+��  �� $
��auto ��� $
��auto �.+�?  9KyoeiSystem.Framework.Windows.Controls.UcLabelTwinTextBox  -   $��txtMyProduct��$��0��=  7KyoeiSystem.Framework.Windows.Controls.FrameworkControl   	IsKeyItem .    
IsRequired . $
��Left=�   Label_Context$ セット品番��   Label_Width$	 100Q�   Label1Visibility$ 	Collapsed=�   Label2Visibility$ 	Collapsed=�   
Text1Width$ 90Q�   Text1MaxLength$ 12��   
Text2Width$	 200Q�   Text2IsReadOnly .     Text2BorderBrush
  ����   -   $��
txtMyColor��$��1�� . !   DataAccessName$! M06_IRO��"   MasterCheckEnabled" . $
��Left=�$ セット品番色��$	 100Q�$ 	Collapsed=�$ 	Collapsed=�$ 45Q�$ 3��$	 200Q� . 
  ���� / 
 )GrapeCity.Windows.SpreadGrid.GcSpreadGrid  -   $��spComponent��$��2��#   HighlightHeader$
# None=�$   
LayoutMode$
$ Cell=�%   	Protected% . $
��Left=�$��Stretch=�$&   HorizontalScrollBarVisibility$
& Auto=�'   CanUserSortColumns' .  (   ColumnDragMode$( Direct=�)   AutoGenerateColumns) .  *   DocumentUri$K* E/KyoeiSystem.Application.Windows;component/views/MST/MST03010G1.sgxml?�$��2,2,2,2q�+   ItemsSource+ �� $ SetKouseihin�$ TwoWay=�, �� UpdateSourceTrigger$, PropertyChanged=� .  . -   SelectionBorderUnfocused- - 
 'GrapeCity.Windows.SpreadGrid.BorderLine  .   Color$. Orange��/   Style$/ Thick=�0   SelectionBorder0   $. Orange��$/ Thick=�1   Columns1 )	 
 #GrapeCity.Windows.SpreadGrid.Column	  2 	  	Focusable$2 FalseX�3 	  Locked$
3 TrueX�4 	  	IsVisible4 .  5 	  	DataField5 3
 
 -GrapeCity.Windows.SpreadGrid.BindingDataField
  6 
  Binding6 �� $ 品番コード�	  $2 FalseX�$
3 TrueX�4 .  5 
  6 �� $	 行�	  $
2 TrueX�7 	  CellType7 / 
 )GrapeCity.Windows.SpreadGrid.TextCellType  8   ImeConversionMode$8 Native=�5 
  6 �� $ 材料品番�$ TwoWay=�$, PropertyChanged=� .  . 	  $
2 TrueX�$
3 TrueX�5 
  6 �� $ 材料品名�	  $
2 TrueX�$
3 TrueX�5 
  6 �� $ 	材料色�	  $
2 TrueX�9 	  HorizontalAlignment$9 RightX�7 1 
 +GrapeCity.Windows.SpreadGrid.NumberCellType  :   MaxValue$: 99999999~�;   MinValue$; 0~�<   FieldSetF 
 @GrapeCity.Windows.SpreadGrid.Editors.NumberFieldSetTypeConverter$< ##,###,##0,,,-, =   SpinButtonVisibility$= NotShow=�5 
  6 �� $ 数量�$ TwoWay=�$, PropertyChanged=� .  . 