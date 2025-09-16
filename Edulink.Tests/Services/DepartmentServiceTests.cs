using AutoMapper;
using Edulink.Tests.Helpers;
using Edulink.Tests.TestData;
using EduLink.Core.Entities;
using EduLink.Core.IRepositories;
using EduLink.Core.IServices;
using EduLink.Service;
using EduLink.Utilities.DTO.Department;
using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Edulink.Tests.Services
{
    public class DepartmentServiceTests : IDisposable
    {
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly Mock<IGenericRepository<Department>> _mockDepartmentRepository;
        private readonly Mock<IMapper> _mockMapper;
        private readonly DepartmentService _departmentService;
        public DepartmentServiceTests()
        {
            // Initialize mocks
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _mockDepartmentRepository = new Mock<IGenericRepository<Department>>();
            _mockMapper = TestHelpers.CreateMapperMock();

            // Setup UnitOfWork to return our mocked repository
            _mockUnitOfWork.Setup(uow => uow.Repository<Department>())
                          .Returns(_mockDepartmentRepository.Object);

            // Initialize service with mocked dependencies
            _departmentService = new DepartmentService(_mockUnitOfWork.Object, _mockMapper.Object);
        }
        [Fact]
        public async Task CreateDepartment_WithValidData_ShouldReturnSuccessResponse()
        {
            // Arrange
            var createDto = DepartmentTestData.GetValidCreateDepartmentDTO();
            var department = DepartmentTestData.GetValidDepartment();

            _mockDepartmentRepository.Setup(repo => repo.AddAsync(It.IsAny<Department>()))
                                    .Returns(Task.CompletedTask);

            _mockUnitOfWork.Setup(uow => uow.CompleteAsync())
                           .ReturnsAsync(1); // Simulate successful save

            // Act
            var result = await _departmentService.CreateDepartment(createDto);

            // Assert
            result.Should().NotBeNull();
            result.IsSuccess.Should().BeTrue();
            result.Message.Should().Contain("created successfully");

            // Verify repository methods were called
            _mockDepartmentRepository.Verify(repo => repo.AddAsync(It.IsAny<Department>()), Times.Once);
            _mockUnitOfWork.Verify(uow => uow.CompleteAsync(), Times.Once);
        }

        [Fact]
        public async Task CreateDepartment_WithNullDto_ShouldReturnErrorResponse()
        {
            // Arrange
            CreateDepartmentDTO createDto = null;

            // Act
            var result = await _departmentService.CreateDepartment(createDto);

            // Assert
            result.Should().NotBeNull();
            result.IsSuccess.Should().BeFalse();
            result.Message.Should().Contain("Invalid data");

            // Verify repository methods were NOT called
            _mockDepartmentRepository.Verify(repo => repo.AddAsync(It.IsAny<Department>()), Times.Never);
            _mockUnitOfWork.Verify(uow => uow.CompleteAsync(), Times.Never);
        }

        [Fact]
        public async Task CreateDepartment_WhenSaveFails_ShouldReturnErrorResponse()
        {
            // Arrange
            var createDto = DepartmentTestData.GetValidCreateDepartmentDTO();

            _mockDepartmentRepository.Setup(repo => repo.AddAsync(It.IsAny<Department>()))
                                    .Returns(Task.CompletedTask);

            _mockUnitOfWork.Setup(uow => uow.CompleteAsync())
                           .ReturnsAsync(0); // Simulate failed save

            // Act
            var result = await _departmentService.CreateDepartment(createDto);

            // Assert
            result.Should().NotBeNull();
            result.IsSuccess.Should().BeFalse();
            result.Message.Should().Contain("Failed to create");
        }

        [Fact]
        public async Task CreateDepartment_WhenExceptionThrown_ShouldReturnErrorResponse()
        {
            // Arrange
            var createDto = DepartmentTestData.GetValidCreateDepartmentDTO();

            _mockDepartmentRepository.Setup(repo => repo.AddAsync(It.IsAny<Department>()))
                                    .ThrowsAsync(new Exception("Database error"));

            // Act
            var result = await _departmentService.CreateDepartment(createDto);

            // Assert
            result.Should().NotBeNull();
            result.IsSuccess.Should().BeFalse();
            result.Message.Should().Contain("error occurred");
        }
        [Fact]
        public async Task UpdateDepartment_WithValidData_ShouldReturnSuccessResponse()
        {
            // Arrange
            var departmentId = 1;
            var updateDto = DepartmentTestData.GetValidUpdateDepartmentDTO();
            var existingDepartment = DepartmentTestData.GetValidDepartment();

            _mockDepartmentRepository.Setup(repo => repo.GetByIdAsync(departmentId))
                                    .ReturnsAsync(existingDepartment);

            _mockDepartmentRepository.Setup(repo => repo.UpdateAsync(It.IsAny<Department>()))
                                    .Returns(Task.CompletedTask);

            _mockUnitOfWork.Setup(uow => uow.CompleteAsync())
                           .ReturnsAsync(1);

            // Act
            var result = await _departmentService.UpdateDepartment(departmentId, updateDto);

            // Assert
            result.Should().NotBeNull();
            result.IsSuccess.Should().BeTrue();
            result.Message.Should().Contain("updated successfully");

            // Verify calls
            _mockDepartmentRepository.Verify(repo => repo.GetByIdAsync(departmentId), Times.Once);
            _mockDepartmentRepository.Verify(repo => repo.UpdateAsync(It.IsAny<Department>()), Times.Once);
            _mockUnitOfWork.Verify(uow => uow.CompleteAsync(), Times.Once);
        }

        [Fact]
        public async Task UpdateDepartment_WithNonExistentId_ShouldReturnErrorResponse()
        {
            // Arrange
            var departmentId = 999;
            var updateDto = DepartmentTestData.GetValidUpdateDepartmentDTO();

            _mockDepartmentRepository.Setup(repo => repo.GetByIdAsync(departmentId))
                                    .ReturnsAsync((Department)null);

            // Act
            var result = await _departmentService.UpdateDepartment(departmentId, updateDto);

            // Assert
            result.Should().NotBeNull();
            result.IsSuccess.Should().BeFalse();
            result.Message.Should().Contain("not found");

            // Verify only GetByIdAsync was called
            _mockDepartmentRepository.Verify(repo => repo.GetByIdAsync(departmentId), Times.Once);
            _mockDepartmentRepository.Verify(repo => repo.UpdateAsync(It.IsAny<Department>()), Times.Never);
        }

        [Fact]
        public async Task UpdateDepartment_WithNullDto_ShouldReturnErrorResponse()
        {
            // Arrange
            var departmentId = 1;
            UpdateDepartmentDTO updateDto = null;

            // Act
            var result = await _departmentService.UpdateDepartment(departmentId, updateDto);

            // Assert
            result.Should().NotBeNull();
            result.IsSuccess.Should().BeFalse();
            result.Message.Should().Contain("Invalid data");
        }
        [Fact]
        public async Task GetDepartment_WithExistingId_ShouldReturnSuccessResponse()
        {
            // Arrange
            var departmentId = 1;
            var existingDepartment = DepartmentTestData.GetValidDepartment();

            _mockDepartmentRepository.Setup(repo => repo.GetByIdAsync(departmentId))
                                    .ReturnsAsync(existingDepartment);

            // Act
            var result = await _departmentService.GetDepartment(departmentId);

            // Assert
            result.Should().NotBeNull();
            result.IsSuccess.Should().BeTrue();
            result.Data.Should().NotBeNull();
            result.Message.Should().Contain("retrieved successfully");

            _mockDepartmentRepository.Verify(repo => repo.GetByIdAsync(departmentId), Times.Once);
        }

        [Fact]
        public async Task GetDepartment_WithNonExistentId_ShouldReturnErrorResponse()
        {
            // Arrange
            var departmentId = 999;

            _mockDepartmentRepository.Setup(repo => repo.GetByIdAsync(departmentId))
                                    .ReturnsAsync((Department)null);

            // Act
            var result = await _departmentService.GetDepartment(departmentId);

            // Assert
            result.Should().NotBeNull();
            result.IsSuccess.Should().BeFalse();
            result.Message.Should().Contain("not found");
            result.Data.Should().BeNull();
        }
        [Fact]
        public async Task GetAllDepartments_WithExistingDepartments_ShouldReturnSuccessResponse() 
        {
            var departments = DepartmentTestData.GetDepartmentsList();
            var result = await _departmentService.GetAllDepartments();
            result.Should().NotBeNull();
            result.IsSuccess.Should().BeTrue();
            result.Data.Should().NotBeNull();
            result.Message.Should().Contain("retrieved successfully");

            _mockDepartmentRepository.Verify(repo => repo.GetAllAsync(), Times.Once);
        }
        [Fact]
        public async Task GetAllDepartments_WhenExceptionThrown_ShouldReturnErrorResponse()
        {
            // Arrange
            _mockDepartmentRepository.Setup(repo => repo.GetAllAsync())
                                    .ThrowsAsync(new Exception("Database connection failed"));

            // Act
            var result = await _departmentService.GetAllDepartments();

            // Assert
            result.Should().NotBeNull();
            result.IsSuccess.Should().BeFalse();
            result.Message.Should().Contain("error occurred");
        }
        public void Dispose()
        {
            _mockUnitOfWork?.Reset();
            _mockDepartmentRepository?.Reset();
            _mockMapper?.Reset();
        }
    }
}
