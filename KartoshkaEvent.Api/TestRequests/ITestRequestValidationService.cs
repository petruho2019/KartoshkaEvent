namespace KartoshkaEvent.Api.TestRequests
{
    public interface ITestRequestValidationService
    {
        bool IsTestOrganizerRequest(HttpRequest request);
        bool IsTestVisitorRequest(HttpRequest request);

        string GenerateOrganizerToken();
        string GenerateVisitorToken();

    }
}
