//game1: select correct gears
//game2: surpass the car and avoid to be crash
//game3: put out the fire and save men.
import ddf.minim.*;// declare the audio library and the player 
Minim minim1;
AudioPlayer song1; //start page bgm
Gear gear; //declare gear class
Timer timer1; //declare one timer
Car car; //declare class of car
FireBuilding fireBuilding;
WaterCannon water;
//int restTime; //rest time
PFont f1, f2, f3;//declare some text fonts
int namePosY; //game's name
int alphaStart;  //transparancy
int butStaX, butStaY, butWidth, butHeight; //button position
int qNum;
boolean g1Finished, g2Finished, g3Finished, wgFinished; //whether each section are finished
boolean gameStart;
boolean gameFail;
//boolean timerExist;
boolean leftKey, rightKey;
int t2;
boolean startP2;
PImage superman1, superman2;

void setup() {
  size(800, 800);
  smooth();
  timer1=new Timer();
  //restTime=9;
  // timerExist=false;
  butStaX=300;
  butStaY=550;
  butWidth=200;
  butHeight=50;
  f1=createFont("Algerian", 80, true);
  f2=createFont("CenturyGothic-Bold", 60, true);//AgencyFB-Bold-48
  f3=createFont("TimesNewRomanPSMT", 40, true);
  namePosY=0;
  alphaStart=0;
  minim1 = new Minim(this);
  song1 = minim1.loadFile("superman_theme.mp3");
  qNum=0;
  gear = new Gear();
  car = new Car();
  water = new WaterCannon();
  fireBuilding=new FireBuilding();
  gameStart=false;
  startP2=false;
  g1Finished=false;
  g2Finished=false;
  g3Finished=false;
  imageMode(CORNER);
  superman1=loadImage("superman1.jpg");
  superman2=loadImage("Superman.jpg");
}

void draw() {
  imageMode(CORNER);
  if (timer1.restTime<=0||car.carCrashed==true||fireBuilding.game3fail==true) { //if game fail go to fail page
    failPage();
  } else if (gameStart==false) { 
    startPage();
  } else if (startP2==true) {
    startPage2();
  } else if (g1Finished==false) { //start page finished go to game1
    // println(timer1.restTime);
    game1();
  } else if (g1Finished==true&&g2Finished==false) {//game1 finished go to game2
    game2();
  } else if (g2Finished==true&&g3Finished==false) {//game2 finished go to game3
    game3();
  } else if (g3Finished==true) { //game3 finished go to final page
    finalPage();
  }
}

void startPage() {
 // song1.rewind();
  song1.play(); //play background music
  if (namePosY<height/2-50) {
    background(0, 0, 155);
    tint(255, 120);
    image(superman2, 0, 150, 800, 500);
    tint(255, 255);
    showName();
    namePosY=namePosY+2;
  } else if (alphaStart<255) {
    background(0, 0, 155);
    tint(255, 120);
    image(superman2, 0, 150, 800, 500);
    tint(255, 255);
    showName();
    fill(255, 0, 0, alphaStart);
    textFont(f2);
    text("START", width/2-100, 600);
    alphaStart=alphaStart+2;
  } else if (mouseX>butStaX&&mouseX<butStaX+butWidth&&mouseY>butStaY&&mouseY<butStaY+butHeight&&mousePressed==true) {
    gameStart=true;   
    startP2=true;
    mousePressed=false;
  }
}

void startPage2() {
  background(0);
  tint(255, 100);
  image(superman1, 0, 150, 800, 500);
  tint(255, 255);
  //fill(100,100,100,80);
  //rectMode(CORNER);
  //rect(0,0,800,800);
  fill(255);
  textFont(f3);
  text("Task1: Select correct gears for fireman", 100, 200);
  text("Task2: Surpass the cars in front of you", 100, 300);
  text("and don't get crash", 200, 400);
  text("Task3: Put out all fires in time", 100, 500);
  text("and don't let any people died", 200, 600);
  if (mousePressed==true) {
    startP2=false;
    mousePressed=false;
    song1.close();
  }
}

void showName() {
  textFont(f1);
  fill(255, 0, 0);
  text("SUPER FIREMAN", width/2-300, namePosY);
}

void game1() {
  //if success go to game2
  //if fail go to game1 again
  //println(timer1.restTime);
  //println("game1");
  //timerExist=false;
  //println(timerExist);
  //timerExist=false;
  if (timer1.timerExist==true) {
    //println(timer1.restTime);
    if (qNum<8) {
      gear.display(qNum);
      gear.judge();
      //println(timer1.restTime);
      timer1.display();
      //println(timer1.restTime);
    } else {
      g1Finished=true;
      //println(timer1.restTime);
    }
  } else {
    timer1.reset(30, millis()/1000);
    //println(timer1.restTime);
  }
}

void game2() {
  car.display();
}

void game3() {
  //if success go to final page
  //if fail go to game3 again
  println("game3");
  fireBuilding.display();
  water.display();
}

void finalPage() {
  fireBuilding.display();
  println("win");
  textFont(f1);
  fill(255, 0, 0);
  text("You Saved Them!", width/2-300, 300);
  textFont(f3);
  fill(255);
  text("Press space to start from beginning", width/2-300, 700);
  if (keyPressed==true&&key==' ') {
    qNum=0;
    timer1.restTime=9;
    gameStart=true;
    startP2=true;
    g1Finished=false;
    g2Finished=false;
    g3Finished=false;
    car = new Car();
    timer1=new Timer();
    gear = new Gear();
    fireBuilding = new FireBuilding(); 
    water = new WaterCannon();
    fireBuilding.newtime=true;
  }
}

void failPage() {
  gameFail=true;
  background(0, 0, 255);  
  fill(255, 0, 0);
  textFont(f2);
  text("You failed...", 300, 300);
  text("restart this section", 200, 500);
  //println(restTime);
  if (mousePressed==true) {
    println("useful");
    gameFail=false;
    gameStart=true;
    if (g2Finished==true) {
      fireBuilding = new FireBuilding();
      fireBuilding.newtime=true;
    } else if (g1Finished==true) {
      car = new Car();
    } else {
      g1Finished=false;
      timer1.reset(30, millis()/1000);
      qNum=0;
      timer1.restTime=9;
    }
    mousePressed=false;
  }
}

void keyPressed() {
  if (keyCode==LEFT) {
    leftKey=true;
  }
  if (keyCode==RIGHT) {
    rightKey=true;
  }
}
void keyReleased() {
  if (keyCode==LEFT) {
    leftKey=false;
  }
  if (keyCode==RIGHT) {
    rightKey=false;
  }
}