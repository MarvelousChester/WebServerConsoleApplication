/*
 * Name: 
  Project Name : myOwnWebServer
  File Name : Logger.cs
  Date : 2023 - 11 - 22
  Purpose : Contains the listener class that listens for requests and then creates a response builder object to create a message to send back.
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace myOwnWebServer
{
	internal class Listener
	{
		string webRoot;
		string ip;
		string port;


		/*  Constructor
			Name: Listener
			Purpose: Sets web root and ip, and port
			Parameters: webRoot -> Folder in which the server will folder that server operates and tries to retrive files from
					    serverIP -> Ip that server will run on
						port -> Port that server will run on
						
		*/
		public Listener(string webRoot, string serverIP, string serverPort)
		{
			this.webRoot = webRoot;
			this.ip = serverIP;
			this.port = serverPort;
		}

		/*  Method
		Name: StartListner
		Purpose: Sets up Servers IP, and Port and starts listener. Then waits for a request and once recieved will get a response ready using ResponseBuilder class. Then send that back to the client
		Return: NONE
		Paramter: NONE
	*/
		public void StartListener()
		{
			const int SERVER_ERROR = -1;
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

					Logger.NormalLog("Server Ready to Receive Request");

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

				ServerUI.displayServerMsg(ex.Message);
				Logger.NormalLog(ex.Message);
				Environment.Exit(SERVER_ERROR);
			}
			finally
			{
				server.Stop();
				Logger.NormalLog("Server Stoppped");
			}
		}

	}
}
