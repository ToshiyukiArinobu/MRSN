   M S B A M L   `   `   `  ����X  TKyoeiSystem.Framework.Windows, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null�[clr-namespace:KyoeiSystem.Framework.Windows.ViewBase;assembly=KyoeiSystem.Framework.Windows&KyoeiSystem.Framework.Windows.ViewBase  5      �[clr-namespace:KyoeiSystem.Framework.Windows.ViewBase;assembly=KyoeiSystem.Framework.Windows&KyoeiSystem.Framework.Windows.ViewBase  5   	   a ]KyoeiSystem.Framework.Windows.Controls, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null�dclr-namespace:KyoeiSystem.Framework.Windows.Controls;assembly=KyoeiSystem.Framework.Windows.Controls&KyoeiSystem.Framework.Windows.Controls 5   	   R NSystem.Data, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089>.clr-namespace:System.Data;assembly=System.DataSystem.Data 5	      # KyoeiSystem.Application.Windows4   .KyoeiSystem.Application.Windows.Views.MST08020   5      bfw[clr-namespace:KyoeiSystem.Framework.Windows.ViewBase;assembly=KyoeiSystem.Framework.Windows  \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      5   	   R	 NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml 	    5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006 	    5   	   @d2http://schemas.microsoft.com/expression/blend/2008 	    5   	   jvm[clr-namespace:KyoeiSystem.Framework.Windows.ViewBase;assembly=KyoeiSystem.Framework.Windows 	    5   	   uctlsdclr-namespace:KyoeiSystem.Framework.Windows.Controls;assembly=KyoeiSystem.Framework.Windows.Controls 	    5   	   ?Data.clr-namespace:System.Data;assembly=System.Data 	    5	      -   5          � AllowsTransparency  .   � Title$( "摘要マスタ問合せ(MST08020)��5      $	��350��6   $
��1024��6.   $	��350��6>   $
��1024��6K    � WindowStartupLocation$ CenterOwner=�6Y    � Left$ 0��6}   
 � Top$ 0��6�   .��5   
   � $��5q�6    � RowDefinitions 5      �� 5      $
��auto �6    �� 5      $
��auto �6    �� 5      $��* �6    .+�5      +  %System.Windows.Controls.Ribbon.Ribbon  $��Stretch=�6   $��2��64   $��Stretch=�6H      ApplicationMenu 5      :  4System.Windows.Controls.Ribbon.RibbonApplicationMenu  5      5  /System.Windows.Controls.Ribbon.RibbonMenuButton   SmallImageSource$ ../Images/Menu.png��6,   .@�5      >  8System.Windows.Controls.Ribbon.RibbonApplicationMenuItem  $��マスタ照会��64   3  -System.Windows.Controls.Ribbon.RibbonMenuItem   ImageSource$% ../Images/15マスタ照会.png��6C     5      $��マスタ入力��64   $% ../Images/16マスタ入力.png��6C   C  =System.Windows.Controls.Ribbon.RibbonApplicationSplitMenuItem  5       $��印刷��69   $ ../Images/23印刷.png��6E   .@�5!        $��印刷��68   $ ../Images/23印刷.png��6D     5"      $��ページ設定��68   $ ../Images/22リスト.png��6G     5#      $��印刷プレビュー��68   $ ../Images/22リスト.png��6I   5$        5&      $��終了��64   $ ../Images/LogOut.bmp��6@   5'      .@�5+      .  (System.Windows.Controls.Ribbon.RibbonTab  $��	ホーム��6   	   KeyTip$	 h��6%   $��75��60   .@�5,      0  *System.Windows.Controls.Ribbon.RibbonGroup  $��90��6   5.        50      $��F1��6   
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
 MASTER��6+   #��[  6;   .@�5X      	  -	   5        $y�Hidden=�5X   #   $ ../Images/28削除.png��67   $	 F12��6x   $ 削除��5Y      $ 削除(F12)��6   $ 入力を削除します��6-   $ ../Images/28削除.png��6K   5Z      5[        5]      $��	ヘルプ��6   
�������$��0,10,0,8q�67   .@�5^        $��サポート��6"   .@�5_      	  -
   5        $% ../Images/30マニュアル.png��5_   '   $ MANUAL��6P   $ ﾏﾆｭｱﾙ��5`      $ 取扱説明書��6   $* $取り扱い説明書を開きます��6.   	  5a      -   5        $( "../Images/29遠隔サポート.png��5a   '   $ SUPPORT��6Q   $ お問合せ��5b      $ お問合せ��6   $< 6Web上の｢お問合せ｣ページに遷移します��6,   	  5c      -   5        $ ../Images/QandA.png��5c   '   $	 FAQ��6N   $ QandA��5d      $ QandA��6   $E ?Web上の｢よくあるご質問｣ページに遷移します��6.   	  5e      -   5        $( "../Images/13共栄Webリンク.png��5e   '   $ KYOEI��6T   $ 共栄ｼｽﾃﾑ��5f      $ 共栄システムHP��6   $< 6共栄システムのホームページを開きます��62   5g      5h      5i      �� 5l      $��0��6   $��Bottom=�6!   
��� ���
���  ����5m      �� *ErrorMessage+y�6/   �� *ErrorMessageVisibility+�� 5p      $��1��6   ���#2792c3$��1q�68   $��5q�6L   
�������._�5q      � $��5q�6    5r      �� 5s      $��30 �6$   �� 5t      $��3 �6$   �� 5u      $��30 �6$   �� 5v      $��3 �6$   �� 5w      $��30 �6$    � ColumnDefinitions 5z      �� 5{      $
��auto �6+   �� 5|      $��* �6+   .+�5~      ?
  9KyoeiSystem.Framework.Windows.Controls.UcLabelTwinTextBox
   
  Label_Context$ 摘要ID��6.   $��0��6C    
  Label_Width$	 100Q�6P   $��5,0,0,0q�6b    
  Text2Visibility$ 	Collapsed=�6s    
  
Label1Text$	 ～��5   "    
  Label2Visibility$ 	Collapsed=�61    
  MasterCheckEnabled .   
  Text1MaxLength$ 5��6i    
  
Text1Width$	 100Q�5�   "    
  
Text2Width$	 100Q�63    
  DataAccessName$ M11_TEK��6D    
  Text1 5�   "   ��  �� Path$ 摘要コードFROM� �� Mode$ TwoWay=� �� NotifyOnSourceUpdated .   �� NotifyOnTargetUpdated  . 
  5�      ! 
  LabelVisibility$! 	Collapsed=�6.   $��0��6J   $	 100Q�6W    .  $ 5��6�   " 
  Label1Visibility$" 	Collapsed=�5�   !   $ 	Collapsed=�6>   $ 	Collapsed=�6[   $	 100Q�6w   $ M11_TEK��6�   # 
  Text1ValidationType$# Integer=�5�   �   $��2��6�   $��
233,0,55,0q�6�    6!   �� *摘要コードTO+$ TwoWay=� .   . 6�   ;  5KyoeiSystem.Framework.Windows.Controls.UcLabelTextBox  5�      $   Label_Context$$ 摘要指定��6*   $��2��6?   %   Label_Width$	% 100Q�6M   $
��Left=�5�   "   &   DataAccessName$& M11_TEK��6=   $��5,0,0,0q�6V   '   cWidth$	' 500Q�5�   "   (   Text( 5�   "   �� $ 摘要指定�$ TwoWay=� .   . C  =KyoeiSystem.Framework.Windows.Controls.UcLabelTextRadioButton  5�      )   RadioViewCount$	) Two=�62   *   Label_Content$* 表示方法��6G   +   Label_Width$	+ 100Q�6\   ,   RadioOne_Content$, コード順��6n   -   RadioSecond_Content$- カナ読み順��6�   $��4��6�   .   RadioSelectButton$	. One=�6�   $��5,0,0,0q�6�   /   Text/ 5�   &   �� $ 表示方法�$ TwoWay=� .   . 5�      5�      5�      5�      