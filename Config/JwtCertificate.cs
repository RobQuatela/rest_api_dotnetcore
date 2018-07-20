namespace rest_api_dotnetcore.Config
{
    public class JwtCertificate
    {
        public string Secret { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
    }
}