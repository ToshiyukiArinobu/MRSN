   M S B A M L   `   `   `  ����X  TKyoeiSystem.Framework.Windows, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null�[clr-namespace:KyoeiSystem.Framework.Windows.ViewBase;assembly=KyoeiSystem.Framework.Windows&KyoeiSystem.Framework.Windows.ViewBase  5   	   �[clr-namespace:KyoeiSystem.Framework.Windows.ViewBase;assembly=KyoeiSystem.Framework.Windows&KyoeiSystem.Framework.Windows.ViewBase  5   	   a ]KyoeiSystem.Framework.Windows.Controls, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null�dclr-namespace:KyoeiSystem.Framework.Windows.Controls;assembly=KyoeiSystem.Framework.Windows.Controls&KyoeiSystem.Framework.Windows.Controls 5   	   # KyoeiSystem.Application.Windows4   .KyoeiSystem.Application.Windows.Views.TKS13010   5   	   bfw[clr-namespace:KyoeiSystem.Framework.Windows.ViewBase;assembly=KyoeiSystem.Framework.Windows  \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   jvm[clr-namespace:KyoeiSystem.Framework.Windows.ViewBase;assembly=KyoeiSystem.Framework.Windows     5   	   uctlsdclr-namespace:KyoeiSystem.Framework.Windows.Controls;assembly=KyoeiSystem.Framework.Windows.Controls     5   	   -   5          � WindowStyle$  SingleBorderWindow=�5       � AllowsTransparency .   � Title$( "得意先売上明細書(TKS13010)��5      $	��728��6   $
��1024��6.   $	��768��6>   $
��1024��6K    � WindowStartupLocation$ CenterOwner=�6Y    � Left$ 0��6}   
 � Top$ 0��6�    � Language$ ja-JP�5      .��5      (� $
��auto��6   $
��auto��6    (� Stretch$
 Fill=�6)    (� StretchDirection$
 Both=�68   .��5   
   � 	 � ColumnDefinitions	 5      �� 5      $
��1024 �6#   
 � RowDefinitions
 5      �� 5      $
��auto �6    �� 5      $	��600 �6    .+�5      +  %System.Windows.Controls.Ribbon.Ribbon  $��Stretch=�6   $��Stretch=�63   .@�5      .  (System.Windows.Controls.Ribbon.RibbonTab  $��	ホーム��6      KeyTip$ h��6)   $��75��64   .@�5       0  *System.Windows.Controls.Ribbon.RibbonGroup  $��90��6"   5"        5$      $��F1��6"      KeyTip$ MASTER��6.      RibbonFunctionBackground#��[  6>   .@�5%      1  +System.Windows.Controls.Ribbon.RibbonButton  -   5           LargeImageSource$% ../Images/15マスタ照会.png��5%   '      ToolTipTitle$ マスタ照会(F1)��6P      ToolTipDescription$3 -入力済みのデータを検索します。��6i      ToolTipImageSource$% ../Images/15マスタ照会.png��6�      Label$ ﾏｽﾀ照会��5&         KeyTip$ F1��6B   $��50��6N   5'        5(      $��F2��6"   $ MASTER��6.   #��[  6>   .@�5)        $y�Hidden=�6'   $  ��6;   $  ��6D   $  ��6T   5*        5+      $��F3��6"   $ MASTER��6.   #��[  6>   .@�5,        $y�Hidden=�6'   $  ��6;   $  ��6D   $  ��6T   5-        5.      $��F4��6"   $ MASTER��6.   #��[  6>   .@�5/        $y�Hidden=�6'   $  ��6;   $  ��6D   $  ��6T   50        51      $��F5��6"   $ MASTER��6.   #��[  6>   .@�52        $y�Hidden=�6'   $  ��6;   $  ��6D   $  ��6T   53        54      $��F6��6"   $ MASTER��6.   #��[  6>   .@�55        $y�Hidden=�6'   $  ��6;   $  ��6D   $  ��6T   56        57      $��F7��6"   $ MASTER��6.   #��[  6>   .@�58        $y�Hidden=�6'   $  ��6;   $  ��6D   $  ��6T   59        5:      $��F8��6"   $ MASTER��6.   #��[  6>   .@�5;        -   5        $ ../Images/23印刷.png��5;   '   $ F8��6h   $��50��6t   $ 印刷��5<      $ 
印刷(F8)��6   $ 印刷を開始します��6,   $ ../Images/23印刷.png��6J   5=        5>      $��F9��6"   $ MASTER��6.   #��[  6>   .@�5?        $y�Hidden=�6'   $  ��6;   $  ��6D   $  ��6T   5@        5A      $	��F10��6"   $ MASTER��6/   #��[  6?   .@�5B        $y�Hidden=�6'   $  ��6;   $  ��6D   $  ��6T   5C        5D      $	��F11��6"   $ MASTER��6/   #��[  6?   .@�5E        -   5        $ ../Images/27終了.png��5E   '   $	 F11��6h   $��50��6u   $ 終了��5F      $ 終了(F11)��6   $ 終了します��6-   $ ../Images/27終了.png��6H   5G        5H      $	��F12��6"   $ MASTER��6/   #��[  6?   .@�5I        $y�Hidden=�6'   $  ��6;   $  ��6D   $  ��6T   5J      5L        5N      $��	ヘルプ��6   
�������.@�5O        $��サポート��6"   .@�5P        -   5        $% ../Images/30マニュアル.png��5P   '   $ MANUAL��6P   $ ﾏﾆｭｱﾙ��5Q      $ 取扱説明書��6   $* $取り扱い説明書を開きます��6.     5R      -   5        $( "../Images/29遠隔サポート.png��5R   '   $ SUPPORT��6Q   $ お問合せ��5S      $ お問合せ��6   $< 6Web上の｢お問合せ｣ページに遷移します��6,     5T      -   5        $ ../Images/QandA.png��5T   '   $	 FAQ��6N   $ QandA��5U      $ QandA��6   $E ?Web上の｢よくあるご質問｣ページに遷移します��6.     5V      -   5        $( "../Images/13共栄Webリンク.png��5V   '   $ KYOEI��6T   $ 共栄ｼｽﾃﾑ��5W      $ 共栄システムHP��6   $< 6共栄システムのホームページを開きます��62   5X      5Y      5Z      � 5\      $��0��6   $��Bottom=�6    
 5]      �� 5^      $��74 �6$   	 5`      �� 5a      $��90 �6'   �� 5b      $��* �6'   .+�5e      ]� $��0��6   $��0��6)   $��Stretch=�69   $��2q�6W   ��5f      �� *MaintenanceModeBackground+.+�5g      �� $��メイリオ)�5h      $��24(�6"   $��Medium"�60   $��Stretch=�5i      $��Center=�6.   $��Center=�6R   $��Center=�6m   ��5g      �� *MaintenanceMode+��6@   �� *MaintenanceModeForeground+5i   m   5j      �� 5m      $��0��6   $��1��6%   $��Bottom=�65   
��� ���
���  ��$��2,0,2,0q�6u   ��5n      �� *ErrorMessage+y�6/   �� *ErrorMessageVisibility+5p      �� 5r      $��1��6   $��5q�6"   
���   �$��1q�6A   
�������._�5s      � $	��800��6   $
��0,20q�6#   
 5t      �� 5u      $
��auto �6(   �� 5v      $
��auto �6(   �� 5w      $
��auto �6(   �� 5x      $
��auto �6(   �� 5y      $
��auto �6(   �� 5z      $
��auto �6(   �� 5{      $
��auto �6(   �� 5|      $
��auto �6(   �� 5}      $
��auto �6(   �� 5~      $��* �6(   .+�5�      � $��2q�6   	 5�      �� 5�      $
��auto �6/   �� 5�      $��* �6/   �� 5�      $��* �6/   
 5�      �� 5�      $��30 �6,   �� 5�      $��30 �6,   .+�5�      4  .KyoeiSystem.Framework.Windows.Controls.UcLabel     cContent$ 得意先指定��6'      cWidth$	 100Q�68   $��1,0,2,0q�6E   ;  5KyoeiSystem.Framework.Windows.Controls.UcLabelTextBox  5�      -	   5        $��TokuisakiPickup��5�   .      Label_Context$$ ﾋﾟｯｸｱｯﾌﾟ指定��6E      Label_Width$	 100Q�6`   $��1��6r   $��0��6�   $��2��6�      DataAccessName$ M01_TOK_PICKUP��5�          Text 5�       �� *得意先ピックアップ+ �� Mode$ TwoWay=� �� NotifyOnSourceUpdated .  �� NotifyOnTargetUpdated .  �� UpdateSourceTrigger$ PropertyChanged=�� 5�      $��1��6   $��1��6/   	 5�      �� 5�   "   $
��auto �63   �� 5�   "   $
��auto �63   �� 5�   "   $
��auto �63   �� 5�   "   $
��auto �63   �� 5�   "   $��* �63   .+�5�        $��0��6+   $ 範囲指定��6;   $	 100Q�6K   $��0,1,1,1q�6X   6  0KyoeiSystem.Framework.Windows.Controls.UcTextBox  5�      $��1��6-   $��1q�6=   $	��100��6H      DataAccessName$ M01_TOK��5�         ValidationType$ Integer=�66      
cMaxLength$ 8��6O       Text  5�      �� *得意先範囲指定From+$ TwoWay=� .  . $ PropertyChanged=��� 5�      $��2��6$   $	��～��64     5�      $��3��6-   $��1q�6=   $	��100��6H   $ M01_TOK��5�      $
 None=�66   $ 8��6L     5�      �� *得意先範囲指定To+$ TwoWay=� .  . $ PropertyChanged=�5�      5�      � 5�      $��2��6   
 5�      �� 5�      $��30 �6,   	 5�      �� 5�      $
��auto �6/   �� 5�      $
��auto �6/   �� 5�      $��3* �6/   .+�5�        $ 作成締日��6.   $��1q�6C   $	 100Q�6N   !   cWidth$! 50Q�6`   "   cHorizontalAlignment$
" Left=�6l   #   ValidationType$# Integer=�5�       $   
cMaxLength$$ 2��69   %   MaxValue$% 31��6H   &   MinValue$& 1��6V    5�       �� *作成締日+$ TwoWay=� .  . $ PropertyChanged=�  5�      $ 末日は31��6'   $ 80Q�68   '   cBackground
' ���� (   cForeground
( �   �")   cHorizontalContentAlignment$
) Left=�6r   $��1��6�   5�        5�      $��4��6*   $��1q�67   $	��180��6B   $
��Left=�6N   $ 作成年月��6i   $	 100Q�6~   $
# Date=�5�      *   Mask$* yyyy/MM��64   +   
IsRequired+ .  5�      �� *作成年月+, �� StringFormat$, yyyy/MM��$ TwoWay=� .  . $ PropertyChanged=�� 5�      $��5��6   	 5�      �� 5�      $
��auto �6/   �� 5�      $
��auto �6/   �� 5�      $
��auto �6/   �� 5�      $
��auto �6/   �� 5�      $��* �6/   .+�5�        $��0��6'   $ 集計期間��67   $	 100Q�6G   $��1q�6T     5�      $��1��6)   $��1q�69   $	��100��6D   $
 Date=�5�      -   Mask$- 
yyyy/MM/dd��64   .   
IsRequired. .   5�      �� *
期間FROM+$, 
yyyy/MM/dd��$ TwoWay=� .  . $ PropertyChanged=��� 5�      $��2��6    $	��～��60     5�      $��3��6)   $��1q�69   $	��100��6D   $
 Date=�5�      $- 
yyyy/MM/dd��64   . .   5�      �� *
期間TO+$, 
yyyy/MM/dd��$ TwoWay=� .  . $ PropertyChanged=�5�      5�      5�      5�      5�      5�      