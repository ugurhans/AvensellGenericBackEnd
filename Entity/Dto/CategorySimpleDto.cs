using System;
using Core;

namespace Entity.Dto
{
    public class CategorySimpleDto : IDto
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public int OrderBy { get; set; }
        public string ImageUrl { get; set; }
    }
}

