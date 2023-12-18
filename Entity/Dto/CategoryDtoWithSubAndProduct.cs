using System;
using Core;

namespace Entity.Dto
{
    public class CategoryDtoWithSubAndProduct : IDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int OrderBy { get; set; }
        public string ImageUrl { get; set; }

        public List<SubCategoryDto> SubCategories { get; set; }
        public List<ProductSimpleDto> Products { get; set; }
    }

}

