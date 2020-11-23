using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using BookingHotels.Domain.Abstract;
using BookingHotels.Domain.Entities;
using BookingHotels.WebUI.Controllers;
using BookingHotels.WebUI.Models;
using BookingHotels.WebUI.HtmlHelpers;

namespace BookingHotels.UnitTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void Can_Paginate()
        {
            // Организация (arrange)
            Mock<IHotelRepository> mock = new Mock<IHotelRepository>();
            mock.Setup(m => m.Hotels).Returns(new List<Hotel>
            {
                new Hotel { HotelId = 1, Name = "Отель1"},
                new Hotel { HotelId = 2, Name = "Отель2"},
                new Hotel { HotelId = 3, Name = "Отель3"},
                new Hotel { HotelId = 4, Name = "Отель4"},
                new Hotel { HotelId = 5, Name = "Отель5"}
            });
            HotelController controller = new HotelController(mock.Object);
            controller.pageSize = 3;

            // Действие (act)
            HotelsListViewModel result
        = (HotelsListViewModel)controller.List_of_hotels(null, 2).Model;

            // Assert
            List<Hotel> hotels = result.Hotels.ToList();
            Assert.IsTrue(hotels.Count == 2);
            Assert.AreEqual(hotels[0].Name, "Игра4");
            Assert.AreEqual(hotels[1].Name, "Игра5");
        }

        [TestMethod]
        public void Can_Generate_Page_Links()
        {

            // Организация - определение вспомогательного метода HTML - это необходимо
            // для применения расширяющего метода
            HtmlHelper myHelper = null;

            // Организация - создание объекта PagingInfo
            PagingInfo pagingInfo = new PagingInfo
            {
                CurrentPage = 2,
                TotalItems = 28,
                ItemsPerPage = 10
            };

            // Организация - настройка делегата с помощью лямбда-выражения
            Func<int, string> pageUrlDelegate = i => "Page" + i;

            // Действие
            MvcHtmlString result = myHelper.PageLinks(pagingInfo, pageUrlDelegate);

            // Утверждение
            Assert.AreEqual(@"<a class=""btn btn-default"" href=""Page1"">1</a>"
                + @"<a class=""btn btn-default btn-primary selected"" href=""Page2"">2</a>"
                + @"<a class=""btn btn-default"" href=""Page3"">3</a>",
                result.ToString());
        }

        [TestMethod]
        public void Can_Send_Pagination_View_Model()
        {
            // Организация (arrange)
            Mock<IHotelRepository> mock = new Mock<IHotelRepository>();
            mock.Setup(m => m.Hotels).Returns(new List<Hotel>
            {
              new Hotel { HotelId = 1, Name = "Отель1"},
              new Hotel { HotelId = 2, Name = "Отель2"},
              new Hotel { HotelId = 3, Name = "Отель3"},
              new Hotel { HotelId = 4, Name = "Отель4"},
              new Hotel { HotelId = 5, Name = "Отель5"}
            });
            HotelController controller = new HotelController(mock.Object);
            controller.pageSize = 3;

            // Act
            HotelsListViewModel result
                = (HotelsListViewModel)controller.List_of_hotels(null, 2).Model;

            // Assert
            PagingInfo pageInfo = result.PagingInfo;
            Assert.AreEqual(pageInfo.CurrentPage, 2);
            Assert.AreEqual(pageInfo.ItemsPerPage, 3);
            Assert.AreEqual(pageInfo.TotalItems, 5);
            Assert.AreEqual(pageInfo.TotalPages, 2);
        }


        [TestMethod]
        public void Can_Filter_Games()
        {
            // Организация (arrange)
            Mock<IHotelRepository> mock = new Mock<IHotelRepository>();
            mock.Setup(m => m.Hotels).Returns(new List<Hotel>
    {
        new Hotel { HotelId = 1, Name = "Отель1", Location="Cat1"},
        new Hotel { HotelId = 2, Name = "Отель2", Location="Cat2"},
        new Hotel { HotelId = 3, Name = "Отель3", Location="Cat1"},
        new Hotel { HotelId = 4, Name = "Отель4", Location="Cat2"},
        new Hotel { HotelId = 5, Name = "Отель5", Location="Cat3"}
    });
            HotelController controller = new HotelController(mock.Object);
            controller.pageSize = 3;

            // Action
            List<Hotel> result = ((HotelsListViewModel)controller.List_of_hotels("Cat2", 1).Model)
                .Hotels.ToList();

            // Assert
            Assert.AreEqual(result.Count(), 2);
            Assert.IsTrue(result[0].Name == "Отель2" && result[0].Location == "Cat2");
            Assert.IsTrue(result[1].Name == "Отель4" && result[1].Location == "Cat2");
        }

        [TestMethod]
        public void Can_Create_Location()
        {
            // Организация - создание имитированного хранилища
            Mock<IHotelRepository> mock = new Mock<IHotelRepository>();
            mock.Setup(m => m.Hotels).Returns(new List<Hotel> {
        new Hotel { HotelId = 1, Name = "Отель1", Location="Беларусь"},
        new Hotel { HotelId = 2, Name = "Отель2", Location="Беларусь"},
        new Hotel { HotelId = 3, Name = "Отель3", Location="Россия"},
        new Hotel { HotelId = 4, Name = "Отель4", Location="Германия"},
    });

            // Организация - создание контроллера
            NavController target = new NavController(mock.Object);

            // Действие - получение набора категорий
            List<string> results = ((IEnumerable<string>)target.Menu().Model).ToList();

            // Утверждение
            Assert.AreEqual(results.Count(), 3);
            Assert.AreEqual(results[0], "Германия");
            Assert.AreEqual(results[1], "Беларусь");
            Assert.AreEqual(results[2], "Россия");
        }
    }

    [TestClass]
    public class ImageTests
    {
        [TestMethod]
        public void Can_Retrieve_Image_Data()
        {
            // Организация - создание объекта Game с данными изображения
            Hotel hotel = new Hotel
            {
                HotelId = 2,
                Name = "Отель2",
                ImageData = new byte[] { },
                ImageMimeType = "image/png"
            };

            // Организация - создание имитированного хранилища
            Mock<IHotelRepository> mock = new Mock<IHotelRepository>();
            mock.Setup(m => m.Hotels).Returns(new List<Hotel> {
                new Hotel {HotelId = 1, Name = "Отель1"},
                hotel,
                new Hotel {HotelId = 3, Name = "Отель3"}
            }.AsQueryable());

            // Организация - создание контроллера
            HotelController controller = new HotelController(mock.Object);

            // Действие - вызов метода действия GetImage()
            ActionResult result = controller.GetImage(2);

            // Утверждение
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(FileResult));
            Assert.AreEqual(hotel.ImageMimeType, ((FileResult)result).ContentType);
        }

        [TestMethod]
        public void Cannot_Retrieve_Image_Data_For_Invalid_ID()
        {
            // Организация - создание имитированного хранилища
            Mock<IHotelRepository> mock = new Mock<IHotelRepository>();
            mock.Setup(m => m.Hotels).Returns(new List<Hotel> {
                new Hotel {HotelId = 1, Name = "Отель1"},
                new Hotel {HotelId = 2, Name = "Отель2"}
            }.AsQueryable());

            // Организация - создание контроллера
            HotelController controller = new HotelController(mock.Object);

            // Действие - вызов метода действия GetImage()
            ActionResult result = controller.GetImage(10);

            // Утверждение
            Assert.IsNull(result);
        }
    }
}
