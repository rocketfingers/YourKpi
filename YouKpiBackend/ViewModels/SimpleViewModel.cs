namespace YouKpiBackend.ViewModels
{
    public class SimpleViewModel
    {
        public SimpleViewModel(string id, string name)
        {
            Id = id;
            Name = name;
        }

        public string Id { get; set; }
        public string Name { get; set; }
    }
}
