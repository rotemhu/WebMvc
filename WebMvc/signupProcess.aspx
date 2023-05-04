<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="signupProcess.aspx.cs" Inherits="WebMvc.signupProcess" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <%
        //קבלת הנתונים מהבקשה
        string username = Request.Form["username"].Trim();
        string password = Request.Form["password"].Trim();
        string email = Request.Form["email"].Trim();
        //שדות רשות - אם השדה נול נכניס מחרוזת ריקה
        string country = Request.Form["country"] == null? "" : Request.Form["country"].Trim();
        //שדה רשות מסוג מספר - אם לא הוכנס ערך הוא מאותחל למחרוזת ריקה
        int age = Request.Form["age"].ToString() == ""? 0 : int.Parse(Request.Form["age"].ToString()); //לתקן

        //אריזה בעצם מטיפוס משתמש
        UserSystem.User user = new UserSystem.User(
            username,
            password,
            email,
            country,
            age,
            false
            );

        //עדכון בבסיס הנתונים
        bool signupSucceed = UserSystem.UserController.SignUp(user);

        //בדיקה אם עדכון הצליח
        if (!signupSucceed)
        {
                %> <h2>The username is taken!</h2> 
    <%
        }
        else
        {
            %>
    <h1>You've successfully signed up!</h1>
    <h2>Your details...</h2>
    <table>
        <tr>
            <td>Username: </td><td><%=username%></td>
        </tr>
        <tr>
            <td>Password: </td><td>****<%=password.Substring(password.Length - 3)%></td>
        </tr>
        <tr>
            <td>Email: </td><td><%=email%></td>
        </tr>
        <tr>
            <td>Country: </td><td><%=country%></td>
        </tr>
        <tr>
            <td>Age: </td><td><%=age%></td>
        </tr>
    </table>
    <% } %>
    <a href="index.aspx">Home Page...</a>
</body>
</html>
