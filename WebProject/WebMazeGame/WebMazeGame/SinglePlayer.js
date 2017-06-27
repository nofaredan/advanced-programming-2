// add plugin
var imported = document.createElement('script');
imported.src = 'CanvasPlugin.js';
document.head.appendChild(imported);

var myMazeBoard;
var timer;
var counter = 0;
var animationSolve = false;
var DOWN = 40;
var RIGHT = 39;
var LEFT = 37;
var UP = 38;

$("#btnStartGame").click(function () {
    var game = {
        Name: $("#name").val()
        , Rows: $("#rows").val()
        , Cols: $("#cols").val()
    };

    // show loader
    $("<div id= loader_div </div>").appendTo("#start_load_div");
    $("<div class=\"loader\"></div>").appendTo("#loader_div");
    $("<h2  class=\"loaderHeader\">Loading game..</h2>").appendTo("#loader_div");

    $.post("api/SingleGame/GenerateMaze", game)

        (function (data) {
            $("#start_load_div").remove();
            myMazeBoard = $("#mazeCanvas").mazeBoard(data, movePlayer, "bob");
        }).fail(function (jqXHR, textStatus, err) {
            alert("Error: " + err);
        });;
});

// click on solve button
$("#btnSolve").click(function () {
    var solutionRequest = {
        Name: $("#name").val(),
        SearchAlgo: $("#search_algo_text").val()
    };
    $.post("api/SingleGame/SolveMaze", solutionRequest)
        .done(function (solutionData) {
            // remove the event of click
            document.removeEventListener("keydown", movePlayer);
            animationSolve = true;
            solveMaze(solutionData.Solution);
        }).fail(function (jqXHR, textStatus, err) {
            alert("Error: " + err);
        });;
});

// solve the maze
function solveMaze(solution) {
    timer = setInterval(moveSolvePlayer, 150, solution);
}

// move the player
function moveSolvePlayer(solution) {
    if (myMazeBoard.gameOn) {
        moveOneStep(numberToKeyDirection(solution, counter));
        counter++;
    }
    else {
        clearInterval(timer);
        counter = 0;
        animationSolve = false;
    }
}

// get the key
function numberToKeyDirection(solution, i) {
    switch (solution.charAt(i)) {
        case '0':
            return LEFT;
            break;
        case '1':
            return RIGHT;
            break;
        case '2':
            return UP;
            break;
        case '3':
            return DOWN;
            break;
    }
}

// draw the maze
function drawMaze(maze) {
    var myCanvas = document.getElementById("mazeCanvas");
}

// move the player
function movePlayer(event) {
    moveOneStep(event.keyCode);
}

// move the player one step
function moveOneStep(key) {
    var lastRow = myMazeBoard.currentRow;
    var lastCol = myMazeBoard.currentCol;
    var arrResult = getNewRowAndCol(key, myMazeBoard.currentRow, myMazeBoard.currentCol);
    var newRow = arrResult[0];
    var newCol = arrResult[1];

    // if valid and not a wall:
    if (newRow >= 0 && newRow < myMazeBoard.rows && newCol >= 0 && newCol < myMazeBoard.cols
        && myMazeBoard.mazeArray[newRow][newCol] != 1) {
        myMazeBoard.currentRow = newRow;
        myMazeBoard.currentCol = newCol;

        // draw the player:
        drawPlayer(lastRow, lastCol);
    }
    // if got to end point:
    if (newRow == myMazeBoard.goalPos.Row && newCol == myMazeBoard.goalPos.Col) {
        if (!animationSolve) {
            alert("you win!");
        }

        myMazeBoard.gameOn = false;
        document.removeEventListener("keydown", movePlayer);
    }
}

// draw the player
function drawPlayer(oldRow, oldCol) {
    myMazeBoard.context.fillStyle = "#FFFFFF";
    myMazeBoard.context.fillRect(myMazeBoard.cellWidth * oldCol, myMazeBoard.cellHeight * oldRow,
        myMazeBoard.cellWidth, myMazeBoard.cellHeight);

    myMazeBoard.context.drawImage(myMazeBoard.bob, myMazeBoard.cellWidth * myMazeBoard.currentCol,
        myMazeBoard.cellHeight * myMazeBoard.currentRow,
        myMazeBoard.cellWidth, myMazeBoard.cellHeight);
}

// get new row and column
function getNewRowAndCol(key, currentRowPlace, currentColPlace) {
    var arrResult = new Array(2);
    var newRow = currentRowPlace;
    var newCol = currentColPlace;

    switch (key) {
        case UP:
            newRow -= 1;
            break;
        case DOWN:
            newRow += 1;
            break;
        case LEFT:
            newCol -= 1;
            break;
        case RIGHT:
            newCol += 1;
            break;
    }

    arrResult[0] = newRow;
    arrResult[1] = newCol;
    return arrResult;
}

