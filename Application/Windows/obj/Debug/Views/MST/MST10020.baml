   M S B A M L   `   `   `  ����X  TKyoeiSystem.Framework.Windows, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null�[clr-namespace:KyoeiSystem.Framework.Windows.ViewBase;assembly=KyoeiSystem.Framework.Windows&KyoeiSystem.Framework.Windows.ViewBase  5      �[clr-namespace:KyoeiSystem.Framework.Windows.ViewBase;assembly=KyoeiSystem.Framework.Windows&KyoeiSystem.Framework.Windows.ViewBase  5   	   a ]KyoeiSystem.Framework.Windows.Controls, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null�dclr-namespace:KyoeiSystem.Framework.Windows.Controls;assembly=KyoeiSystem.Framework.Windows.Controls&KyoeiSystem.Framework.Windows.Controls 5   	   R NSystem.Data, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089>.clr-namespace:System.Data;assembly=System.DataSystem.Data 5	      # KyoeiSystem.Application.Windows4   .KyoeiSystem.Application.Windows.Views.MST10020   5      bfw[clr-namespace:KyoeiSystem.Framework.Windows.ViewBase;assembly=KyoeiSystem.Framework.Windows  \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      5   	   R	 NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml 	    5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006 	    5   	   @d2http://schemas.microsoft.com/expression/blend/2008 	    5   	   jvm[clr-namespace:KyoeiSystem.Framework.Windows.ViewBase;assembly=KyoeiSystem.Framework.Windows 	    5   	   uctlsdclr-namespace:KyoeiSystem.Framework.Windows.Controls;assembly=KyoeiSystem.Framework.Windows.Controls 	    5   	   ?Data.clr-namespace:System.Data;assembly=System.Data 	    5	      -   5          � AllowsTransparency  .   � Title$. (自社部門マスタ問合せ(MST10020)��5      $	��350��6    $
��1024��60   $	��350��6@   $
��1024��6M    � WindowStartupLocation$ CenterOwner=�6[    � Left$ 0��6   
 � Top$ 0��6�   .��5      � $��5q�6    � RowDefinitions 5   
   �� 5      $
��auto �6   �� 5      $
��auto �6   �� 5      $��* �6   .+�5   
   +  %System.Windows.Controls.Ribbon.Ribbon  $��Stretch=�6   $��2��60   $��Stretch=�6D      ApplicationMenu 5      :  4System.Windows.Controls.Ribbon.RibbonApplicationMenu  5      5  /System.Windows.Controls.Ribbon.RibbonMenuButton   SmallImageSource$ ../Images/Menu.png��6(   .@�5      >  8System.Windows.Controls.Ribbon.RibbonApplicationMenuItem  $��マスタ照会��60   3  -System.Windows.Controls.Ribbon.RibbonMenuItem   ImageSource$% ../Images/15マスタ照会.png��6?     5      $��マスタ入力��60   $% ../Images/16マスタ入力.png��6?   C  =System.Windows.Controls.Ribbon.RibbonApplicationSplitMenuItem  5      $��印刷��65   $ ../Images/23印刷.png��6A   .@�5        $��印刷��64   $ ../Images/23印刷.png��6@     5       $��ページ設定��64   $ ../Images/22リスト.png��6C     5!      $��印刷プレビュー��64   $ ../Images/22リスト.png��6E   5"        5$      $��終了��60   $ ../Images/LogOut.bmp��6<   5%      .@�5)      .  (System.Windows.Controls.Ribbon.RibbonTab  $��	ホーム��6   	   KeyTip$	 h��6%   $��75��60   .@�5*      0  *System.Windows.Controls.Ribbon.RibbonGroup  $��90��6   5,        5.      $��F1��6   
   KeyTip$
 MASTER��6*      RibbonFunctionBackground#��[  6:   .@�5/      1	  +System.Windows.Controls.Ribbon.RibbonButton	  -   5         	  LargeImageSource$% ../Images/15マスタ照会.png��5/   #   $��0,0,0,0q�6L    	  ToolTipTitle$ マスタ照会(F1)��6]    	  ToolTipDescription$3 -入力済みのデータを検索します。��6v    	  ToolTipImageSource$% ../Images/15マスタ照会.png��6�    	  Label$ ﾏｽﾀ照会��50       	  KeyTip$ F1��6B   $��50��6N   51        52      $��F2��6   $
 MASTER��6*   #��[  6:   .@�53      	  -   5        $y�Hidden=�53   #   $% ../Images/16マスタ入力.png��67   $ 入力��6a   $ F2��6�   $ マスタ入力(F2)��54   #   $- 'マスタの入力画面を開きます��6<   $% ../Images/16マスタ入力.png��6`   55        56      $��F3��6   $
 MASTER��6*   #��[  6:   .@�57      	  $y�Hidden=�6#   $  ��68   $  ��6A   $  ��6Q   58        59      $��F4��6   $
 MASTER��6*   #��[  6:   .@�5:      	  $y�Hidden=�6#   $  ��67   $  ��6@   $  ��6P   5;        5<      $��F5��6   $
 MASTER��6*   #��[  6:   .@�5=      	  -   5        $ ../Images/19CSV出力.png��5=   #   $ 	CSV出力��6M   $ F5��6v   $��50��6�   5>        5?      $��F6��6   $
 MASTER��6*   #��[  6:   .@�5@      	  $y�Hidden=�6#   $  ��68   $  ��6A   $  ��6Q   5A        5B      $��F7��6   $
 MASTER��6*   #��[  6:   .@�5C      	  $y�Hidden=�6#   $  ��68   $  ��6A   $  ��6Q   5D        5E      $��F8��6   $
 MASTER��6*   #��[  6:   .@�5F      	  -   5        $ ../Images/23印刷.png��5F   #   $ F8��6d   $��50��6p   $ 印刷��5G      $ 
印刷(F8)��6   $ 印刷します��6,   $ ../Images/23印刷.png��6H   5H        5I      $��F9��6   $
 MASTER��6*   #��[  6:   .@�5J      	  -   5        $y�Hidden=�5J   #   $ ../Images/24登録.png��67   $ F9��6x   $ 登録��5K      $ データ登録(F9)��6   $- '入力データの登録を行います��6/   $ ../Images/24登録.png��6R   5L        5M      $	��F10��6   $
 MASTER��6+   #��[  6;   .@�5N      	  -   5        $y�Hidden=�5N   #   $ ../Images/26クリア.png��67   $	 F10��6y   $ 入力取消��5O      $ 入力取消(F10)��6   $' !入力の取消しを行います��61   $ ../Images/26クリア.png��6R   5P        5Q      $	��F11��6   $
 MASTER��6+   #��[  6;   .@�5R      	  -   5        $ ../Images/27終了.png��5R   #   $	 F11��6d   $��50��6q   $ 終了��5S      $ 終了(F11)��6   $ 終了します��6-   $ ../Images/27終了.png��6H   5T        5U      $	��F12��6   $
 MASTER��6+   #��[  6;   .@�5V      	  -	   5        $y�Hidden=�5V   #   $ ../Images/28削除.png��67   $	 F12��6x   $ 削除��5W      $ 削除(F12)��6   $ 入力を削除します��6-   $ ../Images/28削除.png��6K   5X      5Y        5[      $��	ヘルプ��6   
�������.@�5\        $��サポート��6   .@�5]      	  -
   5        $% ../Images/30マニュアル.png��5]   #   $ MANUAL��6L   $ ﾏﾆｭｱﾙ��5^      $ 取扱説明書��6   $* $取り扱い説明書を開きます��6.   	  5_      -   5        $( "../Images/29遠隔サポート.png��5_   #   $ SUPPORT��6M   $ お問合せ��5`      $ お問合せ��6   $< 6Web上の｢お問合せ｣ページに遷移します��6,   	  5a      -   5        $ ../Images/QandA.png��5a   #   $	 FAQ��6J   $ QandA��5b      $ QandA��6   $E ?Web上の｢よくあるご質問｣ページに遷移します��6.   	  5c      -   5        $( "../Images/13共栄Webリンク.png��5c   #   $ KYOEI��6P   $ 共栄ｼｽﾃﾑ��5d      $ 共栄システムHP��6   $< 6共栄システムのホームページを開きます��62   5e      5f      5g      �� 5j   
   $��0��6   $��Bottom=�6   
��� ���
���  ����5k      �� *ErrorMessage+y�6/   �� *ErrorMessageVisibility+�� 5m   
   $��1��6   ���#2792c3$��1q�64   $��5q�6H   
�������._�5n      � $��5q�6    5o      �� 5p      $��30 �6$   �� 5q      $��3 �6$   �� 5r      $��30 �6$   �� 5s      $��3 �6$   �� 5t      $��30 �6$    � ColumnDefinitions 5v      �� 5w      $
��auto �6'   �� 5x      $��30 �6'   �� 5y      $��* �6'   .+�5{      �  5|      �� 5}      $
��auto �6+   �� 5~      $��* �6+   .+�5�      ?
  9KyoeiSystem.Framework.Windows.Controls.UcLabelTwinTextBox
   
  Label_Context$ 自社部門ID��6.   $��0��6E    
  Label_Width$	 100Q�6R    
  Text1MaxLength$ 5��6d    
  Text2MaxLength$ 5��6w    
  
Label1Text$	 ～��5�   "    
  Label2Visibility$ 	Collapsed=�61    
  Text2Visibility$ 	Collapsed=�6N    
  
Text1Width$	 100Q�6j    
  DataAccessName$ M71_BUM��6{    
  Text1ValidationType$ Integer=�5�   �    
  Text1 6"   ��  �� Path$ 自社部門コードFROM� �� Mode$ TwoWay=� �� NotifyOnSourceUpdated .   �� NotifyOnTargetUpdated  . 6�   
  5�      ! 
  LabelVisibility$! 	Collapsed=�6.   $��1��6J   $	 100Q�6Z   $ 5��6l   $ 5��6   " 
  Label1Visibility$" 	Collapsed=�5�   "   $ 	Collapsed=�6?   $ 	Collapsed=�6\   $	 100Q�6x   $ M71_BUM��6�   $ Integer=�5�   �    6"   �� $ 自社部門コードTO�$ TwoWay=� .   . 6�   5�      ;  5KyoeiSystem.Framework.Windows.Controls.UcLabelTextBox  5�      #   Label_Context$# 自社部門指定��6&   $��2��6=   $   Label_Width$	$ 100Q�6J   $
��Left=�5�   "   %   cWidth$	% 500Q�6?   &   ImeType$	& Off=�6L   '   DataAccessName$' M71_BUM_PICUP��6Z   (   Text( 5�   "   �� $ 自社部門指定�$ TwoWay=� .   . C  =KyoeiSystem.Framework.Windows.Controls.UcLabelTextRadioButton  5�      $��4��6.   )   RadioViewCount$	) Two=�6<   *   Label_Content$* 表示方法��6Q   +   Label_Width$	+ 100Q�6f   ,   RadioOne_Content$, コード順��5�   .   -   RadioSecond_Content$- カナ読み順��6F   .   RadioSelectButton$	. One=�6b   /   Text/ 5�   &   �� $ 表示方法�$ TwoWay=� .   . �� 5�      -   5        $��SearchButton��5�   0   $y�	Collapsed=�6   $��2��6D   $��4��6T   $��50��6e   $��50��6p   $��25q�6|   $��Right=�6�   $��Stretch=�6�   $��0q�6�   .��5�      �� $��../Images/Kensaku.bmp��6   $
��Fill=�6;   5�      5�      5�      7  1KyoeiSystem.Framework.Windows.Controls.UcDataGrid  5�   
   -   5        $��DataGrid��5�   .   $y�Hidden=�6   0   cAutoGenerateColumns0 . %1   cHorizontalScrollBarVisibility$
1 Auto=�6Z   #2   cVerticalScrollBarVisibility$
2 Auto=�6�   $��Stretch=�5�      3   cCanUserResizeColumns3 . 4   cCanUserReorderColumns4 . 5   cCanUserSortColumns5 . 6   cRowBackground
6 �����$��2��5�   &   $��5,0,5,5q�63   7   ItemSources7 5�      �� $ SeachResultData�$ TwoWay=� .   . 5�      5�      