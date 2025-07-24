namespace Producer.API
{
    public class Settings
    {
        public string SomeProp { get; set; }

        public Dictionary<string, IConfigurationSection> Sections { get; set; }
    }
}
