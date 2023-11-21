using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace myOwnWebServer
{
	internal class Program
	{
		static void Main(string[] args)
		{

			// CALL CLEAR LOG 


			Logger.StartLog($"Server recieved args{string.Join(",", args)}");
			// Accept Args
			const int MIN_ARGS = 3;
			string webRoot = "";
			string ip;
			string port;

			// Validate Length and extract 3 args given
			// –webRoot=C:\localWebSite –webIP=192.168.100.23 –webPort=5300
			if (args.Length != MIN_ARGS)
			{

				Console.WriteLine("Invalid Amount of Args Provided");
				Environment.Exit(0);
			}

			// PArse
			const int WEBROOT_START_INDEX = 9;
			const int IP_START_INDEX = 7;
			const int PORT_START_INDEX = 9;

			webRoot = args[0];
			webRoot = webRoot.Trim().Substring(WEBROOT_START_INDEX);
			Console.WriteLine(webRoot);
			ip = args[1];
			ip = ip.Trim().Substring(IP_START_INDEX);
			Console.WriteLine(ip);
			port = args[2];
			port = port.Trim().Substring(PORT_START_INDEX);
			Console.WriteLine(port);


			// Check if web server exists
			if (Directory.Exists(webRoot) == false)
			{
				ServerUI.displayServerMsg("Web Root does not exist");
				Environment.Exit(0);
			}

			// TCP/ IP Listener Test create here

			// Test if IP Exists and port valid
			try
			{
				TcpListener server = null;
				IPAddress serverIP = IPAddress.Parse(ip);
				Int32 serverPort = Int32.Parse(port);

				server = new TcpListener(serverIP, serverPort);
				server.Start();

				TcpClient client = server.AcceptTcpClient();

				// Read the msg 
				byte[] bytes = new byte[1024];
				string filePath = null;

				NetworkStream stream = client.GetStream();
				int i = stream.Read(bytes, 0, bytes.Length);


			}
			catch (Exception ex)
			{
				ServerUI.displayServerMsg(ex.Message);
				Environment.Exit(0);
			}

		}
	}
}
