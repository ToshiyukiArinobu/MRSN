   M S B A M L   `   `   `  ���� X  TKyoeiSystem.Framework.Windows, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null�[clr-namespace:KyoeiSystem.Framework.Windows.ViewBase;assembly=KyoeiSystem.Framework.Windows&KyoeiSystem.Framework.Windows.ViewBase  �[clr-namespace:KyoeiSystem.Framework.Windows.ViewBase;assembly=KyoeiSystem.Framework.Windows&KyoeiSystem.Framework.Windows.ViewBase  a ]KyoeiSystem.Framework.Windows.Controls, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null�dclr-namespace:KyoeiSystem.Framework.Windows.Controls;assembly=KyoeiSystem.Framework.Windows.Controls&KyoeiSystem.Framework.Windows.Controls Z VKyoeiSystem.Application.Windows, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]3clr-namespace:KyoeiSystem.Application.Windows.Views%KyoeiSystem.Application.Windows.Views R NSystem.Data, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089>.clr-namespace:System.Data;assembly=System.DataSystem.Data 4   .KyoeiSystem.Application.Windows.Views.MST01010   bfw[clr-namespace:KyoeiSystem.Framework.Windows.ViewBase;assembly=KyoeiSystem.Framework.Windows  \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      R	 NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml 	    Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006 	    @d2http://schemas.microsoft.com/expression/blend/2008 	    jvm[clr-namespace:KyoeiSystem.Framework.Windows.ViewBase;assembly=KyoeiSystem.Framework.Windows 	    uctlsdclr-namespace:KyoeiSystem.Framework.Windows.Controls;assembly=KyoeiSystem.Framework.Windows.Controls 	    Buc3clr-namespace:KyoeiSystem.Application.Windows.Views 	    ?Data.clr-namespace:System.Data;assembly=System.Data 	    -     � AllowsTransparency  .   � Language$ ja-JP�$	��980��$	��970��$	��980��$
��1024�� � WindowStartupLocation$ CenterOwner=� � Title$( "取引先マスタ保守(MST01010)��.���  � RowDefinitions �� $
��auto ��� $��* �.+�+  %System.Windows.Controls.Ribbon.Ribbon  $��Stretch=�$��2��$��Stretch=�.@�.  (System.Windows.Controls.Ribbon.RibbonTab  $��	ホーム��   KeyTip$ h��$��75��.@�0  *System.Windows.Controls.Ribbon.RibbonGroup  $��90��  $��F1��   KeyTip$ MASTER��   RibbonFunctionBackground#��[  .@�1  +System.Windows.Controls.Ribbon.RibbonButton  -      LargeImageSource$% ../Images/15マスタ照会.png��$��0,-1,0,0q�   ToolTipTitle$ マスタ照会(F1)��	   ToolTipDescription$3	 -入力済みのデータを検索します。��
   ToolTipImageSource$%
 ../Images/15マスタ照会.png��   Label$ ﾏｽﾀ照会��   KeyTip$ F1��$��50��  $��F2��$ MASTER��#��[  .@�  $y�Hidden=�$  ��$  ��$	  ��  $��F3��$ MASTER��#��[  .@�  $y�Hidden=�$  ��$  ��$	  ��  $��F4��$ MASTER��#��[  .@�  $y�Hidden=�$  ��$  ��$	  ��  $��F5��$ MASTER��#��[  .@�  $y�Hidden=�$  ��$  ��$	  ��  $��F6��$ MASTER��#��[  .@�  $y�Hidden=�$  ��$  ��$	  ��  $��F7��$ MASTER��#��[  .@�  $y�Hidden=�$  ��$  ��$	  ��  $��F8��$ MASTER��#��[  .@�  $y�Hidden=�$  ��$  ��$	  ��  $��F9��$ MASTER��#��[  .@�  -   $ ../Images/24登録.png��$ F9��$��50��$ 登録��$ データ登録(F9)��$-	 '入力データの登録を行います��$
 ../Images/24登録.png��y���  �� Path$ DataUpdateVisible� �� Mode$ TwoWay=� �� NotifyOnSourceUpdated .  �� NotifyOnTargetUpdated .   $	��F10��$ MASTER��#��[  .@�  -   $ ../Images/26クリア.png��$	 F10��$��50��$ 入力取消��$ 入力取消(F10)��$'	 !入力の取消しを行います��$
 ../Images/26クリア.png��  $	��F11��$ MASTER��#��[  .@�  -   $ ../Images/27終了.png��$	 F11��$��50��$ 終了��$ 終了(F11)��$	 終了します��$
 ../Images/27終了.png��  $	��F12��$ MASTER��#��[  .@�  $y�Hidden=�$  ��$  ��$	  ��  $��	ヘルプ��
�������.@�  $��サポート��.@�  -   $% ../Images/30マニュアル.png��$ MANUAL��$ ﾏﾆｭｱﾙ��$ 取扱説明書��$*	 $取り扱い説明書を開きます��  -   $( "../Images/29遠隔サポート.png��$ SUPPORT��$ お問合せ��$ お問合せ��$<	 6Web上の｢お問合せ｣ページに遷移します��  -   $ ../Images/QandA.png��$	 FAQ��$ QandA��$ QandA��$E	 ?Web上の｢よくあるご質問｣ページに遷移します��  -	   $( "../Images/13共栄Webリンク.png��$ KYOEI��$ 共栄ｼｽﾃﾑ��$ 共栄システムHP��$<	 6共栄システムのホームページを開きます��  $��実施状況入力��
�������   ContextualTabGroupHeader$ 入力補助��.@�  $'��!従来ファンクションキー��.@�  $% ../Images/16マスタ入力.png��$ d��$ 	行追加��$ 行追加(F5)��$'	 !売上の履歴を表示します��$%
 ../Images/16マスタ入力.png��  -
   $ ../Images/24登録.png��$ e��$ マスタ登録��$ 伝票登録(F9)��$$	 伝票の登録を行います��$
 ../Images/24登録.png��  -   $ ../Images/26クリア.png��$ f��$ 入力取消し��$ 入力取消(F12)��$'	 !入力の取消しを行います��$
 ../Images/26クリア.png��  -   $ ../Images/27終了.png��$ g��$ 終了��$ 入力取消(F11)��$	 終了します��$
 ../Images/27終了.png��  $��一括入力��
�������$ 入力補助��.@�  $'��!従来ファンクションキー��.@�  $ ../Images/22リスト.png��$ d��$ プレビュー��$ プレビュー(F5)��$'	 !売上の履歴を表示します��$
 ../Images/22リスト.png��  -   $ ../Images/24登録.png��$ e��$ 印刷��$ 
印刷(F9)��$$	 伝票の登録を行います��$
 ../Images/24登録.png��  -   $ ../Images/24登録.png��$ e��$ マスタ登録��$ マスタ登録(F9)��$$	 伝票の登録を行います��$
 ../Images/24登録.png��  -   $ ../Images/26クリア.png��$ f��$ 入力取消し��$ 入力取消(F12)��$'	 !入力の取消しを行います��$
 ../Images/26クリア.png��  -   $ ../Images/27終了.png��$ g��$ 終了��$ 入力取消(F11)��$	 終了します��$
 ../Images/27終了.png��� $��0��$��Bottom=� �� $��74 � � ColumnDefinitions �� $��90 ��� $��* �.+�]� $��0��$��0��$��Stretch=�$��2q����� *MaintenanceModeBackground+.+��� $��メイリオ)�$��24(�$��Medium"�$��Stretch=�$��Center=�$��Center=�$��Center=����� *MaintenanceMode+���� *MaintenanceModeForeground+�� $��0��$��1��$��Bottom=�
��� ���
���  ��$��2,0,2,0q����� *ErrorMessage+y��� *ErrorMessageVisibility+� $��1��
�������$��5q�.+�� $��15q� �� $��30 ��� $��* � �� $	��55* ��� $	��67* �.+��  �� $
��auto ��� $
��auto ��� $��* �.+�;  5KyoeiSystem.Framework.Windows.Controls.UcLabelTextBox  -     Name$ 	TORI_CODE��   Label_Context$ 取引先コード��   cWidth$ 60Q�   Label_Width$	 100Q�=  7KyoeiSystem.Framework.Windows.Controls.FrameworkControl   	IsKeyItem .    ImeType$	 Off=�   
cMaxLength$ 4��$
��Left=�   ValidationType$ Integer=�    cVerticalContentAlignment$ Center=�   Text �� *取引先コード+$ TwoWay=� .  . �� $	��―��$��20��$
��Left=�$��1��6  0KyoeiSystem.Framework.Windows.Controls.UcTextBox  -     Name$ TORI_EDA�� . $��2��   cWidth$ 30Q�   ImeType$	 Off=�    
cMaxLength$  2��$
��Left=�!   ValidationType$! Integer=� "   cVerticalContentAlignment$" Center=�#   Text# �� *枝番+$ TwoWay=� .  . � -   $ �Name$$ grdLeft��$	��Top=�$��1��$	��760�� �� $��30 ��� $��30 ��� $��30 ��� $��30 ��� $��30 ��� $��30 ��� $��30 ��� $��30 ��� $��30 ��� $��30 ��� $��40 ��� $��30 ��� $��30 ��� $��30 ��� $��30 ��� $��30 ��� $��30 ��� $��30 ��� $��30 ��� $��30 ��� $��30 ��� $��30 ��� $��30 ��� $��30 ��� $��30 ��� $��30 � �� $
��auto �.+�  -   $ 	TORI_NM_1��$ 正式名称��$��0��$	 100Q�$ Native=�$ 50��$
��Left=�$	��430��$ Center=� �� *RowM01TOK[得意先名１]+$ TwoWay=� .  .   -   $ 	TORI_NM_2��$ 正式名称２��$��1��$	 100Q�$ Native=�$ 50��$
��Left=�$	��430��$ Center=� �� *RowM01TOK[得意先名２]+$ TwoWay=� .  .   -   $ TORI_DEPT_NM��$ 部課名称��$��2��$	 100Q�$ Native=�$ 50��$
��Left=�$	��430��$ Center=� �� *RowM01TOK[部課名称]+$ TwoWay=� .  .   -   $ TORI_RNM��$ 	略　称��$��3��$	 100Q�$ Native=�$ 30��$
��Left=�$	��430��$ Center=� �� *RowM01TOK[略称名]+$ TwoWay=� .  .   -   $ YUBIN_NO��$ 郵便番号��$��4��$	 100Q�$	 Off=�$ 8��$
��Left=�$	 100Q�$ Center=� �� *RowM01TOK[郵便番号]+$ TwoWay=� .  .   -   $ 	ADDRESS_1��$ 	住所１��$��5��$	 100Q�$ Native=�$ 50��$
��Left=�$	��430��$ Center=� �� *RowM01TOK[住所１]+$ TwoWay=� .  .   -   $ 	ADDRESS_2��$ 	住所２��$��6��$	 100Q�$ Native=�$ 50��$
��Left=�$	��430��$ Center=� �� *RowM01TOK[住所２]+$ TwoWay=� .  .   -   $	 TEL��$ 	ＴＥＬ��$��7��$	 100Q�$	 Off=�$ 15��$
��Left=�$	 100Q�$ Center=� �� *RowM01TOK[電話番号]+$ TwoWay=� .  .   -   $	 FAX��$ 	ＦＡＸ��$��8��$	 100Q�$	 Off=�$ 15��$
��Left=�$	 100Q�$ Center=� �� *RowM01TOK[ＦＡＸ]+$ TwoWay=� .  .   -   $ TORI_KNM��$ カナ名称��$��9��$	 100Q�$ HankakuKatakana=�$ 20��$
��Left=�$	��430��$ Center=� �� *RowM01TOK[かな読み]+$ TwoWay=� .  . ?  9KyoeiSystem.Framework.Windows.Controls.UcLabelTwinTextBox  -   %  Name$% JIS_COMPANY��&   Label_Context$& 担当会社コード��$��10��'   Label_Width$	' 100Q�(   DataAccessName$( M70_JIS��)   DataAccessMode$) On=� .  $	��Top=�$
��Left=�$��30��*   Label1Visibility$* 	Collapsed=�+   Label2Visibility$+ 	Collapsed=�,   
Text1Width$, 40Q�-   Text1MaxLength$- 3��.   Text1IMEType$	. Off=�/   Text1ValidationType$/ Integer=�$0   Text1VerticalContentAlignment$0 Center=�1   
Text2Width$	1 286Q�2   Text2IsReadOnly2 . 3   Text2Background
3 ���� 4   Text2BorderThickness$4 0q�$5   Text2VerticalContentAlignment$5 Center=�6   Text16 �� *" RowM01TOK[担当会社コード]+$ TwoWay=� .  . ;	  5KyoeiSystem.Application.Windows.Views.M01_TOK_TextBox	  -   7 	 Name$7 	KAISYA_CD��$��11��$
��Left=�$��	0,0,240,0q�8 	  DataAccessName$8 M01_TOK��9 	  DataAccessMode$9 On=�: 	  Label_Context$: 集約取引先コード��; 	  Label_Width$	; 100Q�< 	  
Label1Text$	< －��= 	  Label1Visibility$= Visible=�> 	  
Text1Width$> 60Q�? 	  Text1MaxLength$? 4��@ 	  Text1IMEType$	@ Off=�A 	  Text1ValidationType$A Integer=�$B 	  Text1VerticalContentAlignment$B Center=�C 	  
Text2Width$C 30Q�D 	  Text2MaxLength$D 2��E 	  Text2IMEType$	E Off=�F 	  Text2ValidationType$F Integer=�$G 	  Text2VerticalContentAlignment$G Center=�H 	  Text1H �� *%#RowM01TOK[集約取引先コード]+$ TwoWay=� .  . I 	  Text2I �� *" RowM01TOK[集約取引先枝番]+$ TwoWay=� .  .   -    $ SHUKEI_CD_1��$ 集計ｺｰﾄﾞ①��$��12��$	 100Q�$ 60Q�$	 Off=�$ 5��$
��Left=�$ Integer=�$ Center=� �� *RowM01TOK[集計コード１]+$ TwoWay=� .  .   -!   $ SHUKEI_CD_2��$ 集計ｺｰﾄﾞ②��$��13��$	 100Q�$ 60Q�$	 Off=�$ 5��$
��Left=�$ Integer=�$ Center=� �� *RowM01TOK[集計コード２]+$ TwoWay=� .  .   -"   $ SHUKEI_CD_3��$ 集計ｺｰﾄﾞ③��$��14��$	 100Q�$ 60Q�$	 Off=�$ 5��$
��Left=�$ Integer=�$ Center=� �� *RowM01TOK[集計コード３]+$ TwoWay=� .  . � $��15��$��2�� �� $	��100 ��� $
��auto � �� $��* ��� $��* �.+�4
  .KyoeiSystem.Framework.Windows.Controls.UcLabel
  J 
  	LabelText$J 自社備考��$��0��$��0��$��2��$
��Left=�$	��Top=�$��58��$	��100��  -#   $% BIKO_1��$��0��$��1��$( M11_TEK��$) On=� .  $
��Left=�K   Label_Visibility$K 	Collapsed=�$* 	Collapsed=�$+ 	Collapsed=�$, 40Q�$- 5��$	. Off=�$/ Integer=�$0 Center=�$	1 285Q�L   Text2MaxLength$L 50��M   Text2IMEType$M Native=�$5 Center=�N   Text2N �� *RowM01TOK[自社備考１]+$ TwoWay=� .  .   -$   $% BIKO_2��$��1��$��1��$( M11_TEK��$) On=� .  $
��Left=�$K 	Collapsed=�$* 	Collapsed=�$+ 	Collapsed=�$, 40Q�$- 5��$	. Off=�$/ Integer=�$0 Center=�$	1 285Q�$L 50��$M Native=�$5 Center=�N �� *RowM01TOK[自社備考２]+$ TwoWay=� .  . � $��17�� �� $
��0.5* ��� $��* �.+�  -%   $ DEL_FLG��$��17��$
��Left=�$ 論理削除��$	 100Q�$ Integer=�$	 Off=�$ 30Q�$ 1��$ Center=� �� *RowM01TOK[論理削除]+$ TwoWay=� .  . �� $<��6"0"は使用中、"9"は削除済みになります。��$
��Left=�$��17��$��2��$	��267��  -&   $ DEL_DT��$��18��O   	FocusableO .  $ 	削除日��$	 100Q�$	 305Q�P   cIsReadOnlyP . Q   cBorderThickness$Q 0q�R   cBackground
R ���� "S   cHorizontalContentAlignment$S Right=�$ Center=� �� *RowM01TOK[削除日時]+$ TwoWay=� .  . T �� StringFormat$T yyyy/MM/dd HH:mm:ss��  -'   $% 	DEL_STAFF��$��19��$
��Left=�$( M72_TNT��$) On=�$& 	削除者��$	' 100Q�$* 	Collapsed=�$+ 	Collapsed=�U   Text1Visibility$U 	Collapsed=�V   Text1IsReadOnlyV . W   Text1AcceptsTabW .  2 . $	1 286Q�$5 Center=�X   Text2AcceptsTabX .  $4 0q�
3 ���� 6 �� *RowM01TOK[削除者]+$ TwoWay=� .  .   -(   $ ADD_DT��$��20��O .  $ 	登録日��$	 100Q�$	 305Q�P . $Q 0q�
R ���� $S Right=�$ Center=� �� *RowM01TOK[登録日時]+$ TwoWay=� .  . $T yyyy/MM/dd HH:mm:ss��  -)   $% 	ADD_STAFF��$��21��$
��Left=�$& 	登録者��$	' 100Q�$* 	Collapsed=�$+ 	Collapsed=�$( M72_TNT��$) On=�$U 	Collapsed=�V . W .  2 . $	1 286Q�$5 Center=�X .  
3 ���� $4 0q�6 �� *RowM01TOK[登録者]+$ TwoWay=� .  .   -*   $ UPD_DT��$��22��O .  $ 最終更新日��$	 100Q�$	 305Q�P . $Q 0q�
R ���� $S Right=�$ Center=� �� *RowM01TOK[最終更新日時]+$ TwoWay=� .  . $T yyyy/MM/dd HH:mm:ss��  -+   $% 	UPD_STAFF��$��23��$
��Left=�$& 最終更新者��$	' 100Q�$* 	Collapsed=�$+ 	Collapsed=�$( M72_TNT��$) On=�$U 	Collapsed=�V . W .  2 . $	1 286Q�$5 Center=�X .  
3 ���� $4 0q�6 �� *RowM01TOK[最終更新者]+$ TwoWay=� .  . � -,   $$ 	grdInPut2��$��1��$��1��$��10,0,0,0q� �� $��30 ��� $��* �.+�C  =KyoeiSystem.Framework.Windows.Controls.UcLabelTextRadioButton  --   Y  Name$Y M01_TORI_KBN��Z   Label_Content$Z 取引区分��[   Label_Width$	[ 100Q�\   RadioSelectButton$	\ One=�]   RadioViewCount$
] Five=�^   RadioOne_Content$^ 	得意先��_   RadioSecond_Content$_ 	仕入先��`   RadioThird_Content$` 	加工先��a   RadioFourth_Content$a 相殺��b   RadioFifth_Content$b 販社��$c   Text1VerticalContentAlignment$c Center=�d   Textd �� $ RowM01TOK[取引区分]�$ TwoWay=� .  . � $��1�� �� $
��auto ��� $
��auto ��� $
��auto �.+�� $��0��$��0,10,0,0q� �� $��40 ��� $��* � �� $��30 ��� $��30 ��� $��30 ��� $��30 ��� $��30 ��� $��30 ��� $��30 ��� $��30 ��� $��30 �.+��� $��




請



求��$��37��
��� ���$��0,0,1,0q�
�������$��Center=�
���   �$��1q�$��0��$��9��� $��0��$��1�� �� $
��auto ��� $
��auto ��� $��* �.+�  -.   $ T_YOSHIN��$ 	与信枠��$	 100Q�$ Integer=�$	 Off=�$ 50Q�$ 5��e   cTextAlignment$e Right=�$ Center=� �� *RowM01TOK[与信枠]+$ TwoWay=� .  . �� $��1��$��万円��$��50��$
��Left=�� $��1��$��1�� �� $
��auto ��� $
��auto �.+�  -/   $Y 	T_TAX_KBN��$Z 消費税区分��$	[ 100Q�$	\ One=�f   RadioOneStartf . $
] Four=�$_ 一括��$` 個別��$a 	請求無��$c Center=�d �� $# RowM01TOK[Ｔ消費税区分]�$ TwoWay=� .  . � $��2��$��1�� �� $
��auto ��� $
��auto �.+�  -0   $Y 
T_TAX_TANI��$Z 消費税単位��$	[ 100Q�$	\ One=�f . $
] Four=�$_ 	切捨て��$` 四捨五入��$a 	切上げ��$c Center=�d �� $ RowM01TOK[Ｔ税区分ID]�$ TwoWay=� .  . � $��3��$��1�� �� $
��auto ��� $
��auto �.+�  -1   $ 
T_SHIME_DT��$ 締日��$	 100Q�$ 29Q�$	 Off=�$ 2��$ Integer=�$ Center=� �� *RowM01TOK[Ｔ締日]+$ TwoWay=� .  . �� $.��(末締めは[31]を入力して下さい��$
��Left=�$��24��$��1��$��Center=�$	��267��� $��4��$��1�� �� $
��auto ��� $
��auto �.+�  -2   $ 
T_SHIHARAI��$ 請求条件��$��1��$��7��$	 100Q�$	 100Q�$	 Off=�$ 8��$ Integer=�$��	237,0,0,0q�$ Center=� �� *RowM01TOK[Ｔ請求条件]+$ TwoWay=� .  . � $��5��$��1�� �� $
��auto ��� $
��auto �.+�<  6KyoeiSystem.Framework.Windows.Controls.UcLabelComboBox  -3   g  Name$g T_SHIHARAI_KBN��$��27��$	��200��$��	237,0,0,0q�h   Label_Context$h 請求区分��i   Label_Background
i � ���j   Label_Width$	j 100Q�k   Label_Focusablek .  $��1��l   ComboListingParams$/l )取引先マスタ,コンボ,支払区分��m   SelectedValuem �� *RowM01TOK[Ｔ請求区分]+$ TwoWay=� .  . n �� UpdateSourceTrigger$n PropertyChanged=�� $��6��$��1�� �� $
��auto ��� $
��auto �.+�  -4   $ T_SITE1��$��0��$
��Left=�$ 	サイト��$	 100Q�$ 30Q�o   cValidationType$o Integer=�$	 Off=�$ Center=� �� $  RowM01TOK[Ｔサイト１]�$ TwoWay=� .  . �� $��ヶ月��$��0��$��	130,0,0,0q�$
��Left=�$	��107��  -5   $ T_SITE2��$��1��$
��Left=�$ 	サイト��$	 100Q�$ 30Q�$o Integer=�$	 Off=�$ Center=� �� $  RowM01TOK[Ｔサイト２]�$ TwoWay=� .  . �� $��ヶ月��$��1��$��	130,0,0,0q�$
��Left=�$	��107��� $��7��$��1�� �� $
��auto ��� $
��auto �.+�  -6   $ T_NYUKIN_DT1��$ 	入金日��$��0��$	 100Q�$ 30Q�$	 Off=�$ Integer=�$ 2��$��27��$ Center=� �� *RowM01TOK[Ｔ入金日１]+$ TwoWay=� .  .   -7   $ T_NYUKIN_DT2��$ 	入金日��$��1��$	 100Q�$ 30Q�$	 Off=�$ Integer=�$ 2��$��27��$��	105,0,0,0q�$ Center=� �� *RowM01TOK[Ｔ入金日2]+$ TwoWay=� .  .   -8   $% T_TANTO�� .  $& 	担当者��$	' 100Q�$��8��$��1��$( M72_TNT��p   Text2Visibility$p Visible=�$* 	Collapsed=�$, 50Q�$- 4��q   Label_Background
q � ���$/ Integer=�$0 Center=�2 . $4 0q�
3 ���� $5 Center=�6 �� *" RowM01TOK[Ｔ担当者コード]+$ TwoWay=� .  . N �� *Ｔ担当者名+$ TwoWay=� .  . � $��1��$��0,10,0,0q� �� $��40 ��� $��* � �� $��30 ��� $��30 ��� $��30 ��� $��30 ��� $��30 ��� $��30 ��� $��30 ��� $��30 �.+��� $��





支



払��
��� � �$��9��$��0,0,1,0q�
�������$��Center=�
���   �$��1q�$	��270��$��Bottom=�� $��1�� �� $
��auto ��� $
��auto �.+�  -9   $Y 	S_TAX_KBN��$Z 消費税区分��$	[ 100Q�$	\ One=�f . $
] Four=�$_ 一括��$` 個別��$a 	請求無��$c Center=�d �� $) #RowM01TOK[Ｓ支払消費税区分]�$ TwoWay=� .  . � $��1��$��1�� �� $
��auto ��� $
��auto �.+�  -:   $Y 
S_TAX_TANI��$Z 消費税単位��$	[ 100Q�$	\ One=�f . $
] Four=�$_ 	切捨て��$` 四捨五入��$a 	切上げ��$c Center=�d �� $ RowM01TOK[Ｓ税区分ID]�$ TwoWay=� .  . � $��2��$��1�� �� $
��auto ��� $
��auto �.+�  -;   $ 
S_SHIME_DT��$��0��$��13��$ 締日��$	 100Q�$ Integer=�$	 Off=�$ 29Q�$ 2��$ Center=� �� *RowM01TOK[Ｓ締日]+$ TwoWay=� .  . �� $.��(末締めは[31]を入力して下さい��$
��Left=�$��24��$��1��$	��267��� $��3��$��1�� �� $
��auto ��� $
��auto �.+�  -<   $ 
S_SHIHARAI��$��1��$��	237,0,0,0q�$ 支払条件��$	 100Q�$ Integer=�$	 Off=�$	 100Q�$ 8��$ Center=� �� *RowM01TOK[Ｓ支払条件]+$ TwoWay=� .  . � $��4��$��1�� �� $
��auto ��� $
��auto �.+�  -=   $g S_SHIHARAI_KBN��$h 支払区分��$��27��$	��200��$	j 100Q�$��1��
i � � �k .  $��	237,0,0,0q�$/l )取引先マスタ,コンボ,支払区分��m �� *RowM01TOK[Ｓ支払区分]+$ TwoWay=� .  . $n PropertyChanged=�� $��5��$��1�� �� $
��auto ��� $
��auto �.+�  ->   $ S_SITE1��$��0��$
��Left=�$ 	サイト��$	 100Q�$ 30Q�$o Integer=�$	 Off=�$ Center=� �� $  RowM01TOK[Ｓサイト１]�$ TwoWay=� .  . �� $��ヶ月��$��0��$��	130,0,0,0q�$
��Left=�$	��107��  -?   $ S_SITE2��$��1��$
��Left=�$ 	サイト��$	 100Q�$ 30Q�$o Integer=�$	 Off=�$ Center=� �� $  RowM01TOK[Ｓサイト２]�$ TwoWay=� .  . �� $��ヶ月��$��1��$��	130,0,0,0q�$
��Left=�$	��107��� $��6��$��1�� �� $
��auto ��� $
��auto �.+�  -@   $ S_NYUKIN_DT1��$ 	入金日��$��0��$��17��$	 100Q�$ 30Q�$	 Off=�$ 2��$��27��$ Center=� �� *RowM01TOK[Ｓ入金日１]+$ TwoWay=� .  .   -A   $ S_NYUKIN_DT2��$ 	入金日��$��1��$��17��$	 100Q�$ 30Q�$	 Off=�$ 2��$��	105,0,0,0q�$��27��$ Center=� �� *RowM01TOK[Ｓ入金日２]+$ TwoWay=� .  .   -B   $% S_TANTO��$& 	担当者��$��1��$��7��$	' 100Q� .  $( M72_TNT��
q � � �$* 	Collapsed=�$, 50Q�$- 4��$	. Off=�$/ Integer=�$0 Center=�2 . $4 0q�
3 ���� $5 Center=�6 �� *" RowM01TOK[Ｓ担当者コード]+$ TwoWay=� .  . N �� *Ｓ担当者名+$ TwoWay=� .  . � $��2��$��0,10,0,0q� �� $
��auto ��� $
��auto � �� $��30 ��� $��30 ��� $��30 �.+��� -C   r ��Name$r btnTokuShoBaikaSet��$!��得意先商品売価設定��$��0��$	��160���� -D   $r btnShiShoGenkaSet��$!��仕入先商品原価設定��$��1��$	��160���� -E   $r btnGaiShoKakoSet��$'��!外注先商品加工原価設定��$��2��$	��160��	  -F   $7 	SAN_TORI1��$��0��$��1��$
��Left=� .  $��10,1,0,1q�$8 M01_TOK��$9 On=�s 	  LinkItem$	s 0,3��$: 参照取引先��$	; 100Q�$	< －��$= Visible=�$> 40Q�$? 4��$	@ Off=�$A Integer=�$B Center=�$C 30Q�$D 2��$	E Off=�$F Integer=�$G Center=�H �� *(&RowM01TOK[参照取引先コード１]+$ TwoWay=� .  . I �� *%#RowM01TOK[参照取引先枝番１]+$ TwoWay=� .  . 	  -G   $7 	SAN_TORI2��$��1��$��1��$
��Left=� .  $��10,1,0,1q�$8 M01_TOK��$9 On=�$	s 1,3��$: 参照取引先��$	; 100Q�$	< －��$= Visible=�$> 40Q�$? 4��$	@ Off=�$A Integer=�$B Center=�$C 30Q�$D 2��$	E Off=�$F Integer=�$G Center=�H �� *(&RowM01TOK[参照取引先コード２]+$ TwoWay=� .  . I �� *%#RowM01TOK[参照取引先枝番２]+$ TwoWay=� .  . 	  -H   $7 	SAN_TORI3��$��2��$��1��$
��Left=� .  $��10,1,0,1q�$8 M01_TOK��$9 On=�$	s 2,3��$: 参照取引先��$	; 100Q�$	< －��$= Visible=�$> 40Q�$? 4��$	@ Off=�$A Integer=�$B Center=�$C 30Q�$D 2��$	E Off=�$F Integer=�$G Center=�H �� *(&RowM01TOK[参照取引先コード３]+$ TwoWay=� .  . I �� *%#RowM01TOK[参照取引先枝番３]+$ TwoWay=� .  . 