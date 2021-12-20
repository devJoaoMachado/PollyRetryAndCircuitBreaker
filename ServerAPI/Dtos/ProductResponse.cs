namespace ServerAPI.Model
{
    public record ProductResponse
    {
        public int Id { get; init; }
        public string Name { get; init; }
        public decimal Price { get; init; }
    }
}
