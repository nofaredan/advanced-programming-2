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

    $.fn.mazeBoard = function (data, callBackOnMove, gif) {
        var array = convertToMazeArr(data.Maze, data.Rows, data.Cols);
        var currentBoard = {
            mazeArray: array,
            rows: data.Rows,
            cols: data.Cols,
            initPos: data.Start,
            goalPos: data.End,
            bob: document.getElementById(gif),
            exit: document.getElementById("exit"),
            currentRow: 0,
            currentCol: 0,
            context: null,
            cellWidth: 0,
            cellHeight: 0,
            gameOn: true
        };
        DrawMazes(this[0], currentBoard);

        if (callBackOnMove != null) {
            document.addEventListener("keydown", callBackOnMove);
        }

        return currentBoard;
    };
})(jQuery);