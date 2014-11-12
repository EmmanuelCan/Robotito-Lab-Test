<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>REST JSON C#</title>
    <link href="Css/cssLab.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div style="margin:auto auto; width:550px">
    
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <asp:Button ID="btnNew" runat="server" Text="Nuevo" onclick="Button1_Click" />


                <asp:GridView ID="GridView1" runat="server" AllowPaging="True" 
                    AutoGenerateColumns="False" ondatabinding="GridView1_DataBinding" 
                    onpageindexchanging="GridView1_PageIndexChanging"  CssClass ="mGrid"
                     CellPadding="6"
                    onrowcommand="GridView1_RowCommand" 
                    onrowediting="GridView1_RowEditing" onrowupdated="GridView1_RowUpdated" 
                    onrowupdating="GridView1_RowUpdating" 
                    onrowcancelingedit="GridView1_RowCancelingEdit">
                    <Columns>
                        <asp:CommandField ShowEditButton="True"  />
                        <asp:BoundField DataField="UserName" HeaderText="User Name"   />
                        <asp:BoundField DataField="Email" HeaderText="Email" />
                        <asp:BoundField DataField="Phone" HeaderText="Phone" />
                    </Columns>
                </asp:GridView>
            </ContentTemplate>
        </asp:UpdatePanel>
    <center id="info" style ="padding:10px; border :2px outset navy" >Robotcito Lab Test- EACP Coder</center>
    </div>
    </form>
</body>
</html>
