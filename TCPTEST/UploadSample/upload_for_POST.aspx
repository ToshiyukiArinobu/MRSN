<%@ Page Language="C#" %>
<script runat="server">
    string resultMsg;
    protected void uploadButton_Click(object sender, EventArgs e)
    {
        string fileName = FileUpload1.FileName;
        PlaceHolder1.Visible = false;
        PlaceHolder2.Visible = true;
        try
        {
            FileUpload1.SaveAs(Request.PhysicalApplicationPath + "\\upload\\test\\" + fileName);
            resultMsg = "�t�@�C�� \"" + fileName + "\" ���A�b�v���[�h���܂����B";
        }
        catch(Exception exp)
   {
     resultMsg = "�t�@�C���̃A�b�v���[�h�Ɏ��s���܂����B<br />���� : " + exp.Message;
        } 
    }
</script>

<html>
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <asp:PlaceHolder ID="PlaceHolder1" runat="server">
    <!-- �A�b�v���[�h�O�̉�� -->
     <asp:FileUpload ID="FileUpload1" runat="server" />
 <br />
        <asp:Button ID="uploadButton" 
  runat="server" onclick="uploadButton_Click" 
  Text="�A�b�v���[�h" />
    <!-- �����܂� --->
    </asp:PlaceHolder>
    <asp:PlaceHolder ID="PlaceHolder2" Visible="false"  runat="server">
    <!-- �A�b�v���[�h��̉�� -->
   <%=resultMsg%>
    <!-- �����܂� --->
    </asp:PlaceHolder>
    </form>
</body>
</html>
