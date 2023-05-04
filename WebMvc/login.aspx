<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="WebMvc.login" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
        <h1>Log in</h1>
    <form action ="loginProcess.aspx" method="post">
        <%  if (Request.QueryString["success"] == "false")
                Response.Write("Username or password is wrong! <br />");%>
        <input type ="text" name="usr" placeholder="username"/>
        <input type ="text" name="pass" placeholder="password"/>
        <input type="submit" value="Login" />
    </form>
</asp:Content>
