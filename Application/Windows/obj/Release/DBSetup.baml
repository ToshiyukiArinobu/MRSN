   M S B A M L   `   `   `  ���� X  TKyoeiSystem.Framework.Windows, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null�[clr-namespace:KyoeiSystem.Framework.Windows.ViewBase;assembly=KyoeiSystem.Framework.Windows&KyoeiSystem.Framework.Windows.ViewBase  �[clr-namespace:KyoeiSystem.Framework.Windows.ViewBase;assembly=KyoeiSystem.Framework.Windows&KyoeiSystem.Framework.Windows.ViewBase  a ]KyoeiSystem.Framework.Windows.Controls, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null�dclr-namespace:KyoeiSystem.Framework.Windows.Controls;assembly=KyoeiSystem.Framework.Windows.Controls&KyoeiSystem.Framework.Windows.Controls # KyoeiSystem.Application.Windows3   -KyoeiSystem.Application.Windows.Views.DBSetup   bfw[clr-namespace:KyoeiSystem.Framework.Windows.ViewBase;assembly=KyoeiSystem.Framework.Windows  \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     jvm[clr-namespace:KyoeiSystem.Framework.Windows.ViewBase;assembly=KyoeiSystem.Framework.Windows     uctlsdclr-namespace:KyoeiSystem.Framework.Windows.Controls;assembly=KyoeiSystem.Framework.Windows.Controls     -     � WindowStyle$
  None=� � Title$ DB接続情報設定��$	��350��$	��360��$	��320��$	��320�� � ShowInTaskbar .  � WindowStartupLocation$ CenterScreen=�����    LoginWindow �� Source# [   � 	Resources   ButtonFocusVisual �� 
TargetType �� Property �� Value	 J� SnapsToDevicePixels
 �� StrokeDashArray  ButtonNormalBackground �� EndPoint �� 
StartPoint � Color � Offset  ButtonNormalBorder �� Color��%  &	       &	 S     &	 �     �� " ��.���  � �� �� .R��� $��2q�	 . #��� ��$��1��$	
 1 2R��� $	 0,1&�$	 0,0&�.��� $ #F3F3F3��$ 0Q�� $ #EBEBEB��$	 0.5Q�� $ #DDDDDD��$	 0.5Q�� $ #CDCDCD��$ 1Q��� $ 	#FF707070��.���  � RowDefinitions �� $
��auto ��� $��* �.+��� $��5q�
���   �$��1q�
�������$��2��._�� $��5q� � ColumnDefinitions �� $��10 ��� $��* ��� $��* ��� $��10 � �� $��40 ��� $
��auto ��� $
��auto ��� $
��auto ��� $
��auto ��� $
��auto ��� $
��auto ��� $��30 ��� $
��auto ��� $��* �.+��� $��0��$��0��$��4������ *NaviMessage+ �� UpdateSourceTrigger$ PropertyChanged=�;  5KyoeiSystem.Framework.Windows.Controls.UcLabelTextBox  -   $��	txtServer��$��1��$��1��$��2��$��0,4,0,0q�   Label_Context$ SQLサーバー��   Label_Width$	 100Q�   
cMaxLength$ 60��   Text �� *
DBServer+ �� Mode$ TwoWay=� �� NotifyOnSourceUpdated .  �� NotifyOnTargetUpdated . $ PropertyChanged=�  -   $��	txtDBName��$��2��$��1��$��2��$��0,4,0,0q�$ DB名��$	 100Q�$ 60�� �� *DBName+$ TwoWay=� .  . $ PropertyChanged=�  -   $��	txtUserID��$��3��$��1��$��2��$��0,4,0,0q�$ ユーザ名��$	 100Q�$ 60�� �� *
UserName+$ TwoWay=� .  . $ PropertyChanged=�� $��4��$��1��$��2��$��0,4,0,0q� �� $	��100 ��� $��* �.+�4  .KyoeiSystem.Framework.Windows.Controls.UcLabel  $��0��   cContent$ パスワード��$	��100��:  4KyoeiSystem.Framework.Windows.Controls.UcPasswordBox  -   $��	txtPasswd��$��1��$��2,0,0,0q�   PasswordText �� *
Password+$ TwoWay=� .  . $ PropertyChanged=��� $��8��$��1��$��2�� �� Orientation$ 
Horizontal=�$��Center=�.+��� -   $��BtnSave��$��90��$��設定保存�����#3b5998
�������$��0,0,10,0q��� -   $��	BtnLogout��$��90��$��キャンセル�����#3b5998
�������$��0,0,0,0q��� $��9��$��3��$��Bottom=�
��� ���
���  ������ *ErrorMessage+y��� *ErrorMessageVisibility+