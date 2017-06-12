$(function () {
    $('#nav h2 button').on('click', function (e) {
        e.preventDefault();
        $('#content').load("Register.html");
    });
});
