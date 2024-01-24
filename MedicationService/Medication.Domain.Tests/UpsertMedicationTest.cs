using Medication.Domain.DomainCommands;
using Medication.Domain.Exceptions;
using NUnit.Framework;

namespace Medication.Domain.Tests
{
    public class UpsertMedicationTest
    {
        //private Entities.Medication _existingMedication;

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void UpsertMedication_NameOfMedicationIsNull_ShouldThrowException()
        {
            //Arrange
            var upsertMedicationDomainCommand = new UpsertMedicationDomandCommand()
            {
                MedicineName = null,
                Price = 12
            };

            //Act && Assert
            Assert.That(() => new Entities.Medication(upsertMedicationDomainCommand), Throws.TypeOf<InvalidArgumentException>());
        }

        [Test]
        public void UpsertMedication_NameOfMedicationIsGreaterThan50Letters_ShouldThrowException()
        {
            //Arrange
            var upsertMedicationDomainCommand = new UpsertMedicationDomandCommand()
            {
                MedicineName = "Name Of Medication Is Greater Than 50 Letters => Should Throw Exception",
                Price = 12
            };

            //Act && Assert
            Assert.That(() => new Entities.Medication(upsertMedicationDomainCommand), Throws.TypeOf<InvalidArgumentException>());
        }

        [Test]
        public void UpsertMedication_PriceOfMedicationIsLessThanOrEqualToZero_ShouldThrowException()
        {
            //Arrange
            var upsertMedicationDomainCommand = new UpsertMedicationDomandCommand()
            {
                MedicineName = "Medication A",
                Price = 0
            };

            //Act && Assert
            Assert.That(() => new Entities.Medication(upsertMedicationDomainCommand), Throws.TypeOf<InvalidArgumentException>());
        }

        [Test]
        public void UpsertMedication_ArgumentOfMedicationIsValid_ShouldLinkNewMedication()
        {
            //Arrange
            var upsertMedicationDomainCommand = new UpsertMedicationDomandCommand()
            {
                MedicineName = "Medication A",
                Price = 12
            };

            //Act && Assert
            Assert.AreEqual(12, new Entities.Medication(upsertMedicationDomainCommand).Price);
        }
    }
}