/*
 *
  Project Name : myOwnWebServer
  File Name : Program.cs
  Date : 2023 - 11 - 22
  Purpose : Contains Main Program that takes args passed and parses them. Then creates a Listener class and passes args if they are correct
 */


using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Net.Security;

namespace myOwnWebServer
{
	internal class Program
	{
		static void Main(string[] args)
		{
			
			// CALL CLEAR LOG 
			const int SERVER_ERROR = -1;
			Logger.ClearLog();

			Logger.StartLog($"Server Recieved Args: {string.Join(",", args)}");
			// Accept Args
			const int MIN_ARGS = 3;
			string webRoot = "";
			string ip;
			string port;

			// Validate Length and extract 3 args given
			// –webRoot=C:\localWebSite –webIP=192.168.100.23 –webPort=5300
			if (args.Length != MIN_ARGS)
			{

				ServerUI.displayServerMsg("Invalid Amount of Args Provided");
				Logger.ErrorLog("Invalid Amount of Args Provided");
				Environment.Exit(SERVER_ERROR);
			}

			// Parse Args
			const int WEBROOT_START_INDEX = 9;
			const int IP_START_INDEX = 7;
			const int PORT_START_INDEX = 9;



			webRoot = args[0];
			webRoot = webRoot.Trim().Substring(WEBROOT_START_INDEX);
			ip = args[1];
			ip = ip.Trim().Substring(IP_START_INDEX);
			port = args[2];
			port = port.Trim().Substring(PORT_START_INDEX);


			

			// Check if web server exists
			if (Directory.Exists(webRoot) == false)
			{
				ServerUI.displayServerMsg("Web Root does not exist, Server Shutdown");
				Logger.ErrorLog("Web Root does not exist");
				Environment.Exit(SERVER_ERROR);
			}
			
			

			// Start Listener
			Listener listener = new Listener(webRoot, ip, port);

			listener.StartListener();
			

		}
	}
}
