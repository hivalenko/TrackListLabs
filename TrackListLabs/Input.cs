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

        public static string GetParameter(string message, string invalidCaseMessage, string symbolsToExclude)
        {
            String[] input = GetParameter(message, invalidCaseMessage).Split(symbolsToExclude);
            if (input.Length > 1)
            {
                throw new InvalidInputException(invalidCaseMessage);
            }

            return input[0];
        }

        public static string GetParameter(string message, string invalidCaseMessage)
        {
            string parameter = ReadString(message);
            parameter = parameter.Trim(' ');
            if (parameter == "")
            {
                throw new InvalidInputException(invalidCaseMessage);
            }

            return parameter;

        }

        public static string[] GetFewParametersFromLine(string separator, string message, int paramCounts)
        {
            string[] info = new string[paramCounts];
            
            try
            {
                info = GetParameter(message + ",separated with " + separator,
                                    "This input can not be empty")
                    .Split(separator.ToCharArray());
                if (info.Length < 2)
                {
                    throw new InvalidInputException();
                }
            }
            catch (Exception)
            {
                throw new InvalidInputException("Invalid format of input");
            }

            int counter = 0;
            
            foreach (string param in info)
            {
                if (info[counter] == "")
                {
                    throw new InvalidInputException("This parameters can not be empty.");
                }
                counter++;
            }

            return info;
        }
        
        public static string GetCommand() => ReadString("Please, enter command.");
        
    }
}