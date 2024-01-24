using MedicineOrder.Application.Exceptions;
using MedicineOrder.Application.MedicineOrder.Dtos;
using MedicineOrder.Application.MedicineOrder.Handlers;
using MedicineOrder.Domain.DomainCommands;
using MedicineOrder.Domain.Entities;
using MedicineOrder.Domain.Exceptions;
using MedicineOrder.Domain.Extensions;
using MedicineOrder.Domain.Interfaces;
using Moq;
using NUnit.Framework;

namespace MedicineOrder.Application.Tests
{
    public class UpsertMedicineOrderHandlerTest
    {
        private Mock<IMedicineOrderRepository> _mockMedicineOrderRepository;
        private Mock<IMedicineRepository> _mockMedicineRepository;
        private Mock<IMessageHandler> _mockMessageHandler;
        private Domain.Entities.Order _existingOrder;
        private List<Domain.Entities.Medicine> _existingListMedicine;
        private UpsertMedicineOrderHandler _handler;

        [SetUp]
        public void Setup()
        {
            var createListMedicineOrderDomainCommand = new List<InsertMedicineOrderDomainCommand>()
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

            var createOrderDomainCommand = new InsertOrderDomainCommand()
            {
                MedicineOrderDetails = createListMedicineOrderDomainCommand,
                TotalPrice = 100,
                OrderStatus = true,
                PharmacyID = 1,
            };

            //set up a list medicine 
            var _createListMedicineDomainCommand = new List<CreateMedicineDomainCommand>()
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

            _existingOrder = new Domain.Entities.Order(createOrderDomainCommand, _createListMedicineDomainCommand);

            _mockMedicineOrderRepository = new Mock<IMedicineOrderRepository>();
            _mockMedicineOrderRepository.Setup(mockMedicineOrderResitory => mockMedicineOrderResitory.GetSingleMedicineOrder(It.IsAny<ExpressionModel<Domain.Entities.Order>>(), It.IsAny<bool?>()))
                .ReturnsAsync(_existingOrder);
            _mockMedicineOrderRepository.Setup(mockMedicineOrderRepository => mockMedicineOrderRepository.InsertMedicineOrder(It.IsAny<Domain.Entities.Order>()))
                .Verifiable();
            _mockMedicineOrderRepository.Setup(mockMedicineOrderRepository => mockMedicineOrderRepository.UpdateMedicineOrder(It.IsAny<Domain.Entities.Order>()))
                .Verifiable();
            _mockMedicineOrderRepository.Setup(mockMedicineOrderRepository => mockMedicineOrderRepository.UnitOfWork.SaveEntityChangesAsync())
                .Verifiable();
            

            _existingListMedicine = new List<Domain.Entities.Medicine>
            {
                new Domain.Entities.Medicine(_createListMedicineDomainCommand[0]),
                new Domain.Entities.Medicine(_createListMedicineDomainCommand[1]),
                new Domain.Entities.Medicine(_createListMedicineDomainCommand[2])
            };

            
            _mockMedicineRepository = new Mock<IMedicineRepository>();
            _mockMedicineRepository.Setup(mockMedicineRepository => mockMedicineRepository.GetListMedicines(It.IsAny<ExpressionModel<Domain.Entities.Medicine>>(), It.IsAny<bool?>()))
                .ReturnsAsync(_existingListMedicine);

            _mockMessageHandler = new Mock<IMessageHandler>();
            _mockMessageHandler.Setup(mockMessageHandler => mockMessageHandler.BroadcastMedicineOrder(It.IsAny<BroadcastOrderDto>()))
                .Verifiable();


            _handler = new UpsertMedicineOrderHandler(_mockMedicineOrderRepository.Object, _mockMedicineRepository.Object, _mockMessageHandler.Object);

        }

        #region Insert Medicine Order 
        [Test]
        public void HandleInsertMedicineOrder_MedicineOrderDetailsIsNull_ShouldThrowException()
        {
            //Arrange
            var upsertOrderDto = new UpsertOrderDto()
            {
                Id = _existingOrder.Id,
                PharmacyID = 2,
                OrderStatus = true,
                TotalPrice = 1,
                OrderDetails = { }
            };
            _mockMedicineOrderRepository.Setup(mockMedicineOrderRepository => mockMedicineOrderRepository.GetSingleMedicineOrder(It.IsAny<ExpressionModel<Domain.Entities.Order>>(), It.IsAny<bool?>()))
                .ReturnsAsync((Domain.Entities.Order)null);

            //Act && Assert
            Assert.That(async () => await _handler.InsertOrderHandler(upsertOrderDto), Throws.TypeOf<ArgumentNullException>());
        }

        [Test]
        public void HandleInsertMedicineOrder_MedicineIsNotExisted_ShouldThrowException()
        {
            //Arrange
            var upsertOrderDto = new UpsertOrderDto()
            {
                Id = 1,
                PharmacyID = 2,
                OrderStatus = true,
                TotalPrice = 1,
                OrderDetails = new List<UpsertMedicineOrderDto>()
                {
                    new UpsertMedicineOrderDto()
                    {
                        OrderID = 1,
                        MedicineID = 1,
                        Quantity = 2
                    },
                    new UpsertMedicineOrderDto()
                    {
                        OrderID = 1,
                        MedicineID = 2,
                        Quantity = 5
                    },
                    new UpsertMedicineOrderDto()
                    {
                        OrderID = 1,
                        MedicineID = 3,
                        Quantity = 1
                    }
                }.ToList()
            };
            _mockMedicineOrderRepository.Setup(mockMedicineOrderResitory => mockMedicineOrderResitory.GetSingleMedicineOrder(It.IsAny<ExpressionModel<Domain.Entities.Order>>(), It.IsAny<bool?>()))
                .ReturnsAsync((Domain.Entities.Order)null);
            _mockMedicineRepository.Setup(mockMedicineRepository => mockMedicineRepository.GetListMedicines(It.IsAny<ExpressionModel<Domain.Entities.Medicine>>(), It.IsAny<bool?>()))
                .ReturnsAsync((List< Domain.Entities.Medicine>)null);

            //Act && Assert
            Assert.That(async () => await _handler.InsertOrderHandler(upsertOrderDto), Throws.TypeOf<ArgumentNullException>());
        }


        [Test]
        public void HandleInsertMedicineOrder_MedicineOrderIdIsExisted_ShouldThrowException()
        {
            //Arrange
            var upsertOrderDto = new UpsertOrderDto()
            {
                Id = _existingOrder.Id,
                PharmacyID = 2,
                OrderStatus = true,
                TotalPrice = 1,
                OrderDetails = new List<UpsertMedicineOrderDto>()
                {
                    new UpsertMedicineOrderDto()
                    {
                        OrderID = _existingOrder.Id,
                        MedicineID = 1,
                        Quantity = 2
                    },
                    new UpsertMedicineOrderDto()
                    {
                        OrderID = _existingOrder.Id,
                        MedicineID = 2,
                        Quantity = 5
                    },
                    new UpsertMedicineOrderDto()
                    {
                        OrderID = _existingOrder.Id,
                        MedicineID = 3,
                        Quantity = 1
                    }
                }.ToList()
            };

            _mockMedicineOrderRepository.Setup(mockMedicineOrderResitory => mockMedicineOrderResitory.GetSingleMedicineOrder(It.IsAny<ExpressionModel<Domain.Entities.Order>>(), It.IsAny<bool?>()))
                .ReturnsAsync(_existingOrder);
            
            //Act && Assert
            Assert.That(async()=> await _handler.InsertOrderHandler(upsertOrderDto), Throws.TypeOf<MedicineOrderAlreadyExistedException>());
        }

        [Test]
        public async Task HandleInsertMedicineOrder_MedicineOrderIdIsNotExistedAndNoException_ShouldInsertNewOne()
        {
            //Arrange
            var upsertOrderDto = new UpsertOrderDto()
            {
                Id = _existingOrder.Id,
                PharmacyID = 2,
                OrderStatus = true,
                TotalPrice = 1,
                OrderDetails = new List<UpsertMedicineOrderDto>()
                {
                    new UpsertMedicineOrderDto()
                    {
                        OrderID = _existingOrder.Id,
                        MedicineID = 1,
                        Quantity = 2
                    },
                    new UpsertMedicineOrderDto()
                    {
                        OrderID = _existingOrder.Id,
                        MedicineID = 2,
                        Quantity = 5
                    },
                    new UpsertMedicineOrderDto()
                    {
                        OrderID = _existingOrder.Id,
                        MedicineID = 3,
                        Quantity = 1
                    }
                }.ToList()
            };

            _mockMedicineOrderRepository.Setup(mockMedicineOrderResitory => mockMedicineOrderResitory.GetSingleMedicineOrder(It.IsAny<ExpressionModel<Domain.Entities.Order>>(), It.IsAny<bool?>()))
                .ReturnsAsync((Domain.Entities.Order)null);
            _mockMedicineRepository.Setup(mockMedicineRepository => mockMedicineRepository.GetListMedicines(It.IsAny<ExpressionModel<Domain.Entities.Medicine>>(), It.IsAny<bool?>()))
                .ReturnsAsync(_existingListMedicine);
            
            //Act
            await _handler.InsertOrderHandler(upsertOrderDto);

            //Assert
            _mockMedicineOrderRepository.Verify(_mockMedicineOrderRepository => _mockMedicineOrderRepository.InsertMedicineOrder(It.IsAny<Domain.Entities.Order>()), Times.Once);
            _mockMedicineOrderRepository.Verify(_mockMedicineOrderRepository => _mockMedicineOrderRepository.UnitOfWork.SaveEntityChangesAsync(), Times.Once);
        }

        [Test]
        public async Task HandleInsertMedicineOrder_CalculateTotalPriceIsRight_ShouldInsertNewOne()
        {
            //Arrange
            var upsertOrderDto = new UpsertOrderDto()
            {
                Id = _existingOrder.Id,
                PharmacyID = 2,
                OrderStatus = true,
                TotalPrice = 1,
                OrderDetails = new List<UpsertMedicineOrderDto>()
                {
                    new UpsertMedicineOrderDto()
                    {
                        OrderID = _existingOrder.Id,
                        MedicineID = 1,
                        Quantity = 2
                    },
                    new UpsertMedicineOrderDto()
                    {
                        OrderID = _existingOrder.Id,
                        MedicineID = 1,
                        Quantity = 5
                    },
                    new UpsertMedicineOrderDto()
                    {
                        OrderID = _existingOrder.Id,
                        MedicineID = 1,
                        Quantity = 1
                    }
                }.ToList()
            };

            _mockMedicineOrderRepository.Setup(mockMedicineOrderResitory => mockMedicineOrderResitory.GetSingleMedicineOrder(It.IsAny<ExpressionModel<Domain.Entities.Order>>(), It.IsAny<bool?>()))
                .ReturnsAsync((Domain.Entities.Order)null);
            _mockMedicineRepository.Setup(mockMedicineRepository => mockMedicineRepository.GetListMedicines(It.IsAny<ExpressionModel<Domain.Entities.Medicine>>(), It.IsAny<bool?>()))
               .ReturnsAsync(_existingListMedicine);

            //Act
            await _handler.InsertOrderHandler(upsertOrderDto);

            //Assert
            _mockMedicineOrderRepository.Verify(_mockMedicineOrderRepository => _mockMedicineOrderRepository.InsertMedicineOrder(It.IsAny<Domain.Entities.Order>()), Times.Once);
            _mockMedicineOrderRepository.Verify(_mockMedicineOrderRepository => _mockMedicineOrderRepository.UnitOfWork.SaveEntityChangesAsync(), Times.Once);
            Assert.That((int)upsertOrderDto.TotalPrice, Is.EqualTo((8 * 15)));
        }

        #endregion

        #region Update medicine order is deleted
        [Test]
        public void HandleDeleteMedicineOrder_MedicineOrderNotFound_ShouldThrowException()
        {
            //Arrange
            var createMedicineOrderId = 1;
            _mockMedicineOrderRepository.Setup(mockMedicineOrderResitory => mockMedicineOrderResitory.GetSingleMedicineOrder(It.IsAny<ExpressionModel<Domain.Entities.Order>>(), It.IsAny<bool?>()))
                .ReturnsAsync((Domain.Entities.Order)null);
            var upsertOrderDto = new UpsertOrderDto()
            {
                Id = createMedicineOrderId
            };

            //Act && Assert
            Assert.That(async () => await _handler.DeleteMedicineOrderHandler(upsertOrderDto), Throws.TypeOf<MedicineOrderNotFoundException>());
        }

        [Test]
        public async Task HandleDeleteMedicineOrder_MedicineOrderFound_ShouldUpdateExistingOne()
        {
            //arrange
            var createMedicineOrderId = _existingOrder.Id;
            _mockMedicineOrderRepository.Setup(mockMedicineOrderResitory => mockMedicineOrderResitory.GetSingleMedicineOrder(It.IsAny<ExpressionModel<Domain.Entities.Order>>(), It.IsAny<bool?>()))
                .ReturnsAsync(_existingOrder);
            var upsertOrderDto = new UpsertOrderDto()
            {
                Id = createMedicineOrderId
            };

            //Act
            await _handler.DeleteMedicineOrderHandler(upsertOrderDto);

            //Assert
            _mockMedicineOrderRepository.Verify(_mockMedicineOrderRepository => _mockMedicineOrderRepository.UpdateMedicineOrder(It.IsAny<Domain.Entities.Order>()), Times.Once);
            _mockMedicineOrderRepository.Verify(_mockMedicineOrderRepository => _mockMedicineOrderRepository.UnitOfWork.SaveEntityChangesAsync(), Times.Once);
        }
        #endregion
    }
}