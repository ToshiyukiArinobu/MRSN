   M S B A M L   `   `   `  ���� T  PKyoeiSystem.Framework.Net, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullhJclr-namespace:KyoeiSystem.Framework.Net;assembly=KyoeiSystem.Framework.NetKyoeiSystem.Framework.Net  O Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem  HTTPTEST   TCPTEST.MainWindow   RnetJclr-namespace:KyoeiSystem.Framework.Net;assembly=KyoeiSystem.Framework.Net  R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     9System&clr-namespace:System;assembly=mscorlib     -     � Title$  HTTP通信テスト��$	��700��$��1045.642�� � 	Resources    SecureModeList S� 
MethodName S� 
ObjectType  System.Enum S� MethodParameters M� TypeName��%B   &	        S� $ 	GetValues��"   M� $ net:SecureMode��.���� .@��� $��
HTTP通信��.���  � ColumnDefinitions �� $	��100 ��� $��* � � RowDefinitions �� $
��auto ��� $
��auto ��� $
��auto ��� $
��auto ��� $
��auto ��� $
��auto ��� $��* ��� $
��auto ��� $��* �.+��� $��0��$��2�� �� Orientation$ 
Horizontal=�.+��� -   $��Brows��$��60���� -   $��FileGet��$��70���� -   $��FilePut��$��70���� -   $
��POST��$��70���� -   $��テストURI���� -   $��
MyNumTest1���� -   $��
MyNumTest2���� -	   $��
MyNumTest3���� $��70������ *ptime+	 �� UpdateSourceTrigger$	 PropertyChanged=�� $��0��$��1��$��2��$��Stretch=� �� $
��auto ��� $��* �.+��� $��0��$	��URI���� $��1��$��Stretch=�$��Stretch=����� *	URIString+
 �� Mode$
 TwoWay=�$	 PropertyChanged=� �� NotifyOnSourceUpdated .  �� NotifyOnTargetUpdated . � $��2��$��0��$��2��$	��140�� �� $
��auto ��� $��* � �� $��* ��� $��* �.+��� $��0��$��0��$ Vertical=�.+��� $��パラメータ���� -
   $��追加��$��40��&   System.Windows.Controls.DataGrid  $��0��$��1��   AutoGenerateColumns .     AlternatingRowBackground
 ��ح�   SelectionMode$ Single=�   CanUserAddRows .     CanUserDeleteRows .     FrozenColumnCount$ 1������ *HttpUriParameters+$
 TwoWay=� .  . $	 PropertyChanged=� �� SelectedItem �� *SelectedParameter+$
 TwoWay=� .  . $	 PropertyChanged=�   RowHeaderStyle ��  �� 
TargetType:  4System.Windows.Controls.Primitives.DataGridRowHeader"  .���  �� Property � �� �� Value �� *INDEX+��  � ��$ Right=���  � ��$ 30����  � ��$ Right=�   Columns 4  .System.Windows.Controls.DataGridTemplateColumn  ,  &System.Windows.Controls.DataGridColumn   Header$  ��   CellTemplate �� .R��� -   $��削除��0  *System.Windows.Controls.DataGridTextColumn  $
 Name��   Width5  /System.Windows.Controls.DataGridLengthConverter$	 150 1  +System.Windows.Controls.DataGridBoundColumn   Binding �� *KEY+  $ Value��$ *  �� *VALUE+�� $��1��$��0��$ Vertical=�.+��� $��Reqデータ���� -   $��追加��$��40��  $��1��$��1�� .  
 ��ح�$ Single=� .   .  $ 1������ *HttpReqParameters+$
 TwoWay=� .  . $	 PropertyChanged=� �� *SelectedReqParameter+$
 TwoWay=� .  . $	 PropertyChanged=� �� "  .���  � �� �� *INDEX+��  � ��$ Right=���  � ��$ 30����  � ��$ Right=�   $  �� �� .R��� -   $��削除��  $
 Name��$	 150  �� *KEY+  $ Value��$ *  �� *VALUE+�� $ 
Horizontal=�$��0��$��3��$��2��.+��� -   $��	basicAuth��$��基本認証：��$��Center=� d� 	IsChecked �� *IsBasicAuthEnabled+$
 TwoWay=�$	 PropertyChanged=� .  . �� $ 
Horizontal=�{��� *IsBasicAuthEnabled+$
 TwoWay=�$	 PropertyChanged=� .  . .+��� $��	ユーザ���� $��Stretch=�$
��Left=�$	��200������ *userID+$
 TwoWay=�$	 PropertyChanged=� .  . �� $��パスワード��G� -   $��
htmlPasswd��$	��200���� $ 
Horizontal=�$��0��$��4��$��2��.+��� $0��*サーバー証明書をチェックする��$��Center=�$��Center=�$��0,0,10,0q� �� *IsCheckServerCert+$
 TwoWay=�$	 PropertyChanged=� .  . �� $$��クライアント証明書：��$��Center=�$��Center=�$��10,0,0,0q��� $��Stretch=�$
��Left=�$	��400������ *ClientCertSerialNo+$
 TwoWay=�$	 PropertyChanged=� .  . �� -   $��参照���� $ Vertical=�$��0��$��5��$��2��.+��� 
����  �$��1q��  �� $
��auto ��� $��* �.+��� $��状態��$��0��$
��Left=��� $��1��$��Stretch=�$
��Left=����� *status+$
 TwoWay=�$	 PropertyChanged=� .  . �� $��ヘッダ情報��$��0��$��6��$��Right=��� $��6��$��1��
����ح�$��2q�
�������.���� 
�������$	��1.5q�._��� $��WrapWithOverflow=����� *
HeaderText+�� $��7��$��2��$��5��$��Center=�$��Stretch=� �� ShowsPreview . 
��� ��� �� ResizeDirection$
 Rows=��� $��8��$��0��$ Vertical=�.+��� $��コンテンツ��$��Center=�	� -   $��テキスト��$
��Left=�$
 TrueY�$��20,0,0,0q�	� $
��JSON��$
��Left=�$��20,0,0,0q��� -   $
��Copy��$��40���� $��8��$��1��$
��Auto=�$
��Auto=�.���� 
�������$	��1.5q�._��� $��WrapWithOverflow=����� *ResponseText+���� *HttpLogColor+$
 TwoWay=�$	 PropertyChanged=� .  .   $��8��$��1�� .  
 ��ح�$ Single=�    SelectionUnit$  FullRow=� .   .  $ 1��$
��auto��y��� *HttpResJsonVisibility+$
 TwoWay=� .  . $	 PropertyChanged=����� *HttpResponses+$
 TwoWay=� .  . $	 PropertyChanged=� �� *SelectedResponse+$
 TwoWay=� .  . $	 PropertyChanged=�   $
 Name��$	 150  �� *KEY+  $ Value��$ *  �� *VALUE+�� $��メール送信��$y�Visible=�.���  �� $	��100 ��� $��* � �� $��28 ��� $
��auto ��� $
��auto ��� $
��auto ��� $��28 ��� $��28 ��� $��28 ��� $��28 ��� $��28 ��� $��28 ��� $��* �$��40���� $
��auto ��� $��* �.+��� $��0��$��2��$ 
Horizontal=�.+��� -   $��送信��$��60���� $��70������ *ptime+$	 PropertyChanged=��� $��1��$��0��$��2��$��3��
����  �$��1q�._��  �� $
��auto ��� $��28 ��� $��28 ��� $��28 �.+�� $��0��! � Orientation$! 
Horizontal=�$��Center=�.+��� $��SMTPサーバー��$��Right=��� $��Stretch=�$
��Left=�$	��200������ *
SmtpServer+$
 TwoWay=�$	 PropertyChanged=� .  . �� $��Port:��$��Center=�$��Center=�$��10,0,0,0q��� $��50������ *
SmtpPort+$
 TwoWay=�$	 PropertyChanged=� .  . �� $��接続暗号化:��$��Center=�$��Center=�$��10,0,0,0q��� " �� 
IsEditable" .  $	��100������ # �� Source## [  $ �� SelectedValue$ �� *SmtpSecureMode+$
 TwoWay=�$	 PropertyChanged=� .  . �� $0��*サーバー証明書をチェックする��$��Center=�$��Center=�$��	20,0,10,0q� �� *IsCheckServerCert+$
 TwoWay=�$	 PropertyChanged=� .  . �� $ 
Horizontal=�.+��� $'��!クライアント証明書適用��$��Center=�$��Center=�$��	10,0,10,0q� �� *IsCheckClientCert+$
 TwoWay=�$	 PropertyChanged=� .  . �� $	��200������ *ClientCertSerialNo+$
 TwoWay=�$	 PropertyChanged=� .  . {��� *IsCheckClientCert+$
 TwoWay=�$	 PropertyChanged=� .  . �� -   $��参照��{��� *IsCheckClientCert+$
 TwoWay=�$	 PropertyChanged=� .  . �� $��1��$ 
Horizontal=�$��Center=�.+��� $��
SMTP認証��$��10,0,0,0q�$��Center=� �� *
IsSmtpAuth+$
 TwoWay=�$	 PropertyChanged=� .  . �� $��0q�
����  �$��10,0,0,0q�$��Center=�{��� *IsSmtpAuthEnabled+$
 TwoWay=�$	 PropertyChanged=� .  . ._��� $ 
Horizontal=�$��Center=�.+�	� $��CRAM-MD5��$��10,0,0,0q�$��Center=� �� *IsSmtpAuthCramMd5+$
 TwoWay=�$	 PropertyChanged=� .  . 	� $��LOGIN��$��10,0,0,0q�$��Center=� �� *IsSmtpAuthLogin+$
 TwoWay=�$	 PropertyChanged=� .  . 	� $��PLAIN��$��	10,0,10,0q�$��Center=� �� *IsSmtpAuthPlain+$
 TwoWay=�$	 PropertyChanged=� .  . �� $��ユーザID��$��Center=�$��Center=��� $��80������ *
SmtpUserId+$
 TwoWay=�$	 PropertyChanged=� .  . �� $��パスワード��$��Center=�$��Center=�G� -   $��
smtpPasswd��$��1��$��3��$��80���� $+��%SMTP送信前にPOPログインする��$��20,0,0,0q�$��Center=� �� *IsPopBeforeSmtp+$
 TwoWay=�$	 PropertyChanged=� .  . �� $��2��$ 
Horizontal=�$��Center=�{��� *IsPopEnabled+$
 TwoWay=�$	 PropertyChanged=� .  . .+��� $��POPサーバー��$��Center=�$��Center=��� $	��180������ *	PopServer+$
 TwoWay=�$	 PropertyChanged=� .  . �� $��Port:��$��Center=�$��Center=�$��10,0,0,0q��� $��50������ *	PopPort+$
 TwoWay=�$	 PropertyChanged=� .  . �� $ 
Horizontal=�.+��� $��プロトコル:��$��Center=�$��Center=�$��10,0,0,0q�	� $
��POP3��$��5,0,0,0q�$��Center=� �� *IsPOP3+$
 TwoWay=�$	 PropertyChanged=� .  . 	� $
��IMAP��$��5,0,0,0q�$��Center=� �� *IsIMAP+$
 TwoWay=�$	 PropertyChanged=� .  . �� $ 
Horizontal=�.+��� $��POP接続暗号化:��$��Center=�$��Center=�$��10,0,0,0q��� " .  $	��100������ ## [  $ �� *PopSecureMode+$
 TwoWay=�$	 PropertyChanged=� .  . �� $��3��$ 
Horizontal=�$��Center=�{��� *IsPopEnabled+$
 TwoWay=�$	 PropertyChanged=� .  . .+��� $��	POP認証��	� $��標準��$��10,0,0,0q�$��Center=� �� *IsPopAuthStd+$
 TwoWay=�$	 PropertyChanged=� .  . 	� $��CRAM-MD5��$��10,0,0,0q�$��Center=� �� *IsPopAuthCramMd5+$
 TwoWay=�$	 PropertyChanged=� .  . 	� $
��APOP��$��10,0,0,0q�$��Center=� �� *IsPopAuthAPOP+$
 TwoWay=�$	 PropertyChanged=� .  . 	� $
��NTLM��$��	10,0,10,0q�$��Center=� �� *IsPopAuthNTLM+$
 TwoWay=�$	 PropertyChanged=� .  . �� $��POPユーザID��$��Center=�$��Center=��� $��80������ *	PopUserId+$
 TwoWay=�$	 PropertyChanged=� .  . �� $��POPパスワード��$��Center=�$��Center=�G� -   $��	popPasswd��$��80���� $��POP後待ち時間(msec)��$��Center=�$��Center=��� $��60������ *PopDelayTime+$
 TwoWay=�$	 PropertyChanged=� .  . �� $��From:��$��0��$��4��$��Right=��� $��1��$��4��$	��300��$
��Left=����� *
MailFrom+$
 TwoWay=�$	 PropertyChanged=� .  . �� $	��To:��$��0��$��5��$��Right=��� $��1��$��5��$��Stretch=�$
��Left=����� *MailTo+$
 TwoWay=�$	 PropertyChanged=� .  . �� $	��Cc:��$��0��$��6��$��Right=��� $��1��$��6��$��Stretch=�$
��Left=����� *MailCc+$
 TwoWay=�$	 PropertyChanged=� .  . �� $
��Bcc:��$��0��$��7��$��Right=��� $��1��$��7��$	��300��$
��Left=����� *	MailBcc+$
 TwoWay=�$	 PropertyChanged=� .  . �� $��	Reply-To:��$��0��$��8��$��Right=��� $��1��$��8��$ 
Horizontal=�.+��� $	��300��$
��Left=����� *MailReplyTo+$
 TwoWay=�$	 PropertyChanged=� .  . �� $��Sender:��$��0��$��8��$��Right=��� $	��300��$
��Left=����� *
MailSender+$
 TwoWay=�$	 PropertyChanged=� .  . �� $��件名��$��0��$��9��$��Right=��� $��1��$��9��$��Stretch=����� *MailSubject+$
 TwoWay=�$	 PropertyChanged=� .  . �� $��メール本文��$��0��$��10��$��Right=�� $��1��$��10�� �� $��* � �� $��* ��� $
��auto ��� $
��auto �$	��100��.+��� $��0��.���� $��Stretch=�$
��Left=�% �� AcceptsReturn% . $��	MS Gothic)����� *MailBodyText+$
 TwoWay=�$	 PropertyChanged=� .  . �� $��1��$��5��$��Stretch=�$��Center=� . 
��� ���$ Columns=�� $��2�� �� $
��auto ��� $��* �.+��� $ 
Horizontal=�$��0��.+��� $��添付ファイル��$��26���� -   $��	クリア���� -   $��1��$��Stretch=�$��Stretch=�$��Stretch=�& J� 	AllowDrop& . ���� *	MailFiles+$
 TwoWay=�$	 PropertyChanged=� .  . �� $��11��$��2��$��5��$��Center=�$��Stretch=� . 
��� ���$
 Rows=��� $��通信ログ��$��0��$��12��$��Right=��� $��1��$��12��.���� $��Stretch=�$
��Left=�% . $��	MS Gothic)����� *MailLogColor+$
 TwoWay=�$	 PropertyChanged=� .  . ���� *	MailLog+$
 TwoWay=�$	 PropertyChanged=� .  . 