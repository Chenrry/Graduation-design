using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        UserLogin(Request["name"], Request["pwd"]);
    }

    //用户登录
    protected void UserLogin(string name, string pwd)
    {
        if ((name == "张三" || name == "李四" || name == "王二" || name == "麻子") && pwd == "123456")
        {
            Session["user"] = name; //记录用户登录状态
            Application["user"] = name; //记录用户发送内容
            Response.Write("success");
        }
        else
            Response.Write("fail");
    }
}