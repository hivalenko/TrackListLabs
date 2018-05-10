using System;
using System.Collections.Generic;

namespace TrackListLabs
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Track> trackList = new List<Track>();
            TrackListMenu tarTrackListMenu = new TrackListMenu(trackList, "/");
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