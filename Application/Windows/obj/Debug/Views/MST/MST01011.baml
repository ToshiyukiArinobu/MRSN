   M S B A M L   `   `   `  ����X  TKyoeiSystem.Framework.Windows, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null�[clr-namespace:KyoeiSystem.Framework.Windows.ViewBase;assembly=KyoeiSystem.Framework.Windows&KyoeiSystem.Framework.Windows.ViewBase  5      a ]KyoeiSystem.Framework.Windows.Controls, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null�dclr-namespace:KyoeiSystem.Framework.Windows.Controls;assembly=KyoeiSystem.Framework.Windows.Controls&KyoeiSystem.Framework.Windows.Controls 5      �dclr-namespace:KyoeiSystem.Framework.Windows.Controls;assembly=KyoeiSystem.Framework.Windows.Controls&KyoeiSystem.Framework.Windows.Controls 5	      Z VKyoeiSystem.Application.Windows, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]3clr-namespace:KyoeiSystem.Application.Windows.Views%KyoeiSystem.Application.Windows.Views 5
      O Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem 5      4   .KyoeiSystem.Application.Windows.Views.MST01011   5      bfw[clr-namespace:KyoeiSystem.Framework.Windows.ViewBase;assembly=KyoeiSystem.Framework.Windows  \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      5      R	 NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml 	    5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006 	    5      e
 aGrapeCity.WPF.SpreadGrid, Version=1.0.2015.1109, Culture=neutral, PublicKeyToken=5c5c8ff7a6858ccc=sg4http://schemas.grapecity.com/windows/spreadgrid/2012 
 5      :d2http://schemas.microsoft.com/expression/blend/2008 
 5      octlsdclr-namespace:KyoeiSystem.Framework.Windows.Controls;assembly=KyoeiSystem.Framework.Windows.Controls 
 5      sControlsdclr-namespace:KyoeiSystem.Framework.Windows.Controls;assembly=KyoeiSystem.Framework.Windows.Controls 
 5	      <uc3clr-namespace:KyoeiSystem.Application.Windows.Views 
 5
      3System&clr-namespace:System;assembly=mscorlib 
 5      -   5          � WindowStyle$  SingleBorderWindow=�5       � AllowsTransparency .  $	��728��5      $
��1024��6   $	��768��6#   $
��1024��60    � WindowStartupLocation$ CenterOwner=�6>    � Left$ 0��6b   
 � Top$ 0��6k    � Title$( "仕入データ問合せ(MST01011)��5      .��5      �  � RowDefinitions 5   
   �� 5      $
��auto �6   �� 5      $
��auto �6   �� 5      $
��300* �6   .+�5   
   +  %System.Windows.Controls.Ribbon.Ribbon  $��Stretch=�6   $��Stretch=�6/   .@�5      .  (System.Windows.Controls.Ribbon.RibbonTab  $��	ホーム��6      KeyTip$ h��6%   $��75��60   .@�5      0  *System.Windows.Controls.Ribbon.RibbonGroup  $��90��6   5!        5#      $��F1��6      KeyTip$ MASTER��6*      RibbonFunctionBackground#��[  6:   .@�5$      1  +System.Windows.Controls.Ribbon.RibbonButton  -   5        	   LargeImageSource$%	 ../Images/15マスタ照会.png��5$   #   $��0,0,0,0q�6L   
   ToolTipTitle$
 マスタ照会(F1)��6]      ToolTipDescription$3 -入力済みのデータを検索します。��6v      ToolTipImageSource$% ../Images/15マスタ照会.png��6�      Label$ ﾏｽﾀ照会��5%         KeyTip$ F1��6B   $��50��6N   5&        5'      $��F2��6   $ MASTER��6*   #��[  6:   .@�5(        $y�Hidden=�6#   $  ��67   $
  ��6@   $  ��6P   5)        5*      $��F3��6   $ MASTER��6*   #��[  6:   .@�5+        $y�Hidden=�6#   $  ��67   $
  ��6@   $  ��6P   5,        5-      $��F4��6   $ MASTER��6*   #��[  6:   .@�5.        $y�Hidden=�6#   $  ��67   $
  ��6@   $  ��6P   5/        50      $��F5��6   $ MASTER��6*   #��[  6:   .@�51        $y�Hidden=�6#   $  ��67   $
  ��6@   $  ��6P   52        53      $��F6��6   $ MASTER��6*   #��[  6:   .@�54        $y�Hidden=�6#   $  ��67   $
  ��6@   $  ��6P   55        56      $��F7��6   $ MASTER��6*   #��[  6:   .@�57        $y�Hidden=�6#   $  ��67   $
  ��6@   $  ��6P   58        59      $��F8��6   $ MASTER��6*   #��[  6:   .@�5:        $y�Hidden=�6#   $  ��67   $
  ��6@   $  ��6P   5;        5<      $��F9��6   $ MASTER��6*   #��[  6:   .@�5=        -   5        $	 ../Images/24登録.png��5>      $ F9��6X   $��50��6d   $- '入力データの登録を行います��5?      $ ../Images/24登録.png��6:   $ 登録��5@      $
 データ登録(F9)��6"   y�5A      ��  �� Path$ DataUpdateVisible� �� Mode$ TwoWay=� �� NotifyOnSourceUpdated .  �� NotifyOnTargetUpdated . 5C        5D      $	��F10��6   $ MASTER��6+   #��[  6;   .@�5E        -   5        $	 ../Images/26クリア.png��5E   #   $	 F10��6e   $��50��6r   $' !入力の取消しを行います��5F      $ ../Images/26クリア.png��68   $ 入力取消��5G      $
 入力取消(F10)��6$   5I        5J      $	��F11��6   $ MASTER��6+   #��[  6;   .@�5K        -   5        $	 ../Images/27終了.png��5K   #   $	 F11��6d   $��50��6q   $ 終了��5L      $
 終了(F11)��6$   $ 終了します��6;   $ ../Images/27終了.png��6V   5N        5O      $	��F12��6   $ MASTER��6+   #��[  6;   .@�5P        -   5        $	 ../Images/28削除.png��5Q      $	 F12��6X   $��50��6e   $ 入力を削除します��5R      $ ../Images/28削除.png��65   $ 削除��5S      $
 削除(F12)��6"   y�5T      �� $ DataUpdateVisible�$ TwoWay=� .  . 5V      5X        5Z      $��	ヘルプ��6   
�������.@�5[        $��サポート��6   .@�5\        -   5        $%	 ../Images/30マニュアル.png��5\   #   $ MANUAL��6L   $ ﾏﾆｭｱﾙ��5]   #   $
 取扱説明書��61   $* $取り扱い説明書を開きます��6F     5^      -   5        $(	 "../Images/29遠隔サポート.png��5^   #   $ SUPPORT��6M   $ お問合せ��5_   #   $
 お問合せ��60   $< 6Web上の｢お問合せ｣ページに遷移します��6D     5`      -	   5        $	 ../Images/QandA.png��5`   #   $	 FAQ��6J   $ QandA��5a   #   $
 QandA��61   $E ?Web上の｢よくあるご質問｣ページに遷移します��6F     5b      -
   5        $(	 "../Images/13共栄Webリンク.png��5b   #   $ KYOEI��6P   $ 共栄ｼｽﾃﾑ��5c   #   $
 共栄システムHP��62   $< 6共栄システムのホームページを開きます��6J   5d      5e      5g      � 5i   
   $��0��6   $��Bottom=�6    5j      �� 5k      $��74 �6     � ColumnDefinitions 5m      �� 5n      $��90 �6#   �� 5o      $��* �6#   .+�5s      ]� $��0��6   $��0��6%   $��Stretch=�65   $��2q�6S   ��5t      �� *MaintenanceModeBackground+.+�5u      �� $��メイリオ)�5v      $��24(�6"   $��Medium"�60   $��Stretch=�5w      $��Center=�6.   $��Center=�6R   $��Center=�6m   ��5u      �� *MaintenanceMode+��6<   �� *MaintenanceModeForeground+5w   m   5x      �� 5|      $��0��6   $��1��6!   $��Bottom=�61   
��� ���
���  ��$��2,0,2,0q�6q   ��5}      �� *ErrorMessage+y�6/   �� *ErrorMessageVisibility+5      � 5�   
   -   5        $
��Func��5�      $��1��6   
�������$��3��6?    5�      �� 5�      $
��auto �6    �� 5�      $
��auto �6    �� 5�      $
��auto �6    .+�5�      �� -   5        $��Header��5�      $��0��6#   $��1q�60   $��
10,10,10,5q�6D    �� Padding$ 5q�6X   ._�5�      �  5�      �� 5�      $��30 �6(   �� 5�      $��30 �6(   �� 5�      $��30 �6(   �� 5�      $
��auto �6(    5�      �� 5�      $	��450 �6+   �� 5�      $	��300 �6+   �� 5�      $	��350 �6+   .+�5�      ;  5KyoeiSystem.Framework.Windows.Controls.UcLabelTextBox  -   5        $��txt正式名称��5�      $��0��6&      ImeType$ Native=�63   $
��Left=�6D   $��0,2,0,0q�6_      Label_Context$ 中分類名��5�         Label_Width$	 100Q�6,      cWidth$	 330Q�5�         
cMaxLength$ 50��6$      cValidationType$
 None=�64   ?  9KyoeiSystem.Framework.Windows.Controls.UcLabelTwinTextBox  5�      -   5        $��	myCompany��5�   .   $��1��6?   $
��Left=�6L      DataAccessName$ M72_TNT��5�   $      DataAccessMode$ On=�6=      Label1Visibility$ 	Collapsed=�5�   $      Label2Visibility$ 	Collapsed=�6A      Label_Context$ 担当会社コード��5�   $       Label_Width$	  100Q�6<   !   
Text1Width$	! 120Q�5�   $   "   Text1MaxLength$" 3��65   #   Text1IMEType$	# Off=�6H   $$   Text1VerticalContentAlignment$$ Center=�6[   $	��223��6�   C  =KyoeiSystem.Framework.Windows.Controls.UcLabelTextRadioButton  5�      -   5        $��radio_取引区分��5�   2   $��0��6D   $��1��6T   %   Label_Content$% 取引区分��5�      &   Label_Width$	& 100Q�6*   '   RadioSelectButton$	' One=�5�      (   RadioOneStart( .  )   RadioViewCount$
) Five=�6C   *   RadioOne_Content$* 	得意先��5�      +   RadioSecond_Content$+ 	仕入先��6,   ,   RadioThird_Content$, 	加工先��6F   -   RadioFourth_Content$- 相殺��6_   .   RadioFifth_Content$. 販社��6x   $/   Text1VerticalContentAlignment$/ Center=�5�      $��2��5�   �   $��
228,0,93,0q�6�   0   Text0 6   �� $# RowM01TOK[Ｔ消費税区分]�$ TwoWay=� .  . 6�   �� 5�      -   5        $��	btnSearch��5�      $��0��6.   $��2��6;   $��2��6K   $��50��6\   $��50��6g   $
��Left=�5�      $��Stretch=�66   $��0q�6R   .��5�      �� $)��#../Images/検索実行ボタン.png��6    $
��Fill=�6?   5�      � 5�      $��1��6   $��0,3,0,0q�6(    5�      �� 5�      $
��auto �6/   �� 5�      $
��auto �6/   �� 5�      $	��150 �6/   �� 5�      $
��auto �6/   �� 5�      $
��auto �6/   �� 5�      $
��auto �6/   5�      � 5�      $��2��6   $��0,3,0,0q�6(    5�      �� 5�      $
��auto �6/   �� 5�      $	��150 �6/   �� 5�      $
��auto �6/   5�      5�      5�      � 5�      $��1��6   .+�5�      / 
 )GrapeCity.Windows.SpreadGrid.GcSpreadGrid  -   5        $��
spGridList��5�      1   AutoGenerateColumns1 .  2   
LayoutMode$2 Template=�6A   3   RowCount$3 0��6W   4   ColumnCount$4 9��6d   $	��450��6t   5   ColumnDragMode$5 Direct=�5�      6   CanUserSortColumns6 .  7   CanUserFilterColumns7 .  $��10,10,192,-438q�6c   8   DocumentUri$K8 E/KyoeiSystem.Application.Windows;component/views/MST/MST01011G1.sgxml?�5�      9   ItemsSource9 5�      �� *SearchResult+$ TwoWay=�: �� UpdateSourceTrigger$: PropertyChanged=� .  . ;   SelectionBorderUnfocused; 5�      -	 
 'GrapeCity.Windows.SpreadGrid.BorderLine	  5�      < 	  Color$< Orange��6(   = 	  Style$= Thick=�67   >   SelectionBorder> 5�      	  5�      $< Orange��6(   $= Thick=�67   ?   Columns? 5�      )
 
 #GrapeCity.Windows.SpreadGrid.Column
  5�      @ 
  Locked$
@ TrueX�6$   A 
  	Focusable$
A TrueX�62   B 
  HorizontalAlignment$B CenterX�6C   C 
  	DataFieldC 5�      3 
 -GrapeCity.Windows.SpreadGrid.BindingDataField  5�   "   D   BindingD 66   �� *取引先コード+5�      
  5�      $
@ TrueX�6$   $
A TrueX�62   $B CenterX�6C   C 5�        5�   "   D 66   �� *枝番+5�      
  5�      $
@ TrueX�6$   $
A TrueX�62   $B CenterX�6C   C 5�        5�   "   D 66   �� *正式名称+5�      
  5�      $@ FalseX�6$   $
A TrueX�63   $B RightX�6D   C 5�        5�   "   D 66   �� *請求担当者コード+5�      
  5�      $@ FalseX�6$   $
A TrueX�63   $B RightX�6D   C 5�        5�   "   D 66   �� *請求担当者名+5�      
  5�      $@ FalseX�6$   $B RightX�63   C 5        5  "   D 66   �� *支払担当者コード+5     
  5     $@ FalseX�6$   $B RightX�63   C 5       5  "   D 66   �� *担当者名+5	     5     5     5     5     5     