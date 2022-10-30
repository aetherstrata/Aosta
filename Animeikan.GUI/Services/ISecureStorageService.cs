using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Animeikan.GUI
{
  interface ISecureStorageService
  {
    Task Save(string key, string value);

    Task<string> Get(string key);  
  }
}
