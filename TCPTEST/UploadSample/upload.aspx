<%@ Import Namespace="System"%>
<%@ Import Namespace="System.IO"%>
<%@ Import Namespace="System.Net"%>
<%@ Import NameSpace="System.Web"%>

<Script language="C#" runat=server>
void Page_Load(object sender, EventArgs e) {
	
	foreach(string f in Request.Files.AllKeys) {
		HttpPostedFile file = Request.Files[f];
		file.SaveAs("C:\\Web\\Input\\" + file.FileName);
	}	
}

</Script>
<html>
<body>
<p> Upload complete.  </p>
</body>
</html>
