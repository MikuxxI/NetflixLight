using category_service.Context;
using category_service.Controllers.Request;
using category_service.Controllers.Response;
using category_service.Models;
using Microsoft.AspNetCore.Mvc;
using Polly;

namespace category_service.Controllers
{
    [ApiController]
    [Route("")]
    public class CategorieApiController : ControllerBase
    {
        private readonly ILogger<CategorieApiController> _logger;
        private readonly CategorieContext _categorieContext;

        public CategorieApiController(ILogger<CategorieApiController> logger, 
                                     CategorieContext categorieContext)
        {
            _logger = logger;
            _categorieContext = categorieContext;
        }

        [HttpGet("{id}")]
        public async Task<CategorieResponse> FindById([FromRoute] int id)
        {
            Categorie categorie = this._categorieContext.Categories.First(c => c.Id == id);

            var fallbackForAnyException = Policy<string>
                .Handle<Exception>()
                .FallbackAsync(async (ct) => "- Inconnu -");

            return new CategorieResponse
            {
                Id = categorie.Id,
                Name = categorie.Name
            };
        }

        [HttpPost]
        public async Task<IActionResult> Add(CategorieRequest categorieRequest)
        {

            Categorie categorie = new Categorie
            {
                Name = categorieRequest.Name
            };

            this._categorieContext.Categories.Add(categorie);
            this._categorieContext.SaveChanges();

            return Ok(categorie.Id);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit([FromRoute] int id, CategorieRequest categorieRequest)
        {
            Categorie categorie = this._categorieContext.Categories.First(c => c.Id == id);
           
            if (categorie == null)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }

            categorie.Name = categorieRequest.Name;
 
            this._categorieContext.SaveChanges();

            return Ok(categorie.Id);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteById([FromRoute] int id)
        {
            try
            {
                this._categorieContext.Categories.Remove(new Categorie() { Id = id });
                this._categorieContext.SaveChanges();

                return Ok(true);
            }

            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
