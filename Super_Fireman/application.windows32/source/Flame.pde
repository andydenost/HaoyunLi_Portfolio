class Flame {
  PImage flame;
  int flamePosX, flamePosY, flameSize;
  Flame(int posXtmp, int posYtmp, int flameSizetmp) {
    flamePosX=posXtmp;
    flamePosY=posYtmp;
    flameSize=flameSizetmp;
    flame=loadImage("flame.png");
  }
  void display(boolean firetmp) {
    if(firetmp==true){
    image(flame, flamePosX, flamePosY, flameSize, flameSize);
    }
  }
}