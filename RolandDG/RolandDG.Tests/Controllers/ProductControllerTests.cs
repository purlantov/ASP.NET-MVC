using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Moq;
using NUnit.Framework;
using Roland.Data.Model;
using RolandDG.Services.Contracts;
using RolandDG.Web.Controllers;
using RolandDG.Web.Providers.Contracts;
using RolandDG.Web.ViewModels.Product;
using TestStack.FluentMVCTesting;

namespace RolandDG.Tests.Controllers
{
    [TestFixture]
    public class ProductControllerTests
    {
        [Test]
        public void Controller_ShouldThrowArgumentNullException_WhenProviderIsNull()
        {
            //Arrange
            var mockedProvider = new Mock<IVerificationProvider>();
            var mockedMapper = new Mock<IMapper>();
            var mockedUsersService = new Mock<IUsersService>();
            var mockedPrintersService = new Mock<IPrintersService>();
            var mockedVinylCuttersService = new Mock<IVinylCuttersService>();
            var mockedEngraversService = new Mock<IEngraversService>();
            var mockedImpacktPrintersService = new Mock<IImpactPrintersService>();

            Assert.Throws<ArgumentNullException>(() =>
                new ProductController(mockedMapper.Object, mockedPrintersService.Object, mockedImpacktPrintersService.Object, mockedEngraversService.Object,
                    mockedVinylCuttersService.Object, null));
        }

        [Test]
        public void Controller_ShouldThrowArgumentNullException_WhenMapperIsNull()
        {
            //Arrange
            var mockedProvider = new Mock<IVerificationProvider>();
            var mockedMapper = new Mock<IMapper>();
            var mockedUsersService = new Mock<IUsersService>();
            var mockedPrintersService = new Mock<IPrintersService>();
            var mockedVinylCuttersService = new Mock<IVinylCuttersService>();
            var mockedEngraversService = new Mock<IEngraversService>();
            var mockedImpacktPrintersService = new Mock<IImpactPrintersService>();

            Assert.Throws<ArgumentNullException>(() =>
                new ProductController(null, mockedPrintersService.Object, mockedImpacktPrintersService.Object, mockedEngraversService.Object,
                    mockedVinylCuttersService.Object, mockedProvider.Object));
        }

        [Test]
        public void Controller_ShouldThrowArgumentNullException_WhenPrinterIsNull()
        {
            //Arrange
            var mockedProvider = new Mock<IVerificationProvider>();
            var mockedMapper = new Mock<IMapper>();
            var mockedUsersService = new Mock<IUsersService>();
            var mockedPrintersService = new Mock<IPrintersService>();
            var mockedVinylCuttersService = new Mock<IVinylCuttersService>();
            var mockedEngraversService = new Mock<IEngraversService>();
            var mockedImpacktPrintersService = new Mock<IImpactPrintersService>();

            Assert.Throws<ArgumentNullException>(() =>
                new ProductController(mockedMapper.Object, null, mockedImpacktPrintersService.Object, mockedEngraversService.Object,
                    mockedVinylCuttersService.Object, mockedProvider.Object));
        }

        [Test]
        public void Controller_ShouldThrowArgumentNullException_WhenImpactPrinterIsNull()
        {
            //Arrange
            var mockedProvider = new Mock<IVerificationProvider>();
            var mockedMapper = new Mock<IMapper>();
            var mockedUsersService = new Mock<IUsersService>();
            var mockedPrintersService = new Mock<IPrintersService>();
            var mockedVinylCuttersService = new Mock<IVinylCuttersService>();
            var mockedEngraversService = new Mock<IEngraversService>();
            var mockedImpacktPrintersService = new Mock<IImpactPrintersService>();

            Assert.Throws<ArgumentNullException>(() =>
                new ProductController(mockedMapper.Object, mockedPrintersService.Object, null, mockedEngraversService.Object,
                    mockedVinylCuttersService.Object, mockedProvider.Object));
        }

        [Test]
        public void Controller_ShouldThrowArgumentNullException_WhenEngraverIsNull()
        {
            //Arrange
            var mockedProvider = new Mock<IVerificationProvider>();
            var mockedMapper = new Mock<IMapper>();
            var mockedUsersService = new Mock<IUsersService>();
            var mockedPrintersService = new Mock<IPrintersService>();
            var mockedVinylCuttersService = new Mock<IVinylCuttersService>();
            var mockedEngraversService = new Mock<IEngraversService>();
            var mockedImpacktPrintersService = new Mock<IImpactPrintersService>();

            Assert.Throws<ArgumentNullException>(() =>
                new ProductController(mockedMapper.Object, mockedPrintersService.Object,
                mockedImpacktPrintersService.Object, null,
                    mockedVinylCuttersService.Object, mockedProvider.Object));
        }

        [Test]
        public void Controller_ShouldThrowArgumentNullException_WhenCutterIsNull()
        {
            //Arrange
            var mockedProvider = new Mock<IVerificationProvider>();
            var mockedMapper = new Mock<IMapper>();
            var mockedUsersService = new Mock<IUsersService>();
            var mockedPrintersService = new Mock<IPrintersService>();
            var mockedVinylCuttersService = new Mock<IVinylCuttersService>();
            var mockedEngraversService = new Mock<IEngraversService>();
            var mockedImpacktPrintersService = new Mock<IImpactPrintersService>();

            Assert.Throws<ArgumentNullException>(() =>
                new ProductController(mockedMapper.Object, mockedPrintersService.Object,
                    mockedImpacktPrintersService.Object, mockedEngraversService.Object,
                    null, mockedProvider.Object));
        }

        [Test]
        public void Printers_ShouldReturnsTrue_WhenViewResult_IsValid()
        {
            // Arrange
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Printer, PrinterViewModel>();
                cfg.CreateMap<PrinterViewModel, Printer>();
            });

            var mockedProvider = new Mock<IVerificationProvider>();
            var mockedMapper = new Mock<IMapper>();
            var mockedUsersService = new Mock<IUsersService>();
            var mockedPrintersService = new Mock<IPrintersService>();
            var mockedVinylCuttersService = new Mock<IVinylCuttersService>();
            var mockedEngraversService = new Mock<IEngraversService>();
            var mockedImpacktPrintersService = new Mock<IImpactPrintersService>();

            var controller = new ProductController(mockedMapper.Object, mockedPrintersService.Object,
                mockedImpacktPrintersService.Object, mockedEngraversService.Object,
                mockedVinylCuttersService.Object, mockedProvider.Object);

            //Act and Assert
            controller
                .WithCallTo(c => c.Printers(1))
                .ShouldRenderView("Printers");
        }

        [Test]
        public void Engravers_ShouldReturnsTrue_WhenViewResult_IsValid()
        {
            // Arrange
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Engraver, EngraverViewModel>();
                cfg.CreateMap<EngraverViewModel, Engraver>();
            });

            var mockedProvider = new Mock<IVerificationProvider>();
            var mockedMapper = new Mock<IMapper>();
            var mockedUsersService = new Mock<IUsersService>();
            var mockedPrintersService = new Mock<IPrintersService>();
            var mockedVinylCuttersService = new Mock<IVinylCuttersService>();
            var mockedEngraversService = new Mock<IEngraversService>();
            var mockedImpacktPrintersService = new Mock<IImpactPrintersService>();

            var controller = new ProductController(mockedMapper.Object, mockedPrintersService.Object,
                mockedImpacktPrintersService.Object, mockedEngraversService.Object,
                mockedVinylCuttersService.Object, mockedProvider.Object);

            //Act and Assert
            controller
                .WithCallTo(c => c.Engravers(1))
                .ShouldRenderView("Engravers");
        }
        [Test]
        public void ImpactPrinters_ShouldReturnsTrue_WhenViewResult_IsValid()
        {
            // Arrange
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<ImpactPrinter, ImpactPrinterViewModel>();
                cfg.CreateMap<ImpactPrinterViewModel, ImpactPrinter>();
            });

            var mockedProvider = new Mock<IVerificationProvider>();
            var mockedMapper = new Mock<IMapper>();
            var mockedUsersService = new Mock<IUsersService>();
            var mockedPrintersService = new Mock<IPrintersService>();
            var mockedVinylCuttersService = new Mock<IVinylCuttersService>();
            var mockedEngraversService = new Mock<IEngraversService>();
            var mockedImpacktPrintersService = new Mock<IImpactPrintersService>();

            var controller = new ProductController(mockedMapper.Object, mockedPrintersService.Object,
                mockedImpacktPrintersService.Object, mockedEngraversService.Object,
                mockedVinylCuttersService.Object, mockedProvider.Object);

            //Act and Assert
            controller
                .WithCallTo(c => c.ImpactPrinters(1))
                .ShouldRenderView("ImpactPrinters");
        }

        [Test]
        public void VinylCutters_ShouldReturnsTrue_WhenViewResult_IsValid()
        {
            // Arrange
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<VinylCutter, VinylCutterViewModel>();
                cfg.CreateMap<VinylCutterViewModel, VinylCutter>();
            });

            var mockedProvider = new Mock<IVerificationProvider>();
            var mockedMapper = new Mock<IMapper>();
            var mockedUsersService = new Mock<IUsersService>();
            var mockedPrintersService = new Mock<IPrintersService>();
            var mockedVinylCuttersService = new Mock<IVinylCuttersService>();
            var mockedEngraversService = new Mock<IEngraversService>();
            var mockedImpacktPrintersService = new Mock<IImpactPrintersService>();

            var controller = new ProductController(mockedMapper.Object, mockedPrintersService.Object,
                mockedImpacktPrintersService.Object, mockedEngraversService.Object,
                mockedVinylCuttersService.Object, mockedProvider.Object);

            //Act and Assert
            controller
                .WithCallTo(c => c.Cutters(1))
                .ShouldRenderView("Cutters");
        }

        [Test]
        public void Printer_ShouldReturnsTrue_WhenViewResult_IsValid()
        {
            // Arrange
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Printer, PrinterViewModel>();
                cfg.CreateMap<PrinterViewModel, Printer>();
            });

            var mockedProvider = new Mock<IVerificationProvider>();
            var mockedMapper = new Mock<IMapper>();
            var mockedUsersService = new Mock<IUsersService>();
            var mockedPrintersService = new Mock<IPrintersService>();
            var mockedVinylCuttersService = new Mock<IVinylCuttersService>();
            var mockedEngraversService = new Mock<IEngraversService>();
            var mockedImpacktPrintersService = new Mock<IImpactPrintersService>();

            var controller = new ProductController(mockedMapper.Object, mockedPrintersService.Object,
                mockedImpacktPrintersService.Object, mockedEngraversService.Object,
                mockedVinylCuttersService.Object, mockedProvider.Object);

            // Act
            var printer = new Printer
            {
                Id = Guid.NewGuid()
            };
            var printersCollection = new List<Printer>() { printer };

            mockedPrintersService.Setup(c => c.GetAll()).Returns(printersCollection.AsQueryable());


            //Assert
            controller
                .WithCallTo(c => c.Printer(printer.Id))
                .ShouldRenderView("Printer");
        }

        [Test]
        public void ImpactPrinter_ShouldReturnsTrue_WhenViewResult_IsValid()
        {
            // Arrange
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<ImpactPrinter, ImpactPrinterViewModel>();
                cfg.CreateMap<ImpactPrinterViewModel, ImpactPrinter>();
            });

            var mockedProvider = new Mock<IVerificationProvider>();
            var mockedMapper = new Mock<IMapper>();
            var mockedUsersService = new Mock<IUsersService>();
            var mockedPrintersService = new Mock<IPrintersService>();
            var mockedVinylCuttersService = new Mock<IVinylCuttersService>();
            var mockedEngraversService = new Mock<IEngraversService>();
            var mockedImpacktPrintersService = new Mock<IImpactPrintersService>();

            var controller = new ProductController(mockedMapper.Object, mockedPrintersService.Object,
                mockedImpacktPrintersService.Object, mockedEngraversService.Object,
                mockedVinylCuttersService.Object, mockedProvider.Object);

            // Act
            var impactPrinter = new ImpactPrinter
            {
                Id = Guid.NewGuid()
            };
            var printersCollection = new List<ImpactPrinter>() { impactPrinter };

            mockedImpacktPrintersService.Setup(c => c.GetAll()).Returns(printersCollection.AsQueryable());


            //Assert
            controller
                .WithCallTo(c => c.ImpactPrinter(impactPrinter.Id))
                .ShouldRenderView("ImpactPrinter");
        }

        [Test]
        public void Index_ShouldReturnsTrue_WhenViewResult_IsValid()
        {
            // Arrange
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Engraver, EngraverViewModel>();
                cfg.CreateMap<EngraverViewModel, Engraver>();
            });

            var mockedProvider = new Mock<IVerificationProvider>();
            var mockedMapper = new Mock<IMapper>();
            var mockedUsersService = new Mock<IUsersService>();
            var mockedPrintersService = new Mock<IPrintersService>();
            var mockedVinylCuttersService = new Mock<IVinylCuttersService>();
            var mockedEngraversService = new Mock<IEngraversService>();
            var mockedImpacktPrintersService = new Mock<IImpactPrintersService>();

            var controller = new ProductController(mockedMapper.Object, mockedPrintersService.Object,
                mockedImpacktPrintersService.Object, mockedEngraversService.Object,
                mockedVinylCuttersService.Object, mockedProvider.Object);

            // Act
            var engraver = new Engraver
            {
                Id = Guid.NewGuid()
            };
            var engraversCollection = new List<Engraver>() { engraver };

            mockedEngraversService.Setup(c => c.GetAll()).Returns(engraversCollection.AsQueryable());


            //Assert
            controller
                .WithCallTo(c => c.Index())
                .ShouldRenderView("Index");
        }

        [Test]
        public void Engraver_ShouldReturnsTrue_WhenViewResult_IsValid()
        {
            // Arrange
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Engraver, EngraverViewModel>();
                cfg.CreateMap<EngraverViewModel, Engraver>();
            });

            var mockedProvider = new Mock<IVerificationProvider>();
            var mockedMapper = new Mock<IMapper>();
            var mockedUsersService = new Mock<IUsersService>();
            var mockedPrintersService = new Mock<IPrintersService>();
            var mockedVinylCuttersService = new Mock<IVinylCuttersService>();
            var mockedEngraversService = new Mock<IEngraversService>();
            var mockedImpacktPrintersService = new Mock<IImpactPrintersService>();

            var controller = new ProductController(mockedMapper.Object, mockedPrintersService.Object,
                mockedImpacktPrintersService.Object, mockedEngraversService.Object,
                mockedVinylCuttersService.Object, mockedProvider.Object);

            // Act
            var engraver = new Engraver
            {
                Id = Guid.NewGuid()
            };
            var engraversCollection = new List<Engraver>() { engraver };

            mockedEngraversService.Setup(c => c.GetAll()).Returns(engraversCollection.AsQueryable());


            //Assert
            controller
                .WithCallTo(c => c.Engraver(engraver.Id))
                .ShouldRenderView("Engraver");
        }

        [Test]
        public void VinylCutter_ShouldReturnsTrue_WhenViewResult_IsValid()
        {
            // Arrange
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<VinylCutter, VinylCutterViewModel>();
                cfg.CreateMap<VinylCutterViewModel, VinylCutter>();
            });

            var mockedProvider = new Mock<IVerificationProvider>();
            var mockedMapper = new Mock<IMapper>();
            var mockedUsersService = new Mock<IUsersService>();
            var mockedPrintersService = new Mock<IPrintersService>();
            var mockedVinylCuttersService = new Mock<IVinylCuttersService>();
            var mockedEngraversService = new Mock<IEngraversService>();
            var mockedImpacktPrintersService = new Mock<IImpactPrintersService>();

            var controller = new ProductController(mockedMapper.Object, mockedPrintersService.Object,
                mockedImpacktPrintersService.Object, mockedEngraversService.Object,
                mockedVinylCuttersService.Object, mockedProvider.Object);

            // Act
            var vinylCutter = new VinylCutter
            {
                Id = Guid.NewGuid()
            };
            var engraversCollection = new List<VinylCutter>() { vinylCutter };

            mockedVinylCuttersService.Setup(c => c.GetAll()).Returns(engraversCollection.AsQueryable());


            //Assert
            controller
                .WithCallTo(c => c.Cutter(vinylCutter.Id))
                .ShouldRenderView("Cutter");
        }
    }
}
