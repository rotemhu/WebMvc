using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.SessionState;

namespace UserSystem
{
    public class User
    {
        private string username;
        private string password;
        private string email;
        private string country;
        private int age;
        private bool isManager;

        public User(string username, string password, string email, string country, int age, bool isManager)
        {
            this.username = username;
            this.password = password;
            this.email = email;
            this.country = country;
            this.age = age;
            this.isManager = isManager;
        }

        public string GetUsername() { return username; }
        public void SetUsername(string username) { this.username = username; }

        public string GetPassword() { return password; }
        public void SetPassword(string password) { this.password = password; }
        public string GetEmail() { return email; }
        public void SetEmail(string email) { this.email = email; }
        public string GetCountry() { return country; }
        public void SetCountry(string country) { this.country = country; }
        public int GetAge() { return age; }
        public void SetAge(int age) { this.age = age; }
        public bool IsManager() { return isManager; }
        public void SetManager(bool isManager) { this.isManager = isManager; }

        public override string ToString()
        {
            return $"username: {username}, pass: {password}, email: {email}, country: {country}, age: {age}, manager: {isManager}";
        }

    }
    public class UserController
    {
        //התחברות
        public static bool Login(string username, string password) {
            // שאיבת פרטי המשתמש ממסד הנתונים
            User user = UserDao.GetUser(username);
            
            //בדיקה אם שם המשתמש שהתקבל קיים
            if (user == null)
                return false;
            
            //בדיקה אם הסיסמא שהתקבלה תואמת
            if (user.GetPassword() != password)
                return false;

            //שם המשתמש והסיסמא נכונים
            //קביעת המשתמש כמחובר
            LoggedInUser.SetUser(user);
            
            //החזרת ערך - התחברות הצליחה
            return true;
        }
        //התנתקות
        public static void Logout() {
            LoggedInUser.Logout();
        }
        public static bool SignUp(User user) {
            //בדיקה אם שם המשתמש כבר תפוס
            if(UserDao.IsExist(user.GetUsername()))
                return false;

            //אם לא - יצירת רשומת משתמש חדשה בבסיס הנתונים
            UserDao.CreateUser(user);
            return true; //דיווח שהמשתמש נוצר בהצלחה
        }
        public static bool UpdateUserDetails(string username, User newUserDetails) {
            
            //בדיקה אם שם המשתמש אמור להשתנות
            if(username != newUserDetails.GetUsername())
            {
                //בדיקה אם שם המשתמש החדש תפוס
                if (UserDao.IsExist(newUserDetails.GetUsername()))
                    return false;
            }

            UserDao.UpdateUser(username, newUserDetails);
            return true;
        }
    }
    public class UserDao
    {
        const string DB_FILE_NAME = "myDb.mdf";
        const string TABLE_NAME = "users";
        public static void CreateUser(User user) {
            int isManagerNum = user.IsManager() ? 1 : 0;
            string insertQuery = $"INSERT INTO {TABLE_NAME} VALUES(" +
                $"'{user.GetUsername()}', " +
                $"'{user.GetPassword()}', " +
                $"'{user.GetEmail()}', " +
                $"'{user.GetCountry()}', " +
                $"{user.GetAge()}, " +
                $"{isManagerNum})";
            Helper.DoQuery(DB_FILE_NAME, insertQuery);
        }
        public static void UpdateUser(string username, User newUserDetails) {
            int isManagerNum = newUserDetails.IsManager() ? 1 : 0;
            string updateQuery = $"UPDATE {TABLE_NAME} SET " +
                $"user_name='{newUserDetails.GetUsername()}', " +
                $"password='{newUserDetails.GetPassword()}', " +
                $"email='{newUserDetails.GetEmail()}', " +
                $"country='{newUserDetails.GetCountry()}', " +
                $"age={newUserDetails.GetAge()}, " +
                $"is_manager='{isManagerNum}' " +
                $"WHERE user_name='{username}'";
            Helper.DoQuery(DB_FILE_NAME, updateQuery);
        }
        public static void DeleteUser(string username) {
            string deleteQuery = $"DELETE FROM {TABLE_NAME} WHERE username='{username}'";
            Helper.DoQuery(DB_FILE_NAME, deleteQuery);
        }
        public static User GetUser(string username) {
            string selectQuery = $"SELECT * FROM {TABLE_NAME} WHERE user_name='{username}'";
            DataTable res = Helper.ExecuteDataTable(DB_FILE_NAME, selectQuery);

            if(res.Rows.Count == 0)
                return null;

            string password = res.Rows[0]["password"].ToString();
            string email = res.Rows[0]["email"].ToString();
            string country = res.Rows[0]["country"].ToString();
            int age = int.Parse(res.Rows[0]["age"].ToString());
            bool isManager = bool.Parse(res.Rows[0]["is_manager"].ToString());

            User user = new User(username, password, email, country, age, isManager);
            return user;
        }
        public static User[] GetAllUsers() {
            string selectQuery = $"SELECT * FROM {TABLE_NAME}";
            DataTable res = Helper.ExecuteDataTable(DB_FILE_NAME, selectQuery);

            User[] users = new User[res.Rows.Count];

            for (int i = 0; i < users.Length; i++)
            {
                string username = res.Rows[i]["user_name"].ToString();
                string password = res.Rows[i]["password"].ToString();
                string email = res.Rows[i]["email"].ToString();
                string country = res.Rows[i]["country"].ToString();
                int age = int.Parse(res.Rows[i]["age"].ToString());
                string f = res.Rows[i]["is_manager"].ToString();
                bool isManager = bool.Parse(res.Rows[i]["is_manager"].ToString());

                users[i] = new User(username, password, email, country, age, isManager);
                
            }
            return users;
        }

        public static bool IsExist(string username)
        {
            return GetUser(username) != null;
        }
    }
    public class LoggedInUser
    {
        public static void Init()
        {
            HttpSessionState Session = HttpContext.Current.Session;
            Session["user_name"] = "Guest";
            Session["admin"] = "False";

        }
        public static void SetUser(User user)
        {
            HttpSessionState Session = HttpContext.Current.Session; //הפניה לסשן הנוכחי
            Session["user_name"] = user.GetUsername();
            Session["admin"] = user.IsManager().ToString();
        }
        public static User GetUser()
        {
            //שאיבת שם המשתמש מהסשן
            HttpSessionState Session = HttpContext.Current.Session; //הפניה לסשן הנוכחי
            string username = Session["user_name"].ToString();

            //בדיקה אם אף אחד לא מחובר
            if (username == "Guest")
                return null;

            //שאיבת הפרטים של המשתמש מבסיס הנתונים
            User user = UserDao.GetUser(username);

            return user;
        }
        public static string GetUsername() {
            HttpSessionState Session = HttpContext.Current.Session; //הפניה לסשן הנוכחי
            return Session["user_name"].ToString();
        }
        public static bool IsAdmin() {
            HttpSessionState Session = HttpContext.Current.Session; //הפניה לסשן הנוכחי
            return Session["admin"].ToString() == "True";
        }
        public static bool IsGuest()
        {
            return GetUser() == null;
        }
        public static void Logout()
        {
            HttpSessionState Session = HttpContext.Current.Session;
            Session.Abandon();
        }
        
        
    }
    
}