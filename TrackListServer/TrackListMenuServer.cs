using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using TrackListLabs;

namespace TrackListServer
{
    public class TrackListMenuServer
    {
        public Menu Menu { get; private set; }
        public List<Track> TrackList { get; private set; }
        public String Separator { get; private set; }

        public TrackListMenuServer(List<Track> trackList, String separator)
        {
            TrackList = trackList;
            Separator = separator;
            Menu = new Menu()
                .Add("add", Add)
                .Add("delete", Delete)
                .Add("search", Search)
                .Add("list", List)
                .Add("save-to-pls", SaveTrackListToPls)
                .Add("load-from-pls", LoadTrackListFromPls);

        }

        private void List()
        {
            Client client;
            InputServer.ClientQueue.TryDequeue(out client);
            try
            {
                
                
                foreach (Track track in TrackList)
                {
                    OutputServer.WriteLine(client, "Author/name = " + track.Author + Separator + track.Name);
                    OutputServer.WriteLine(client, "Filepath  = " + track.FilePath);
                }
            }
            catch (InvalidInputException e)
            {
                client.AddOutput(e.Message);
            }

            client.Close();
        }

        private void Add()
        {
            Client client;
            InputServer.ClientQueue.TryDequeue(out client);
            try
            {
                string author = InputServer.GetParameter(
                    "Please, enter author's name. Author's name should not contain '-' symbol",
                    "There can not be author with such name", "-");


                string trackName = InputServer.GetParameter("Enter name of the track",
                    "Invalid name of track");

                string trackFilename = InputServer.GetParameter("Enter fullFilename",
                    "Filepath can not be empty");
                TrackList.Add(new Track(author, trackName, trackFilename));
            }
            catch (InvalidInputException e)
            {
                client.AddOutput(e.Message);
            }

            client.Close();

        }

        private void Delete()
        {
            Client client;
            InputServer.ClientQueue.TryDequeue(out client);
            Track suitableTrack = null;

            try
            {
                String[] input =
                    InputServer.GetFewParametersFromLine("/", "Please, enter full author's name and track's name", 2);

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
            catch (InvalidInputException e)
            {
                client.AddOutput(e.Message);
            }

            client.Close();

        }

        private void Search()
        {
            Client client;
            InputServer.ClientQueue.TryDequeue(out client);
            try
            {
                String information = InputServer.GetParameter(
                    ("Please enter part of Author's name, part of track name or both of them, separated with" +
                     Separator),
                    "InputServer can not be empty. Next time enter in appropriate form.");
                foreach (Track track in TrackList.FindAll(track => track.Includes(information, Separator)))
                {
                    OutputServer.WriteLine(client, track.Author + Separator + track.Name);
                }
            }
            catch (InvalidInputException e)
            {
                client.AddOutput(e.Message);
            }

            client.Close();

        }

  

        private void SaveTrackListToPls()
        {
            Client client;
            InputServer.ClientQueue.TryDequeue(out client);
            try
            {
                String filePath = InputServer.GetParameter("Please enter full path to pls file",
                    "Filepath can't be empty");
                Console.WriteLine(filePath);
                TrackListSerializer.saveToPls(filePath, TrackList);
            }
            catch (InvalidInputException e)
            {
                client.AddOutput(e.Message);
            }

            client.Close();

        }

        private void LoadTrackListFromPls()
        {
            Client client;
            InputServer.ClientQueue.TryDequeue(out client);
            try
            {
                String filePath = InputServer.GetParameter("Please enter full path to file",
                    "Filepath can't be empty");
                TrackListSerializer.loadFromPls(filePath, TrackList);
            }
            catch (InvalidInputException e)
            {
                client.AddOutput(e.Message);
            }

            client.Close();

        }
    }
}