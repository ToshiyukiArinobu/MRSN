   M S B A M L   `   `   `  ����X  TKyoeiSystem.Framework.Windows, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null�[clr-namespace:KyoeiSystem.Framework.Windows.ViewBase;assembly=KyoeiSystem.Framework.Windows&KyoeiSystem.Framework.Windows.ViewBase  5      �[clr-namespace:KyoeiSystem.Framework.Windows.ViewBase;assembly=KyoeiSystem.Framework.Windows&KyoeiSystem.Framework.Windows.ViewBase  5   	   a ]KyoeiSystem.Framework.Windows.Controls, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null�dclr-namespace:KyoeiSystem.Framework.Windows.Controls;assembly=KyoeiSystem.Framework.Windows.Controls&KyoeiSystem.Framework.Windows.Controls 5   	   R NSystem.Data, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089>.clr-namespace:System.Data;assembly=System.DataSystem.Data 5	      # KyoeiSystem.Application.Windows4   .KyoeiSystem.Application.Windows.Views.MST01020   5      bfw[clr-namespace:KyoeiSystem.Framework.Windows.ViewBase;assembly=KyoeiSystem.Framework.Windows  \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      5   	   R	 NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml 	    5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006 	    5   	   @d2http://schemas.microsoft.com/expression/blend/2008 	    5   	   jvm[clr-namespace:KyoeiSystem.Framework.Windows.ViewBase;assembly=KyoeiSystem.Framework.Windows 	    5   	   uctlsdclr-namespace:KyoeiSystem.Framework.Windows.Controls;assembly=KyoeiSystem.Framework.Windows.Controls 	    5   	   ?Data.clr-namespace:System.Data;assembly=System.Data 	    5	      -   5          � AllowsTransparency  .   � Title$+ %取引先マスタ問合せ(MST01020)��5      $	��350��6    $
��1024��60   $	��350��6@   $
��1024��6M    � Left$ 0��6Z   
 � Top$ 0��6c   .��5      � $��5q�6	    � RowDefinitions 5      �� 5      $
��auto �6   �� 5      $
��auto �6   �� 5      $��* �6   .+�5      +  %System.Windows.Controls.Ribbon.Ribbon  $��Stretch=�6   $��Stretch=�6+   .@�5      .  (System.Windows.Controls.Ribbon.RibbonTab  $��	ホーム��6      KeyTip$ h��6   $��75��6*   .@�5   
   0  *System.Windows.Controls.Ribbon.RibbonGroup  $��90��6   5        5   
   $��F1��6      KeyTip$ MASTER��6"      RibbonFunctionBackground#��[  62   .@�5       1  +System.Windows.Controls.Ribbon.RibbonButton  -   5           LargeImageSource$% ../Images/15マスタ照会.png��5          Label$ ﾏｽﾀ照会��5!      	   ToolTipTitle$	 マスタ照会(F1)��6'   
   ToolTipDescription$3
 -入力済みのデータを検索します。��6@      ToolTipImageSource$% ../Images/15マスタ照会.png��6f      KeyTip$ F1��5"   4   $��50��6@   5#        5$   
   $��F2��6   $ MASTER��6"   #��[  62   .@�5%        $y�Hidden=�6   $  ��6-   $	  ��66   $
  ��6F   5&        5'   
   $��F3��6   $ MASTER��6"   #��[  62   .@�5(        $y�Hidden=�6   $  ��6-   $	  ��66   $
  ��6F   5)        5*   
   $��F4��6   $ MASTER��6"   #��[  62   .@�5+        $y�Hidden=�6   $  ��6-   $	  ��66   $
  ��6F   5,        5-   
   $��F5��6   $ MASTER��6"   #��[  62   .@�5.        $y�Hidden=�6   $  ��6-   $	  ��66   $
  ��6F   5/        50   
   $��F6��6   $ MASTER��6"   #��[  62   .@�51        $y�Hidden=�6   $  ��6-   $	  ��66   $
  ��6F   52        53   
   $��F7��6   $ MASTER��6"   #��[  62   .@�54        $y�Hidden=�6   $  ��6-   $	  ��66   $
  ��6F   55        56   
   $��F8��6   $ MASTER��6"   #��[  62   .@�57        -   5        $ ../Images/23印刷.png��57      $ F8��6Z   $��50��6f   $ 印刷��58      $	 
印刷(F8)��6   $
 印刷を開始します��6,   $ ../Images/23印刷.png��6J   59        5:   
   $��F9��6   $ MASTER��6"   #��[  62   .@�5;        $y�Hidden=�6   $  ��6-   $	  ��66   $
  ��6F   5<        5=   
   $	��F10��6   $ MASTER��6#   #��[  63   .@�5>        $y�Hidden=�6   $  ��6-   $	  ��66   $
  ��6F   5?        5@   
   $	��F11��6   $ MASTER��6#   #��[  63   .@�5A        -   5        $ ../Images/27終了.png��5A      $	 F11��6Z   $��50��6g   $ 終了��5B      $	 終了(F11)��6   $
 終了します��6-   $ ../Images/27終了.png��6H   5C        5D   
   $	��F12��6   $ MASTER��6#   #��[  63   .@�5E        $y�Hidden=�6   $  ��6-   $	  ��66   $
  ��6F   5F      5H   	     5J      $��	ヘルプ��6   
�������.@�5K   
     $��サポート��6   .@�5L        -   5        $% ../Images/30マニュアル.png��5L      $ MANUAL��6B   $ ﾏﾆｭｱﾙ��5M      $	 取扱説明書��6   $*
 $取り扱い説明書を開きます��6.     5N      -   5        $( "../Images/29遠隔サポート.png��5N      $ SUPPORT��6C   $ お問合せ��5O      $	 お問合せ��6   $<
 6Web上の｢お問合せ｣ページに遷移します��6,     5P      -   5        $ ../Images/QandA.png��5P      $	 FAQ��6@   $ QandA��5Q      $	 QandA��6   $E
 ?Web上の｢よくあるご質問｣ページに遷移します��6.     5R      -   5        $( "../Images/13共栄Webリンク.png��5R      $ KYOEI��6F   $ 共栄ｼｽﾃﾑ��5S      $	 共栄システムHP��6   $<
 6共栄システムのホームページを開きます��62   5T      5U   	   5V      � 5X      $��0��6   $��Bottom=�6    5Y      �� 5Z   
   $��74 �6    � ColumnDefinitions 5\      �� 5]   
   $��90 �6   �� 5^   
   $��* �6   .+�5a      ]� $��0��6   $��0��6   $��Stretch=�6/   $��2q�6M   ��5b      �� *MaintenanceModeBackground+.+�5c   
   �� $��メイリオ)�5d      $��24(�6"   $��Medium"�60   $��Stretch=�5e      $��Center=�6.   $��Center=�6R   $��Center=�6m   ��5c      �� *MaintenanceMode+��64   �� *MaintenanceModeForeground+5e   m   5f   	   �� 5i      $��0��6   $��1��6   $��Bottom=�6+   
��� ���
���  ��$��2,0,2,0q�6k   ��5j      �� *ErrorMessage+y�6/   �� *ErrorMessageVisibility+5l      �� 5o      $��1��6   ���#2792c3$��1q�60   $��5q�6D   
�������._�5p      � $��5q�6    5q   
   �� 5r      $��27 �6   �� 5s      $��27 �6   �� 5t      $��27 �6   �� 5u      $��27 �6    5w   
   �� 5x      $
��auto �6   �� 5y      $��* �6   .+�5{   
   � $��0��6   $��0��6    5|      �� 5}      $
��auto �6   �� 5~      $
��auto �6   �� 5      $
��auto �6   �� 5�      $
��auto �6   �� 5�      $��* �6   .+�5�      4  .KyoeiSystem.Framework.Windows.Controls.UcLabel  $��0��6      cContent$ 範囲指定��6)      cWidth$	 100Q�69   $��0,1,1,1q�6F   6  0KyoeiSystem.Framework.Windows.Controls.UcTextBox  5�      $��1��6   $��1q�6+   $	��100��66      DataAccessName$ M01_TOK��5�         ValidationType$ Number=�62      
cMaxLength$ 8��6J      Text 5�      �� *得意先範囲指定From+ �� Mode$ TwoWay=� �� NotifyOnSourceUpdated .  �� NotifyOnTargetUpdated .  �� UpdateSourceTrigger$ PropertyChanged=��� 5�      $��2��6   $	��～��6"     5�      $��3��6   $��1q�6+   $	��100��66   $ M01_TOK��5�      $ Number=�62   $ 8��6J    5�      �� *得意先範囲指定To+$ TwoWay=� .  . $ PropertyChanged=�5�      ;  5KyoeiSystem.Framework.Windows.Controls.UcLabelTextBox  5�   
      Label_Context$ 取引先指定��6   $��2��64   $��1��6H      Label_Width$	 100Q�6U   $
��Left=�5�   "      DataAccessName$ M01_TOK_PIC��6=   $��1q�6Z      cWidth$	 500Q�5�   "      Text 5�   "   ��  �� Path$ 取引先指定�$ TwoWay=� .  . C  =KyoeiSystem.Framework.Windows.Controls.UcLabelTextRadioButton  5�   
   -	   5        $��DisplayMethod��5�   &      RadioViewCount$	 Two=�6;      Label_Content$ 表示方法��6P       Label_Width$	  100Q�6e   !   RadioOne_Content$! コード順��6w   "   RadioSecond_Content$" カナ読み順��6�   $��2��6�   #   RadioSelectButton$	# One=�6�   $��1q�5�   �   $   Text$ 6&   �� $ 表示方法�$ TwoWay=� .  . 6�     5�   
   -
   5        $��DealClassification��5�   &   $
 Four=�6@   $ 取引区分��6V   $	  100Q�6k   $! 全件��6}   $" 	得意先��6�   %   RadioThird_Content$% 	支払先��6�   &   RadioFourth_Content$& 	仕入先��6�   $��3��6�   $	# One=�6�   $��1q�5�   �   $ 6&   �� $ 取引区分�$ TwoWay=� .  . 6�   5�   	   5�      5�      5�      