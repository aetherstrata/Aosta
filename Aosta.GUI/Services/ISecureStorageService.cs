namespace Aosta.GUI.Services;

internal interface ISecureStorageService
{
    Task Save(string key, string value);

    Task<string> Get(string key);
}