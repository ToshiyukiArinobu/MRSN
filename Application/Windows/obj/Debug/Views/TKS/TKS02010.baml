   M S B A M L   `   `   `  ����X  TKyoeiSystem.Framework.Windows, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null�[clr-namespace:KyoeiSystem.Framework.Windows.ViewBase;assembly=KyoeiSystem.Framework.Windows&KyoeiSystem.Framework.Windows.ViewBase  5   	   �[clr-namespace:KyoeiSystem.Framework.Windows.ViewBase;assembly=KyoeiSystem.Framework.Windows&KyoeiSystem.Framework.Windows.ViewBase  5   	   a ]KyoeiSystem.Framework.Windows.Controls, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null�dclr-namespace:KyoeiSystem.Framework.Windows.Controls;assembly=KyoeiSystem.Framework.Windows.Controls&KyoeiSystem.Framework.Windows.Controls 5   	   �dclr-namespace:KyoeiSystem.Framework.Windows.Controls;assembly=KyoeiSystem.Framework.Windows.Controls&KyoeiSystem.Framework.Windows.Controls 5	      O Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem 5
      # KyoeiSystem.Application.Windows4   .KyoeiSystem.Application.Windows.Views.TKS02010   5   	   bfw[clr-namespace:KyoeiSystem.Framework.Windows.ViewBase;assembly=KyoeiSystem.Framework.Windows  \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      5   	   R	 NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml 	    5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006 	    5   	   @d2http://schemas.microsoft.com/expression/blend/2008 	    5   	   jvm[clr-namespace:KyoeiSystem.Framework.Windows.ViewBase;assembly=KyoeiSystem.Framework.Windows 	    5   	   uctlsdclr-namespace:KyoeiSystem.Framework.Windows.Controls;assembly=KyoeiSystem.Framework.Windows.Controls 	    5   	   yControlsdclr-namespace:KyoeiSystem.Framework.Windows.Controls;assembly=KyoeiSystem.Framework.Windows.Controls 	    5	      9System&clr-namespace:System;assembly=mscorlib 	    5
      e
 aGrapeCity.WPF.SpreadGrid, Version=1.0.2015.1109, Culture=neutral, PublicKeyToken=5c5c8ff7a6858ccc=sg4http://schemas.grapecity.com/windows/spreadgrid/2012 
 5      -   5        $��
MainWindow��5   .     � WindowStyle$  SingleBorderWindow=�5       � AllowsTransparency .   � Title$% 内訳請求書発行(TKS02010)��5      $	��728��6!   $
��1024��61   $	��768��6A   $
��1024��6N    � WindowStartupLocation$ CenterOwner=�6\    � Left$ 0��6�   
 � Top$ 0��6�   .��5      (� $
��auto��6   $
��auto��6    (� Stretch$
 Fill=�6)    (� StretchDirection$
 Both=�68   .��5   
   �  � ColumnDefinitions 5      �� 5      $
��1024 �6#   	 � RowDefinitions	 5      �� 5      $
��auto �6    �� 5      $	��600 �6    .+�5      +  %System.Windows.Controls.Ribbon.Ribbon  $��Stretch=�6   $��2��64   $��Stretch=�6H   
   ApplicationMenu
 5      :  4System.Windows.Controls.Ribbon.RibbonApplicationMenu  5      5  /System.Windows.Controls.Ribbon.RibbonMenuButton   SmallImageSource$ ../Images/Menu.png��6,   .@�5      >  8System.Windows.Controls.Ribbon.RibbonApplicationMenuItem  $��マスタ照会��64   3  -System.Windows.Controls.Ribbon.RibbonMenuItem   ImageSource$% ../Images/15マスタ照会.png��6C     5       $��マスタ入力��64   $% ../Images/16マスタ入力.png��6C   C  =System.Windows.Controls.Ribbon.RibbonApplicationSplitMenuItem  5!      $��印刷��69   $ ../Images/23印刷.png��6E   .@�5"        $��印刷��68   $ ../Images/23印刷.png��6D     5#      $��ページ設定��68   $ ../Images/22リスト.png��6G     5$      $��印刷プレビュー��68   $ ../Images/22リスト.png��6I   5%        5'      $��終了��64   $ ../Images/LogOut.bmp��6@   5(      .@�5,      .  (System.Windows.Controls.Ribbon.RibbonTab  $��	ホーム��6      KeyTip$ h��6)   $��75��64   .@�5-      0  *System.Windows.Controls.Ribbon.RibbonGroup  $��90��6"   5/        51      $��F1��6"      KeyTip$ MASTER��6.      RibbonFunctionBackground#��[  6>   .@�52      1	  +System.Windows.Controls.Ribbon.RibbonButton	  -   5         	  LargeImageSource$% ../Images/15マスタ照会.png��52   '   $��0,0,0,0q�6P    	  ToolTipTitle$ マスタ照会(F1)��6a    	  ToolTipDescription$3 -入力済みのデータを検索します。��6z    	  ToolTipImageSource$% ../Images/15マスタ照会.png��6�    	  Label$ ﾏｽﾀ照会��53       	  KeyTip$ F1��6B   $��50��6N   54        55      $��F2��6"   $ MASTER��6.   #��[  6>   .@�56      	  -   5        $y�Hidden=�56   '   $% ../Images/16マスタ入力.png��6;   $ 入力��6e   $ F2��6�   $ マスタ入力(F2)��57   #   $- 'マスタの入力画面を開きます��6<   $% ../Images/16マスタ入力.png��6`   58        59      $��F3��6"   $ MASTER��6.   #��[  6>   .@�5:      	  $y�Hidden=�6'   $  ��6<   $  ��6E   $  ��6U   5;        5<      $��F4��6"   $ MASTER��6.   #��[  6>   .@�5=      	  $y�Hidden=�6'   $  ��6;   $  ��6D   $  ��6T   5>        5?      $��F5��6"   $ MASTER��6.   #��[  6>   .@�5@      	  -   5        $ ../Images/19CSV出力.png��5@   '   $ 	CSV出力��6Q   $ F5��6z   $��50��6�   5A        5B      $��F6��6"   $ MASTER��6.   #��[  6>   .@�5C      	  $y�Hidden=�6'   $  ��6<   $  ��6E   $  ��6U   5D        5E      $��F7��6"   $ MASTER��6.   #��[  6>   .@�5F      	  $y�Hidden=�6'   $  ��6<   $  ��6E   $  ��6U   5G        5H      $��F8��6"   $ MASTER��6.   #��[  6>   .@�5I      	  -   5        $ ../Images/23印刷.png��5I   '   $ F8��6h   $��50��6t   $ 印刷��5J      $ 
印刷(F8)��6   $ 印刷します��6,   $ ../Images/23印刷.png��6H   5K        5L      $��F9��6"   $ MASTER��6.   #��[  6>   .@�5M      	  -   5        $y�Hidden=�5M   '   $ ../Images/24登録.png��6;   $ F9��6|   $ 登録��5N      $ データ登録(F9)��6   $- '入力データの登録を行います��6/   $ ../Images/24登録.png��6R   5O        5P      $	��F10��6"   $ MASTER��6/   #��[  6?   .@�5Q      	  -   5        $y�Hidden=�5Q   '   $ ../Images/26クリア.png��6;   $	 F10��6}   $ 入力取消��5R      $ 入力取消(F10)��6   $' !入力の取消しを行います��61   $ ../Images/26クリア.png��6R   5S        5T      $	��F11��6"   $ MASTER��6/   #��[  6?   .@�5U      	  -   5        $ ../Images/27終了.png��5U   '   $	 F11��6h   $��50��6u   $ 終了��5V      $ 終了(F11)��6   $ 終了します��6-   $ ../Images/27終了.png��6H   5W        5X      $	��F12��6"   $ MASTER��6/   #��[  6?   .@�5Y      	  -	   5        $y�Hidden=�5Y   '   $ ../Images/28削除.png��6;   $	 F12��6|   $ 削除��5Z      $ 削除(F12)��6   $ 入力を削除します��6-   $ ../Images/28削除.png��6K   5[      5\        5^      $��	ヘルプ��6   
�������.@�5_        $��サポート��6"   .@�5`      	  -
   5        $% ../Images/30マニュアル.png��5`   '   $ MANUAL��6P   $ ﾏﾆｭｱﾙ��5a      $ 取扱説明書��6   $* $取り扱い説明書を開きます��6.   	  5b      -   5        $( "../Images/29遠隔サポート.png��5b   '   $ SUPPORT��6Q   $ お問合せ��5c      $ お問合せ��6   $< 6Web上の｢お問合せ｣ページに遷移します��6,   	  5d      -   5        $ ../Images/QandA.png��5d   '   $	 FAQ��6N   $ QandA��5e      $ QandA��6   $E ?Web上の｢よくあるご質問｣ページに遷移します��6.   	  5f      -   5        $( "../Images/13共栄Webリンク.png��5f   '   $ KYOEI��6T   $ 共栄ｼｽﾃﾑ��5g      $ 共栄システムHP��6   $< 6共栄システムのホームページを開きます��62   5h      5i      5j      �� 5m      $��0��6   $��Bottom=�6!   
��� ���
���  ����5n      �� *ErrorMessage+y�6/   �� *ErrorMessageVisibility+�� 5q      $��1��6   $��5q�6"   
���   �$��1q�6A   
�������._�5r      � $��5q�6   	 5s      �� 5u      $
��auto �6(   �� 5v      $��3 �6(   �� 5w      $��30 �6(   �� 5x      $��3 �6(   �� 5y      $��30 �6(   �� 5z      $��3 �6(   �� 5{      $��30 �6(   �� 5|      $��3 �6(   �� 5}      $��30 �6(   �� 5~      $��3 �6(   �� 5�      $��* �6(   .+�5�      � $��0��6    5�      �� 5�      $
��auto �6/   �� 5�      $��* �6/   �� 5�      $��* �6/   	 5�      �� 5�      $��30 �6,   �� 5�      $��30 �6,   .+�5�      4
  .KyoeiSystem.Framework.Windows.Controls.UcLabel
   
  	LabelText$ 得意先指定��6'    
  cWidth$	 100Q�69   $	��5,0q�6G   ?  9KyoeiSystem.Framework.Windows.Controls.UcLabelTwinTextBox  5�      -   5        $��LabelTextTokuisaki��5�   2   $��1��6L      Label_Context$ 範囲指定��6\      Label_Width$	 100Q�6q      Label1Visibility$ 	Collapsed=�6�      
Text1Width$ 80Q�5�   "      
Text2Width$	 300Q�62   $
��Left=�6C   =  7KyoeiSystem.Framework.Windows.Controls.FrameworkControl   	IsKeyItem .    Text1MaxLength$ 8��6o      DataAccessName$ M01_TOK��5�   "      MasterCheckEnabled .     Text1  5�   "   �� ! �� Path$! 得意先コード�" �� Mode$" TwoWay=�# �� NotifyOnSourceUpdated# . $ �� NotifyOnTargetUpdated$ . %   Text2% 5�   "   �� $! 得意先名�$" TwoWay=�# . $ . ;  5KyoeiSystem.Framework.Windows.Controls.UcLabelTextBox  5�      &   Label_Context$$& ﾋﾟｯｸｱｯﾌﾟ指定��6.   '   DataAccessName$' TKS02010_PICUP��6I   $��1��5�   �   $��1��6�   (   Label_Width$	( 100Q�6�   )   Text) 6*   �� *得意先ピックアップ+$" TwoWay=�# . $ . 6�   5�      � 5�      $��2��6   $��2��6)   $��5,0,0,0q�6=    5�      �� 5�      $
��199* �6/   �� 5�      $	��76* �6/   �� 5�      $
��722* �6/   .+�5�        -   5        $��Simebi��5�   .   $& 作成締日��6<   $	( 100Q�6Q   *   cWidth$* 80Q�6c   +   
cMaxLength$+ 2��6o   $
��Left=�6~   ,   ValidationType$, Integer=�5�   *   -   Mask$- 00��6C   .   MinValue$. 1��6M   /   MaxValue$/ 31��6Z   ) 5�   *   �� *作成締日+$" TwoWay=�# . $ . 5�   Z   
  5�      $��1��6'   0 
  cBackground
0 ���� 1 
  cForeground
1 �   �2 
  cContent$2 末日は31��6e   $
��Left=�6v   5�      � 5�      $��4��6   $��5,0,0,0q�6(    5�      �� 5�      $
��auto �6/   �� 5�      $
��auto �6/   �� 5�      $
��auto �6/   �� 5�      $
��auto �6/   .+�5�        -   5        $��
SakuseiNen��5�   .   $��0��6@   $& 作成年月��6P   $	( 100Q�6e   $��4��6w   $* 80Q�6�   $
��Left=�6�   $+ 4��5�   &   $, Integer=�6K   $
- 0000��6d   $
/ 9999��6p   $. 1��6�   ) 5�   &   �� *	作成年+$" TwoWay=�# . $ . 5�   �   
  5�      $��1��6'   $	��年��67   $��15��6C   $��5,0,0,0q�6N   $��4��6_   $��Center=�6l     5�      -   5        $��Sakuseigetu��5�   .   $��2��6A   3   Label_Visibility$3 	Collapsed=�6Q   $��4��6n   $* 75Q�6{   $
��Left=�6�   $+ 2��5�   &   $, Integer=�6L   $- 00��6e   $. 1��6o   $/ 12��6|   ) 5�   &   �� *	作成月+$" TwoWay=�# . $ . 5�   |   
  5�      $��3��6'   $	��月��67   $��15��6C   $��5,0,0,0q�6N   $��4��6_   $��Center=�6l   5�      � 5�      $��6��6   $��5,0,0,0q�6(    5�      �� 5�      $
��auto �6/   �� 5�      $
��auto �6/   �� 5�      $
��auto �6/   .+�5�        $��0��6.   $��6��6>   $& 集計期間��6K   $- 
yyyy/MM/dd��6`   $	( 100Q�6r   $	* 150Q�6�   $
��Left=�6�   $
, Date=�5�   &   ) 5�   &   �� *集計期間From+$" TwoWay=�# . 4 �� StringFormat$4 
yyyy/MM/dd��$ . 5 �� UpdateSourceTrigger$5 	LostFocus=�5�   &   
  5�      $��1��6'   $	��～��67   $��15��6C   $��5,0,0,0q�6N   $��Center=�6_   $��6��6z     5�      $��2��6.   $��6��6>   $- 
yyyy/MM/dd��6K   $3 	Collapsed=�6]   $	& ～��6z   $	* 150Q�6�   $
, Date=�5�   &   ) 5�   &   �� *集計期間To+$" TwoWay=�# . $4 
yyyy/MM/dd��$ . $5 	LostFocus=�5�   &   5�        5�      -   5        $��8��5�   .   $ 自社名ID��6;   $	 100Q�6Q   $ 	Collapsed=�6c   $ 80Q�5�   "   $	 200Q�62   $
��Left=�6C   $��5,0,0,0q�6^   $ M70_JIS��5�   "    .   .   5�   "   �� $! 自社ID�$" TwoWay=�# . $ . % 5�   "   �� $! 	自社名�$" TwoWay=�# . $ . � 5�      $��0��6   $��12��6+   $��2��69   5�      5�      5�      5�      5�      