$(function () {
    //用户登录状态判断
    //if (!CheckLoginState()) {
    //    window.location.href = "Login.html";
    //}
    //else {
    //    GetMessage();
    //    GetOnline();
    //    AddExpression();
    //    CheckSend();
    //    UpdateMessage();
    //    UpdateOnline();
    //    Exit();
    //}

    //1.用户登录状态判断
    $.ajax({
        type: "GET",
        url: "Index.aspx",
        data: "action=checkLoginState",
        success: function (data) {
            if (data != "true") {
                window.location.href = "Login.html";
            }
        }
    });

    //2.获取聊天内容
    $.ajax({
        type: "GET",
        url: "Index.aspx",
        data: "action=getMsg",
        error:"",
        success: function (data) {
            $("#chat-dialog-con").html(data);
            //$("#chat-dialog-con ul").html(data);
        } 
    });

    //3.获取在线用户
    $.ajax({
        type: "GET",
        url: "Index.aspx",
        data: "action=getOnline",
        success: function (data) {
            if (data != "null") {
                $("#chat-user-con ul").html(data);
            }
        }
    });

    //强行隐藏滚动条
    $("#right-content-b").niceScroll({
        cursorborder: "",
        cursorcolor: "#dedede",
        boxzoom: true
    });

    //绑定回车事件
    $('#txtInput').keydown(function (e) {
        if (e.keyCode == 13) {
            var sendMsg = $("#txtInput");
            if ($.trim(sendMsg.val()) != "") {
                SendMessage(sendMsg.val());
            }
            else {
                alert("发送内容不能为空!");
                sendMsg.focus();
                return false;
            }
        }
    });

    //6.发送消息判断
    $("#btnSend").click(function () {
        var sendMsg = $("#txtInput");
        if ($.trim(sendMsg.val()) != "") {
            SendMessage(sendMsg.val());
        }
        else {
            alert("发送内容不能为空!");
            sendMsg.focus();
            return false;
        }
    });

    //7.实时刷新聊天内容
    //GetMessage();
    setInterval(GetMessage, 1500);

    //8.实时刷新在线用户
    setInterval(GetOnline, 2000);

    //9.退出时销毁一切信息
    $(window).unload(function () {
        $.ajax({
            type: "GET",
            url: "Index.aspx",
            data: "action=exit",
            async: false,
            success: function (data) { }
        })
    });
});

//发送消息
function SendMessage(msg) {
    var now = new Date();
    $.ajax({
        type: "GET",
        url: "Index.aspx",
        data: "action=sendMsg&con=" + escape(msg) + "&d=" + now.getHours() + ":" + now.getMinutes() + ":" + now.getSeconds(),
        success: function (data) {
            if (data == "success") {
                GetMessage();
                $("#txtInput").val("");
                $("#txtInput").focus();
                scrollB();
            }
            else {
                alert("发送失败!");
                return false;
            }
        }
    });
}

//获取消息
//flagB是否在底部
function GetMessage() {
    var flagB = false;
    if (isBottom()) {
        flagB = true;
    }
    $.ajax({
        type:"GET",
        url:"Index.aspx",
        data:"action=getMsg",
        success: function (data) {
            $("#chat-dialog-con").html(data);
            if (flagB) {
                scrollB();
            }
        }
    });
}

//获取在线用户
function GetOnline() {
    $.ajax({
        type: "GET",
        url: "Index.aspx",
        data: "action=getOnline",
        success: function (data) {
            if (data != "null") {
                $("#chat-user-con ul").html(data);
            }
        }
    });
}

//判断是否到达底部
function isBottom() {
    $("#right-content-b").scroll(function () {
        var scrollTop = $(this).scrollTop();
        var scrollHeight = $(document).height();
        var windowHeight = $(this).height();
        console.log(scrollTop, scrollHeight, windowHeight);
        //if (scrollTop + windowHeight > scrollHeight-50) {
        //    return true;
        //} else {
        //    return false;
        //}
    });
}
//自动滚动到底部
function scrollB() {
    //$("#right-content-b").scrollTop($("#right-content-b")[0].scrollHeight-55);
    $("#right-content-b").scrollTop($("#right-content-b")[0].scrollHeight);
    //console.log($("#right-content-b")[0].scrollHeight);
}