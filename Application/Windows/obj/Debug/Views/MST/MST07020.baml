   M S B A M L   `   `   `  ����X  TKyoeiSystem.Framework.Windows, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null�[clr-namespace:KyoeiSystem.Framework.Windows.ViewBase;assembly=KyoeiSystem.Framework.Windows&KyoeiSystem.Framework.Windows.ViewBase  5      �[clr-namespace:KyoeiSystem.Framework.Windows.ViewBase;assembly=KyoeiSystem.Framework.Windows&KyoeiSystem.Framework.Windows.ViewBase  5   	   a ]KyoeiSystem.Framework.Windows.Controls, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null�dclr-namespace:KyoeiSystem.Framework.Windows.Controls;assembly=KyoeiSystem.Framework.Windows.Controls&KyoeiSystem.Framework.Windows.Controls 5   	   R NSystem.Data, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089>.clr-namespace:System.Data;assembly=System.DataSystem.Data 5	      # KyoeiSystem.Application.Windows4   .KyoeiSystem.Application.Windows.Views.MST07020   5      bfw[clr-namespace:KyoeiSystem.Framework.Windows.ViewBase;assembly=KyoeiSystem.Framework.Windows  \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      5   	   R	 NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml 	    5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006 	    5   	   @d2http://schemas.microsoft.com/expression/blend/2008 	    5   	   jvm[clr-namespace:KyoeiSystem.Framework.Windows.ViewBase;assembly=KyoeiSystem.Framework.Windows 	    5   	   uctlsdclr-namespace:KyoeiSystem.Framework.Windows.Controls;assembly=KyoeiSystem.Framework.Windows.Controls 	    5   	   ?Data.clr-namespace:System.Data;assembly=System.Data 	    5	      -   5          � AllowsTransparency  .   � Title$( "商品マスタ問合せ(MST07020)��5      $	��350��6   $
��1024��6.   $	��350��6>   $
��1024��6K    � WindowStartupLocation$ CenterOwner=�6Y    � Left$ 0��6}   
 � Top$ 0��6�   .��5      � $��5q�6    � RowDefinitions 5   
   �� 5      $
��auto �6   �� 5      $
��auto �6   �� 5      $��* �6   .+�5   
   +  %System.Windows.Controls.Ribbon.Ribbon  $��Stretch=�6   $��2��60   $��Stretch=�6D      ApplicationMenu 5      :  4System.Windows.Controls.Ribbon.RibbonApplicationMenu  5      5  /System.Windows.Controls.Ribbon.RibbonMenuButton   SmallImageSource$ ../Images/Menu.png��6(   .@�5      >  8System.Windows.Controls.Ribbon.RibbonApplicationMenuItem  $��マスタ照会��60   3  -System.Windows.Controls.Ribbon.RibbonMenuItem   ImageSource$% ../Images/15マスタ照会.png��6?     5      $��マスタ入力��60   $% ../Images/16マスタ入力.png��6?   C  =System.Windows.Controls.Ribbon.RibbonApplicationSplitMenuItem  5       $��印刷��65   $ ../Images/23印刷.png��6A   .@�5!        $��印刷��64   $ ../Images/23印刷.png��6@     5"      $��ページ設定��64   $ ../Images/22リスト.png��6C     5#      $��印刷プレビュー��64   $ ../Images/22リスト.png��6E   5$        5&      $��終了��60   $ ../Images/LogOut.bmp��6<   5'      .@�5+      .  (System.Windows.Controls.Ribbon.RibbonTab  $��	ホーム��6   	   KeyTip$	 h��6%   $��75��60   .@�5,      0  *System.Windows.Controls.Ribbon.RibbonGroup  $��90��6   5.        50      $��F1��6   
   KeyTip$
 MASTER��6*      RibbonFunctionBackground#��[  6:   .@�51      1	  +System.Windows.Controls.Ribbon.RibbonButton	  -   5         	  LargeImageSource$% ../Images/15マスタ照会.png��51   #   $��0,0,0,0q�6L    	  ToolTipTitle$ マスタ照会(F1)��6]    	  ToolTipDescription$3 -入力済みのデータを検索します。��6v    	  ToolTipImageSource$% ../Images/15マスタ照会.png��6�    	  Label$ ﾏｽﾀ照会��52       	  KeyTip$ F1��6B   $��50��6N   53        54      $��F2��6   $
 MASTER��6*   #��[  6:   .@�55      	  -   5        $y�Hidden=�55   #   $% ../Images/16マスタ入力.png��67   $ 入力��6a   $ F2��6�   $ マスタ入力(F2)��56   #   $- 'マスタの入力画面を開きます��6<   $% ../Images/16マスタ入力.png��6`   57        58      $��F3��6   $
 MASTER��6*   #��[  6:   .@�59      	  $y�Hidden=�6#   $  ��68   $  ��6A   $  ��6Q   5:        5;      $��F4��6   $
 MASTER��6*   #��[  6:   .@�5<      	  $y�Hidden=�6#   $  ��67   $  ��6@   $  ��6P   5=        5>      $��F5��6   $
 MASTER��6*   #��[  6:   .@�5?      	  -   5        $ ../Images/19CSV出力.png��5?   #   $ 	CSV出力��6M   $ F5��6v   $��50��6�   5@        5A      $��F6��6   $
 MASTER��6*   #��[  6:   .@�5B      	  $y�Hidden=�6#   $  ��68   $  ��6A   $  ��6Q   5C        5D      $��F7��6   $
 MASTER��6*   #��[  6:   .@�5E      	  $y�Hidden=�6#   $  ��68   $  ��6A   $  ��6Q   5F        5G      $��F8��6   $
 MASTER��6*   #��[  6:   .@�5H      	  -   5        $ ../Images/23印刷.png��5H   #   $ F8��6d   $��50��6p   $ 印刷��5I      $ 
印刷(F8)��6   $ 印刷します��6,   $ ../Images/23印刷.png��6H   5J        5K      $��F9��6   $
 MASTER��6*   #��[  6:   .@�5L      	  -   5        $y�Hidden=�5L   #   $ ../Images/24登録.png��67   $ F9��6x   $ 登録��5M      $ データ登録(F9)��6   $- '入力データの登録を行います��6/   $ ../Images/24登録.png��6R   5N        5O      $	��F10��6   $
 MASTER��6+   #��[  6;   .@�5P      	  -   5        $y�Hidden=�5P   #   $ ../Images/26クリア.png��67   $	 F10��6y   $ 入力取消��5Q      $ 入力取消(F10)��6   $' !入力の取消しを行います��61   $ ../Images/26クリア.png��6R   5R        5S      $	��F11��6   $
 MASTER��6+   #��[  6;   .@�5T      	  -   5        $ ../Images/27終了.png��5T   #   $	 F11��6d   $��50��6q   $ 終了��5U      $ 終了(F11)��6   $ 終了します��6-   $ ../Images/27終了.png��6H   5V        5W      $	��F12��6   $
 MASTER��6+   #��[  6;   .@�5X      	  -	   5        $y�Hidden=�5X   #   $ ../Images/28削除.png��67   $	 F12��6x   $ 削除��5Y      $ 削除(F12)��6   $ 入力を削除します��6-   $ ../Images/28削除.png��6K   5Z      5[        5\      $��	ヘルプ��6   
�������.@�5]        $��サポート��6   .@�5^      	  -
   5        $% ../Images/30マニュアル.png��5^   #   $ MANUAL��6L   $ ﾏﾆｭｱﾙ��5_      $ 取扱説明書��6   $* $取り扱い説明書を開きます��6.   	  5`      -   5        $( "../Images/29遠隔サポート.png��5`   #   $ SUPPORT��6M   $ お問合せ��5a      $ お問合せ��6   $< 6Web上の｢お問合せ｣ページに遷移します��6,   	  5b      -   5        $ ../Images/QandA.png��5b   #   $	 FAQ��6J   $ QandA��5c      $ QandA��6   $E ?Web上の｢よくあるご質問｣ページに遷移します��6.   	  5d      -   5        $( "../Images/13共栄Webリンク.png��5d   #   $ KYOEI��6P   $ 共栄ｼｽﾃﾑ��5e      $ 共栄システムHP��6   $< 6共栄システムのホームページを開きます��62   5f      5g      5j      �� 5m   
   $��0��6   $��Bottom=�6   
��� ���
���  ����5n      �� *ErrorMessage+y�6/   �� *ErrorMessageVisibility+�� 5p   
   $��1��6   ���#2792c3$��1q�64   $��5q�6H   
�������._�5q      � $��5q�6    5r      �� 5s      $��30 �6$   �� 5t      $��3 �6$   �� 5u      $��30 �6$   �� 5v      $��3 �6$   �� 5w      $��30 �6$    � ColumnDefinitions 5y      �� 5z      $
��auto �6'   �� 5{      $��* �6'   .+�5}      � $��0��6    5~      �� 5      $
��auto �6'   �� 5�      $
��auto �6'   �� 5�      $
��auto �6'   �� 5�      $
��auto �6'   �� 5�      $��* �6'   .+�5�      ?
  9KyoeiSystem.Framework.Windows.Controls.UcLabelTwinTextBox
   
  Label_Context$ 商品ID��6.   $��0��6C    
  Label_Width$	 100Q�6P    
  DataAccessName$ M09_HIN��5�   "    
  Text1MaxLength$ 5��6;    
  
Label1Text$	 ～��6O    
  Label2Visibility$ 	Collapsed=�6^    
  Text2Visibility$ 	Collapsed=�6{    
  
Text1Width$	 100Q�6�    
  Text1ValidationType$ Number=�6�    
  Text1 5�   "   �� *商品コードFROM+ �� Mode$ TwoWay=� �� NotifyOnSourceUpdated .  �� NotifyOnTargetUpdated . 
  5�       
  LabelVisibility$ 	Collapsed=�6.   $��1��6J   $	 100Q�6Z   $ M09_HIN��5�   "   $ 5��6;     
  Label1Visibility$  	Collapsed=�6N   $ 	Collapsed=�6k   $ 	Collapsed=�6�   $	 100Q�6�   $ Number=�6�    5�   "   �� *商品コードTO+$ TwoWay=� .  . 5�      ;  5KyoeiSystem.Framework.Windows.Controls.UcLabelTextBox  5�      !   Label_Context$! 商品指定��6&   $��2��6;   "   Label_Width$	" 100Q�6H   $
��Left=�5�   "   #   cWidth$	# 500Q�6=   $   DataAccessName$$ M09_HIN_PICUP��6J   %   Text% 5�   "   �� & �� Path$& 商品指定�$ TwoWay=� .  . C  =KyoeiSystem.Framework.Windows.Controls.UcLabelTextRadioButton  5�      '   RadioViewCount$	' Two=�6.   (   Label_Content$( 表示方法��6C   )   Label_Width$	) 100Q�6X   *   RadioOne_Content$* コード順��6j   +   RadioSecond_Content$+ カナ読み順��6�   $��4��6�   ,   RadioSelectButton$	, One=�6�   -   Text- 5�   &   �� $& 表示方法�$ TwoWay=� .  . 5�      5�      5�      5�      