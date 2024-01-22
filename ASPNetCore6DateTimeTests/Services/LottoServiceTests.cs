using Microsoft.VisualStudio.TestTools.UnitTesting;
using ASPNetCore6DateTime.Services;
using ASPNetCore6DateTime.ViewModels;
using ExpectedObjects;
using ASPNetCore6DateTime.Wrapper;
using Moq;

namespace ASPNetCore6DateTime.Services.Tests
{
    [TestClass()]
    public class LottoServiceTests
    {
        [TestMethod()]
        public void Test_Lottoing_20240105_輸入亂數範圍_0_10_預期回傳_9_恭喜中獎()
        {
            // Arrange
            var expected = new LottoViewModel()
            { YourNumber = 9, Message = "恭喜中獎" }
                        .ToExpectedObject();

            int fixedValue = 9;
            DateTime today = new(2024, 01, 05);
            var mockRandomGenerator = new Mock<IRandomGenerator>();
            var mockDateTimeProvider = new Mock<IDateTimeProvider>();
            mockRandomGenerator.Setup(r => r.Next(It.IsAny<int>(), It.IsAny<int>())).Returns(fixedValue);
            mockDateTimeProvider.Setup(d => d.GetCurrentTime()).Returns(today);

            // Act
            var target = new LottoService(mockRandomGenerator.Object, mockDateTimeProvider.Object);
            var actual = target.Lottoing(0, 10);

            // Assert
            expected.ShouldEqual(actual);
        }


        [TestMethod()]
        public void Test_Lottoing_20240105_輸入亂數範圍_0_10_預期回傳_1_再接再厲()
        {
            // Arrange
            var expected = new LottoViewModel()
            { YourNumber = 1, Message = "再接再厲" }
                        .ToExpectedObject();

            int fixedValue = 1;
            DateTime today = new(2024, 01, 05);
            var mockRandomGenerator = new Mock<IRandomGenerator>();
            var mockDateTimeProvider = new Mock<IDateTimeProvider>();
            mockRandomGenerator.Setup(r => r.Next(It.IsAny<int>(), It.IsAny<int>())).Returns(fixedValue);
            mockDateTimeProvider.Setup(d => d.GetCurrentTime()).Returns(today);

            // Act
            var target = new LottoService(mockRandomGenerator.Object, mockDateTimeProvider.Object);
            var actual = target.Lottoing(0, 10);

            // Assert
            expected.ShouldEqual(actual);
        }

    }
}