using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;



namespace A05WDD
{
	public static class Logger 
	{
		/// <summary>
		/// Logs the File with an [START] bracket after Date
		/// </summary>
		/// <param name="msg"></param>
		public static void StartLog(string msg)
		{

			string workingDirectory = Environment.CurrentDirectory;
			string projectDirectory = Directory.GetParent(workingDirectory).Parent.FullName;

			string filePath = projectDirectory + "/" + "myOwnWebServer.log";

			DateTime dt = DateTime.Now;
			string formattedMsg = dt.ToString() + "[START] " + msg;
			using (StreamWriter sw = File.AppendText(filePath))
			{
				sw.WriteLine(formattedMsg);
			}

		}

		/// <summary>
		/// Logs file with Date + msg
		/// </summary>
		/// <param name="msg"></param>
		public static void NormalLog(string msg)
		{

			string workingDirectory = Environment.CurrentDirectory;
			string projectDirectory = Directory.GetParent(workingDirectory).Parent.FullName;

			string filePath = projectDirectory + "/" + "myOwnWebServer.log";

			DateTime dt = DateTime.Now;
			string formattedMsg = dt.ToString() + " " +  msg;
			using (StreamWriter sw = File.AppendText(filePath))
			{
				sw.WriteLine(formattedMsg);
			}
		

		}

		/// <summary>
		/// Logs the File with an [REQUEST] bracket after Date
		/// </summary>
		/// <param name="msg"></param>
		public static void RequestLog(string msg)
		{
			string workingDirectory = Environment.CurrentDirectory;
			string projectDirectory = Directory.GetParent(workingDirectory).Parent.FullName;

			string filePath = projectDirectory + "/" + "myOwnWebServer.log";

			DateTime dt = DateTime.Now;
			string formattedMsg = dt.ToString() + "[REQUEST] " + msg;
			using (StreamWriter sw = File.AppendText(filePath))
			{
				sw.WriteLine(formattedMsg);
			}
		}

		/// <summary>
		/// Logs the File with an [RESPONSE] bracket after Date
		/// </summary>
		/// <param name="msg"></param>
		public static void ResponseLog(string msg) {

			string workingDirectory = Environment.CurrentDirectory;
			string projectDirectory = Directory.GetParent(workingDirectory).Parent.FullName;

			string filePath = projectDirectory + "/" + "myOwnWebServer.log";

			DateTime dt = DateTime.Now;
			string formattedMsg = dt.ToString() + "[RESPONSE] " + msg;
			using (StreamWriter sw = File.AppendText(filePath))
			{
				sw.WriteLine(formattedMsg);
			}
		}

		/// <summary>
		/// Logs the File with an [ERROR] bracket after Date
		/// </summary>
		/// <param name="msg"></param>
		public static void ErrorLog(string msg)
		{

			string workingDirectory = Environment.CurrentDirectory;
			string projectDirectory = Directory.GetParent(workingDirectory).Parent.FullName;

			string filePath = projectDirectory + "/" + "myOwnWebServer.log";

			DateTime dt = DateTime.Now;
			string formattedMsg = dt.ToString() + "[ERROR] " + msg;
			using (StreamWriter sw = File.AppendText(filePath))
			{
				sw.WriteLine(formattedMsg);
			}
		}

	}
}
