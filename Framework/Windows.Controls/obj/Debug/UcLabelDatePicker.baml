   M S B A M L   `   `   `  ����a  ]KyoeiSystem.Framework.Windows.Controls, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null_4clr-namespace:KyoeiSystem.Framework.Windows.Controls&KyoeiSystem.Framework.Windows.Controls  5      _4clr-namespace:KyoeiSystem.Framework.Windows.Controls&KyoeiSystem.Framework.Windows.Controls  5      >    8KyoeiSystem.Framework.Windows.Controls.UcLabelDatePicker   5      AControls4clr-namespace:KyoeiSystem.Framework.Windows.Controls  ;fw4clr-namespace:KyoeiSystem.Framework.Windows.Controls  5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5         validationTemplate  =� ErrorTemplate#  [  5      .��5      � .+�5      �  � ColumnDefinitions 5      �� 5   
   $
��auto �6   �� 5   
   $��* �6   �� 5   
   $
��auto �6   .+�5      4   .KyoeiSystem.Framework.Windows.Controls.UcLabel  -   5          Name$ cLabel��5         	LabelText 5      ��  �� Path$ 	LabelText� �� RelativeSource �� *FindAncestor+ �� AncestorType" >�9   3KyoeiSystem.Framework.Windows.Controls.UcDatePicker  5      -   5          Name$ FirstDatePicker��5      9   3KyoeiSystem.Framework.Windows.Controls.UcDatePicker   	LostFocus$ FirstDatePicker_LostFocus��6G   $��1��67   	   SelectedDate	 5      �� $ SelectedDate1� �� *FindAncestor+" >�
 �� Mode$
 TwoWay=� �� NotifyOnSourceUpdated .  �� NotifyOnTargetUpdated .  �� UpdateSourceTrigger$ PropertyChanged=�� 5      -   5         �Name$ 
DoubleGrid��5      $��2��6!   $y�	Collapsed=�61    5   
   �� 5      $
��auto �6   �� 5      $��* �6   .+�5   
   �� $	��～��6   
������ $��23��65     5   
   -   5        $ SecondDatePicker��5       $ FirstDatePicker_LostFocus��6J   $��1��6:   	 5       �� $ SelectedDate2� �� *FindAncestor+" >�$
 TwoWay=� .  . 5!   	   5"      5#      5$      