   M S B A M L   `   `   `  ����X  TKyoeiSystem.Framework.Windows, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null�[clr-namespace:KyoeiSystem.Framework.Windows.ViewBase;assembly=KyoeiSystem.Framework.Windows&KyoeiSystem.Framework.Windows.ViewBase  5   	   �[clr-namespace:KyoeiSystem.Framework.Windows.ViewBase;assembly=KyoeiSystem.Framework.Windows&KyoeiSystem.Framework.Windows.ViewBase  5   	   a ]KyoeiSystem.Framework.Windows.Controls, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null�dclr-namespace:KyoeiSystem.Framework.Windows.Controls;assembly=KyoeiSystem.Framework.Windows.Controls&KyoeiSystem.Framework.Windows.Controls 5   	   # KyoeiSystem.Application.Windows4   .KyoeiSystem.Application.Windows.Views.TKS05010   5   	   bfw[clr-namespace:KyoeiSystem.Framework.Windows.ViewBase;assembly=KyoeiSystem.Framework.Windows  \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   jvm[clr-namespace:KyoeiSystem.Framework.Windows.ViewBase;assembly=KyoeiSystem.Framework.Windows     5   	   uctlsdclr-namespace:KyoeiSystem.Framework.Windows.Controls;assembly=KyoeiSystem.Framework.Windows.Controls     5   	   e	 aGrapeCity.WPF.SpreadGrid, Version=1.0.2015.1109, Culture=neutral, PublicKeyToken=5c5c8ff7a6858ccc=sg4http://schemas.grapecity.com/windows/spreadgrid/2012 	 5	   	   -   5        $��Window��5   .     � WindowStyle$  SingleBorderWindow=�5       � AllowsTransparency .   � Title$% 得意先集計処理(TKS05010)��5      $	��728��6   $
��1024��6-   $	��768��6=   $
��1024��6J    � WindowStartupLocation$ CenterOwner=�6X    � Left$ 0��6|   
 � Top$ 0��6�    � Language$ ja-JP�5       � 	Resources 5      <  6KyoeiSystem.Framework.Windows.Controls.BooleanInverter   boolInverter��%   &	          5   
   6   5      .��5      � $��5q�6    � RowDefinitions 5   
   �� 5      $
��auto �6   �� 5      $
��auto �6   �� 5      $��27 �6   �� 5      $��27 �6   �� 5      $��27 �6   �� 5      $��10 �6   �� 5      $��* �6   	 � ColumnDefinitions	 5    
   �� 5!      $
��180* �6   �� 5"      $
��339* �6   .+�5&   
   �� $��0��6   $��1��6!   $��2��6.   $��9��6B   ���#2792c3$��1q�6i   5(      +  %System.Windows.Controls.Ribbon.Ribbon  5+   
   $��Stretch=�6   $��2��60   $��Stretch=�6D   .@�5-      .  (System.Windows.Controls.Ribbon.RibbonTab  $��	ホーム��6   
   KeyTip$
 h��6%   $��75��60   .@�5.      0  *System.Windows.Controls.Ribbon.RibbonGroup  $��90��6   50        52      $��F1��6      KeyTip$ MASTER��6*     RibbonFunctionBackground#��[ 6:   .@�53      1  +System.Windows.Controls.Ribbon.RibbonButton  -   5           LargeImageSource$% ../Images/15マスタ照会.png��53   #      ToolTipTitle$ マスタ照会(F1)��6L      ToolTipDescription$3 -入力済みのデータを検索します。��6e      ToolTipImageSource$% ../Images/15マスタ照会.png��6�      Label$ ﾏｽﾀ照会��54         KeyTip$ F1��6B   $��50��6N   55        56      $��F2��6   $ MASTER��6*   #��[ 6:   .@�57        $y�Hidden=�6#   $  ��67   $  ��6@   $  ��6P   58        59      $��F3��6   $ MASTER��6*   #��[ 6:   .@�5:        $y�Hidden=�6#   $  ��67   $  ��6@   $  ��6P   5;        5<      $��F4��6   $ MASTER��6*   #��[ 6:   .@�5=        $y�Hidden=�6#   $  ��67   $  ��6@   $  ��6P   5>        5?      $��F5��6   $ MASTER��6*   #��[ 6:   .@�5@        $y�Hidden=�6#   $  ��67   $  ��6@   $  ��6P   5A        5B      $��F6��6   $ MASTER��6*   #��[ 6:   .@�5C        $y�Hidden=�6#   $  ��67   $  ��6@   $  ��6P   5D        5E      $��F7��6   $ MASTER��6*   #��[ 6:   .@�5F        $y�Hidden=�6#   $  ��67   $  ��6@   $  ��6P   5G        5H      $��F8��6   $ MASTER��6*   #��[ 6:   .@�5I        -   5        $y�Hidden=�5I   #   $ ../Images/23印刷.png��67   $ F8��6x   $ 印刷��5J      $ 
印刷(F8)��6   $ 印刷を開始します��6,   $ ../Images/23印刷.png��6J   5K        5L      $��F9��6   $ MASTER��6*   #��[ 6:   .@�5M        -   5        $" ../Images/25集計開始.png��5M   #   $ F9��6f   $��50��6r   $ 集計開始��5N      $ 集計開始(F9)��6   $! 集計処理を行います��60   $" ../Images/25集計開始.png��6O   5O        5P      $	��F10��6   $ MASTER��6+   #��[ 6;   .@�5Q        $y�Hidden=�6#   $  ��67   $  ��6@   $  ��6P   5R        5S      $	��F11��6   $ MASTER��6+   #��[ 6;   .@�5T        -   5        $ ../Images/27終了.png��5T   #   $	 F11��6d   $��50��6q   $ 終了��5U      $ 終了(F11)��6   $ 終了します��6-   $ ../Images/27終了.png��6H   5V        5W      $	��F12��6   $ MASTER��6+   #��[ 6;   .@�5X        $y�Hidden=�6#   $  ��67   $  ��6@   $  ��6P   5Y      5[        5]      $��	ヘルプ��6   
�������.@�5^        $��サポート��6   .@�5_        -   5        $% ../Images/30マニュアル.png��5_   #   $ MANUAL��6L   $ ﾏﾆｭｱﾙ��5`      $ 取扱説明書��6   $* $取り扱い説明書を開きます��6.     5a      -   5        $( "../Images/29遠隔サポート.png��5a   #   $ SUPPORT��6M   $ お問合せ��5b      $ お問合せ��6   $< 6Web上の｢お問合せ｣ページに遷移します��6,     5c      -   5        $ ../Images/QandA.png��5c   #   $	 FAQ��6J   $ QandA��5d      $ QandA��6   $E ?Web上の｢よくあるご質問｣ページに遷移します��6.     5e      -	   5        $( "../Images/13共栄Webリンク.png��5e   #   $ KYOEI��6P   $ 共栄ｼｽﾃﾑ��5f      $ 共栄システムHP��6   $< 6共栄システムのホームページを開きます��62   5g      5h      5i      � 5k   
   $��0��6   $��2��6   $��Bottom=�60    5l      �� 5m      $��74 �6    	 5o      �� 5p      $��90 �6#   �� 5q      $��* �6#   .+�5t      ]� $��0��6   $��0��6%   $��Stretch=�65   $��2q�6S   ��5u      �� *MaintenanceModeBackground+.+�5v      �� $��メイリオ)�5w      $��24(�6"   $��Medium"�60   $��Stretch=�5x      $��Center=�6.   $��Center=�6R   $��Center=�6m   ��5v      �� *MaintenanceMode+��6<   �� *MaintenanceModeForeground+5x   m   5y      �� 5|      $��0��6   $��1��6!   $��Bottom=�61   
��� ���
���  ��$��2,0,2,0q�6q   ��5}      �� *ErrorMessage+y�6/   �� *ErrorMessageVisibility+5      � 5�   
   $��0��6   $��1��6   $��2��6,   $��5,5,0,0q�6@   	 5�      �� 5�      $	��150 �6#   �� 5�      $��3* �6#   �� 5�      $��2* �6#    5�      �� 5�      $��27 �6    �� 5�      $��27 �6    .+�5�      4  .KyoeiSystem.Framework.Windows.Controls.UcLabel  $��0��6   $��0��6+      cContent$ 得意先指定��68   $��1q�6I   � 5�      $��1��6   .+�5�      ?  9KyoeiSystem.Framework.Windows.Controls.UcLabelTwinTextBox     Label_Context$ 範囲指定��6*      Label_Width$	 100Q�6?      
Label1Text$	 ～��6Q   $��0��6`   $��1��6m      
Text1Width$	 150Q�6}   $��5,0,0,0q�6�      DataAccessName$ M01_TOK��5�   .      MasterCheckEnabled .     Label2Visibility$ 	Collapsed=�6b      Text2Visibility$ 	Collapsed=�6      Text1MaxLength$ 8��6�      Text1ValidationType$ Integer=�6�      Text1 5�   .   �� *得意先範囲指定From+ �� Mode$ TwoWay=� �� NotifyOnSourceUpdated .   �� NotifyOnTargetUpdated  . !   LinkItem! 5�   .   �� " �� Path$" 取引区分�$ TwoWay=� .   .   5�      $��0��6*   $��1��67   $	 150Q�6G   $��Right=�6X   $ M01_TOK��5�   .    .  $��0,0,10,0q�6b   #   LabelVisibility$# 	Collapsed=�5�   .   $   Label1Visibility$$ 	Collapsed=�6J   $ 	Collapsed=�6g   $ 	Collapsed=�6�   $ 8��6�   $ Integer=�6�    5�   .   �� *得意先範囲指定To+$ TwoWay=� .   . ! 5�   .   �� $" 取引区分�$ TwoWay=� .   . 5�      ;  5KyoeiSystem.Framework.Windows.Controls.UcLabelTextBox  5�      %   Label_Context$$% ﾋﾟｯｸｱｯﾌﾟ指定��6"   &   DataAccessName$& M_01_TOK_PICUP��6>   $��1��6_   $��1��6l   '   Label_Width$	' 100Q�6|   $��5,0,50,0q�6�   (   Text( 5�   *   �� *得意先ピックアップ+$ TwoWay=� .   . � 5�      $��1��6   $��1��6#   	 5�      �� 5�      $
��auto �6'   �� 5�      $
��auto �6'   �� 5�      $
��auto �6'   �� 5�      $
��auto �6'   �� 5�      $��* �6'   5�      �� 5�      -
   5        $��2��5�      $��2��6%   $��50��66   $��50��6A   $
��Left=�6M   $��Stretch=�6h   $��0q�6�   .��5�      �� $)��#../Images/検索実行ボタン.png��6   $
��Fill=�67   5�      �� 5�      -   5        $��2��5�      $��2��6&   $
��Left=�67   $��Bottom=�6R   $%��表示項目
位置ﾘｾｯﾄ��6m   $��76��6�   |�.  $��50��5�   )   $��	100,0,0,0q�65   5�   B   5�      � 5�   
   $��2��6   $��2��6   $��5,0,0,0q�61   	 5�      �� 5�      $
��auto �6#   �� 5�      $��* �6#   .+�5�        -   5        $��Simebi��5�   "   $% 作成締日��60   $	' 150Q�6E   $��0��6W   $��0��6g   $	��200��6t   $
��Left=�6�   )   ValidationType$) Integer=�5�      *   
cMaxLength$* 2��65   +   MaxValue$+ 31��6]   ,   MinValue$, 1��6k   ( 5�      �� *作成締日+$ TwoWay=� .   . - �� UpdateSourceTrigger$- PropertyChanged=��� 5�      $��2��6   $��0��6$   $0��*末日は31 ※空白で全締日集計※��61   $
��Left=�6L   5�        5�   
   -   5        $��SakuseiNengetu��5�      $% 作成年月��64   $	' 150Q�6I   $��0��6[   $��3��6k   $��5,1,1,1q�6x   .   
IsRequired. . $) 
DateYYYYMM=�5�   ,   $* 7��6H   /   Mask$/ yyyy/MM��6W   ( 5�      �� *作成年月+0 �� StringFormat$0 yyyy/MM��$ TwoWay=� .   . $- PropertyChanged=�5�      � 5�   
   $��4��6   $��2��6!   $��5,0,0,0q�66   .+�5�      �� -   5        $��Kotei��5�      $��0��6&   $��計算期間の再計算��63   $��15(�6F   $��	160,4,0,0q�6T   $	��150��6g   |�.  1 d� 	IsChecked1 5�       �� *計算期間の再計算+$ TwoWay=� .   . 5�      � 5�   
   $��9��6   $��2��6   $��0,10,0,0q�60   .+�5�      /	 	 )GrapeCity.Windows.SpreadGrid.GcSpreadGrid	  -   5        $��sp請求データ一覧��5�      $��1��5�      $��0��6.   2 	  AutoGenerateColumns2 .  3 	  
LayoutMode$
3 Cell=�5�   )   4 	  ColumnDragMode$4 SelectThenDrag=�6;   5 	  DocumentUri$K5 E/KyoeiSystem.Application.Windows;component/views/TKS/TKS05010G1.sgxml?�5�      6 	  RowCount$6 0��6l   7 	  ItemsSource7 5�      �� *請求書一覧データ+$ TwoWay=� .   . $- PropertyChanged=�8 	  Columns8 5�      )
 	 #GrapeCity.Windows.SpreadGrid.Column
  5�      9 
  Locked$
9 TrueX�6    : 
  	DataField: 5�      3 	 -GrapeCity.Windows.SpreadGrid.BindingDataField  5�      ;   Binding; 62   �� *得意先ID+5�      
  5�      $
9 TrueX�6    : 5�        5�      ; 62   �� *得意先名+5�      
  5�      $
9 TrueX�6    : 5�        5�      ; 62   �� *得意先締日+5�      
  5�      < 
  	Focusable$< FalseX�6    = 
  CellType= 5�      1 	 +GrapeCity.Windows.SpreadGrid.ButtonCellType  5�      >   Content$> 	クリア��60   5�      
  5�      $
9 TrueX�6    : 5�        5�      ; 62   �� *新規区分+5�      
  5�      ? 
  HorizontalAlignment$? RightX�6    = 5�      / 	 )GrapeCity.Windows.SpreadGrid.TextCellType  5      @   	MaxLength$@ 10��6.   A   MaxLengthUnit$
A Byte=�6=   B   ImeState$	B Off=�6R   : 5       5     ; 62   �� *str開始日付1+5     
  5     $? RightX�6    = 5       5     $@ 10��6.   $
A Byte=�6=   $	B Off=�6R   : 5
       5     ; 62   �� *str終了日付1+5     
  5     $? RightX�6    = 5       5     $@ 10��6.   $
A Byte=�6=   $	B Off=�6R   : 5       5     ; 62   �� *str開始日付2+5     
  5     $? RightX�6    = 5       5     $@ 10��6.   $
A Byte=�6=   $	B Off=�6R   : 5       5     ; 62   �� *str終了日付2+5     
  5     $? RightX�6    = 5       5      $@ 10��6.   $
A Byte=�6=   $	B Off=�6R   : 5"       5#     ; 62   �� *str開始日付3+5%     
  5&     $? RightX�6    = 5'       5(     $@ 10��6.   $
A Byte=�6=   $	B Off=�6R   : 5*       5+     ; 62   �� *str終了日付3+5-     
  5.     $
9 TrueX�6    C 
  	IsVisibleC .  : 5/       50     ; 62   �� *strクリア開始日付+52     
  53     $
9 TrueX�6    C .  : 54       55     ; 62   �� *strクリア終了日付+57     5:     5<     5=     5>     