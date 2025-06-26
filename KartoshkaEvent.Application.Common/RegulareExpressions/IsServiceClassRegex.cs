using System.Text.RegularExpressions;

namespace KartoshkaEvent.Application.Common.RegulareExpressions
{
    public static partial class IsServiceClassRegex
    {
        [GeneratedRegex(@".*Service")]
        private static partial Regex MatchIsService();

        public static bool IsService(string className) => MatchIsService().IsMatch(className);
    }
}
