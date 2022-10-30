using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Animeikan.GUI
{
  class SecureStorageService : ISecureStorageService
  {
    public async Task<string> Get(string key) => 
      await SecureStorage.Default.GetAsync(key);

    public async Task Save(string Key, string value) => 
      await SecureStorage.Default.SetAsync(Key, value);    
  }
}
