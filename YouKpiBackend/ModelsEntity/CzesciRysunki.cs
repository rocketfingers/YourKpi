
namespace YouKpiBackend.ModelsEntity
{
    public partial class CzesciRysunki
    {
        public int Id { get; set; }
        public byte[] Base64FileContent { get; set; }
        public string Rozszerzenie { get; set; }
        public virtual CzesciRysunkiInfo CzesciRysunkiInfo { get; set; }
    }
}