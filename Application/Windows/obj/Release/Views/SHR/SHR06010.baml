   M S B A M L   `   `   `  ���� X  TKyoeiSystem.Framework.Windows, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null�[clr-namespace:KyoeiSystem.Framework.Windows.ViewBase;assembly=KyoeiSystem.Framework.Windows&KyoeiSystem.Framework.Windows.ViewBase  �[clr-namespace:KyoeiSystem.Framework.Windows.ViewBase;assembly=KyoeiSystem.Framework.Windows&KyoeiSystem.Framework.Windows.ViewBase  a ]KyoeiSystem.Framework.Windows.Controls, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null�dclr-namespace:KyoeiSystem.Framework.Windows.Controls;assembly=KyoeiSystem.Framework.Windows.Controls&KyoeiSystem.Framework.Windows.Controls Z VKyoeiSystem.Application.Windows, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]3clr-namespace:KyoeiSystem.Application.Windows.Views%KyoeiSystem.Application.Windows.Views �dclr-namespace:KyoeiSystem.Framework.Windows.Controls;assembly=KyoeiSystem.Framework.Windows.Controls&KyoeiSystem.Framework.Windows.Controls O Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem 4   .KyoeiSystem.Application.Windows.Views.SHR06010   bfw[clr-namespace:KyoeiSystem.Framework.Windows.ViewBase;assembly=KyoeiSystem.Framework.Windows  \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      R	 NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml 	    Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006 	    @d2http://schemas.microsoft.com/expression/blend/2008 	    jvm[clr-namespace:KyoeiSystem.Framework.Windows.ViewBase;assembly=KyoeiSystem.Framework.Windows 	    uctlsdclr-namespace:KyoeiSystem.Framework.Windows.Controls;assembly=KyoeiSystem.Framework.Windows.Controls 	    Buc3clr-namespace:KyoeiSystem.Application.Windows.Views 	    yControlsdclr-namespace:KyoeiSystem.Framework.Windows.Controls;assembly=KyoeiSystem.Framework.Windows.Controls 	    9System&clr-namespace:System;assembly=mscorlib 	    e
 aGrapeCity.WPF.SpreadGrid, Version=1.0.2015.1109, Culture=neutral, PublicKeyToken=5c5c8ff7a6858ccc=sg4http://schemas.grapecity.com/windows/spreadgrid/2012 
 -   $��
MainWindow��  � WindowStyle$  SingleBorderWindow=� � AllowsTransparency .   � Left$ 0��
 � Top$ 0��$	��728��$
��1024��$	��768��$
��1024�� � Title$% 出金予定実績表(SHR06010)�� � WindowStartupLocation$ CenterOwner=�.��(� $
��auto��$
��auto�� (� Stretch$
 Fill=� (� StretchDirection$
 Both=�.���  � ColumnDefinitions �� $
��1024 �	 � RowDefinitions	 �� $
��auto ��� $	��600 �.+�+  %System.Windows.Controls.Ribbon.Ribbon  $��Stretch=�$��2��$��Stretch=�
   ApplicationMenu
 :  4System.Windows.Controls.Ribbon.RibbonApplicationMenu  5  /System.Windows.Controls.Ribbon.RibbonMenuButton   SmallImageSource$ ../Images/Menu.png��.@�>  8System.Windows.Controls.Ribbon.RibbonApplicationMenuItem  $��マスタ照会��3  -System.Windows.Controls.Ribbon.RibbonMenuItem   ImageSource$% ../Images/15マスタ照会.png��  $��マスタ入力��$% ../Images/16マスタ入力.png��C  =System.Windows.Controls.Ribbon.RibbonApplicationSplitMenuItem  $��印刷��$ ../Images/23印刷.png��.@�  $��印刷��$ ../Images/23印刷.png��  $��ページ設定��$ ../Images/22リスト.png��  $��印刷プレビュー��$ ../Images/22リスト.png��  $��終了��$ ../Images/LogOut.bmp��.@�.  (System.Windows.Controls.Ribbon.RibbonTab  $��	ホーム��   KeyTip$ h��$��75��.@�0  *System.Windows.Controls.Ribbon.RibbonGroup  $��90��  $��F1��   KeyTip$ MASTER��   RibbonFunctionBackground#��[  .@�1	  +System.Windows.Controls.Ribbon.RibbonButton	  -    	  Label$ ﾏｽﾀ照会�� 	  KeyTip$ F1��$��50�� 	  LargeImageSource$% ../Images/15マスタ照会.png�� 	  ToolTipTitle$ マスタ照会(F1)�� 	  ToolTipDescription$3 -入力済みのデータを検索します。�� 	  ToolTipImageSource$% ../Images/15マスタ照会.png��  $��F2��$ MASTER��#��[  .@�	  $y�Hidden=�$  ��$  ��$  ��  $��F3��$ MASTER��#��[  .@�	  $y�Hidden=�$  ��$  ��$  ��  $��F4��$ MASTER��#��[  .@�	  $y�Hidden=�$  ��$  ��$  ��  $��F5��$ MASTER��#��[  .@�	  -   $ 	CSV出力��$ F5��$��50��$ ../Images/19CSV出力.png��  $��F6��$ MASTER��#��[  .@�	  $y�Hidden=�$  ��$  ��$  ��  $��F7��$ MASTER��#��[  .@�	  $y�Hidden=�$  ��$  ��$  ��  $��F8��$ MASTER��#��[  .@�	  -   $ 印刷��$ F8��$��50��$ ../Images/23印刷.png��$ 
印刷(F8)��$ 印刷します��$ ../Images/23印刷.png��  $��F9��$ MASTER��#��[  .@�	  $y�Hidden=�$  ��$  ��$  ��  $	��F10��$ MASTER��#��[  .@�	  $y�Hidden=�$  ��$  ��$  ��  $	��F11��$ MASTER��#��[  .@�	  -   $ 終了��$	 F11��$��50��$ ../Images/27終了.png��$ 終了(F11)��$ 終了します��$ ../Images/27終了.png��  $	��F12��$ MASTER��#��[  .@�	  $y�Hidden=�$  ��$  ��$  ��  $��	ヘルプ��
�������.@�  $��サポート��.@�	  -   $% ../Images/30マニュアル.png��$ MANUAL��$ ﾏﾆｭｱﾙ��$ 取扱説明書��$* $取り扱い説明書を開きます��	  -   $( "../Images/29遠隔サポート.png��$ SUPPORT��$ お問合せ��$ お問合せ��$< 6Web上の｢お問合せ｣ページに遷移します��	  -   $ ../Images/QandA.png��$	 FAQ��$ QandA��$ QandA��$E ?Web上の｢よくあるご質問｣ページに遷移します��	  -	   $( "../Images/13共栄Webリンク.png��$ KYOEI��$ 共栄ｼｽﾃﾑ��$ 共栄システムHP��$< 6共栄システムのホームページを開きます���� $��0��$��Bottom=�
��� ���
���  ������ *ErrorMessage+y��� *ErrorMessageVisibility+�� $��1��$��5q�
���   �$��1q�
�������._�� $��5q�	 �� $
��auto ��� $
��auto ��� $
��auto ��� $
��auto ��� $
��auto ��� $
��auto �.+�?
  9KyoeiSystem.Framework.Windows.Controls.UcLabelTwinTextBox
  -
   $��	myCompany��$��0��$
��Left=�$��0,3,0,0q� 
  DataAccessName$ M70_JIS�� 
  DataAccessMode$ On=� 
  
IsRequired . =  7KyoeiSystem.Framework.Windows.Controls.FrameworkControl   	IsKeyItem .  
  Label1Visibility$ 	Collapsed=� 
  Label2Visibility$ 	Collapsed=� 
  Label_Context$ 自社コード�� 
  Label_Width$	 100Q� 
  
Text1Width$ 40Q� 
  Text1MaxLength$ 3�� 
  Text1IMEType$	 Off=�$  
  Text1VerticalContentAlignment$  Center=�! 
  
Text2Width$	! 250Q�" 
  Text2IsReadOnly" . # 
  Text2Background
# ���� $ 
  Text2BorderThickness$$ 0q�% 
  Text2AcceptsTab% .  $& 
  Text2VerticalContentAlignment$& Center=�;  5KyoeiSystem.Framework.Windows.Controls.UcLabelTextBox  -   $��PaymentYearMonth��$��1�� . '   
IsRequired' . $��0,3,0,0q�$
��Left=�(   ValidationType$( 
DateYYYYMM=�)   Mask$) yyyy/MM��*   Label_Context$* 支払年月��+   Label_Width$	+ 100Q�,   cWidth$, 60Q�-   
cMaxLength$- 7�� .   cVerticalContentAlignment$. Center=�� $��2��$��0,3,0,0q� �� $
��auto ��� $
��auto ��� $��* �.+�  -   $��
PaymentDay��$
��Left=�/   MinValue$/ 1��0   MaxValue$0 31��$( Integer=�$* 支払締日��$	+ 100Q�$, 30Q�$- 2��$. Center=��� $��1��$��末日は31を設定��$
��Left=��� -   $��isPaymentAllDays��$��2��$��10,0,0,0q�$
��Left=�$��Center=�$��全支払締日��1 d� 	IsChecked$
1 TrueY�;  5KyoeiSystem.Application.Windows.Views.M01_TOK_TextBox  -   2  Name$2 Customer��$��3��$��2�� .  $
��Left=�$��0,3,0,0q�3   DataAccessMode$3 On=�4   DataAccessName$4 M01_TOK��5   LinkItem$	5 1,3��6   Label_Context$6 支払先指定��7   Label_Width$	7 100Q�8   Label1Visibility$8 	Collapsed=�9   Label2Visibility$9 	Collapsed=�:   
Text1Width$: 40Q�;   Text1MaxLength$; 4��<   Text1IMEType$	< Off=�$=   Text1VerticalContentAlignment$= Center=�>   
Label1Text$	> －��&?   Label1VerticalConteintAlignment$? Center=�@   
Text2Width$@ 30Q�A   Text2MaxLength$A 2��B   Text2IMEType$	B Off=�$C   Text2VerticalContentAlignment$C Center=�D   
Label2Text$D  ��<  6KyoeiSystem.Framework.Windows.Controls.UcLabelComboBox  -   $��
CreateType��$��4��$
��Left=�$��0,3,0,0q�E   Label_Context$E 作成区分��F   Label_Width$	F 100Q�$	��300��G   ComboListingParams$,G &支払先,出金予定表,作成区分��