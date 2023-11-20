using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WDDA05
{
	internal class Program
	{
		static void Main(string[] args)
		{
			// Get Args
			const int MIN_ARGS = 3;
			string webRoot;
			string ip;
			string port;

			// Validate Length and extract 3 args given
			// –webRoot=C:\localWebSite –webIP=192.168.100.23 –webPort=5300
			if (args.Length == MIN_ARGS) {

				webRoot = args[0].Trim('=');
				ip = args[1].Trim('=');
				port = args[2].Trim('=');

			}
            else
            {

				Console.WriteLine("Invalid Amount of Args Provided");
				
            }

         


            // Call Validation Method to validate each

            // IP validation

            // Port validation >0 < MAX PORT NUM

            // VALIDATE FILE EXISTS AND PATH


            // Call Server Listener with param



        }


	}
}
