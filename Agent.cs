/*
 * Created by SharpDevelop.
 * User: hadie
 * Date: 26/02/2022
 * Time: 08:36 a. m.
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
	/// Description of Agent.
	/// </summary>
	public class Agent
	{
		Point P_A;
		Point P_D;
		
		public Agent()
		{

		}
		
		public Agent(Point p_A, Point p_D)
		{
			this.P_A = p_A;
			this.P_D = p_D;
		}
		
		public Point pA
        {
            get { return P_A; }
            set { P_A = value; }
        }
		
		public bool canWalk()
		{
			if(P_A == P_D)
			{
				return false;
			}
			
			else
			{
				return true;
			}
		}
	}
}
