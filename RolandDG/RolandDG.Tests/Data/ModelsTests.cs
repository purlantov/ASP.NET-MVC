using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Roland.Data.Model;

namespace RolandDG.Tests.Data
{
    [TestFixture]
    public class ModelsTests
    {
        public void ImpactPrinterShould_ReturnCorrectValueForModel()
        {
            var impactPrinter = new ImpactPrinter()
            {
                Model = "MPX-90",
                Resolution = 600
            };

            Assert.That(impactPrinter.Model == "MPX-90");
        }

        public void ImpactPrinterShould_ReturnCorrectValueForResolution()
        {
            var impactPrinter = new ImpactPrinter()
            {
                Model = "MPX-90",
                Resolution = 600
            };

            Assert.That(impactPrinter.Resolution == 600);
        }

        public void ImpactPrinterShould_SetCorrectValueForResolution()
        {
            var impactPrinter = new ImpactPrinter()
            {
                Model = "MPX-90",
                Resolution = 600
            };

            Assert.That(impactPrinter.Resolution == 600);
        }
    }
}
