namespace KartoshkaEvent.Application.Interfaces
{
    public interface IConfirmationService
    {
        string GenerateConfirmationCode();
        string GenerateConfirmationToken();
        string GenerateRecoveryToken();
    }
}
