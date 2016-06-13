using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Chat.Dal;
using System.Data;

public partial class Index : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string action = Request.QueryString["action"];
        if (action == "getName")
            GetName();
        else if (action == "getOnline")
            GetOnline();
        else if (action == "sendMsg")
            SendMessage(Request.QueryString["con"], Request.QueryString["rec_id"], Request.QueryString["d"]);
        else if (action == "getMsg")
            GetMessage(Request.QueryString["recid"]);
        else if (action == "exit")
            Exit();
            
    }

    //返回用户名
    protected void GetName()
    {
        try
        {
            Response.Write(Session["user"].ToString());
        }
        catch (Exception)
        {
            throw ;
        }
    }

    //发送消息
    protected void SendMessage(string msg, string rec_id, string time)
    {
        Messages m = new Messages();
        MessagesDal d = new MessagesDal();
        m.Msg_con = msg;
        if ((rec_id==null) ||(rec_id==""))
        {
        }
        else
        {
            m.Msg_rec = Convert.ToInt32(rec_id);
        }
        m.Msg_send = Convert.ToInt32(Session["userid"]);
        m.Msg_sendTime = Convert.ToDateTime(time);
        d.Insert(m);
        Response.Write("success");
    }

    //获取消息
    //protected void GetMessage()
    //{
    //    MessagesDal dd = new MessagesDal();
    //    DataTable dt1 = dd.GetMessage();
    //    for (int i = 0; i < dt1.Rows.Count; i++)
    //    {
    //        if (Convert.ToString(Session["userid"]) == Convert.ToString(dt1.Rows[i]["Msg_send"]))
    //        {
    //            Response.Write(@"<li><p class='time'><span>"+ dt1.Rows[i]["Msg_sendTime"]+ "</span></p><div class='main'><img class='avatar' width='30' height='30' src='Images/2.png'><div class='text'>"+dt1.Rows[i]["Msg_con"]+"</div></div></li>");
    //        }
    //        else
    //        {
    //            Response.Write(@"<li><p class='time'><span>" + dt1.Rows[i]["Msg_sendTime"] + "</span></p><div class='main self'><img class='avatar' width='30' height='30' src='Images/2.png'><div class='text'>" + dt1.Rows[i]["Msg_con"] + "</div></div></li>"); }
    //    }
    //}

    //获取对应消息


    protected void GetMessage(string rec_id)
    {
        MessagesDal dd = new MessagesDal();
        DataTable dt1 = dd.GetMessage(rec_id, Convert.ToInt32(Session["userid"]));

        for (int i = 0; i < dt1.Rows.Count; i++)
        {
            DateTime date1 = Convert.ToDateTime(dt1.Rows[i]["Msg_sendTime"]);

            if (Convert.ToString(Session["userid"]) == Convert.ToString(dt1.Rows[i]["Msg_send"]))
            {
                Response.Write(@"<li><p class='time'><span>" + date1.TimeOfDay + "</span></p><div class='main'><img class='avatar' width='30' height='30' src='Images/2.png'><div class='text'>" + dt1.Rows[i]["Msg_con"] + "</div></div></li>");
            }
            else
            {
                Response.Write(@"<li><p class='time'><span>" + dt1.Rows[i]["Msg_sendTime"] + "</span></p><div class='main self'><img class='avatar' width='30' height='30' src='Images/2.png'><div class='text'>" + dt1.Rows[i]["Msg_con"] + "</div><span class='chat-list-name'> " + dt1.Rows[i]["NickName"] + "</span></div></li>");
            }
        }
    }

    //用户退出后销毁用户信息和聊天内容
    protected void Exit()
    {
       Session.Abandon();
    }

    //获取在线用户
    protected void GetOnline()
    {
        usersDal ud1 = new usersDal();
        DataTable dt2 = ud1.inquiry_users();
        if (dt2.Rows.Count<=0)
        {
            Response.Write("fail");
        }
        else
        {
            for (int i = 0; i < dt2.Rows.Count; i++)
            {
                string inid = Convert.ToString(dt2.Rows[i]["UserID"]);
                string inname = Convert.ToString(dt2.Rows[i]["NickName"]);
                if (Convert.ToInt32(Session["userid"])== Convert.ToInt32(inid))
                {
                    inid = "";
                    inname = "张家港时间众筹会所";
                }
                Response.Write("<li id='"+inid+"'>" + inname + "</li>");
            }
        }
    }

    //用户登录状态判断
    //protected void CheckUserState()
    //{
    //    if (Session["user"] != null)
    //        Response.Write("true");
    //    else
    //        Response.Write("false");
    //}

}