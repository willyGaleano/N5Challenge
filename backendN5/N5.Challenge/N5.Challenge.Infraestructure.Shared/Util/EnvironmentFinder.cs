namespace N5.Challenge.Infraestructure.Shared.Util
{
    public class EnvironmentFinder
    {
        public static readonly string PRODUCTION = "Prod";
        public static readonly string STAGE = "Stage";
        public static readonly string LOCAL = "Local";

        public static readonly string PRODUCTION_FILE = "appsettings.Prod.json";
        public static readonly string STAGE_FILE = "appsettings.Stage.json";
        public static readonly string LOCAL_FILE = "appsettings.Local.json";

        public static string GetEnvironmentName(string environment)
        {
            if (string.IsNullOrEmpty(environment)) return LOCAL;
            return (environment.ToLower()) switch
            {
                "production" => PRODUCTION,
                "stage" => STAGE,
                "local" => LOCAL,
                _ => LOCAL,
            };
        }

        public static string GetConfigurationFileName(string environment)
        {
            if (string.IsNullOrEmpty(environment)) return LOCAL_FILE;
            return (environment.ToLower()) switch
            {
                "production" => PRODUCTION_FILE,
                "stage" => STAGE_FILE,
                "local" => LOCAL_FILE,
                _ => LOCAL_FILE,
            };
        }
    }
}
