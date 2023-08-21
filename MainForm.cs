/*
 * Created by SharpDevelop.
 * User: hadie
 * Date: 29/01/2022
 * Time: 08:50 a. m.
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Proyect1
{
	/// <summary>
	/// Description of MainForm.
	/// </summary>
	public partial class MainForm : Form
	{
		Bitmap bmpImage, bmpAnimation;
		List<Circle> circleList;
		
		int circleHeight = 0;
		int circleWidth = 0;
		
		int circleCenterX = 0;
		int circleCenterY = 0;
		
		int circleRadius;
		
		int v = 0;
		
		int[] x = new int[100];
		int[] y = new int[100];
		int[] radius = new int[100];
		
		int countClicks = 0;
		
		
		Point P_origin;
		Point P_destiny;
		public MainForm()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			circleList = new List<Circle>();
			
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
		}
		
		void ButtonSelectImageClick(object sender, EventArgs e)
		{
			openFileDialog1.ShowDialog();
			bmpImage = new Bitmap(openFileDialog1.FileName);
			bmpAnimation = new Bitmap(bmpImage.Width, bmpImage.Height);
			pictureBoxImage.BackgroundImage = bmpImage;
			pictureBoxImage.BackgroundImageLayout = BackgroundImageLayout = ImageLayout.Zoom;
			pictureBoxImage.Image = bmpAnimation;
		}
		
		//BUSCAR CIRCULO
		void ButtonSearchClick(object sender, EventArgs e)
		{
			v=0;
			countClicks = 0;
			circleList.Clear();
			treeView1.Nodes.Clear();
			Color c_i;
			for(int y_i = 0 ; y_i < bmpImage.Height ; y_i++)
			{
				for(int x_i = 0 ; x_i < bmpImage.Width ; x_i++)
				{
					c_i = bmpImage.GetPixel(x_i, y_i);
					if (isBlack(c_i))
					{
						getCircle(x_i, y_i, bmpImage);
						x[v] = circleCenterX;
						y[v] = circleCenterY;
						radius[v] = circleRadius;
						v++;
						Circle circle = new Circle(circleCenterX, circleCenterY, v, circleRadius);
						circleList.Add(circle);
						//EVITAR CIRCULOS REPETIDOS
						drawCircule(circleCenterX-(circleWidth/2+2), circleCenterY-(circleHeight/2+2), circleWidth+4, circleHeight+4, bmpImage);
						pictureBoxImage.Refresh();
					}
				}
			}
			
			for(int i = 0;i<circleList.Count;i++)
			{
				v=i+1;
				TreeNode n_v = new TreeNode(v.ToString());
				for(int j=i+1;j<circleList.Count;j++){
					v=j+1;
					TreeNode n_a = new TreeNode(v.ToString());
					drawLine(x[i], y[i], x[j], y[j], bmpImage);
					n_v.Nodes.Add(n_a);
				}
				treeView1.Nodes.Add(n_v);
			}
			
			for(int i = 0;i<circleList.Count;i++)
			{
				drawCircule(x[i]-radius[i], y[i]-radius[i], radius[i]*2+2, radius[i]*2+2, bmpImage);
				drawString(i+1, x[i], y[i], bmpImage);
			}
			
			pictureBoxImage.Refresh();
		}
		
		bool isBlack(Color color)
		{
			if(color.R == 0)
			{
				if(color.G == 0)
				{
					if(color.B == 0)
					{
						return true;
					}
				}
			}
			
			return false;
		}
		
		void drawCircule(int x, int y, int w, int h, Bitmap bmpLocal)
		{
			Graphics g = Graphics.FromImage(bmpLocal);
			Brush brocha = new SolidBrush(Color.Gold);
			g.FillEllipse(brocha, x, y, w, h);
		}
		
		void drawCircle(int x, int y, int w, int h, Bitmap bmpLocal)
		{
			Graphics g = Graphics.FromImage(bmpLocal);
			Brush brocha = new SolidBrush(Color.Red);
			g.FillEllipse(brocha, x, y, w, h);
		}

		void drawString(int num, int x, int y, Bitmap bmpLocal)
		{
			Graphics g = Graphics.FromImage(bmpLocal);
			String drawstring = num.ToString();
		             
		    Font drawFont = new Font("Arial", 20);
		    SolidBrush drawBrush = new SolidBrush(Color.Red);
		            
		    StringFormat drawFormat = new StringFormat();
		    drawFormat.FormatFlags = StringFormatFlags.DirectionRightToLeft;
		             
		    g.DrawString(drawstring, drawFont, drawBrush, x, y, drawFormat);
		}
		
		//CALCULAR CENTRO
		void getCircle(int c_x, int c_y, Bitmap bmpLocal)
		{
			Color c_i;
			
			int y_i = c_y;
			int x_i = c_x;
			
			do
			{
				c_i = bmpImage.GetPixel(x_i, y_i);
				y_i++;
			}while (isBlack(c_i));
			
			circleCenterY = ((((y_i-1)-c_y)/2) + c_y);
			
			circleHeight = (y_i-1)-c_y;
			
			do
			{
				c_i = bmpImage.GetPixel(x_i, circleCenterY);
				x_i++;
			}while (isBlack(c_i));
			
			
			int x_i2 = c_x;
			
			do
			{
				c_i = bmpImage.GetPixel(x_i2, circleCenterY);
				x_i2--;
			}while (isBlack(c_i));
			
			circleCenterX = ((((x_i-1)-x_i2+1)/2) + x_i2+1);
			
			circleWidth = (x_i-1)-(x_i2+1);
			
			circleRadius = circleWidth/2;
		}

		void drawLine(float x1, float y1, float x2, float y2, Bitmap bmpLocal)
		{
			Graphics g = Graphics.FromImage(bmpLocal);
			Pen bluePen = new Pen(Color.Blue, 2);
			g.DrawLine(bluePen, x1, y1, x2, y2);
		}
		
		void PictureBoxImageMouseClick(object sender, MouseEventArgs e)
		{
			Graphics g = Graphics.FromImage(bmpImage);
			Brush brocha1 = new SolidBrush(Color.Red);
			Brush brocha2 = new SolidBrush(Color.Blue);
			
			double distance;
			
			float x_b, y_b;
			float x_p, y_p;
			float w_b, h_b;
			float w_p, h_p;
			float r, r_x, r_y;
			float d_x, d_y;
		
			w_p = pictureBoxImage.Width;
			h_p = pictureBoxImage.Height;
			w_b = bmpImage.Width;
			h_b = bmpImage.Height;
			x_p=e.X;
			y_p=e.Y;
		
			r_x=w_p/w_b;
			r_y = h_p/h_b;
			
			if(r_x<r_y)
				r=r_x;
			else
				r=r_y;
			d_x=(w_p-r*w_b)/2;
			d_y=(h_p-r*h_b)/2;
		
			x_b= (x_p-d_x)/r;
			y_b= (y_p-d_y)/r;
			
			
			for(int i=0 ; i<circleList.Count ; i++)
			{
				distance = Math.Sqrt( ((x[i]-x_b)*(x[i]-x_b)) + ((y[i]-y_b)*(y[i]-y_b)) );
				
				if(distance <= radius[i])
				{
					if(countClicks == 0)
					{
						g.FillEllipse(brocha1, x[i]-10, y[i]-10, 20, 20);
						P_origin.X = x[i];
						P_origin.Y = y[i];
						countClicks++;
					}
					else if(countClicks == 1)
					{
						g.FillEllipse(brocha2, x[i]-10, y[i]-10, 20, 20);
						P_destiny.X = x[i];
						P_destiny.Y = y[i];
						countClicks++;
					}
					pictureBoxImage.Refresh();
					break;
				}
			}
	
		}
		
		
		void ButtonAnimationClick(object sender, EventArgs e)
		{	
			int [] positionVisited = new int[100];
			Agent Agente = new Agent(P_origin, P_destiny);
			
			Point p_origen, p_destino;
			
			bool visit;
			
			int value, co=0;
			
			p_origen = P_origin;
			p_destino = P_destiny;
			
			for(int i=0; i<circleList.Count; i++)
			{
				if (x[i]==P_origin.X)
				{
					if(y[i]==P_origin.Y)
					{
						positionVisited[co] = i;
					}
				}
			}
			
			co++;
			
			do
			{
				do
				{
					visit = false;
					Random random = new Random(DateTime.Now.Millisecond);
					value = random.Next(0, circleList.Count);
					
					for(int i=0; i<circleList.Count; i++)
					{
						if (positionVisited[i] == value)
						{
							visit = true;
						}
					}
					
					positionVisited[co] = value;
				}while(visit);
				
				co++;
			    p_destino.X = x[value];
			    p_destino.Y = y[value];
			    
				animation(p_origen, p_destino);
				
				p_origen.X = x[value];
			    p_origen.Y = y[value];
			    
			    Agente.pA = p_origen;
			    
			}while(Agente.canWalk());
		}
		
		void animation(Point por, Point pdes)
		{
			Graphics g = Graphics.FromImage(bmpAnimation);
			Brush brocha = new SolidBrush(Color.Green);
			
			float x_0, y_0;
			float x_f, y_f;
			float x_k, y_k;
			float m, b;
			double d;
			int inc = 20;
			
			y_0 = por.Y;
			x_0 = por.X;
			y_f = pdes.Y;
			x_f = pdes.X;
			
			if(x_0 == x_f)
			{
				if(y_f < y_0)
				{
					inc = -20;	
				}
				for(y_k = y_0; y_k != y_f; y_k+=inc)
			    {
			    	g.FillEllipse(brocha,(int)Math.Round(x_0-10),(int)Math.Round(y_k-10), 20, 20);
			    	pictureBoxImage.Refresh();
			    	g.Clear(Color.Transparent);
			    	d= Math.Sqrt( ((x_0-x_f)*(x_0-x_f)) + ((y_k-y_f)*(y_k-y_f)) );
			    	
			    	if(d<=40)
			    	{
			    		inc = 1;
			    		if(y_f < y_0)
						{
							inc = -1;	
						}
			    	}
			    }
				
			}
			
			else
			{
				m = (y_f - y_0) / (x_f - x_0);
			
				b = y_0 - m * x_0;
				
				if(m <= 1 && m>=-1)
				{
					if(x_f < x_0)
					{
						inc = -20;	
					}
					
					for(x_k = x_0; x_k != x_f; x_k+=inc)
				    {
				    	y_k = m*x_k+b;
				    	g.FillEllipse(brocha,(int)Math.Round(x_k-10),(int)Math.Round(y_k-10), 20, 20);
				    	pictureBoxImage.Refresh();
				    	g.Clear(Color.Transparent);
				    	
				    	d= Math.Sqrt( ((x_k-x_f)*(x_k-x_f)) + ((y_k-y_f)*(y_k-y_f)) );
			    	
				    	if(d<=40)
				    	{
				    		inc = 1;
				    		if(x_f < x_0)
							{
								inc = -1;	
							}
				    	}
			
				    }
				}
				
				else
				{
					if(y_f < y_0)
					{
						inc = -20;	
					}
						
					for(y_k = y_0; y_k != y_f; y_k+=inc)
				    {
				    	x_k = 1/m*(y_k-b);
				    	g.FillEllipse(brocha,(int)Math.Round(x_k-10),(int)Math.Round(y_k-10), 20, 20);
				    	pictureBoxImage.Refresh();
				    	g.Clear(Color.Transparent);
				    	
				    	d= Math.Sqrt( ((x_k-x_f)*(x_k-x_f)) + ((y_k-y_f)*(y_k-y_f)) );
			    	
				    	if(d<=40)
				    	{
				    		inc = 1;
				    		
				    		if(y_f < y_0)
							{
								inc = -1;	
							}
				    	}
				    }
				}
			}
		}
		
		
		
	}
}
