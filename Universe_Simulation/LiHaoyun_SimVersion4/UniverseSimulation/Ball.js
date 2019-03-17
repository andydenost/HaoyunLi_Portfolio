class Ball {

    constructor(x, y, vx, vy, ax, ay, m, size, img) { //position, velocity, accelerated velocity, mass, size, color
        this.x = x;
        this.y = y;
        this.vx = vx;
        this.vy = vy;
        this.ax = ax;
        this.ay = ay;
        this.m = m;
        this.size = size;
        this.img = img;
        //this.speedLose = 1;
    }

    move() {
        this.vx += this.ax; // speed change
        this.vy += this.ay;
        this.x += this.vx; //position change
        this.y += this.vy;
    }

    applyForce(force) {
        this.ax = force.x / this.m;
        this.ay = force.y / this.m;
        //acceleration.add(f);
    }

    display() {
        //fill(this.color);
        //ellipse(this.x, this.y, this.size, this.size);
        imageMode(CENTER);
        image(this.img, this.x, this.y, this.size, this.size);
    }
    /* collision(ux1, uy1, x1, y1, m1) {
         this.ux1 = ux1;
         this.uy1 = uy1;
         this.m1 = m1;
         this.x1 = x1;
         this.y1 = y1;
         this.ux = this.vx;
         this.uy = this.vy;
         //this.u = sqrt(sq(this.vx) + sq(this.vy));
         this.vx = (this.ux * (this.m - this.m1) + (2 * this.m1 * this.ux1)) / (this.m + this.m1);
         this.vy = (this.uy * (this.m - this.m1) + (2 * this.m1 * this.uy1)) / (this.m + this.m1);
         console.log("ball " + this.vx + " " + this.vy);
     }*/
}
