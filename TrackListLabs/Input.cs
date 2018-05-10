using System;

namespace TrackListLabs
{
    public class Input
    {

        public static string ReadString() => Console.ReadLine();

        public static string ReadString(string message)
        {
            Output.WriteLine(message);
            return Console.ReadLine();
        }

        public static string GetCommand() => ReadString("Please, enter command.");
        
    }
}