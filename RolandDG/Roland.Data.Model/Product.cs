using Roland.Data.Model.Abstracts;

namespace Roland.Data.Model
{
    public class Product : DataModel
    {
        public string Category { get; set; }

        public decimal Price { get; set; }
    }
}