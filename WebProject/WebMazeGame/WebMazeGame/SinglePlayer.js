var myMazeBoard;
var timer;
var counter = 0;
var animationSolve = false;
const DOWN = 40;
const RIGHT = 39;
const LEFT = 37;
const UP = 38;

$("#btnStartGame").click(function () {
    var game = {
        Name: $("#name").val()
        , Rows: $("#rows").val()
        , Cols: $("#cols").val()
    };
    $.post("api/SingleGame/GenerateMaze", game)
     .done(function (data) {         myMazeBoard = $("#mazeCanvas").mazeBoard(data, movePlayer);
     });
});

(function ($) {

    function DrawMazes(mazeCanvas, currentBoard) {
        currentBoard.context = mazeCanvas.getContext("2d");
        var rows = currentBoard.rows;
        var cols = currentBoard.cols;
        currentBoard.cellWidth = mazeCanvas.width / cols;
        currentBoard.cellHeight = mazeCanvas.height / rows;
        var cellWidth = currentBoard.cellWidth;
        var cellHeight = currentBoard.cellHeight;

        for (var i = 0; i < rows; i++) {
            for (var j = 0; j < cols; j++) {
                if (i == currentBoard.initPos.Row && j == currentBoard.initPos.Col) {
                    currentBoard.context.drawImage(currentBoard.bob, cellWidth * j, cellHeight * i,
                    cellWidth, cellHeight);
                    currentBoard.currentRow = i;
                    currentBoard.currentCol = j;
                }
                else if (i == currentBoard.goalPos.Row && j == currentBoard.goalPos.Col) {
                    currentBoard.context.drawImage(currentBoard.exit, cellWidth * j, cellHeight * i,
                 cellWidth, cellHeight);
                }
                else if (currentBoard.mazeArray[i][j] == 1) {
                    currentBoard.context.fillRect(cellWidth * j, cellHeight * i,
                   cellWidth, cellHeight);
                }
            }
        }

    };

    function convertToMazeArr(strMaze, rows, cols) {
        var maze = new Array();

        var k = 0;
        for (var i = 0; i < rows; i++) {
            maze[i] = new Array();
            for (var j = 0; j < cols; j++) {
                if (strMaze.charAt(k) == '0')
                    maze[i].push(0);
                else if (strMaze.charAt(k) == '1')
                    maze[i].push(1);
                k++;
            }
        }
        return maze;
    }

    $.fn.mazeBoard = function (data, callBackOnMove) {
        var array = convertToMazeArr(data.Maze, data.Rows, data.Cols);
        var currentBoard = {
            mazeArray: array,
            rows: data.Rows,
            cols: data.Cols,
            initPos: data.Start,
            goalPos: data.End,
            bob: document.getElementById("bob"),
            exit: document.getElementById("exit"),
            currentRow: 0,
            currentCol: 0,
            context: null,
            cellWidth: 0,
            cellHeight: 0,
            gameOn: true
        };
        DrawMazes(this[0], currentBoard);

        document.addEventListener("keydown", callBackOnMove);
        return currentBoard;
    };
})(jQuery);


$("#btnSolve").click(function () {
    alert("in btn");
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

