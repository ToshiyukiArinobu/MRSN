   M S B A M L   `   `   `  ����X  TKyoeiSystem.Framework.Windows, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null�[clr-namespace:KyoeiSystem.Framework.Windows.ViewBase;assembly=KyoeiSystem.Framework.Windows&KyoeiSystem.Framework.Windows.ViewBase  5   	   �[clr-namespace:KyoeiSystem.Framework.Windows.ViewBase;assembly=KyoeiSystem.Framework.Windows&KyoeiSystem.Framework.Windows.ViewBase  5   	   a ]KyoeiSystem.Framework.Windows.Controls, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null�dclr-namespace:KyoeiSystem.Framework.Windows.Controls;assembly=KyoeiSystem.Framework.Windows.Controls&KyoeiSystem.Framework.Windows.Controls 5   	   # KyoeiSystem.Application.Windows4   .KyoeiSystem.Application.Windows.Views.TKS01010   5   	   bfw[clr-namespace:KyoeiSystem.Framework.Windows.ViewBase;assembly=KyoeiSystem.Framework.Windows  \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   jvm[clr-namespace:KyoeiSystem.Framework.Windows.ViewBase;assembly=KyoeiSystem.Framework.Windows     5   	   uctlsdclr-namespace:KyoeiSystem.Framework.Windows.Controls;assembly=KyoeiSystem.Framework.Windows.Controls     5   	   e	 aGrapeCity.WPF.SpreadGrid, Version=1.0.2015.1109, Culture=neutral, PublicKeyToken=5c5c8ff7a6858ccc=sg4http://schemas.grapecity.com/windows/spreadgrid/2012 	 5	   	   -   5        $��Window��5   .     � WindowStyle$  SingleBorderWindow=�5       � AllowsTransparency .   � Title$ 請求書発行(TKS01010)��5      $	��728��6   $
��1024��6+   $	��768��6;   $
��1024��6H    � WindowStartupLocation$ CenterOwner=�6V    � Left$ 0��6z   
 � Top$ 0��6�    � Language$ ja-JP�5       � 	Resources 5      <  6KyoeiSystem.Framework.Windows.Controls.BooleanInverter   boolInverter��%   &	          5      6   5      .��5      � $��5q�6	    � RowDefinitions 5      �� 5      $
��auto �6   �� 5      $
��auto �6   �� 5      $��27 �6   �� 5      $��27 �6   �� 5      $��27 �6   �� 5      $��27 �6   �� 5      $��27 �6   �� 5      $��27 �6   �� 5      $
��auto �6   �� 5       $
��auto �6   �� 5!      $��* �6   	 � ColumnDefinitions	 5$      �� 5%      $
��180* �6   �� 5&      $
��339* �6   .+�5*      �� $��0��6   $��1��6   $��2��6*   $��9��6>   ���#2792c3$��1q�6e   5,      +  %System.Windows.Controls.Ribbon.Ribbon  5/      $��Stretch=�6   $��2��6,   $��Stretch=�6@   .@�51      .  (System.Windows.Controls.Ribbon.RibbonTab  $��	ホーム��6   
   KeyTip$
 h��6   $��75��6*   .@�52   
   0  *System.Windows.Controls.Ribbon.RibbonGroup  $��90��6   54        56   
   $��F1��6      KeyTip$ MASTER��6"     RibbonFunctionBackground#��[ 62   .@�57      1  +System.Windows.Controls.Ribbon.RibbonButton  -   5           LargeImageSource$% ../Images/15マスタ照会.png��57   #      ToolTipTitle$ マスタ照会(F1)��6L      ToolTipDescription$3 -入力済みのデータを検索します。��6e      ToolTipImageSource$% ../Images/15マスタ照会.png��6�      Label$ ﾏｽﾀ照会��58         KeyTip$ F1��6B   $��50��6N   59        5:   
   $��F2��6   $ MASTER��6"   #��[ 62   .@�5;        $y�Hidden=�6   $  ��6-   $  ��66   $  ��6F   5<        5=   
   $��F3��6   $ MASTER��6"   #��[ 62   .@�5>        $y�Hidden=�6   $  ��6-   $  ��66   $  ��6F   5?        5@   
   $��F4��6   $ MASTER��6"   #��[ 62   .@�5A        $y�Hidden=�6   $  ��6-   $  ��66   $  ��6F   5B        5C   
   $��F5��6   $ MASTER��6"   #��[ 62   .@�5D        $y�Hidden=�6   $  ��6-   $  ��66   $  ��6F   5E        5F   
   $��F6��6   $ MASTER��6"   #��[ 62   .@�5G        $y�Hidden=�6   $  ��6-   $  ��66   $  ��6F   5H        5I   
   $��F7��6   $ MASTER��6"   #��[ 62   .@�5J        $y�Hidden=�6   $  ��6-   $  ��66   $  ��6F   5K        5L   
   $��F8��6   $ MASTER��6"   #��[ 62   .@�5M        -   5        $ ../Images/23印刷.png��5M      $ F8��6Z   $��50��6f   $ 印刷��5N      $ 
印刷(F8)��6   $ 印刷を開始します��6,   $ ../Images/23印刷.png��6J   5O        5P   
   $��F9��6   $ MASTER��6"   #��[ 62   .@�5Q        $y�Hidden=�6   $  ��6-   $  ��66   $  ��6F   5R        5S   
   $	��F10��6   $ MASTER��6#   #��[ 63   .@�5T        $y�Hidden=�6   $  ��6-   $  ��66   $  ��6F   5U        5V   
   $	��F11��6   $ MASTER��6#   #��[ 63   .@�5W        -   5        $ ../Images/27終了.png��5W      $	 F11��6Z   $��50��6g   $ 終了��5X      $ 終了(F11)��6   $ 終了します��6-   $ ../Images/27終了.png��6H   5Y        5Z   
   $	��F12��6   $ MASTER��6#   #��[ 63   .@�5[        $y�Hidden=�6   $  ��6-   $  ��66   $  ��6F   5\      5^   	     5`      $��	ヘルプ��6   
�������.@�5a   
     $��サポート��6   .@�5b        -   5        $% ../Images/30マニュアル.png��5b   #   $ MANUAL��6L   $ ﾏﾆｭｱﾙ��5c      $ 取扱説明書��6   $* $取り扱い説明書を開きます��6.     5d      -   5        $( "../Images/29遠隔サポート.png��5d      $ SUPPORT��6C   $ お問合せ��5e      $ お問合せ��6   $< 6Web上の｢お問合せ｣ページに遷移します��6,     5f      -   5        $ ../Images/QandA.png��5f      $	 FAQ��6@   $ QandA��5g      $ QandA��6   $E ?Web上の｢よくあるご質問｣ページに遷移します��6.     5h      -   5        $( "../Images/13共栄Webリンク.png��5h      $ KYOEI��6F   $ 共栄ｼｽﾃﾑ��5i      $ 共栄システムHP��6   $< 6共栄システムのホームページを開きます��62   5j      5k   	   5l      � 5n      $��0��6   $��2��6   $��Bottom=�6,    5o      �� 5p   
   $��74 �6   	 5r      �� 5s   
   $��90 �6   �� 5t   
   $��* �6   .+�5w      ]� $��0��6   $��0��6   $��Stretch=�6/   $��2q�6M   ��5x      �� *MaintenanceModeBackground+.+�5y   
   �� $��メイリオ)�5z      $��24(�6"   $��Medium"�60   $��Stretch=�5{      $��Center=�6.   $��Center=�6R   $��Center=�6m   ��5y      �� *MaintenanceMode+��64   �� *MaintenanceModeForeground+5{   m   5|   	   �� 5      $��0��6   $��1��6   $��Bottom=�6+   
��� ���
���  ��$��2,0,2,0q�6k   ��5�      �� *ErrorMessage+y�6/   �� *ErrorMessageVisibility+5�      � 5�      $��0��6   $��1��6   $��2��6(   $��5,5,0,0q�6<   	 5�      �� 5�   
   $	��150 �6   �� 5�   
   $��3* �6   �� 5�   
   $��2* �6    5�      �� 5�   
   $��27 �6   �� 5�   
   $��27 �6   .+�5�      4  .KyoeiSystem.Framework.Windows.Controls.UcLabel  $��0��6   $��0��6%      cContent$ 得意先指定��62   $��1q�6C   ;  5KyoeiSystem.Framework.Windows.Controls.UcLabelTextBox  5�      -	   5        $��TokuisakiPickup��5�         Label_Context$$ ﾋﾟｯｸｱｯﾌﾟ指定��63   $
��Left=�6N   $	��480��6i      Label_Width$	 100Q�6u   $��1��6�   $��0��6�   $��2��6�      DataAccessName$ M01_TOK_PICKUP��5�         Text 5�      �� *得意先ピックアップ+ �� Mode$ TwoWay=� �� NotifyOnSourceUpdated .  �� NotifyOnTargetUpdated .  �� UpdateSourceTrigger$ PropertyChanged=�5�      � 5�      $��1��6   $��1��6   	 5�   
   �� 5�      $
��auto �6   �� 5�      $
��auto �6   �� 5�      $
��auto �6   �� 5�      $
��auto �6   �� 5�      $��* �6   .+�5�   
     $��0��6   $ 範囲指定��6'      cWidth$	 100Q�67   $��0,1,1,1q�6D   6  0KyoeiSystem.Framework.Windows.Controls.UcTextBox  5�   
   -
   5        $��ID_From��5�      $��1��6(   $��1q�68   $	��100��6C      DataAccessName$ M01_TOK��5�         ValidationType$ Number=�62      
cMaxLength$ 8��6J      Text 5�      �� *得意先範囲指定From+$ TwoWay=� .  . $ PropertyChanged=�5�      �� 5�   
   $��2��6   $	��～��6      5�   
   -   5        $��ID_To��5�      $��3��6&   $��1q�66   $	��100��6A   $ M01_TOK��5�      $ Number=�62   $ 8��6J    5�      �� *得意先範囲指定To+$ TwoWay=� .  . $ PropertyChanged=�5�      5�   	   �� 5�      -   5        $��
btnKensaku��5�      $��2��6'   $��2��67   $��50��6H   $��50��6S   $
��Left=�6_   $��Stretch=�6z   $��0q�6�   .��5�      �� $)��#../Images/検索実行ボタン.png��6   $
��Fill=�67   5�      5�      � 5�      $��2��6   $��2��6   $��5,0,0,0q�6-   	 5�      �� 5�   
   $
��auto �6   �� 5�   
   $��* �6   .+�5�        -   5        $��Simebi��5�      $ 作成締日��6*       MaxValue$  31��6?   !   MinValue$! 1��6M   $	 150Q�6Z   $��0��6l   $��0��6|   $	��190��6�   $
��Left=�6�   "   ValidationType$" Integer=�5�      #   
cMaxLength$# 2��65   $   
IsRequired$ .  5�      �� *作成締日+$ TwoWay=� .  . $ PropertyChanged=��� 5�      $��2��6   $��0��6   $��末日は31��6+   $
��Left=�6;   5�        5�      -   5        $��SakuseiNengetu��5�      $ 作成年月��60   $	 150Q�6E   $��0��6W   $��3��6g   $��5,1,70,1q�6t   $" 
DateYYYYMM=�5�      $# 7��66   %   Mask$% yyyy/MM��6E    5�      �� *作成年月+& �� StringFormat$& yyyy/MM��$ TwoWay=� .  . $ PropertyChanged=�5�      � 5�      $��4��6   $��2��6   .+�5�      <	  6KyoeiSystem.Framework.Windows.Controls.UcLabelComboBox	  -   5        ' 	 Name$' 	cmb回数��5�      ( 	  Label_Width$	( 150Q�6,   ) 	  Label_Context$) 回数��6>   * 	  Combo_SelectedIndex$* 0��6Q   $	��250��6i   $��5,1,10,1q�6u   + 	  ComboListingParams$&+  得意先,請求書発行,回数��5�   +   , 	  SelectedValue, 5�   +   �� *回数+$ TwoWay=� .  . $ 	LostFocus=�5�   +   �� 5�      $@��:得意先を1件指定した時のみ選択できます。��6   $
��Left=�6-   5�      �� 5�      - �� Orientation$- 
Horizontal=�6   $��5��6*   .+�5�        .   	LabelText$. 請求対象期間��6   $	 150Q�6(   $��5,1,1,1q�65     5�      $��80��6   $
 Date=�6"   $ 10��68   /   Mask$/ 
yyyy/MM/dd��6H   0   
IsRequired0 .  5�      �� *請求対象期間From+$& 
yyyy/MM/dd��$ TwoWay=� .  . $ 	LostFocus=��� 5�      $	��～��6   $��Center=�6     5�      $��80��6   $
 Date=�6"   $ 10��68   $/ 
yyyy/MM/dd��6H   0 .  5�      �� *請求対象期間To+$& 
yyyy/MM/dd��$ TwoWay=� .  . $ 	LostFocus=�5�      � 5�      $��5,0,0,0q�6   $��2��6   	 5�      �� 5�   
   $��14000 �6   �� 5�   
   $
��auto �6   �� 5�   
   $��24 �6   �� 5�   
   $
��auto �6   5�        5�      -   5        $��SyuturyokuHiduke��5�      $ 出力日付��62   $	 150Q�6G   $��0��6Z   $��6��6j   $��5,0,0,0q�6w   $	��232��6�   $
��Left=�6�   $
" Date=�5�      $# 10��60   $% 
yyyy/MM/dd��6@   $ .  5�      �� *出力日付+$& 
yyyy/MM/dd��$ TwoWay=� .  . $ PropertyChanged=�C
  =KyoeiSystem.Framework.Windows.Controls.UcLabelTextRadioButton
  5�      -   5        1 
  
cGroupName$1 SyuturyokuTaisyou��5�   "   $��7��6A   $��2��6N   2 
  Label_Content$2 出力対象��6b   3 
  Label_Width$	3 150Q�6w   $��5,0,0,0q�6�   4 
  
IsRequired4 . 5 
  RadioOne_Content$5 全て��6�   6 
  RadioViewCount$6 Three=�6�   7 
  RadioSecond_Content$ 7 一般/子/内訳の明細��6�   8 
  RadioThird_Content$8 親/内訳の鑑��6�   9 
  RadioSelectButton$	9 One=�6  : 
  Text: 5�   "   �� *出力対象+$ TwoWay=� .  . $ PropertyChanged=�5�   "   � 5�      $��10��6   $��2��6   $��0,10,0,0q�6-   	 5�      �� 5�   
   $	��150 �6   �� 5�   
   $��* �6   .+�5�      �� .+�5�   
   �� -   5        $��全てチェック��5�      $��0��6"   $��0��62   $��30��6?   $��2,10,2,1q�6K   5�      �� 5�   
   -   5        $��全てチェック外す��5�      $��0��6$   $��0��64   $��30��6A   $��2,5,2,1q�6N   5�      5�   	   / 	 )GrapeCity.Windows.SpreadGrid.GcSpreadGrid  5      -   5        $��sp請求データ一覧��5     $��1��5      $��0��6(   ;   AutoGenerateColumns; .  <   
LayoutMode$
< Cell=�5  )   =   ColumnDragMode$= SelectThenDrag=�6;   >   DocumentUri$K> E/KyoeiSystem.Application.Windows;component/views/TKS/TKS01010G1.sgxml?�5     ?   RowCount$? 0��6l   @   ItemsSource@ 5     �� *請求書一覧データ+$ TwoWay=� .  . $ PropertyChanged=�A   ColumnsA 5  
   ) 	 #GrapeCity.Windows.SpreadGrid.Column  5     B   CellTypeB 5     3 	 -GrapeCity.Windows.SpreadGrid.CheckBoxCellType  5     C   	DataFieldC 5
     3 	 -GrapeCity.Windows.SpreadGrid.BindingDataField  5     D   BindingD 6$   �� *印刷区分+5       5     E   Locked$
E TrueX�6   C 5       5     D 6$   �� *取引先ID+5       5     $
E TrueX�6   C 5       5     D 6$   �� *取引先名+5       5     $
E TrueX�6   C 5       5     D 6$   �� *	内訳名+5       5     $
E TrueX�6   F   HorizontalAlignment$F RightX�6$   C 5       5     D 6$   �� *当月請求合計+$	& #,0��5!       5"     $
E TrueX�6   C 5#       5$     D 6$   �� *郵便番号+5&       5'     $
E TrueX�6   C 5(       5)     D 6$   �� *	住所１+5+       5,     $
E TrueX�6   C 5-       5.     D 6$   �� *	住所２+50       51     $
E TrueX�6   C 52       53     D 6$   �� *電話番号+55     58  	   �� 59     -   5        $��2��59     $��0��6   $
��Left=�6,   $��Bottom=�6G   $%��表示項目
位置ﾘｾｯﾄ��6b   $��76��6|   |�.  $��56��5:  )   $��2,1,2,6q�65   5;  B   5=     5>     5?     