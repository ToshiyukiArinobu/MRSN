   M S B A M L   `   `   `  ����X  TKyoeiSystem.Framework.Windows, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null�[clr-namespace:KyoeiSystem.Framework.Windows.ViewBase;assembly=KyoeiSystem.Framework.Windows&KyoeiSystem.Framework.Windows.ViewBase  5   	   a ]KyoeiSystem.Framework.Windows.Controls, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null�dclr-namespace:KyoeiSystem.Framework.Windows.Controls;assembly=KyoeiSystem.Framework.Windows.Controls&KyoeiSystem.Framework.Windows.Controls 5   	   ! KyoeiSystem.Framework.Reports@   :KyoeiSystem.Framework.Reports.Preview.PrinterSettingWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35n jSAPBusinessObjects.WPF.ViewerShared, Version=13.0.3500.0, Culture=neutral, PublicKeyToken=692fbea5521e1304J 9http://schemas.microsoft.com/winfx/2006/xaml/presentation       5   	   nfw[clr-namespace:KyoeiSystem.Framework.Windows.ViewBase;assembly=KyoeiSystem.Framework.Windows       5   	   R	 NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml 	    5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006 	    5   	   @d2http://schemas.microsoft.com/expression/blend/2008 	    5   	   uctlsdclr-namespace:KyoeiSystem.Framework.Windows.Controls;assembly=KyoeiSystem.Framework.Windows.Controls 	    5   	   -   5          � 
ResizeMode$  NoResize=�5	   	    � WindowStartupLocation$ CenterOwner=�6    � WindowStyle$ 
ToolWindow=�6C   $	��540��5
   	   $	��457��6    � Title$ プリンター設定��6"    � 	Resources 5         ButtonFocusVisual �� 
TargetType �� Property �� Value J� SnapsToDevicePixels	 �� StrokeDashArray  ButtonNormalBackground
 �� EndPoint �� 
StartPoint � Color � Offset  ButtonNormalBorder �� Color��%�  &	        &	 �     &	 �    �� 5      " ��6&   .�5      ��  � �� 5   
   �� 5      .R�5      �� $��2q�6    . #��� ��6>   $��1��6�   $		 1 2R�6�   5      5   	   5      �� 5      $	
 0,1&�69   $	 0,0&�6H   .��5      � $ #F3F3F3��6   $ 0Q�6%   � 5      $ #EBEBEB��6   $	 0.5Q�6%   � 5      $ #DDDDDD��6   $	 0.5Q�6%   � 5      $ #CDCDCD��6   $ 1Q�6%   5      �� 5      $ 	#FF707070��61   5      .��5       �� $��8q�6   .+�5!      ��  �� Orientation$ Vertical=�6   .+�5"      �� $��用紙設定��6   
����ح�6   <  6KyoeiSystem.Framework.Windows.Controls.UcLabelComboBox  5#      -   5        $��4q�5#         Label_Context$ プリンター名��5&         Label_Width$	 116Q�64      Label_Margin$ 0,0,4,0q�6F   $��28��6]   =  7KyoeiSystem.Framework.Windows.Controls.FrameworkControl   	IsEnabled 5$      �� *IsPageSizeEnabled+ �� Mode$ TwoWay=� �� NotifyOnSourceUpdated .  �� NotifyOnTargetUpdated .  �� UpdateSourceTrigger$ PropertyChanged=�y�5%      �� *PrinterNameVisibility+$ OneTime=�   ComboboxItems 5'      �� *PrinterNames+$ TwoWay=� .  . $ PropertyChanged=�   SelectedValue 5(      �� *SelectedPrinterName+$ TwoWay=� .  . $ PropertyChanged=�5)      5+      � 5,      $��2q�6    � ColumnDefinitions 5-      �� 5.   
   $	��120 �6   �� 5/   
   $��* �6    � RowDefinitions 51      �� 52   
   $
��auto �6   �� 53   
   $
��auto �6   �� 54   
   $
��auto �6   �� 55   
   $
��auto �6   �� 56   
   $
��auto �6   �� 57   
   $
��auto �6   �� 58   
   $
��auto �6   �� 59   
   $
��auto �6   �� 5:   
   $��6 �6   �� 5;   
   $��* �6   �� 5<   
   $��6 �6   .+�5?        -   5        $��cmbPaparSize��5?      $��1��61   $��0��6>   $��2��6N   $��2q�6b   $ 用紙サイズ��5@      $	 116Q�63   $ 0,0,4,0q�6E   $��28��6\      Combo_DisplayMemberPath$ 	PaperName��5D         Combo_SelectedValuePath$ 	PaperName��5E       5A      �� *IsPageSizeEnabled+$ TwoWay=� .  . $ PropertyChanged=� 5B      �� *
PaperSizes+$ TwoWay=� .  . $ PropertyChanged=� 5C      �� *PaperSizeName+$ TwoWay=� .  . $ PropertyChanged=�5E      �� 5G      $��2��6   $��0��6   $�� ��6+   $��2q�66   7  1KyoeiSystem.Framework.Windows.Controls.UcCheckBox  5H      -   5        $��2��5H      $��0��6%      cContent$ カスタム用紙��66   $��Center=�6H   $��2q�6l      	IsChecked 5I      �� *IsCustomPageSize+$ TwoWay=� .  . $ PropertyChanged=�5J      �� 5K      $ 
Horizontal=�6   $��2��6,   $��1��69   $��Center=�6I   {�5L      �� *IsCustomPageSizeEnabled+$ TwoWay=� .  . $ PropertyChanged=�.+�5M   
   �� $��高さ��6   $��2q�6   6  0KyoeiSystem.Framework.Windows.Controls.UcTextBox  5N   
   -   5        $��txtCustomHeight��5N      $��2��60   $��1��6=   $��60��6M   $��2q�6X       ValidationType$  Integer=�5O      !   MinValue$! 10��62   "   MaxValue$
" 9999��6@   #   cTextAlignment$# Right=�6P   $   Text$ 5P      �� *CustomHeight+% �� StringFormat$% #��$ TwoWay=� .  . $ PropertyChanged=��� 5Q   
   $	��幅��6   $��10,2,2,2q�6     5R   
   $��2��6   $��1��6&   $��60��66   $��2q�6A   $  Integer=�5S      $! 10��62   $
" 9999��6@   $# Right=�6P   $ 5T      �� *CustomWidth+$% #��$ TwoWay=� .  . $ PropertyChanged=�5U   	   4  .KyoeiSystem.Framework.Windows.Controls.UcLabel  5V      $��3��6   $��0��6"   $��1��62   &   cContent$& 印刷方向��6F   $��2q�6V   �� 5W      $ 
Horizontal=�6   $��3��6,   $��1��69   $��Center=�6I   .+�5X   
   �� -   5        $��	Landscape��5X      $��Center=�6$   $��回転��6?   6h   �� 5Y   
   -   5        $��LandscapeAngle��5Y      $ 
Horizontal=�6+   {�.  $y�Hidden=�6V   .+�5Z      	� -   5        $��
Landscape0��5Z      $
��0度��6*   $��8,0,0,0q�67   $��Center=�6H   $��Center=�6j   	� 5[      -	   5        $��Landscape90��5[      $��90度��6+   $��4,0,0,0q�69   $��Center=�6J   $��Center=�6l   	� 5\      -
   5        $��Landscape180��5\      $��180度��6,   $��4,0,0,0q�6;   $��Center=�6L   $��Center=�6n   	� 5]      -   5        $��Landscape270��5]      $��270度��6,   $��4,0,0,0q�6;   $��Center=�6L   $��Center=�6n   5^      5_   	     5`      $��4��6   $��0��6*   $��2��6:   $��2q�6N   $ 用紙トレイ��5a      $	 116Q�63   $ 0,0,4,0q�6E   $��28��6\   $ 
SourceName��5d      $ 
SourceName��5e       5b      �� *PaperSources+$ TwoWay=� .  . $ PropertyChanged=� 5c      �� *PaperSourceName+$ TwoWay=� .  . $ PropertyChanged=�5e        5g      $��6��6   $��0��6"   $& 余白調整��62   $��2q�6B   �� 5h      $��5��6   $��1��6   $��3��6,   $��1q�6=   
���   �6Q   �� 5i      $��5��6   $��1��6   $	��左��6+   $��20,3,0,1q�67   $
��Left=�6I   �� 5j      $��5��6   $��1��6   $	��右��6+   $��0,4,20,0q�67   $��Right=�6I   �� 5k      $��5��6   $��1��6   $	��上��6+   $��
76,3,167,1q�67   $��Center=�6K   �� 5l      $��7��6   $��1��6   $	��下��6+   $��
76,3,167,1q�67   $��Center=�6K     5m      $��5��6   $��1��6$   $��60��64   $��Center=�6?   $��2q�6\   $# Right=�6g   $  Decimal=�5n      $! -9999��60   $
" 9999��6A   $ 5o      �� *	MarginTop+$% #0��$ TwoWay=� .  . $ PropertyChanged=�  5p      $��6��6   $��1��6$   $��60��64   $
��Left=�6?   $��20,2,2,2q�6Z   $# Right=�6l   $  Decimal=�5q      $! -9999��60   $
" 9999��6A   $ 5r      �� *
MarginLeft+$% #0��$ TwoWay=� .  . $ PropertyChanged=��� 5s      $��6��6   $��1��6   $	��100��6,   $��2q�68   
�������$��1q�6Z   
���   �6n     5t      $��6��6   $��1��6$   $��60��64   $��Right=�6?   $��2,2,20,2q�6[   $# Right=�6m   $  Decimal=�5u      $! -9999��60   $
" 9999��6A   $ 5v      �� *MarginRight+$% #0��$ TwoWay=� .  . $ PropertyChanged=�  5w      $��7��6   $��1��6$   $��60��64   $��Center=�6?   $��2q�6\   $# Right=�6g   $  Decimal=�5x      $! -9999��60   $
" 9999��6A   $ 5y      �� *MarginBottom+$% #0��$ TwoWay=� .  . $ PropertyChanged=��� 5{      $��9��6   $��2��6   $O��I※ 各寸法の値は 1/100インチ単位で指定してください。��6/   $��Right=�5|      5}      � 5      $��2q�6   {�6   �� *Is印刷指示+$ OneWay=�$ PropertyChanged=� .  5�      �� 5�   
   $	��120 �6   �� 5�   
   $��* �6    5�      �� 5�   
   $
��auto �6   �� 5�   
   $
��auto �6   �� 5�   
   $
��auto �6   .+�5�      �� $��印刷範囲��6   $��0��6   $��2��6*   
����ح�6>     5�      $��1��6   $��0��6%   $ 全ページ印刷��66   $��Center=�6H   $��2q�6l    5�      �� *全ページ印刷+$ TwoWay=� .  . $ PropertyChanged=��� 5�      $ 
Horizontal=�6   $��1��6,   $��1��69   $
��Left=�6I   .+�5�   
   �� $��開始ページ��6   $��2q�6      5�   
   $��2��6   $��1��6&   $��54��66   $��2q�6A   $# Right=�6L   $  Integer=�5�      $! 0��62   $
" 9999��6?   $ 5�      �� *
Page開始+$% 0��$ TwoWay=� .  . $ PropertyChanged=��� 5�   
   $��終了ページ��6   $��10,2,2,2q�6      5�   
   $��2��6   $��1��6&   $��54��66   $��2q�6A   $# Right=�6L   $  Integer=�5�      $! 0��62   $
" 9999��6?   $ 5�      �� *
Page終了+$% 0��$ TwoWay=� .  . $ PropertyChanged=�5�   	   �� 5�      $ 
Horizontal=�6   $��2��6,   $��1��69   $
��Left=�6I   .+�5�   
   �� $��部数��6   $��2q�6     5�   
   -   5        $��2��5�      $��1��6&   $��60��66   $��2q�6A   $# Right=�6L   $  Integer=�5�      $! 1��62   $
" 9999��6?   $ 5�      �� *印刷部数+$% 0��$ TwoWay=� .  . $ PropertyChanged=�5�      5�   	   5�      � 5�      $��4,20,4,4q�6    5�      �� 5�   
   $��80 �6   �� 5�   
   $	��120 �6   �� 5�   
   $	��130 �6   �� 5�   
   $��* �6   .+�5�      �� -   5        $��	btnCnacel��5�      $��キャンセル��6    $��0��60   $��1��6=   $��2q�6M   $	��100��6X   
�������
�������6�   �� 5�      -   5        $��btnSave��5�      $��0��6;   $��2��6H   $��2q�6X   $	��100��6c   
�������
���������6   �� *OKボタン機能名+6�   5�      5�      5�      