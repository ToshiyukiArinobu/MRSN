   M S B A M L   `   `   `  ���� X  TKyoeiSystem.Framework.Windows, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null�[clr-namespace:KyoeiSystem.Framework.Windows.ViewBase;assembly=KyoeiSystem.Framework.Windows&KyoeiSystem.Framework.Windows.ViewBase  �[clr-namespace:KyoeiSystem.Framework.Windows.ViewBase;assembly=KyoeiSystem.Framework.Windows&KyoeiSystem.Framework.Windows.ViewBase  a ]KyoeiSystem.Framework.Windows.Controls, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null�dclr-namespace:KyoeiSystem.Framework.Windows.Controls;assembly=KyoeiSystem.Framework.Windows.Controls&KyoeiSystem.Framework.Windows.Controls Z VKyoeiSystem.Application.Windows, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]3clr-namespace:KyoeiSystem.Application.Windows.Views%KyoeiSystem.Application.Windows.Views [ WSystem.Windows.Forms, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089Y@clr-namespace:System.Windows.Forms;assembly=System.Windows.FormsSystem.Windows.Forms �dclr-namespace:KyoeiSystem.Framework.Windows.Controls;assembly=KyoeiSystem.Framework.Windows.Controls&KyoeiSystem.Framework.Windows.Controls O Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem 4   .KyoeiSystem.Application.Windows.Views.MST17010   bfw[clr-namespace:KyoeiSystem.Framework.Windows.ViewBase;assembly=KyoeiSystem.Framework.Windows  \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W	 SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     	 R
 NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml 
   	 Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006 
   	 @d2http://schemas.microsoft.com/expression/blend/2008 
   	 jvm[clr-namespace:KyoeiSystem.Framework.Windows.ViewBase;assembly=KyoeiSystem.Framework.Windows 
   	 uctlsdclr-namespace:KyoeiSystem.Framework.Windows.Controls;assembly=KyoeiSystem.Framework.Windows.Controls 
   	 Buc3clr-namespace:KyoeiSystem.Application.Windows.Views 
   	 Owf@clr-namespace:System.Windows.Forms;assembly=System.Windows.Forms 
   	 yControlsdclr-namespace:KyoeiSystem.Framework.Windows.Controls;assembly=KyoeiSystem.Framework.Windows.Controls 
   	 9System&clr-namespace:System;assembly=mscorlib 
   	 -   $��MainForm��  � Language$  ja-JP� � AllowsTransparency .   � WindowStyle$ SingleBorderWindow=� � WindowStartupLocation$ CenterOwner=�$	��728��$
��1024��$	��768��$
��1024�� � Left$ 0��
 � Top$ 0�� � Title$+ %仕入先商品売価設定(MST17010)�� � 
ResizeMode$ NoResize=� � 	Resources    DataGridHeaderBackground	 �� EndPoint
 �� 
StartPoint � Color � Offset��%A   &	        �� $		 0,1&�$	
 0,0&�.��� $ #3b5998��$ 0Q�.��(� $
��auto��$
��auto�� (� Stretch$
 Fill=� (� StretchDirection$
 Both=�.��� -    �Name$ BaseGrid�� � RowDefinitions �� $
��auto ��� $	��650 �.+�+  %System.Windows.Controls.Ribbon.Ribbon  $��Stretch=�$��Stretch=�.@�.  (System.Windows.Controls.Ribbon.RibbonTab  $��	ホーム��   KeyTip$ h��$��75��.@�0  *System.Windows.Controls.Ribbon.RibbonGroup  $��90��  $��F1��   KeyTip$ MASTER��  RibbonFunctionBackground#��[ .@�1  +System.Windows.Controls.Ribbon.RibbonButton  -      LargeImageSource$% ../Images/15マスタ照会.png��$��0,-1,0,0q�   Label$ ﾏｽﾀ照会��   KeyTip$ F1��$��50��  $��F2��$ MASTER��#��[ .@�  $y�Hidden=�$  ��   ToolTipTitle$  ��   ToolTipDescription$  ��  $��F3��$ MASTER��#��[ .@�  $y�Hidden=�$  ��$  ��$  ��  $��F4��$ MASTER��#��[ .@�  $y�Hidden=�$  ��$  ��$  ��  $��F5��$ MASTER��#��[ .@�  -   $ ../Images/行追加.png��$ F5��$��50��$ 	行追加��$ 行追加(F5)��$' !リストに行を追加します��   ToolTipImageSource$ ../Images/行追加.png��  $��F6��$ MASTER��#��[ .@�  -   $ ../Images/28削除.png��$ F6��$��50��$ 	行削除��$ 行削除(F6)��$0 *選択されている行を削除します��$ ../Images/28削除.png��  $��F7��$ MASTER��#��[ .@�  $y�Hidden=�$  ��$  ��$  ��  $��F8��$ MASTER��#��[ .@�  $y�Hidden=�$  ��$  ��$  ��  $��F9��$ MASTER��#��[ .@�  -   $ ../Images/24登録.png��$ F9��$��50��$ 登録��$ データ登録(F9)��$- '入力データの登録を行います��$ ../Images/24登録.png��y���  �� Path$ DataUpdateVisible� �� Mode$ TwoWay=� �� NotifyOnSourceUpdated .  �� NotifyOnTargetUpdated .   $	��F10��$ MASTER��#��[ .@�  -   $ ../Images/26クリア.png��$	 F10��$��50��$ 入力取消��$ 入力取消(F10)��$' !入力の取消しを行います��$ ../Images/26クリア.png��  $	��F11��$ MASTER��#��[ .@�  -   $ ../Images/27終了.png��$	 F11��$��50��$ 終了��$ 終了(F11)��$ 終了します��$ ../Images/27終了.png��  $	��F12��$ MASTER��#��[ .@�  $y�Hidden=�$  ��$  ��$  ��  $��	ヘルプ��
�������.@�  $��サポート��.@�  -	   $% ../Images/30マニュアル.png��$ MANUAL��$ ﾏﾆｭｱﾙ��$ 取扱説明書��$* $取り扱い説明書を開きます��  -
   $( "../Images/29遠隔サポート.png��$ SUPPORT��$ お問合せ��$ お問合せ��$< 6Web上の｢お問合せ｣ページに遷移します��  -   $ ../Images/QandA.png��$	 FAQ��$ QandA��$ QandA��$E ?Web上の｢よくあるご質問｣ページに遷移します��  -   $( "../Images/13共栄Webリンク.png��$ KYOEI��$ 共栄ｼｽﾃﾑ��$ 共栄システムHP��$< 6共栄システムのホームページを開きます��  $��実施状況入力��
�������   ContextualTabGroupHeader$ 入力補助��.@�  $'��!従来ファンクションキー��.@�  $% ../Images/16マスタ入力.png��$ d��$ 	行追加��$ 行追加(F5)��$' !売上の履歴を表示します��$% ../Images/16マスタ入力.png��  -   $ ../Images/24登録.png��$ e��$ マスタ登録��$ 伝票登録(F9)��$$ 伝票の登録を行います��$ ../Images/24登録.png��  -   $ ../Images/26クリア.png��$ f��$ 入力取消し��$ 入力取消(F12)��$' !入力の取消しを行います��$ ../Images/26クリア.png��  -   $ ../Images/27終了.png��$ g��$ 終了��$ 入力取消(F11)��$ 終了します��$ ../Images/27終了.png��  $��一括入力��
�������$ 入力補助��.@�  $'��!従来ファンクションキー��.@�  $ ../Images/22リスト.png��$ d��$ プレビュー��$ プレビュー(F5)��$' !売上の履歴を表示します��$ ../Images/22リスト.png��  -   $ ../Images/24登録.png��$ e��$ 印刷��$ 
印刷(F9)��$$ 伝票の登録を行います��$ ../Images/24登録.png��  -   $ ../Images/24登録.png��$ e��$ マスタ登録��$ マスタ登録(F9)��$$ 伝票の登録を行います��$ ../Images/24登録.png��  -   $ ../Images/26クリア.png��$ f��$ 入力取消し��$ 入力取消(F12)��$' !入力の取消しを行います��$ ../Images/26クリア.png��  -   $ ../Images/27終了.png��$ g��$ 終了��$ 入力取消(F11)��$ 終了します��$ ../Images/27終了.png��� $��0��$��Bottom=� �� $��74 � � ColumnDefinitions �� $��90 ��� $��* �.+�]� $��0��$��0��$��Stretch=�$��2q����� *MaintenanceModeBackground+.+��� $��メイリオ)�$��24(�$��Medium"�$��Stretch=�$��Center=�$��Center=�$��Center=����� *MaintenanceMode+���� *MaintenanceModeForeground+�� $��0��$��1��$��Bottom=�
��� ���
���  ��$��2,0,2,0q����� *ErrorMessage+y��� *ErrorMessageVisibility+�� $��1�����#2792c3$��1q�$��5q�
�������._�� $��65,15,15,15q� �� $��40 ��� $
��auto ��� $��10 ��� $
��auto ��� $��10 ��� $��* � �� $
��auto �.+��� $!��仕入先商品売価設定��$��0��$
��Left=�$	��Top=�$��16(�$��SemiBold"�� $��1�� �� $
��auto ��� $	��100 � �� $��30 ��� $��30 ��� $��30 �.+�;  5KyoeiSystem.Application.Windows.Views.M01_TOK_TextBox  -     Name$ 
SHIIRESAKI��$��0��$��0��$
��Left=�=  7KyoeiSystem.Framework.Windows.Controls.FrameworkControl    	IsKeyItem  . $��1q�!   DataAccessName$! M01_TOK��"   DataAccessMode$" On=�#   LinkItem$	# 1,3��$   Label_Context$$ 仕入先コード��%   Label_Width$	% 100Q�&   
Label1Text$	& －��'   Label1Visibility$' Visible=�(   
Text1Width$( 40Q�)   Text1MaxLength$) 4��*   Text1IMEType$	* Off=�+   Text1ValidationType$+ Integer=�,   
Text2Width$, 30Q�-   Text2MaxLength$- 2��.   Text2IMEType$	. Off=�/   Text2ValidationType$/ Integer=�?  9KyoeiSystem.Framework.Windows.Controls.UcLabelTwinTextBox  -   0  Name$0 HINBAN��$��1��$��0��$
��Left=�  . $��1q�1   DataAccessName$1 	M09_MYHIN��2   DataAccessMode$2 On=�3   Label_Context$3 自社品番��4   Label_Width$	4 100Q�5   Label1Visibility$5 	Collapsed=�6   
Text1Width$	6 100Q�7   Text1MaxLength$7 12��8   Text1IMEType$	8 Off=�9   
Text2Width$	9 250Q�:   Text2IsReadOnly: . ;   Text2BorderThickness$; 0q�<   Text2Background
< ����   -   $0 COLOR��$��3��$��0��$
��Left=�  . $��1q�$1 M06_IRO��$2 On=�$	3 色��$	4 100Q�$5 	Collapsed=�$6 40Q�$7 12��$	8 Off=�$	9 310Q�: . $; 0q�
< ���� �� -   = ��Name$= BtnStart��$��0��$��1��$��2��$��	0,15,0,15q�$��表示開始��� $��3�� �� $
��auto � �� $
��auto �.+��� $��絞込条件��$��13(�
�������$��3q�$��5q�.���  �� $
��auto ��� $
��auto � �� $
��auto ��� $
��auto �.+�  -   $0 HINGUN��$��0��$��0��$
��Left=�$3 商品群コード��$	4 100Q�$1 
M16_HINGUN��$2 On=�$5 	Collapsed=�$6 40Q�$7 3��$	8 Off=�$	9 220Q�: . $; 0q�
< ���� C  =KyoeiSystem.Framework.Windows.Controls.UcLabelTextRadioButton  -   >  Name$> ITEM_KBN��$��1��?   Label_Content$? 商品区分��@   Label_Width$	@ 100Q�A   RadioSelectButton$	A One=�B   RadioOneStartB . C   RadioViewCount$
C Five=�D   RadioSecond_Content$D SET品��E   RadioThird_Content$E 単品��F   RadioFourth_Content$F 材料��G   RadioFifth_Content$G 	副資材��H   Text$H  ��;	  5KyoeiSystem.Framework.Windows.Controls.UcLabelTextBox	  -   I 	 Name$I SHIIRE_NAME��$��0��$��1��$
��Left=�$	��350��J 	  Label_Context$J 仕入先名��K 	  Label_Width$	K 100Q�L 	  
cMaxLength$L 30��M 	  ImeType$M Native=�	  -   $I 	ITEM_NAME��$��1��$��1��$
��Left=�$	��350��$J 自社品名��$	K 100Q�$L 30��$M Native=�&
   System.Windows.Controls.DataGrid
  -   $��
SearchGrid��$��5��$	��Top=�$
��Left=�N 
  SelectionMode$N Single=�O 
  SelectionUnit$O CellOrRowHeader=�P 
  AutoGenerateColumnsP .  Q 
  CanUserSortColumnsQ .  R 
  CanUserAddRowsR .  S 
  CanUserDeleteRowsS .  ���� *SearchResult+$ TwoWay=� .  . T 
  ColumnHeaderStyleT �� U �� 
TargetType=  7System.Windows.Controls.Primitives.DataGridColumnHeader"U  .��� V �� PropertyV � ��W �� Value$W 25���� V � ��
W ������� V � ��$	W 0.5q��� V � ��
W �   ��� V � ��$W Center=��� GridHeaderStye��V � ��#W [  X 
  ColumnsX 0  *System.Windows.Controls.DataGridTextColumn  -   Grid_SupplierCode��,  &System.Windows.Controls.DataGridColumnY   Header$Y 仕入先コード��Z   Width5  /System.Windows.Controls.DataGridLengthConverter$Z 80 [   
IsReadOnly[ . 1  +System.Windows.Controls.DataGridBoundColumn\   Binding\ �� *仕入先コード+  -   Grid_SupplierEda��$Y 枝番��$Z 80 [ . \ �� *仕入先コード枝番+  -   Grid_SupplierName��$Y 仕入先名��$	Z 200 [ . \ �� *仕入先名１+  -    Grid_ProductNumber��$Y 品番��$Z 80 ]   MinWidth$] 80Q�[ . \ �� *品番コード+^ �� ValidatesOnExceptions^ .   -!   Grid_ProductName��$Y 品名��$	Z 300 [ . \ �� *品番名称+  -"   
Grid_Price��$Y 単価��$Z 60 $] 60Q�\ �� *単価+^ . _ �� StringFormat$_ #,##0.##��`   ElementStyle` �� "U ��.��� V �@��TextAlignment$W Right=�4  .System.Windows.Controls.DataGridCheckBoxColumn  -#   Grid_Delete��a   
Visibility$a Hidden=�\ �� *論理削除+