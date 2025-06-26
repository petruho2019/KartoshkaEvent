using KartoshkaEvent.Application.Interfaces;

namespace KartoshkaEvent.Application.Services.Confirmation
{
    public class ConfirmationService : IConfirmationService
    {
        public string GenerateConfirmationCode()
        {
            return new Random().Next(0, 1000000).ToString("D6");
        }

        public string GenerateConfirmationToken()
        {
            return Guid.NewGuid().ToString("N");
        }

        public string GenerateRecoveryToken()
        {
            return Guid.NewGuid().ToString("N");
        }
    }
}
