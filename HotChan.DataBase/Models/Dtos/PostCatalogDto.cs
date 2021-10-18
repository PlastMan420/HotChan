using System;

namespace HotChan.DataBase.Models.Dtos
{
    public class PostCatalogDto
    {
        public Guid Id {  get; set; }
        public string Name {  get; set; }
        public Uri ThumbnailUrl {  get; set; }
    }
}
