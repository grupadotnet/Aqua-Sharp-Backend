namespace Aqua_Sharp_Backend.Authorization
{
    public class AdminConfig
    {
        private static IConfiguration config;
        public static IConfiguration Configuration
        {
            get
            {
                var builder = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.Development.json");
                config = builder.Build();
                return config;
            }
        }
    }
}
