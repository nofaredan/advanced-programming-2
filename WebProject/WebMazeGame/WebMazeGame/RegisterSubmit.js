$("#btnAddUser").click(function () {
    // create a user
    var user = {
        Name: $("#username").val(),
        Password: $("#password").val(),
        Email: $("#email").val()
    };

    var inputs = document.getElementsByTagName('input');
    for (var i = 0; i < inputs.length; ++i) {
        if (inputs[i].value == '') {
            $('#submit_handle').click();
            return;
        }
    }

    // check if the passwords match
    if (user.Password != $("#confirmPassword").val()){
        alert("the passwords don't match");
        return;
    }
    $.post("api/Users/AddUser", user)
        .done(function (data) {
        // if the user exists
        if (data == "exist") {
            alert("This user already exists");
        } else {
            // a new user is created
            localStorage.userName = $("#username").val();

            // load main manu
           $('#content').load("Home.html");
            var buttonLogIn = document.getElementById("login");
            buttonLogIn.innerHTML = "Log off";
            sessionStorage.setItem("ifUserIn", "yes");
            document.getElementById("register").innerHTML = "hello " + user.Name;
        }
        }).fail(function (jqXHR, textStatus, err) {
            alert("Error: " + err);
        });
});