namespace YouKpiBackend.ModelsEntity
{
    public partial class ProcessesProcessAreas
    {
        public int Id { get; set; }
        public string ProcessId { get; set; }
        public int ProcessAreaId{ get; set; }

        public virtual Process Process { get; set; }
        public virtual ProcessArea ProcessArea { get; set; }
    }
}
