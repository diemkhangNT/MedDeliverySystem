using NUnit.Framework;
using Pharmacy.Domain.DomainCommands;
using Pharmacy.Domain.Exceptions;

namespace Pharmacy.Domain.Tests
{
    public class GetAllPharmaciesTest
    {
        private List<CreatePharmacyDomainCommand> _listCreatePharmacyDomainCommand;

        [SetUp]
        public void Setup()
        {
            _listCreatePharmacyDomainCommand = new List<CreatePharmacyDomainCommand>()
            {
                new CreatePharmacyDomainCommand
                {
                    PharmacyId = 1,
                    PharmacyName = "Test True",
                    Address = new(
                        "457 Nguyen Dinh Chieu, Ward.5, Dist.3, HCMC",
                        "6 Nguyen van Cu, Ward.7, Tan Binh Dist., HCMC",
                        "12 Dong Khoi, Ben Tre",
                        "HCMC",
                        "19040",
                        1),
                    Contact = new(
                        "0985754545",
                        "example123@gmail.com",
                        1)
                }
            };
        }

        [Test]
        public void GetAllPharmacies_PharmacyHasOne_ShouldBePass()
        {
            //Arrange
            var listPharmacies = new List<Entities.Pharmacy>()
            {
                new Entities.Pharmacy(_listCreatePharmacyDomainCommand[0])
            };
            //Act && Assert
            Assert.That(listPharmacies.Count(), Is.EqualTo(1));
        }
    }
}