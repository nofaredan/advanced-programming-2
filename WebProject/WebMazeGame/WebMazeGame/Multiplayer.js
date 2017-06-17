/*
note to Nofar:
---------------------------------------
to update a winner use:
    var user = {
        Name: "ana"
    };
    $.post("api/Users/UpdateWinner", user)
        .done(function (data) {
            alert("done!");
    });
** You just need to send the name.
----------------------------------------
to update the looser use:
    var user = {
        Name: "ana"
    };
    $.post("api/Users/UpdateLooser", user)
        .done(function (data) {
     });
(send the looser's name to the function inside the object: user)
------------------------------------------
*/


loadList();

function loadList() {
    alert("in load");
    $.post("api/Multiplayer/GetList")
    .done(function (data) {
        alert("data is " + data);
        document.getElementById("main_div").hidden = false;
        var arr = [];
        var tempStr = data.substring(1, data.length - 1);

        if (tempStr != "") {
            arr = tempStr.split(",");
        }

        arr.forEach(function (game) {
            // Add a list item for the product
            $("<option>" + game.substring(1,game.length -1) + "</option>").appendTo("#games");
        });
    });
}

$("#btnJoinGame").click(function () {
    // the game:
    //$("#games").val()

    $.post("api/SingleGame/GenerateMaze", game)
     .done(function (data) {
         alert();
     });
});

$("#btnStart").click(function () {
    alert("in btn");

    var game = {
        Name: $("#name").val()
    , Rows: $("#rows").val()
    , Cols: $("#cols").val()
    };
    $.post("api/SingleGame/SolveMaze", solutionRequest)
    .done(function (solutionData) {
    });
});
