using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
<<<<<<< HEAD
    public static class SendAndRecieve
    {
        public static void Send(StreamWriter writer, string message)
        {
            // send message
            writer.WriteLine(message);
            writer.Flush();
        }
        public static bool RecieveInfo(StreamReader reader)
        {
            try {
                string result = reader.ReadLine();
                if (result == null || result.Equals("invalid command"))
                {
                    return false;
                }
                if (!result.Equals("end"))
                {
                    Console.WriteLine("Result = {0}", result);
                    result = reader.ReadLine();
                    while (!result.Equals("end"))
                    {
                        Console.WriteLine(result);
                        // Get result from server
                        result = reader.ReadLine();
                    }
                }
            }
            catch
            {
=======
	public static class SendAndRecieve
	{
		public static void Send(StreamWriter writer, string message)
		{
			// send message
			writer.WriteLine(message);
			writer.Flush();
		}
		public static bool RecieveInfo(StreamReader reader)
		{
			try
			{
				string result = reader.ReadLine();
				if (result == null || result.Equals("invalid command"))
				{
					return false;
				}
				if (!result.Equals(""))
				{
					Console.WriteLine("Result = {0}", result);
					result = reader.ReadLine();
					while (result != null && !result.Equals("end") && !result.Equals(""))
					{
						Console.WriteLine(result);
						// Get result from server
						result = reader.ReadLine();
					}
				}
			}
			catch
			{
>>>>>>> ed4fabfa7ebd57e25b740162d6dc41f2519e9a48

			}
			return true;
		}
	}
}