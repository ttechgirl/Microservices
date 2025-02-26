namespace CatalogService
{
    public record ItemsDto(Guid Id, string? Name, decimal Price, string? Description, DateTimeOffset Date);

}