using Models.ViewModels.Config;

namespace Aqua_Sharp_Backend.Interfaces
{
    public interface IConfigService
    {
        Task Configure();
        Task ChangePassword();
        Task ChangeQuestion();
        string GenerateJwt(LoginViewModel vm);

    }
}
