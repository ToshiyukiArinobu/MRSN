   M S B A M L   `   `   `  ����X  TKyoeiSystem.Framework.Windows, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null�[clr-namespace:KyoeiSystem.Framework.Windows.ViewBase;assembly=KyoeiSystem.Framework.Windows&KyoeiSystem.Framework.Windows.ViewBase  5   	   �[clr-namespace:KyoeiSystem.Framework.Windows.ViewBase;assembly=KyoeiSystem.Framework.Windows&KyoeiSystem.Framework.Windows.ViewBase  5   	   a ]KyoeiSystem.Framework.Windows.Controls, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null�dclr-namespace:KyoeiSystem.Framework.Windows.Controls;assembly=KyoeiSystem.Framework.Windows.Controls&KyoeiSystem.Framework.Windows.Controls 5   	   # KyoeiSystem.Application.Windows4   .KyoeiSystem.Application.Windows.Views.TKS31010   5   	   bfw[clr-namespace:KyoeiSystem.Framework.Windows.ViewBase;assembly=KyoeiSystem.Framework.Windows  \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   jvm[clr-namespace:KyoeiSystem.Framework.Windows.ViewBase;assembly=KyoeiSystem.Framework.Windows     5   	   uctlsdclr-namespace:KyoeiSystem.Framework.Windows.Controls;assembly=KyoeiSystem.Framework.Windows.Controls     5   	   -   5          � WindowStyle$  SingleBorderWindow=�5       � AllowsTransparency .   � Title$( "★締日集計処理★(TKS31010)��5      $	��728��6"   $
��1024��62   $	��768��6B   $
��1024��6O    � WindowStartupLocation$ CenterScreen=�6]    � Left$ 0��6�   
 � Top$ 0��6�    � 	Resources 5      <  6KyoeiSystem.Framework.Windows.Controls.BooleanInverter   boolInverter��%   &	          5   
   6   5      .��5      �  � RowDefinitions 5   
   �� 5      $
��auto �6   �� 5      $��* �6   .+�5   
   +  %System.Windows.Controls.Ribbon.Ribbon  $��Stretch=�6   $��Stretch=�6/   .@�5      .  (System.Windows.Controls.Ribbon.RibbonTab  $��	ホーム��6      KeyTip$ h��6%   $��75��60   .@�5      0  *System.Windows.Controls.Ribbon.RibbonGroup  $��90��6   5!        5#      $��F1��6   	   KeyTip$	 MASTER��6*     RibbonFunctionBackground#��[ 6:   .@�5$      1  +System.Windows.Controls.Ribbon.RibbonButton  -   5        
   LargeImageSource$%
 ../Images/15マスタ照会.png��5$   #      ToolTipTitle$ マスタ照会(F1)��6L      ToolTipDescription$3 -入力済みのデータを検索します。��6e      ToolTipImageSource$% ../Images/15マスタ照会.png��6�      Label$ ﾏｽﾀ照会��5%         KeyTip$ F1��6B   $��50��6N   5&        5'      $��F2��6   $	 MASTER��6*   #��[ 6:   .@�5(        $y�Hidden=�6#   $  ��67   $  ��6@   $  ��6P   5)        5*      $��F3��6   $	 MASTER��6*   #��[ 6:   .@�5+        $y�Hidden=�6#   $  ��67   $  ��6@   $  ��6P   5,        5-      $��F4��6   $	 MASTER��6*   #��[ 6:   .@�5.        $y�Hidden=�6#   $  ��67   $  ��6@   $  ��6P   5/        50      $��F5��6   $	 MASTER��6*   #��[ 6:   .@�51        $y�Hidden=�6#   $  ��67   $  ��6@   $  ��6P   52        53      $��F6��6   $	 MASTER��6*   #��[ 6:   .@�54        $y�Hidden=�6#   $  ��67   $  ��6@   $  ��6P   55        56      $��F7��6   $	 MASTER��6*   #��[ 6:   .@�57        $y�Hidden=�6#   $  ��67   $  ��6@   $  ��6P   58        59      $��F8��6   $	 MASTER��6*   #��[ 6:   .@�5:        $y�Hidden=�6#   $  ��67   $  ��6@   $  ��6P   5;        5<      $��F9��6   $	 MASTER��6*   #��[ 6:   .@�5=        -   5        $
 ../Images/24登録.png��5=   #   $ F9��6d   $��50��6p   $ 集計開始��5>      $ 集計開始(F9)��6   $- '締め日集計処理を開始します��60   $ ../Images/24登録.png��6S   5?        5@      $	��F10��6   $	 MASTER��6+   #��[ 6;   .@�5A        $y�Hidden=�6#   $  ��67   $  ��6@   $  ��6P   5B        5C      $	��F11��6   $	 MASTER��6+   #��[ 6;   .@�5D        -   5        $
 ../Images/27終了.png��5D   #   $	 F11��6d   $��50��6q   $ 終了��5E      $ 終了(F11)��6   $ 終了します��6-   $ ../Images/27終了.png��6H   5F        5G      $	��F12��6   $	 MASTER��6+   #��[ 6;   .@�5H        $y�Hidden=�6#   $  ��67   $  ��6@   $  ��6P   5I      5K        5M      $��	ヘルプ��6   
�������.@�5N        $��サポート��6   .@�5O        -   5        $%
 ../Images/30マニュアル.png��5O   #   $ MANUAL��6L   $ ﾏﾆｭｱﾙ��5P      $ 取扱説明書��6   $* $取り扱い説明書を開きます��6.     5Q      -   5        $(
 "../Images/29遠隔サポート.png��5Q   #   $ SUPPORT��6M   $ お問合せ��5R      $ お問合せ��6   $< 6Web上の｢お問合せ｣ページに遷移します��6,     5S      -   5        $
 ../Images/QandA.png��5S   #   $	 FAQ��6J   $ QandA��5T      $ QandA��6   $E ?Web上の｢よくあるご質問｣ページに遷移します��6.     5U      -   5        $(
 "../Images/13共栄Webリンク.png��5U   #   $ KYOEI��6P   $ 共栄ｼｽﾃﾑ��5V      $ 共栄システムHP��6   $< 6共栄システムのホームページを開きます��62   5W      5X      5Y      � 5[   
   $��0��6   $��Bottom=�6    5\      �� 5]      $��74 �6     � ColumnDefinitions 5_      �� 5`      $��90 �6#   �� 5a      $��* �6#   .+�5d      ]� $��0��6   $��0��6%   $��Stretch=�65   $��2q�6S   ��5e      �� *MaintenanceModeBackground+.+�5f      �� $��メイリオ)�5g      $��24(�6"   $��Medium"�60   $��Stretch=�5h      $��Center=�6.   $��Center=�6R   $��Center=�6m   ��5f      �� *MaintenanceMode+��6<   �� *MaintenanceModeForeground+5h   m   5i      �� 5l      $��0��6   $��1��6!   $��Bottom=�61   
��� ���
���  ��$��2,0,2,0q�6q   ��5m      �� *ErrorMessage+y�6/   �� *ErrorMessageVisibility+5o      �� 5q   
   $��1��6   $��5q�6   
���   �$��1q�6=   
�������._�5r      � $	��800��6   $
��0,20q�6    5s      �� 5t      $
��auto �6$   �� 5u      $
��auto �6$   �� 5v      $
��auto �6$   �� 5w      $
��auto �6$   �� 5x      $
��auto �6$   �� 5y      $
��auto �6$   �� 5z      $
��auto �6$   �� 5{      $
��auto �6$   �� 5|      $
��auto �6$   �� 5}      $��* �6$   .+�5�      � $��2q�6    5�      �� 5�      $
��auto �6+   �� 5�      $��* �6+   �� 5�      $��* �6+    5�      �� 5�      $��30 �6(   �� 5�      $��30 �6(   .+�5�      4  .KyoeiSystem.Framework.Windows.Controls.UcLabel     cContent$ 得意先指定��6#      cWidth$	 100Q�64   $��1,0,2,0q�6A   ;  5KyoeiSystem.Framework.Windows.Controls.UcLabelTextBox  5�         Label_Context$$ ﾋﾟｯｸｱｯﾌﾟ指定��6*      DataAccessName$ TKS31010_PICKUP��6E      Label_Width$	 100Q�6f   $��1��6x   $��0��6�   $��2��6�   ?  9KyoeiSystem.Framework.Windows.Controls.UcLabelTwinTextBox  5�         Label_Context$ 範囲指定��6.   $��1,1,45,1q�6C      Label_Width$	 100Q�6U   $��1��6g   $��1��6w      
Label1Text$	 ～��6�   $
��Left=�6�      
Text1Width$	 150Q�5�   $      
Text2Width$	 150Q�65      Text1Background
 �����   Text2Background
 �����   Text1MaxLength$ 8��6v      Text2MaxLength$ 8��6�      DataAccessName$ M01_TOK��6�   $��2��6�       Text1  5�   $   �� *得意先FROM+! �� Mode$! TwoWay=�" �� NotifyOnSourceUpdated" . # �� NotifyOnTargetUpdated# . $ �� UpdateSourceTrigger$$ PropertyChanged=�%   Text2% 5�   $   �� *得意先TO+$! TwoWay=�" . # . $$ PropertyChanged=�5�      � 5�      $��2��6    5�      �� 5�      $��30 �6(    5�      �� 5�      $
��auto �6+   �� 5�      $
��auto �6+   �� 5�      $��3* �6+   .+�5�        $ 作成締日��6*   $��1q�6?   $	 100Q�6J   &   cWidth$& 50Q�6\   '   cHorizontalAlignment$
' Left=�6h   (   ValidationType$( Integer=�5�       )   
cMaxLength$) 2��69   *   MinValue$* 1��6H   +   MaxValue$+ 31��6U   ,   Text, 5�       �� *作成締日+$! TwoWay=�" . # . $$ PropertyChanged=�  5�      $ 末日は31��6#   $ 80Q�64   -   cBackground
- ���� .   cForeground
. �   �"/   cHorizontalContentAlignment$
/ Left=�6n   $��1��6�   �� 5�      -	   5        $��2��5�      $��全締日集計��6/   $��Center=�6Z   0 d� 	IsChecked0 5�      �� *全締日集計+$! TwoWay=�# . " . 1 �� 	Converter#1 [  5�        5�      $��4��6&   $��1q�63   $	��200��6>   $
��Left=�6J   $ 作成年月��6e   $	 100Q�6z   $
( Date=�5�      2   Mask$2 yyyy/MM��64   3   
IsRequired3 . , 5�      �� *作成年月+4 �� StringFormat$4 yyyy/MM��$! TwoWay=�" . # . $$ PropertyChanged=�  5�      $��5��6&   $��1q�63   $	��160��6>   $
��Left=�6J   $ 回数��6e   $	 100Q�6x   $( Integer=�5�      $) 1��67   3 . , 5�      �� *回数+$! TwoWay=�" . # . $$ PropertyChanged=��� 5�      -
   5        $��0��5�      $��6��6+   $��Center=�68   $��集計期間の再計算��6S   $��5,0,0,2q�6�   0 5�      �� *	再計算+$! TwoWay=�# . " . #1 [  5�      5�      5�      5�      