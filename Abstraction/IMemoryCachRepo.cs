using Microsoft.AspNetCore.Mvc;

namespace URLShortener.Abstraction
{
    public interface IMemoryCachRepo
    {
        string Get(string key);
        string Set(string key, string value);
    }
}
