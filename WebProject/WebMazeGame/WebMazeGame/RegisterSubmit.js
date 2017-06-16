$("#btnAddUser").click(function () {
    var user = {
        Name: $("#username").val(),
        Password: $("#password").val(),
        Email: $("#email").val()
    };
    alert("Product added successfully");
    $.post("api/Users/AddUser", user)
    .done(function () {
        alert("done!");
        localStorage.userName = $("#username").val();
        $('#content').load("Home.html");
    });
});



/*function onPress() {
    alert("Product added successfully");
    var user = {
        Name: $("#username").val(),
        Password: $("#password").val(),
        Email: $("#email").val()
    };
    alert("Product2 added successfully");
    $.post("api/User", user)
    .done(function () {
        alert("Product3 added successfully");
        localStorage.userName = $("#username").val();
        $('#content').load("Home.html");
    });
};*/

