   M S B A M L   `   `   `  ����X  TKyoeiSystem.Framework.Windows, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null�[clr-namespace:KyoeiSystem.Framework.Windows.ViewBase;assembly=KyoeiSystem.Framework.Windows&KyoeiSystem.Framework.Windows.ViewBase  5   	   �[clr-namespace:KyoeiSystem.Framework.Windows.ViewBase;assembly=KyoeiSystem.Framework.Windows&KyoeiSystem.Framework.Windows.ViewBase  5   	   a ]KyoeiSystem.Framework.Windows.Controls, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null�dclr-namespace:KyoeiSystem.Framework.Windows.Controls;assembly=KyoeiSystem.Framework.Windows.Controls&KyoeiSystem.Framework.Windows.Controls 5   	   �dclr-namespace:KyoeiSystem.Framework.Windows.Controls;assembly=KyoeiSystem.Framework.Windows.Controls&KyoeiSystem.Framework.Windows.Controls 5	      O Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem 5
      # KyoeiSystem.Application.Windows4   .KyoeiSystem.Application.Windows.Views.SRY04010   5   	   bfw[clr-namespace:KyoeiSystem.Framework.Windows.ViewBase;assembly=KyoeiSystem.Framework.Windows  \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      5   	   R	 NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml 	    5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006 	    5   	   @d2http://schemas.microsoft.com/expression/blend/2008 	    5   	   jvm[clr-namespace:KyoeiSystem.Framework.Windows.ViewBase;assembly=KyoeiSystem.Framework.Windows 	    5   	   uctlsdclr-namespace:KyoeiSystem.Framework.Windows.Controls;assembly=KyoeiSystem.Framework.Windows.Controls 	    5   	   yControlsdclr-namespace:KyoeiSystem.Framework.Windows.Controls;assembly=KyoeiSystem.Framework.Windows.Controls 	    5	      9System&clr-namespace:System;assembly=mscorlib 	    5
      -   5        $��
MainWindow��5   .     � WindowStyle$  SingleBorderWindow=�5       � AllowsTransparency .   � Title$" 車種別日計表(SRY04010)��5      $	��728��6    $
��1024��60   $	��768��6@   $
��1024��6M    � WindowStartupLocation$ CenterOwner=�6[    � Left$ 0��6   
 � Top$ 0��6�   .��5      (� $
��auto��6   $
��auto��6    (� Stretch$
 Fill=�6)    (� StretchDirection$
 Both=�68   .��5   
   �  � ColumnDefinitions 5      �� 5      $
��1024 �6#   	 � RowDefinitions	 5      �� 5      $
��auto �6    �� 5      $	��600 �6    .+�5   
   +  %System.Windows.Controls.Ribbon.Ribbon  $��Stretch=�6   $��2��60   $��Stretch=�6D   
   ApplicationMenu
 5      :  4System.Windows.Controls.Ribbon.RibbonApplicationMenu  5      5  /System.Windows.Controls.Ribbon.RibbonMenuButton   SmallImageSource$ ../Images/Menu.png��6(   .@�5      >  8System.Windows.Controls.Ribbon.RibbonApplicationMenuItem  $��マスタ照会��60   3  -System.Windows.Controls.Ribbon.RibbonMenuItem   ImageSource$% ../Images/15マスタ照会.png��6?     5      $��マスタ入力��60   $% ../Images/16マスタ入力.png��6?   C  =System.Windows.Controls.Ribbon.RibbonApplicationSplitMenuItem  5       $��印刷��65   $ ../Images/23印刷.png��6A   .@�5!        $��印刷��64   $ ../Images/23印刷.png��6@     5"      $��ページ設定��64   $ ../Images/22リスト.png��6C     5#      $��印刷プレビュー��64   $ ../Images/22リスト.png��6E   5$        5&      $��終了��60   $ ../Images/LogOut.bmp��6<   5'      .@�5+      .  (System.Windows.Controls.Ribbon.RibbonTab  $��	ホーム��6      KeyTip$ h��6)   $��75��64   .@�5,      0  *System.Windows.Controls.Ribbon.RibbonGroup  $��90��6"   5.        50      $��F1��6"      KeyTip$ MASTER��6.      RibbonFunctionBackground#��[  6>   .@�51      1	  +System.Windows.Controls.Ribbon.RibbonButton	  -   5         	  LargeImageSource$% ../Images/15マスタ照会.png��51   '   $��0,0,0,0q�6P    	  ToolTipTitle$ マスタ照会(F1)��6a    	  ToolTipDescription$3 -入力済みのデータを検索します。��6z    	  ToolTipImageSource$% ../Images/15マスタ照会.png��6�    	  Label$ ﾏｽﾀ照会��52       	  KeyTip$ F1��6B   $��50��6N   53        54      $��F2��6"   $ MASTER��6.   #��[  6>   .@�55      	  -   5        $y�Hidden=�55   '   $% ../Images/16マスタ入力.png��6;   $ 入力��6e   $ F2��6�   $ マスタ入力(F2)��56   #   $- 'マスタの入力画面を開きます��6<   $% ../Images/16マスタ入力.png��6`   57        58      $��F3��6"   $ MASTER��6.   #��[  6>   .@�59      	  $y�Hidden=�6'   $  ��6<   $  ��6E   $  ��6U   5:        5;      $��F4��6"   $ MASTER��6.   #��[  6>   .@�5<      	  $y�Hidden=�6'   $  ��6;   $  ��6D   $  ��6T   5=        5>      $��F5��6"   $ MASTER��6.   #��[  6>   .@�5?      	  -   5        $ ../Images/19CSV出力.png��5?   '   $ 	CSV出力��6Q   $ F5��6z   $��50��6�   5@        5A      $��F6��6"   $ MASTER��6.   #��[  6>   .@�5B      	  $y�Hidden=�6'   $  ��6<   $  ��6E   $  ��6U   5C        5D      $��F7��6"   $ MASTER��6.   #��[  6>   .@�5E      	  $y�Hidden=�6'   $  ��6<   $  ��6E   $  ��6U   5F        5G      $��F8��6"   $ MASTER��6.   #��[  6>   .@�5H      	  -   5        $ ../Images/23印刷.png��5H   '   $ F8��6h   $��50��6t   $ 印刷��5I      $ 
印刷(F8)��6   $ 印刷します��6,   $ ../Images/23印刷.png��6H   5J        5K      $��F9��6"   $ MASTER��6.   #��[  6>   .@�5L      	  -   5        $y�Hidden=�5L   '   $ ../Images/24登録.png��6;   $ F9��6|   $ 登録��5M      $ データ登録(F9)��6   $- '入力データの登録を行います��6/   $ ../Images/24登録.png��6R   5N        5O      $	��F10��6"   $ MASTER��6/   #��[  6?   .@�5P      	  -   5        $y�Hidden=�5P   '   $ ../Images/26クリア.png��6;   $	 F10��6}   $ 入力取消��5Q      $ 入力取消(F10)��6   $' !入力の取消しを行います��61   $ ../Images/26クリア.png��6R   5R        5S      $	��F11��6"   $ MASTER��6/   #��[  6?   .@�5T      	  -   5        $ ../Images/27終了.png��5T   '   $	 F11��6h   $��50��6u   $ 終了��5U      $ 終了(F11)��6   $ 終了します��6-   $ ../Images/27終了.png��6H   5V        5W      $	��F12��6"   $ MASTER��6/   #��[  6?   .@�5X      	  -	   5        $y�Hidden=�5X   '   $ ../Images/28削除.png��6;   $	 F12��6|   $ 削除��5Y      $ 削除(F12)��6   $ 入力を削除します��6-   $ ../Images/28削除.png��6K   5Z      5[        5\      $��	ヘルプ��6   
�������.@�5]        $��サポート��6   .@�5^      	  -
   5        $% ../Images/30マニュアル.png��5^   '   $ MANUAL��6P   $ ﾏﾆｭｱﾙ��5_      $ 取扱説明書��6   $* $取り扱い説明書を開きます��6.   	  5`      -   5        $( "../Images/29遠隔サポート.png��5`   #   $ SUPPORT��6M   $ お問合せ��5a      $ お問合せ��6   $< 6Web上の｢お問合せ｣ページに遷移します��6,   	  5b      -   5        $ ../Images/QandA.png��5b   #   $	 FAQ��6J   $ QandA��5c      $ QandA��6   $E ?Web上の｢よくあるご質問｣ページに遷移します��6.   	  5d      -   5        $( "../Images/13共栄Webリンク.png��5d   #   $ KYOEI��6P   $ 共栄ｼｽﾃﾑ��5e      $ 共栄システムHP��6   $< 6共栄システムのホームページを開きます��62   5f      5g      5h      �� 5k   
   $��0��6   $��Bottom=�6   
��� ���
���  ����5l      �� *ErrorMessage+y�6/   �� *ErrorMessageVisibility+�� 5o   
   $��1��6   $��5q�6   
���   �$��1q�6=   
�������._�5p      � $��5q�6   	 5q      �� 5s      $
��auto �6$   �� 5t      $��3 �6$   �� 5u      $��30 �6$   �� 5v      $��3 �6$   �� 5w      $��30 �6$   �� 5x      $��3 �6$   �� 5y      $��30 �6$   �� 5z      $��3 �6$   �� 5{      $��30 �6$   .+�5~      � $��0��6    5      �� 5�      $
��auto �6+   �� 5�      $��* �6+   �� 5�      $��* �6+   	 5�      �� 5�      $��30 �6(   �� 5�      $��30 �6(   .+�5�      4
  .KyoeiSystem.Framework.Windows.Controls.UcLabel
   
  	LabelText$ 車種指定��6#    
  cWidth$	 100Q�64   $	��5,0q�6B   ?  9KyoeiSystem.Framework.Windows.Controls.UcLabelTwinTextBox  5�         Label_Context$ 範囲指定��6.      Label_Width$ 98Q�6C      
Label1Text$	 ～��6T   $��1��6c      
Text1Width$	 150Q�6s      DataAccessName$ M06_SYA��5�   .      MasterCheckEnabled .     Text1MaxLength$ 5��6b      Label2Visibility$ 	Collapsed=�5�   .      Text2Visibility$ 	Collapsed=�6K       Text1ValidationType$  Integer=�6g   !   Text1! 5�   .   �� *
車種From+" �� Mode$" TwoWay=�# �� NotifyOnSourceUpdated# . $ �� NotifyOnTargetUpdated$ .   5�      $��1��6.   $	 150Q�6>   $��Right=�6O   $ M06_SYA��5�   .    .  $��0,0,15,0q�6b   $ 5��6t   $  Integer=�6�   %   LabelVisibility$% 	Collapsed=�5�   .   &   Label1Visibility$& 	Collapsed=�6J   $ 	Collapsed=�6g   $ 	Collapsed=�6�   ! 5�   .   �� *
車種To+$" TwoWay=�# . $ . ;  5KyoeiSystem.Framework.Windows.Controls.UcLabelTextBox  5�      '   Label_Context$$' ﾋﾟｯｸｱｯﾌﾟ指定��6*   (   DataAccessName$( M06_SYA_PICUP��6F   $��1��5�   �   $��1��6�   )   Label_Width$	) 100Q�6�   *   Text* 6*   �� *車種ピックアップ+$" TwoWay=�# . $ . 6�   5�      � 5�      $��2��6   $��2��6%   $��5,0,0,0q�69    5�      �� 5�      $	��75* �6+   �� 5�      $
��339* �6+   .+�5�        -   5        $��Simebi��5�   *   $' 作成締日��68   $	) 100Q�6M   $��0��6_   $��0��6o   +   ValidationType$+ Integer=�5�   *   ,   
cMaxLength$, 2��6C   -   MinValue$- 1��6R   .   MaxValue$. 31��6_   * 5�   *   �� *作成締日+$" TwoWay=�# . $ . 5�   _   
  5�      $��2��6#   $��0��63   / 
  cBackground
/ ���� 0 
  cForeground
0 �   �1 
  cContent$1 末日は31��6n   "2 
  cHorizontalContentAlignment$
2 Left=�6   5�      � 5�      $��4��6    5�      �� 5�      $
��auto �6/   �� 5�      $
��auto �6/   �� 5�      $
��auto �6/   �� 5�      $
��auto �6/   .+�5�        -   5        $��
SakuseiNen��5�   &   $' 作成年月��68   $	) 100Q�6M   $��0��6_   $��4��6o   3   cWidth$3 80Q�6|   $
��Left=�6�   $��5,0,0,0q�6�   $+ Integer=�5�   &   $, 4��6?   $
. 9999��6N   $- 1��6^   * 5�   &   �� *	作成年+$" TwoWay=�# . $ . 5�   l   
  5�      $��1��6#   $	��年��63   $��Right=�6?   $��15��6[   $��10,7,0,0q�6f   $��4��6x     5�      -   5        $��Sakuseigetu��5�   &   4   Label_Visibility$4 	Collapsed=�69   $��2��6V   $��4��6f   $3 75Q�6s   $
��Left=�6   $��10,0,0,0q�6�   $+ Integer=�5�   &   $, 2��6?   $- 1��6N   $. 12��6[   * 5�   &   �� *	作成月+$" TwoWay=�# . $ . 5�   i   
  5�      $��3��6#   $	��月��63   $��Right=�6?   $��15��6[   $��10,9,0,1q�6f   $��4��6x   5�      � 5�      $��6��6   $��5,0,0,0q�6(    5�      �� 5�      $
��auto �6/   �� 5�      $
��auto �6/   �� 5�      $
��auto �6/   .+�5�        $��0��6.   $��6��6>   $' 集計期間��6K   5   Mask$5 
yyyy/MM/dd��6`   $	) 100Q�6r   $	3 150Q�6�   $
��Left=�6�   $
+ Date=�5�   &   * 5�   &   �� *集計期間From+$" TwoWay=�# . 6 �� StringFormat$6 
yyyy/MM/dd��$ . 7 �� UpdateSourceTrigger$7 	LostFocus=�5�   &   
  5�      $��1��6'   $	��～��67   $��Right=�6C   $��15��6_   $��10,7,0,0q�6j   $��6��6|     5�      -   5        $��2��5�   .   $��6��6>   $5 
yyyy/MM/dd��6K   $4 	Collapsed=�6]   $	' ～��6z   $	3 150Q�6�   $��10,0,0,0q�6�   $
+ Date=�5�   &   * 5�   &   �� *集計期間To+$" TwoWay=�# . $6 
yyyy/MM/dd��$ . $7 	LostFocus=�5�   <   5�      5�      5�      5�      5�      5�      