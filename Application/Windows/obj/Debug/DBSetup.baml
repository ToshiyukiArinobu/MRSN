   M S B A M L   `   `   `  ����X  TKyoeiSystem.Framework.Windows, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null�[clr-namespace:KyoeiSystem.Framework.Windows.ViewBase;assembly=KyoeiSystem.Framework.Windows&KyoeiSystem.Framework.Windows.ViewBase  5   	   �[clr-namespace:KyoeiSystem.Framework.Windows.ViewBase;assembly=KyoeiSystem.Framework.Windows&KyoeiSystem.Framework.Windows.ViewBase  5   	   a ]KyoeiSystem.Framework.Windows.Controls, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null�dclr-namespace:KyoeiSystem.Framework.Windows.Controls;assembly=KyoeiSystem.Framework.Windows.Controls&KyoeiSystem.Framework.Windows.Controls 5   	   # KyoeiSystem.Application.Windows3   -KyoeiSystem.Application.Windows.Views.DBSetup   5   	   bfw[clr-namespace:KyoeiSystem.Framework.Windows.ViewBase;assembly=KyoeiSystem.Framework.Windows  \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   jvm[clr-namespace:KyoeiSystem.Framework.Windows.ViewBase;assembly=KyoeiSystem.Framework.Windows     5   	   uctlsdclr-namespace:KyoeiSystem.Framework.Windows.Controls;assembly=KyoeiSystem.Framework.Windows.Controls     5   	   -   5          � WindowStyle$
  None=�5
   	    � Title$ DB接続情報設定��5   	   $	��350��6   $	��360��6)   $	��320��65   $	��320��6E    � ShowInTaskbar .  � WindowStartupLocation$ CenterScreen=�6g   ��5   	   ��    LoginWindow �� Source# [   � 	Resources 5        ButtonFocusVisual �� 
TargetType �� Property �� Value	 J� SnapsToDevicePixels
 �� StrokeDashArray  ButtonNormalBackground �� EndPoint �� 
StartPoint � Color � Offset  ButtonNormalBorder �� Color��%�  &	       &	 �     &	 �    �� 5      " ��6&   .�5      ��  � �� 5   
   �� 5      .R�5      �� $��2q�6   	 . #��� ��6>   $��1��6�   $	
 1 2R�6�   5      5   	   5      �� 5      $	 0,1&�69   $	 0,0&�6H   .��5      � $ #F3F3F3��6   $ 0Q�6%   � 5      $ #EBEBEB��6   $	 0.5Q�6%   � 5      $ #DDDDDD��6   $	 0.5Q�6%   � 5      $ #CDCDCD��6   $ 1Q�6%   5      �� 5       $ 	#FF707070��61   5!      .��5#      �  � RowDefinitions 5$      �� 5%      $
��auto �6   �� 5&      $��* �6   .+�5*      �� $��5q�6   
���   �$��1q�6,   
�������$��2��6S   ._�5+      � $��5q�6    � ColumnDefinitions 5,   
   �� 5-      $��10 �6   �� 5.      $��* �6   �� 5/      $��* �6   �� 50      $��10 �6    52   
   �� 53      $��40 �6   �� 54      $
��auto �6   �� 55      $
��auto �6   �� 56      $
��auto �6   �� 57      $
��auto �6   �� 58      $
��auto �6   �� 59      $
��auto �6   �� 5:      $��30 �6   �� 5;      $
��auto �6   �� 5<      $��* �6   .+�5>   
   �� $��0��6   $��0��6   $��4��6-   ��6A   �� *NaviMessage+ �� UpdateSourceTrigger$ PropertyChanged=�;  5KyoeiSystem.Framework.Windows.Controls.UcLabelTextBox  5?   
   -   5        $��	txtServer��5?      $��1��6/   $��1��6<   $��2��6L   $��0,4,0,0q�6`      Label_Context$ SQLサーバー��5@         Label_Width$	 100Q�66      
cMaxLength$ 60��6H      Text 5A      �� *
DBServer+ �� Mode$ TwoWay=� �� NotifyOnSourceUpdated .  �� NotifyOnTargetUpdated . $ PropertyChanged=�  5D   
   -   5        $��	txtDBName��5D      $��2��6/   $��1��6<   $��2��6L   $��0,4,0,0q�6`   $ DB名��5E      $	 100Q�62   $ 60��6D    5F      �� *DBName+$ TwoWay=� .  . $ PropertyChanged=�  5I   
   -   5        $��	txtUserID��5I      $��3��6/   $��1��6<   $��2��6L   $��0,4,0,0q�6`   $ ユーザ名��5J      $	 100Q�63   $ 60��6E    5K      �� *
UserName+$ TwoWay=� .  . $ PropertyChanged=�� 5N   
   $��4��6   $��1��6   $��2��6,   $��0,4,0,0q�6@    5O      �� 5P      $	��100 �6   �� 5Q      $��* �6   .+�5S      4  .KyoeiSystem.Framework.Windows.Controls.UcLabel  $��0��6      cContent$ パスワード��6)   $	��100��6:   :  4KyoeiSystem.Framework.Windows.Controls.UcPasswordBox  5T      -   5        $��	txtPasswd��5T      $��1��60   $��2,0,0,0q�6@      PasswordText 5U      �� *
Password+$ TwoWay=� .  . $ PropertyChanged=�5V      5X      �� 5Z   
   $��8��6   $��1��6"   $��2��62    �� Orientation$ 
Horizontal=�6F   $��Center=�6_   .+�5[      �� -   5        $��BtnSave��5[      $��90��6"   $��設定保存��6-   ���#3b5998
�������$��0,0,10,0q�6d   5\      �� 5]      -   5        $��	BtnLogout��5]      $��90��6$   $��キャンセル��6/   ���#3b5998
�������$��0,0,0,0q�6g   5^      5_      �� 5b   
   $��9��6   $��3��6   $��Bottom=�61   
��� ���
���  ����5c      �� *ErrorMessage+y�6/   �� *ErrorMessageVisibility+5d   	   5f      5g      5h      