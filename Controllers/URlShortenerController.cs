using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using URLShortener.Abstraction;
using URLShortener.Entity;
using URLShortener.Services;

namespace URLShortener.Controllers
{
    public class URlShortenerController : ControllerBase
    {
        private readonly ShortnerURlContext _context;
        private readonly IURLShortenerService _uRLShortener;
        private readonly IMemoryCachRepo _memoryCach;


        public URlShortenerController(ShortnerURlContext context, IURLShortenerService uRLShortener, IMemoryCachRepo memoryCach)
        {
                _context = context;
                _uRLShortener = uRLShortener;
            _memoryCach = memoryCach;
        }


        [HttpPost("api/Shorten")]
        public  async Task<ActionResult<string>> post(string URL,int type,string ?code)
        {
            var ShortUrl=string.Empty;
            if (!Uri.TryCreate(URL,UriKind.Absolute,out _))
            {
                return BadRequest("This Url is Invalid");
            }

            if (type == 0)
            {
                ShortUrl = await _uRLShortener.GenerateURLCode();
            }
            else if (type == 1 && code!=null)
            {

                if (await _uRLShortener.checkTheCode(code) == true) return BadRequest("This Url is Invalid");else ShortUrl = code;
            }
            var urlShotener = new URLShortenerEntity
            {
                LongURl = URL,
                ShortURl=ShortUrl??""
            

            };
            await _context.URLShorteners.AddAsync(urlShotener);
            await  _context.SaveChangesAsync();
            


            return Ok($"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}/{ShortUrl} ");
        }


        [HttpGet("{code}")]

        public async Task<ActionResult> Get(string code)
        {
           
            //get from cach
            var cachedURL = _memoryCach.Get(code);
            if (cachedURL == null)
            {
               var url = await _context.URLShorteners.FirstOrDefaultAsync(url => url.ShortURl == code);
                if (url == null) return NotFound();
                var res = _memoryCach.Set(code, url.LongURl);
                return Redirect(url.LongURl);
            }
           
            return Redirect(cachedURL);
        }
    }
}
