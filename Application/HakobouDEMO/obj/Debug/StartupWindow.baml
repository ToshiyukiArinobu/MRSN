   M S B A M L   `   `   `  ����X  TKyoeiSystem.Framework.Windows, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null�[clr-namespace:KyoeiSystem.Framework.Windows.ViewBase;assembly=KyoeiSystem.Framework.Windows&KyoeiSystem.Framework.Windows.ViewBase  5   	   O Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem 5   	   a ]KyoeiSystem.Framework.Windows.Controls, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null�dclr-namespace:KyoeiSystem.Framework.Windows.Controls;assembly=KyoeiSystem.Framework.Windows.Controls&KyoeiSystem.Framework.Windows.Controls 5   	    Hakobou.NET   Hakobo.StartupWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      lfw[clr-namespace:KyoeiSystem.Framework.Windows.ViewBase;assembly=KyoeiSystem.Framework.Windows      5   	   R	 NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml 	    5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006 	    5   	   @d2http://schemas.microsoft.com/expression/blend/2008 	    5   	   9System&clr-namespace:System;assembly=mscorlib 	    5   	   uctlsdclr-namespace:KyoeiSystem.Framework.Windows.Controls;assembly=KyoeiSystem.Framework.Windows.Controls 	    5   	   -   5          � Title$  ■スタートアップ��5
   	    � WindowStartupLocation$ CenterScreen=�5      $��368.834��5   	   $��364.417��6    � ShowInTaskbar .  � WindowStyle$
 None=�6?    � Topmost .  � 
ResizeMode$ NoResize=�6a   ��5   	   ��    StartupWindow �� Source# [   � 	Resources 5        ButtonFocusVisual �� 
TargetType	 �� Property
 �� Value J� SnapsToDevicePixels �� StrokeDashArray  ButtonNormalBackground �� EndPoint �� 
StartPoint � Color � Offset  ButtonNormalBorder �� Color��%�  &	       &	 �     &	 �    �� 5      " ��6&   .�5      �� 	 � ��
 5   
   �� 5      .R�5      �� $��2q�6    . #��� ��6>   $��1��6�   $	 1 2R�6�   5      5   	   5      �� 5      $	 0,1&�69   $	 0,0&�6H   .��5      � $ #F3F3F3��6   $ 0Q�6%   � 5      $ #EBEBEB��6   $	 0.5Q�6%   � 5      $ #DDDDDD��6   $	 0.5Q�6%   � 5      $ #CDCDCD��6   $ 1Q�6%   5      �� 5       $ 	#FF707070��61   5!      .��5#      �� $��10q�6   .+�5$      �� $
��Left=�6   $��20��6+   $��
10,10,0,10q�67   $
��Wrap=�6K   $*��$アプリケーション準備中...��6_   � 5%      $��20��6   $��
20,0,20,10q�6   
�������
������� � IsIndeterminate .  � Value 62   �� *
Progress+y�6M   �� *ProgressVisibility+6�   �� 5&      $
��Left=�6   $	��160��6+   $��10q�68   $
��Wrap=�6D   $��Visible=�6q   ��6X   �� *	Message+6q   �� 5'       �� Orientation$ 
Horizontal=�6   $��Right=�6*   .+�5(      �� -   5        $��btnSetup��5(      $
��Auto��6   $ �� データベース設定 ��6,   $��0,0,0,0q�6A   
���**��|�.  
�������y�6R   �� *DBSetupButtonVisibility+6�   �� 5)      -   5        $��60��5)      $��中止��6   $��20,0,0,0q�6(   
���**��|�.  
�������6t   5*      5+      5,      