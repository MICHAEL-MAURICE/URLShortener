namespace URLShortener.Abstraction
{
    public interface IURLShortenerService
    {
       public Task<string> GenerateURLCode();
       public Task<bool> checkTheCode(string code);
    }
}
