   M S B A M L   `   `   `  ���� X  TKyoeiSystem.Framework.Windows, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null�[clr-namespace:KyoeiSystem.Framework.Windows.ViewBase;assembly=KyoeiSystem.Framework.Windows&KyoeiSystem.Framework.Windows.ViewBase  �[clr-namespace:KyoeiSystem.Framework.Windows.ViewBase;assembly=KyoeiSystem.Framework.Windows&KyoeiSystem.Framework.Windows.ViewBase  a ]KyoeiSystem.Framework.Windows.Controls, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null�dclr-namespace:KyoeiSystem.Framework.Windows.Controls;assembly=KyoeiSystem.Framework.Windows.Controls&KyoeiSystem.Framework.Windows.Controls R NSystem.Data, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089>.clr-namespace:System.Data;assembly=System.DataSystem.Data # KyoeiSystem.Application.Windows4   .KyoeiSystem.Application.Windows.Views.MST02010   bfw[clr-namespace:KyoeiSystem.Framework.Windows.ViewBase;assembly=KyoeiSystem.Framework.Windows  \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      R	 NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml 	    Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006 	    @d2http://schemas.microsoft.com/expression/blend/2008 	    jvm[clr-namespace:KyoeiSystem.Framework.Windows.ViewBase;assembly=KyoeiSystem.Framework.Windows 	    uctlsdclr-namespace:KyoeiSystem.Framework.Windows.Controls;assembly=KyoeiSystem.Framework.Windows.Controls 	    ?Data.clr-namespace:System.Data;assembly=System.Data 	    -     � AllowsTransparency  .   � WindowStartupLocation$ CenterOwner=�$	��860��$
��1024��$	��860��$
��1024�� � 
ResizeMode$ NoResize=� � Title$% 品番マスタ入力(MST02010)��.��(� $
��auto��$
��auto�� (� Stretch$
 Fill=� (� StretchDirection$
 Both=�.���  � RowDefinitions �� $
��auto ��� $
��auto �.+�+  %System.Windows.Controls.Ribbon.Ribbon  $��Stretch=�$��2��$��Stretch=�.@�.  (System.Windows.Controls.Ribbon.RibbonTab  $��	ホーム��   KeyTip$ h��$��75��.@�0  *System.Windows.Controls.Ribbon.RibbonGroup  $��90��  $��F1��   KeyTip$ MASTER��   RibbonFunctionBackground#��[  .@�1  +System.Windows.Controls.Ribbon.RibbonButton  -   	   LargeImageSource$%	 ../Images/15マスタ照会.png��$��0,-1,0,0q�
   ToolTipTitle$
 マスタ照会(F1)��   ToolTipDescription$3 -入力済みのデータを検索します。��   ToolTipImageSource$% ../Images/15マスタ照会.png��   Label$ ﾏｽﾀ照会��   KeyTip$ F1��$��50��  $��F2��$ MASTER��#��[  .@�  $y�Hidden=�$  ��$
  ��$  ��  $��F3��$ MASTER��#��[  .@�  $y�Hidden=�$  ��$
  ��$  ��  $��F4��$ MASTER��#��[  .@�  $y�Hidden=�$  ��$
  ��$  ��  $��F5��$ MASTER��#��[  .@�  $y�Hidden=�$  ��$
  ��$  ��  $��F6��$ MASTER��#��[  .@�  $y�Hidden=�$  ��$
  ��$  ��  $��F7��$ MASTER��#��[  .@�  $y�Hidden=�$  ��$
  ��$  ��  $��F8��$ MASTER��#��[  .@�  $y�Hidden=�$  ��$
  ��$  ��  $��F9��$ MASTER��#��[  .@�  -   $	 ../Images/24登録.png��$ F9��$��50��$ 登録��$
 データ登録(F9)��$- '入力データの登録を行います��$ ../Images/24登録.png��y���  �� Path$ DataUpdateVisible� �� Mode$ TwoWay=� �� NotifyOnSourceUpdated .  �� NotifyOnTargetUpdated .   $	��F10��$ MASTER��#��[  .@�  -   $	 ../Images/26クリア.png��$	 F10��$��50��$ 入力取消��$
 入力取消(F10)��$' !入力の取消しを行います��$ ../Images/26クリア.png��  $	��F11��$ MASTER��#��[  .@�  -   $	 ../Images/27終了.png��$	 F11��$��50��$ 終了��$
 終了(F11)��$ 終了します��$ ../Images/27終了.png��  $	��F12��$ MASTER��#��[  .@�  $y�Hidden=�$  ��$
  ��$  ��  $��	ヘルプ��
�������.@�  $��サポート��.@�  -   $%	 ../Images/30マニュアル.png��$ MANUAL��$ ﾏﾆｭｱﾙ��$
 取扱説明書��$* $取り扱い説明書を開きます��  -   $(	 "../Images/29遠隔サポート.png��$ SUPPORT��$ お問合せ��$
 お問合せ��$< 6Web上の｢お問合せ｣ページに遷移します��  -   $	 ../Images/QandA.png��$	 FAQ��$ QandA��$
 QandA��$E ?Web上の｢よくあるご質問｣ページに遷移します��  -	   $(	 "../Images/13共栄Webリンク.png��$ KYOEI��$ 共栄ｼｽﾃﾑ��$
 共栄システムHP��$< 6共栄システムのホームページを開きます��  $��実施状況入力��
�������   ContextualTabGroupHeader$ 入力補助��.@�  $'��!従来ファンクションキー��.@�  $%	 ../Images/16マスタ入力.png��$ d��$ 	行追加��$
 行追加(F5)��$' !売上の履歴を表示します��$% ../Images/16マスタ入力.png��  -
   $	 ../Images/24登録.png��$ e��$ マスタ登録��$
 伝票登録(F9)��$$ 伝票の登録を行います��$ ../Images/24登録.png��  -   $	 ../Images/26クリア.png��$ f��$ 入力取消し��$
 入力取消(F12)��$' !入力の取消しを行います��$ ../Images/26クリア.png��  -   $	 ../Images/27終了.png��$ g��$ 終了��$
 入力取消(F11)��$ 終了します��$ ../Images/27終了.png��  $��一括入力��
�������$ 入力補助��.@�  $'��!従来ファンクションキー��.@�  $	 ../Images/22リスト.png��$ d��$ プレビュー��$
 プレビュー(F5)��$' !売上の履歴を表示します��$ ../Images/22リスト.png��  -   $	 ../Images/24登録.png��$ e��$ 印刷��$
 
印刷(F9)��$$ 伝票の登録を行います��$ ../Images/24登録.png��  -   $	 ../Images/24登録.png��$ e��$ マスタ登録��$
 マスタ登録(F9)��$$ 伝票の登録を行います��$ ../Images/24登録.png��  -   $	 ../Images/26クリア.png��$ f��$ 入力取消し��$
 入力取消(F12)��$' !入力の取消しを行います��$ ../Images/26クリア.png��  -   $	 ../Images/27終了.png��$ g��$ 終了��$
 入力取消(F11)��$ 終了します��$ ../Images/27終了.png��� $��0��$��Bottom=� �� $��74 � � ColumnDefinitions �� $��90 ��� $��* �.+�]� $��0��$��0��$��Stretch=�$��2q����� *MaintenanceModeBackground+.+��� $��メイリオ)�$��24(�$��Medium"�$��Stretch=�$��Center=�$��Center=�$��Center=����� *MaintenanceMode+���� *MaintenanceModeForeground+�� $��0��$��1��$��Bottom=�
��� ���
���  ��$��2,0,2,0q����� *ErrorMessage+y��� *ErrorMessageVisibility+�� $��1�����#2792c3$��1q�$��5q�
�������._�� $��15q�.+�� $��0�� �� $
��auto ��� $��5 ��� $��30 ��� $��30 ��� $��30 ��� $��30 ��� $��30 ��� $��30 ��� $��30 ��� $��30 ��� $��5 ��� $��30 ��� $��30 ��� $��5 ��� $��30 ��� $��5 ��� $��30 ��� $��5 ��� $��60 ��� $��10 ��� $��30 ��� $��1 ��� $��30 ��� $��1 ��� $��30 ��� $��1 ��� $��30 ��� $
��auto � �� $��2* ��� $��* �.+�� $��0�� �� $��30 ��� $��5 ��� $��30 ��� $��30 � �� $	��300 ��� $
��auto �.+�?  9KyoeiSystem.Framework.Windows.Controls.UcLabelTwinTextBox  -     Name$ ProductCode��   Label_Context$ 品番コード��$��0��$
��Left=�   DataAccessName$ M09_HIN��   DataAccessMode$	 Off=�=  7KyoeiSystem.Framework.Windows.Controls.FrameworkControl   	IsKeyItem .    Label_Width$	 100Q�   Text2Visibility$ 	Collapsed=�   
Text1Width$ 80Q�   Text1IMEType$	 Off=�   Text1MaxLength$ 7��$   Text1VerticalContentAlignment$ Center=�    Text1  �� $ 品番コード�$ TwoWay=� .  . ;  5KyoeiSystem.Framework.Windows.Controls.UcLabelTextBox  -   !  Name$! MyProductCode��"   Label_Context$" 自社品番��$��2��$��0��$
��Left=� .  #   
IsRequired# . $��0,0,0,2q�$   Label_Width$	$ 100Q�%   cWidth$% 90Q�&   
cMaxLength$& 12�� '   cVerticalContentAlignment$' Center=�(   Text( �� $% M09_HIN_SearchRow[自社品番]�$ TwoWay=� .  .   -   $ 	ColorCode��$	 色��$��2��$��1��$	 100Q�$
��Left=�$��0,0,0,2q�)   Label1Visibility$) 	Collapsed=�$ M06_IRO��$ 40Q�$	 Off=�$ 3��$ Center=�*   
Text2Width$	* 200Q�$+   Text2VerticalContentAlignment$+ Center=�,   Text2IsReadOnly, . -   Text2BorderThickness$- 0q�.   Text2Background
. ����   �� $" M09_HIN_SearchRow[自社色]�$ TwoWay=� .  .   -   $! MyProductName��$��3��$��0��$��2��$
��Left=� .  $��0,0,0,2q�$" 自社品名��$	$ 100Q�$	% 544Q�/   ImeType$/ Native=�$& 50��$' Center=�( �� $% M09_HIN_SearchRow[自社品名]�$ TwoWay=� .  . C  =KyoeiSystem.Framework.Windows.Controls.UcLabelTextRadioButton  -   0  Name$0 ItemType��1   Label_Content$1 商品分類��$��2��2   Label_Width$	2 100Q�$��0,0,0,1q�3   RadioOneStart3 . 4   RadioViewCount$
4 Four=�5   RadioSelectButton$	5 Two=�6   
IsRequired6 . 7   RadioSecond_Content$7 食品��8   RadioThird_Content$8 繊維��9   RadioFourth_Content$9 	その他��:   Text: �� $% M09_HIN_SearchRow[商品分類]�$ TwoWay=� .  .   -   $0 	ItemStyle��$1 商品形態��$��3��$	2 100Q�$��0,0,0,1q�3 . $
4 Five=�$	5 Two=�6 . $7 SET品��$8 資材・単品��$9 雑コード��;   RadioFifth_Content$; 	副資材��: �� $+ %M09_HIN_SearchRow[商品形態分類]�$ TwoWay=� .  . � $��4�� �� $	��300 ��� $
��auto �.+�  -   $ txtMajorClassCode��$ 大分類コード��$��0��$
��Left=�$��0,0,0,1q�$ M12_DAI��<   MasterCheckEnabled< . $) 	Collapsed=�=   Label2Visibility$= 	Collapsed=�$	 100Q�$ 30Q�$ 3��$	 Off=�$ Center=�$	* 160Q�, . $- 0q�
. ���� $+ Center=�  �� $" M09_HIN_SearchRow[大分類]�$ TwoWay=� .  .   -   $��txtMediumClassCode��$ 中分類コード��$��1��$
��Left=�$��0,0,0,1q�$ M13_CHU��< . $) 	Collapsed=�$= 	Collapsed=�$	 100Q�$ 30Q�$ 3��$	 Off=�$ Center=�$	* 160Q�, . $- 0q�
. ���� $+ Center=�  �� $" M09_HIN_SearchRow[中分類]�$ TwoWay=� .  . >   LinkItem> �� *M09_HIN_SearchRow[大分類]+$ TwoWay=� .  .   $" 単位��$��5��$	$ 100Q�$% 80Q�$
��Left=� .  $��0,0,0,1q�$/ Native=�$& 10��$' Center=�( �� $ M09_HIN_SearchRow[単位]�$ TwoWay=� .  .   $" 原価��$��6��$	$ 100Q�$% 80Q�$
��Left=� .  $��0,0,0,1q�$	/ Off=�?   ValidationType$? Decimal=�@   MaxValue$@ 
9999999.99��A   cTextAlignment$A Right=�$' Center=�( �� $ M09_HIN_SearchRow[原価]�$ TwoWay=� .  . B �� StringFormat$B {0:N2}��  $" 加工原価��$��7��$	$ 100Q�$% 80Q�$
��Left=� .  $��0,0,0,1q�$	/ Off=�$? Decimal=�$@ 
9999999.99��$A Right=�$' Center=�( �� $% M09_HIN_SearchRow[加工原価]�$ TwoWay=� .  . $B {0:N2}��  $" 卸値��$��8��$	$ 100Q�$% 80Q�$
��Left=� .  $��0,0,0,1q�$	/ Off=�$? Decimal=�$@ 
9999999.99��$A Right=�$' Center=�( �� $ M09_HIN_SearchRow[卸値]�$ TwoWay=� .  . $B {0:N2}��  $" 売値��$��9��$	$ 100Q�$% 80Q�$
��Left=� .  $��0,0,0,1q�$	/ Off=�$? Decimal=�$@ 
9999999.99��$A Right=�$' Center=�( �� $ M09_HIN_SearchRow[売価]�$ TwoWay=� .  . $B {0:N2}��  $��11��$" 掛率��$	$ 100Q�$% 40Q�$
��Left=� .  $��0,0,0,1q�$	/ Off=�$? Decimal=�$@ 999.9��$A Right=�$' Center=�( �� $ M09_HIN_SearchRow[掛率]�$ TwoWay=� .  . $B {0:N1}��� $��12��$��2�� �� $	��300 ��� $	��300 ��� $	��300 �.+�  -   $��LabelTextBrandCode��$ ブランドコード��$��0��$��0,0,0,1q�$ 	M14_BRAND��$ On=�< . $	 100Q�$) 	Collapsed=�$= 	Collapsed=�$ 3��$ 40Q�C   Text1TextAlignment$
C Left=�$ Center=�, . $- 0q�
. ���� $+ Center=�  �� $% M09_HIN_SearchRow[ブランド]�$ TwoWay=� .  .   -   $��LabelTextSiriseCode��$ シリーズコード��$��1��$��0,0,0,1q�$ 
M15_SERIES��$ On=�< . $	 100Q�$) 	Collapsed=�$= 	Collapsed=�$ 3��$ 40Q�$
C Left=�$ Center=�, . $- 0q�
. ���� $+ Center=�  �� $% M09_HIN_SearchRow[シリーズ]�$ TwoWay=� .  .   -   $��LabelTextHinGunCode��$ 品群コード��$��0,0,0,1q�$��2��$ 
M16_HINGUN��$ On=�< . $	 100Q�$) 	Collapsed=�$= 	Collapsed=�$ 3��$ 40Q�$
C LEFT=�$ Center=�, . $- 0q�
. ���� $+ Center=�  �� $ M09_HIN_SearchRow[品群]�$ TwoWay=� .  . � $��14�� �� $	��450 ��� $	��150 ��� $
��auto �.+�  $��0��$1 消費税区分��$	2 100Q�$��0,0,0,1q�$4 Three=�$	5 One=�6 . D   RadioOne_Content$D  通常 ��$7  軽減税率 ��$8  非課税 ��: �� $( "M09_HIN_SearchRow[消費税区分]�$ TwoWay=� .  .   -   $��tSite��$" 返品可能期限��$��1��$��0,0,0,1q�$	$ 100Q�$
��Left=�$% 40Q�$& 2��$? Integer=�$A Right=�$' Center=�( �� $+ %M09_HIN_SearchRow[返却可能期限]�$ TwoWay=� .  . 4	  .KyoeiSystem.Framework.Windows.Controls.UcLabel	  E 	  	LabelText$E ヶ月まで��$��2��F 	  cBackground
F ���� "G 	  cHorizontalContentAlignment$
G Left=�H 	  cForeground
H �   �  $	" JAN��$��16�� .  $
��Left=�$��0,0,0,1q�$	$ 100Q�$	% 100Q�$& 13��$	/ Off=�$? Number=�( �� $+ %M09_HIN_SearchRow[ＪＡＮコード]�$ TwoWay=� .  . � $��18��$��2�� �� $��30 ��� $��30 � �� $	��100 ��� $
��auto �.+�	  $��0��$��0��$��2��$��0,1,0,0q�$	��100��I 	  cContent$I 備考��6
  0KyoeiSystem.Framework.Windows.Controls.UcTextBox
  -   J 
 Name$J Remarks1��$��1��$��0��K 
  ImeType$K Native=�L 
  
cMaxLength$L 32��M 
  cWidth$	M 340Q�$��0,1,0,1q�N 
  TextN �� $" M09_HIN_SearchRow[備考１]�$ TwoWay=� .  . 
  -   $J Remarks2��$��1��$��1��$K Native=�$L 32��$	M 340Q�$��0,1,0,1q�N �� $" M09_HIN_SearchRow[備考２]�$ TwoWay=� .  . � $��20�� �� $��0.25* ��� $��* �.+�  -   $! 
LogicalDel��$" 論理削除��$��20�� .  $
��Left=�$��0,0,0,1q�$	$ 100Q�$% 30Q�$	/ Off=�$& 1��$? Integer=�( �� $% M09_HIN_SearchRow[論理削除]�$ TwoWay=� .  . �� $<��6"0"は使用中、"9"は削除済みになります。��$
��Left=�$��20��$��2��$	��267��� $��22�� �� $	��300 ��� $��* �.+�  $" 	削除日��$��0�� .  $
��Left=�$��0,0,0,1q�
������ O   cIsReadOnlyO . P   	FocusableP .  Q   cAcceptsReturnQ .  R   cAcceptsTabR .  $	$ 100Q�$	% 150Q�S   cBorderThickness$S 0q�T   cBackground
T ���� $' Center=�( �� $% M09_HIN_SearchRow[削除日時]�$ TwoWay=� .  . $B {0:yyyy/MM/dd HH:mm:ss}��  $ 	削除者��$��0��$��1��$��0,0,0,1q�$ M72_TNT��$ On=�$
��Left=�$	 100Q�|�.  $) 	Collapsed=�$ 50Q�U   Text1IsReadOnlyU . V   Text1Visibility$V Hidden=�W   Text1BorderThickness$W 0q�X   Text1Background
X ���� $ Center=�$	* 120Q�, . $- 0q�
. ���� $+ Center=�  �� $" M09_HIN_SearchRow[削除者]�$ TwoWay=� .  . � $��24�� �� $	��300 ��� $��* �.+�  $" 	登録日��$��0�� .  $
��Left=�$��0,0,0,1q�
������ O . P .  Q .  R .  $	$ 100Q�$	% 150Q�$S 0q�
T ���� $' Center=�( �� $% M09_HIN_SearchRow[登録日時]�$ TwoWay=� .  . $B {0:yyyy/MM/dd HH:mm:ss}��  $ 	登録者��$��0��$��1��$��0,0,0,1q�$ M72_TNT��$ On=�$
��Left=�$	 100Q�|�.  $) 	Collapsed=�$ 50Q�U . $V Hidden=�$W 0q�
X ���� $ Center=�$	* 120Q�, . $- 0q�
. ���� $+ Center=�  �� $" M09_HIN_SearchRow[登録者]�$ TwoWay=� .  . � $��26�� �� $	��300 ��� $��* �.+�  $" 最終更新日��$��0�� .  $
��Left=�$��0,0,0,1q�
������ O . P .  Q .  R .  $	$ 100Q�$	% 150Q�$S 0q�
T ���� $' Center=�( �� $+ %M09_HIN_SearchRow[最終更新日時]�$ TwoWay=� .  . $B {0:yyyy/MM/dd HH:mm:ss}��  $ 最終更新者��$��0��$��1��$��0,0,0,1q�$ M72_TNT��$ On=�$
��Left=�$	 100Q�|�.  $) 	Collapsed=�$ 50Q�U . $V Hidden=�$W 0q�
X ���� $ Center=�$	* 120Q�, . $- 0q�
. ���� $+ Center=�  �� $( "M09_HIN_SearchRow[最終更新者]�$ TwoWay=� .  . � $��2��$��	700,0,0,0q� �� $��30 ��� $��30 ��� $��30 ��� $��30 ��� $��30 ��� $��30 ��� $��30 ��� $��30 �.+��� -    $��	SetButton��$��セット構成品��$��3��$
��Left=�$��0,0,0,1q�$	��160��|�.  �� -!   $��TokuiButton��$��得意先売価設定��$��4��$
��Left=�$��0,0,0,1q�$	��160��|�. �� -"   $��ShiireButton��$��仕入先原価設定��$��5��$
��Left=�$��0,0,0,1q�$	��160��|�. �� -#   $��GaichuButton��$!��外注先加工原価設定��$��6��$
��Left=�$��0,0,0,1q�$	��160��|�. �� -$   $��KyakusakiButton��$��得意先品番登録��$��7��$
��Left=�$��0,0,0,1q�$	��160��|�. 