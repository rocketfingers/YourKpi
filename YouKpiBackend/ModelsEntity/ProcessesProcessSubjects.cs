namespace YouKpiBackend.ModelsEntity
{
    public partial class ProcessesProcessSubjects
    {
        public int Id { get; set; }
        public string ProcessId { get; set; }
        public int ProcessSubjectId { get; set; }

        public virtual Process Process { get; set; }
        public virtual ProcessSubject ProcessSubject { get; set; }
    }
}
