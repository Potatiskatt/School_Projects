
public class Snake extends Dot
{
	private int length;
	//The maximum length is the total amount on dots that can fit on the screen, minus one for the apple.
	private int maxLength = (Board.WIDTH * Board.HEIGHT) / (SIZE * SIZE) - 1;	
	private int xDir = 1;					//Direction in x-plane. Starts with 1, which means the snake will be facing right
	private int yDir = 0;					//Direction in y-plane
	private int[] x = new int[maxLength];	//X-position of all parts of the snake
	private int[] y = new int[maxLength];	//Y-position of all parts of the snake

	public Snake(int x, int y, int length)
	{
		this.length = length;
		
		for(int i = 0; i < length; i++)
		{
			this.x[i] = x - i * SIZE;	//Place the snake facing to the right starting at x 
			this.y[i] = y;	
		}
	}
	
	//Getters and setters
	public int getY(int index)
	{
		return y[index];
	}

	public int getX(int index)
	{
		return x[index];
	}
	
	public int getXDir()
	{
		return xDir;
	}
	
	public int getYDir()
	{
		return yDir;
	}
	
	public int getLength()
	{
		return length;
	}
	
	public int getMaxLength()
	{
		return maxLength;
	}
	
	public void setDirection(int xDir, int yDir)
	{
		this.xDir = xDir;
		this.yDir = yDir;
	}
	
	/**
	 * Increase the length by a certain amount
	 * @param amount
	 */
	public void increaseLength(int amount)
	{
		length += amount;
	}

	/**
	 * Move the snake one dot forward in the right direction.
	 */
	public void move()
	{
		for(int i = length; i > 0; i--)
		{
			x[i] = x[(i - 1)];
			y[i] = y[(i - 1)];
		}
		//Left
		if(xDir == -1)
			x[0] -= SIZE;
		//Right
		if(xDir == 1)
			x[0] += SIZE;
		//Up
		if(yDir == -1)
			y[0] -= SIZE;
		//Down
		if(yDir == 1)
			y[0] += SIZE;
	}

}
