//this class for game2, surpass 30 cars and don't be crash.

class Car {
  PImage[] carColor= new PImage[3];//three colors of cars image
  PImage fireTruck;
  int line;
  int[] carPosX, carPosY;//car position
  int truckPosX, truckPosY;//fire truck postion
  int j;
  int col;
  PImage[] pic;
  int z;
  int carNum;
  boolean carCrashed;
  AudioPlayer firetruckSound, carCrashSound;
  Car() { //constructor used to initial the attribute of cars and firetruck
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

  void display() {
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
        pic[j]=carColor[int(random(3))]; //random car colors
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

  void truckMove() {
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
  
  void carCrash(int numTmp){
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