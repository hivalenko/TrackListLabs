using System;
using System.Collections.Generic;

namespace TrackListLabs
{
    public class TrackListMenu
    {
        public Menu Menu { get; private set; }
        public List<Track> TrackList { get; private set; }
        public String Separator { get; private set; }

        public TrackListMenu(List<Track> trackList, String separator)
        {
            TrackList = trackList;
            Separator = separator;
            Menu = new Menu()
                .Add("add", Add)
                .Add("delete", Delete)
                .Add("search", Search)
                .Add("quit", Quit)
                .Add("list", List)
                .Add("help", Help);

        }

        private void List()
        {
            foreach (Track track in TrackList)
            {
                Output.WriteLine(track.Author + Separator + track.Name);
            }
        }

        private void Add()
        {
            string author = Input.GetParameter("Please, enter author's name",
                                                      "There can not be author with such name");
            
            string trackName = Input.GetParameter("Enter name of the track",
                                                         "Invalid name of track");
            TrackList.Add(new Track(author, trackName));
        }

        private void Delete()
        {
            Track suitableTrack = null;


            String[] input =
                Input.GetFewParametersFromLine("/", "Please, enter full author's name and track's name", 2);

            foreach (Track track in TrackList)
            {
                if (track.Author == input[0] && track.Name == input[1])
                {
                    suitableTrack = track;
                }
            }

            if (suitableTrack != null)
            {
                TrackList.Remove(suitableTrack);
            }
            else
            {
                throw new InvalidInputException("There are no tracks with such fulll name.");
            }
        }

        private void Search()
        {

            String information = Input.GetParameter(("Please enter part of Author's name, part of track name or both of them, separated with" + Separator),
                                                            "Input can not be empty. Next time enter in appropriate form.");
            foreach (Track track in TrackList.FindAll(track => track.Includes(information, Separator)))
            {
                Output.WriteLine(track.Author + Separator + track.Name);
            }
        }

        private void Quit()
        {
            Menu.AppIsRunning = false;
        }

        private void Help()
        {
            Menu.Display();
        }
    }
}