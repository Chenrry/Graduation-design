using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Chat.Dal;

public partial class Index : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string action = Request.QueryString["action"];
        if (action == "checkLoginState")
            CheckUserState();
        else if (action == "getOnline")
            GetOnline();
        else if (action == "sendMsg")
            SendMessage(Request.QueryString["con"], Request.QueryString["d"]);
        else if (action == "getMsg")
            GetMessage();
        else if (action == "exit")
            Exit();
            
    }

    //发送消息
    protected void SendMessage(string msg, string time)
    {
        Messages m = new Messages();
        MessagesDal d = new MessagesDal();
        m.Msg_con = msg;
        m.Msg_sendTime = Convert.ToDateTime(time);
        d.Insert(m);

        Application.Lock();  //锁定application
        if (Application["msg"] != null)
        {
            Application["msg"] += @"<li class='rec-msg clear-fix'><div class='chating-icon icon-left'><img src = '../Images/usericon.jpg' ></div><div class='mes-con re-mes'><p>" + msg + @"</p></div></li>";
        }
        else
        { 
            Application["msg"] = @"<li class='rec-msg clear-fix'><div class='chating-icon icon-left'><img src = '../Images/usericon.jpg' ></div><div class='mes-con re-mes'><p>" + msg + @"</p></div></li>";
        }
        Application.UnLock();
        Response.Write("success");
    }

    //获取消息
    protected void GetMessage()
    {
        Response.Write(Application["msg"]);
    }

    //用户登录状态判断
    protected void CheckUserState()
    {
        if (Session["user"] != null)
            Response.Write("true");
        else
            Response.Write("false");
    }

    //用户退出后销毁用户信息和聊天内容
    protected void Exit()
    {
        Session.Abandon();
        Application.RemoveAll();
    }

    //获取在线用户
    protected void GetOnline()
    {
        if (Application["user"] != null)
            Response.Write("<li>" + Application["user"] + "</li>");
        else
            Response.Write("null");
    }
}