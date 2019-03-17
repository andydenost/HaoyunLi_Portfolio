class Man{
  int x,y;
  PImage man;
  Man(){
    man = loadImage("man.png");
  }
  void display(int xtmp, int ytmp){
    x=xtmp;
    y=ytmp;
    image(man,x,y,150,150);
  }
}