<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="signup.aspx.cs" Inherits="WebMvc.signup" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="scripts/validation.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h2>Sign Up</h2>
    <form action="signupProcess.aspx" method="post" onsubmit="return checkForm()">
        <input type="text" name="username" placeholder="User name"/>
        <div class="badge text-bg-primary" id="info_username">The username length must be between 4-16 characters.</div><br />
        <input type="password" name="password" placeholder="Password" />
        <div class="badge text-bg-primary" id="info_password">The password length must be between 8-20 characters.</div><br />
        <input type="password" name="co_password" placeholder="Confirm Password" />
        <div class="badge text-bg-primary" id="info_co_password">The passwords must be the same</div><br />
        <input type="email" name="email" placeholder="Email" />
        <div class="badge text-bg-primary" id="info_email">The email must be valid</div><br />
        <input type="text" name="country" placeholder="Country" /><br />
        <input type="number" name="age" placeholder="Age" /><br />

        <input type="checkbox" id="cbxTerms" required/><label for="cbxTerms">I agree the <a href="terms.htm">Terms Of Use</a></label><br />
        <input type="submit" value="Sign Up!" />
    </form>
</asp:Content>