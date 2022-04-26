namespace models
{
    public class Message
    {
        public int id { get; set; }
        public string text { get; set; }
        public int trigger { get; set; }
        public int time { get; set; }
        
        public Message(int id, string text, int trigger, int time)
        {
            this.id = id;
            this.text = text;
            this.trigger = trigger;
            this.time = time;
        }
    }
}