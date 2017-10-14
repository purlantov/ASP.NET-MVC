using System.Collections.Generic;
using RolandDG.Web.Infrastructure;

namespace RolandDG.Web.ViewModels.Product
{
    public class ProductViewModel
    {
        public ICollection<PrinterViewModel> PrintersCollection { get; set; }
        public ICollection<ImpactPrinterViewModel> ImpactPrintersCollection { get; set; }
        public ICollection<EngraverViewModel> EngraversCollection { get; set; }
        public ICollection<VinylCutterViewModel> VinylCuttersCollection { get; set; }
    }
}