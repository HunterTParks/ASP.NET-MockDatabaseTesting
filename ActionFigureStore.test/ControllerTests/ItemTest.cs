using System.Collections.Generic;
using System.Linq;
 using ActionFigureStore.Models;
using Xunit;
using Moq;
using ActionFigureStore.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace ActionFigureStore.test
{
    public class ItemTest
    {
        Mock<IItemRepository> mock = new Mock<IItemRepository>();
        EFItemRepository db = new EFItemRepository(new TestDbContext());

        private void DbSetup()
        {
            mock.Setup(m => m.Items).Returns(new Item[]
            {
                new Item {ItemId = 1, Description = "Wolverine" },
                new Item {ItemId = 2, Description = "Captain America" },
                new Item {ItemId = 3, Description = "Batman" }
            }.AsQueryable());
        }

        [Fact]
        public void Mock_GetViewResultIndex_Test() //Confirms route returns view
        {
            //Arrange
            DbSetup();
            ItemsController controller = new ItemsController(mock.Object);

            //Act
            var result = controller.Index();

            //Assert
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void Mock_IndexListOfItems_Test() //Confirms model as list of items
        {
            // Arrange
            DbSetup();
            ViewResult indexView = new ItemsController(mock.Object).Index() as ViewResult;

            // Act
            var result = indexView.ViewData.Model;

            // Assert
            Assert.IsType<List<Item>>(result);
        }

        [Fact]
        public void Mock_ConfirmEntry_Test() //Confirms presence of known entry
        {
            // Arrange
            DbSetup();
            ItemsController controller = new ItemsController(mock.Object);
            Item testItem = new Item();
            testItem.Description = "Captain America";
            testItem.ItemId = 2;

            // Act
            controller.Create(testItem);
            ViewResult indexView = controller.Index() as ViewResult;
            var collection = indexView.ViewData.Model as IEnumerable<Item>;

            // Assert
            Assert.Contains<Item>(testItem, collection);
        }

        [Fact]
        public void DB_CreateNewEntry_Test()
        {
            // Arrange
            ItemsController controller = new ItemsController(db);
            Item testItem = new Item();
            testItem.Description = "TestDb Item";

            // Act
            controller.Create(testItem);
            var collection = (controller.Index() as ViewResult).ViewData.Model as IEnumerable<Item>;
            
            // Assert
            Assert.Contains<Item>(testItem, collection);
            db.RemoveAll(db.Items);
        }

        [Fact]
        public void DB_EditEntry_Test()
        {
            //Arrange
            ItemsController controller = new ItemsController(db);
            Item testItem = new Item();
            testItem.Description = "TestDb Item";
            controller.Create(testItem);
   
            //Act
            testItem.Description = "TestDb Object";
            controller.Edit(testItem);
            ViewResult indexView = controller.Index() as ViewResult;
            var collection = indexView.ViewData.Model as IEnumerable<Item>;

            //Assert
            Assert.Equal(testItem.Description, "TestDb Object");
        }




        [Fact]
        public void DB_DeleteAll_Test()
        {
            ItemsController controller = new ItemsController(db);
            Item testItem = new Item();
            testItem.Description = "TestDb Item";

            controller.Create(testItem);
            db.RemoveAll(db.Items);
            var collection = (controller.Index() as ViewResult).ViewData.Model as IEnumerable<Item>;

            Assert.Empty(collection);
        }

    }
}
