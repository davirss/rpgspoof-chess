
public class BaseMovement {
    public Direction direction;
    public int amount;
    public int minAmount;
    public BaseMovement(Direction direction, int amount, int minAmount = 1) {
        this.direction = direction;
        this.amount = amount;
        this.minAmount = minAmount;
    }
}
