namespace TMS.ViewModel
{
    internal class City
    {
        public string Destination { get; set; }
        public int KMs { get; set; }
        public double Time { get; set; }
        public string West { get; set; }
        public string East { get; set; }

        public City(string destination, int kMs, double time, string west, string east)
        {
            Destination = destination;
            KMs = kMs;
            Time = time;
            West = west;
            East = east;
        }
    }
}
