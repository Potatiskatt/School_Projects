import java.awt.EventQueue;
import javax.swing.JFrame;

/**
 * Handles the window.
 */
public class Window extends JFrame
{
	public Window()
	{
		initUI();
	}
	
	/**
	 * Defines the window
	 */
	private void initUI()
	{
		add(new Board());
		
		setResizable(false);
		pack();
		
		setTitle("Snake");
		setLocationRelativeTo(null);
		setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);
	}
	
	/**
	 * Display the window
	 * @param args
	 */
	public static void main(String[] args)
	{
		EventQueue.invokeLater(() ->
		{
			JFrame ex = new Window();
			ex.setVisible(true);
		});
	}
}
