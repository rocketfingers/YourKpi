namespace YouKpiBackend.ModelsEntity
{
    public partial class ProcessArea
    {
        public int Id { get; set; }
        public int? ProcessAreaId{ get; set; }
        public int Group { get; set; }
        public string Nazwa { get; set; }
        public string NazwaEn { get; set; }
        public int ProcessLevel { get; set; }
    }
}
