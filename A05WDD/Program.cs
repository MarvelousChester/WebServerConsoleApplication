using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A05WDD
{
	internal class Program
	{
		static void Main(string[] args)
		{
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
		}
	}
}
