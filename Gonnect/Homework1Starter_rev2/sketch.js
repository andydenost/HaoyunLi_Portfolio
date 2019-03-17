"use strict"

let grid; //store spot
let stateGrid; //store color states
let turn; // black turn or white turn
let openList; // A* openlist
let closedList; //A* closedlist
let winDirection; //top to bottom or left to right
let start; //the start point of A*
let stoneColor;
let gameOver; // store the state of end or not
let group; //the group of same color stones
let boardStorage1; //store the past board
let boardStorage2;

function setup() {
    //initialize all variable
    gameOver = false;
    openList = [];
    closedList = [];
    grid = [];
    stateGrid = [];
    boardStorage1 = [];
    boardStorage2 = [];
    for (let i = 0; i < 6; i++) {
        boardStorage1[i] = []
        for (let j = 0; j < 6; j++) {
            boardStorage1[i][j] = []
        }
    }
    for (let i = 0; i < 6; i++) {
        boardStorage2[i] = []
        for (let j = 0; j < 6; j++) {
            boardStorage2[i][j] = []
        }
    }
    turn = "black"; //always black play first.
    for (let x = 0; x < 6; x++) {
        stateGrid[x] = [];
        for (let y = 0; y < 6; y++) {
            stateGrid[x][y] = "empty";
        }
    }
    createCanvas(600, 600);
    clearAndDrawBoard();
    drawText();
}

function removeFromArray(arr, elt) {
    for (let i = arr.length - 1; i >= 0; i--) {
        if (arr[i] == elt) {
            arr.splice(i, 1);
        }
    }
}

function Spot(i, j) {
    this.i = i;
    this.j = j;
    this.f = 0;
    this.g = 0;
    this.h = 0;
    this.neighbors = [];

    this.addNeighbors = function (grid) {

        let i = this.i;
        let j = this.j;
        if (i < 5 && stateGrid[i + 1][j] == stoneColor) {
            this.neighbors.push(grid[i + 1][j]);
        }
        if (i > 0 && stateGrid[i - 1][j] == stoneColor) {
            this.neighbors.push(grid[i - 1][j]);
        }
        if (j < 5 && stateGrid[i][j + 1] == stoneColor) {
            this.neighbors.push(grid[i][j + 1]);
        }
        if (j > 0 && stateGrid[i][j - 1] == stoneColor) {
            this.neighbors.push(grid[i][j - 1]);
        }
    }
}

function heuristic(a) { //calculate distance to destination
    let d;
    if (winDirection == "vertical") {
        d = dist(a.i, a.j, a.i, 5);
    } else if (winDirection == "horizon") {
        d = dist(a.i, a.j, 5, a.j);
    }
    return d;
}

function AStar() { //Algorithm from https://www.youtube.com/watch?v=aKYlikFAV4k&feature=youtu.be
    do {
        let winner = 0;
        for (let i = 0; i < openList.length; i++) {
            if (openList[i].f < openList[winner].f) {
                winner = i;
            }
        }

        let current = openList[winner];
        if (heuristic(current) == 0) {
            return true;
        }

        removeFromArray(openList, current);
        closedList.push(current);

        let neighbors = current.neighbors;
        for (let i = 0; i < neighbors.length; i++) {
            let neighbor = neighbors[i];
            if (!closedList.includes(neighbor)) {
                let tempG = current.g + 1;
                if (openList.includes(neighbor)) {
                    if (tempG < neighbor.g) {
                        neighbor.g = tempG;
                    }
                } else {
                    neighbor.g = tempG;
                    openList.push(neighbor);
                }
                neighbor.h = heuristic(neighbor);
                neighbor.f = neighbor.g + neighbor.h;
            }
        }
    } while (openList.length > 0)
    return false;
}



function draw() {
    // TIP: you can probably leave the draw function empty for this assignment!
}

function winCondition(stonei) {
    for (let i = 0; i < 6; i++) {
        grid[i] = [];
        for (let j = 0; j < 6; j++) {
            grid[i][j] = new Spot(i, j);
        }
    }
    for (let i = 0; i < 6; i++) {
        for (let j = 0; j < 6; j++) {
            grid[i][j].addNeighbors(grid);
        }
    }
    openList = [];
    closedList = [];
    if (winDirection == "vertical") {
        start = grid[stonei][0];
    } else if (winDirection == "horizon") {
        start = grid[0][stonei];
    }
    openList.push(start);
    let b = AStar();
    if (b == true) {
        gameOver = true;
        alert(stoneColor + " win! Game over!");
    }
}

function findGroup(i, j) {
    //Left  
    if (i - 1 >= 0 && stateGrid[i - 1][j] == stateGrid[i][j] && !group.includes((i - 1) * 10 + j)) {
        group.push((i - 1) * 10 + j);
        findGroup(i - 1, j);
    }
    //Up  
    if (j - 1 >= 0 && stateGrid[i][j - 1] == stateGrid[i][j] && !group.includes(i * 10 + j - 1)) {
        group.push(i * 10 + j - 1);
        findGroup(i, j - 1);
    }
    //Right  
    if (i + 1 < 6 && stateGrid[i + 1][j] == stateGrid[i][j] && !group.includes((i + 1) * 10 + j)) {
        group.push((i + 1) * 10 + j);
        findGroup(i + 1, j);
    }
    //Down  
    if (j + 1 < 6 && stateGrid[i][j + 1] == stateGrid[i][j] && !group.includes(i * 10 + j + 1)) {
        group.push(i * 10 + j + 1);
        findGroup(i, j + 1);
    }
}

function haveLiberties() {
    let i, j;
    for (let t = 0; t < group.length; t++) {
        i = Math.floor(group[t] / 10);
        j = group[t] % 10;
        if (i - 1 >= 0 && stateGrid[i - 1][j] == "empty" || i + 1 < 6 && stateGrid[i + 1][j] == "empty" || j - 1 >= 0 && stateGrid[i][j - 1] == "empty" || j + 1 < 6 && stateGrid[i][j + 1] == "empty") {
            return true;
        }
    }
    return false;
}



function mouseClicked() {
    if (gameOver == false) {
        let x = findNearestIndex(mouseX);
        let y = findNearestIndex(mouseY);
        if (stateGrid[x][y] == "empty") {
            drawStone(x, y);
            let jump2 = false;
            let legal1 = false;
            let legal2 = false;
            for (let i = 0; i < 6; i++) {
                for (let j = 0; j < 6; j++) {
                    if (stateGrid[i][j] != "empty") {
                        group = [];
                        group[0] = i * 10 + j;
                        findGroup(i, j);
                        if (haveLiberties() == false) {
                            if (stateGrid[i][j] != stateGrid[x][y]) {
                                legal1 = true;
                                for (let t = 0; t < group.length; t++) {
                                    stateGrid[Math.floor(group[t] / 10)][group[t] % 10] = "empty";
                                }
                            }
                            if (stateGrid[i][j] == stateGrid[x][y]) {
                                legal2 = true;
                            }
                        }
                    }
                }
            }
            if (legal1 == false && legal2 == true) {
                stateGrid[x][y] = "empty";
                if (turn == "black") {
                    turn = "white";
                } else {
                    turn = "black";
                }
                alert("illegal move! 1");
                jump2 = true;
            }
            //ko
            if (jump2 == false) {
                if (checkBoard()) {
                    if (turn == "black") {
                        turn = "white";
                    } else {
                        turn = "black";
                    }
                    for (let i = 0; i < 6; i++) {
                        for (let j = 0; j < 6; j++) {
                            stateGrid[i][j] = boardStorage2[i][j];
                        }
                    }
                    alert("illegal move! 2");
                } else {
                    for (let i = 0; i < 6; i++) {
                        for (let j = 0; j < 6; j++) {
                            boardStorage1[i][j] = boardStorage2[i][j];
                        }
                    }
                    for (let i = 0; i < 6; i++) {
                        for (let j = 0; j < 6; j++) {
                            boardStorage2[i][j] = stateGrid[i][j];
                        }
                    }
                }
            }

            clearAndDrawBoard();
            redrawPrestones();
            drawText();
            //win condition
            winDirection = "vertical"
            for (let i = 0; i < 6; i++) {
                if (stateGrid[i][0] == "black") {
                    stoneColor = "black";
                    winCondition(i);
                } else if (stateGrid[i][0] == "white") {
                    stoneColor = "white";
                    winCondition(i);
                }
            }
            winDirection = "horizon"
            for (let i = 0; i < 6; i++) {
                if (stateGrid[0][i] == "black") {
                    stoneColor = "black";
                    winCondition(i);
                } else if (stateGrid[0][i] == "white") {
                    stoneColor = "white";
                    winCondition(i);
                }
            }
            //
            let legal3 = false;
            let legal4 = false;
            let count1 = 0;
            let count2 = 0;
            for (let i = 0; i < 6; i++) {
                for (let j = 0; j < 6; j++) {
                    if (stateGrid[i][j] == "empty") {
                        count1++;
                        stateGrid[i][j] = turn;
                        if (checkBoard()) {
                            count2++;
                        }
                        for (let a = 0; a < 6; a++) {
                            for (let b = 0; b < 6; b++) {
                                if (stateGrid[a][b] != "empty") {
                                    group = [];
                                    group[0] = a * 10 + b;
                                    findGroup(a, b);
                                    if (haveLiberties() == false) {
                                        if (stateGrid[i][j] != stateGrid[a][b]) {
                                            legal3 = true;
                                        }
                                        if (stateGrid[i][j] == stateGrid[a][b]) {
                                            legal4 = true;
                                        }
                                    }
                                }
                            }
                        }
                        if (legal3 == false && legal4 == true) {
                            count2++;
                        }
                        stateGrid[i][j] = "empty";
                        legal3 = false;
                        legal4 = false;
                    }
                }
            }
            if (count2 == count1) {
                if (turn == "black") {
                    alert("Black no legal move! White win! game over!");
                } else {
                    alert("White no legal move! black win! game over!");
                }
                gameOver = true;
            }
        }

    }
}


function checkBoard() {
    for (let i = 0; i < 6; i++) {
        for (let j = 0; j < 6; j++) {
            if (boardStorage1[i][j] != stateGrid[i][j]) {
                return false;
            }
        }
    }
    return true;
}

function redrawPrestones() {
    for (let i = 0; i < 6; i++) {
        for (let j = 0; j < 6; j++) {
            if (stateGrid[i][j] == "black") {
                stroke(0);
                fill(0);
                ellipse((i + .5) * 100, (j + .5) * 100, 50, 50);
            } else if (stateGrid[i][j] == "white") {
                stroke(255);
                fill(255);
                ellipse((i + .5) * 100, (j + .5) * 100, 50, 50);
            }
        }
    }
}

function findNearestIndex(pt) {
    let nearest = Math.round(pt / 100 - 0.5);
    return nearest;
}

function drawStone(x, y) {
    if (x >= 0 && x < 6 && y >= 0 && y < 6) {
        //if (stateGrid[x][y] == "empty") {
        if (turn == "black") {
            stroke(0);
            fill(0);
            ellipse((x + .5) * 100, (y + .5) * 100, 50, 50);
            stateGrid[x][y] = "black";
            turn = "white";
        } else {
            stroke(255);
            fill(255);
            ellipse((x + .5) * 100, (y + .5) * 100, 50, 50);
            stateGrid[x][y] = "white";
            turn = "black";
        }
        //}
    }
}


function clearAndDrawBoard() {
    background(127, 127, 0);
    stroke(0);
    for (let i = 0; i < 6; i++) {
        line((i + .5) * 100, 50, (i + .5) * 100, 550);
        line(50, (i + .5) * 100, 550, (i + .5) * 100);
    }
}

function keyPressed() {
    if (keyCode == 32) {
        gameOver = false;
        openList = [];
        closedList = [];
        grid = [];
        stateGrid = [];
        turn = "black"; //always black play first.
        for (let x = 0; x < 6; x++) {
            stateGrid[x] = [];
            for (let y = 0; y < 6; y++) {
                stateGrid[x][y] = "empty";
            }
        }
        clearAndDrawBoard();
        drawText();
    }
}

function drawText() {
    textSize(18);
    if (turn == "black") {
        fill(0);
        stroke(0);
        text("Black's turn. Click a grid intersection.  Or press SPACE to restart!", 40, 30);
    } else {
        fill(255);
        stroke(255);
        text("White's turn. Click a grid intersection.  Or press SPACE to restart!", 40, 30);
    }
}
