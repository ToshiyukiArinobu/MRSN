   M S B A M L   `   `   `  ���� X  TKyoeiSystem.Framework.Windows, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null�[clr-namespace:KyoeiSystem.Framework.Windows.ViewBase;assembly=KyoeiSystem.Framework.Windows&KyoeiSystem.Framework.Windows.ViewBase  a ]KyoeiSystem.Framework.Windows.Controls, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null�dclr-namespace:KyoeiSystem.Framework.Windows.Controls;assembly=KyoeiSystem.Framework.Windows.Controls&KyoeiSystem.Framework.Windows.Controls ! KyoeiSystem.Framework.Reports@   :KyoeiSystem.Framework.Reports.Preview.PrinterSettingWindow   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35n jSAPBusinessObjects.WPF.ViewerShared, Version=13.0.3500.0, Culture=neutral, PublicKeyToken=692fbea5521e1304J 9http://schemas.microsoft.com/winfx/2006/xaml/presentation       nfw[clr-namespace:KyoeiSystem.Framework.Windows.ViewBase;assembly=KyoeiSystem.Framework.Windows       R	 NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml 	    Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006 	    @d2http://schemas.microsoft.com/expression/blend/2008 	    uctlsdclr-namespace:KyoeiSystem.Framework.Windows.Controls;assembly=KyoeiSystem.Framework.Windows.Controls 	    -     � 
ResizeMode$  NoResize=� � WindowStartupLocation$ CenterOwner=� � WindowStyle$ 
ToolWindow=�$	��540��$	��457�� � Title$ プリンター設定�� � 	Resources    ButtonFocusVisual �� 
TargetType �� Property �� Value J� SnapsToDevicePixels	 �� StrokeDashArray  ButtonNormalBackground
 �� EndPoint �� 
StartPoint � Color � Offset  ButtonNormalBorder �� Color��%  &	        &	 S     &	 �     �� " ��.���  � �� �� .R��� $��2q� . #��� ��$��1��$		 1 2R��� $	
 0,1&�$	 0,0&�.��� $ #F3F3F3��$ 0Q�� $ #EBEBEB��$	 0.5Q�� $ #DDDDDD��$	 0.5Q�� $ #CDCDCD��$ 1Q��� $ 	#FF707070��.���� $��8q�.+���  �� Orientation$ Vertical=�.+��� $��用紙設定��
����ح�<  6KyoeiSystem.Framework.Windows.Controls.UcLabelComboBox  -   $��4q�   Label_Context$ プリンター名��   Label_Width$	 116Q�   Label_Margin$ 0,0,4,0q�$��28��=  7KyoeiSystem.Framework.Windows.Controls.FrameworkControl   	IsEnabled �� *IsPageSizeEnabled+ �� Mode$ TwoWay=� �� NotifyOnSourceUpdated .  �� NotifyOnTargetUpdated .  �� UpdateSourceTrigger$ PropertyChanged=�y��� *PrinterNameVisibility+$ OneTime=�   ComboboxItems �� *PrinterNames+$ TwoWay=� .  . $ PropertyChanged=�   SelectedValue �� *SelectedPrinterName+$ TwoWay=� .  . $ PropertyChanged=�� $��2q� � ColumnDefinitions �� $	��120 ��� $��* � � RowDefinitions �� $
��auto ��� $
��auto ��� $
��auto ��� $
��auto ��� $
��auto ��� $
��auto ��� $
��auto ��� $
��auto ��� $��6 ��� $��* ��� $��6 �.+�  -   $��cmbPaparSize��$��1��$��0��$��2��$��2q�$ 用紙サイズ��$	 116Q�$ 0,0,4,0q�$��28��   Combo_DisplayMemberPath$ 	PaperName��   Combo_SelectedValuePath$ 	PaperName�� �� *IsPageSizeEnabled+$ TwoWay=� .  . $ PropertyChanged=� �� *
PaperSizes+$ TwoWay=� .  . $ PropertyChanged=� �� *PaperSizeName+$ TwoWay=� .  . $ PropertyChanged=��� $��2��$��0��$�� ��$��2q�7  1KyoeiSystem.Framework.Windows.Controls.UcCheckBox  -   $��2��$��0��   cContent$ カスタム用紙��$��Center=�$��2q�   	IsChecked �� *IsCustomPageSize+$ TwoWay=� .  . $ PropertyChanged=��� $ 
Horizontal=�$��2��$��1��$��Center=�{��� *IsCustomPageSizeEnabled+$ TwoWay=� .  . $ PropertyChanged=�.+��� $��高さ��$��2q�6  0KyoeiSystem.Framework.Windows.Controls.UcTextBox  -   $��txtCustomHeight��$��2��$��1��$��60��$��2q�    ValidationType$  Integer=�!   MinValue$! 10��"   MaxValue$
" 9999��#   cTextAlignment$# Right=�$   Text$ �� *CustomHeight+% �� StringFormat$% #��$ TwoWay=� .  . $ PropertyChanged=��� $	��幅��$��10,2,2,2q�  $��2��$��1��$��60��$��2q�$  Integer=�$! 10��$
" 9999��$# Right=�$ �� *CustomWidth+$% #��$ TwoWay=� .  . $ PropertyChanged=�4  .KyoeiSystem.Framework.Windows.Controls.UcLabel  $��3��$��0��$��1��&   cContent$& 印刷方向��$��2q��� $ 
Horizontal=�$��3��$��1��$��Center=�.+��� -   $��	Landscape��$��Center=�$��回転���� -   $��LandscapeAngle��$ 
Horizontal=�{�.  $y�Hidden=�.+�	� -   $��
Landscape0��$
��0度��$��8,0,0,0q�$��Center=�$��Center=�	� -	   $��Landscape90��$��90度��$��4,0,0,0q�$��Center=�$��Center=�	� -
   $��Landscape180��$��180度��$��4,0,0,0q�$��Center=�$��Center=�	� -   $��Landscape270��$��270度��$��4,0,0,0q�$��Center=�$��Center=�  $��4��$��0��$��2��$��2q�$ 用紙トレイ��$	 116Q�$ 0,0,4,0q�$��28��$ 
SourceName��$ 
SourceName�� �� *PaperSources+$ TwoWay=� .  . $ PropertyChanged=� �� *PaperSourceName+$ TwoWay=� .  . $ PropertyChanged=�  $��6��$��0��$& 余白調整��$��2q��� $��5��$��1��$��3��$��1q�
���   ��� $��5��$��1��$	��左��$��20,3,0,1q�$
��Left=��� $��5��$��1��$	��右��$��0,4,20,0q�$��Right=��� $��5��$��1��$	��上��$��
76,3,167,1q�$��Center=��� $��7��$��1��$	��下��$��
76,3,167,1q�$��Center=�  $��5��$��1��$��60��$��Center=�$��2q�$# Right=�$  Decimal=�$! -9999��$
" 9999��$ �� *	MarginTop+$% #0��$ TwoWay=� .  . $ PropertyChanged=�  $��6��$��1��$��60��$
��Left=�$��20,2,2,2q�$# Right=�$  Decimal=�$! -9999��$
" 9999��$ �� *
MarginLeft+$% #0��$ TwoWay=� .  . $ PropertyChanged=��� $��6��$��1��$	��100��$��2q�
�������$��1q�
���   �  $��6��$��1��$��60��$��Right=�$��2,2,20,2q�$# Right=�$  Decimal=�$! -9999��$
" 9999��$ �� *MarginRight+$% #0��$ TwoWay=� .  . $ PropertyChanged=�  $��7��$��1��$��60��$��Center=�$��2q�$# Right=�$  Decimal=�$! -9999��$
" 9999��$ �� *MarginBottom+$% #0��$ TwoWay=� .  . $ PropertyChanged=��� $��9��$��2��$O��I※ 各寸法の値は 1/100インチ単位で指定してください。��$��Right=�� $��2q�{��� *Is印刷指示+$ OneWay=�$ PropertyChanged=� .  �� $	��120 ��� $��* � �� $
��auto ��� $
��auto ��� $
��auto �.+��� $��印刷範囲��$��0��$��2��
����ح�  $��1��$��0��$ 全ページ印刷��$��Center=�$��2q� �� *全ページ印刷+$ TwoWay=� .  . $ PropertyChanged=��� $ 
Horizontal=�$��1��$��1��$
��Left=�.+��� $��開始ページ��$��2q�  $��2��$��1��$��54��$��2q�$# Right=�$  Integer=�$! 0��$
" 9999��$ �� *
Page開始+$% 0��$ TwoWay=� .  . $ PropertyChanged=��� $��終了ページ��$��10,2,2,2q�  $��2��$��1��$��54��$��2q�$# Right=�$  Integer=�$! 0��$
" 9999��$ �� *
Page終了+$% 0��$ TwoWay=� .  . $ PropertyChanged=��� $ 
Horizontal=�$��2��$��1��$
��Left=�.+��� $��部数��$��2q�  -   $��2��$��1��$��60��$��2q�$# Right=�$  Integer=�$! 1��$
" 9999��$ �� *印刷部数+$% 0��$ TwoWay=� .  . $ PropertyChanged=�� $��4,20,4,4q� �� $��80 ��� $	��120 ��� $	��130 ��� $��* �.+��� -   $��	btnCnacel��$��キャンセル��$��0��$��1��$��2q�$	��100��
�������
��������� -   $��btnSave��$��0��$��2��$��2q�$	��100��
�������
����������� *OKボタン機能名+