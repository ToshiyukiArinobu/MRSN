   M S B A M L   `   `   `  ���� X  TKyoeiSystem.Framework.Windows, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null�[clr-namespace:KyoeiSystem.Framework.Windows.ViewBase;assembly=KyoeiSystem.Framework.Windows&KyoeiSystem.Framework.Windows.ViewBase  �[clr-namespace:KyoeiSystem.Framework.Windows.ViewBase;assembly=KyoeiSystem.Framework.Windows&KyoeiSystem.Framework.Windows.ViewBase  a ]KyoeiSystem.Framework.Windows.Controls, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null�dclr-namespace:KyoeiSystem.Framework.Windows.Controls;assembly=KyoeiSystem.Framework.Windows.Controls&KyoeiSystem.Framework.Windows.Controls R NSystem.Data, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089>.clr-namespace:System.Data;assembly=System.DataSystem.Data # KyoeiSystem.Application.Windows4   .KyoeiSystem.Application.Windows.Views.MST90100   bfw[clr-namespace:KyoeiSystem.Framework.Windows.ViewBase;assembly=KyoeiSystem.Framework.Windows  \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      R	 NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml 	    Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006 	    @d2http://schemas.microsoft.com/expression/blend/2008 	    jvm[clr-namespace:KyoeiSystem.Framework.Windows.ViewBase;assembly=KyoeiSystem.Framework.Windows 	    uctlsdclr-namespace:KyoeiSystem.Framework.Windows.Controls;assembly=KyoeiSystem.Framework.Windows.Controls 	    e
 aGrapeCity.WPF.SpreadGrid, Version=1.0.2015.1109, Culture=neutral, PublicKeyToken=5c5c8ff7a6858ccc=sg4http://schemas.grapecity.com/windows/spreadgrid/2012 
 9Data.clr-namespace:System.Data;assembly=System.Data 
 -     � AllowsTransparency  .   � WindowStartupLocation$ CenterOwner=�$	��728��$
��1024��$	��768��$
��1024�� � Title$% 権限マスタ入力(MST90100)��.��(� $
��auto��$
��auto�� (� Stretch$
 Fill=� (� StretchDirection$
 Both=�.���  � RowDefinitions �� $
��auto ��� $	��650 �.+�+  %System.Windows.Controls.Ribbon.Ribbon  $��Stretch=�$��Stretch=�.@�.  (System.Windows.Controls.Ribbon.RibbonTab  $��	ホーム��   KeyTip$ h��$��75��.@�0  *System.Windows.Controls.Ribbon.RibbonGroup  $��90��  $��F1��   KeyTip$ MASTER��   RibbonFunctionBackground#��[  .@�1  +System.Windows.Controls.Ribbon.RibbonButton  $y�Hidden=�   Label$  ��	   ToolTipTitle$	  ��
   ToolTipDescription$
  ��  $��F2��$ MASTER��#��[  .@�  $y�Hidden=�$  ��$	  ��$
  ��  $��F3��$ MASTER��#��[  .@�  $y�Hidden=�$  ��$	  ��$
  ��  $��F4��$ MASTER��#��[  .@�  $y�Hidden=�$  ��$	  ��$
  ��  $��F5��$ MASTER��#��[  .@�  $y�Hidden=�$  ��$	  ��$
  ��  $��F6��$ MASTER��#��[  .@�  $y�Hidden=�$  ��$	  ��$
  ��  $��F7��$ MASTER��#��[  .@�  $y�Hidden=�$  ��$	  ��$
  ��  $��F8��$ MASTER��#��[  .@�  $y�Hidden=�$  ��$	  ��$
  ��  $��F9��$ MASTER��#��[  .@�  -      LargeImageSource$ ../Images/24登録.png��   KeyTip$ F9��$��50��$ 登録��$	 データ登録(F9)��$-
 '入力データの登録を行います��   ToolTipImageSource$ ../Images/24登録.png��y���  �� Path$ DataUpdateVisible� �� Mode$ TwoWay=� �� NotifyOnSourceUpdated .  �� NotifyOnTargetUpdated .   $	��F10��$ MASTER��#��[  .@�  -   $ ../Images/26クリア.png��$	 F10��$��50��$ 入力取消��$	 入力取消(F10)��$'
 !入力の取消しを行います��$ ../Images/26クリア.png��  $	��F11��$ MASTER��#��[  .@�  -   $ ../Images/27終了.png��$	 F11��$��50��$ 終了��$	 終了(F11)��$
 終了します��$ ../Images/27終了.png��  $	��F12��$ MASTER��#��[  .@�  $y�Hidden=�$  ��$	  ��$
  ��  $��	ヘルプ��
�������.@�  $��サポート��.@�  -   $% ../Images/30マニュアル.png��$ MANUAL��$ ﾏﾆｭｱﾙ��$	 取扱説明書��$*
 $取り扱い説明書を開きます��  -   $( "../Images/29遠隔サポート.png��$ SUPPORT��$ お問合せ��$	 お問合せ��$<
 6Web上の｢お問合せ｣ページに遷移します��  -   $ ../Images/QandA.png��$	 FAQ��$ QandA��$	 QandA��$E
 ?Web上の｢よくあるご質問｣ページに遷移します��  -   $( "../Images/13共栄Webリンク.png��$ KYOEI��$ 共栄ｼｽﾃﾑ��$	 共栄システムHP��$<
 6共栄システムのホームページを開きます��  $��実施状況入力��
�������   ContextualTabGroupHeader$ 入力補助��.@�  $'��!従来ファンクションキー��.@�  $% ../Images/16マスタ入力.png��$ d��$ 	行追加��$	 行追加(F5)��$'
 !売上の履歴を表示します��$% ../Images/16マスタ入力.png��  -	   $ ../Images/24登録.png��$ e��$ マスタ登録��$	 伝票登録(F9)��$$
 伝票の登録を行います��$ ../Images/24登録.png��  -
   $ ../Images/26クリア.png��$ f��$ 入力取消し��$	 入力取消(F12)��$'
 !入力の取消しを行います��$ ../Images/26クリア.png��  -   $ ../Images/27終了.png��$ g��$ 終了��$	 入力取消(F11)��$
 終了します��$ ../Images/27終了.png��  $��一括入力��
�������$ 入力補助��.@�  $'��!従来ファンクションキー��.@�  $ ../Images/22リスト.png��$ d��$ プレビュー��$	 プレビュー(F5)��$'
 !売上の履歴を表示します��$ ../Images/22リスト.png��  -   $ ../Images/24登録.png��$ e��$ 印刷��$	 
印刷(F9)��$$
 伝票の登録を行います��$ ../Images/24登録.png��  -   $ ../Images/24登録.png��$ e��$ マスタ登録��$	 マスタ登録(F9)��$$
 伝票の登録を行います��$ ../Images/24登録.png��  -   $ ../Images/26クリア.png��$ f��$ 入力取消し��$	 入力取消(F12)��$'
 !入力の取消しを行います��$ ../Images/26クリア.png��  -   $ ../Images/27終了.png��$ g��$ 終了��$	 入力取消(F11)��$
 終了します��$ ../Images/27終了.png��� $��0��$��Bottom=� �� $��74 � � ColumnDefinitions �� $��90 ��� $��* �.+�]� $��0��$��0��$��Stretch=�$��2q����� *MaintenanceModeBackground+.+��� $��メイリオ)�$��24(�$��Medium"�$��Stretch=�$��Center=�$��Center=�$��Center=����� *MaintenanceMode+���� *MaintenanceModeForeground+�� $��0��$��1��$��Bottom=�
��� ���
���  ��$��2,0,2,0q����� *ErrorMessage+y��� *ErrorMessageVisibility+�� $��1�����#2792c3$��1q�$��5q�
�������._�� $��15q� �� $��30 ��� $��* � �� $��2* �.+�� $��0�� �� $
��auto ��� $
��auto ��� $
��auto ��� $
��auto ��� $��2* � �� $��30 �.+�?  9KyoeiSystem.Framework.Windows.Controls.UcLabelTwinTextBox  -      DataAccessName$ M74_AUTHORITY_NAME��   DataAccessMode$ On=�   MasterCheckEnabled .  =  7KyoeiSystem.Framework.Windows.Controls.FrameworkControl   	IsKeyItem .    Label_Context$ グループ権限ID��   Label_Width$	 100Q�   Label1Visibility$ 	Collapsed=�   Label2Visibility$ 	Collapsed=�   
Text1Width$ 80Q�   Text1MaxLength$ 1��   Text1ValidationType$ Integer=�   Text1TextAlignment$ Right=�    
Text2Width$	  125Q�!   Text1! �� $ IN_グループ権限ID�$ TwoWay=� .  . "   Text2" �� $ IN_グループ権限名�$ TwoWay=� .  . �� -   $��BtnNewID��$��1��|�.  $��新規権限ID取得��$	��150��� $��1�� �� $
��auto � �� $��30 ��� $��* �.+��  �� $
��auto ��� $
��auto �.+�;  5KyoeiSystem.Framework.Windows.Controls.UcLabelTextBox  -   $��
txbGrpName��$��0�� .  #   ImeType$# Native=�$   Label_Context$$ グループ権限名��%   Label_Width$	% 100Q�&   cWidth$	& 205Q�'   Text' �� $ IN_グループ権限名�$ TwoWay=� .  . � $��1��$��	150,0,0,0q� �� $
��auto ��� $
��auto �.+��� -   $��EnableAllChange��$��0��$��0,2,2,2q�|�.  $��使用可能全変更��$	��150���� -   $��UpdateAllChange��$��1��$��0,2,2,2q�|�.  $��データ更新全変更��$	��150��/ 
 )GrapeCity.Windows.SpreadGrid.GcSpreadGrid  -   $ ��sp権限マスタデータ��$��1��$��Stretch=�$��Stretch=�(   AutoGenerateColumns( .  )   HorizontalScrollMode$) Column=�*   ColumnDragMode$* Default=�+   CanUserResizeColumns+ .  ,   CanUserResizeRows, .  $-   HorizontalScrollBarVisibility$- Visible=�".   VerticalScrollBarVisibility$. Visible=�/   DocumentUri$K/ E/KyoeiSystem.Application.Windows;component/views/MST/MST90100G1.sgxml?�0   ItemsSource0 �� *権限マスタデータ+$ TwoWay=� .  . 1 �� UpdateSourceTrigger$1 PropertyChanged=�2   Columns2 )	 
 #GrapeCity.Windows.SpreadGrid.Column	  3 	  Locked$
3 TrueX�4 	  HorizontalAlignment$
4 leftX�5 	  CanUserSort$5 FalseX�6 	  CanUserFilter$
6 TrueX�7 	  	DataField7 3
 
 -GrapeCity.Windows.SpreadGrid.BindingDataField
  8 
  Binding8 �� *メニュー名称+	  $
3 TrueX�$
4 LeftX�7 
  8 �� *プログラム名称+	  $
3 TrueX�$4 CenterX�7 
  8 �� *プログラムID+	  $4 CenterX�9 	  CellType9 3 
 -GrapeCity.Windows.SpreadGrid.CheckBoxCellType  7 
  8 �� *使用可能FLG+	  $4 CenterX�: 	  	IsVisible: .  9   7 
  8 �� *データ更新FLG+	  $
3 TrueX�$4 CenterX�9 3 
 -GrapeCity.Windows.SpreadGrid.DateTimeCellType  ;   AllowDropDownOpen; .  ; 
 5GrapeCity.Windows.SpreadGrid.FieldsEditorBaseCellType<   	AllowSpin< .  =   AutoOpenDropDown= .  >   DropDownButtonVisibility$> NotShow=�?   SpinButtonVisibility$? NotShow=�7 
  8 �� *登録日時+	  $
3 TrueX�$4 CenterX�9   ; .  < .  = .  $> NotShow=�$? NotShow=�7 
  8 �� *更新日時+