window.fbAsyncInit = function () {
    FB.init({
        appId: '1776547099029856',
        status: true,
        cookie: true,
        xfbml: true
    });
};
(function (doc) {
    var js;
    var id = 'facebook-jssdk';
    var ref = doc.getElementsByTagName('script')[0];
    if (doc.getElementById(id)) {
        return;
    }
    js = doc.createElement('script');
    js.id = id;
    js.async = true;
    js.src = "//connect.facebook.net/en_US/all.js";
    ref.parentNode.insertBefore(js, ref);
}(document));

function LoginFB() {

    FB.login(function (response) {
        if (response.authResponse) {
            FB.api('/me?fields=id,name,email', function (response) {
                var MethodParam = { 'SocialMediaID': response.id, 'Email': response.email, 'Name': response.name, 'SocialMediaType': 1 };
                $.ajax({
                    type: 'POST',
                    contentType: "application/json; charset=utf-8",
                    url: '/Account/Register_Login_Website',
                    dataType: "json",
                    async: false,
                    data: JSON.stringify(MethodParam),
                    success: function (response) {
                        debugger;
                        if (response.result != "0") {
                            location.reload();
                        }
                        else {
                            UserId = 0;
                        }
                    },
                    error: function () {
                    }
                });
            });
        } else {
            $('#ErrorMessage').text("Login attempt failed!");
        }
    }, { scope: 'user_about_me,email' });
}