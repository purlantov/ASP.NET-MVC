﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using Roland.Data.Model;
using Roland.Data.Repositories;
using Roland.Data.UnitOfWork;
using RolandDG.Services;

namespace RolandDG.Tests.Services
{
    [TestFixture]
    public class ImpactPrintersServiceTests
    {
        [Test]
        public void Service_ShouldThrowArgumentNullException_WhenEfRepositoryIsNull()
        {
            // Arrange
            var mockedEfRepository = new Mock<IEfRepository<ImpactPrinter>>();
            var mockedUnitOfWork = new Mock<IUnitOfWork>();

            //Act and Assert
            Assert.Throws<ArgumentNullException>(() => new ImpactPrintersService(null, mockedUnitOfWork.Object));
        }

        [Test]
        public void Service_ShouldThrowArgumentNullException_WhenUnitOfWorkIsNull()
        {
            // Arrange
            var mockedEfRepository = new Mock<IEfRepository<ImpactPrinter>>();
            var mockedUnitOfWork = new Mock<IUnitOfWork>();

            //Act and Assert
            Assert.Throws<ArgumentNullException>(() => new ImpactPrintersService(mockedEfRepository.Object, null));
        }

        [Test]
        public void GetAll_ShouldReturnAllPrintersFromDatabase_WhickAreNotDeleted()
        {
            // Arrange
            var mockedEfRepository = new Mock<IEfRepository<ImpactPrinter>>();
            var mockedUnitOfWork = new Mock<IUnitOfWork>();

            mockedEfRepository.Setup(x => x.All);

            var service = new ImpactPrintersService(mockedEfRepository.Object, mockedUnitOfWork.Object);

            // Act
            var result = service.GetAll();

            // Assert
            mockedEfRepository.Verify(x => x.All, Times.Once);
        }

        [Test]
        public void UpdateComputer_ShouldUpdatePrinterInDatabase()
        {
            // Arrange
            var mockedEfRepository = new Mock<IEfRepository<ImpactPrinter>>();
            var mockedUnitOfWork = new Mock<IUnitOfWork>();
            var printer = new ImpactPrinter();


            mockedEfRepository.Setup(x => x.Update(printer));

            var service = new ImpactPrintersService(mockedEfRepository.Object, mockedUnitOfWork.Object);


            // Act
            service.Update(printer);

            // Assert
            mockedEfRepository.Verify(x => x.Update(printer), Times.Once);
        }

        [Test]
        public void AddComputer_ShouldAddPrinterToDatabase()
        {
            // Arrange
            var mockedEfRepository = new Mock<IEfRepository<ImpactPrinter>>();
            var mockedUnitOfWork = new Mock<IUnitOfWork>();
            var computer = new ImpactPrinter();

            mockedEfRepository.Setup(x => x.Add(computer));

            var service = new ImpactPrintersService(mockedEfRepository.Object, mockedUnitOfWork.Object);


            // Act
            service.Add(computer);

            // Assert
            mockedEfRepository.Verify(x => x.Add(computer), Times.Once);
        }

        [Test]
        public void DeletePrinter_ShouldDeletePrinterInDatabase()
        {
            // Arrange
            var mockedEfRepository = new Mock<IEfRepository<ImpactPrinter>>();
            var mockedUnitOfWork = new Mock<IUnitOfWork>();

            var computer = new ImpactPrinter();

            mockedEfRepository.Setup(x => x.Delete(computer));

            var service = new ImpactPrintersService(mockedEfRepository.Object, mockedUnitOfWork.Object);

            // Act
            service.Delete(computer);

            // Assert
            mockedEfRepository.Verify(x => x.Delete(computer), Times.Once);
        }
    }
}
