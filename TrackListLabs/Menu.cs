using System;
using System.Collections.Generic;

namespace TrackListLabs
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
            Output.WriteLine("Possible commands");
            for (int i = 0; i < Options.Count; i++)
            {
                Output.WriteLine(Options[i].Name);
            }
        }

        public void ChooseCommandFromOptions()
        {
            string command = Input.GetCommand();

            foreach (Option option in Options)
            {
                if (option.Name.Equals(command.ToLower()))
                {
                    option.Callback();
                    Output.WriteLine("command " + option.Name + " executed");
                    Output.WriteLine("======================");
                    return;
                }
            }
            throw new InvalidInputException("Unknown command");
        }
    }
}