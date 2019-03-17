class WaterCannon{
  PImage waterCannon;
  WaterCannon(){
    waterCannon = loadImage("waterCannon.png");
  }
  void display(){
    stroke(0,0,255);
    strokeWeight(30);
    line(mouseX,mouseY,740,750);
    image(waterCannon,700,750,200,200);
  }
}