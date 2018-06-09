using System;
using System.Collections.Generic;
using TrackListLabs;

namespace TrackListServer
{
    public class Menu
    {
        public bool AppIsRunning { get; set; }
        public List<Option> Options { get; private set; }

        public Menu()
        {
            Options = new List<Option>();
            AppIsRunning = true;
        }

        public Menu Add(string option, Action callback)
        {
            return Add(new Option(option, callback));
        }
        public Menu Add(Option option)
        {
            Options.Add(option);
            return this;
        }

        public void Display()
        {
            for (int i = 0; i < Options.Count; i++)
            {
                Output.WriteLine(Options[i].Name);
            }
        }

        public void ChooseCommandFromOptions()
        {
            string command = InputServer.GetCommand();
            
            foreach (Option option in Options)
            {
                if (option.Name.Equals(command.ToLower()))
                {
                    option.Callback();
                    return;
                }
            }

            Console.WriteLine("\"" + command + "\"");
            
        }
    }
}