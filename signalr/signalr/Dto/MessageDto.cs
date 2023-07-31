namespace signalr.Dto
{
    public class MessageDto
    {
        public int Convid { get; set; }
        public string message { get; set; }
        public DateTime date { get;set; }
        public int writer { get; set; }
        public int receiver { get; set; }
    }
}
