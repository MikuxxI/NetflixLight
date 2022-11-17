using user_service.Context;
using user_service.Controllers.Request;
using user_service.Controllers.Response;
using user_service.Messaging;
using user_service.Model;
using Microsoft.AspNetCore.Mvc.Core;
using Polly;
using Steeltoe.Messaging.RabbitMQ.Core;
using Microsoft.AspNetCore.Mvc;

namespace user_service.Controllers
{

    [ApiController]
    [Route("")]
    public class UserApiController : ControllerBase
    {
        private readonly ILogger<UserApiController> _logger;
        private readonly UserContext _userContext;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly HttpClient _httpClient;
       // private readonly RabbitTemplate _rabbitTemplate;

        public UserApiController(ILogger<UserApiController> logger,
                                        UserContext userContext,
                                        IHttpClientFactory httpClientFactory
                                       /* RabbitTemplate rabbitTemplate*/)
        {
            _logger = logger;
            _userContext = userContext;
            _httpClientFactory = httpClientFactory;
            _httpClient = httpClientFactory.CreateClient("user-service");
           // _rabbitTemplate = rabbitTemplate;
        }

        [HttpGet("{id}")]
        public async Task<UserResponse> FindById([FromRoute] int id)
        {
            User user = this._userContext.Users.First(c => c.Id == id);

            var fallbackForAnyException = Policy<string>
                .Handle<Exception>()
                .FallbackAsync(async (ct) => "- Inconnu -");

            // string produitNom = await _httpClient.GetStringAsync($"/info/{ commentaire.ProduitId }/nom");

            string userName = await fallbackForAnyException.ExecuteAsync(async () => {
                return await _httpClient.GetStringAsync($"/{user.Id}/nom");
            });

            return new UserResponse
            {
                Id = user.Id,
                Firstname = user.Firstname,
                Lastname = user.Lastname,
                Username = user.Username,
                Password = user.Password,
                Sold = user.Sold,
                
            };
        }


        [HttpPost]
        public async Task<IActionResult> Add(UserRequest userRequest)
        {
            User user = new User
            {
                Firstname = userRequest.Firstname,
                Lastname = userRequest.Lastname,
                Username = userRequest.Username,
                Password = userRequest.Password,
                Sold = userRequest.Sold,
                //Etat = CommentaireEtat.PENDING
            };

            this._userContext.Users.Add(user);
            this._userContext.SaveChanges();

          /*  _rabbitTemplate.ConvertAndSend("ms.user", new UserDetails
            {
                UserId = user.Id,
            });*/

            return Ok(user.Id);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit([FromRoute] int id, UserRequest userRequest)
        {
            User user = this._userContext.Users.First(c => c.Id == id);

            if (user == null)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }

            user.Firstname = userRequest.Firstname;
            user.Lastname = userRequest.Lastname;
            user.Username = userRequest.Username;
            user.Password = userRequest.Password;
            user.Sold = user.Sold;

            this._userContext.SaveChanges();

            return Ok(user.Id);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteById([FromRoute] int id)
        {
            try
            {
                this._userContext.Users.Remove(new User() { Id = id });
                this._userContext.SaveChanges();

                return Ok(true);
            }

            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        private async Task<Boolean> isAdmin(int userId)
        {
            var fallbackForAnyException = Policy<Boolean>
                .Handle<Exception>()
                .FallbackAsync(async (ct) => false);

            // return await _httpClient.GetFromJsonAsync<Boolean>($"/info/{ commentaireRequest.ProduitId }/notable");

            return await fallbackForAnyException.ExecuteAsync(async () => {
                return await _httpClient.GetFromJsonAsync<Boolean>($"/{userId}/admin");
            });
        }

        private int validateAdmin(int admin)
        {
            if (admin < 0)
            {
                return 0;
            }

            if (admin > 1)
            {
                return 1;
            }

            return admin;
        }
    }
}
