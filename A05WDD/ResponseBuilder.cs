using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Data;
using System.Text.RegularExpressions;
using System.Drawing;


namespace myOwnWebServer
{
	public class ResponseBuilder
	{

		private string protocol;
		private string method;
		private string contentType;
		private long contentLength;
		private byte[] content;
		private string server;
		private string date;
		private string request;
		private string path;
		private string HTTPCode;


		public ResponseBuilder(string requestMsg, string webRoot)
		{
			Logger.NormalLog("Getting Path from Request");
			string pathPattern = @"(?<=GET\s)(.*?)(?=\sHTTP/1.1)";

			
			string requestFile = Regex.Match(requestMsg, pathPattern).Value;
		
			if (requestFile.StartsWith("/"))
			{

				requestFile =  requestFile.Replace("/", "\\");
				path = webRoot + requestFile;
			}
			else
			{
				path = webRoot + "\\" + requestFile;
			}

			request = requestMsg;
			request = request.Replace("\r", " ");
			request = request.Replace("\n", "");
			
			// Removing Filler
			string[] requestFields = request.Split(' ');
			//const int HOST_NAME = 4;
			//const int REDUNDANTFILLER = 3;
			//requestField[REDUNDANTFILLER] = requestField[HOST_NAME];
					
			//Array.Resize(ref requestField, 4);		
			// Get file to open path
			method = requestFields[0];
			protocol = "HTTP/1.1";

			// Get Content Type
			try 
			{
				Logger.NormalLog("Opening File");
				FileInfo fileInfo = new FileInfo(path);
				Logger.NormalLog("Getting content-type");

				Logger.NormalLog("Reading Extension");
				string extension = fileInfo.Extension;
				// *************
				// plain text
				// **************
				if(extension == ".txt")
				{
					Logger.NormalLog("Setting content-type");
					contentType = "text/plain";

					Logger.NormalLog("Reading Content Length");
					contentLength = fileInfo.Length;

					Logger.NormalLog("Setting up Content");
					content = File.ReadAllBytes(path);

					Logger.NormalLog("Getting Server");
					server = "Llanfairpwllgwyngyll";

					Logger.NormalLog("Getting Time");
					DateTime dt = DateTime.UtcNow;
					TimeZoneInfo timeInfo = TimeZoneInfo.Local;
					date = dt.ToLongDateString() + " " + dt.ToString($"HH:mm:ss \"GMT\"");

					HTTPCode = "200 OK";
				}
				else if (extension == ".html" || extension == ".htm") {
					// ************
					// HTML
					// ***************

					Logger.NormalLog("Setting content-type");
					contentType = "text/html";
					
					Logger.NormalLog("Reading Content Length");
					contentLength = fileInfo.Length;

					Logger.NormalLog("Setting up Content");
					content = File.ReadAllBytes(path);

					Logger.NormalLog("Getting Server");
					server = "Llanfairpwllgwyngyll";

					Logger.NormalLog("Getting Time");
					DateTime dt = DateTime.UtcNow;
					TimeZoneInfo timeInfo = TimeZoneInfo.Local;
					date = dt.ToLongDateString() + " " + dt.ToString($"HH:mm:ss \"GMT\"");

					Logger.NormalLog("Getting Server");
					HTTPCode = "200 OK";

				}
				else if(extension == ".jpeg" || extension == ".jpg")
				{
					// ************
					// JPEG
					// *************** 
					Logger.NormalLog("Setting content-type");
					contentType = "image/jpeg";

					Logger.NormalLog("Reading Content Length");
					contentLength = fileInfo.Length;

					Logger.NormalLog("Setting up Content");
					
					content = File.ReadAllBytes(path); // Change to Byte if not readable in Text

					Logger.NormalLog("Setting Server");
					server = "Llanfairpwllgwyngyll";

					Logger.NormalLog("Setting Time");
					DateTime dt = DateTime.UtcNow;
					TimeZoneInfo timeInfo = TimeZoneInfo.Local;
					date = dt.ToLongDateString() + " " + dt.ToString($"HH:mm:ss \"GMT\"");

					HTTPCode = "200 OK";
				}


				// ************
				// XHTML
				// *************** 

			}
			catch (FileNotFoundException ex)
			{
				HTTPCode = "404 Not Found";
				Logger.ErrorLog(ex.Message);
				// RESPONSE HTTP
			}
			catch(IOException ex) {

				HTTPCode = "500 Internal Server Error ";
				Logger.ErrorLog(ex.Message);
			}
			catch(Exception ex)
			{
				HTTPCode = "500 Internal Server Error";
				Logger.ErrorLog(ex.Message);
			}
			

			// Get Server


			// Get Date
		
		}



		public byte[] ResponseMsg()
		{
			string responseMsg = protocol + " " + HTTPCode + "\r\nDate: " + date +"\r\nContent-Type: " + contentType + "\r\nServer: " + server + "\r\nContent-Length: "  + contentLength + "\r\n\r\n";
			byte[] responseMSGByte = Encoding.ASCII.GetBytes(responseMsg);
			byte[] withContent = responseMSGByte.Concat(content).ToArray();
			return withContent;		
		}
	}
}
