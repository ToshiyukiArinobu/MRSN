   M S B A M L   `   `   `  ����X  TKyoeiSystem.Framework.Windows, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null�[clr-namespace:KyoeiSystem.Framework.Windows.ViewBase;assembly=KyoeiSystem.Framework.Windows&KyoeiSystem.Framework.Windows.ViewBase  5   	   �[clr-namespace:KyoeiSystem.Framework.Windows.ViewBase;assembly=KyoeiSystem.Framework.Windows&KyoeiSystem.Framework.Windows.ViewBase  5   	   a ]KyoeiSystem.Framework.Windows.Controls, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null�dclr-namespace:KyoeiSystem.Framework.Windows.Controls;assembly=KyoeiSystem.Framework.Windows.Controls&KyoeiSystem.Framework.Windows.Controls 5   	   # KyoeiSystem.Application.Windows4   .KyoeiSystem.Application.Windows.Views.SCH14010   5   	   bfw[clr-namespace:KyoeiSystem.Framework.Windows.ViewBase;assembly=KyoeiSystem.Framework.Windows  \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   jvm[clr-namespace:KyoeiSystem.Framework.Windows.ViewBase;assembly=KyoeiSystem.Framework.Windows     5   	   uctlsdclr-namespace:KyoeiSystem.Framework.Windows.Controls;assembly=KyoeiSystem.Framework.Windows.Controls     5   	   -   5        $��	MinWindow��5      $	��768��5      $	��342��6     � 
ResizeMode$  NoResize=�6    � Left$
 1025��5      
 � Top$ 0��6    � 	Resources 5         DataGridHeaderBackground �� EndPoint �� 
StartPoint � Color � Offset��%p   &	        �� 5   
   $	 0,1&�6?   $	 0,0&�6N   .��5      � $ #3b5998��6   $ 0Q�6+   5      5      .��5      � -   5        #��� ��5      $��5q�6t    � RowDefinitions 5      �� 5      $
��auto �6   �� 5      $��* �6   .+�5      � $��	20,15,5,5q�6    5      �� 5   
   $��30 �6   �� 5   
   $��30 �6   �� 5   
   $��30 �6   �� 5   
   $��30 �6   �� 5    
   $��30 �6   �� 5!   
   $��30 �6   �� 5"   
   $
��auto �6   �� 5#   
   $��* �6   	 � ColumnDefinitions	 5%      �� 5&   
   $��* �6   �� 5'   
   $
��auto �6   .+�5)      ?  9KyoeiSystem.Framework.Windows.Controls.UcLabelTwinTextBox  -   5        
  Name$
 txtName��5)   &   $��0��68      Label_Context$ 経費先名��6E      Label_Width$ 80Q�6Z      
Text1Width$ 50Q�6k      
Text2Width$	 100Q�6{      Label1Visibility$ 	Collapsed=�6�      Label2Visibility$ 	Collapsed=�6�   $
��Left=�6�   $��1q�6�   $��3��5*   &      Text1MaxLength$ 6��6:      DataAccessName$ M01_KEI��5+   &      Text1 5,   &   �� *得意先名+ �� Mode$ TwoWay=� �� NotifyOnSourceUpdated .  �� NotifyOnTargetUpdated . ;  5KyoeiSystem.Framework.Windows.Controls.UcLabelTextBox  5/      -   5           Label_Context$ 確定コード��5/   #   $��1��69      Label_Width$ 80Q�6F      cWidth$	 128Q�6W      cHorizontalAlignment$
 Left=�6d   $��1q�6�      Text 50   #   ��  �� Path$ 確定コード�$ TwoWay=� .  . 6�   � 52      $��3��6   	 53   
   �� 54      $
��auto �6   �� 55      $��* �6   57   	   �� 59      -   5        $��CancelButton��59      $��4��6)   $��3��69   $��5q�6F   $��3��6Q   $
��Left=�6b   $��61��6}   $	��Top=�6�   $��51��6�    J� RenderTransformOrigin$ 0.5,0.5&�6�    J� OpacityMask# � ��6�   $��3q�6d  ���	#FF071281��5:      �� 5;       �� ImageSource$+ %../Images/appbar.app.remove.white.png��6!     j� Stretch$
  None=�6V   ! j� 
AlignmentY$! Bottom=�6e   5=      �� 5?      -   5        $��OkButton��5?      $��3��6%   $��4��66   $��0��6G   $��5q�6T   $��Right=�6_   $��61��6{   $��Bottom=�6�   $��49��6�   $ 0.5,0.5&�6�   # � ��6�   $��3q�5@   Q   ���	#FF071281��5A      �� 5B      $5 /../Images/appbar.checkmark.pencil.top.white.png��6!   $
  None=�6_   $! Bottom=�6n   5D      5E      &   System.Windows.Controls.DataGrid  5K   
   -   5        $��
SearchGrid��5K      $��1��6%   "   CanUserSortColumns" . #   SelectionMode$# Single=�6L   $   CanUserAddRows$ .  %   CanUserDeleteRows% .  &   
IsReadOnly& . '   AutoGenerateColumns' . ��5L      �� $ SearchResult�$ TwoWay=� .  . (   ColumnHeaderStyle( 5O      �� 5P      ) �� 
TargetType=  7System.Windows.Controls.Primitives.DataGridColumnHeader")  6   .�5Q      �� * �� Property* � ��+ �� Value$+ 25��6/   �� 5R      * � ��
+ �����63   �� 5S      * � ��$	+ 0.5q�68   �� 5T      * � ��
+ �   �64   �� 5U      * � ��$+ Center=�6C   �� 5V      GridHeaderStye��* � ��#+ [  6K   5W      5t      5u      5v      