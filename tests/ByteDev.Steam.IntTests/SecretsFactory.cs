using ByteDev.Testing.Setting;
using ByteDev.Testing.Setting.Providers;

namespace ByteDev.Steam.IntTests;

internal static class SecretsFactory
{
    public static string GetApiKey()
    {
        var testSetting = new TestSetting();

        testSetting.AddProvider(new FileSettingProvider(new[]
        {
            @"C:\Dev\Secrets\SteamApi-ByteDevSteam.apikey"
        }));

        return testSetting.GetSetting();
    }
}