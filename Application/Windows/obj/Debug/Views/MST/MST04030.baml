   M S B A M L   `   `   `  ����X  TKyoeiSystem.Framework.Windows, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null�[clr-namespace:KyoeiSystem.Framework.Windows.ViewBase;assembly=KyoeiSystem.Framework.Windows&KyoeiSystem.Framework.Windows.ViewBase  5      �[clr-namespace:KyoeiSystem.Framework.Windows.ViewBase;assembly=KyoeiSystem.Framework.Windows&KyoeiSystem.Framework.Windows.ViewBase  5   	   a ]KyoeiSystem.Framework.Windows.Controls, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null�dclr-namespace:KyoeiSystem.Framework.Windows.Controls;assembly=KyoeiSystem.Framework.Windows.Controls&KyoeiSystem.Framework.Windows.Controls 5   	   R NSystem.Data, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089>.clr-namespace:System.Data;assembly=System.DataSystem.Data 5	      # KyoeiSystem.Application.Windows4   .KyoeiSystem.Application.Windows.Views.MST04030   5      bfw[clr-namespace:KyoeiSystem.Framework.Windows.ViewBase;assembly=KyoeiSystem.Framework.Windows  \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      5   	   R	 NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml 	    5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006 	    5   	   @d2http://schemas.microsoft.com/expression/blend/2008 	    5   	   jvm[clr-namespace:KyoeiSystem.Framework.Windows.ViewBase;assembly=KyoeiSystem.Framework.Windows 	    5   	   uctlsdclr-namespace:KyoeiSystem.Framework.Windows.Controls;assembly=KyoeiSystem.Framework.Windows.Controls 	    5   	   ?Data.clr-namespace:System.Data;assembly=System.Data 	    5	      -   5          � AllowsTransparency  .   � Title$+ %乗務員マスタ問合せ(MST04030)��5      $	��350��6   $
��1024��6/   $	��350��6?   $
��1024��6L    � Left$ 0��6Y   
 � Top$ 0��6b   .��5      � $��5q�6	    � RowDefinitions 5      �� 5      $
��auto �6   �� 5      $
��auto �6   �� 5      $��* �6   .+�5      +  %System.Windows.Controls.Ribbon.Ribbon  $��Stretch=�6   $��Stretch=�6+   .@�5      .  (System.Windows.Controls.Ribbon.RibbonTab  $��	ホーム��6      KeyTip$ h��6   $��75��6*   .@�5   
   0  *System.Windows.Controls.Ribbon.RibbonGroup  $��90��6   5        5   
   $��F1��6      KeyTip$ MASTER��6"      RibbonFunctionBackground#��[  62   .@�5       1  +System.Windows.Controls.Ribbon.RibbonButton  -   5           LargeImageSource$% ../Images/15マスタ照会.png��5    #      ToolTipTitle$ マスタ照会(F1)��6L   	   ToolTipDescription$3	 -入力済みのデータを検索します。��6e   
   ToolTipImageSource$%
 ../Images/15マスタ照会.png��6�      Label$ ﾏｽﾀ検索��5!         KeyTip$ F1��6B   $��50��6N   5"        5#   
   $��F2��6   $ MASTER��6"   #��[  62   .@�5$        $y�Hidden=�6   $  ��6-   $  ��66   $	  ��6F   5%        5&   
   $��F3��6   $ MASTER��6"   #��[  62   .@�5'        $y�Hidden=�6   $  ��6-   $  ��66   $	  ��6F   5(        5)   
   $��F4��6   $ MASTER��6"   #��[  62   .@�5*        $y�Hidden=�6   $  ��6-   $  ��66   $	  ��6F   5+        5,      $��F5��6   $ MASTER��6*   #��[  6:   .@�5-        -   5        $ ../Images/19CSV出力.png��5-   #   $ 	CSV出力��6M   $ F5��6v   $��50��6�   5.        5/   
   $��F6��6   $ MASTER��6"   #��[  62   .@�50        $y�Hidden=�6   $  ��6-   $  ��66   $	  ��6F   51        52   
   $��F7��6   $ MASTER��6"   #��[  62   .@�53        $y�Hidden=�6   $  ��6-   $  ��66   $	  ��6F   54        55   
   $��F8��6   $ MASTER��6"   #��[  62   .@�56        -   5        $ ../Images/23印刷.png��56      $ F8��6Z   $��50��6f   $ 印刷��57      $ 
印刷(F8)��6   $	 印刷を開始します��6,   $
 ../Images/23印刷.png��6J   58        59   
   $��F9��6   $ MASTER��6"   #��[  62   .@�5:        $y�Hidden=�6   $  ��6-   $  ��66   $	  ��6F   5;        5<   
   $	��F10��6   $ MASTER��6#   #��[  63   .@�5=        $y�Hidden=�6   $  ��6-   $  ��66   $	  ��6F   5>        5?   
   $	��F11��6   $ MASTER��6#   #��[  63   .@�5@        -   5        $ ../Images/27終了.png��5@      $	 F11��6Z   $��50��6g   $ 終了��5A      $ 終了(F11)��6   $	 終了します��6-   $
 ../Images/27終了.png��6H   5B        5C   
   $	��F12��6   $ MASTER��6#   #��[  63   .@�5D        $y�Hidden=�6   $  ��6-   $  ��66   $	  ��6F   5E      5G   	     5I      $��	ヘルプ��6   
�������.@�5J   
     $��サポート��6   .@�5K        -   5        $% ../Images/30マニュアル.png��5K      $ MANUAL��6B   $ ﾏﾆｭｱﾙ��5L      $ 取扱説明書��6   $*	 $取り扱い説明書を開きます��6.     5M      -   5        $( "../Images/29遠隔サポート.png��5M      $ SUPPORT��6C   $ お問合せ��5N      $ お問合せ��6   $<	 6Web上の｢お問合せ｣ページに遷移します��6,     5O      -   5        $ ../Images/QandA.png��5O      $	 FAQ��6@   $ QandA��5P      $ QandA��6   $E	 ?Web上の｢よくあるご質問｣ページに遷移します��6.     5Q      -	   5        $( "../Images/13共栄Webリンク.png��5Q      $ KYOEI��6F   $ 共栄ｼｽﾃﾑ��5R      $ 共栄システムHP��6   $<	 6共栄システムのホームページを開きます��62   5S      5T   	   5U      � 5W      $��0��6   $��Bottom=�6    5X      �� 5Y   
   $��74 �6    � ColumnDefinitions 5[      �� 5\   
   $��90 �6   �� 5]   
   $��* �6   .+�5`      ]� $��0��6   $��0��6   $��Stretch=�6/   $��2q�6M   ��5a      �� *MaintenanceModeBackground+.+�5b   
   �� $��メイリオ)�5c      $��24(�6"   $��Medium"�60   $��Stretch=�5d      $��Center=�6.   $��Center=�6R   $��Center=�6m   ��5b      �� *MaintenanceMode+��64   �� *MaintenanceModeForeground+5d   m   5e   	   �� 5h      $��0��6   $��1��6   $��Bottom=�6+   
��� ���
���  ��$��2,0,2,0q�6k   ��5i      �� *ErrorMessage+y�6/   �� *ErrorMessageVisibility+5k      �� 5n      $��1��6   ���#2792c3$��1q�60   $��5q�6D   
�������._�5o      � $��5q�6    5p   
   �� 5q      $��27 �6   �� 5r      $��27 �6   �� 5s      $��27 �6   �� 5t      $��27 �6   �� 5u      $��27 �6    5w   
   �� 5x      $
��auto �6   �� 5y      $��* �6   .+�5|   
   � $��0��6   $��0��6    5}      �� 5~      $
��auto �6   �� 5      $
��auto �6   �� 5�      $
��auto �6   �� 5�      $
��auto �6   �� 5�      $��* �6   .+�5�      ?  9KyoeiSystem.Framework.Windows.Controls.UcLabelTwinTextBox     Label_Context$ 乗務員ID��6.   $��0��6D      Label_Width$	 100Q�6Q      MasterCheckEnabled .     
Label1Text$	 ～��5�         Label2Visibility$ 	Collapsed=�6(      Text2Visibility$ 	Collapsed=�6E      
Text1Width$	 100Q�6a      DataAccessName$ M04_DRV��6r      Text1MaxLength$ 7��6�      Text1 5�      �� *乗務員範囲指定From+ �� Mode$ TwoWay=� �� NotifyOnSourceUpdated .  �� NotifyOnTargetUpdated .  �� UpdateSourceTrigger$ PropertyChanged=�  5�      $��3��6.   $��0��6>   $	 100Q�6K    .  $ 7��6x      LabelVisibility$ 	Collapsed=�5�         Label1Visibility$ 	Collapsed=�65   $ 	Collapsed=�6R   $ 	Collapsed=�6o   $	 100Q�6�   $ M04_DRV��6�    5�      �� *乗務員範囲指定To+$ TwoWay=� .  . $ PropertyChanged=�5�      � 5�   
   $��0��6   $��1��6    5�      �� 5�      $
��auto �6   �� 5�      $
��auto �6   �� 5�      $
��auto �6   �� 5�      $
��auto �6   �� 5�      $��* �6   .+�5�        $ 自社部門ID��6.   $��0��6E   $	 100Q�6R    .  $	 ～��5�      $ 	Collapsed=�6(   $ 	Collapsed=�6E   $	 100Q�6a   $ M71_BUM��6r   $ 5��6�    5�      �� *自社部門From+$ TwoWay=� .  . $ PropertyChanged=�  5�      $��3��6.   $��0��6>   $	 100Q�6K    .  $ 5��6x   $ 	Collapsed=�5�      $ 	Collapsed=�65   $ 	Collapsed=�6R   $ 	Collapsed=�6o   $	 100Q�6�   $ M71_BUM��6�    5�      �� *自社部門To+$ TwoWay=� .  . $ PropertyChanged=�5�      ;  5KyoeiSystem.Framework.Windows.Controls.UcLabelTextBox  5�         Label_Context$ 乗務員指定��6&   $��2��6<      Label_Width$	 100Q�6I   $
��Left=�5�   "       DataAccessName$  M04_DRV_PICUP��6=   !   cWidth$	! 300Q�5�   "   "   Text" 5�   "   �� # �� Path$# 乗務員指定�$ TwoWay=� .  . C  =KyoeiSystem.Framework.Windows.Controls.UcLabelTextRadioButton  5�      $��3��6.   $   RadioViewCount$
$ Four=�6;   %   Label_Content$% 就労区分��6Q   &   Label_Width$	& 100Q�6f   '   RadioSelectButton$	' One=�6x   (   RadioOne_Content$( 全件表示��5�   &   )   RadioSecond_Content$) 就労者のみ��6>   *   RadioThird_Content$* 休職者のみ��6Z   +   RadioFourth_Content$+ 退職者のみ��6u   ,   Text, 5�   &   �� $# 就労区分�$ TwoWay=� .  .   5�   
   -
   5        $��DisplayMethod��5�   &   $	$ Two=�6;   $% 表示方法��6P   $	& 100Q�6e   $( コード順��6w   $) カナ読み順��6�   $��4��6�   $	' One=�6�   , 5�   &   �� $# 表示方法�$ TwoWay=� .  . 5�   	   5�      5�      5�      