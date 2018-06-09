namespace TrackListServer
{
    public class OutputServer
    {

        public static void WriteLine(Client client, string line)
        {
            client.AddOutput(line);
        }
    }
}