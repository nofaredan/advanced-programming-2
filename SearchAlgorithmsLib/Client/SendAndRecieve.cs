﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
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
        public static bool RecieveInfo(StreamReader reader)
        {
            try
            {
                string result = reader.ReadLine();
                if (result == null || result.Equals("invalid command"))
                {
                    return false;
                }
                // read until end:
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
            }
            return true;
        }
    }
}