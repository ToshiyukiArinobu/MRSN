   M S B A M L   `   `   `  ����X  TKyoeiSystem.Framework.Windows, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null�[clr-namespace:KyoeiSystem.Framework.Windows.ViewBase;assembly=KyoeiSystem.Framework.Windows&KyoeiSystem.Framework.Windows.ViewBase  5      �[clr-namespace:KyoeiSystem.Framework.Windows.ViewBase;assembly=KyoeiSystem.Framework.Windows&KyoeiSystem.Framework.Windows.ViewBase  5   	   a ]KyoeiSystem.Framework.Windows.Controls, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null�dclr-namespace:KyoeiSystem.Framework.Windows.Controls;assembly=KyoeiSystem.Framework.Windows.Controls&KyoeiSystem.Framework.Windows.Controls 5   	   R NSystem.Data, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089>.clr-namespace:System.Data;assembly=System.DataSystem.Data 5	      # KyoeiSystem.Application.Windows4   .KyoeiSystem.Application.Windows.Views.MST13010   5      bfw[clr-namespace:KyoeiSystem.Framework.Windows.ViewBase;assembly=KyoeiSystem.Framework.Windows  \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      5      R	 NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml 	    5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006 	    5   	   @d2http://schemas.microsoft.com/expression/blend/2008 	    5   	   jvm[clr-namespace:KyoeiSystem.Framework.Windows.ViewBase;assembly=KyoeiSystem.Framework.Windows 	    5   	   uctlsdclr-namespace:KyoeiSystem.Framework.Windows.Controls;assembly=KyoeiSystem.Framework.Windows.Controls 	    5   	   ?Data.clr-namespace:System.Data;assembly=System.Data 	    5	      -   5          � AllowsTransparency  .   � Title$+ %消費税率マスタ入力(MST13010)��5      $	��728��6   $
��1024��6/   $	��768��6?   $
��1024��6L    � WindowStartupLocation$ CenterOwner=�6Z    � Left$ 0��6~   
 � Top$ 0��6�   .��5      (� $
��auto��6   $
��auto��6    (� Stretch$
 Fill=�6)    (� StretchDirection$
 Both=�68   .��5   
   �  � RowDefinitions 5      �� 5      $
��auto �6    �� 5      $	��650 �6    .+�5      +  %System.Windows.Controls.Ribbon.Ribbon  $��Stretch=�6   $��Stretch=�63   .@�5      .  (System.Windows.Controls.Ribbon.RibbonTab  $��	ホーム��6      KeyTip$ h��6)   $��75��64   .@�5      0  *System.Windows.Controls.Ribbon.RibbonGroup  $��90��6"   5        5       $��F1��6"   	   KeyTip$	 MASTER��6.      RibbonFunctionBackground#��[  6>   .@�5!      1  +System.Windows.Controls.Ribbon.RibbonButton  $y�Hidden=�6'   
   Label$
  ��6;      ToolTipTitle$  ��6D      ToolTipDescription$  ��6T   5"        5#      $��F2��6"   $	 MASTER��6.   #��[  6>   .@�5$        $y�Hidden=�6'   $
  ��6;   $  ��6D   $  ��6T   5%        5&      $��F3��6"   $	 MASTER��6.   #��[  6>   .@�5'        $y�Hidden=�6'   $
  ��6;   $  ��6D   $  ��6T   5(        5)      $��F4��6"   $	 MASTER��6.   #��[  6>   .@�5*        $y�Hidden=�6'   $
  ��6;   $  ��6D   $  ��6T   5+        5,      $��F5��6"   $	 MASTER��6.   #��[  6>   .@�5-        $y�Hidden=�6'   $
  ��6;   $  ��6D   $  ��6T   5.        5/      $��F6��6"   $	 MASTER��6.   #��[  6>   .@�50        $y�Hidden=�6'   $
  ��6;   $  ��6D   $  ��6T   51        52      $��F7��6"   $	 MASTER��6.   #��[  6>   .@�53        $y�Hidden=�6'   $
  ��6;   $  ��6D   $  ��6T   54        55      $��F8��6"   $	 MASTER��6.   #��[  6>   .@�56        -   5           LargeImageSource$ ../Images/22リスト.png��56   '      KeyTip$ F8��6i   $��50��6u   $
 	リスト��57      $ リスト一覧(F8)��6   $! リストを表示します��60      ToolTipImageSource$ ../Images/22リスト.png��6P   58        59      $��F9��6"   $	 MASTER��6.   #��[  6>   .@�5:        -   5        $ ../Images/24登録.png��5:   '   $ F9��6h   $��50��6t   $
 登録��5;      $ データ登録(F9)��6   $- '入力データの登録を行います��6/   $ ../Images/24登録.png��6R   5<        5=      $	��F10��6"   $	 MASTER��6/   #��[  6?   .@�5>        -   5        $ ../Images/26クリア.png��5>   '   $	 F10��6i   $��50��6v   $
 入力取消��5?      $ 入力取消(F10)��6   $' !入力の取消しを行います��61   $ ../Images/26クリア.png��6R   5@        5A      $	��F11��6"   $	 MASTER��6/   #��[  6?   .@�5B        -   5        $ ../Images/27終了.png��5B   '   $	 F11��6h   $��50��6u   $
 終了��5C      $ 終了(F11)��6   $ 終了します��6-   $ ../Images/27終了.png��6H   5D        5E      $	��F12��6"   $	 MASTER��6/   #��[  6?   .@�5F        -   5        $ ../Images/28削除.png��5F   '   $	 F12��6h   $��50��6u   $
 削除��5G      $ 削除(F12)��6   $ 入力を削除します��6-   $ ../Images/28削除.png��6K   5H      5J        5L      $��	ヘルプ��6   
�������.@�5M        $��サポート��6"   .@�5N        -   5        $% ../Images/30マニュアル.png��5N   '   $ MANUAL��6P   $
 ﾏﾆｭｱﾙ��5O      $ 取扱説明書��6   $* $取り扱い説明書を開きます��6.     5P      -   5        $( "../Images/29遠隔サポート.png��5P   '   $ SUPPORT��6Q   $
 お問合せ��5Q      $ お問合せ��6   $< 6Web上の｢お問合せ｣ページに遷移します��6,     5R      -	   5        $ ../Images/QandA.png��5R   '   $	 FAQ��6N   $
 QandA��5S      $ QandA��6   $E ?Web上の｢よくあるご質問｣ページに遷移します��6.     5T      -
   5        $( "../Images/13共栄Webリンク.png��5T   '   $ KYOEI��6T   $
 共栄ｼｽﾃﾑ��5U      $ 共栄システムHP��6   $< 6共栄システムのホームページを開きます��62   5V      5W        5Y      $��実施状況入力��6   
�������   ContextualTabGroupHeader$ 入力補助��6>   .@�5Z        $'��!従来ファンクションキー��6"   .@�5[        $% ../Images/16マスタ入力.png��6'   $ d��6P   $
 	行追加��5\      $ 行追加(F5)��6   $' !売上の履歴を表示します��6/   $% ../Images/16マスタ入力.png��6Q     5]      -   5        $ ../Images/24登録.png��5]   '   $ e��6h   $
 マスタ登録��5^      $ 伝票登録(F9)��6   $$ 伝票の登録を行います��62   $ ../Images/24登録.png��6R     5_      -   5        $ ../Images/26クリア.png��5_   '   $ f��6i   $
 入力取消し��5`      $ 入力取消(F12)��6   $' !入力の取消しを行います��63   $ ../Images/26クリア.png��6T     5a      -   5        $ ../Images/27終了.png��5a   '   $ g��6h   $
 終了��5b      $ 入力取消(F11)��6   $ 終了します��60   $ ../Images/27終了.png��6K   5c      5d        5e      $��一括入力��6   
�������$ 入力補助��6<   .@�5f        $'��!従来ファンクションキー��6"   .@�5g        $ ../Images/22リスト.png��6'   $ d��6N   $
 プレビュー��5h      $ プレビュー(F5)��6   $' !売上の履歴を表示します��63   $ ../Images/22リスト.png��6U     5i      -   5        $ ../Images/24登録.png��5i   '   $ e��6h   $
 印刷��5j      $ 
印刷(F9)��6   $$ 伝票の登録を行います��6-   $ ../Images/24登録.png��6M     5k      -   5        $ ../Images/24登録.png��5k   '   $ e��6h   $
 マスタ登録��5l      $ マスタ登録(F9)��6   $$ 伝票の登録を行います��62   $ ../Images/24登録.png��6R     5m      -   5        $ ../Images/26クリア.png��5m   '   $ f��6i   $
 入力取消し��5n      $ 入力取消(F12)��6   $' !入力の取消しを行います��63   $ ../Images/26クリア.png��6T     5o      -   5        $ ../Images/27終了.png��5o   '   $ g��6h   $
 終了��5p      $ 入力取消(F11)��6   $ 終了します��60   $ ../Images/27終了.png��6K   5q      5r      5s      � 5u      $��0��6   $��Bottom=�6     5v      �� 5w      $��74 �6$    � ColumnDefinitions 5y      �� 5z      $��90 �6'   �� 5{      $��* �6'   .+�5~      ]� $��0��6   $��0��6)   $��Stretch=�69   $��2q�6W   ��5      �� *MaintenanceModeBackground+.+�5�      �� $��メイリオ)�5�      $��24(�6"   $��Medium"�60   $��Stretch=�5�      $��Center=�6.   $��Center=�6R   $��Center=�6m   ��5�      �� *MaintenanceMode+��6@   �� *MaintenanceModeForeground+5�   m   5�      �� 5�      $��0��6   $��1��6%   $��Bottom=�65   
��� ���
���  ��$��2,0,2,0q�6u   ��5�      �� *ErrorMessage+y�6/   �� *ErrorMessageVisibility+5�      �� 5�      $��1��6   ���#2792c3$��1q�68   $��5q�6L   
�������._�5�      � $��15q�6    5�      �� 5�      $��30 �6(   �� 5�      $��10 �6(   �� 5�      $��30 �6(   �� 5�      $��3 �6(   �� 5�      $��30 �6(    5�      �� 5�      $��2* �6+   �� 5�      $��* �6+   .+�5�      � $��0��6    5�      �� 5�      $
��auto �6/   �� 5�      $
��auto �6/   �� 5�      $
��auto �6/   �� 5�      $
��auto �6/   �� 5�      $
��auto �6/   �� 5�      $��2* �6/    5�      �� 5�      $��30 �6,   .+�5�      �� -   5        $��FistIdButton��5�   1   $��1��6!   $	��≪��6E   $��	300,2,2,2q�6Q   $��30��6d   |�.  6�   �� 5�      -   5        $��BeforeIdButton��5�   1   $��2��6!   $	��＜��6G   $��0,2,2,2q�6S   $��30��6e   |�.  $��Right=�6�   �� 5�      -   5        $��NextIdButton��5�   1   $��3��6!   $	��＞��6E   $��2q�6Q   $��30��6]   |�.  6�   �� 5�      -   5        $��LastIdButoon��5�   1   $��4��6!   $	��≫��6E   $��0,2,2,2q�6Q   $��30��6b   |�.  6�   5�      ;  5KyoeiSystem.Framework.Windows.Controls.UcLabelTextBox  5�      -   5        $��	Tekiyoymd��5�   +      Label_Context$ 適用開始年月日��6<      Label_Width$	 100Q�6T   $	��300��6f   $��0��6r   $��0��6�   $��	0,0,350,0q�6�      Mask$ 
yyyy/MM/dd��5�   '      
cMaxLength$ 10��69   $
��Left=�6I   =  7KyoeiSystem.Framework.Windows.Controls.FrameworkControl   	IsKeyItem .    cValidationType$
 Date=�5�   Y      Text 5�   &   ��  �� Path$ 適用開始年月日� �� Mode$ TwoWay=� �� NotifyOnTargetUpdated .  �� NotifyOnSourceUpdated .  �� StringFormat$ 
yyyy/MM/dd�� �� UpdateSourceTrigger$ 	LostFocus=�5�   Y   � 5�      $��1��6   $��2��6(   $��2��6<    5�      �� 5�      $��* �6/   .+�5�        -   5        $	��Zei��5�   .   $ 消費税率��69   $	 100Q�6N      cWidth$ 45Q�6`       cTextAlignment$  Right=�6l   $��0,9,2,2q�6�    .  !   ValidationType$! Integer=�6�   $	 #,0��6�   $
��Left=�6�   "   MaxValue$" 99��6�   #   MinValue$# 0��6�    5�   *   �� *消費税率+$	 #,0��$ TwoWay=� .  . 5�   +   4  .KyoeiSystem.Framework.Windows.Controls.UcLabel  5�      -   5        $   cContent$$ %��5�   '   $��1��64   %   cBackground
% ���� &   cForeground
& �   �$��
0,11,660,0q�6r   6�   5�      5�      5�      5�      5�      5�      