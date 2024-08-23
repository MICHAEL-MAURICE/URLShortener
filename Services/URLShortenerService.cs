using Microsoft.EntityFrameworkCore;
using URLShortener.Abstraction;
using URLShortener.Entity;

namespace URLShortener.Services
{
    public class URLShortenerService: IURLShortenerService
    {
        private const string Alphapet = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
        private const int NumberOfCharsForShortCode = 7;
        private readonly Random _random = new Random();
        private readonly ShortnerURlContext _context;

        public URLShortenerService(ShortnerURlContext context)
        {
                _context = context;
        }

        public async Task<string> GenerateURLCode()
        {
            while (true)
            {
                var chars = new Char[NumberOfCharsForShortCode];

                for (int i = 0; i < NumberOfCharsForShortCode; i++)
                {
                    var RandomIndex = _random.Next(Alphapet.Length - 1);
                    chars[i] = Alphapet[RandomIndex];
                }
                var code = new string(chars);

                if (!await _context.URLShorteners.AnyAsync(url=>url.ShortURl==code))
                {
                    return code;
                }
            }

        }


        public  async Task<bool> checkTheCode(string code) {

            return await _context.URLShorteners.AnyAsync(url => url.ShortURl == code);
        }
    }
}
