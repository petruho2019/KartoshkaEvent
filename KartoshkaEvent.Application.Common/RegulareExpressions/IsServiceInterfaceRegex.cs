using System.Text.RegularExpressions;

namespace KartoshkaEvent.Application.Common.RegulareExpressions
{
    public static partial class IsServiceInterfaceRegex
    {
        [GeneratedRegex("I.*Service")]
        private static partial Regex MatchIsServiceInterface();

        public static bool IsServiceInterface(string serviceName) => MatchIsServiceInterface().IsMatch(serviceName);
    }
}
