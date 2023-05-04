<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="updateDetails.aspx.cs" Inherits="WebMvc.updateDetails" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%
        //אבטחה - אם משתמש שאינו רשום מנסה להכנס - העברה לדף אין הרשאה
        if (UserSystem.LoggedInUser.IsGuest())
            Response.Redirect("noPermission.aspx");%>
    
    <h2>Update Details</h2>
    <% UserSystem.User user = UserSystem.LoggedInUser.GetUser(); %>
    <form action="updateDetailsProcess.aspx" method="post" onsubmit="return checkForm()">
        <input type="text" name="username" placeholder="User name" value="<%=user.GetUsername()%>" /><br />
        <input type="password" name="password" placeholder="Password" value="<%=user.GetPassword()%>" /><br />
        <input type="password" name="co_password" placeholder="Confirm Password" value="<%=user.GetPassword()%>" /><br />
        <input type="email" name="email" placeholder="Email" value="<%=user.GetEmail()%>" /><br />
        <input type="text" name="country" placeholder="Country" value="<%=user.GetCountry()%>" /><br />
        <input type="text" name="age" placeholder="Age" value="<%=user.GetAge()%>" /><br />

        <input type="submit" value="Update" />
        <input type="button" value="Cancel" onclick ="location.href='<%=Request.UrlReferrer %>'" />
    </form>
</asp:Content>
