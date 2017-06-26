var messagesHub;
var dataMaze;
var gameName;
var MultiMazeBoard;
var OpponentMazeBoard;
var DOWN_CODE = 40;
var RIGHT_CODE = 39;
var LEFT_CODE = 37;
var UP_CODE = 38;

// add plugin
var imported = document.createElement('script');
imported.src = 'CanvasPlugin.js';
document.head.appendChild(imported);

loadList();

function loadList() {

    $("<div id= loader_div </div>").appendTo("#start_load_div");    $("<div class=\"loader\"></div>").appendTo("#loader_div");    $("<h2  class=\"loaderHeader\">Loading games..</h2>").appendTo("#loader_div");

    $.post("api/Multiplayer/GetList")
    .done(function (data) {
        document.getElementById("main_div").hidden = false;
        $("#start_load_div").remove();

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

    var gameInfo = {
        Name: $("#games").val()
    };

    gameName = gameInfo.Name;
    //send to server the maze
    $.post("api/Multiplayer/Join", gameInfo)
        .done(function (data) {            // create connection
            messagesHub = $.connection.multiHub;

            // on got message
            messagesHub.client.gotMessage = onGotMessage;

            // starting connection
            $.connection.hub.start().done(function () {

                dataMaze = data;

                // start connection
                messagesHub.server.connect(gameInfo.Name);
            });        });
});

function onGotMessage(keyMove) {
    // start game
    if (keyMove == "start") {
        // delete loader 
        $("#loader_div").remove();

        // init boards
        MultiMazeBoard = $("#mazeCanvas_left").mazeBoard(dataMaze, movePlayer,"bob");

        OpponentMazeBoard = $("#mazeCanvas_right").mazeBoard(dataMaze, null,"purple");
    }
    else {
        // move opponent
        moveOpponentPlayer(keyMove);
    }
    
}

$("#btnStart").click(function () {
    var gameInfo = {
        Name: $("#name").val()
        , Rows: $("#rows").val()
        , Cols: $("#cols").val()
    };

    gameName = gameInfo.Name;

    // create connection
    messagesHub = $.connection.multiHub;

    // on got message
    messagesHub.client.gotMessage = onGotMessage;

    // starting connection
    $.connection.hub.start().done(function () {
        // start connection
        messagesHub.server.connect(gameInfo.Name);
    });

    //send to server the maze
    $.post("api/Multiplayer/Start", gameInfo)
        .done(function (data) {            // add attribute for waiting to partner            $("<div id= loader_div </div>").appendTo("#main_div");              $("<div class=\"loader\"></div>").appendTo("#loader_div");              $("<h2  class=\"loaderHeader\">Waiting for partner</h2>").appendTo("#loader_div");             //MultiMazeBoard = $("#mazeCanvas_left").mazeBoard(data, movePlayer);
            dataMaze = data;
        });
});


function moveOpponentPlayer(keyCode) {
    moveOneStep(parseInt(keyCode), OpponentMazeBoard);
}

function movePlayer(event) {
    moveOneStep(event.keyCode, MultiMazeBoard);
    messagesHub.server.sendMessage(gameName, event.keyCode);
}

function moveOneStep(key,board) {
    var lastRow = board.currentRow;
    var lastCol = board.currentCol;

    var arrResult = getNewRowAndCol(key, board.currentRow, board.currentCol);
    var newRow = arrResult[0];
    var newCol = arrResult[1];

    // if valid and not a wall:
    if (newRow >= 0 && newRow < board.rows && newCol >= 0 && newCol < board.cols
        && board.mazeArray[newRow][newCol] != 1) {
        board.currentRow = newRow;
        board.currentCol = newCol;

        // draw the player:
        drawPlayer(lastRow, lastCol, board);
    }
    // if got to end point:
    if (newRow == board.goalPos.Row && newCol == board.goalPos.Col) {
        if (board !== OpponentMazeBoard) {
            alert("you win!");
            updateWinOrLose(true);
        }
        else {
            alert("you lost!");
            updateWinOrLose(false);
        }

        board.gameOn = false;
        document.removeEventListener("keydown", movePlayer);
    }
}

function updateWinOrLose(isWon) {
    document.removeEventListener("keydown", movePlayer);
    alert("session " + sessionStorage.getItem("user"));
    alert("localStorage.userName " + localStorage.userName);
    var user = {
        Name: sessionStorage.getItem("user")
    };

    var strPath;
    if (isWon) {
        
        strPath = "api/Users/UpdateWinner";
    } else {
        strPath = "api/Users/UpdateLooser";
    }
   
    $.post(strPath, user)
        .done(function (data) {
            alert("done!");
        });
}

function drawPlayer(oldRow, oldCol, board) {
    board.context.fillStyle = "#FFFFFF";
    board.context.fillRect(board.cellWidth * oldCol, board.cellHeight * oldRow,
        board.cellWidth, board.cellHeight);

    board.context.drawImage(board.bob, board.cellWidth * board.currentCol,
        board.cellHeight * board.currentRow,
        board.cellWidth, board.cellHeight);
}

function getNewRowAndCol(key, currentRowPlace, currentColPlace) {
    var arrResult = new Array(2);
    var newRow = currentRowPlace;
    var newCol = currentColPlace;

    switch (key) {
        case UP_CODE:
            newRow -= 1;
            break;
        case DOWN_CODE:
            newRow += 1;
            break;
        case LEFT_CODE:
            newCol -= 1;
            break;
        case RIGHT_CODE:
            newCol += 1;
            break;
    }

    arrResult[0] = newRow;
    arrResult[1] = newCol;
    return arrResult;
}
