namespace Aqua_Sharp_Backend.Services.ConfigService
{
    public interface IConfigService
    {
        Task Configure();
        Task ChangePassword();
        Task ChangeQuestion();

    }
}
