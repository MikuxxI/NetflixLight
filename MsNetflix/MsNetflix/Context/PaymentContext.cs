using Payment_service.Model;
using Microsoft.EntityFrameworkCore;

namespace Payment_service.Context;

public class PaymentContext : DbContext
{
    public DbSet<User>? Users { get; set; }

    public PaymentContext(DbContextOptions<PaymentContext> options) : base(options)
    {

    }
}