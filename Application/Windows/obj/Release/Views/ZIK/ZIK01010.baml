   M S B A M L   `   `   `  ���� X  TKyoeiSystem.Framework.Windows, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null�[clr-namespace:KyoeiSystem.Framework.Windows.ViewBase;assembly=KyoeiSystem.Framework.Windows&KyoeiSystem.Framework.Windows.ViewBase  �[clr-namespace:KyoeiSystem.Framework.Windows.ViewBase;assembly=KyoeiSystem.Framework.Windows&KyoeiSystem.Framework.Windows.ViewBase  a ]KyoeiSystem.Framework.Windows.Controls, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null�dclr-namespace:KyoeiSystem.Framework.Windows.Controls;assembly=KyoeiSystem.Framework.Windows.Controls&KyoeiSystem.Framework.Windows.Controls Z VKyoeiSystem.Application.Windows, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]3clr-namespace:KyoeiSystem.Application.Windows.Views%KyoeiSystem.Application.Windows.Views 4   .KyoeiSystem.Application.Windows.Views.ZIK01010   bfw[clr-namespace:KyoeiSystem.Framework.Windows.ViewBase;assembly=KyoeiSystem.Framework.Windows  \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     jvm[clr-namespace:KyoeiSystem.Framework.Windows.ViewBase;assembly=KyoeiSystem.Framework.Windows     uctlsdclr-namespace:KyoeiSystem.Framework.Windows.Controls;assembly=KyoeiSystem.Framework.Windows.Controls     Buc3clr-namespace:KyoeiSystem.Application.Windows.Views     e	 aGrapeCity.WPF.SpreadGrid, Version=1.0.2015.1109, Culture=neutral, PublicKeyToken=5c5c8ff7a6858ccc=sg4http://schemas.grapecity.com/windows/spreadgrid/2012 	 -   $��Window��  � WindowStyle$  SingleBorderWindow=� � AllowsTransparency .  $	��728��$
��1024��$	��768��$
��1024�� � WindowStartupLocation$ CenterOwner=� � Language$ ja-JP� � Title$" 月次在庫集計(ZIK01010)�� � 	Resources <  6KyoeiSystem.Framework.Windows.Controls.BooleanInverter   boolInverter��%   &	          .��� $��5q� � RowDefinitions �� $
��auto ��� $��* �.+�+  %System.Windows.Controls.Ribbon.Ribbon  $��Stretch=�$��2��$��Stretch=�.@�.  (System.Windows.Controls.Ribbon.RibbonTab  $��	ホーム��   KeyTip$ h��$��75��.@�0  *System.Windows.Controls.Ribbon.RibbonGroup  $��90��  $��F1��   KeyTip$ MASTER��  RibbonFunctionBackground#��[ .@�1  +System.Windows.Controls.Ribbon.RibbonButton  $y�Hidden=�	   Label$	  ��
   ToolTipTitle$
  ��   ToolTipDescription$  ��  $��F2��$ MASTER��#��[ .@�  $y�Hidden=�$	  ��$
  ��$  ��  $��F3��$ MASTER��#��[ .@�  $y�Hidden=�$	  ��$
  ��$  ��  $��F4��$ MASTER��#��[ .@�  $y�Hidden=�$	  ��$
  ��$  ��  $��F5��$ MASTER��#��[ .@�  $y�Hidden=�$	  ��$
  ��$  ��  $��F6��$ MASTER��#��[ .@�  $y�Hidden=�$	  ��$
  ��$  ��  $��F7��$ MASTER��#��[ .@�  $y�Hidden=�$	  ��$
  ��$  ��  $��F8��$ MASTER��#��[ .@�  -      LargeImageSource$ ../Images/23印刷.png��   KeyTip$ F8��$	 印刷��$
 
印刷(F8)��$ 印刷を開始します��   ToolTipImageSource$ ../Images/23印刷.png��  $��F9��$ MASTER��#��[ .@�  -   $" ../Images/25集計開始.png��$ F9��$��50��$	 集計開始��$
 集計開始(F9)��$! 集計処理を行います��$" ../Images/25集計開始.png��  $	��F10��$ MASTER��#��[ .@�  $y�Hidden=�$	  ��$
  ��$  ��  $	��F11��$ MASTER��#��[ .@�  -   $ ../Images/27終了.png��$	 F11��$��50��$	 終了��$
 終了(F11)��$ 終了します��$ ../Images/27終了.png��  $	��F12��$ MASTER��#��[ .@�  $y�Hidden=�$	  ��$
  ��$  ��  $��	ヘルプ��
�������.@�  $��サポート��.@�  -   $% ../Images/30マニュアル.png��$ MANUAL��$	 ﾏﾆｭｱﾙ��$
 取扱説明書��$* $取り扱い説明書を開きます��  -   $( "../Images/29遠隔サポート.png��$ SUPPORT��$	 お問合せ��$
 お問合せ��$< 6Web上の｢お問合せ｣ページに遷移します��  -   $ ../Images/QandA.png��$	 FAQ��$	 QandA��$
 QandA��$E ?Web上の｢よくあるご質問｣ページに遷移します��  -   $( "../Images/13共栄Webリンク.png��$ KYOEI��$	 共栄ｼｽﾃﾑ��$
 共栄システムHP��$< 6共栄システムのホームページを開きます��� $��0��$��2��$��Bottom=� �� $��74 � � ColumnDefinitions �� $��90 ��� $��* �.+�]� $��0��$��0��$��Stretch=�$��2q����� *MaintenanceModeBackground+.+��� $��メイリオ)�$��24(�$��Medium"�$��Stretch=�$��Center=�$��Center=�$��Center=����� *MaintenanceMode+���� *MaintenanceModeForeground+�� $��0��$��1��$��Bottom=�
��� ���
���  ��$��2,0,2,0q����� *ErrorMessage+y��� *ErrorMessageVisibility+�� $��1��
�������$
��auto��.+�� $��5q�.+�;  5KyoeiSystem.Framework.Windows.Controls.UcLabelTextBox  -	   $��CreateYearMonth��$
��Left=�   
IsRequired . $��5,5,0,0q�   ValidationType$ 
DateYYYYMM=�   ImeType$	 Off=�   Mask$ yyyy/MM��   Label_Context$ 集計年月��   Label_Width$	 100Q�   
cMaxLength$ 7��   cWidth$ 60Q�    cVerticalContentAlignment$ Center=�