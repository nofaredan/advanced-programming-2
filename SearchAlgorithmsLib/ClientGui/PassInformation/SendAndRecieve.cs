using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientGui
{
    public static class SendAndRecieve
    {
        /// <summary>
        /// Send information.
        /// </summary>
        /// <param name="writer">The writer.</param>
        /// <param name="message">The message.</param>
        public static void Send(StreamWriter writer, string message)
        {
            // send message
            writer.WriteLine(message);
            writer.Flush();
        }

        /// <summary>
        /// Recieves information.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <returns></returns>
        public static string RecieveInfo(StreamReader reader)
        {
            string result = "";
            string currentMessage = "";
            try
            {
                result = reader.ReadLine();
                if (result == null)
                {
                    return "";
                }
                if (/*result == null ||*/ result.Equals("invalid command"))
                {
                    return "invalid command";
                }
                // read until end:
                if (!result.Equals("end"))
                {
                    Console.WriteLine("Result = {0}", result);

                    currentMessage = reader.ReadLine();
                   // result += currentMessage;
                    while (currentMessage!= null && !currentMessage.Equals("end"))
                    {
                        // Console.WriteLine(result);
                        // Get result from server
                        result += "\n" + currentMessage;
                        currentMessage = reader.ReadLine();
                       
                    }
                }
            }
            catch
            {
            }
            return result;
        }
    }
}