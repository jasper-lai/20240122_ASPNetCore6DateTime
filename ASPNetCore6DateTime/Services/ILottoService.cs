namespace ASPNetCore6DateTime.Services
{
    using ASPNetCore6DateTime.ViewModels;

    public interface ILottoService
    {
        LottoViewModel Lottoing(int min, int max);
    }
}
