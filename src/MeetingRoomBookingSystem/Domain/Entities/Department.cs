namespace Domain.Entities
{
    public class Department : IEntity<Guid>
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;

    }
}
