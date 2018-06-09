using System;
using System.Security.Cryptography.X509Certificates;
using TrackListLabs;

namespace TrackListClient
{
    class Program
    {
        public static String Ipport { get; private set; }
        
        static void Main(string[] args)
        {
            if (args.Length != 1 || args[0].IndexOf('=') < 0)
            {
                Console.WriteLine("point server address");
                return;
            }
            Ipport = "http://" + args[0].Split('=')[1];
            
            TrackListMenuClient tarTrackListMenu = new TrackListMenuClient("/");
            tarTrackListMenu.Menu.Display();

            while (tarTrackListMenu.Menu.AppIsRunning)
            {
                try
                {
                    tarTrackListMenu.Menu.ChooseCommandFromOptions();
                }
                catch (InvalidInputException e)
                {
                    Output.WriteLine(e.Message);
                }
                catch (Exception e)
                {
                    Output.WriteLine(e.Message);
                }
            }
        }
    }
}