/*
* Name: Karandeep Sandhu
		 Leiya Ma
 Project Name : A05-TPI/IP
 File Name : ServerUI.cs
 Date : 2023 - 11 - 19
 Purpose : Contain the class ServerUI, which is used to display server messages

*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A05WDD
{

	public static class ServerUI
	{


		// display server message
		public static void displayServerMsg(string msg)
		{
			Console.WriteLine(msg);
		}

		// display server message
		public static void displayServerMsg(string msg, params object[] args)
		{
			Console.WriteLine(msg, args);
		}

	}

}