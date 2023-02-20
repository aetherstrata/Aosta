namespace Aosta.GUI.Services
{
    class SecureStorageService : ISecureStorageService
    {
        public async Task<string> Get(string key) =>
          await SecureStorage.Default.GetAsync(key);

        public async Task Save(string Key, string value) =>
          await SecureStorage.Default.SetAsync(Key, value);
    }
}
