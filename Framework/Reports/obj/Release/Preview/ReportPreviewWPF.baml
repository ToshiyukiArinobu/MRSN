   M S B A M L   `   `   `  ���� X  TKyoeiSystem.Framework.Windows, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null�[clr-namespace:KyoeiSystem.Framework.Windows.ViewBase;assembly=KyoeiSystem.Framework.Windows&KyoeiSystem.Framework.Windows.ViewBase  �[clr-namespace:KyoeiSystem.Framework.Windows.ViewBase;assembly=KyoeiSystem.Framework.Windows&KyoeiSystem.Framework.Windows.ViewBase  h dSAPBusinessObjects.WPF.Viewer, Version=13.0.3500.0, Culture=neutral, PublicKeyToken=692fbea5521e1304tRclr-namespace:SAPBusinessObjects.WPF.Viewer;assembly=SAPBusinessObjects.WPF.ViewerSAPBusinessObjects.WPF.Viewer a ]KyoeiSystem.Framework.Windows.Controls, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null�dclr-namespace:KyoeiSystem.Framework.Windows.Controls;assembly=KyoeiSystem.Framework.Windows.Controls&KyoeiSystem.Framework.Windows.Controls X TKyoeiSystem.Framework.Reports, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]3clr-namespace:KyoeiSystem.Framework.Reports.Preview%KyoeiSystem.Framework.Reports.Preview <   6KyoeiSystem.Framework.Reports.Preview.ReportPreviewWPF   bfw[clr-namespace:KyoeiSystem.Framework.Windows.ViewBase;assembly=KyoeiSystem.Framework.Windows  \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35n	 jSAPBusinessObjects.WPF.ViewerShared, Version=13.0.3500.0, Culture=neutral, PublicKeyToken=692fbea5521e1304J 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      	 R
 NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml 
    Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006 
    @d2http://schemas.microsoft.com/expression/blend/2008 
    jvm[clr-namespace:KyoeiSystem.Framework.Windows.ViewBase;assembly=KyoeiSystem.Framework.Windows 
    acrRclr-namespace:SAPBusinessObjects.WPF.Viewer;assembly=SAPBusinessObjects.WPF.Viewer 
    yControlsdclr-namespace:KyoeiSystem.Framework.Windows.Controls;assembly=KyoeiSystem.Framework.Windows.Controls 
    Elocal3clr-namespace:KyoeiSystem.Framework.Reports.Preview 
    -     � Title$  帳票プレビュー��$	��820��$	��880��$	��880��
 � Top$ 0�� � Left$ 0�� � 	Resources    ComboBoxMaxLengthStyle �� 
TargetType��%   &	        �� " ��.�-   .��]� .+�]� -   $��
toolpannel��$	��Top=�$��Stretch=�$��0q�.+��� 
�������$��2q�._���  �� Orientation$ 
Horizontal=�$��Right=�$��Bottom=�.+��� $ 
Horizontal=�$y�Visible=�$��8,0,0,0q�.+��� $��1q�
���   �._��� $ 
Horizontal=�.+��� $��表示ページ���� -   $��25��$	��＜��$��5q��� -    �� TextAlignment$ Right=�$��50��$��28�� �� 
IsReadOnly .  ���� *CurrentPage+ �� Mode$ TwoWay=�	 �� UpdateSourceTrigger$	 PropertyChanged=�
 �� NotifyOnSourceUpdated
 .  �� NotifyOnTargetUpdated . �� $��/��$��28����  . $ Right=�$��50��$��28������ *
LastPage+$ TwoWay=�$	 PropertyChanged=�
 .  . �� -   $��25��$	��＞��$��5q��� -   $��50��$��印刷��$��20,5,0,5q��� $ 
Horizontal=�.+��� -   $��70��$��やり直し��$��10,4,4,4q��� -	   $��70��$��取消終了��$��10,4,4,4q��� -
   $��70��$��保存終了 ��$��25��$��10,4,4,4q��� -   $	��130��$��全ての変更の取消��$
��Left=�$��10,4,4,4q��� -   $��50��$��	閉じる��$��20,5,0,5q��� -   $��propertyPanel��$
��Auto=�.���  � RowDefinitions �� $
��auto ��� $
��auto ��� $	��240 �$	��100���� $
��auto ��� $
��auto ��� $
��auto ��� $��* �.+��� $��0��$��	20,5,20,5q�._��� $ 
Horizontal=�.+��� $��モード　��$��0q��� $ 
Horizontal=�
����ح�.+�	� -   $��chkDesignner��$��50��$��デザイナ��$��8,5,0,5q� d� 	IsChecked$
 TrueY�	� -   $��
chkPreview��$��50��$��プレビュー��$��4,5,8,5q�$ FalseY��� $��1��$ 
Horizontal=�$��	20,0,20,0q�.+��� $��表示倍率��$��0q��� $��60��#��[  
 � Tag$ 10�� �� DisplayMemberPath$ 	倍率名�� �� SelectedValuePath$ 倍率������ *表示倍率リスト+$ TwoWay=� �� SelectedItem �� *表示倍率+$ TwoWay=�� $��2��.+�R� -   $��tvReportObject��
���   �$��6,9,2,0q�����  �� Path$
 Tree�  �� DataType4  .KyoeiSystem.Framework.Reports.Preview.TreeNode �� ItemsSource8  2KyoeiSystem.Framework.Reports.Preview.TerminalNode��%�   (��       *, +)(�� >     *, +)�� "   �� $ Children�.R��� ���� $
 Name��� "  .R��� $ 
Horizontal=�.+��� -   |�.   �� *	Visible+�� $��10,0,0,0q����� *Name+���� " Q�.���  �� Property �@Q�
IsSelected �� Value �� $ 
IsSelected�$ TwoWay=���  �@Q�
IsExpanded . �� -   $��3��$��5��$��Center=�$��Stretch=� �� ShowsPreview . 
��� ��� �� ResizeDirection$
 Rows=��� $��4��$��	10,4,20,4q�$ 
Horizontal=�.+��� -    ��Name$ 
pFieldName��$�� ��
���   �$��1q��� $��5��$��1q�
���   �$��10,0,6,0q�
����� �._��� -   $��spProperties��$��4,0,4,0q�.+��� $ Vertical=�$��0,2,0,0q�.+�4  .KyoeiSystem.Framework.Windows.Controls.UcLabel  $��＜プロパティ＞��$��Stretch=�$
��Left=�
�����F�
��������� $ 
Horizontal=�$��0,2,0,0q�.+��� $ 
Horizontal=�.+��� $��	上座標��$��Center=�$��60��6  0KyoeiSystem.Framework.Windows.Controls.UcTextBox  -   $
��pTop��$��60��   cTextAlignment$ Right=�   ValidationType$ Integer=�   Mask$ 0��   MinValue$ 0��   MaxValue$ 99999���� $ 
Horizontal=�.+��� $��	左座標��$��Center=�$��60��  -   $��pLeft��$��60��$ Right=�$ Integer=�$ 0��$ 0��$ 99999���� -   $
��spWH��$ 
Horizontal=�$��0,2,0,0q�.+��� $ 
Horizontal=�.+��� $	��幅��$��Center=�$��60��  -   $��pWidth��$��60��$ Right=�$ Integer=�$ 0��$ 0��$ 99999���� $ 
Horizontal=�.+��� $��高さ��$��Center=�$��60��  -   $��pHeight��$��60��$ Right=�$ Integer=�$ 0��$ 0��$ 99999���� -   $
��spBR��$ 
Horizontal=�$��0,2,0,0q�.+��� $ 
Horizontal=�.+��� $��	下座標��$��Center=�$��60��  -   $��pBottom��$��60��$ Right=�$ Integer=�$ 0��$ 0��$ 99999���� $ 
Horizontal=�.+��� $��	右座標��$��Center=�$��60��  -   $��pRight��$��60��$ Right=�$ Integer=�$ 0��$ 0��$ 99999���� -   $
��spTH��$ 
Horizontal=�$��0,2,0,0q�.+��� $ 
Horizontal=�.+��� $��	線太さ��$��Center=�$��60��  -   $��
pThickness��$��60��$ Right=�$ Integer=�$ 0��$ 0��$	 999���� -   $��	colorFore��$ 
Horizontal=�$y�Visible=�$��0,2,0,0q�.+��� -    $��	colorText��$��	文字色��$��60��$��Center=�$��Center=��� -!    ��Name$ 	cboxColor��$	��120������ *ForeColorList+$ TwoWay=�
 .  . $	 PropertyChanged=� �� *SelectedForeColor+$ TwoWay=�
 .  . $	 PropertyChanged=����� 9  3KyoeiSystem.Framework.Reports.Preview.ComboboxColor"  .R��� $ 
Horizontal=�.+��� $��24��$��18������   �� Color  �� *Color+�� $��Center=�$��5q����� *Text+�� -"   $��
colorFrame��$ 
Horizontal=�$y�Visible=�$��0,2,0,0q�.+��� $��線色��$��60��$��Center=�$��Center=��� -#   $ cboxColorBorder��$	��120������ *FrameColorList+$ TwoWay=�
 .  . $	 PropertyChanged=� �� *SelectedFrameColor+$ TwoWay=�
 .  . $	 PropertyChanged=����� "  .R��� $ 
Horizontal=�.+��� $��24��$��18������   �� *Color+$ TwoWay=��� $��Center=�$��5q����� *Text+$ TwoWay=��� -$   $��spCorner��$ 
Horizontal=�$��0,2,0,0q�.+��� $��	枠角丸���� $ 
Horizontal=�.+��� $��高さ��$��Right=�$��40��  -%   $��pCornerH��$��60��$ Right=�$ Integer=�$ 0��$ 0��$
 9999���� $ 
Horizontal=�.+��� $	��幅��$��Right=�$��30��  -&   $��pCornerW��$��60��$ Right=�$ Integer=�$ 0��$ 0��$
 9999���� -'   $��	fontFrame��$ 
Horizontal=�$y�Visible=�$��0,2,0,0q�.+��� $��フォント��$��60��$��Center=�� ! � ColumnDefinitions! �� �� �� �� ��  �� �� .+��� -(   $��0��$��0��$
��auto��$��60������ *SelectedFont+$ TwoWay=�
 .  . $	 PropertyChanged=�  -)   $��	pFontSize��$��0��$��1��$��40��$ Right=�$ Integer=�$ 0��$ 0��$	 999��"   Text" �� *SelectedFontSize+$ TwoWay=�
 .  . $	 PropertyChanged=��� $��0��$��2��$��pt���� $��1��$��2��$ 
Horizontal=�.+�7  1KyoeiSystem.Framework.Windows.Controls.UcCheckBox  #   cContent$# 下線��$��2,0,0,2q�$   	IsChecked$ �� *SelectedFontUnderLine+$ TwoWay=�
 .  . $	 PropertyChanged=�  $# 太字��$��4,0,0,2q�$ �� *SelectedFontBold+$ TwoWay=�
 .  . $	 PropertyChanged=�  $# 斜体��$��4,0,0,2q�$ �� *SelectedFontItalic+$ TwoWay=�
 .  . $	 PropertyChanged=��� -*   $��chkFrame��$ 
Horizontal=�$��0,2,0,0q�.+�� ! �� $��* ��� $��* ��� $��* ��� $��* ��� $��* � �� $��26 ��� $��26 ��� $��26 �.+��� $��1��$��2��$��3q�$��2q�
���   ��� $��1��$��2��$��Center=�$	��枠���� $��0��$��1��$��Right=�$	��上��$��1q��� $��1��$��0��$��Right=�$	��左��$��1q��� $��1��$��4��$
��Left=�$	��右��$��1q��� $��2��$��3��$
��Left=�$	��下��$��1q�<  6KyoeiSystem.Framework.Windows.Controls.UcLabelComboBox  $��0��$��2��%   Label_Visibility$% 	Collapsed=�$��60��&   ComboboxItems& �� *
lsComboT+$ TwoWay=�
 .  . $	 PropertyChanged=�'   SelectedValue' �� *SelectedLS_T+$ TwoWay=�
 .  . $	 PropertyChanged=�  $��1��$��1��$% 	Collapsed=�$��60��& �� *
lsComboL+$ TwoWay=�
 .  . $	 PropertyChanged=�' �� *SelectedLS_L+$ TwoWay=�
 .  . $	 PropertyChanged=�  $��1��$��3��$% 	Collapsed=�$��60��& �� *
lsComboR+$ TwoWay=�
 .  . $	 PropertyChanged=�' �� *SelectedLS_R+$ TwoWay=�
 .  . $	 PropertyChanged=�  -+   $��2��$��2��$% 	Collapsed=�$��60��& �� *
lsComboB+$ TwoWay=�
 .  . $	 PropertyChanged=�' �� *SelectedLS_B+$ TwoWay=�
 .  . $	 PropertyChanged=�  -,   (  Name$( 
pTextValue��$��49��
�������)   cAcceptsReturn) . $��0,2,0,0q��� $ 
Horizontal=�$��Center=�$��0,2,0,0q�.+��� --   $��btnItemProperyUpdate��$��80��$��4q�$��適用��|�. �� $��6��$y�	Collapsed=�.+��� 
���   �$��1q�._��� .+��� $��ページ余白��$��5,1,0,0q�$��23���� $ 
Horizontal=�$��30��.+��� $	��左��$��22��$��10,0,5,0q��� $��40��$��0��$ Right=�$��0,0,0,6q��� $	��上��$��22��$��1,0,5,0q��� $��40��$��0��$ Right=�$��0,0,0,6q��� $	��左��$��22��$��1,0,5,0q��� $��40��$��0��$ Right=�$��0,0,0,6q��� $	��下��$��22��$��1,0,5,0q��� $��40��$��0��$ Right=�$��0,0,0,6q�� -.   $��grdViewArea�� �� $
��auto ��� -/   * �Name$* 
grDesigner��$	��400 ��� $
��auto ��� $
��300* �.+��� $ 
Horizontal=�.+��� $��0��$�� デザイン��
�������$��1q�$��26��$	��100��
�������$
��Left=�$
��Left=�$��Stretch=�$��Center=�$��12(�� $y�	Collapsed=�! �� $��80 ��� $��80 ��� $��80 ��� $��80 �.+��� $��0������ *	座標TOP+�� $��1������ *
座標LEFT+�� -0   $��areaDesigner��$��1��$��Visible=�$��Visible=�$��Stretch=�$	��Top=�$��0q�$��Stretch=�
�������.���� -1   $��rptLayoutBack��
�����F�$	��1.5q�
�������._��� -2   $��	rptLayout��$
��Left=�$	��Top=�
�������$��8q�$	��150��$	��100��z��� -3   stLayout��.+�� -4   $��movingFrame��+ �� Left$+ 0��
, �� Top$, 0��$	��150��$	��100��- K� ZIndex$	- 100��.+��� -5   $��
innerFrame��
����  �$��20q�. J� Opacity$	. 0.4Q��� -6   $��catcherFrame��
������ $��15��
���  ��/ �� StrokeStartLineCap$/ Round=�0 �� StrokeEndLineCap$0 Round=�1 �� StrokeLineJoin$1 Round=�2 �� StrokeDashCap$2 Round=�3 �� RadiusX$3 30��4 �� RadiusY$4 30��5 �� StrokeDashArray$5 2 0 2R��� -7   $��2��$��5��$��Center=�$��Stretch=� . 
��� ���$
 Rows=��� $��3��
���   �$��1q�._�8  2SAPBusinessObjects.WPF.Viewer.CrystalReportsViewer  -8   6  Name$6 reportViewer��$��Stretch=�$��Stretch=�$��0q�7 J� SnapsToDevicePixels7 . 8   EnableToolTips8 . 9   ShowLogo9 .  :   ShowPrevPageButton: . ;   ShowSearchTextButton; .  <   ShowStatusbar< .  =   ShowToolbar= .   >   ShowToggleSidePanelButton> .  ?   ShowNextPageButton? . @   ShowCopyButton@ .  A   ShowExportButtonA .  B   ShowGoToPageButtonB . C   ShowPrintButtonC . D   ShowRefreshButtonD .  E   ToggleSidePanel$
E None=�