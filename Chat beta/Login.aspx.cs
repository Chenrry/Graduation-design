using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Chat.Dal;
using System.Data;

public partial class Login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        UserLogin(Request["name"], Request["pwd"]);
    }

    //用户登录
    protected void UserLogin(string name, string pwd)
    {
        usersDal md = new usersDal();
        DataTable dt1 = md.inquiry_login(name);

        if (dt1.Rows.Count <= 0)
        {
            Response.Redirect("fail");
            return;
        }

        if (dt1.Rows[0][0].ToString() == pwd)
        {
            Session["user"] = dt1.Rows[0][1].ToString();
            Session["userid"] = name;
            Response.Write("success");
        }
        else
        {
            Response.Redirect("fail");
        }

    }
}