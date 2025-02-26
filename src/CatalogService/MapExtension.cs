using CatalogService.Models;

namespace CatalogService
{
    public static class MapExtension
    {
        public static ItemsDto DtoMap(this Items item)
        {
            return new ItemsDto(
                item.Id,
                item.Name,
                item.Price,
                item.Description,
                item.CreatedDate

            );
        }

        public static ItemsDto UpdateMap(this UpdateItemsRequest item)
        {
            return new ItemsDto(
                item.Id,
                item.Name,
                item.Price,
                item.Description,
                DateTimeOffset.Now

            );
        }


    }
}
