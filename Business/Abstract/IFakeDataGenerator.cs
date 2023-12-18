using System;
using Entity.Concrate;

namespace Business.Abstract
{
    public interface IFakeDataGenerator
    {
        public List<Product> GenerateProducts(int count);

        public List<Category> GenerateCategories(int count);

    }
}

