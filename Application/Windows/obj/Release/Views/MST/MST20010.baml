   M S B A M L   `   `   `  ���� X  TKyoeiSystem.Framework.Windows, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null�[clr-namespace:KyoeiSystem.Framework.Windows.ViewBase;assembly=KyoeiSystem.Framework.Windows&KyoeiSystem.Framework.Windows.ViewBase  �[clr-namespace:KyoeiSystem.Framework.Windows.ViewBase;assembly=KyoeiSystem.Framework.Windows&KyoeiSystem.Framework.Windows.ViewBase  a ]KyoeiSystem.Framework.Windows.Controls, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null�dclr-namespace:KyoeiSystem.Framework.Windows.Controls;assembly=KyoeiSystem.Framework.Windows.Controls&KyoeiSystem.Framework.Windows.Controls [ WSystem.Windows.Forms, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089Y@clr-namespace:System.Windows.Forms;assembly=System.Windows.FormsSystem.Windows.Forms �dclr-namespace:KyoeiSystem.Framework.Windows.Controls;assembly=KyoeiSystem.Framework.Windows.Controls&KyoeiSystem.Framework.Windows.Controls O Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem # KyoeiSystem.Application.Windows4   .KyoeiSystem.Application.Windows.Views.MST20010   bfw[clr-namespace:KyoeiSystem.Framework.Windows.ViewBase;assembly=KyoeiSystem.Framework.Windows  \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W	 SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     	 R
 NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml 
   	 Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006 
   	 @d2http://schemas.microsoft.com/expression/blend/2008 
   	 jvm[clr-namespace:KyoeiSystem.Framework.Windows.ViewBase;assembly=KyoeiSystem.Framework.Windows 
   	 uctlsdclr-namespace:KyoeiSystem.Framework.Windows.Controls;assembly=KyoeiSystem.Framework.Windows.Controls 
   	 Owf@clr-namespace:System.Windows.Forms;assembly=System.Windows.Forms 
   	 yControlsdclr-namespace:KyoeiSystem.Framework.Windows.Controls;assembly=KyoeiSystem.Framework.Windows.Controls 
   	 9System&clr-namespace:System;assembly=mscorlib 
   	 -     � Language$  ja-JP� � WindowStyle$ SingleBorderWindow=� � AllowsTransparency .  $	��728��$
��1024��$	��768��$
��1024�� � WindowStartupLocation$ CenterOwner=� � Left$ 0��
 � Top$ 0�� � Title$% 得意先品番登録(MST20010)�� � 	Resources    DataGridHeaderBackground �� EndPoint	 �� 
StartPoint
 � Color � Offset��%A   &	        �� $	 0,1&�$		 0,0&�.��� $
 #3b5998��$ 0Q�.��(� $
��auto��$
��auto�� (� Stretch$
 Fill=� (� StretchDirection$
 Both=�.��� -    �Name$ BaseGrid�� � RowDefinitions �� $
��auto ��� $	��650 �.+�+  %System.Windows.Controls.Ribbon.Ribbon  $��Stretch=�$��Stretch=�.@�.  (System.Windows.Controls.Ribbon.RibbonTab  $��	ホーム��   KeyTip$ h��$��75��.@�0  *System.Windows.Controls.Ribbon.RibbonGroup  $��90��  $��F1��   KeyTip$ MASTER��  RibbonFunctionBackground#��[ .@�1  +System.Windows.Controls.Ribbon.RibbonButton  -      LargeImageSource$% ../Images/15マスタ照会.png��$��0,-1,0,0q�   Label$ ﾏｽﾀ照会��   KeyTip$ F1��$��50��  $��F2��$ MASTER��#��[ .@�  $y�Hidden=�$  ��   ToolTipTitle$  ��   ToolTipDescription$  ��  $��F3��$ MASTER��#��[ .@�  $y�Hidden=�$  ��$  ��$  ��  $��F4��$ MASTER��#��[ .@�  $y�Hidden=�$  ��$  ��$  ��  $��F5��$ MASTER��#��[ .@�  -   $ ../Images/行追加.png��$ F5��$��50��$ 	行追加��$ 行追加(F5)��$' !リストに行を追加します��   ToolTipImageSource$ ../Images/行追加.png��  $��F6��$ MASTER��#��[ .@�  -   $ ../Images/28削除.png��$ F6��$��50��$ 	行削除��$ 行削除(F6)��$0 *選択されている行を削除します��$ ../Images/28削除.png��  $��F7��$ MASTER��#��[ .@�  $y�Hidden=�$  ��$  ��$  ��  $��F8��$ MASTER��#��[ .@�  $y�Hidden=�$  ��$  ��$  ��  $��F9��$ MASTER��#��[ .@�  -   $ ../Images/24登録.png��$ F9��$��50��$ 登録��$ データ登録(F9)��$- '入力データの登録を行います��$ ../Images/24登録.png��y���  �� Path$ DataUpdateVisible� �� Mode$ TwoWay=� �� NotifyOnSourceUpdated .  �� NotifyOnTargetUpdated .   $	��F10��$ MASTER��#��[ .@�  -   $ ../Images/26クリア.png��$	 F10��$��50��$ 入力取消��$ 入力取消(F10)��$' !入力の取消しを行います��$ ../Images/26クリア.png��  $	��F11��$ MASTER��#��[ .@�  -   $ ../Images/27終了.png��$	 F11��$��50��$ 終了��$ 終了(F11)��$ 終了します��$ ../Images/27終了.png��  $	��F12��$ MASTER��#��[ .@�  $y�Hidden=�$  ��$  ��$  ��  $��	ヘルプ��
�������.@�  $��サポート��.@�  -	   $% ../Images/30マニュアル.png��$ MANUAL��$ ﾏﾆｭｱﾙ��$ 取扱説明書��$* $取り扱い説明書を開きます��  -
   $( "../Images/29遠隔サポート.png��$ SUPPORT��$ お問合せ��$ お問合せ��$< 6Web上の｢お問合せ｣ページに遷移します��  -   $ ../Images/QandA.png��$	 FAQ��$ QandA��$ QandA��$E ?Web上の｢よくあるご質問｣ページに遷移します��  -   $( "../Images/13共栄Webリンク.png��$ KYOEI��$ 共栄ｼｽﾃﾑ��$ 共栄システムHP��$< 6共栄システムのホームページを開きます��  $��実施状況入力��
�������   ContextualTabGroupHeader$ 入力補助��.@�  $'��!従来ファンクションキー��.@�  $% ../Images/16マスタ入力.png��$ d��$ 	行追加��$ 行追加(F5)��$' !売上の履歴を表示します��$% ../Images/16マスタ入力.png��  -   $ ../Images/24登録.png��$ e��$ マスタ登録��$ 伝票登録(F9)��$$ 伝票の登録を行います��$ ../Images/24登録.png��  -   $ ../Images/26クリア.png��$ f��$ 入力取消し��$ 入力取消(F12)��$' !入力の取消しを行います��$ ../Images/26クリア.png��  -   $ ../Images/27終了.png��$ g��$ 終了��$ 入力取消(F11)��$ 終了します��$ ../Images/27終了.png��  $��一括入力��
�������$ 入力補助��.@�  $'��!従来ファンクションキー��.@�  $ ../Images/22リスト.png��$ d��$ プレビュー��$ プレビュー(F5)��$' !売上の履歴を表示します��$ ../Images/22リスト.png��  -   $ ../Images/24登録.png��$ e��$ 印刷��$ 
印刷(F9)��$$ 伝票の登録を行います��$ ../Images/24登録.png��  -   $ ../Images/24登録.png��$ e��$ マスタ登録��$ マスタ登録(F9)��$$ 伝票の登録を行います��$ ../Images/24登録.png��  -   $ ../Images/26クリア.png��$ f��$ 入力取消し��$ 入力取消(F12)��$' !入力の取消しを行います��$ ../Images/26クリア.png��  -   $ ../Images/27終了.png��$ g��$ 終了��$ 入力取消(F11)��$ 終了します��$ ../Images/27終了.png��� $��0��$��Bottom=� �� $��74 � � ColumnDefinitions �� $��90 ��� $��* �.+�]� $��0��$��0��$��Stretch=�$��2q����� *MaintenanceModeBackground+.+��� $��メイリオ)�$��24(�$��Medium"�$��Stretch=�$��Center=�$��Center=�$��Center=����� *MaintenanceMode+���� *MaintenanceModeForeground+�� $��0��$��1��$��Bottom=�
��� ���
���  ��$��2,0,2,0q����� *ErrorMessage+y��� *ErrorMessageVisibility+�� $��1�����#2792c3$��1q�$��5q�
�������._�� $��65,15,15,15q� �� $��40 ��� $
��auto ��� $��10 ��� $
��auto ��� $��10 ��� $��* � �� $
��auto �.+��� $��得意先品番登録��$��0��$
��Left=�$	��Top=�$��16(�$��SemiBold"�� $��1�� �� $
��auto ��� $	��100 � �� $��30 ��� $��30 ��� $��30 �.+�?  9KyoeiSystem.Framework.Windows.Controls.UcLabelTwinTextBox  -     Name$ 	TOKUISAKI��   Label_Context$ 得意先コード��$��0��$��0��    Label_Width$	  100Q�!   DataAccessName$! M01_TOK��"   DataAccessMode$	" Off=�#   LinkItem$	# 0,3��$
��Left=�=  7KyoeiSystem.Framework.Windows.Controls.FrameworkControl$   	IsKeyItem$ . %   
Label1Text$	% －��&   Label1Visibility$& Visible=�'   
Text1Width$' 40Q�(   Text1MaxLength$( 4��)   Text1IMEType$	) Off=�*   Text1ValidationType$* Integer=�+   
Text2Width$+ 30Q�,   Text2MaxLength$, 2��-   Text2IMEType$	- Off=�.   Text2ValidationType$. Integer=�/   
Label2Text$/  ��  -   $ HINBAN��$ 自社品番��$��1��$��0��$	  100Q�$! 	M09_MYHIN��$
��Left=�$ . $& 	Collapsed=�$	' 100Q�$( 12��$	) Off=�$	+ 250Q�0   Text2IsReadOnly0 . 1   Text2BorderBrush
1 ����   -   $ COLOR��$	 色��$��3��$��0��$	  100Q�$! M06_IRO��$
��Left=�$ . $& 	Collapsed=�$' 40Q�$( 12��$	) Off=�$	+ 310Q�0 . 
1 ���� �� -   2 ��Name$2 BtnStart��$��表示開始��$��0��$��1��$��2��$��	0,15,0,15q�� $��3�� �� $
��auto � �� $
��auto �.+��� $��絞込条件��$��13(�
�������$��3q�$��5q�.���  �� $
��auto ��� $
��auto � �� $
��auto ��� $
��auto �.+�  -   $ HINGUN��$ 商品群コード��$��0��$��0��$	  100Q�$! 
M16_HINGUN��$
��Left=�$& 	Collapsed=�$' 40Q�$( 3��$	) Off=�$	+ 220Q�0 . 
1 ���� C  =KyoeiSystem.Framework.Windows.Controls.UcLabelTextRadioButton  -   3  Name$3 ITEM_KBN��4   Label_Content$4 商品区分��$��1��5   Label_Width$	5 100Q�6   RadioSelectButton$	6 One=�7   RadioOneStart7 . 8   RadioViewCount$
8 Five=�9   RadioSecond_Content$9 SET品��:   RadioThird_Content$: 単品��;   RadioFourth_Content$; 材料��<   RadioFifth_Content$< 	副資材��=   Text$=  ��;  5KyoeiSystem.Framework.Windows.Controls.UcLabelTextBox  -   >  Name$> 
TOKUI_NAME��?   Label_Context$? 得意先名��$��0��$��1��@   Label_Width$	@ 100Q�A   ImeType$A Native=�B   
cMaxLength$B 30��$
��Left=�$	��350��  -   $> 	ITEM_NAME��$? 自社品名��$��1��$��1��$	@ 100Q�$A Native=�$B 30��$
��Left=�$	��350��&	   System.Windows.Controls.DataGrid	  -   $��
SearchGrid��$��5��C 	  CanUserSortColumnsC .  D 	  SelectionMode$D Single=�E 	  SelectionUnit$E CellOrRowHeader=�F 	  CanUserAddRowsF .  G 	  CanUserDeleteRowsG .  H 	  AutoGenerateColumnsH .  $	��Top=�$
��Left=����� *SearchResult+$ TwoWay=� .  . I 	  ColumnHeaderStyleI �� J �� 
TargetType=
  7System.Windows.Controls.Primitives.DataGridColumnHeader"J 
 .��� K �� PropertyK � ��L �� Value$L 25���� K � ��
L ������� K � ��$	L 0.5q��� K � ��
L �   ��� K � ��$L Center=��� GridHeaderStye��K � ��#L [  M 	  ColumnsM 0  *System.Windows.Controls.DataGridTextColumn  -   Grid_SupplierCode��,  &System.Windows.Controls.DataGridColumnN   Header$N 得意先コード��O   
Visibility$O Hidden=�P   
IsReadOnlyP . 1  +System.Windows.Controls.DataGridBoundColumnQ   BindingQ �� *得意先コード+  -   Grid_SupplierEda��$N 得意先枝番��$O Hidden=�P . Q �� *枝番+  -   Grid_SupplierName��$N 得意先名��$O Hidden=�P . R   Width5  /System.Windows.Controls.DataGridLengthConverter$	R 200 Q �� *得意先名１+  -    Grid_ProductNumber��$N 品番��$R 80 S   MinWidth$S 80Q�P . Q �� *品番コード+$ TwoWay=� .  . T �� ValidatesOnExceptionsT .   -!   Grid_ProductName��$N 品名��$	R 300 P . Q �� *品番名称+  -"   Grid_CustomerProduct��$N 客先品番��$	R 200 $S 80Q�P .  Q �� *得意先品番+$ TwoWay=� .  . T . 4  .System.Windows.Controls.DataGridCheckBoxColumn  -#   Grid_Delete��$O Hidden=�Q �� *論理削除+