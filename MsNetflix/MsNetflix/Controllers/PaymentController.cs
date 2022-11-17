using Payment_service.Context;
using Steeltoe.Messaging.RabbitMQ.Core;
using Microsoft.AspNetCore.Mvc;
using Payment_service.Model;
using Payment_service.Controllers.Request;

namespace Payment_service.Controllers;

[ApiController]
[Route("")]
public class PaymentController : ControllerBase
{
    private readonly ILogger<PaymentController> _logger;
    private readonly PaymentContext _paymentContext;
    private readonly RabbitTemplate _rabbitTemplate;

    public PaymentController(ILogger<PaymentController> logger,
                             PaymentContext paymentContext,
                             RabbitTemplate rabbitTemplate)
    {
        _logger = logger;
        _paymentContext = paymentContext;
        _rabbitTemplate = rabbitTemplate;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="id">Id de l'utilisateur dont on veut mettre le solde à jour</param>
    /// <param name="sold">Montant à ajouter au solde de l'utilisateur</param>
    /// <param name="paymentRequest"></param>
    /// <returns></returns>
    [HttpPut("{id}, {sold}")]
    public IActionResult UpdateSoldOfUser([FromRoute] int id, [FromRoute] double sold, PaymentRequest paymentRequest )
    {
        User user = this._paymentContext.Users.First(u => u.Id == id);

        // Si l'user n'existe pas ou n'à pas été trouvé
        if (user == null)
        {
            return StatusCode(StatusCodes.Status404NotFound);
        }

        user.Sold = sold;
        user.Firstname = paymentRequest.Firstname;
        user.Lastname = paymentRequest.Lastname;
        user.Username = paymentRequest.Username;
        user.Password = paymentRequest.Password;
        user.AdminRole = paymentRequest.AdminRole;

        this._paymentContext.SaveChanges();

        return Ok(user.Id);
    }

}

