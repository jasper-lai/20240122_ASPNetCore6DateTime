## 靜態元素 (Static Elements) 的單元測試, 以亂數 (DateTime) 為例
Unit Test for Static Elements (DateTime class) in ASP.NET Core 6 MVC   

## 前言

接續前一篇 <a href="https://www.jasperstudy.com/2024/01/non-deterministic-elements-random.html" target="_blank">樂透開獎</a> 的例子, 假設有一個新的需求: "樂透開奬不是每天開, 而是固定每個月的 5 日開獎一次".  
因此, 程式要加一個當前日期時間的判斷, 若為 5 日才可開獎. 若為其它日期, 就不可開獎, 回覆警告訊息.  
這個邏輯要如何測試呢? 總不能等到每個月 5 日, 才來測試吧.  

以下係採 參考文件2.. 的 "An interface that wraps the DateTime.Now" 方式進行演練及實作.  

完整範例可由 GitHub 下載.

## 演練細節



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


