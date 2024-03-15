namespace EventPro.Application;

public static class Configuration
{
    public static SecretsConfiguration Secrets { get; set; } = new();

    public class SecretsConfiguration
    {
        public string TokenKey { get; set; } = string.Empty;
    }

}