   M S B A M L   `   `   `  ����X  TKyoeiSystem.Framework.Windows, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null�[clr-namespace:KyoeiSystem.Framework.Windows.ViewBase;assembly=KyoeiSystem.Framework.Windows&KyoeiSystem.Framework.Windows.ViewBase  5      ! KyoeiSystem.Framework.Reports8   2KyoeiSystem.Framework.Reports.Preview.FontDialogEx   bfw[clr-namespace:KyoeiSystem.Framework.Windows.ViewBase;assembly=KyoeiSystem.Framework.Windows  \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35n jSAPBusinessObjects.WPF.ViewerShared, Version=13.0.3500.0, Culture=neutral, PublicKeyToken=692fbea5521e1304J 9http://schemas.microsoft.com/winfx/2006/xaml/presentation       5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      -   5          � Title$  フォントの選択��5       � SizeToContent$ WidthAndHeight=�6    � WindowStyle$
 None=�64    � ShowInTaskbar .  $��Meiryo)�6]   
������  � AllowsTransparency .  � 
ResizeMode$ NoResize=�5   7    � 	Resources 5      F   @KyoeiSystem.Framework.Windows.ViewBase.FontFamilyToNameConverter   FontFamilyToNameConverter��%   &	          5	      6#   5
      .��5      ��  �� CornerRadius$ 6��6   
����iA�$��1q�64   ��5      �� 5       �� 
StartPoint$ 0.5,0&�6   	 �� EndPoint$	 0.5,1&�6/   .��5   
   � 
 � Offset$
 0Q�6    � Color$ LightSteelBlue��6"   � 5   
   $	
 0.1Q�6   $ 	AliceBlue��6$   � 5   
   $	
 0.5Q�6   $ 
GhostWhite��6$   � 5   
   $
 1Q�6   $ LightSteelBlue��6"   5   	   ._�5      ��  �� Orientation$ Vertical=�6   .+�5      �� 
�������$��0,0,0,1q�6&   ._�5   
   �� $��フォントの選択��6   
���""��$
��Bold"�6:   $��	10,10,0,6q�6L   5   	   �� 5      $ 
Horizontal=�6   $��
10,10,10,0q�6,   .+�5   
   �� $ Vertical=�6   $	��230��6,   .+�5      �� $��25��6   $��フォント(_F)��6K    �� Target 6   ��  �� ElementName$ txtFamilyName��6K   �� 5      -   5        $��txtFamilyName��5      $��25��6)   �� 5      -   5        $��lstFamilyName��5      $	��310��6)   ��5      �� 5       .R�5!      $� 5"      ��5$      �� 5%      .R�5&      �� $��12(�6   ��5'      ��  �� 	Converter# [   �� Mode$ OneWay=�5(      52      53      �� 55   
   $ Vertical=�6   $��6,0,0,0q�6,   .+�56      �� $ 
Horizontal=�6   .+�57      �� $ Vertical=�6   $	��240��60   .+�58      �� $��25��6   $��タイプフェース(_Y)��6M    6"   �� $ txtTypeface��6M   �� 59      -   5        $��txtTypeface��59      $��25��6+   $
��Bold"�67   �� 5:      -   5        $��lstTypeface��5:      $	��150��6+   68   5;      �� 5=      $ Vertical=�6   $��6,0,0,0q�60   .+�5>      �� $��25��6   $��サイズ(_S)��6M    6"   �� $ txtFontSize��6M   �� 5?      -   5        $��txtFontSize��5?      $��90��6+   $��25��66   $
��Left=�6B   $
��Bold"�6]   �� 5@      -   5        $��lstFontSize��5@      $��90��6+   $	��160��66   $
��Left=�6C   6^   5A      5B      �� 5D      -   5        $��PART_SamplePanel��5D      $ Vertical=�6/   .+�5E      �� $��サンプル(_M)��6   $��0,4,0,0q�6P    6'   �� $ 	txtSample��6P   �� 5F      -	   5        $��	txtSample��5F      $��70��6h   $
��Left=�5G       �� AcceptsReturn .  �� TextWrapping$
 Wrap=�6F    � ToolTip$! テキストの編集可能��6Z   ��5F   '   �� $ PART_SamplePanel�� �� Path$ ActualWidth�5G   Z   �� 5H      $��25��6   $��
言語(_L)��6     61   �� $ cmbLanguage���� 5I      -
   5        $��cmbLanguage��5I      $��25��5J      $��0,0,0,4q�6#   $
��Left=�64   ��5I   *   �� $ PART_SamplePanel��$ ActualWidth�5J   O   5K      5L      5M   	   �� 5O      
�������$��0,1,0,0q�6&   ._�5P   
   �� $ 
Horizontal=�6   $��40��6.   $��Right=�6:   $��Center=�6V   .+�5Q      �� -   5        $��btnOK��5Q      $��80��6    $��25��6+   $��0q�67    �� 	IsDefault . $��OK��6S   $��0��6t   �� 5R      -   5        $��	btnCancel��5R      $��80��6$   $��25��6/   $��4,0,20,0q�6;    �� IsCancel . $��キャンセル��6]   $��1��6m   5S      5T   	   5U      5V      5W      