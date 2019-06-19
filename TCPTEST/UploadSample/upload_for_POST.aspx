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
            resultMsg = "ファイル \"" + fileName + "\" をアップロードしました。";
        }
        catch(Exception exp)
   {
     resultMsg = "ファイルのアップロードに失敗しました。<br />原因 : " + exp.Message;
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
    <!-- アップロード前の画面 -->
     <asp:FileUpload ID="FileUpload1" runat="server" />
 <br />
        <asp:Button ID="uploadButton" 
  runat="server" onclick="uploadButton_Click" 
  Text="アップロード" />
    <!-- ここまで --->
    </asp:PlaceHolder>
    <asp:PlaceHolder ID="PlaceHolder2" Visible="false"  runat="server">
    <!-- アップロード後の画面 -->
   <%=resultMsg%>
    <!-- ここまで --->
    </asp:PlaceHolder>
    </form>
</body>
</html>
