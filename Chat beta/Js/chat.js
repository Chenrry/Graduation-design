$(function () {

    //1.获取用户名
    $.ajax({
        type: "GET",
        url: "Index.aspx",
        data: "action=getName",
        success: function (data) {
            rec_id="";
            $("#username").html(data);
        }
    });

    //2.获取聊天内容
    $.ajax({
        type: "GET",
        url: "Index.aspx",
        data: "action=getMsg",
        error: "",
        success: function (data) {
            $(".m-message ul").html(data);
            scrollB();
        }
    });


    //3.获取在线用户
    $.ajax({
        type: "GET",
        url: "Index.aspx",
        data: "action=getOnline",
        success: function (data) {
            if (data != "null") {
                $("#m-list-ul").html(data);
            } else {
                console.log("user list data null");
            }
        }
    });

    //对应id获取消息
    $('#m-list-ul').on('click', 'li', function () {
        console.log("rec_id change");
        rec_id = $(this).attr('id');
        GetMessage();
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


    //7.实时刷新聊天内容
    //GetMessage();
    setInterval(GetMessage, 1500);

    //8.实时刷新在线用户
    setInterval(GetOnline, 1000);

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
        data: "action=sendMsg&con=" + escape(msg) +"&rec_id="+rec_id+ "&d=" + now.getHours() + ":" + now.getMinutes() + ":" + now.getSeconds(),
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
function GetMessage() {
    $.ajax({
        type: "GET",
        url: "Index.aspx",
        data: "action=getMsg&recid=" + rec_id,
        success: function (data) {
            $(".m-message ul").html(data);
            scrollB();
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
                $("#m-list-ul").html(data);
            } else {
                console.log("user list data null");
            }
        }
    });
}
//是否底部
function isBottom() {
    var viewH = $(".m-message").height(),//可见高度
      contentH = $(".m-message").get(0).scrollHeight,//内容高度
      scrollTop = $(".m-message").scrollTop();//滚动高度
    if ((scrollTop + viewH) <= contentH - 20) {
        Bflag = true;
    }
    else {
        Bflag = false;
    }
}
//自动滚动到底部
function scrollB() {
    $(".m-message").scrollTop($(".m-message")[0].scrollHeight);
}