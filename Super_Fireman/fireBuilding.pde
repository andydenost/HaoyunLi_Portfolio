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
  boolean[][] fire = new boolean[3][3];
  int fireNum;
  float chance;
  float z;
  int t1;
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

  void display() {
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