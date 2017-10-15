using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using Roland.Data;
using Roland.Data.UnitOfWork;

namespace RolandDG.Tests.Data
{
    [TestFixture()]
    public class UnitOfWorkTests
    {
        [Test]
        public void Controller_ShouldThrowsArgumentNullException_WhenPassedParametersAreNull()
        {
            var mockedDbContext = new Mock<MsSqlDbContext>();

            //Act and Assert
            Assert.Throws<ArgumentNullException>(() => new UnitOfWork(null));
        }

        [Test]
        public void Controller_ShouldNotThrowArgumentNullException_WhenPassedParametersAreNull()
        {
            var mockedDbContext = new Mock<MsSqlDbContext>();

            //Act and Assert
            Assert.DoesNotThrow(() => new UnitOfWork(mockedDbContext.Object));
        }

        [Test]
        public void Commit_ShouldCallSaveChangesToDatabaseContext()
        {
            // Arrange
            var mockedDbContext = new Mock<MsSqlDbContext>();
            mockedDbContext.Setup(x => x.SaveChanges());

            var saveContext = new UnitOfWork(mockedDbContext.Object);

            // Act
            saveContext.Commit();

            // Assert
            mockedDbContext.Verify(dbc => dbc.SaveChanges(), Times.Once);
        }
    }
}
