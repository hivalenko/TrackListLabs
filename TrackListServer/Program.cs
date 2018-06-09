using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TrackListLabs;

namespace TrackListServer
{
    class Program
    {
        public static bool IsRunning { get; set; }

        static void Main(string[] args)
        {
            IsRunning = true;
            List<Track> trackList = new List<Track>();
            
            Task.Factory.StartNew(() => new HttpServer(8080));
            
            TrackListMenuServer tarTrackListMenu = new TrackListMenuServer(trackList, "%");
            
            
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