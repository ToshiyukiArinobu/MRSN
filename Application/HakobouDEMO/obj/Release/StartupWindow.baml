   M S B A M L   `   `   `  ���� X  TKyoeiSystem.Framework.Windows, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null�[clr-namespace:KyoeiSystem.Framework.Windows.ViewBase;assembly=KyoeiSystem.Framework.Windows&KyoeiSystem.Framework.Windows.ViewBase  O Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem a ]KyoeiSystem.Framework.Windows.Controls, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null�dclr-namespace:KyoeiSystem.Framework.Windows.Controls;assembly=KyoeiSystem.Framework.Windows.Controls&KyoeiSystem.Framework.Windows.Controls  Hakobo.NET.MRSN   Hakobo.StartupWindow   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      lfw[clr-namespace:KyoeiSystem.Framework.Windows.ViewBase;assembly=KyoeiSystem.Framework.Windows      R	 NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml 	    Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006 	    @d2http://schemas.microsoft.com/expression/blend/2008 	    9System&clr-namespace:System;assembly=mscorlib 	    uctlsdclr-namespace:KyoeiSystem.Framework.Windows.Controls;assembly=KyoeiSystem.Framework.Windows.Controls 	    -     � Title$  ■スタートアップ�� � WindowStartupLocation$ CenterScreen=�$��368.834��$��364.417�� � ShowInTaskbar .  � WindowStyle$
 None=� � Topmost .  � 
ResizeMode$ NoResize=�����    StartupWindow �� Source# [   � 	Resources   ButtonFocusVisual �� 
TargetType	 �� Property
 �� Value J� SnapsToDevicePixels �� StrokeDashArray  ButtonNormalBackground �� EndPoint �� 
StartPoint � Color � Offset  ButtonNormalBorder �� Color��%  &	       &	 S     &	 �     �� " ��.��� 	 � ��
 �� .R��� $��2q� . #��� ��$��1��$	 1 2R��� $	 0,1&�$	 0,0&�.��� $ #F3F3F3��$ 0Q�� $ #EBEBEB��$	 0.5Q�� $ #DDDDDD��$	 0.5Q�� $ #CDCDCD��$ 1Q��� $ 	#FF707070��.���� $��10q�.+��� $
��Left=�$��20��$��
10,10,0,10q�$
��Wrap=�$*��$アプリケーション準備中...��� $��20��$��
20,0,20,10q�
�������
������� � IsIndeterminate .  � Value �� *
Progress+y��� *ProgressVisibility+�� $
��Left=�$	��160��$��10q�$
��Wrap=�$��Visible=����� *	Message+��  �� Orientation$ 
Horizontal=�$��Right=�.+��� -   $��btnSetup��$
��Auto��$ �� データベース設定 ��$��0,0,0,0q�
���**��|�.  
�������y��� *DBSetupButtonVisibility+�� -   $��60��$��中止��$��20,0,0,0q�
���**��|�.  
�������