   M S B A M L   `   `   `  ���� X  TKyoeiSystem.Framework.Windows, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null�[clr-namespace:KyoeiSystem.Framework.Windows.ViewBase;assembly=KyoeiSystem.Framework.Windows&KyoeiSystem.Framework.Windows.ViewBase  �[clr-namespace:KyoeiSystem.Framework.Windows.ViewBase;assembly=KyoeiSystem.Framework.Windows&KyoeiSystem.Framework.Windows.ViewBase  a ]KyoeiSystem.Framework.Windows.Controls, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null�dclr-namespace:KyoeiSystem.Framework.Windows.Controls;assembly=KyoeiSystem.Framework.Windows.Controls&KyoeiSystem.Framework.Windows.Controls �dclr-namespace:KyoeiSystem.Framework.Windows.Controls;assembly=KyoeiSystem.Framework.Windows.Controls&KyoeiSystem.Framework.Windows.Controls O Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem R NSystem.Data, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089>.clr-namespace:System.Data;assembly=System.DataSystem.Data # KyoeiSystem.Application.Windows4   .KyoeiSystem.Application.Windows.Views.MST04022   bfw[clr-namespace:KyoeiSystem.Framework.Windows.ViewBase;assembly=KyoeiSystem.Framework.Windows  \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W	 SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     	 R
 NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml 
   	 Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006 
   	 @d2http://schemas.microsoft.com/expression/blend/2008 
   	 jvm[clr-namespace:KyoeiSystem.Framework.Windows.ViewBase;assembly=KyoeiSystem.Framework.Windows 
   	 uctlsdclr-namespace:KyoeiSystem.Framework.Windows.Controls;assembly=KyoeiSystem.Framework.Windows.Controls 
   	 yControlsdclr-namespace:KyoeiSystem.Framework.Windows.Controls;assembly=KyoeiSystem.Framework.Windows.Controls 
   	 9System&clr-namespace:System;assembly=mscorlib 
   	 Aim2http://schemas.grapecity.com/windows/2010/inputman 
   	 ?Data.clr-namespace:System.Data;assembly=System.Data 
   	 -     � AllowsTransparency  .   � Title$" 商品群マスタ(MST04022)��$	��728��$
��1024��$	��768��$
��1024�� � WindowStartupLocation$ CenterOwner=� � Left$ 0��
 � Top$ 0��.��(� $
��auto��$
��auto�� (� Stretch$
 Fill=� (� StretchDirection$
 Both=�.���  � RowDefinitions �� $
��auto ��� $	��650 �.+�+  %System.Windows.Controls.Ribbon.Ribbon  $��Stretch=�$��2��$��Stretch=�.@�.  (System.Windows.Controls.Ribbon.RibbonTab  $��	ホーム��   KeyTip$ h��$��75��.@�0  *System.Windows.Controls.Ribbon.RibbonGroup  $��90��  $��F1��	   KeyTip$	 MASTER��   RibbonFunctionBackground#��[  .@�1  +System.Windows.Controls.Ribbon.RibbonButton  -   
   LargeImageSource$%
 ../Images/15マスタ照会.png��$��0,-1,0,0q�   ToolTipTitle$ マスタ照会(F1)��   ToolTipDescription$3 -入力済みのデータを検索します。��   ToolTipImageSource$% ../Images/15マスタ照会.png��   Label$ ﾏｽﾀ照会��   KeyTip$ F1��$��50��  $��F2��$	 MASTER��#��[  .@�  $y�Hidden=�$  ��$  ��$  ��  $��F3��$	 MASTER��#��[  .@�  $y�Hidden=�$  ��$  ��$  ��  $��F4��$	 MASTER��#��[  .@�  $y�Hidden=�$  ��$  ��$  ��  $��F5��$	 MASTER��#��[  .@�  $y�Hidden=�$  ��$  ��$  ��  $��F6��$	 MASTER��#��[  .@�  $y�Hidden=�$  ��$  ��$  ��  $��F7��$	 MASTER��#��[  .@�  $y�Hidden=�$  ��$  ��$  ��  $��F8��$	 MASTER��#��[  .@�  -   $
 ../Images/22リスト.png��$ F8��$ 	リスト��$ リスト一覧(F8)��$! リストを表示します��$ ../Images/22リスト.png��  $��F9��$	 MASTER��#��[  .@�  -   $
 ../Images/24登録.png��$ F9��$��50��$ 登録��$ データ登録(F9)��$- '入力データの登録を行います��$ ../Images/24登録.png��y���  �� Path$ DataUpdateVisible� �� Mode$ TwoWay=� �� NotifyOnSourceUpdated .  �� NotifyOnTargetUpdated .   $	��F10��$	 MASTER��#��[  .@�  -   $
 ../Images/26クリア.png��$	 F10��$��50��$ 入力取消��$ 入力取消(F10)��$' !入力の取消しを行います��$ ../Images/26クリア.png��  $	��F11��$	 MASTER��#��[  .@�  -   $
 ../Images/27終了.png��$	 F11��$��50��$ 終了��$ 終了(F11)��$ 終了します��$ ../Images/27終了.png��  -   $��RibbonDelete��$	��F12��$	 MASTER��#��[  .@�  -   $
 ../Images/28削除.png��$	 F12��$��50��$ 削除��$ 削除(F12)��$ 入力を削除します��$ ../Images/28削除.png��y��� $ DataUpdateVisible�$ TwoWay=� .  .   $��	ヘルプ��
�������.@�  $��サポート��.@�  -	   $%
 ../Images/30マニュアル.png��$ MANUAL��$ ﾏﾆｭｱﾙ��$ 取扱説明書��$* $取り扱い説明書を開きます��  -
   $(
 "../Images/29遠隔サポート.png��$ SUPPORT��$ お問合せ��$ お問合せ��$< 6Web上の｢お問合せ｣ページに遷移します��  -   $
 ../Images/QandA.png��$	 FAQ��$ QandA��$ QandA��$E ?Web上の｢よくあるご質問｣ページに遷移します��  -   $(
 "../Images/13共栄Webリンク.png��$ KYOEI��$ 共栄ｼｽﾃﾑ��$ 共栄システムHP��$< 6共栄システムのホームページを開きます��  $��実施状況入力��
�������   ContextualTabGroupHeader$ 入力補助��.@�  $'��!従来ファンクションキー��.@�  $%
 ../Images/16マスタ入力.png��$ d��$ 	行追加��$ 行追加(F5)��$' !売上の履歴を表示します��$% ../Images/16マスタ入力.png��  -   $
 ../Images/24登録.png��$ e��$ マスタ登録��$ 伝票登録(F9)��$$ 伝票の登録を行います��$ ../Images/24登録.png��  -   $
 ../Images/26クリア.png��$ f��$ 入力取消し��$ 入力取消(F12)��$' !入力の取消しを行います��$ ../Images/26クリア.png��  -   $
 ../Images/27終了.png��$ g��$ 終了��$ 入力取消(F11)��$ 終了します��$ ../Images/27終了.png��  $��一括入力��
�������$ 入力補助��.@�  $'��!従来ファンクションキー��.@�  $
 ../Images/22リスト.png��$ d��$ プレビュー��$ プレビュー(F5)��$' !売上の履歴を表示します��$ ../Images/22リスト.png��  -   $
 ../Images/24登録.png��$ e��$ 印刷��$ 
印刷(F9)��$$ 伝票の登録を行います��$ ../Images/24登録.png��  -   $
 ../Images/24登録.png��$ e��$ マスタ登録��$ マスタ登録(F9)��$$ 伝票の登録を行います��$ ../Images/24登録.png��  -   $
 ../Images/26クリア.png��$ f��$ 入力取消し��$ 入力取消(F12)��$' !入力の取消しを行います��$ ../Images/26クリア.png��  -   $
 ../Images/27終了.png��$ g��$ 終了��$ 入力取消(F11)��$ 終了します��$ ../Images/27終了.png��� $��0��$��Bottom=� �� $��74 � � ColumnDefinitions �� $��90 ��� $��* �.+�]� $��0��$��0��$��Stretch=�$��2q����� *MaintenanceModeBackground+.+��� $��メイリオ)�$��24(�$��Medium"�$��Stretch=�$��Center=�$��Center=�$��Center=����� *MaintenanceMode+���� *MaintenanceModeForeground+�� $��0��$��1��$��Bottom=�
��� ���
���  ��$��2,0,2,0q����� *ErrorMessage+y��� *ErrorMessageVisibility+�� $��1�����#2792c3$��1q�$��5q�
�������._�� $��15q� �� $
��auto ��� $��5 ��� $��30 ��� $��5 ��� $��30 ��� $��30 � �� $��2* ��� $��* �.+�� $��0�� �� $
��auto ��� $
��auto ��� $
��auto ��� $
��auto ��� $
��auto ��� $��2* � �� $��30 �.+�?  9KyoeiSystem.Framework.Windows.Controls.UcLabelTwinTextBox  -   $��LabelTextHingunCode��   Label_Context$ 	コード��   Label_Width$	 100Q�=  7KyoeiSystem.Framework.Windows.Controls.FrameworkControl   	IsKeyItem .    MasterCheckEnabled .     DataAccessName$ 
M16_HINGUN��   
Text1Width$ 65Q�   Text1MaxLength$ 3��   Text1TextAlignment$
 Left=�   Label1Visibility$ 	Collapsed=� �� IsInputMethodEnabled .    �� PreferredImeState$  On=�!! �� PreferredImeConversionMode$! Alphanumeric=�"   Text2Visibility$" 	Collapsed=�#   Label2Visibility$# 	Collapsed=�$   Text1$ �� *商品群コード+$ TwoWay=� .  . %   Text2% �� *商品群名+$ TwoWay=� .  . �� -   $	��TOP��$y�Hidden=�$	��≪��$��16(�$��30��$��Center=�$��5��$��
10,0,424,0q��� -   $
��PREV��$y�Hidden=�$	��＜��$��16(�$��30��$��Center=�$��6��$��
55,0,379,0q��� -   $
��NEXT��$y�Hidden=�$	��＞��$��16(�$��30��$��Center=�$��5��$��100,0,334,0q��� -   $	��END��$y�Hidden=�$	��≫��$��16(�$��30��$��Center=�$��5��$��145,0,289,0q�� $��2��.+�;  5KyoeiSystem.Framework.Windows.Controls.UcLabelTextBox  -   $��
HingunName��&   Label_Context$& 商品群名��$��2��'   
IsRequired' . (   Label_Width$	( 100Q�)   cWidth$	) 400Q�*   ImeType$* Native=�$
��Left=�+   
cMaxLength$+ 30��,   Text, �� *商品群名+$ TwoWay=� .  . - �� UpdateSourceTrigger$- PropertyChanged=�� $��4��.+�  -   $��KanaName��$& カナ読み��$��2��$	( 100Q�$	) 400Q�$* HankakuKatakana=�$
��Left=�$+ 20��, �� *かな読み+$ TwoWay=� .  . $- PropertyChanged=�