using System;
using System.Threading.Tasks;
using TrackListLabs;


namespace TrackListClient
{
    public class TrackListMenuClient
    {
        
        public Menu Menu { get; private set; }
        public String Separator { get; private set; }

        public TrackListMenuClient(String separator)
        {
            Separator = separator;
            Menu = new Menu()
                .Add("add", Add)
                .Add("delete", Delete)
                .Add("search", Search)
                .Add("quit", Quit)
                .Add("list", List)
                .Add("help", Help)
                .Add("save-to-pls", SaveTrackListToPls)
                .Add("load-from-pls", LoadTrackListFromPls);
        }

        private void List()
        {
            Send("list");
        }

        private void Add()
        {
            string author = Input.GetParameter("Please, enter author's name. Author's name should not contain '-' symbol",
                "There can not be author with such name","-");
            
            
            string trackName = Input.GetParameter("Enter name of the track",
                "Invalid name of track");

            string trackFilename = Input.GetParameter("Enter fullFilename",
                "Filepath can not be empty");
            Send("add", author, trackName, trackFilename);
        }

        private void Delete()
        {
            Track suitableTrack = null;


            String input =
                Input.GetParameter("Please, enter full author's name and track's name, separated with","invalid input");
            if (!input.Contains(Separator))
            {
                throw new InvalidInputException("invalid input");
            }
            Send("delete", input);

        }

        private void Search()
        {

            String information = Input.GetParameter(("Please enter part of Author's name, part of track name or both of them, separated with" + Separator),
                "Input can not be empty. Next time enter in appropriate form.");
            
            Send("search", information);

        }

        private void Quit()
        {
            Menu.AppIsRunning = false;
        }

        private void Help()
        {
            Menu.Display();
        }

        private void SaveTrackListToPls()
        {
            String filePath = Input.GetParameter("Please enter full path to pls file",
                "Filepath can't be empty");
            
            Send("save", filePath);
        }

        private void LoadTrackListFromPls()
        {
            String filePath = Input.GetParameter("Please enter full path to file",
                "Filepath can't be empty");
            
            Send("load", filePath);

        }


        private void Send(string method, params string[] lines)
        {
            Task<string> http = HttpPostClient.Exec("/" + method, HttpPostClient.ConstructXml(lines));
            http.Wait();
        }
    }
}