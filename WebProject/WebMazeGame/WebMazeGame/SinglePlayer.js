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
    $("<div id= loader_div </div>").appendTo("#start_load_div");    $("<div class=\"loader\"></div>").appendTo("#loader_div");    $("<h2  class=\"loaderHeader\">Loading game..</h2>").appendTo("#loader_div");

    $.post("api/SingleGame/GenerateMaze", game)
        .done(function (data) {            $("#start_load_div").remove();            myMazeBoard = $("#mazeCanvas").mazeBoard(data, movePlayer, "bob");
        });
});

$("#btnSolve").click(function () {
    var solutionRequest = {
        Name: $("#name").val(),
        SearchAlgo: $("#search_algo_text").val()
    };
    $.post("api/SingleGame/SolveMaze", solutionRequest)
        .done(function (solutionData) {
            document.removeEventListener("keydown", movePlayer);
            animationSolve = true;
            solveMaze(solutionData.Solution);
        });
});

function solveMaze(solution) {
    timer = setInterval(moveSolvePlayer, 150, solution);
    alert("first timer " + timer);
}

function moveSolvePlayer(solution) {
    if (myMazeBoard.gameOn) {
        moveOneStep(numberToKeyDirection(solution, counter));
        counter++;
    }
    else {
        alert("last timerbefore clear " + timer);
        clearInterval(timer);
        counter = 0;
        animationSolve = false;
        alert("last timer " + timer);
    }
}

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

function drawMaze(maze) {
    var myCanvas = document.getElementById("mazeCanvas");
}

function movePlayer(event) {
    moveOneStep(event.keyCode);
}

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

function drawPlayer(oldRow, oldCol) {
    myMazeBoard.context.fillStyle = "#FFFFFF";
    myMazeBoard.context.fillRect(myMazeBoard.cellWidth * oldCol, myMazeBoard.cellHeight * oldRow,
        myMazeBoard.cellWidth, myMazeBoard.cellHeight);

    myMazeBoard.context.drawImage(myMazeBoard.bob, myMazeBoard.cellWidth * myMazeBoard.currentCol,
        myMazeBoard.cellHeight * myMazeBoard.currentRow,
        myMazeBoard.cellWidth, myMazeBoard.cellHeight);
}

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

