//for calculate collision motion I make a class of Vector
class Vector2 {
    constructor(x, y) {
        this.x = x;
        this.y = y;
    }

    add(vec) {
        this.x += vec.x;
        this.y += vec.y;
        return this;
    }

    sub(vec) {
        this.x -= vec.x;
        this.y -= vec.y;
        return this;
    }
    negate() {
        this.x = -this.x;
        this.y = -this.y;
        return this;
    }
    length() {
        return Math.sqrt(this.x * this.x + this.y * this.y);
    }
    normalize() {
        var len = Math.sqrt(this.x * this.x + this.y * this.y);
        if (len) {
            this.x /= len;
            this.y /= len;
        }
    }
    rotate(radian) {
        let xx = this.x;
        let yy = this.y;
        let cosVal = Math.cos(radian);
        let sinVal = Math.sin(radian);;
        this.x = xx * cosVal - yy * sinVal;
        this.y = xx * sinVal + yy * cosVal;
        return this;
    }

    dotProduct(vec) {
        return (this.x * vec.x) + (this.y * vec.y);
    }

    scale(s) {
        this.x *= s;
        this.y *= s;
    }
}
