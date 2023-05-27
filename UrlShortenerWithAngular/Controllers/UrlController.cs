using Microsoft.AspNetCore.Mvc;
using UrlShortenerWithAngular.Data;
using UrlShortenerWithAngular.Models;

namespace UrlShortenerWithAngular.Controllers
{   
    public class UrlController : Controller
    {
        
            private readonly UrlDbContext _context;

            public UrlController(UrlDbContext context)
            {
                _context = context;
            }

            //create a shortened URL       
            [HttpPost("create")]
            public IActionResult CreateShortenedUrl(string originalURL)
            {
                
                string shortCode = GenerateShortCode();

                var url = new Url
                {
                    LongUrl = originalURL,
                    ShortUrl = $"{Request.Scheme}://{Request.Host}/{shortCode}" 
                };

                _context.Urls.Add(url);
                _context.SaveChanges();

                return Ok(url);
            }

            private string GenerateShortCode()
            {
                int count = _context.Urls.Count() + 1; 
                return Base62Encode(count);
            }

            //encode original Url
            private string Base62Encode(int value)
            {
                const string characters = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
                var base62 = "";

                do
                {
                    base62 = characters[value % 62] + base62;
                    value /= 62;
                } while (value > 0);

                return base62;
            }
            //redirection to the original link using short one
            [HttpGet("{shortCode}")]
            public IActionResult RedirectToUrl(string shortCode)
            {
                var url = _context.Urls.FirstOrDefault(u => u.ShortUrl.EndsWith($"/{shortCode}"));
                if (url == null)
                {
                    return NotFound();
                }

                return RedirectPermanent(url.LongUrl);
            }



            [HttpGet("get-url")]

            public IActionResult GetUrl(int id)
            {
                

                var url = _context.Urls.FirstOrDefault(u => u.Id == id);
                if (url == null)
                {
                    return NotFound();
                }

                return Ok(url);
            }

            //retrieve all URLs
            [HttpGet("all")]

            public IActionResult GetAllUrls()
            {
                var urls = _context.Urls.ToList();
                return Ok(urls);
            }


    }
}

