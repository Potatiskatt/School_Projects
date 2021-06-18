
public abstract class Dot
{
	protected final static int SIZE = 10;	//Length of one side
	protected int x;						//X position on the board
	protected int y;						//Y position on the board
	
	public Dot() {}
	
	public int getSize()
	{
		return SIZE;
	}
	public int getY()
	{
		return y;
	}
	public int getX()
	{
		return x;
	}
	public void setX(int x)
	{
		this.x = x;
	}
	public void setY(int y)
	{
		this.y = y;
	}
}
