   M S B A M L   `   `   `  ����X  TKyoeiSystem.Framework.Windows, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null�[clr-namespace:KyoeiSystem.Framework.Windows.ViewBase;assembly=KyoeiSystem.Framework.Windows&KyoeiSystem.Framework.Windows.ViewBase  5   	   �[clr-namespace:KyoeiSystem.Framework.Windows.ViewBase;assembly=KyoeiSystem.Framework.Windows&KyoeiSystem.Framework.Windows.ViewBase  5   	   a ]KyoeiSystem.Framework.Windows.Controls, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null�dclr-namespace:KyoeiSystem.Framework.Windows.Controls;assembly=KyoeiSystem.Framework.Windows.Controls&KyoeiSystem.Framework.Windows.Controls 5	   	   O Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem 5
   	   # KyoeiSystem.Application.Windows4   .KyoeiSystem.Application.Windows.Views.MST18010   5   	   bfw[clr-namespace:KyoeiSystem.Framework.Windows.ViewBase;assembly=KyoeiSystem.Framework.Windows  \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      5   	   R	 NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml 	    5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006 	    5   	   e
 aGrapeCity.WPF.SpreadGrid, Version=1.0.2015.1109, Culture=neutral, PublicKeyToken=5c5c8ff7a6858ccc=sg4http://schemas.grapecity.com/windows/spreadgrid/2012 
 5   	   :d2http://schemas.microsoft.com/expression/blend/2008 
 5   	   dvm[clr-namespace:KyoeiSystem.Framework.Windows.ViewBase;assembly=KyoeiSystem.Framework.Windows 
 5   	   octlsdclr-namespace:KyoeiSystem.Framework.Windows.Controls;assembly=KyoeiSystem.Framework.Windows.Controls 
 5	   	   3System&clr-namespace:System;assembly=mscorlib 
 5
   	   -   5        $��
MainWindow��5   .     � AllowsTransparency  .   � Title$4 .得意先個建単価マスタ入力(MST18010)��5      $	��728��6"   $
��1024��62   $	��768��6B   $
��1024��6O    � WindowStartupLocation$ CenterOwner=�6\    � Left$ 0��6�   
 � Top$ 0��6�   .��5      (� $
��auto��6   $
��auto��6    (� Stretch$
 Fill=�6)    (� StretchDirection$
 Both=�68   .��5   
   �  � RowDefinitions 5      �� 5      $
��auto �6    �� 5      $	��657 �6    .+�5      +  %System.Windows.Controls.Ribbon.Ribbon  $��Stretch=�6   $��2��64   $��Stretch=�6H   .@�5      .  (System.Windows.Controls.Ribbon.RibbonTab  $��	ホーム��6      KeyTip$ h��6)   $��75��64   .@�5      0  *System.Windows.Controls.Ribbon.RibbonGroup  $��90��6"   5        5      $��F1��6"   	   KeyTip$	 MASTER��6.      RibbonFunctionBackground#��[  6>   .@�5      1  +System.Windows.Controls.Ribbon.RibbonButton  -   5        
   LargeImageSource$%
 ../Images/15マスタ照会.png��5   '   $��0,-1,0,0q�6P      ToolTipTitle$ マスタ照会(F1)��6b      ToolTipDescription$3 -入力済みのデータを検索します。��6{      ToolTipImageSource$% ../Images/15マスタ照会.png��6�      Label$ ﾏｽﾀ照会��5          KeyTip$ F1��6B   $��50��6N   5!        5"      $��F2��6"   $	 MASTER��6.   #��[  6>   .@�5#        -   5        $%
 ../Images/16マスタ入力.png��5#   '   $ ﾏｽﾀ入力��6P   $ F2��6y   $��50��6�   $ マスタ入力(F2)��5$   '   $- 'マスタの入力画面を開きます��6@   $% ../Images/16マスタ入力.png��6d   5%        5&      $��F3��6"   $	 MASTER��6.   #��[  6>   .@�5'        $y�Hidden=�6'   $  ��6;   $  ��6D   $  ��6T   5(        5)      $��F4��6"   $	 MASTER��6.   #��[  6>   .@�5*        $y�Hidden=�6'   $  ��6;   $  ��6D   $  ��6T   5+        5,      $��F5��6"   $	 MASTER��6.   #��[  6>   .@�5-        $y�Hidden=�6'   $  ��6;   $  ��6D   $  ��6T   5.        5/      $��F6��6"   $	 MASTER��6.   #��[  6>   .@�50        $y�Hidden=�6'   $  ��6;   $  ��6D   $  ��6T   51        52      $��F7��6"   $	 MASTER��6.   #��[  6>   .@�53        $y�Hidden=�6'   $  ��6;   $  ��6D   $  ��6T   54        55      $��F8��6"   $	 MASTER��6.   #��[  6>   .@�56        -   5        $
 ../Images/22リスト.png��56   '   $ F8��6i   $��50��6u   $ 	リスト��57      $ リスト一覧(F8)��6   $! リストを表示します��60   $ ../Images/22リスト.png��6P   58        59      $��F9��6"   $	 MASTER��6.   #��[  6>   .@�5:        -   5        $
 ../Images/24登録.png��5:   '   $ F9��6h   $��50��6t   $ 登録��5;      $ データ登録(F9)��6   $- '入力データの登録を行います��6/   $ ../Images/24登録.png��6R   5<        5=      $	��F10��6"   $	 MASTER��6/   #��[  6?   .@�5>        -   5        $
 ../Images/26クリア.png��5>   '   $	 F10��6i   $��50��6v   $ 入力取消��5?      $ 入力取消(F10)��6   $' !入力の取消しを行います��61   $ ../Images/26クリア.png��6R   5@        5A      -   5        $��delete_button��5A   ?   $	��F11��6"   $	 MASTER��6/   #��[  6T   .@�5B        -   5        $
 ../Images/27終了.png��5B   '   $	 F11��6h   $��50��6u   $ 終了��5C      $ 終了(F11)��6   $ 終了します��6-   $ ../Images/27終了.png��6H   5D        5E      $	��F12��6"   $	 MASTER��6/   #��[  6?   .@�5F        -	   5        $
 ../Images/28削除.png��5F   '   $	 F12��6h   $��50��6u   $ 削除��5G      $ 削除(F12)��6   $ 入力を削除します��6-   $ ../Images/28削除.png��6K   5H      5I        5K      $��	ヘルプ��6   
�������.@�5L        $��サポート��6"   .@�5M        -
   5        $%
 ../Images/30マニュアル.png��5M   '   $ MANUAL��6P   $ ﾏﾆｭｱﾙ��5N      $ 取扱説明書��6   $* $取り扱い説明書を開きます��6.     5O      -   5        $(
 "../Images/29遠隔サポート.png��5O   '   $ SUPPORT��6Q   $ お問合せ��5P      $ お問合せ��6   $< 6Web上の｢お問合せ｣ページに遷移します��6,     5Q      -   5        $
 ../Images/QandA.png��5Q   '   $	 FAQ��6N   $ QandA��5R      $ QandA��6   $E ?Web上の｢よくあるご質問｣ページに遷移します��6.     5S      -   5        $(
 "../Images/13共栄Webリンク.png��5S   '   $ KYOEI��6T   $ 共栄ｼｽﾃﾑ��5T      $ 共栄システムHP��6   $< 6共栄システムのホームページを開きます��62   5U      5V        5X      $��実施状況入力��6   
�������   ContextualTabGroupHeader$ 入力補助��6>   .@�5Y        $'��!従来ファンクションキー��6"   .@�5Z        $%
 ../Images/16マスタ入力.png��6'   $ d��6P   $ 	行追加��5[      $ 行追加(F5)��6   $' !売上の履歴を表示します��6/   $% ../Images/16マスタ入力.png��6Q     5\      -   5        $
 ../Images/24登録.png��5\   '   $ e��6h   $ マスタ登録��5]      $ 伝票登録(F9)��6   $$ 伝票の登録を行います��62   $ ../Images/24登録.png��6R     5^      -   5        $
 ../Images/26クリア.png��5^   '   $ f��6i   $ 入力取消し��5_      $ 入力取消(F12)��6   $' !入力の取消しを行います��63   $ ../Images/26クリア.png��6T     5`      -   5        $
 ../Images/27終了.png��5`   '   $ g��6h   $ 終了��5a      $ 入力取消(F11)��6   $ 終了します��60   $ ../Images/27終了.png��6K   5b      5c        5d      $��一括入力��6   
�������$ 入力補助��6<   .@�5e        $'��!従来ファンクションキー��6"   .@�5f        $
 ../Images/22リスト.png��6'   $ d��6N   $ プレビュー��5g      $ プレビュー(F5)��6   $' !売上の履歴を表示します��63   $ ../Images/22リスト.png��6U     5h      -   5        $
 ../Images/24登録.png��5h   '   $ e��6h   $ 印刷��5i      $ 
印刷(F9)��6   $$ 伝票の登録を行います��6-   $ ../Images/24登録.png��6M     5j      -   5        $
 ../Images/24登録.png��5j   '   $ e��6h   $ マスタ登録��5k      $ マスタ登録(F9)��6   $$ 伝票の登録を行います��62   $ ../Images/24登録.png��6R     5l      -   5        $
 ../Images/26クリア.png��5l   '   $ f��6i   $ 入力取消し��5m      $ 入力取消(F12)��6   $' !入力の取消しを行います��63   $ ../Images/26クリア.png��6T     5n      -   5        $
 ../Images/27終了.png��5n   '   $ g��6h   $ 終了��5o      $ 入力取消(F11)��6   $ 終了します��60   $ ../Images/27終了.png��6K   5p      5q      5r      � 5s      $��0��6   $��Bottom=�6     5t      �� 5u      $��74 �6$    � ColumnDefinitions 5w      �� 5x      $��90 �6'   �� 5y      $��* �6'   .+�5|      ]� $��0��6   $��0��6)   $��Stretch=�69   $��2q�6W   ��6b   �� *MaintenanceModeBackground+.+�5}      �� $��メイリオ)�6q   $��24(�6�   $��Medium"�6�   $��Stretch=�5~      $��Center=�6:   $��Center=�6^   $��Center=�6y   ��5}      �� *MaintenanceMode+��6@   �� *MaintenanceModeForeground+5~   y   5      �� 5�      $��0��6   $��1��6%   $��Bottom=�65   
��� ���
���  ��$��2,0,2,0q�6u   ��5�      �� *ErrorMessage+y�6/   �� *ErrorMessageVisibility+5�      �� 5�      $��1��6   ���#2792c3$��1q�68   $��5,5,5,52q�6L   
�������._�5�      � $��15,15,15,40q�6    5�      �� 5�      $��30 �6(   �� 5�      $��3 �6(   �� 5�      $��30 �6(   �� 5�      $��3 �6(   �� 5�      $��30 �6(   �� 5�      $��3 �6(   �� 5�      $��30 �6(   �� 5�      $��3 �6(   �� 5�      $��30 �6(   �� 5�      $��3 �6(   �� 5�      $��30 �6(   �� 5�      $��3 �6(   �� 5�      $��15 �6(   �� 5�      $��3 �6(   �� 5�      $	��200 �6(    5�      �� 5�      $
��auto �6+   .+�5�      �� -   5        $��btnserch��5�      $��10��6-   $��3��6;   $��2��6L   $	��110��6`   $��40��6l   
����  �.��5�      �� $��18(�6$   �� 5�      $��全 件 検 索��6$   
��� ���66   5�      5�      ?  9KyoeiSystem.Framework.Windows.Controls.UcLabelTwinTextBox  5�      -   5        $��LabelTextTokuisaki��5�   .      Label_Context$ 得意先ID��6H      Label_Width$	 100Q�6^   $
��Left=�6p   $��0��6�      Label1Visibility$ 	Collapsed=�6�      Label2Visibility$ 	Collapsed=�6�      
Text1Width$	 100Q�5�   *      
Text2Width$	 200Q�6;      DataAccessName$ M01_TOK��6L      OutputColumnName$ 	取引先��6e      Text1ValidationType$ Integer=�6|   =  7KyoeiSystem.Framework.Windows.Controls.FrameworkControl   	IsKeyItem .    
IsRequired .    MasterCheckEnabled .    Text1MaxLength$ 8��6�      Text1 5�   *   ��   �� Path$  得意先ID�! �� Mode$! TwoWay=�" �� NotifyOnSourceUpdated" . # �� NotifyOnTargetUpdated# . $   Text2$ 5�   *   �� $  得意先名�$! TwoWay=�" . # . %   LinkItem% 5�   *   �� $  取引区分�$! TwoWay=�" . # . 5�   *     5�      -   5        $��LabelTextTyakuti��5�   .   $ 着地ID��6F   $	 100Q�6[   $
��Left=�6m   $��2��6�   $ 	Collapsed=�6�   $ 	Collapsed=�6�   $	 100Q�5�   *   $	 200Q�6;   $ 
M08_TIK_UC��6L   $ 	発着地��6h   $ Integer=�6    .  .  . $ 5��6�    5�   *   �� $  着地ID�$! TwoWay=�" . # . $ 5�   *   �� $  	着地名�$! TwoWay=�" . # . 5�   *   � 5�      $��4��6    5�      �� 5�      $	��300 �6/   �� 5�      $	��200 �6/   .+�5�      ;  5KyoeiSystem.Framework.Windows.Controls.UcLabelTextBox  -   5        $��LabelTextOmosa��5�   .   &   Label_Context$& 重量��6D   $	��274��6W   '   Label_Width$	' 100Q�6c   $
��Left=�6u   (   ImeType$	( Off=�5�   &   )   ValidationType$) Decimal=�64    . *   cTextAlignment$* Right=�6^   +   MaxValue$+ 
999999.999��6u   ,   MinValue$, 0.000��6�   -   Text- 5�   &   �� *重量+$! TwoWay=�. �� StringFormat$. #0.000��" . # . / �� UpdateSourceTrigger$/ 	LostFocus=�5�   &   7  1KyoeiSystem.Framework.Windows.Controls.UcCheckBox  5�      -   5        $��JyuryoCheck��5�   *   $��1��6=   0   cContent$!0 検索条件に含めない��6M   1   	IsChecked1 5�   &   �� *#!重量チェックボックス値+$! TwoWay=�" . # . 5�      � 5�      $��6��6    5�      �� 5�      $	��300 �6/   �� 5�      $	��200 �6/   .+�5�        -   5        $��LabelTextkosuu��5�   .   $& 個数��6D   $	' 100Q�6W   $	��274��6i   $
��Left=�6u   $	( Off=�5�   &   $) Decimal=�64   2   Mask$
2 #0.0��6M    . $* Right=�6j   $+ 
99999999.9��6�   $	, 0.0��6�   - 5�   &   �� *個数+$! TwoWay=�$
. #0.0��" . # . $/ 	LostFocus=�5�   &     5�      -   5        $��
KosuuCheck��5�   *   $��1��6<   $!0 検索条件に含めない��6L   1 5�   &   �� *#!個数チェックボックス値+$! TwoWay=�" . # . 5�        5�      -   5        3  Name$3 KodateTanka��5�   *   $& 個建単価��6?   $	' 100Q�6T   $��8��6f   $��	0,0,700,0q�6s   $	( Off=�5�   &   $* Right=�64   $) Decimal=�6K   $+ 
9999999.99��6d   $
, 0.00��6z   - 5�   &   �� *個建単価+$! TwoWay=�$. #,0.00��" . # . $/ 	LostFocus=�5�   &     5�      -   5        $3 Kingaku��5�   *   $& 個建金額��6;   $	' 100Q�6P   $��10��6b   $��	0,0,700,0q�6p   $	( Off=�5�   &   $* Right=�64   $+ 	999999999��6K   $, 0��6`   - 5�   &   �� *個建金額+$! TwoWay=�$	. #,0��" . # . 5�   &   �� 5�      $��1q�6   ���#2792c3$��
0,0,0,-148q�6G   $��14��6[   
����·�$	��974��6�   ._�5�      /	 
 )GrapeCity.Windows.SpreadGrid.GcSpreadGrid	  -   5        $��spKodate��5�   *   $��Stretch=�6:   $��Stretch=�6X   $��14��6t   $��0��6�   $4 	  HorizontalScrollBarVisibility$
4 Auto=�5�   1   5 	  HighlightHeader$
5 None=�6V   6 	  
LayoutMode$
6 Cell=�6m   7 	  ColumnCount$7 9��6   8 	  RowCount$8 1��6�   9 	  CanUserSortColumns9 .  : 	  ColumnDragMode$: Direct=�5�   L   ; 	  AutoGenerateColumns; .  < 	  DocumentUri$K< E/KyoeiSystem.Application.Windows;component/views/MST/MST18010G1.sgxml?�5�   1   $��2,2,2,2q�6�   $��2��6�   = 	  ItemsSource= 5�   1   �� *KodateKingaku+$! TwoWay=�$/ PropertyChanged=�" . # . > J� InputBindings> 5�      �� 5�   "   
? �� Key$? Enter��6-   @ �� Command@ 69   �� $!  NavigationCommands.MoveNext�A �� ElementName$A spKodate��B 	  ColumnsB 5�      )
 
 #GrapeCity.Windows.SpreadGrid.Column
  5�   "   C 
  Locked$
C TrueX�6,   D 
  	Focusable$D FalseX�6:   E 
  	DataFieldE 5�   &   3 
 -GrapeCity.Windows.SpreadGrid.BindingDataField  5�   *   F   BindingF 6>   �� *取引先ID+5�   #   
  5�   "   $
C TrueX�6,   $D FalseX�6:   E 5�   &     5�   *   F 6>   �� *
着地ID+5�   #   
  5�   "   $
C TrueX�6,   $D FalseX�6:   E 5   &     5  *   F 6>   �� *取引先名+5  #   
  5  "   $
C TrueX�6,   $D FalseX�6:   E 5  &     5  *   F 6>   �� *	着地名+5  #   
  5	  "   $
C TrueX�6,   $D FalseX�6:   G 
  CellTypeG 5
  &   1 
 +GrapeCity.Windows.SpreadGrid.NumberCellType  5  *   H   DropDownButtonVisibility$H NotShow=�6<   I   SpinButtonVisibility$I NotShow=�6_   E 5  &     5  *   F 6>   �� *重量+5  #   
  5  "   $
C TrueX�6,   $D FalseX�6:   G 5  &     5  *   $H NotShow=�6<   $I NotShow=�6_   E 5  &     5  *   F 6>   �� *個数+5  #   
  5  "   J 
  HorizontalAlignment$J RightX�6,   G 5  &     5  *   K   MaxValue$K 	999999999~�6=   L   MinValue$L 
-999999999~�6R   $I NotShow=�6h   M   FieldSetF 
 @GrapeCity.Windows.SpreadGrid.Editors.NumberFieldSetTypeConverter$M ###,###,##0.0,,,-, 6�   E 5  &     5  *   F 6>   �� *個建単価+5   #   
  5!  "   $J RightX�6,   G 5"  &     5#  *   $K 	999999999~�6=   $L 
-999999999~�6R   $I NotShow=�6h   $M ###,###,##0,,,-, 6�   E 5%  &     5&  *   F 6>   �� *d個建金額+5(  #   
  5)  "   $J RightX�6,   $
C TrueX�6H   $D FalseX�6V   G 5*  &     5+  *   $K 	999999999~�6=   $L 
-999999999~�6R   $I NotShow=�6h   $M ###,###,##0,,,-, 6�   E 5-  &     5.  .   F 6B   �� *	d運賃+50  #   52     53     54     55     56     57     58     