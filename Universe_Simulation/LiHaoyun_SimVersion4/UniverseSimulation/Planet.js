class Planet {

    constructor(posX, posY, mass, G, size, img) {
        this.posX = posX;
        this.posY = posY;
        this.mass = mass;
        this.G = G;
        this.size = size;
        this.img = img;
    }

    attract(mover) {
        let force = new Vector2(0, 0);
        let d = dist(this.posX, this.posY, mover.x, mover.y);
        d = d / 5;
        d = constrain(d, 20.0, 50.0);
        if (d < 50) {
            force = new Vector2(this.posX - mover.x, this.posY - mover.y);
            force.normalize();
            let strength = (this.G * this.mass * mover.m) / (d * d); // Calculate gravitional force magnitude
            force.scale(strength);
        }
        return force;
    }

    display() {
        //ellipseMode(CENTER);
        //fill(this.color);
        //ellipse(this.posX, this.posY, this.size, this.size);
        imageMode(CENTER);
        image(this.img, this.posX, this.posY, this.size, this.size);
    }
}
