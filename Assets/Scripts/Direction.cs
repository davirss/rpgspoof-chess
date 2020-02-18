
public enum Direction {
    UP, LEFT, DOWN, RIGHT, UP_LEFT, UP_RIGHT, DOWN_LEFT, DOWN_RIGHT, CENTER
}


public static class DirectionMethods {
    public static bool IsDiagonalDirection(this Direction direction) {
        switch(direction) {
            case Direction.UP_LEFT:
            case Direction.UP_RIGHT:
            case Direction.DOWN_LEFT:
            case Direction.DOWN_RIGHT:
                return true;
            default: return false;
        }
    }
    
    public static bool isUp(this Direction direction) {
        switch (direction) {
            case Direction.UP_RIGHT:
            case Direction.UP:
            case Direction.UP_LEFT:
                return true;
            default: return false;
        }
    }

    public static bool isRight(this Direction direction) {
        switch (direction) {
            case Direction.UP_RIGHT:
            case Direction.RIGHT:
            case Direction.DOWN_RIGHT:
                return true;
            default: return false;
        }
    }
}