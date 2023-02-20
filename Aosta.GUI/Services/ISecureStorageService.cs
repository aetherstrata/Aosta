namespace Aosta.GUI.Services
{
    interface ISecureStorageService
    {
        Task Save(string key, string value);

        Task<string> Get(string key);
    }
}
