   M S B A M L   `   `   `  ���� X  TKyoeiSystem.Framework.Windows, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null�[clr-namespace:KyoeiSystem.Framework.Windows.ViewBase;assembly=KyoeiSystem.Framework.Windows&KyoeiSystem.Framework.Windows.ViewBase  �[clr-namespace:KyoeiSystem.Framework.Windows.ViewBase;assembly=KyoeiSystem.Framework.Windows&KyoeiSystem.Framework.Windows.ViewBase  a ]KyoeiSystem.Framework.Windows.Controls, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null�dclr-namespace:KyoeiSystem.Framework.Windows.Controls;assembly=KyoeiSystem.Framework.Windows.Controls&KyoeiSystem.Framework.Windows.Controls R NSystem.Data, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089>.clr-namespace:System.Data;assembly=System.DataSystem.Data # KyoeiSystem.Application.Windows6   0KyoeiSystem.Application.Windows.Views.MST01020_1   bfw[clr-namespace:KyoeiSystem.Framework.Windows.ViewBase;assembly=KyoeiSystem.Framework.Windows  \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      R	 NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml 	    Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006 	    @d2http://schemas.microsoft.com/expression/blend/2008 	    jvm[clr-namespace:KyoeiSystem.Framework.Windows.ViewBase;assembly=KyoeiSystem.Framework.Windows 	    uctlsdclr-namespace:KyoeiSystem.Framework.Windows.Controls;assembly=KyoeiSystem.Framework.Windows.Controls 	    ?Data.clr-namespace:System.Data;assembly=System.Data 	    -     � AllowsTransparency  .   � Title$- '出荷先マスタリスト(MST01020_1)��$	��350��$
��1024��$	��350��$
��1024�� � WindowStartupLocation$ CenterOwner=� � Left$ 0��
 � Top$ 0��.��� $��5q� � RowDefinitions �� $
��auto ��� $
��auto ��� $��* �.+�+  %System.Windows.Controls.Ribbon.Ribbon  $��Stretch=�$��2��$��Stretch=�   ApplicationMenu :  4System.Windows.Controls.Ribbon.RibbonApplicationMenu  5  /System.Windows.Controls.Ribbon.RibbonMenuButton   SmallImageSource$ ../Images/Menu.png��.@�>  8System.Windows.Controls.Ribbon.RibbonApplicationMenuItem  $��マスタ照会��3  -System.Windows.Controls.Ribbon.RibbonMenuItem   ImageSource$% ../Images/15マスタ照会.png��  $��マスタ入力��$% ../Images/16マスタ入力.png��C  =System.Windows.Controls.Ribbon.RibbonApplicationSplitMenuItem  $��印刷��$ ../Images/23印刷.png��.@�  $��印刷��$ ../Images/23印刷.png��  $��ページ設定��$ ../Images/22リスト.png��  $��印刷プレビュー��$ ../Images/22リスト.png��  $��終了��$ ../Images/LogOut.bmp��.@�.  (System.Windows.Controls.Ribbon.RibbonTab  $��	ホーム��	   KeyTip$	 h��$��75��.@�0  *System.Windows.Controls.Ribbon.RibbonGroup  $��90��  $��F1��
   KeyTip$
 MASTER��   RibbonFunctionBackground#��[  .@�1	  +System.Windows.Controls.Ribbon.RibbonButton	  -    	  LargeImageSource$% ../Images/15マスタ照会.png��$��0,0,0,0q� 	  ToolTipTitle$ マスタ照会(F1)�� 	  ToolTipDescription$3 -入力済みのデータを検索します。�� 	  ToolTipImageSource$% ../Images/15マスタ照会.png�� 	  Label$ ﾏｽﾀ照会�� 	  KeyTip$ F1��$��50��  $��F2��$
 MASTER��#��[  .@�	  -   $y�Hidden=�$% ../Images/16マスタ入力.png��$ 入力��$ F2��$ マスタ入力(F2)��$- 'マスタの入力画面を開きます��$% ../Images/16マスタ入力.png��  $��F3��$
 MASTER��#��[  .@�	  $y�Hidden=�$  ��$  ��$  ��  $��F4��$
 MASTER��#��[  .@�	  $y�Hidden=�$  ��$  ��$  ��  $��F5��$
 MASTER��#��[  .@�	  -   $ ../Images/19CSV出力.png��$ 	CSV出力��$ F5��$��50��  $��F6��$
 MASTER��#��[  .@�	  $y�Hidden=�$  ��$  ��$  ��  $��F7��$
 MASTER��#��[  .@�	  $y�Hidden=�$  ��$  ��$  ��  $��F8��$
 MASTER��#��[  .@�	  -   $ ../Images/23印刷.png��$ F8��$��50��$ 印刷��$ 
印刷(F8)��$ 印刷します��$ ../Images/23印刷.png��  $��F9��$
 MASTER��#��[  .@�	  -   $y�Hidden=�$ ../Images/24登録.png��$ F9��$ 登録��$ データ登録(F9)��$- '入力データの登録を行います��$ ../Images/24登録.png��  $	��F10��$
 MASTER��#��[  .@�	  -   $y�Hidden=�$ ../Images/26クリア.png��$	 F10��$ 入力取消��$ 入力取消(F10)��$' !入力の取消しを行います��$ ../Images/26クリア.png��  $	��F11��$
 MASTER��#��[  .@�	  -   $ ../Images/27終了.png��$	 F11��$��50��$ 終了��$ 終了(F11)��$ 終了します��$ ../Images/27終了.png��  $	��F12��$
 MASTER��#��[  .@�	  -	   $y�Hidden=�$ ../Images/28削除.png��$	 F12��$ 削除��$ 削除(F12)��$ 入力を削除します��$ ../Images/28削除.png��  $��	ヘルプ��
�������$��0,10,0,8q�.@�  $��サポート��.@�	  -
   $% ../Images/30マニュアル.png��$ MANUAL��$ ﾏﾆｭｱﾙ��$ 取扱説明書��$* $取り扱い説明書を開きます��	  -   $( "../Images/29遠隔サポート.png��$ SUPPORT��$ お問合せ��$ お問合せ��$< 6Web上の｢お問合せ｣ページに遷移します��	  -   $ ../Images/QandA.png��$	 FAQ��$ QandA��$ QandA��$E ?Web上の｢よくあるご質問｣ページに遷移します��	  -   $( "../Images/13共栄Webリンク.png��$ KYOEI��$ 共栄ｼｽﾃﾑ��$ 共栄システムHP��$< 6共栄システムのホームページを開きます���� $��0��$��Bottom=�
��� ���
���  ������ *ErrorMessage+y��� *ErrorMessageVisibility+�� $��1�����#2792c3$��1q�$��5q�
�������._�� $��5q� �� $��30 ��� $��3 ��� $��30 ��� $��3 ��� $��30 � � ColumnDefinitions �� $
��auto ��� $��* �.+�?
  9KyoeiSystem.Framework.Windows.Controls.UcLabelTwinTextBox
   
  Label_Context$ 	コード��$��0�� 
  Label_Width$	 100Q�$��5,0,0,0q� 
  Text2Visibility$ 	Collapsed=� 
  
Label1Text$	 ～�� 
  Label2Visibility$ 	Collapsed=� 
  MasterCheckEnabled .   
  Text1MaxLength$ 15�� 
  Text1ValidationType$ ASCII=� 
  
Text1Width$	 100Q� 
  
Text2Width$	 100Q� 
  DataAccessName$ M21_SYUK�� 
  Text1 ��  �� Path$ 出荷先コードFROM� �� Mode$ TwoWay=�  �� NotifyOnSourceUpdated  . ! �� NotifyOnTargetUpdated! . 
  " 
  LabelVisibility$" 	Collapsed=�$��0��$	 100Q� .  $ 15��# 
  Label1Visibility$# 	Collapsed=�$ 	Collapsed=�$ 	Collapsed=�$	 100Q�$ M21_SYUK��$ ASCII=�$��2��$��
233,0,55,0q� �� *出荷先コードTO+$ TwoWay=�  . ! . ;  5KyoeiSystem.Framework.Windows.Controls.UcLabelTextBox  $   Label_Context$$$ ﾋﾟｯｸｱｯﾌﾟ指定��$��2��%   Label_Width$	% 100Q�$
��Left=�$��5,0,0,0q�&   cWidth$	& 500Q�'   
cMaxLength$' 50��(   Text( �� $ 出荷先名指定�$ TwoWay=�  . ! . C  =KyoeiSystem.Framework.Windows.Controls.UcLabelTextRadioButton  )   RadioViewCount$	) Two=�*   Label_Content$* 表示方法��+   Label_Width$	+ 100Q�,   RadioOne_Content$, コード順��-   RadioSecond_Content$- カナ読み順��$��4��.   RadioSelectButton$	. One=�$��5,0,0,0q�/   Text/ �� $ 表示方法�$ TwoWay=�  . ! . 