   M S B A M L   `   `   `  ����   MyNumber    MyNumber.MyNumberWindow   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     -     � Title$*  $マイナンバーダウンロード��$	��400��$	��550��$	��400��$	��500�� � WindowStartupLocation$ CenterScreen=�.���  � ColumnDefinitions �� $��15 ��� $��* ��� $��* ��� $��* ��� $��15 � � RowDefinitions �� $
��auto ��� $��36 ��� $
��auto ��� $
��auto ��� $
��auto ��� $
��auto ��� $
��auto ��� $��* ��� $��10 �.+��� $��1��$��0��$��5��$
��Wrap=�$��18(����� *	Message+� $��2��$��1��$��3�� �� $��80 ��� $��* � �� $
��auto ��� $
��auto ��� $
��auto ��� $
��auto ��� $
��auto �.+��� $��0��$��0��$��
Tenant Key���� -   $��0��$��1������ *	TenantKey+ �� Mode$ TwoWay=� �� NotifyOnSourceUpdated .  �� NotifyOnTargetUpdated .  �� UpdateSourceTrigger$ PropertyChanged=�{��� *
IsEditable+�� $��1��$��0��$��
Access Key���� -   $��1��$��1������ *	AccessKey+$ TwoWay=� .  . $ PropertyChanged=�{��� *
IsEditable+�� $��2��$��0��$��アカウント���� -   $��2��$��1������ *	AccountID+$ TwoWay=� .  . $ PropertyChanged=�{��� *
IsEditable+�� $��3��$��0��$��パスワード��G� -   $��passboxCert��$��3��$��1�� G� Password$  ��{��� *
IsEditable+&   System.Windows.Controls.DataGrid  $��7��$��1��$��3��$��1��$��4,2,4,0q�	   FrozenColumnCount$	 1��
   AutoGenerateColumns
 .     
IsReadOnly .    SelectionMode$ Single=�   SelectionUnit$ FullRow=�y��� *CertListVisibility+$ TwoWay=� .  . $ PropertyChanged=����� *CertInfoList+$ TwoWay=� .  . $ PropertyChanged=� �� SelectedItem �� *SelectedCertInfo+$ TwoWay=� .  . $ PropertyChanged=�   Columns 4  .System.Windows.Controls.DataGridTemplateColumn  ,  &System.Windows.Controls.DataGridColumn   Header$  ��   CellTemplate �� .R��� -   $��選択��$��24��{��� *
Selectable+0  *System.Windows.Controls.DataGridTextColumn  $ 有効期限��   Width5  /System.Windows.Controls.DataGridLengthConverter$ 80    
IsReadOnly . 1  +System.Windows.Controls.DataGridBoundColumn   Binding �� *ToDate+ �� StringFormat$ 
yyyy/MM/dd��  $ 証明書発行元��$	 250  .  �� *FormatedIssuerName+  $ サブジェクト名��$	 500  .  �� *FromatedSubjectName+  $ シリアル番号��$	 250  .  �� *
SerialNo+� $��7��$��1��$��3��$��1��$��4,2,0,2q�y��� *CertSubjectVisibility+$ TwoWay=�$ PropertyChanged=� .  .  �� $��20 ��� $��* � �� $��30 ��� $��* �.+��� $��0��$��2�� �� Orientation$ 
Horizontal=�.+��� $0��*選択されたクライアント証明書���� -   $��証明書一覧表示��$	��120��$��10,2,2,2q�{��� *
IsEditable+�� $��1��$��1�� �� 
IsReadOnly . ��. $
��Auto=�$
��Auto=����� *CertSubject+$ TwoWay=�$ PropertyChanged=� .  . �� $��1��$��1��$��3��$ 
Horizontal=�.+��� -   $��実行��$��90��$��10,2,2,2q�{��� *IsExecutable+�� -	   $��キャンセル��$��90��$��10,2,2,2q��� $��1��$��3��$��0��$��8��
�������y��� *apiVisibility+._��  �� $
��auto ��� $
��auto ��� $��* ��� $
��auto �.+��� $��0��$��2,2,2,2q�$
��Wrap=�$��18(����� *	apistatus+�� $��1��$ 
Horizontal=�.+��� -
   $��10,4,2,4q�$��キャンセル��$��90��$
��Left=�� $��20,4,2,4q�$	��300��
������ � IsIndeterminate .   � Minimum$ 0Q� � Maximum �� *ProgressMaxValue+ � Value �� *
Progress+�� $��2��$��12,2,2,2q�$
��Auto=�$
��Auto=�.���� ���� *apilog+�� $��3��$ 
Horizontal=�.+��� -   $	��200��$
��Left=�$��10,3,3,3q�$0��*ログをクリップボードにコピー���� -   $	��120��$
��Left=�$��10,3,3,3q�$��接続情報入力��{��� *btnSwitchIsEnabled+