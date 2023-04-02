namespace Aosta.GUI.Services;

internal class SecureStorageService : ISecureStorageService
{
    public async Task<string> Get(string key)
    {
        return await SecureStorage.Default.GetAsync(key);
    }

    public async Task Save(string key, string value)
    {
        await SecureStorage.Default.SetAsync(key, value);
    }
}