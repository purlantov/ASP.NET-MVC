using Roland.Data.Model;

namespace RolandDG.Web.ViewModels.Product
{
    public interface IProductViewModel
    {
        string Model { get; set; }

        ProductType ProductType { get; set; }

    }
}