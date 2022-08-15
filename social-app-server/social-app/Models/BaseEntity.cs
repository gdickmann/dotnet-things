namespace social_app.Api.Models
{
    public abstract class BaseEntity
    {
        Guid Id { get; set; }
        DateTime CreatedTime { get; set; }
    }
}
