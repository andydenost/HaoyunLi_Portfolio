//http://vsbattles.wikia.com/wiki/File:Render_sun.png
"use strict"

let collide;
let satellite;
//all planets
let earth;
let moon;
let jupiter;
let mars;
let mercury;
let venus;
let sun;
//spaceJunk using Ball class, not planet, it will be a array.
let spaceJunk;
//data of every planet
let earthPosX;
let earthPosY;
let earthMass;
let earthSize;
let earthG;

let moonPosX;
let moonPosY;
let moonMass;
let moonSize;
let moonG;

let jupiterPosX;
let jupiterPosY;
let jupiterMass;
let jupiterSize;
let jupiterG;

let marsPosX;
let marsPosY;
let marsMass;
let marsSize;
let marsG;

let mercuryPosX;
let mercuryPosY;
let mercuryMass;
let mercurySize;
let mercuryG;

let venusPosX;
let venusPosY;
let venusMass;
let venusSize;
let venusG;

let sunPosX;
let sunPosY;
let sunMass;
let sunSize;
let sunG;

let sumForceJ;
let sumForceS;
let gameOver;
let path;
let pathState;
//let junkNum;
let imgEarth, imgMoon, imgTrash, imgSatellite, imgSpaceship, imgJupiter, imgMars, imgMercury, imgVenus, imgSun; //state every img variable.
let playerSprite;
let zoom;
let zoomStartNum;
let lastMouseX, lastMouseY;
let startDragX, startDragY;
//let endZoomPX, endZoomPY;
//let firstPress;
let fs;
let cnv;
let sketchX, sketchY;
let button1, button2, button3, button4, button5, button6, button7, button8, button9;
let junkAdding;
let bornSateX, bornSateY;
let releaseSateX, releaseSateY;
let viewmove;

function preload() { //load image
    imgEarth = loadImage('assets/earth.png');
    imgMoon = loadImage('assets/moon.png');
    imgTrash = loadImage('assets/trash.png');
    imgSatellite = loadImage('assets/satellite.png');
    imgSpaceship = loadImage('assets/spaceship.png');
    imgJupiter = loadImage('assets/jupiter.png');
    imgMars = loadImage('assets/mars.png');
    imgMercury = loadImage('assets/mercury.png');
    imgVenus = loadImage('assets/venus.png');
    imgSun = loadImage('assets/sun.png');
}

function windowResized() { //make sure the canvas and button would be appear in right place when the browser resize
    centerCanvas();
    button1.position(sketchX + width, sketchY);
    button2.position(sketchX + width, sketchY + 40);
    button3.position(sketchX + width, sketchY + 80);
    button4.position(sketchX + width, sketchY + 120);
    button5.position(sketchX - 100, sketchY);
    button6.position(sketchX - 100, sketchY + 40);
    button7.position(sketchX - 100, sketchY + 80);
    button8.position(sketchX - 100, sketchY + 120);
    button9.position(sketchX - 100, sketchY + 160);
}

function centerCanvas() { //center the canvas
    sketchX = (windowWidth - width) / 2;
    sketchY = (windowHeight - height) / 2;
    cnv.position(sketchX, sketchY);
}

function setup() {
    cnv = createCanvas(720, 720);
    centerCanvas();
    //firstPress = true;
    restart();
    button1 = createButton('Show Path');
    button1.position(sketchX + width, sketchY);
    button1.size(100, 40);
    button1.mousePressed(pathshow);
    button2 = createButton('Ignite');
    button2.position(sketchX + width, sketchY + 40);
    button2.size(100, 40);
    button2.mousePressed(ignite);
    button3 = createButton('Slow down');
    button3.position(sketchX + width, sketchY + 80);
    button3.size(100, 40);
    button3.mousePressed(slow);
    button4 = createButton('Restart');
    button4.position(sketchX + width, sketchY + 120);
    button4.size(100, 40);
    button4.mousePressed(restart);
    button5 = createButton('Add junk');
    button5.position(sketchX - 100, sketchY);
    button5.size(100, 40);
    button5.mousePressed(addJunk);
    button6 = createButton('Reduce junk');
    button6.position(sketchX - 100, sketchY + 40);
    button6.size(100, 40);
    button6.mousePressed(reduceJunk);
    button7 = createButton('Follow Satellite');
    button7.position(sketchX - 100, sketchY + 80);
    button7.size(100, 40);
    button7.mousePressed(followSatellite);
    button8 = createButton('Not Follow Satellite');
    button8.position(sketchX - 100, sketchY + 120);
    button8.size(100, 40);
    button8.mousePressed(notFollowSatellite);
    button9 = createButton('move view');
    button9.position(sketchX - 100, sketchY + 160);
    button9.size(100, 40);
    button9.mousePressed(moveView);
    //console.log("text:" + button6.elt.innerHTML);
    //button6.elt.innerHTML = "haha";   
    //console.log("text:" + button6.elt.innerHTML);
    zoomStartNum = 1;
}

function moveView() {
    viewmove = true;
    junkAdding = false;
}

function followSatellite() {
    fs = true;
}

function notFollowSatellite() {
    fs = false;
}

function addJunk() {
    junkAdding = true;
    viewmove = false;
}

function reduceJunk() {
    if (spaceJunk.length > 0) {
        console.log(spaceJunk.length);
        console.log(spaceJunk);
        spaceJunk.pop(); //pop out the last ball add to the array
    }
}

function draw() {
    console.log((camera.mouseX - camera.position.x) / camera.zoom + camera.position.x);
    console.log((camera.mouseY - camera.position.y) / camera.zoom + camera.position.y);

    if (gameOver == false) {
        background(0, 0, 36);
        //display the planets
        earth.display();
        moon.display();
        jupiter.display();
        mars.display();
        mercury.display();
        venus.display();
        sun.display();

        for (let i = 0; i < spaceJunk.length; i++) {
            sumForceJ[i] = new Vector2(0, 0);
            sumForceJ[i].add(earth.attract(spaceJunk[i]));
            sumForceJ[i].add(moon.attract(spaceJunk[i]));
            sumForceJ[i].add(jupiter.attract(spaceJunk[i]));
            sumForceJ[i].add(mars.attract(spaceJunk[i]));
            sumForceJ[i].add(mercury.attract(spaceJunk[i]));
            sumForceJ[i].add(venus.attract(spaceJunk[i]));
            sumForceJ[i].add(sun.attract(spaceJunk[i]));



        }

        sumForceS = new Vector2(0, 0);
        sumForceS.add(earth.attract(satellite));
        sumForceS.add(moon.attract(satellite));
        sumForceS.add(jupiter.attract(satellite));
        sumForceS.add(mars.attract(satellite));
        sumForceS.add(mercury.attract(satellite));
        sumForceS.add(venus.attract(satellite));
        sumForceS.add(sun.attract(satellite));




        for (let i = 0; i < spaceJunk.length; i++) {
            spaceJunk[i].applyForce(sumForceJ[i]);
        }

        satellite.applyForce(sumForceS)

        for (let i = 0; i < spaceJunk.length; i++) {
            spaceJunk[i].move();
            spaceJunk[i].display();
        }
        //alert(satellite.ax);
        satellite.move();
        satellite.display();
        if (pathState == true) {
            showPath();
        }

        collisionDetect();

        if (dist(satellite.x, satellite.y, earth.posX, earth.posY) < (satellite.size / 2) + (earth.size / 2)) {
            alert("You crash on the earth!");
            gameOver = true;
        } else if (dist(satellite.x, satellite.y, mars.posX, mars.posY) < (mars.size / 2) + (mars.size / 2)) {
            alert("You land on Mars!");
            gameOver = true;
        } else if (dist(satellite.x, satellite.y, jupiter.posX, jupiter.posY) < (satellite.size / 2) + (jupiter.size / 2)) {
            alert("You land on Jupiter!");
            gameOver = true;
        } else if (dist(satellite.x, satellite.y, mercury.posX, mercury.posY) < (satellite.size / 2) + (mercury.size / 2)) {
            alert("You land on Mercury!");
            gameOver = true;
        } else if (dist(satellite.x, satellite.y, venus.posX, venus.posY) < (satellite.size / 2) + (venus.size / 2)) {
            alert("You land on Venus!");
            gameOver = true;
        } else if (dist(satellite.x, satellite.y, sun.posX, sun.posY) < (satellite.size / 2) + (sun.size / 2)) {
            alert("You land on Sun!");
            gameOver = true;
        }
    }
    if (fs == true) {
        camera.position.x = satellite.x;
        camera.position.y = satellite.y;
    }
}

function showPath() {
    let pp = new PathPoint(satellite.x, satellite.y);
    path.push(pp); //store the point for next frame drawing
    stroke(0, 255, 0);
    for (let i = 0; i < path.length; i++) {
        point(path[i].x, path[i].y);
    }
    noStroke();
}

function pathshow() {
    pathState = true;
}


function ignite() {
    let ratio = satellite.vx / satellite.vy;
    satellite.vy *= 1.1;
    satellite.vx = ratio * satellite.vy;
}

function slow() {
    let ratio = satellite.vx / satellite.vy;
    satellite.vy *= 0.9;
    satellite.vx = ratio * satellite.vy;
}

function collisionDetect() { //use loop to calculate every balls distances

    for (let i = 0; i < spaceJunk.length - 1; i++) {
        for (let j = i + 1; j < spaceJunk.length; j++) {
            let d = dist(spaceJunk[i].x, spaceJunk[i].y, spaceJunk[j].x, spaceJunk[j].y);
            if (d < (spaceJunk[i].size / 2) + (spaceJunk[j].size / 2)) {
                collide.PEC(spaceJunk[i], spaceJunk[j]);
            }
        }
    }
    for (let i = 0; i < spaceJunk.length; i++) {
        let d = dist(satellite.x, satellite.y, spaceJunk[i].x, spaceJunk[i].y);
        if (d < (spaceJunk[i].size / 2) + (satellite.size / 2)) {
            collide.PEC(satellite, spaceJunk[i]);
        }
    }
}

function restart() {
    camera.position.x = width / 2;
    camera.position.y = height / 2;
    junkAdding = false;
    viewmove = false;
    fs = false;
    //junkNum = 3;
    pathState = false;
    path = [];
    gameOver = false;
    spaceJunk = [];
    earthPosX = 200;
    earthPosY = height / 2 + 100;
    earthMass = 20;
    earthSize = 80;
    earthG = 9.8

    moonPosX = 500;
    moonPosY = 220;
    moonMass = 20;
    moonSize = 40;
    moonG = 9.8;

    jupiterPosX = 200;
    jupiterPosY = height / 2 - 400;
    jupiterMass = 40;
    jupiterSize = 150;
    jupiterG = 9.8;

    marsPosX = 600;
    marsPosY = -280;
    marsMass = 20;
    marsSize = 80;
    marsG = 9.8;

    mercuryPosX = 900;
    mercuryPosY = -650;
    mercuryMass = 20;
    mercurySize = 60;
    mercuryG = 9.8;

    venusPosX = 640;
    venusPosY = -900;
    venusMass = 20;
    venusSize = 60;
    venusG = 9.8;

    sunPosX = -100;
    sunPosY = -1500;
    sunMass = 200;
    sunSize = 500;
    sunG = 20;

    sumForceJ = [];

    earth = new Planet(earthPosX, earthPosY, earthMass, earthG, earthSize, imgEarth);
    moon = new Planet(moonPosX, moonPosY, moonMass, moonG, moonSize, imgMoon);
    jupiter = new Planet(jupiterPosX, jupiterPosY, jupiterMass, jupiterG, jupiterSize, imgJupiter);
    mars = new Planet(marsPosX, marsPosY, marsMass, marsG, marsSize, imgMars);
    mercury = new Planet(mercuryPosX, mercuryPosY, mercuryMass, mercuryG, mercurySize, imgMercury);
    venus = new Planet(venusPosX, venusPosY, venusMass, venusG, venusSize, imgVenus);
    sun = new Planet(sunPosX, sunPosY, sunMass, sunG, sunSize, imgSun);

    satellite = new Ball(earthPosX - 100, earthPosY, 0, sqrt((earthG * earthMass / sq(20)) * 100), 0, 0, 2, 30, imgSpaceship);
    spaceJunk[0] = new Ball(earthPosX - 130, earthPosY, 0, sqrt((earthG * earthMass / sq(26)) * 130), 0, 0, 2, 20, imgTrash);
    spaceJunk[1] = new Ball(earthPosX, earthPosY + 160, sqrt((earthG * earthMass / sq(32)) * 160), 0, 0, 0, 3, 20, imgTrash);
    spaceJunk[2] = new Ball(earthPosX + 190, earthPosY, 0, -sqrt((earthG * earthMass / sq(38)) * 190), 0, 0, 1, 20, imgTrash);
    collide = new Collision();
}


function mousePressed() {
    //if button add junk is pressed
    if (junkAdding == true && mouseX > 0 && mouseX < width && mouseY > 0 && mouseY < height) {
        bornSateX = (camera.mouseX - camera.position.x) / camera.zoom + camera.position.x;
        bornSateY = (camera.mouseY - camera.position.y) / camera.zoom + camera.position.y;
        spaceJunk.push(new Ball(bornSateX, bornSateY, 0, 0, 0, 0, 2, 20, imgTrash)); //push a new ball in the array
    }
    //if button move view is pressed
    if (viewmove == true) {
        startDragX = camera.position.x;
        startDragY = camera.position.y;
        lastMouseX = mouseX;
        lastMouseY = mouseY;
    }
}

function mouseReleased() {
    //firstPress = true;
    if (junkAdding == true && mouseX > 0 && mouseX < width && mouseY > 0 && mouseY < height) {
        //console.log("cao");
        releaseSateX = (camera.mouseX - camera.position.x) / camera.zoom + camera.position.x;
        releaseSateY = (camera.mouseY - camera.position.y) / camera.zoom + camera.position.y;
        let vSateX = releaseSateX - bornSateX;
        let vSateY = releaseSateY - bornSateY;
        if (vSateX >= 0) {
            vSateX = map(vSateX, 0, 200, 0, 10); //change distance to speed
        } else {
            vSateX = map(vSateX, 0, -200, 0, -10);
        }
        if (vSateY >= 0) {
            vSateY = map(vSateY, 0, 200, 0, 10);
        } else {
            vSateY = map(vSateY, 0, -200, 0, -10);
        }
        spaceJunk[spaceJunk.length - 1].vx = vSateX;
        spaceJunk[spaceJunk.length - 1].vy = vSateY;
    }
}

function mouseDragged() { //control the drag
    if (viewmove == true) {
        camera.position.x = startDragX - (mouseX - lastMouseX) / camera.zoom; //translate to real position
        camera.position.y = startDragY - (mouseY - lastMouseY) / camera.zoom;
    }
}

function mouseWheel(event) { //zoom the canvas
    if (event.delta > 0) {
        if (camera.zoom < 5) {
            console.log("camera.mouseX:" + camera.mouseX);
            console.log("camera.mouseY:" + camera.mouseY);
            console.log("camera.position.x:" + camera.position.x);
            console.log("camera.position.y:" + camera.position.y);
            console.log("camera.position.z:" + camera.position.z);
            console.log("zoom:" + camera.zoom);
            camera.zoom = camera.zoom + 0.1;
            console.log(zoomStartNum);
        }
    }
    if (event.delta < 0) {
        if (camera.zoom > 0.2) {
            console.log("camera.mouseX:" + camera.mouseX);
            console.log("camera.mouseY:" + camera.mouseY);
            console.log("camera.position.x:" + camera.position.x);
            console.log("camera.position.y:" + camera.position.y);
            console.log("zoom:" + camera.zoom);
            camera.zoom = camera.zoom - 0.1;
        }
    }
}
