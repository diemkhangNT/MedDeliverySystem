using MedicineOrder.Domain.DomainCommands;
using MedicineOrder.Domain.Entities;
using MedicineOrder.Domain.Exceptions;
using NUnit.Framework;

namespace MedicineOrder.Domain.Tests
{
    public class InsertMedicineOrderTest
    {
        private List<InsertMedicineOrderDomainCommand> _listInsertMedicineOrderDomainCommand;
        private List<CreateMedicineDomainCommand> _listCreateMedicineDomainCommand;

        [SetUp]
        public void Setup()
        {
            //set up a list medicine order (order detail)
            _listInsertMedicineOrderDomainCommand = new List<InsertMedicineOrderDomainCommand>()
            { 
                new InsertMedicineOrderDomainCommand()
                {
                    OrderID = 1,
                    MedicineID = 1,
                    Quantity = 2
                },
                new InsertMedicineOrderDomainCommand()
                {
                    OrderID = 1,
                    MedicineID = 2,
                    Quantity = 5
                },
                new InsertMedicineOrderDomainCommand()
                {
                    OrderID = 1,
                    MedicineID = 3,
                    Quantity = 1
                }
            }.ToList();

            //set up a list medicine 
            _listCreateMedicineDomainCommand = new List<CreateMedicineDomainCommand>()
            {
                new CreateMedicineDomainCommand()
                {
                    MedicineId = 1,
                    MedicineName = "Eco",
                    Price = 15,
                },
                new CreateMedicineDomainCommand()
                {
                    MedicineId = 2,
                    MedicineName = "Pharmacity",
                    Price = 23,
                },
                new CreateMedicineDomainCommand()
                {
                    MedicineId = 3,
                    MedicineName = "Uniclo",
                    Price = 12,
                }
            };
        }

        [Test]
        public void AddMedicineOrder_MedicineOrderIsNull_ShouldThrowExeption()
        {
            //Arrange
            _listInsertMedicineOrderDomainCommand.Clear();
            var createOrderDomainCommand = new InsertOrderDomainCommand()
            {
                MedicineOrderDetails = _listInsertMedicineOrderDomainCommand,
                TotalPrice = 100,
                OrderStatus = true,
                PharmacyID = 1,
            };

            //Act and Assert
            Assert.That(() => new Order(createOrderDomainCommand, _listCreateMedicineDomainCommand), Throws.TypeOf<MedicineOrdersIsNullException>());
        }

        [Test]
        public void AddMedicineOrder_MedicineOrderIsNotNull_ShouldLinkNewMedicineOrder()
        {
            //Arrange
            var toCreatePharmacyId = 3;
            var createOrderDomainCommand = new InsertOrderDomainCommand()
            {
                MedicineOrderDetails = _listInsertMedicineOrderDomainCommand,
                TotalPrice = 100,
                OrderStatus = true,
                PharmacyID = toCreatePharmacyId,
            };

            //Act
            Order newOrder = new Order(createOrderDomainCommand, _listCreateMedicineDomainCommand);
            
            //Assert
            Assert.That(newOrder.PharmacyID, Is.EqualTo(toCreatePharmacyId));
            Assert.IsTrue(newOrder.MedicineOrders.Any(medOrder => medOrder.OrderID == 1));
        }

        [Test]
        public void AddMedicineOrder_MedicineIsNotExisted_ShouldThrowException()
        {
            //Arrange
            _listCreateMedicineDomainCommand.Clear();
            var createOrderDomainCommand = new InsertOrderDomainCommand()
            {
                MedicineOrderDetails = _listInsertMedicineOrderDomainCommand,
                TotalPrice = 100,
                OrderStatus = true,
                PharmacyID = 1,
            };

            //Act and Assert
            Assert.That(() => new Order(createOrderDomainCommand, _listCreateMedicineDomainCommand), Throws.TypeOf<MedicineIsNotExistedException>());
        }
    }
}