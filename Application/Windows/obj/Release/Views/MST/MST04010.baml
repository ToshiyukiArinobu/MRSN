   M S B A M L   `   `   `  ���� X  TKyoeiSystem.Framework.Windows, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null�[clr-namespace:KyoeiSystem.Framework.Windows.ViewBase;assembly=KyoeiSystem.Framework.Windows&KyoeiSystem.Framework.Windows.ViewBase  �[clr-namespace:KyoeiSystem.Framework.Windows.ViewBase;assembly=KyoeiSystem.Framework.Windows&KyoeiSystem.Framework.Windows.ViewBase  a ]KyoeiSystem.Framework.Windows.Controls, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null�dclr-namespace:KyoeiSystem.Framework.Windows.Controls;assembly=KyoeiSystem.Framework.Windows.Controls&KyoeiSystem.Framework.Windows.Controls �dclr-namespace:KyoeiSystem.Framework.Windows.Controls;assembly=KyoeiSystem.Framework.Windows.Controls&KyoeiSystem.Framework.Windows.Controls O Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem R NSystem.Data, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089>.clr-namespace:System.Data;assembly=System.DataSystem.Data # KyoeiSystem.Application.Windows4   .KyoeiSystem.Application.Windows.Views.MST04010   bfw[clr-namespace:KyoeiSystem.Framework.Windows.ViewBase;assembly=KyoeiSystem.Framework.Windows  \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W	 SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     	 R
 NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml 
   	 Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006 
   	 @d2http://schemas.microsoft.com/expression/blend/2008 
   	 jvm[clr-namespace:KyoeiSystem.Framework.Windows.ViewBase;assembly=KyoeiSystem.Framework.Windows 
   	 uctlsdclr-namespace:KyoeiSystem.Framework.Windows.Controls;assembly=KyoeiSystem.Framework.Windows.Controls 
   	 yControlsdclr-namespace:KyoeiSystem.Framework.Windows.Controls;assembly=KyoeiSystem.Framework.Windows.Controls 
   	 9System&clr-namespace:System;assembly=mscorlib 
   	 ?Data.clr-namespace:System.Data;assembly=System.Data 
   	 -     � AllowsTransparency  .   � WindowStartupLocation$ CenterOwner=�$	��728��$
��1024��$	��768��$
��1024�� � Title$. (商品中分類マスタ保守(MST04010)��.��(� $
��auto��$
��auto�� (� Stretch$
 Fill=� (� StretchDirection$
 Both=�.���  � RowDefinitions �� $
��auto ��� $	��650 �.+�+  %System.Windows.Controls.Ribbon.Ribbon  $��Stretch=�$��2��$��Stretch=�.@�.  (System.Windows.Controls.Ribbon.RibbonTab  $��	ホーム��   KeyTip$ h��$��75��.@�0  *System.Windows.Controls.Ribbon.RibbonGroup  $��90��  $��F1��   KeyTip$ MASTER��   RibbonFunctionBackground#��[  .@�1  +System.Windows.Controls.Ribbon.RibbonButton  -      LargeImageSource$% ../Images/15マスタ照会.png��$��0,-1,0,0q�	   ToolTipTitle$	 マスタ照会(F1)��
   ToolTipDescription$3
 -入力済みのデータを検索します。��   ToolTipImageSource$% ../Images/15マスタ照会.png��   Label$ ﾏｽﾀ照会��   KeyTip$ F1��$��50��  $��F2��$ MASTER��#��[  .@�  $y�Hidden=�$  ��$	  ��$
  ��  $��F3��$ MASTER��#��[  .@�  $y�Hidden=�$  ��$	  ��$
  ��  $��F4��$ MASTER��#��[  .@�  $y�Hidden=�$  ��$	  ��$
  ��  $��F5��$ MASTER��#��[  .@�  $y�Hidden=�$  ��$	  ��$
  ��  $��F6��$ MASTER��#��[  .@�  $y�Hidden=�$  ��$	  ��$
  ��  $��F7��$ MASTER��#��[  .@�  $y�Hidden=�$  ��$	  ��$
  ��  $��F8��$ MASTER��#��[  .@�  -   $ ../Images/22リスト.png��$ F8��$!
 リストを表示します��$ ../Images/22リスト.png��$ 	リスト��$	 リスト一覧(F8)��$y�Hidden=�  $��F9��$ MASTER��#��[  .@�  -   $ ../Images/24登録.png��$ F9��$��50��$-
 '入力データの登録を行います��$ ../Images/24登録.png��$ 登録��$	 データ登録(F9)��y���  �� Path$ DataUpdateVisible� �� Mode$ TwoWay=� �� NotifyOnSourceUpdated .  �� NotifyOnTargetUpdated .   $	��F10��$ MASTER��#��[  .@�  -   $ ../Images/26クリア.png��$	 F10��$��50��$'
 !入力の取消しを行います��$ ../Images/26クリア.png��$ 入力取消��$	 入力取消(F10)��  $	��F11��$ MASTER��#��[  .@�  -   $ ../Images/27終了.png��$	 F11��$��50��$
 終了します��$ ../Images/27終了.png��$ 終了��$	 終了(F11)��  -   $��RibbonDelete��$	��F12��$ MASTER��#��[  .@�  -   $ ../Images/28削除.png��$	 F12��$��50��$
 入力を削除します��$ ../Images/28削除.png��$ 削除��$	 削除(F12)��y��� $ DataUpdateVisible�$ TwoWay=� .  .   $��	ヘルプ��
�������.@�  $��サポート��.@�  -	   $% ../Images/30マニュアル.png��$ MANUAL��$ ﾏﾆｭｱﾙ��$	 取扱説明書��$*
 $取り扱い説明書を開きます��  -
   $( "../Images/29遠隔サポート.png��$ SUPPORT��$ お問合せ��$	 お問合せ��$<
 6Web上の｢お問合せ｣ページに遷移します��  -   $ ../Images/QandA.png��$	 FAQ��$ QandA��$	 QandA��$E
 ?Web上の｢よくあるご質問｣ページに遷移します��  -   $( "../Images/13共栄Webリンク.png��$ KYOEI��$ 共栄ｼｽﾃﾑ��$	 共栄システムHP��$<
 6共栄システムのホームページを開きます��  $��実施状況入力��
�������   ContextualTabGroupHeader$ 入力補助��.@�  $'��!従来ファンクションキー��.@�  $% ../Images/16マスタ入力.png��$ d��$ 	行追加��$	 行追加(F5)��$'
 !売上の履歴を表示します��$% ../Images/16マスタ入力.png��  -   $ ../Images/24登録.png��$ e��$ マスタ登録��$	 伝票登録(F9)��$$
 伝票の登録を行います��$ ../Images/24登録.png��  -   $ ../Images/26クリア.png��$ f��$ 入力取消し��$	 入力取消(F12)��$'
 !入力の取消しを行います��$ ../Images/26クリア.png��  -   $ ../Images/27終了.png��$ g��$ 終了��$	 入力取消(F11)��$
 終了します��$ ../Images/27終了.png��  $��一括入力��
�������$ 入力補助��.@�  $'��!従来ファンクションキー��.@�  $ ../Images/22リスト.png��$ d��$ プレビュー��$	 プレビュー(F5)��$'
 !売上の履歴を表示します��$ ../Images/22リスト.png��  -   $ ../Images/24登録.png��$ e��$ 印刷��$	 
印刷(F9)��$$
 伝票の登録を行います��$ ../Images/24登録.png��  -   $ ../Images/24登録.png��$ e��$ マスタ登録��$	 マスタ登録(F9)��$$
 伝票の登録を行います��$ ../Images/24登録.png��  -   $ ../Images/26クリア.png��$ f��$ 入力取消し��$	 入力取消(F12)��$'
 !入力の取消しを行います��$ ../Images/26クリア.png��  -   $ ../Images/27終了.png��$ g��$
 終了します��$ ../Images/27終了.png��$ 終了��$	 入力取消(F11)��� $��0��$��Bottom=� �� $��74 � � ColumnDefinitions �� $��90 ��� $��* �.+�]� $��0��$��0��$��Stretch=�$��2q����� *MaintenanceModeBackground+.+��� $��メイリオ)�$��24(�$��Medium"�$��Stretch=�$��Center=�$��Center=�$��Center=����� *MaintenanceMode+���� *MaintenanceModeForeground+�� $��0��$��1��$��Bottom=�
��� ���
���  ��$��2,0,2,0q����� *ErrorMessage+y��� *ErrorMessageVisibility+�� $��1�����#2792c3$��1q�$��5q�
�������._�� $��15q� �� $��30 ��� $��30 ��� $��30 ��� $��* �.+�?  9KyoeiSystem.Framework.Windows.Controls.UcLabelTwinTextBox  -   $��txtMajerTypeCode��$��0��=  7KyoeiSystem.Framework.Windows.Controls.FrameworkControl   	IsKeyItem .    
IsRequired . $
��Left=�$��0,0,0,2q�   DataAccessName$ M12_DAI��   DataAccessMode$ On=�   MasterCheckEnabled .    Label1Visibility$ 	Collapsed=�   Label2Visibility$ 	Collapsed=�   Label_Context$ 大分類コード��   Label_Width$	 100Q�   
Text1Width$ 30Q�   Text1MaxLength$ 1��   Text1IMEType$	 Off=�    Text1ValidationType$  Integer=�$!   Text1VerticalContentAlignment$! Center=�"   
Text2Width$	" 250Q�#   Text2IsReadOnly# . $   Text2BorderThickness$$ 0q�%   Text2Background
% ���� $&   Text2VerticalContentAlignment$& Center=�'   Text1' �� *大分類コード+$ TwoWay=� .  . (   Text2( �� *大分類名+$ TwoWay=� .  . � $��1�� �� $
��auto ��� $
��auto ��� $
��auto ��� $
��auto ��� $
��auto �.+�  -   $��txtMediumClassCode��$��0�� .  .  $
��Left=�$��0,2,0,2q�$ 中分類コード��$	 100Q�$ M13_CHU��$ On=� .  )   Text2Visibility$) 	Collapsed=�$ 	Collapsed=�$ 	Collapsed=�$ 30Q�$ 5��$	 Off=�$  Integer=�$! Center=�' �� *中分類コード+$ TwoWay=� .  . *   LinkItem* �� *大分類コード+$ TwoWay=� .  . �� -   $��FistIdButton��$��1��$	��≪��$��20,2,3,2q�$��30��|�.  �� -   $��PrevIdButton��$��2��$	��＜��$��0,2,3,2q�$��30��|�.  �� -   $��NextIdButton��$��3��$	��＞��$��0,2,3,2q�$��30��|�.  �� -   $��LastIdButoon��$��4��$	��≫��$��0,2,3,2q�$��30��|�.  ;  5KyoeiSystem.Framework.Windows.Controls.UcLabelTextBox  -   $��txtMediumClassName��$��2��+   ImeType$+ Native=�$
��Left=�$��0,2,0,0q�,   Label_Context$, 中分類名��-   Label_Width$	- 100Q�.   cWidth$	. 230Q�/   
cMaxLength$/ 20��0   cValidationType$
0 None=�1   Text1 �� *中分類名+$ TwoWay=� .  . 2 �� UpdateSourceTrigger$2 PropertyChanged=�