namespace KartoshkaEvent.Application.Contracts.Models.Dtos.RecoveryPassword
{
    public class RecoveryDto
    {
        public string Email { get; set; }
        public string RecoveryCode { get; set; }
    }
}
