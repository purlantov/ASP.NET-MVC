using Roland_ASP_MVC.Infrastructure;

namespace Roland_ASP_MVC.ViewModels.Product
{
    public class ProductViewModel : IMapFrom<Roland.Data.Model.Product>
    {
        public string Category { get; set; }

        public decimal Price { get; set; }

        //[Index]
        //public Guid Id { get; set; }

        //[Required]
        //[MinLength(4)]
        //[MaxLength(30)]
        //public string Model { get; set; }

        //[Required]
        //public ProductType ProductType { get; set; }

        //public void CreateMappings(IMapperConfigurationExpression configuration)
        //{
        //    configuration.CreateMap<Printer, PrinterViewModel>().
        //        ForMember(viewModel => viewModel.Model, cfg => cfg.MapFrom(model => model.Model));
        //}
    }
}