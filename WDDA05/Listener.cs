using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace WDDA05
{
	internal class Listener
	{

		public void StartListener(IPAddress address, Int32 port)
		{
			// Listener Setup
			TcpListener server = null;
			try
			{
				IPAddress serverIP = address;
				Int32 serverPort = port;

				server.Start();
				while (true) {

					ServerUI.displayServerMsg("Waiting Connection...");
					TcpClient client = server.AcceptTcpClient();

					// Read the msg 
					byte[] bytes = new byte[1024];
					string filePath = null;

					NetworkStream stream = client.GetStream();
					int i = stream.Read(bytes, 0, bytes.Length);


					// Translate data bytes to a ASCII string.
					filePath = System.Text.Encoding.ASCII.GetString(bytes, 0, i);

					
					if (File.Exists(filePath) == true){

						ServerUI.displayServerMsg("File requested found");

						// CALL RESPONSEHTTPFileBuilder
						// This will be used to construct a file

						// CREATE HTTP response object with the file path given
						// object.Generate String

					}
					else
					{
						ServerUI.displayServerMsg("File Path was not sent");
						// CALL RESPONSE FUNCTION WITH ERROR HTTP CODE 400
					}




				}

			}
			catch (Exception ex)
			{
				ServerUI.displayServerMsg(ex.Message);
			}

			
		}
	

	}
}
