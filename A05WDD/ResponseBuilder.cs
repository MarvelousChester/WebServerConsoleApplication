using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Data;
using System.Text.RegularExpressions;
using System.Drawing;
using System.Runtime.Remoting.Messaging;


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
		private string httpCode;
		private string errorMsg;
		private bool errorFound;
		private string responseHeaders;
		public string ResponseHeaders { get { return responseHeaders; } }
		public string Method { get { return method; } }

		public bool ErrorFound { get { return errorFound; } }
		public string HttpCode { get { return httpCode; } }
		public ResponseBuilder(string requestMsg, string webRoot)
		{

			// Validation against POST

			responseHeaders = string.Empty;


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

			Logger.RequestLog($"{method} is used used for request for file {path}");
			

			// Get Content Type
			try
			{
				if (method == "POST")
				{
					httpCode = "405 Method Not Allowed";
					errorFound = true;

				}
				else if (File.Exists(path) == false)
				{
					errorFound = true;
					httpCode = "404 Not Found";
				}
				else
				{
					Logger.NormalLog("Opening File");
					FileInfo fileInfo = new FileInfo(path);
					Logger.NormalLog("Getting content-type");

					Logger.NormalLog("Reading Extension");
					string extension = fileInfo.Extension;



					if (method == "POST")
					{
						httpCode = "405 Method Not Allowed";
						errorFound = true;

					}
					if (extension == ".txt")
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

						httpCode = "200 OK";
					}
					else if (extension == ".html" || extension == ".htm" || extension == ".dhtml" || extension == ".shtml")
					{
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
						httpCode = "200 OK";

					}
					else if (extension == ".jpeg" || extension == ".jpg")
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

						httpCode = "200 OK";
					}
					else if (extension == ".gif")
					{
						// ************
						// GIF
						// *************** 
						Logger.NormalLog("Setting content-type");
						contentType = "image/gif";

						Logger.NormalLog("Reading Content Length");
						contentLength = fileInfo.Length;

						Logger.NormalLog("Setting up Content");

						content = File.ReadAllBytes(path);

						Logger.NormalLog("Setting Server");
						server = "Llanfairpwllgwyngyll";

						Logger.NormalLog("Setting Time");
						DateTime dt = DateTime.UtcNow;
						TimeZoneInfo timeInfo = TimeZoneInfo.Local;
						date = dt.ToLongDateString() + " " + dt.ToString($"HH:mm:ss \"GMT\"");

						httpCode = "200 OK";
					}
					else
					{
						httpCode = "415 Unsupported Media Type";
						errorFound = true;

					}
				}
			}
			catch (FileNotFoundException ex)
			{
				httpCode = "404 Not Found";
				Logger.ErrorLog(ex.Message);
				errorFound = true;
				// RESPONSE HTTP
			}
			catch (IOException ex)
			{

				httpCode = "500 Internal Server Error ";
				Logger.ErrorLog(ex.Message);
				errorFound = true;
			}
			catch (Exception ex)
			{
				httpCode = "500 Internal Server Error";
				Logger.ErrorLog(ex.Message);
				errorFound = true;
			}
			
			
			if(errorFound == true)
			{
				DateTime dt = DateTime.UtcNow;
				TimeZoneInfo timeInfo = TimeZoneInfo.Local;
				date = dt.ToLongDateString() + " " + dt.ToString($"HH:mm:ss \"GMT\"");
				Logger.ErrorLog("Invalid Type Given");
				content = Encoding.ASCII.GetBytes(httpCode);

				contentType = "text/plain";
				server = "Llanfairpwllgwyngyll";

				Logger.ErrorLog(httpCode);
				responseHeaders = protocol + " " + httpCode + "\r\nDate: " + date + "\r\nServer: " + server + "\r\nContent-Type: " + contentType + "\r\nContent:Length: " + contentLength + "\r\n\r\n";

			}
			else
			{
				responseHeaders = protocol + " " + httpCode + "\r\nDate: " + date + "\r\nServer: " + server + "\r\nContent-Type: " + contentType + "\r\nContent:Length: " + contentLength + "\r\n\r\n";
			}


			// Get Server


			// Get Date
		
		}



		//	Name: ResponseMsg
		//  Purpose: Gets all the variable and builds a byte message using the string messag to send to the server. First checks if any error found
		//	Parameters: NONE
		//	Return: Byte[] that is the response with message
		public byte[] ResponseMsg()
		{
			string responseMsg = string.Empty;
			
			if (errorFound == true) { 

				
				content = Encoding.ASCII.GetBytes(httpCode);
				contentLength = httpCode.Length;
				byte[] responseMSGByte = Encoding.ASCII.GetBytes(responseHeaders);
				byte[] withContent = responseMSGByte.Concat(content).ToArray();
				return withContent;

			}
			else
			{
				byte[] responseMSGByte = Encoding.ASCII.GetBytes(responseHeaders);
				byte[] withContent = responseMSGByte.Concat(content).ToArray();
				return withContent;
			}
			
		}
	}
}
