## 靜態元素 (Static Elements) 的單元測試, 以亂數 (DateTime) 為例
Unit Test for Static Elements (DateTime class) in ASP.NET Core 6 MVC   

## 前言

接續前一篇 <a href="https://www.jasperstudy.com/2024/01/non-deterministic-elements-random.html" target="_blank">樂透開獎</a> 的例子, 假設有一個新的需求: "樂透開奬不是每天開, 而是固定每個月的 5 日開獎一次".  
因此, 程式要加一個當前日期時間的判斷, 若為 5 日才可開獎. 若為其它日期, 就不可開獎, 回覆警告訊息.  
這個邏輯要如何測試呢? 總不能等到每個月 5 日, 才來測試吧.  

以下係採 參考文件2.. 的 "An interface that wraps the DateTime.Now" 方式進行演練及實作.  

完整範例可由 GitHub 下載.

## 演練細節

### 步驟_1: 加入 IDateTimeProvider 介面 及 DateTimeProvider 類別

```csharp
public interface IDateTimeProvider
{
    DateTime GetCurrentTime();
}

public class DateTimeProvider : IDateTimeProvider
{
    public DateTime GetCurrentTime()
    {
        return DateTime.Now;
    }
}
```

### 步驟_2: 將 IDateTimeProvider 註冊至 DI container
```csharp
#region 註冊相關的服務
builder.Services.AddSingleton<IRandomGenerator, RandomGenerator>();
builder.Services.AddScoped<ILottoService, LottoService>();
builder.Services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
#endregion
```

### 步驟_3: 修改 LottoService 的處理邏輯

1.. 修改建構子, 加入 IDateTimeProvider 物件的注入.  

```csharp
private readonly IRandomGenerator _randomGenerator;
private readonly IDateTimeProvider _dateTimeProvider;

public LottoService(IRandomGenerator randomGenerator, IDateTimeProvider dateTimeProvider) 
{
    _randomGenerator = randomGenerator;
    _dateTimeProvider = dateTimeProvider;
}
```

2.. 修改 Lottoing() 方法, 加入是否為每個月 5 日的判斷.  
```csharp
public LottoViewModel Lottoing(int min, int max)
{

    var result = new LottoViewModel();
    var now = _dateTimeProvider.GetCurrentTime();

    if (now.Day != 5)
    {
        result.YourNumber = -1;
        result.Message = "非每個月5日, 不開獎";
        return result;
    }

    // Random(min, max): 含下界, 不含上界
    var yourNumber = _randomGenerator.Next(min, max);
    // 只要餘數是 9, 就代表中獎
    var message = (yourNumber % 10 == 9) ? "恭喜中獎" : "再接再厲";

    result.YourNumber = yourNumber;
    result.Message = message;

    return result;
}
```

### 步驟_4: 修改原有的測試案例

1.. 因為 LottoService 的建構子增加了 IDataTimeProvider 這個參數, 所以, 原有的測試案例, 也要跟著改, 不然會編譯失敗.  

```csharp
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
```

```csharp
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
```

### 步驟_5: 針對不開獎的日期, 建立測試案例



## 結論

 

## 參考文件

* <a href="https://columns.chicken-house.net/2022/05/29/datetime-mock/" target="_blank">1.. (安德魯的部落格) [架構師的修練] - 從 DateTime 的 Mock 技巧談 PoC 的應用</a>  
* <a href="https://methodpoet.com/unit-testing-datetime-now/" target="_blank">2.. 4 Golden Strategies for Unit Testing DateTime.Now in C#</a>  
> The most popular strategies for unit testing DateTime.Now in C# are:  
> * An interface that wraps the DateTime.Now  
> * SystemTime static class  
> * Ambient context approach  
> * DateTime property on a class  

* <a href="https://learn.microsoft.com/en-us/dotnet/api/system.datetime?view=net-6.0" target="_blank">3.. (Microsoft Learn) DateTime Struct</a>  
* <a href="https://learn.microsoft.com/en-us/dotnet/api/system.datetime.today?view=net-6.0" target="_blank">4.. (Microsoft Learn) DateTime.Today Property</a>  
* <a href="https://learn.microsoft.com/en-us/dotnet/api/system.datetime.now?view=net-6.0" target="_blank">5.. (Microsoft Learn) DateTime.Now Property</a>  


