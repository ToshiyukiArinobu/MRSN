   M S B A M L   `   `   `  ���� X  TKyoeiSystem.Framework.Windows, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null�[clr-namespace:KyoeiSystem.Framework.Windows.ViewBase;assembly=KyoeiSystem.Framework.Windows&KyoeiSystem.Framework.Windows.ViewBase  ! KyoeiSystem.Framework.Reports8   2KyoeiSystem.Framework.Reports.Preview.FontDialogEx   bfw[clr-namespace:KyoeiSystem.Framework.Windows.ViewBase;assembly=KyoeiSystem.Framework.Windows  \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35n jSAPBusinessObjects.WPF.ViewerShared, Version=13.0.3500.0, Culture=neutral, PublicKeyToken=692fbea5521e1304J 9http://schemas.microsoft.com/winfx/2006/xaml/presentation       R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     -     � Title$  フォントの選択�� � SizeToContent$ WidthAndHeight=� � WindowStyle$
 None=� � ShowInTaskbar .  $��Meiryo)�
������  � AllowsTransparency .  � 
ResizeMode$ NoResize=� � 	Resources F   @KyoeiSystem.Framework.Windows.ViewBase.FontFamilyToNameConverter   FontFamilyToNameConverter��%   &	          .����  �� CornerRadius$ 6��
����iA�$��1q�����  �� 
StartPoint$ 0.5,0&�	 �� EndPoint$	 0.5,1&�.��� 
 � Offset$
 0Q� � Color$ LightSteelBlue��� $	
 0.1Q�$ 	AliceBlue��� $	
 0.5Q�$ 
GhostWhite��� $
 1Q�$ LightSteelBlue��._���  �� Orientation$ Vertical=�.+��� 
�������$��0,0,0,1q�._��� $��フォントの選択��
���""��$
��Bold"�$��	10,10,0,6q��� $ 
Horizontal=�$��
10,10,10,0q�.+��� $ Vertical=�$	��230��.+��� $��25��$��フォント(_F)�� �� Target ��  �� ElementName$ txtFamilyName���� -   $��txtFamilyName��$��25���� -   $��lstFamilyName��$	��310������ .R�$� ���� .R��� $��12(�����  �� 	Converter# [   �� Mode$ OneWay=��� $ Vertical=�$��6,0,0,0q�.+��� $ 
Horizontal=�.+��� $ Vertical=�$	��240��.+��� $��25��$��タイプフェース(_Y)�� �� $ txtTypeface���� -   $��txtTypeface��$��25��$
��Bold"��� -   $��lstTypeface��$	��150���� $ Vertical=�$��6,0,0,0q�.+��� $��25��$��サイズ(_S)�� �� $ txtFontSize���� -   $��txtFontSize��$��90��$��25��$
��Left=�$
��Bold"��� -   $��lstFontSize��$��90��$	��160��$
��Left=��� -   $��PART_SamplePanel��$ Vertical=�.+��� $��サンプル(_M)��$��0,4,0,0q� �� $ 	txtSample���� -	   $��	txtSample��$��70��$
��Left=� �� AcceptsReturn .  �� TextWrapping$
 Wrap=� � ToolTip$! テキストの編集可能������ $ PART_SamplePanel�� �� Path$ ActualWidth��� $��25��$��
言語(_L)�� �� $ cmbLanguage���� -
   $��cmbLanguage��$��25��$��0,0,0,4q�$
��Left=����� $ PART_SamplePanel��$ ActualWidth��� 
�������$��0,1,0,0q�._��� $ 
Horizontal=�$��40��$��Right=�$��Center=�.+��� -   $��btnOK��$��80��$��25��$��0q� �� 	IsDefault . $��OK��$��0���� -   $��	btnCancel��$��80��$��25��$��4,0,20,0q� �� IsCancel . $��キャンセル��$��1��