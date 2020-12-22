namespace InfluencePWA.Data
{
    public class PrincipleDTO
    {
        public PrincipleDTO() { }
        public int Id { get; set; }
        public string Law { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int PrincipleTypeId { get; set; }
        public string PrincipleTypeName { get; set; }
    }
}
