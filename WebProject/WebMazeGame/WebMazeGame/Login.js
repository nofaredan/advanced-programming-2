$("#btnLogIn").click(function () {
    var user = {
        Name: $("#username").val(),
        Password: $("#password").val(),
        Email: $("#password").val(),
    };

    var inputs = document.getElementsByTagName('input');
    for (var i = 0; i < inputs.length; ++i) {
        if (inputs[i].value == '') {
            $('#submit_handle').click();
            return;
        }
    }

    $.post("api/Users/CheckUserForLogIn", user)
        .done(function (data) {
            // if the user exists
            if (data == "not exist") {
                alert("the user or password is incorrect");
                return;
            } else {
                // save in session storage
             //   sessionStorage.setItem("user", data.Name);
                // a new user is created
                localStorage.userName = $("#username").val();

                // load main manu
                $('#content').load("Home.html");
                var buttonLogIn = document.getElementById("login");
                buttonLogIn.innerHTML = "Log off";
                sessionStorage.setItem("ifUserIn", "yes");
                document.getElementById("register").innerHTML = "hello " + data.Name;
            }
        });
});
