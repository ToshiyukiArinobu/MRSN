   M S B A M L   `   `   `  ����X  TKyoeiSystem.Framework.Windows, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null�[clr-namespace:KyoeiSystem.Framework.Windows.ViewBase;assembly=KyoeiSystem.Framework.Windows&KyoeiSystem.Framework.Windows.ViewBase  5      �[clr-namespace:KyoeiSystem.Framework.Windows.ViewBase;assembly=KyoeiSystem.Framework.Windows&KyoeiSystem.Framework.Windows.ViewBase  5   	   a ]KyoeiSystem.Framework.Windows.Controls, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null�dclr-namespace:KyoeiSystem.Framework.Windows.Controls;assembly=KyoeiSystem.Framework.Windows.Controls&KyoeiSystem.Framework.Windows.Controls 5   	   �dclr-namespace:KyoeiSystem.Framework.Windows.Controls;assembly=KyoeiSystem.Framework.Windows.Controls&KyoeiSystem.Framework.Windows.Controls 5	   	   [ WSystem.Windows.Forms, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089Y@clr-namespace:System.Windows.Forms;assembly=System.Windows.FormsSystem.Windows.Forms 5
   	   # KyoeiSystem.Application.Windows4   .KyoeiSystem.Application.Windows.Views.MST33010   5      bfw[clr-namespace:KyoeiSystem.Framework.Windows.ViewBase;assembly=KyoeiSystem.Framework.Windows  \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      5   	   R	 NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml 	    5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006 	    5   	   @d2http://schemas.microsoft.com/expression/blend/2008 	    5   	   jvm[clr-namespace:KyoeiSystem.Framework.Windows.ViewBase;assembly=KyoeiSystem.Framework.Windows 	    5   	   uctlsdclr-namespace:KyoeiSystem.Framework.Windows.Controls;assembly=KyoeiSystem.Framework.Windows.Controls 	    5   	   yControlsdclr-namespace:KyoeiSystem.Framework.Windows.Controls;assembly=KyoeiSystem.Framework.Windows.Controls 	    5	   	   Owf@clr-namespace:System.Windows.Forms;assembly=System.Windows.Forms 	    5
   	   e
 aGrapeCity.WPF.SpreadGrid, Version=1.0.2015.1109, Culture=neutral, PublicKeyToken=5c5c8ff7a6858ccc=sg4http://schemas.grapecity.com/windows/spreadgrid/2012 
 5      -   5          � Language$  ja-JP�5       � WindowStyle$ SingleBorderWindow=�5       � AllowsTransparency .   � Title$. (部門別予算マスタ保守(MST33010)��5      $	��728��6    $
��1024��60   $	��768��6@   $
��1024��6M    � WindowStartupLocation$ CenterOwner=�6\    � Left$ 0��6�   
 � Top$ 0��6�   .��5   
   �  � RowDefinitions 5      �� 5      $
��auto �6    �� 5      $	��600 �6    .+�5      +  %System.Windows.Controls.Ribbon.Ribbon  $��Stretch=�6   $��Stretch=�63   .@�5      .  (System.Windows.Controls.Ribbon.RibbonTab  $��	ホーム��6      KeyTip$ h��6)   $��75��64   .@�5      0  *System.Windows.Controls.Ribbon.RibbonGroup  $��90��6"   5         5"      $��F1��6"   	   KeyTip$	 MASTER��6.      RibbonFunctionBackground#��[  6>   .@�5#      1  +System.Windows.Controls.Ribbon.RibbonButton  -   5        
   LargeImageSource$%
 ../Images/15マスタ照会.png��5#   '   $��0,-1,0,0q�6P      Label$ 検索��5$      $y�Hidden=�6      KeyTip$ F1��6B   $��50��6N   5%        5&      $��F2��6"   $	 MASTER��6.   #��[  6>   .@�5'        $y�Hidden=�6'   $  ��6;      ToolTipTitle$  ��6D      ToolTipDescription$  ��6T   5(        5)      $��F3��6"   $	 MASTER��6.   #��[  6>   .@�5*        $y�Hidden=�6'   $  ��6;   $  ��6D   $  ��6T   5+        5,      $��F4��6"   $	 MASTER��6.   #��[  6>   .@�5-        $y�Hidden=�6'   $  ��6;   $  ��6D   $  ��6T   5.        5/      $��F5��6"   $	 MASTER��6.   #��[  6>   .@�50        $y�Hidden=�6'   $  ��6;   $  ��6D   $  ��6T   51        52      $��F6��6"   $	 MASTER��6.   #��[  6>   .@�53        $y�Hidden=�6'   $  ��6;   $  ��6D   $  ��6T   54        55      $��F7��6"   $	 MASTER��6.   #��[  6>   .@�56        $y�Hidden=�6'   $  ��6;   $  ��6D   $  ��6T   57        58      $��F8��6"   $	 MASTER��6.   #��[  6>   .@�59        $y�Hidden=�6'   $  ��6;   $  ��6D   $  ��6T   5:        5;      $��F9��6"   $	 MASTER��6.   #��[  6>   .@�5<        -   5        $
 ../Images/24登録.png��5<   '   $ F9��6h   $��50��6t   $ 登録��5=      $ データ登録(F9)��6   $- '入力データの登録を行います��6,      ToolTipImageSource$ ../Images/24登録.png��6O   5>        5?      $	��F10��6"   $	 MASTER��6/   #��[  6?   .@�5@        -   5        $
 ../Images/26クリア.png��5@   '   $	 F10��6i   $��50��6v   $ 入力取消��5A      $ 入力取消(F10)��6   $' !入力の取消しを行います��6.   $ ../Images/26クリア.png��6O   5B        5C      $	��F11��6"   $	 MASTER��6/   #��[  6?   .@�5D        -   5        $
 ../Images/27終了.png��5D   '   $	 F11��6h   $��50��6u   $ 終了��5E      $ 終了(F11)��6   $ 終了します��6*   $ ../Images/27終了.png��6E   5F        5G      $	��F12��6"   $	 MASTER��6/   #��[  6?   .@�5H        $y�Hidden=�6'   $  ��6;   $  ��6D   $  ��6T   5I      5K        5M      $��	ヘルプ��6   
�������.@�5N        $��サポート��6"   .@�5O        -   5        $%
 ../Images/30マニュアル.png��5O   '   $ MANUAL��6P   $ ﾏﾆｭｱﾙ��5P      $ 取扱説明書��6   $* $取り扱い説明書を開きます��6+     5Q      -   5        $(
 "../Images/29遠隔サポート.png��5Q   '   $ SUPPORT��6Q   $ お問合せ��5R      $ お問合せ��6   $< 6Web上の｢お問合せ｣ページに遷移します��6)     5S      -   5        $
 ../Images/QandA.png��5S   '   $	 FAQ��6N   $ QandA��5T      $ QandA��6   $E ?Web上の｢よくあるご質問｣ページに遷移します��6+     5U      -	   5        $(
 "../Images/13共栄Webリンク.png��5U   '   $ KYOEI��6T   $ 共栄ｼｽﾃﾑ��5V      $ 共栄システムHP��6   $< 6共栄システムのホームページを開きます��6/   5W      5X        5Z      $��実施状況入力��6   
�������   ContextualTabGroupHeader$ 入力補助��6>   .@�5[        $'��!従来ファンクションキー��6"   .@�5\        $%
 ../Images/16マスタ入力.png��6'   $ d��6P   $ 	行追加��5]      $ 行追加(F5)��6   $' !売上の履歴を表示します��6+   $% ../Images/16マスタ入力.png��6M     5^      -
   5        $
 ../Images/24登録.png��5^   '   $ e��6h   $ マスタ登録��5_      $ 伝票登録(F9)��6   $$ 伝票の登録を行います��6.   $ ../Images/24登録.png��6N     5`      -   5        $
 ../Images/26クリア.png��5`   '   $ f��6i   $ 入力取消し��5a      $ 入力取消(F12)��6   $' !入力の取消しを行います��6/   $ ../Images/26クリア.png��6P     5b      -   5        $
 ../Images/27終了.png��5b   '   $ g��6h   $ 終了��5c      $ 入力取消(F11)��6   $ 終了します��6,   $ ../Images/27終了.png��6G   5d      5e        5f      $��一括入力��6   
�������$ 入力補助��6<   .@�5g        $'��!従来ファンクションキー��6"   .@�5h        $
 ../Images/22リスト.png��6'   $ d��6N   $ プレビュー��5i      $ プレビュー(F5)��6   $' !売上の履歴を表示します��6/   $ ../Images/22リスト.png��6Q     5j      -   5        $
 ../Images/24登録.png��5j   '   $ e��6h   $ 印刷��5k      $ 
印刷(F9)��6   $$ 伝票の登録を行います��6)   $ ../Images/24登録.png��6I     5l      -   5        $
 ../Images/24登録.png��5l   '   $ e��6h   $ マスタ登録��5m      $ マスタ登録(F9)��6   $$ 伝票の登録を行います��6/   $ ../Images/24登録.png��6O     5n      -   5        $
 ../Images/26クリア.png��5n   '   $ f��6i   $ 入力取消し��5o      $ 入力取消(F12)��6   $' !入力の取消しを行います��6/   $ ../Images/26クリア.png��6P     5p      -   5        $
 ../Images/27終了.png��5p   '   $ g��6h   $ 終了��5q      $ 入力取消(F11)��6   $ 終了します��6,   $ ../Images/27終了.png��6G   5r      5s      5t      � 5v      $��0��6   $��Bottom=�6     5w      �� 5x      $��74 �6$    � ColumnDefinitions 5z      �� 5{      $��90 �6'   �� 5|      $��* �6'   .+�5      ]� $��0��6   $��0��6)   $��Stretch=�69   $��2q�6W   ��5�      �� *MaintenanceModeBackground+.+�5�      �� $��メイリオ)�5�      $��24(�6   $��Medium"�6'   $��Stretch=�5�      $��Center=�6%   $��Center=�6I   $��Center=�6d   ��5�      �� *MaintenanceMode+��6@   �� *MaintenanceModeForeground+5�   d   5�      �� 5�      $��0��6   $��1��6%   $��Bottom=�65   
��� ���
���  ��$��2,0,2,0q�6u   ��5�      �� *ErrorMessage+y�6'   �� *ErrorMessageVisibility+5�      �� 5�      $��1��6   ���#2792c3$��1q�68   $��5q�6L   
�������._�5�      � $��15q�6    5�      �� 5�      $��* �6+   .+�5�      � $��0��6    5�      �� 5�      $��5 �6,   �� 5�      $��38 �6,   �� 5�      $��* �6,   .+�5�      � $��1��6$   .+�5�      ;  5KyoeiSystem.Framework.Windows.Controls.UcLabelTextBox  -   5        $��
SakuseiNen��5�   2      Label_Context$ 作成年月��6D      ValidationType$ Integer=�6Y      Label_Width$	 100Q�6r   $	��300��6�   $��0��6�   $��2��6�   $��5,0,0,0q�6�   $
��Left=�6�   =  7KyoeiSystem.Framework.Windows.Controls.FrameworkControl   	IsKeyItem .    
cMaxLength$ 7��5�   *      Text 5�   *   �� *作成年月+ �� Mode$ TwoWay=� �� NotifyOnSourceUpdated .  �� NotifyOnTargetUpdated . 5�   9   �� 5�      -   5        $��前年データセット��5�   %   $��30��68   $��10,0,0,0q�6D   6V   5�      / 
 )GrapeCity.Windows.SpreadGrid.GcSpreadGrid  5�      -   5        $��sp燃費目標データ��5�   )   $��	10,5,10,1q�6:   $��2��6M      AutoGenerateColumns .     
LayoutMode$
 Cell=�5�   4      ColumnCount$ 14��6F      RowCount$ 10��6W      DocumentUri$K E/KyoeiSystem.Application.Windows;component/views/MST/MST33010G1.sgxml?�5�          ItemsSource  5�      �� *部門別予算データ+$ TwoWay=� .  . ! �� UpdateSourceTrigger$! PropertyChanged=�"   Columns" 5�   "   ) 
 #GrapeCity.Windows.SpreadGrid.Column  5�   &   #   Locked$
# TrueX�60   $   	DataField$ 5�   *   3	 
 -GrapeCity.Windows.SpreadGrid.BindingDataField	  5�   .   % 	  Binding% 6B   �� *
部門ID+5�   '     5�   &   $
# TrueX�60   $ 5�   *   	  5�   .   % 6B   �� *	部門名+5�   '     5�   &   &   HorizontalAlignment$& RightX�60   '   CellType' 5�   &   1
 
 +GrapeCity.Windows.SpreadGrid.NumberCellType
  5�   *   ( 
  MaxValue$( 	999999999~�6=   ) 
  MinValue$) 
-999999999~�6R   * 
  SpinButtonVisibility$* NotShow=�6h   + 
  FieldSetF 
 @GrapeCity.Windows.SpreadGrid.Editors.NumberFieldSetTypeConverter$+ ###,###,##0,,,-, 6�   $ 5�   &   	  5�   .   % 6B   �� *月1+5�   '     5�   &   $& RightX�60   ' 5�   &   
  5�   *   $( 	999999999~�6=   $) 
-999999999~�6R   $* NotShow=�6h   $+ ###,###,##0,,,-, 6�   $ 5�   &   	  5�   .   % 6B   �� *月2+5�   '     5�   &   $& RightX�60   ' 5�   &   
  5�   *   $( 	999999999~�6=   $) 
-999999999~�6R   $* NotShow=�6h   $+ ###,###,##0,,,-, 6�   $ 5�   &   	  5�   .   % 6B   �� *月3+5�   '     5�   &   $& RightX�60   ' 5�   &   
  5�   *   $( 	999999999~�6=   $) 
-999999999~�6R   $* NotShow=�6h   $+ ###,###,##0,,,-, 6�   $ 5�   &   	  5�   .   % 6B   �� *月4+5�   '     5�   &   $& RightX�60   ' 5�   &   
  5�   *   $( 	999999999~�6=   $) 
-999999999~�6R   $* NotShow=�6h   $+ ###,###,##0,,,-, 6�   $ 5�   &   	  5�   .   % 6B   �� *月5+5�   '     5�   &   $& RightX�60   ' 5�   &   
  5�   *   $( 	999999999~�6=   $) 
-999999999~�6R   $* NotShow=�6h   $+ ###,###,##0,,,-, 6�   $ 5�   &   	  5�   .   % 6B   �� *月6+5�   '     5�   &   $& RightX�60   ' 5�   &   
  5�   *   $( 	999999999~�6=   $) 
-999999999~�6R   $* NotShow=�6h   $+ ###,###,##0,,,-, 6�   $ 5�   &   	  5�   .   % 6B   �� *月7+5�   '     5�   &   $& RightX�60   ' 5�   &   
  5�   *   $( 	999999999~�6=   $) 
-999999999~�6R   $* NotShow=�6h   $+ ###,###,##0,,,-, 6�   $ 5�   &   	  5�   .   % 6B   �� *月8+5�   '     5�   &   $& RightX�60   ' 5�   &   
  5�   *   $( 	999999999~�6=   $) 
-999999999~�6R   $* NotShow=�6h   $+ ###,###,##0,,,-, 6�   $ 5�   &   	  5�   .   % 6B   �� *月9+5�   '     5�   &   $& RightX�60   ' 5�   &   
  5�   *   $( 	999999999~�6=   $) 
-999999999~�6R   $* NotShow=�6h   $+ ###,###,##0,,,-, 6�   $ 5�   &   	  5�   .   % 6B   �� *月10+5   '     5  &   $& RightX�60   ' 5  &   
  5  *   $( 	999999999~�6=   $) 
-999999999~�6R   $* NotShow=�6h   $+ ###,###,##0,,,-, 6�   $ 5  &   	  5  .   % 6B   �� *月11+5  '     5	  &   $& RightX�60   ' 5
  &   
  5  *   $( 	999999999~�6=   $) 
-999999999~�6R   $* NotShow=�6h   $+ ###,###,##0,,,-, 6�   $ 5  &   	  5  .   % 6B   �� *月12+5  '   5     5     5     5     5     5     