namespace ScrapingTitles
{
    public class Card
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public Card(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}