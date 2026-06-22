using System;
using System.Collections.Generic;
using System.Text;
using HotpotWebApplication.Models;
using HotpotWebApplication.Repositories.Interfaces;
using HotpotWebApplication.Services.Implementations;
using Moq;

namespace HotpotWebApplication.Tests.Services
{
    [TestFixture]
    public class CategoryServiceTest
    {
        private Mock<ICategoryRepository> _repoMock;
        private CategoryService _service;

        [SetUp]
        public void Setup()
        {
            _repoMock = new Mock<ICategoryRepository>();
            _service = new CategoryService(_repoMock.Object);
        }


        [Test]
        public async Task When_GetCategoryById_ExistingId_ReturnsCategory()
        {
            // Arrange
            var category = new Category
            {
                CategoryId = 1,
                CategoryName = "Pizza"
            };

            _repoMock
                .Setup(x => x.GetByIdAsync(1))
                .ReturnsAsync(category);

            // Act
            var result = await _service.GetCategoryByIdAsync(1);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result!.CategoryName, Is.EqualTo("Pizza"));
        }

        [Test]
        public async Task When_GetCategoryById_InvalidId_ReturnsNull()
        {
            // Arrange
            _repoMock
                .Setup(x => x.GetByIdAsync(999))
                .ReturnsAsync((Category?)null);

            // Act
            var result = await _service.GetCategoryByIdAsync(999);

            // Assert
            Assert.That(result, Is.Null);
        }

       

        [TestCase("Pizza")]
        [TestCase("Burger")]
        [TestCase("Biryani")]
        public async Task When_CreateCategory_ValidName_SavesCategory(string categoryName)
        {
            // Arrange
            var category = new Category
            {
                CategoryName = categoryName
            };

            // Act
            await _service.CreateCategoryAsync(category);

            // Assert
            _repoMock.Verify(
                x => x.AddAsync(It.Is<Category>(
                    c => c.CategoryName == categoryName)),
                Times.Once);

            _repoMock.Verify(
                x => x.SaveAsync(),
                Times.Once);
        }

       

        [Test]
        public async Task When_UpdateCategory_ValidCategory_UpdatesSuccessfully()
        {
            // Arrange
            var category = new Category
            {
                CategoryId = 1,
                CategoryName = "Updated Category"
            };

            // Act
            await _service.UpdateCategoryAsync(category);

            // Assert
            _repoMock.Verify(
                x => x.UpdateAsync(It.IsAny<Category>()),
                Times.Once);

            _repoMock.Verify(
                x => x.SaveAsync(),
                Times.Once);
        }

       

        [TestCase(1, true)]
        [TestCase(999, false)]
        public async Task When_DeleteCategory_IdProvided_ReturnsExpectedResult(
            int categoryId,
            bool expectedResult)
        {
            // Arrange

            if (expectedResult)
            {
                _repoMock
                    .Setup(x => x.GetByIdAsync(categoryId))
                    .ReturnsAsync(new Category
                    {
                        CategoryId = categoryId,
                        CategoryName = "Pizza"
                    });
            }
            else
            {
                _repoMock
                    .Setup(x => x.GetByIdAsync(categoryId))
                    .ReturnsAsync((Category?)null);
            }

            // Act
            var result =
                await _service.DeleteCategoryAsync(categoryId);

            // Assert
            Assert.That(result, Is.EqualTo(expectedResult));
        }

       

        [Test]
        public async Task When_GetAllCategories_DataExists_ReturnsCategories()
        {
            // Arrange
            var categories = new List<Category>
            {
                new() { CategoryId = 1, CategoryName = "Pizza" },
                new() { CategoryId = 2, CategoryName = "Burger" }
            };

            _repoMock
                .Setup(x => x.GetAllAsync())
                .ReturnsAsync(categories);

            // Act
            var result = await _service.GetAllCategoriesAsync();

            // Assert
            Assert.That(result.Count(), Is.EqualTo(2));
        }

        
    }
}
