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

  void display(int index) {//display those gears
    background(255);
    fill(255, 0, 0);
    image(gearImg[index][0], 20, 200, 380, 380);
    image(gearImg[index][1], 420, 200, 380, 380);
  }

  void judge() {
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
  void showResult() {
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

  void soundEffect() {
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