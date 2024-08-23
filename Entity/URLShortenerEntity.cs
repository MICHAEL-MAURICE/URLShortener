using System.ComponentModel.DataAnnotations;

namespace URLShortener.Entity
{
    public class URLShortenerEntity
    {
        public int Id { get; set; }
        [Url]
        public string ShortURl { get; set; } = "";
        [Url]
        public string LongURl { get; set; } = "";

    }
}
