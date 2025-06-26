using System.Net;

namespace KartoshkaEvent.Domain.Common.Extensions
{
    public static class GetIntEnumExtension
    {
        public static int GetInt(this HttpStatusCode statusCode)
        {
            return (int)statusCode;
        }
    }
}
