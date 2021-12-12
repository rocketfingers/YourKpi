namespace YouKpiBackend.ModelsEntity
{
    public partial class ProcessSubject
    {
        public int Id { get; set; }
        public int? SubjectAreaId { get; set; }
        public string Nazwa { get; set; }
        public string NazwaEn { get; set; }
        public int ProcessLevel { get; set; }
    }
}
