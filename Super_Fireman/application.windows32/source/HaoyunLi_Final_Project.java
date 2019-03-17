import processing.core.*; 
import processing.data.*; 
import processing.event.*; 
import processing.opengl.*; 

import ddf.minim.*; 

import java.util.HashMap; 
import java.util.ArrayList; 
import java.io.File; 
import java.io.BufferedReader; 
import java.io.PrintWriter; 
import java.io.InputStream; 
import java.io.OutputStream; 
import java.io.IOException; 

public class HaoyunLi_Final_Project extends PApplet {

// declare the audio library and the player 
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

public void setup() {
  
  
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

public void draw() {
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

public void startPage() {
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

public void startPage2() {
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

public void showName() {
  textFont(f1);
  fill(255, 0, 0);
  text("SUPER FIREMAN", width/2-300, namePosY);
}

public void game1() {
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

public void game2() {
  car.display();
}

public void game3() {
  //if success go to final page
  //if fail go to game3 again
  println("game3");
  fireBuilding.display();
  water.display();
}

public void finalPage() {
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

public void failPage() {
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

public void keyPressed() {
  if (keyCode==LEFT) {
    leftKey=true;
  }
  if (keyCode==RIGHT) {
    rightKey=true;
  }
}
public void keyReleased() {
  if (keyCode==LEFT) {
    leftKey=false;
  }
  if (keyCode==RIGHT) {
    rightKey=false;
  }
}
//this class for game2, surpass 30 cars and don't be crash.

class Car {
  PImage[] carColor= new PImage[3];
  PImage fireTruck;
  int line;
  int[] carPosX, carPosY;
  int truckPosX, truckPosY;
  int j;
  int col;
  PImage[] pic;
  int z;
  int carNum;
  boolean carCrashed;
  AudioPlayer firetruckSound, carCrashSound;
  Car() {
    imageMode(CENTER);
    carColor[0]=loadImage("redCar.png");
    carColor[1]=loadImage("blackCar.png");
    carColor[2]=loadImage("yellowCar.png");
    fireTruck= loadImage("fireTruck.png");
    truckPosX=width/2;
    truckPosY=700;
    carNum=20;
    pic = new PImage[carNum];
    carPosX=new int[carNum];
    carPosY=new int[carNum];
    j=0;
    z=0;
    firetruckSound=minim1.loadFile("firetruckSound.mp3");
    carCrashSound=minim1.loadFile("carCrashSound.mp3");
  }

  public void display() {
    imageMode(CENTER);
    if(firetruckSound.isPlaying()==false){
    firetruckSound.play();
    firetruckSound.rewind();
    }
    //firetruckSound.rewind();
    z++;
    background(0,185,0);
    fill(125);
    rectMode(CORNER);
    rect(100,0,600,800);
    //image(road,400,400,800,800);
    strokeWeight(8);
    stroke(255,255,0,100);
    line(100,0,100,800);
    line(300,0,300,800);
    line(500,0,500,800);
    line(700,0,700,800);
    if (z%100==0) {
      //println(millis()-t2);
      if (j<carNum) {
        line = (int)random(3);
        switch(line) {
        case 0 : 
          carPosX[j]=200;
          break;
        case 1 : 
          carPosX[j]=400;
          break;
        case 2 : 
          carPosX[j]=600;
          break;
        }
        pic[j]=carColor[PApplet.parseInt(random(3))];
        for (int i=0; i<j+1; i++) {
          image(pic[i], carPosX[i], carPosY[i], 200, 200);
          carPosY[i]=carPosY[i]+5;
        }
        carCrash(j);
        j++;
      } else {
        g2Finished=true;
      }
    } else if (z>100) {
      for (int i=0; i<j; i++) {
        image(pic[i], carPosX[i], carPosY[i], 200, 200);
        carCrash(j);
        carPosY[i]=carPosY[i]+5;
      }
    }

    if (truckPosX>150&&truckPosX<650) {
      truckMove();
    } else if (truckPosX<=150) {
      leftKey=false;
      truckMove();
    } else if (truckPosX>=650) {
      rightKey=false;
      truckMove();
    }
  }

  public void truckMove() {
    if ((leftKey==false&&rightKey==false)||(leftKey==true&&rightKey==true)) {
      image(fireTruck, truckPosX, truckPosY, 200, 200);
    }
    if (leftKey==true&&rightKey==false) {
      truckPosX=truckPosX-10;
      image(fireTruck, truckPosX, truckPosY, 200, 200);
    }
    if (leftKey==false&&rightKey==true) {
      truckPosX=truckPosX+10;
      image(fireTruck, truckPosX, truckPosY, 200, 200);
    }
  }
  
  public void carCrash(int numTmp){
    for(int i=0;i<numTmp;i++){
      if(abs(truckPosX-carPosX[i])<100&&abs(truckPosY-carPosY[i])<200){
        println("crash");
        carCrashed=true;
        carCrashSound.play();
        carCrashSound.rewind();
        firetruckSound.close();
      }
    }
  }
}
class Flame {
  PImage flame;
  int flamePosX, flamePosY, flameSize;
  Flame(int posXtmp, int posYtmp, int flameSizetmp) {
    flamePosX=posXtmp;
    flamePosY=posYtmp;
    flameSize=flameSizetmp;
    flame=loadImage("flame.png");
  }
  public void display(boolean firetmp) {
    if(firetmp==true){
    image(flame, flamePosX, flamePosY, flameSize, flameSize);
    }
  }
}
//this class for game1--select correct gears

class Gear {
  int t1;
  AudioPlayer correctSound, errorSound;
  boolean[][] q = new boolean[8][2];
  boolean answer, defaultAnswer;
  PImage[][] gearImg= new PImage[8][2];
  PImage correctImg, errorImg;
  boolean showEnd;
  Gear() {
    q[0][0]=true;
    q[0][1]=false;
    q[1][0]=false;
    q[1][1]=true;
    q[2][0]=false;
    q[2][1]=true;
    q[3][0]=true;
    q[3][1]=false;
    q[4][0]=true;
    q[4][1]=false;
    q[5][0]=false;
    q[5][1]=true;
    q[6][0]=true;
    q[6][1]=false;
    q[7][0]=false;
    q[7][1]=true;
    t1=0;
    showEnd=true;
    answer=false;
    defaultAnswer=true;
    correctSound = minim1.loadFile("correctSound.wav");
    errorSound = minim1.loadFile("errorSound.wav");
    for (int i=0; i<8; i++) {
      for (int j=0; j<2; j++) {
        gearImg[i][j]=loadImage("gear"+i+j+".png");
      }
    }
    correctImg = loadImage("correct.png");
    errorImg = loadImage("error.png");
  }

  public void display(int index) {//display those gears
    background(255);
    fill(255, 0, 0);
    image(gearImg[index][0], 20, 200, 380, 380);
    image(gearImg[index][1], 420, 200, 380, 380);
  }

  public void judge() {
    println(showEnd);
    if (showEnd==true) {
        
      if (mouseX<width/2&&mousePressed) {
        mousePressed=false;
        showEnd=false;
        t1=millis();
        answer=q[qNum][0];
        defaultAnswer=false;
        soundEffect();
        qNum++;
        if (answer==false) {
          timer1.time=timer1.time-10;
        }
      } else if (mouseX>width/2&&mousePressed) {
        mousePressed=false;
        showEnd=false;
        t1=millis();
        answer=q[qNum][1];
        defaultAnswer=false;
        soundEffect();
        qNum++;
        if (answer==false) {
          timer1.time=timer1.time-10;
        }
      }
      
    } else {
      showResult();
    }
    //qNum++;
  }
  public void showResult() {
    if (defaultAnswer==false) {
      if (millis()-t1<1000) {
        if (answer==true) {
          display(qNum-1);
          image(correctImg, mouseX, mouseY, 200, 200);
        } else {
          display(qNum-1);
          image(errorImg, mouseX, mouseY, 200, 200);
        }
      } else {
        showEnd=true;
      }
    }
    //showEnd=true;
  }

  public void soundEffect() {
    if (answer==false) {
      //
      errorSound.rewind();
      errorSound.play();
      // timer();
      //delay(1000);
    } else {
      correctSound.rewind();
      correctSound.play();
      //  timer();
      // delay(1000);
    }
  }
}
class Man{
  int x,y;
  PImage man;
  Man(){
    man = loadImage("man.png");
  }
  public void display(int xtmp, int ytmp){
    x=xtmp;
    y=ytmp;
    image(man,x,y,150,150);
  }
}
class Timer {
  int time;
  int thatTime;
  int restTime;
  boolean timerExist;
  //int punTime;
  Timer() {
    restTime=9;
    timerExist=false;
  }
  public void display() {
    println("tm"+millis());
    restTime= time-(millis()/1000-thatTime);
    textFont(f3);
    text(restTime+"s", 380, 100);
    println(thatTime);
    println(time);
    //punTime=0;
  }

  /*boolean timeEnd() {
    if (restTime<=0) {
      return true;
    } else {
      return false;
    }
  }*/
  public void reset(int timetmp,int thatTimetmp){
    time=timetmp;
    thatTime=thatTimetmp;
    timerExist=true;
  }
}
class WaterCannon{
  PImage waterCannon;
  WaterCannon(){
    waterCannon = loadImage("waterCannon.png");
  }
  public void display(){
    stroke(0,0,255);
    strokeWeight(30);
    line(mouseX,mouseY,740,750);
    image(waterCannon,700,750,200,200);
  }
}
class FireBuilding {
  Timer timer2;
  AudioPlayer fireBurn, scream, bodyfall;
  PImage fireBuilding;
  int manX, manY;
  int[][] windowX=new int[3][3];
  int[][] windowY=new int[3][3];
  Man mandrop;
  Flame[][] flame = new Flame[3][3];
  int numX, numY;
  int t;
  int flamesize;
  int selPoX, selPoY;
  boolean[][] fire = new boolean[3][3];
  int fireNum;
  float chance;
  float z;
  int t1, t2;
  boolean firstTime, notNothing;
  int padX;
  boolean game3fail;
  int redlow, redhigh;
  boolean newtime;
  FireBuilding() {
    newtime=true;
    redlow=200;
    redhigh=255;
    timer2 = new Timer();
    println("start:"+millis());
    game3fail=false;
    rectMode(CENTER);
    padX=400;
    notNothing=false;
    firstTime=true;
    z=0;
    fireNum=0;
    fireBurn = minim1.loadFile("fireBurn.mp3");
    scream = minim1.loadFile("scream.mp3");
    bodyfall= minim1.loadFile("bodyfall.mp3");
    flamesize=120;
    mandrop = new Man();
    for (int i=0; i<3; i++) {
      for (int j=0; j<3; j++) {
        fire[i][j]=true;
      }
    }
    fireBuilding = loadImage("fireBuilding.png");
    for (int i=0; i<3; i++) {
      for (int j=0; j<3; j++) {
        if (i==0) {
          windowX[i][j]=270;
        }
        if (i==1) {
          windowX[i][j]=400;
        }
        if (i==2) {
          windowX[i][j]=530;
        }
        if (j==0) {
          windowY[i][j]=220;
        }
        if (j==1) {
          windowY[i][j]=385;
        }
        if (j==2) {
          windowY[i][j]=550;
        }
      }
    }
    for (int i=0; i<3; i++) {
      for (int j=0; j<3; j++) {
        flame[i][j]=new Flame(windowX[i][j], windowY[i][j], flamesize);
      }
    }
  }

  public void display() {
    rectMode(CENTER);
    if (newtime==true) {
      timer2.reset(30, millis()/1000);
      newtime=false;
    }
    if (fireBurn.isPlaying()==false) {
      fireBurn.play();
      fireBurn.rewind();
    }
    z++;
    background(random(redlow, redhigh), 150, 0);
    imageMode(CENTER);
    image(fireBuilding, 400, 400, 800, 800);
    //println(mouseX, mouseY);
    if (g3Finished==false) {
      for (int i=0; i<3; i++) {
        for (int j=0; j<3; j++) {
          flame[i][j].display(fire[i][j]);
        }
      }

      if (mousePressed==true) {
        mousePressed=false;
        for (int i=0; i<3; i++) {
          for (int j=0; j<3; j++) {
            if (dist(mouseX, mouseY, flame[i][j].flamePosX, flame[i][j].flamePosY)<50) {
              fire[i][j]=!fire[i][j];
              if (i==0) fire[i+1][j]=!fire[i+1][j];
              if (j==0) fire[i][j+1]=!fire[i][j+1]; 
              if (i==1) {
                fire[i+1][j]=!fire[i+1][j];
                fire[i-1][j]=!fire[i-1][j];
              }
              if (j==1) {
                fire[i][j+1]=!fire[i][j+1]; 
                fire[i][j-1]=!fire[i][j-1];
              }
              if (i==2) fire[i-1][j]=!fire[i-1][j];
              if (j==2) fire[i][j-1]=!fire[i][j-1];
            }
          }
        }
      }
      if (firstTime==true) {
        t1=millis();
        firstTime=false;
      }
      if (millis()-t1>5000) {
        t1=millis();
        for (int i=0; i<3; i++) {
          for (int j=0; j<3; j++) {
            if (fire[i][j]==true) {
              fireNum++;
            }
          }
        }

        println(chance);
      a:
        for (int i=0; i<3; i++) {
          for (int j=0; j<3; j++) {
            if (fire[i][j]==true) {
              chance=random(fireNum);
              if (chance>0&&chance<1) {
                //display
                scream.play();
                scream.rewind();
                notNothing=true;
                manX=flame[i][j].flamePosX;
                manY=flame[i][j].flamePosY;
                println("aaa");
                //mandrop.display(manX, manY);
                break a;
              }
            }
          }
        }
        fireNum=0;
        println(manX, manY);
      }
      if (notNothing==true) {
        mandrop.display(manX, manY);
        manY=manY+10;
      }
      fill(0, 0, 150);
      noStroke();
      if ((leftKey==false&&rightKey==false)||(leftKey==true&&rightKey==true)) {
        rect(padX, 780, 150, 50);
      }
      if (leftKey==true&&rightKey==false) {
        padX=padX-10;
        rect(padX, 780, 150, 50);
      }
      if (leftKey==false&&rightKey==true) {
        padX=padX+10;
        rect(padX, 780, 150, 50);
      }
      if (manX<padX+50&&manX>padX-50&&manY<800&&manY>750) {

        println("Great");
      } else if ((manY>800&&manX>padX+50)||(manY>800&&manX<padX-50)) {
        println("you fail");
        game3fail=true;
        bodyfall.play();
        fireBurn.close();
      }
      if (manY>800) {
        manX=padX;
      }
      if (g3Finished==false) {
        println("1----");
        println(timer2.restTime);
        timer2.display();
        println(timer2.restTime);
        println("2----");
      }
      if (timer2.restTime<=0) {
        println(timer2.restTime);
        game3fail=true;
        println(game3fail);
      }
    }
  b:
    for (int i=0; i<3; i++) {
      for (int j=0; j<3; j++) {
        if (fire[i][j]==true) {
          break b;
        }
        if (i==2&&j==2) {
          g3Finished=true;
          redlow=50;
          redhigh=50;
          fireBurn.close();
        }
      }
    }
  }
}
  public void settings() {  size(800, 800);  smooth(); }
  static public void main(String[] passedArgs) {
    String[] appletArgs = new String[] { "HaoyunLi_Final_Project" };
    if (passedArgs != null) {
      PApplet.main(concat(appletArgs, passedArgs));
    } else {
      PApplet.main(appletArgs);
    }
  }
}
