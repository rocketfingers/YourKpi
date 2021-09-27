namespace YouKpiBackend.ModelsEntity
{
    public class ProduktyRysunki
    {
        public int Id { get; set; }
        public byte[] Base64FileContent { get; set; }
        public string Rozszerzenie { get; set; }

        public virtual ProduktyRysunkiInfo ProduktyRysunkiInfo { get; set; }
    }
}
