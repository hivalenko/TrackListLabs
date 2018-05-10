using System;
using System.Collections.Generic;

namespace TrackListLabs
{
    public class TrackListMenu
    {
        public Menu Menu { get; private set; }
        public List<Track> TrackList;
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
            String author = Input.ReadString("Please, enter author's name");
            author = author.Trim(' ');
            if (author == "")
            {
                throw new InvalidInputException("There can not be author with such name.");
            }

            String trackName = Input.ReadString("Enter name of track");
            trackName = trackName.Trim(' ');
            if (trackName == "")
            {
                throw new InvalidInputException("There can't be track with such name");
            }
            TrackList.Add(new Track(author, trackName));
        }

        private void Delete()
        {
            Track suitableTrack = null;
            String[] info;

            try
            {
                info = Input.ReadString("You shold enter full author's" +
                                        " name and track's name" +
                                        " separated with " + Separator)
                    .Split(Separator.ToCharArray());
            }
            catch (Exception)
            {
                throw new InvalidInputException("Threre can't be author with such name!");
            }

            String author = info[0];
            String name = info[1];

            if (author == "" || name == "")
            {
                throw new InvalidInputException("There can't be author with such name!");
            }
            
            foreach (Track track in TrackList)
            {
                if (track.Author == author && track.Name == name)
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
            String information = Input.ReadString("Please enter part of Author's name," +
                                                  " part of track name or both of them," +
                                                  " separated with" + Separator);
            information = information.Trim(' ');
            if (information == "")
            {
                throw new InvalidInputException("Input can not be empty. Next time enter in appropriate form.");
            }

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