   M S B A M L   `   `   `  ���� X  TKyoeiSystem.Framework.Windows, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null�[clr-namespace:KyoeiSystem.Framework.Windows.ViewBase;assembly=KyoeiSystem.Framework.Windows&KyoeiSystem.Framework.Windows.ViewBase  �[clr-namespace:KyoeiSystem.Framework.Windows.ViewBase;assembly=KyoeiSystem.Framework.Windows&KyoeiSystem.Framework.Windows.ViewBase  a ]KyoeiSystem.Framework.Windows.Controls, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null�dclr-namespace:KyoeiSystem.Framework.Windows.Controls;assembly=KyoeiSystem.Framework.Windows.Controls&KyoeiSystem.Framework.Windows.Controls Z VKyoeiSystem.Application.Windows, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]3clr-namespace:KyoeiSystem.Application.Windows.Views%KyoeiSystem.Application.Windows.Views [ WSystem.Windows.Forms, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089Y@clr-namespace:System.Windows.Forms;assembly=System.Windows.FormsSystem.Windows.Forms R NSystem.Data, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089>.clr-namespace:System.Data;assembly=System.DataSystem.Data 4   .KyoeiSystem.Application.Windows.Views.MST16010   bfw[clr-namespace:KyoeiSystem.Framework.Windows.ViewBase;assembly=KyoeiSystem.Framework.Windows  \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W	 SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     	 R
 NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml 
   	 Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006 
   	 @d2http://schemas.microsoft.com/expression/blend/2008 
   	 jvm[clr-namespace:KyoeiSystem.Framework.Windows.ViewBase;assembly=KyoeiSystem.Framework.Windows 
   	 uctlsdclr-namespace:KyoeiSystem.Framework.Windows.Controls;assembly=KyoeiSystem.Framework.Windows.Controls 
   	 Buc3clr-namespace:KyoeiSystem.Application.Windows.Views 
   	 Owf@clr-namespace:System.Windows.Forms;assembly=System.Windows.Forms 
   	 ?Data.clr-namespace:System.Data;assembly=System.Data 
   	 -   $��
MainWindow��  � WindowStyle$  SingleBorderWindow=�$	��728��$
��1024��$	��768��$
��1024�� � WindowStartupLocation$ CenterOwner=� � Left$ 0��
 � Top$ 0�� � Title$% 自社マスタ入力(MST16010)�� � 	Resources    ImageButton �� 
TargetType �� BasedOn �� Property	 �� Value
 �� 
TargetType'  !System.Windows.VisualStateManager   VisualStateGroups%  System.Windows.VisualStateGroup   States   System.Windows.VisualState   
Storyboard g� Duration �� 
TargetName �� TargetProperty	 W� To ��Name �� CornerRadius J� Opacity ��Name �� Color �� Opacity ��Name �� RadiusX �� RadiusY J� IsHitTestVisible<  6KyoeiSystem.Framework.Windows.Controls.BooleanInverter  boolInverter��%b  &	        &	 I    �� " �� Z� .���  � ��
	 ���� ��  � ��
	 ���� ��  � ��$	 3q���  � ��$	 0q���  � ��
	 ���� ��  � ��	 �� "
 ��.R��    CommonStates��.   
Normal��  	MouseOver��  Disabled��  Pressed��  FocusStates��.   Focused��. �� .��W� $ 0D�$ FocusVisualElement��$ Opacity�$ 0X�  	Unfocused��.+��� $ 
Background��$ 3��
������ #��z��#��z��._�� #��z��$��1q�.+��� $ BackgroundAnimation��$ 0Q�
������ �� $ BackgroundGradient������ $ Transparent��$
 0.25Q��� $ contentPresenter��#��z��#��z��#��z��#��z��#��z���� $ DisabledVisualElement��$ 3��$ 3�����	#FFFFFFFF$ 0Q� .  �� $ FocusVisualElement��$ 2��$ 2��$��1q����	#FF6DBDD1$��1��$ 0Q� .    .��(� $
��auto��$
��auto�� (� Stretch$
 Fill=� (� StretchDirection$
 Both=�.���  � RowDefinitions �� $
��auto ��� $	��650 �.+�+  %System.Windows.Controls.Ribbon.Ribbon  $��Stretch=�$��2��$��Stretch=�.@�.  (System.Windows.Controls.Ribbon.RibbonTab  $��	ホーム��   KeyTip$ h��$��75��.@�0  *System.Windows.Controls.Ribbon.RibbonGroup  $��90��  $��F1��    KeyTip$  MASTER��  RibbonFunctionBackground#��[ .@�1  +System.Windows.Controls.Ribbon.RibbonButton  -   !   LargeImageSource$%! ../Images/15マスタ照会.png��$��0,-1,0,0q�"   ToolTipTitle$" マスタ照会(F1)��#   ToolTipDescription$3# -入力済みのデータを検索します。��$   ToolTipImageSource$%$ ../Images/15マスタ照会.png��%   Label$% ﾏｽﾀ照会��&   KeyTip$& F1��$��50��  $��F2��$  MASTER��#��[ .@�  $y�Hidden=�$%  ��$"  ��$#  ��  $��F3��$  MASTER��#��[ .@�  $y�Hidden=�$%  ��$"  ��$#  ��  $��F4��$  MASTER��#��[ .@�  $y�Hidden=�$%  ��$"  ��$#  ��  $��F5��$  MASTER��#��[ .@�  $y�Hidden=�$%  ��$"  ��$#  ��  $��F6��$  MASTER��#��[ .@�  $y�Hidden=�$%  ��$"  ��$#  ��  $��F7��$  MASTER��#��[ .@�  $y�Hidden=�$%  ��$"  ��$#  ��  $��F8��$  MASTER��#��[ .@�  $y�Hidden=�$%  ��$"  ��$#  ��  $��F9��$  MASTER��#��[ .@�  -   $! ../Images/24登録.png��$& F9��$��50��$% 登録��$" データ登録(F9)��$-# '入力データの登録を行います��$$ ../Images/24登録.png��y��� ' �� Path$' DataUpdateVisible�( �� Mode$( TwoWay=�) �� NotifyOnSourceUpdated) . * �� NotifyOnTargetUpdated* .   $	��F10��$  MASTER��#��[ .@�  -   $! ../Images/26クリア.png��$	& F10��$��50��$% 入力取消��$" 入力取消(F10)��$'# !入力の取消しを行います��$$ ../Images/26クリア.png��  $	��F11��$  MASTER��#��[ .@�  -   $! ../Images/27終了.png��$	& F11��$��50��$% 終了��$" 終了(F11)��$# 終了します��$$ ../Images/27終了.png��  $	��F12��$  MASTER��#��[ .@�  -   $! ../Images/28削除.png��$	& F12��$��50��$% 削除��$" 削除(F12)��$# 入力を削除します��$$ ../Images/28削除.png��y��� $' DataUpdateVisible�$( TwoWay=�) . * .   $��	ヘルプ��
�������.@�  $��サポート��.@�  -   $%! ../Images/30マニュアル.png��$& MANUAL��$% ﾏﾆｭｱﾙ��$" 取扱説明書��$*# $取り扱い説明書を開きます��  -   $(! "../Images/29遠隔サポート.png��$& SUPPORT��$% お問合せ��$" お問合せ��$<# 6Web上の｢お問合せ｣ページに遷移します��  -	   $! ../Images/QandA.png��$	& FAQ��$% QandA��$" QandA��$E# ?Web上の｢よくあるご質問｣ページに遷移します��  -
   $(! "../Images/13共栄Webリンク.png��$& KYOEI��$% 共栄ｼｽﾃﾑ��$" 共栄システムHP��$<# 6共栄システムのホームページを開きます��  $��実施状況入力��
�������+   ContextualTabGroupHeader$+ 入力補助��.@�  $'��!従来ファンクションキー��.@�  $%! ../Images/16マスタ入力.png��$& d��$% 	行追加��$" 行追加(F5)��$'# !売上の履歴を表示します��$%$ ../Images/16マスタ入力.png��  -   $! ../Images/24登録.png��$& e��$% マスタ登録��$" 伝票登録(F9)��$$# 伝票の登録を行います��$$ ../Images/24登録.png��  -   $! ../Images/26クリア.png��$& f��$% 入力取消し��$" 入力取消(F12)��$'# !入力の取消しを行います��$$ ../Images/26クリア.png��  -   $! ../Images/27終了.png��$& g��$% 終了��$" 入力取消(F11)��$# 終了します��$$ ../Images/27終了.png��  $��一括入力��
�������$+ 入力補助��.@�  $'��!従来ファンクションキー��.@�  $! ../Images/22リスト.png��$& d��$% プレビュー��$" プレビュー(F5)��$'# !売上の履歴を表示します��$$ ../Images/22リスト.png��  -   $! ../Images/24登録.png��$& e��$% 印刷��$" 
印刷(F9)��$$# 伝票の登録を行います��$$ ../Images/24登録.png��  -   $! ../Images/24登録.png��$& e��$% マスタ登録��$" マスタ登録(F9)��$$# 伝票の登録を行います��$$ ../Images/24登録.png��  -   $! ../Images/26クリア.png��$& f��$% 入力取消し��$" 入力取消(F12)��$'# !入力の取消しを行います��$$ ../Images/26クリア.png��  -   $! ../Images/27終了.png��$& g��$% 終了��$" 入力取消(F11)��$# 終了します��$$ ../Images/27終了.png��� $��0��$��Bottom=� �� $��74 �, � ColumnDefinitions, �� $��90 ��� $��* �.+�]� $��0��$��0��$��Stretch=�$��2q����� *MaintenanceModeBackground+.+��� $��メイリオ)�$��24(�$��Medium"�$��Stretch=�$��Center=�$��Center=�$��Center=����� *MaintenanceMode+���� *MaintenanceModeForeground+�� $��0��$��1��$��Bottom=�
��� ���
���  ��$��2,0,2,0q����� *ErrorMessage+y��� *ErrorMessageVisibility+�� $��1�����#2792c3$��1q�$��5q�
�������._�� $��15q� �� $��30 ��� $��10 ��� $��30 ��� $��5 ��� $��30 ��� $��10 ��� $��30 ��� $��5 ��� $��30 ��� $��5 ��� $��30 ��� $��10 ��� $��30 ��� $��5 ��� $��30 ��� $��10 ��� $��30 ��� $��5 ��� $��30 ��� $��5 ��� $��30 ��� $��10 ��� $��30 ��� $��10 ��� $��30 ��� $��10 ��� $��30 ��� $��10 ��� $
��Auto �, �� $
��auto ��� $��* �.+�� $��0��, �� $
��auto ��� $
��auto ��� $
��auto ��� $
��auto ��� $
��auto ��� $��2* � �� $��30 �.+�?	  9KyoeiSystem.Framework.Windows.Controls.UcLabelTwinTextBox	  -   $��	CompanyId��- 	  Label_Context$- 自社ID��. 	  Label_Width$	. 100Q�/ 	  DataAccessName$/ M70_JIS��0 	  MasterCheckEnabled0 .  =
  7KyoeiSystem.Framework.Windows.Controls.FrameworkControl1 
  	IsKeyItem1 . 2 	  Label1Visibility$2 	Collapsed=�3 	  
Text1Width$3 80Q�4 	  Text1MaxLength$4 3��5 	  Text1ValidationType$5 Integer=�6 	  Text2Visibility$6 	Collapsed=�7 	  Label2Visibility$7 	Collapsed=�8 	  Text18 �� $' 自社ID�$( TwoWay=�) . * . �� -   $��PageingFirst��$��1��$	��≪��$��10,0,2,2q�$��30��$��27��|�.  $��Bottom=��� -   $��PageingPrev��$��2��$	��＜��$��0,2,2,2q�$��30��|�.  �� -   $��PageingNext��$��3��$	��＞��$��2q�$��30��|�.  �� -   $��PageingLast��$��4��$	��≫��$��0,2,2,2q�$��30��|�.  ;  5KyoeiSystem.Framework.Windows.Controls.UcLabelTextBox  -   $��CompanyName��9   Label_Context$9 	自社名��$��2��:   Label_Width$	: 100Q�;   cWidth$	; 400Q�$
��Left=�<   cTextAlignment$
< Left=�=   ImeType$= Native=�>   
cMaxLength$> 40��?   Text? �� *	自社名+$( TwoWay=�) . * . @ �� UpdateSourceTrigger$@ PropertyChanged=�  -   $��RepresentativeName��$9 代表者名��$��4��$	: 100Q�$	; 400Q�$
��Left=�$
< Left=�$= Native=�$> 40��? �� *代表者名+$( TwoWay=�) . * . $@ PropertyChanged=�  -   $��PostCode��$9 郵便番号��$��6��$	: 100Q�$	; 120Q�$
��Left=�$
< Left=�$	= Off=�$> 8��? �� *郵便番号+$( TwoWay=�) . * . $@ PropertyChanged=�  -   $��Address1��$9 	住所１��$��8��$	: 100Q�$	; 400Q�$
��Left=�$
< Left=�$= Native=�$> 40��? �� *	住所１+$( TwoWay=�) . * . $@ PropertyChanged=�  -   $��Address2��$9 	住所２��$��10��$	: 100Q�$	; 400Q�$
��Left=�$
< Left=�$= Native=�$> 40��? �� *	住所２+$( TwoWay=�) . * . $@ PropertyChanged=�  -   $��PhoneNumber��$9 電話番号��$��12��$	: 100Q�$	; 120Q�$
��Left=�$
< Left=�$	= Off=�$> 15��? �� *電話番号+$( TwoWay=�) . * . $@ PropertyChanged=�  -   $��	FaxNumber��$9 ＦＡＸ番号��$��14��$	: 100Q�$	; 120Q�$
��Left=�$
< Left=�$	= Off=�$> 15��? �� *ＦＡＸ番号+$( TwoWay=�) . * . $@ PropertyChanged=�  -   $��
BankPayee1��$9 振込先銀行１��$��16��$	: 100Q�$	; 500Q�$
��Left=�$
< Left=�$= Native=�$> 50��? �� *振込先銀行１+$( TwoWay=�) . * . $@ PropertyChanged=�  -   $��
BankPayee2��$9 振込先銀行２��$��18��$	: 100Q�$	; 500Q�$
��Left=�$
< Left=�$= Native=�$> 50��? �� *振込先銀行２+$( TwoWay=�) . * . $@ PropertyChanged=�  -    $��
BankPayee3��$9 振込先銀行３��$��20��$	: 100Q�$	; 500Q�$
��Left=�$
< Left=�$= Native=�$> 50��? �� *振込先銀行３+$( TwoWay=�) . * . $@ PropertyChanged=�  -!   $��CorporationNumber��$9 法人ナンバー��$��22��$	: 100Q�$	; 150Q�$
��Left=�$
< Left=�$	= Off=�$> 20��? �� *法人ナンバー+$( TwoWay=�) . * . $@ PropertyChanged=�C  =KyoeiSystem.Framework.Windows.Controls.UcLabelTextRadioButton  -"   $��DisplayType��A   RadioViewCount$	A Two=�B   Label_Content$B 自社区分��$��24��C   Label_Width$	C 100Q�$��0,0,0,0q�D   
IsRequiredD .   validationTemplateE =� ErrorTemplate#E [ F   RadioSelectButton$	F One=�G   RadioOne_Content$G 自社��H   RadioSecond_Content$H 販社��I   TextI �� *自社区分+$( TwoWay=�) . * . $@ PropertyChanged=�;  5KyoeiSystem.Application.Windows.Views.M01_TOK_TextBox  -#   J  Name$J 	suppliers��$��26��$
��Left=�K   Label_Context$K 取引先コード��L   Label_Width$	L 100Q�M   DataAccessName$M M01_TOK��N   DataAccessMode$N On=�O   LinkItem$O 4��P   Label1Visibility$P 	Collapsed=�Q   Label2Visibility$Q 	Collapsed=�R   
Text1Width$R 40Q�S   Text1MaxLength$S 4��T   Text1IMEType$	T Off=�$U   Text1VerticalContentAlignment$U Center=�V   
Label1Text$	V －��W   
Text2Width$W 30Q�X   Text2MaxLength$X 2��Y   Text2IMEType$	Y Off=�$Z   Text2VerticalContentAlignment$Z Center=�[   
Label2Text$[  ��\   Text1\ �� *取引先コード+$( TwoWay=�) . * . ]   Text2] �� *取引先コード枝番+$( TwoWay=�) . * . �� -$   $��2��$��10��$	��300��$	��170��$��	10,0,10,0q�
���   �$��2q�
������ |�.  ^ J� 	AllowDrop^ . #��[  .���� $��Uniform=����� *ロゴ画像+�� -%   $��
ClearImage��$��10��$��1��$��	クリア��$	��100��$��27��|�.  $��Center=�$��Center=��� -&   $��	ImageInfo��$9��3※PNG形式のみ請求書に印刷されます。��$��12��$��1��
���  ��$
��Bold"�$��Center=�$��Center=�