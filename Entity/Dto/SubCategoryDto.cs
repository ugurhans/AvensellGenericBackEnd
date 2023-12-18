using System;
using Core;

namespace Entity.Dto
{
    public class SubCategoryDto : IDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int OrderBy { get; set; }
        public int CategoryId { get; set; }
        public List<ProductSimpleDto> Products { get; set; }
    }
}

