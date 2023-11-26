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

			Logger.ClearLog();

			Logger.StartLog($"Server recieved args {string.Join(",", args)}");
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
				Environment.Exit(0);
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
			Logger.StartLog("Server Received Args");

			// Check if web server exists
			if (Directory.Exists(webRoot) == false)
			{
				ServerUI.displayServerMsg("Web Root does not exist, Server Shutdown");
				Logger.ErrorLog("Web Root does not exist");
				Environment.Exit(0);
			}

			// TCP/ IP Listener Test create here

			// Test if IP Exists and port valid
			TcpListener server = null;
			try
			{

				Logger.StartLog("Setting up Server Listener");

				IPAddress serverIP = IPAddress.Parse(ip);
				Int32 serverPort = Int32.Parse(port);

				server = new TcpListener(serverIP, serverPort);
				server.Start();
				Logger.NormalLog("Server Setup Successful");
				while (true)
				{

					Logger.RequestLog("Server Ready to Receive Request");

					TcpClient client = server.AcceptTcpClient();
					Logger.RequestLog("Request Recieved");
					// Read the msg 
					byte[] bytes = new byte[1024];
					

					NetworkStream stream = client.GetStream();
					int i = stream.Read(bytes, 0, bytes.Length);

																								
					string request = System.Text.Encoding.ASCII.GetString(bytes, 0, i);


					Logger.NormalLog("Preparing Response to request");
					ResponseBuilder response = new ResponseBuilder(request, webRoot);

					Logger.NormalLog("Building Response Message");
					byte[] bMsg = response.ResponseMsg();

					
					Logger.ResponseLog("Sending Response....");
					
					stream.Write(bMsg, 0, bMsg.Length);
					stream.Flush();
					string responseHeaderWithoutNewLines = response.ResponseHeaders.Replace("\r\n", " ");

					if (response.ErrorFound == true)
					{
						Logger.ResponseLog($"Response Successfully Sent: {response.HttpCode}");
					}
					else
					{
						Logger.ResponseLog($"Response Successfully Sent: \n{responseHeaderWithoutNewLines}");
					}

					Logger.NormalLog("Closing Connection");
					stream.Close();
					client.Close();
					Logger.NormalLog("Connection Successfully Closed");
				}

			}
			catch (Exception ex)
			{
				// WRITE SERVER ERROR

				ServerUI.displayServerMsg(ex.Message);
				Logger.NormalLog(ex.Message);
				Environment.Exit(0);
			}
			finally
			{
				Logger.NormalLog("Server Stoppped");
				server.Stop();
			}

		}

	}
}
