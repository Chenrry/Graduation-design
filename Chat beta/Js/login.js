$(function () {

    //登录判断
    $("#login-button").click(function () {
        var name = $("#txtName");
        var pwd = $("#txtPwd");
        if ($.trim(name.val()) != "" && $.trim(pwd.val()) != "") {
            console.log("login");
            Login(name.val(), pwd.val());
            return false;
        }
        else {
            if ($.trim(name.val())== "") {
                alert("用户名不能为空!");
                name.focus();
                return false;
            }
            else {
                alert("密码不能为空!");
                pwd.focus();
                return false;
            }
        }
    });
});

//登录
function Login(name, pwd) {
    //debugger
    $.ajax({
        type: "POST",
        url: "Login.aspx",
        data: "name=" + name + "&pwd=" + pwd,
        success: function (res) {
            if (res == "success") {
                location.href = "Index.html";
            }
            else {
                alert("用户名或密码不正确!");
                return false;
            }
        }
    });
}
