namespace JobBoard.Client.Static
{
    public class Endpoints
    {
        private static readonly string Prefix = "api";

        public static readonly string CompaniesEndpoint = $"{Prefix}/companies";
        public static readonly string EmployersEndpoint = $"{Prefix}/employers";
        public static readonly string ApplicationsEndpoint = $"{Prefix}/applications";
        public static readonly string JobsEndpoint = $"{Prefix}/jobs";
        public static readonly string MessagesEndpoint = $"{Prefix}/messages";
        public static readonly string JobSeekersEndpoint = $"{Prefix}/jobSeekers";

    }
}
