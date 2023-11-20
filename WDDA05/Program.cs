using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net;

namespace WDDA05
{
	internal class Program
	{
		static void Main(string[] args)
		{
			// Get Args
			const int MIN_ARGS = 3;
			string webRoot = "";
			string ip;
			string port;

			// Validate Length and extract 3 args given
			// –webRoot=C:\localWebSite –webIP=192.168.100.23 –webPort=5300
			if (args.Length != MIN_ARGS) {

			
				Console.WriteLine("Invalid Amount of Args Provided");
				Environment.Exit(0);
			}


			webRoot = args[0];
			ip = args[1];
			port = args[2];
			Int32 serverPort = 0;
			IPAddress serverIP ;
			// Validate Root directory Exists
			if (Directory.Exists(webRoot) == false)
			{
				ServerUI.displayServerMsg("Web root does not exist");
				// return false
			}


			string respondMsg;

			bool IfErrorWithPortOrIP = false;
			try {

				serverPort = Int32.Parse(port);
				serverIP = IPAddress.Parse(ip);

				Listener listener = new Listener();
				listener.StartListener(serverIP, serverPort);
				
				
			
			}
			catch(FormatException e) {

				ServerUI.displayServerMsg(e.Message);
				
			}
			catch(Exception ex ) {
				ServerUI.displayServerMsg(ex.Message);
				
			}





				// Call Validation Method to validate each

				// IP validation

				// Port validation >0 < MAX PORT NUM

				// VALIDATE FILE EXISTS AND PATH


				// Call Server Listener with param



			}


	}
}
