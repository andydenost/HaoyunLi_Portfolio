class Collision {

    constructor() {

    }

    PEC(ball1, ball2) { //perfectly elastic collision

        let initialSpeed1 = new Vector2(ball1.vx, ball1.vy); //each ball intial speed
        let initialSpeed2 = new Vector2(ball2.vx, ball2.vy);

        //orthogonal decomposition
        let s = new Vector2(ball2.x - ball1.x, ball2.y - ball1.y);
        let ss1 = new Vector2(ball2.x - ball1.x, ball2.y - ball1.y);
        let ss2 = new Vector2(ball2.x - ball1.x, ball2.y - ball1.y);

        s.normalize();
        ss1.normalize();
        ss2.normalize();

        let t = new Vector2(ball2.x - ball1.x, ball2.y - ball1.y);
        let tt1 = new Vector2(ball2.x - ball1.x, ball2.y - ball1.y);
        let tt2 = new Vector2(ball2.x - ball1.x, ball2.y - ball1.y);

        t.normalize();
        tt1.normalize();
        tt2.normalize();
        t = t.rotate(Math.PI / 2);
        tt1 = tt1.rotate(Math.PI / 2);
        tt2 = tt2.rotate(Math.PI / 2);

        let initialSpeedSC1 = initialSpeed1.dotProduct(s);
        let initialSpeedSC2 = initialSpeed2.dotProduct(s);
        let initialSpeedTC1 = initialSpeed1.dotProduct(t);
        let initialSpeedTC2 = initialSpeed2.dotProduct(t);

        //momentum conservation formula
        let finalSpeedSC1 = (initialSpeedSC1 * (ball1.m - ball2.m) + (2 * ball2.m * initialSpeedSC2)) / (ball1.m + ball2.m);
        let finalSpeedSC2 = (initialSpeedSC2 * (ball2.m - ball1.m) + (2 * ball1.m * initialSpeedSC1)) / (ball2.m + ball1.m);

        ss1.scale(finalSpeedSC1);
        ss2.scale(finalSpeedSC2);

        tt1.scale(initialSpeedTC1);
        tt2.scale(initialSpeedTC2);


        ss1.add(tt1);
        ss2.add(tt2);

        ball1.vx = ss1.x;
        ball1.vy = ss1.y;
        ball2.vx = ss2.x;
        ball2.vy = ss2.y;

    }
}
