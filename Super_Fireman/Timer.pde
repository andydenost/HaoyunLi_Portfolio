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
  void display() {
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
  void reset(int timetmp,int thatTimetmp){
    time=timetmp;
    thatTime=thatTimetmp;
    timerExist=true;
  }
}