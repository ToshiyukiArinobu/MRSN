   M S B A M L   `   `   `  ���� X  TKyoeiSystem.Framework.Windows, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null�[clr-namespace:KyoeiSystem.Framework.Windows.ViewBase;assembly=KyoeiSystem.Framework.Windows&KyoeiSystem.Framework.Windows.ViewBase  �[clr-namespace:KyoeiSystem.Framework.Windows.ViewBase;assembly=KyoeiSystem.Framework.Windows&KyoeiSystem.Framework.Windows.ViewBase  a ]KyoeiSystem.Framework.Windows.Controls, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null�dclr-namespace:KyoeiSystem.Framework.Windows.Controls;assembly=KyoeiSystem.Framework.Windows.Controls&KyoeiSystem.Framework.Windows.Controls # KyoeiSystem.Application.Windows6   0KyoeiSystem.Application.Windows.Views.SCHM01_TOK   bfw[clr-namespace:KyoeiSystem.Framework.Windows.ViewBase;assembly=KyoeiSystem.Framework.Windows  \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     jvm[clr-namespace:KyoeiSystem.Framework.Windows.ViewBase;assembly=KyoeiSystem.Framework.Windows     uctlsdclr-namespace:KyoeiSystem.Framework.Windows.Controls;assembly=KyoeiSystem.Framework.Windows.Controls     -   $��
MainWindow��$	��768��$	��342��  � Left$
  1025��
 � Top$ 0�� � Title$! 取引先検索(SCHM01_TOK)�� � 	Resources    DataGridHeaderBackground �� EndPoint �� 
StartPoint � Color � Offset��%A   &	        �� $	 0,1&�$	 0,0&�.��� $ #3b5998��$ 0Q�.��� #��� ��$��5q� � RowDefinitions �� $
��auto ��� $��* �.+�� $��	20,15,5,5q� �� $��30 ��� $��30 ��� $��30 ��� $��30 ��� $��30 ��� $��30 �	 � ColumnDefinitions	 �� $��* ��� $
��auto �.+�� $��0��	 �� $
��auto ��� $��* �.+�<  6KyoeiSystem.Framework.Windows.Controls.UcLabelComboBox  -   $��OrderColumn��$��1��$	��208��$
��Left=�$��0,2,0,0q�
   Label_Context$
 	表示順��   Label_Width$ 80Q�   ComboListingParams$# 共通,照会画面,表示順��;  5KyoeiSystem.Framework.Windows.Controls.UcLabelTextBox  -     Name$ txtCD��$��1��$��1q�   ImeType$	 Off=�   ValidationType$ Integer=�   Label_Context$ 取引先コード��   Label_Width$ 80Q�   cWidth$	 128Q�   cHorizontalAlignment$
 Left=�    cVerticalContentAlignment$ Center=�   Text �� *	コード+ �� Mode$ TwoWay=� �� NotifyOnSourceUpdated .  �� NotifyOnTargetUpdated .   -   $��txtKana��$��2��$��1q�$ Native=�$ 名称��$ 80Q�$	 128Q�$
 Left=�$ Center=� �� *名称+$ TwoWay=� .  . � $��3��	 �� $
��auto ��� $��* �.+�  -   $��cmbDealings��$��1��$	��208��$
��Left=�$��1q�$
 取引区分��$ 80Q�$2 ,取引先マスタ,照会画面,取引区分���� -   $��OkButton��$��0��$��4��$��3��$��Right=�$��Bottom=�$��61��$��49��$��5q� J� RenderTransformOrigin$ 0.5,0.5&�$��3q����	#FF071281 J� OpacityMask# � ������  �� ImageSource$5 /../Images/appbar.checkmark.pencil.top.white.png�� j� Stretch$
 None=� j� 
AlignmentY$ Bottom=��� -   $��CancelButton��$��3��$��4��$��3��$
��Left=�$	��Top=�$��61��$��51��$��5q�$ 0.5,0.5&�$��3q����	#FF071281# � ������ $+ %../Images/appbar.app.remove.white.png��$
 None=�$ Bottom=�&   System.Windows.Controls.DataGrid  -   $��
SearchGrid��$��1��   CanUserSortColumns .     SelectionMode$ Single=�    CanUserAddRows  .  !   CanUserDeleteRows! .  "   
IsReadOnly" . #   AutoGenerateColumns# . ���� $ �� Path$$ SearchResult�$ TwoWay=� .  . %   ColumnHeaderStyle% �� & �� 
TargetType=  7System.Windows.Controls.Primitives.DataGridColumnHeader"&  .��� ' �� Property' � ��( �� Value$( 25���� ' � ��
( ������� ' � ��$	( 0.5q��� ' � ��
( �   ��� ' � ��$( Center=��� GridHeaderStye��' � ��#( [  