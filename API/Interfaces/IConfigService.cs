namespace Aqua_Sharp_Backend.Interfaces
{
    using Models.ViewModels.Config;
    public interface IConfigService
    {
        Task Configure();
        Task ChangePassword();
        Task ChangeQuestion();
        string GenerateJwt(LoginViewModel vm);

    }
}
