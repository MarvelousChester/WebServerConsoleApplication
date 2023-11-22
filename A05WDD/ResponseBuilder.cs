using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Data;

namespace myOwnWebServer
{
	public class ResponseBuilder
	{

		private string protocol;
		private string method;
		private string contentType;
		private long contentLength;
		private string content;
		private string server;
		private string date;
		private string request;
		private string path;
		private string HTTPCode;
		public ResponseBuilder(string requestMsg)
		{
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
			path = requestFields[1];
			protocol = requestFields[2];
			
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
					Logger.NormalLog("Getting content-type");
					contentType = "text/plain";

					Logger.NormalLog("Reading Content Length");
					contentLength = fileInfo.Length;

					Logger.NormalLog("Getting Server");
					server = "Llanfairpwllgwyngyll";

					Logger.NormalLog("Getting Time");
					DateTime dt = DateTime.UtcNow;
					TimeZoneInfo timeInfo = TimeZoneInfo.Local;
					date = dt.ToLongDateString() + " " + dt.ToString($"HH:mm:ss \"GMT\"");
				}
				// ************
				// HTML
				// ***************
				if (extension == ".html" || extension == ".htm") {

					Logger.NormalLog("Getting content-type");
					contentType = "text/html";
					
					Logger.NormalLog("Reading Content Length");
					contentLength = fileInfo.Length;

					Logger.NormalLog("Getting Server");
					server = "Llanfairpwllgwyngyll";

					Logger.NormalLog("Getting Time");
					DateTime dt = DateTime.UtcNow;
					TimeZoneInfo timeInfo = TimeZoneInfo.Local;
					date = dt.ToLongDateString() + " " + dt.ToString($"HH:mm:ss \"GMT\"");

					Logger.NormalLog("Getting Server");
					HTTPCode = "200";

				}
				// ************
				// JPEG
				// *************** 
				if(extension == "jpeg" || extension == "jpg")
				{
					
				}


				// ************
				// XHTML
				// *************** 



			}
			catch (FileNotFoundException ex)
			{
				HTTPCode = "404";
				Logger.ErrorLog(ex.Message);
				// RESPONSE HTTP
			}
			catch(IOException ex) {

				HTTPCode = "500";
				Logger.ErrorLog(ex.Message);
			}
			catch(Exception ex)
			{
				HTTPCode = "500";
				Logger.ErrorLog(ex.Message);
			}
			




			// Get Server


			// Get Date
		




		}






	}
}
