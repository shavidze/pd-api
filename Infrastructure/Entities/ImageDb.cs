using System;

namespace Infrastructure.Entities
{
    public class ImageDb : BaseEntity
    {
        public Guid ImageId { get; set; }

        public string ImageName { get; set; }

        public string ImageType { get; set; }

        public string FullPath { get; set; }
    }
}
