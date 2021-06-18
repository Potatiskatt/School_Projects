import java.awt.Color;
import java.awt.Dimension;
import java.awt.Font;
import java.awt.FontMetrics;
import java.awt.Graphics;
import java.awt.Image;
import java.awt.Toolkit;
import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;
import java.awt.event.KeyAdapter;
import java.awt.event.KeyEvent;
import java.net.URL;

import javax.swing.*;

public class Board extends JPanel implements ActionListener
{
	public final static int WIDTH = 500;	//Width of the board
	public final static int HEIGHT = 500;	//Height of the board
	
	private final int DELAY = 140;	//Determines the speed of the game. 
	
	private boolean gameIsRunning = true;	//Checks if the game is running
	
	private Image snakeHeadImage;	//The image for the snake head
	private Image snakeBodyImage;	//The image for the snake body
	private Image appleImage;		//The image for the apple
	
	private Timer timer;
	
	private int score;
	
	//The font we will use when displaying text on screen.
	private Font font = new Font("Helvetica", Font.BOLD, 14);
	private FontMetrics metr = getFontMetrics(font);
	
	private Apple apple = new Apple(0, 0);
	private Snake snake;
	
	/**
	 * Constructor for the board.
	 */
	public Board()
	{
		initBoard();
	}
	
	/**
	 * Initiate the board. Add listener for the keyboard, set background color and size, load the images.
	 * Create the snake and place the apple on the board.
	 * Create the timer that will pace the game.
	 */
	private void initBoard()
	{
		addKeyListener(new KeyInput());
		setBackground(Color.black);
		setFocusable(true);
		setPreferredSize(new Dimension(WIDTH, HEIGHT));
		loadImages();
		
		snake = new Snake(50, 50, 3);	//Create a snake at (50,50) that is 3 dots long.
		placeApple();
		score = 0;
		
		timer = new Timer(DELAY, this);
		timer.start();
	}
	
	/**
	 * Loads an image
	 * @param source
	 * @return
	 */
	private Image loadImage(String source)
	{
		URL url = Board.class.getResource(source);
		ImageIcon icon = new ImageIcon(url);
		return icon.getImage();
	}
	
	/**
	 * Loads all the images for the game from the resources folder.
	 */
	private void loadImages()
	{
		snakeBodyImage = loadImage("/resources/body.png");
		snakeHeadImage = loadImage("/resources/head.png");
		appleImage = loadImage("/resources/apple.png");
	}
	
	/**
	 * Draw everything onto the board.
	 */
	@Override
	public void paintComponent(Graphics g)
	{
		super.paintComponent(g);	//Paints everything that draw() doesn't, so the background is always filled.
		draw(g);
	}
	
	/**
	 * Displays the score, draws the apple and the snake onto the board.
	 * @param g
	 */
	private void draw(Graphics g)
	{
		//Display the score, draw the snake and the apple while the game is running
		if(gameIsRunning)
		{
			String msg = "Score: " + score;
			g.setColor(Color.WHITE);
			g.drawString(msg, 10, 15);
			
			g.drawImage(appleImage, apple.getX(), apple.getY(), this);
			
			for(int i = 0; i < snake.getLength(); i++)
			{
				if(i == 0)	//Head is at 0 in the array.
				{
					g.drawImage(snakeHeadImage, snake.getX(i), snake.getY(i), this);
				}
				else	//Body is in every other spot.
				{
					g.drawImage(snakeBodyImage, snake.getX(i), snake.getY(i), this);
				}
			}
			Toolkit.getDefaultToolkit().sync(); //Makes sure the window is up-to-date.
		}
		//If we lose or win, display some text.
		else
		{
			if(snake.getLength() >= snake.getMaxLength())
			{
				String msg = "Maximum score acquired! You win!";
				g.setColor(Color.WHITE);
				g.drawString(msg, (WIDTH - metr.stringWidth(msg)) / 2, HEIGHT / 2);
			}
			else
			{
				String msg = "Game over";
				g.setColor(Color.WHITE);
				g.drawString(msg, (WIDTH - metr.stringWidth(msg)) / 2, HEIGHT / 2);
			}
		}
	}
	
	/**
	 * Place the apple on a random position on the board.
	 */
	private void placeApple()
	{
		int maxX = WIDTH / apple.getSize();
		int rand = (int) (Math.random() * (maxX));
		apple.setX(rand*apple.getSize());
		
		int maxY = HEIGHT / apple.getSize();
		rand = (int) (Math.random() * (maxY));
		apple.setY(rand*apple.getSize());
	}
	
	/**
	 * Checks input from keyboard.
	 */
	private class KeyInput extends KeyAdapter
	{
		@Override
		public void keyPressed(KeyEvent e)
		{
			int key = e.getKeyCode();
			
			//If left arrow is pressed and the snake is not going right, go left. 
			//We don't want to be able to go left if we are already going right since we would collide with ourselves.
			if((key == KeyEvent.VK_LEFT) && (snake.getXDir() != 1))
			{
				snake.setDirection(-1, 0);
			}
			//If right arrow is pressed and we are not going left, go right
			if((key == KeyEvent.VK_RIGHT) && (snake.getXDir() != -1))
			{
				snake.setDirection(1, 0);
			}
			//If up arrow is pressed...
			if((key == KeyEvent.VK_UP) && (snake.getYDir() != 1))
			{
				snake.setDirection(0, -1);
			}
			//If down arrow is pressed...
			if((key == KeyEvent.VK_DOWN) && (snake.getYDir() != -1))
			{
				snake.setDirection(0, 1);
			}
		}
	}
	
	/**
	 * Checks for collisions between the snake and itself, and the snake and the apple.
	 */
	private void checkCollision()
	{
		//Snake colliding with itself.
		for(int i = snake.getLength(); i > 0; i--)
		{
			if((i > 4) && (snake.getX(0) == snake.getX(i)) && (snake.getY(0) == snake.getY(i)))
			{
				gameIsRunning = false;
			}
		}
		//Snake colliding with the edge of the board
		if(snake.getY(0) >= HEIGHT || snake.getY(0) < 0 
				|| snake.getX(0) >= WIDTH || snake.getX(0) < 0)
			gameIsRunning = false;
		//Snake colliding with apple. Apple is replaced, score is increased, and the snake gets longer.
		if((snake.getX(0) == apple.getX()) && (snake.getY(0) == apple.getY()))
		{
			placeApple();
			score++;
			snake.increaseLength(1);
		}
	}

	/**
	 * When something happens, do this. 
	 * Basically loops as the game runs.
	 */
	@Override
	public void actionPerformed(ActionEvent e)
	{
		if(gameIsRunning)
		{
			snake.move();
			checkCollision();
		}
		repaint();	//Draw the board again so any changes are visible on screen.
	}
}
