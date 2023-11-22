using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;


namespace myOwnWebServer
{
	public static class Logger 
	{


		public static void ClearLog()
		{
			try
			{
				string workingDirectory = Environment.CurrentDirectory;
				string projectDirectory = Directory.GetParent(workingDirectory).Parent.FullName;

				string filePath = projectDirectory + "/" + "myOwnWebServer.log";
					
				File.WriteAllText(filePath, "");
				
			}
			catch(Exception ex) {

				ServerUI.displayServerMsg(ex.Message);
			}
		}

		/// <summary>
		/// Gets File Directory path to project folder, gets message and type of log and logs into file with date
		/// </summary>
		/// <param name="message">Message that needs to be logged</param>
		/// <param name="logType">The [LogType] for the message</param>
		public static void WriteLog(string message, string logType) {

			try
			{
				string workingDirectory = Environment.CurrentDirectory;
				string projectDirectory = Directory.GetParent(workingDirectory).Parent.FullName;

				DateTime dt = DateTime.Now;

				string filePath = projectDirectory + "/" + "myOwnWebServer.log";

				string formattedMsg = dt.ToString("yyyy/M/d HH:mm:ss") + " "  + logType + " " + message;
				using (StreamWriter sw = File.AppendText(filePath))
				{
					sw.WriteLine(formattedMsg);
				}
			}
			catch (Exception ex)
			{
				ServerUI.displayServerMsg(ex.Message);
			}
		}

		/// <summary>
		/// Logs the File with an [START] bracket after Date
		/// </summary>
		/// <param name="msg">Message that needs to be logged</param>
		public static void StartLog(string msg)
		{

			WriteLog(msg, "[SERVER STARTED]");

		}

		/// <summary>
		/// Logs file with Date + msg
		/// </summary>
		/// <param name="msg"></param>
		public static void NormalLog(string msg)
		{

			WriteLog(msg, "");

		}

		/// <summary>
		/// Logs the File with an [REQUEST] bracket after Date
		/// </summary>
		/// <param name="msg">Message that needs to be logged</param>
		public static void RequestLog(string msg)
		{
			WriteLog(msg, "[REQUEST]");
		}

		/// <summary>
		/// Logs the File with an [RESPONSE] bracket after Date
		/// </summary>
		/// <param name="msg">Message that needs to be logged</param>
		public static void ResponseLog(string msg) {

			WriteLog(msg, "[RESPONSE]");
		}

		/// <summary>
		/// Logs the File with an [ERROR] bracket after Date
		/// </summary>
		/// <param name="msg">Message that needs to be logged</param>
		public static void ErrorLog(string msg)
		{

			WriteLog(msg, "[ERROR]");
		}

	}
}
