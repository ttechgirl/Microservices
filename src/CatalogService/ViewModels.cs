using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace CatalogService
{
    public record CreateItemsRequest([Required] string Name, [Range(1, 100000)] decimal Price, string Description);
    public record UpdateItemsRequest(Guid Id, [Required] string Name, [Range(1, 100000)] decimal Price, string Description);
}