using Serilog;
using VkFinalCase.Data.Context;
using VkFinalCase.Data.Domain;
using VkFinalCase.Data.Repository;

namespace VkFinalCase.Data.Uow;

public class UnitOfWork : IUnitOfWork
{
    private readonly VkDbContext dbContext;

    public UnitOfWork(VkDbContext dbContext)
    {
        this.dbContext = dbContext;

        UserRepository = new GenericRepository<User>(dbContext);
        DealerRepository = new GenericRepository<Dealer>(dbContext);
        ProductRepository = new GenericRepository<Product>(dbContext);
        OrderRepository = new GenericRepository<Order>(dbContext);
        OrderPaymentRepository = new GenericRepository<OrderPayment>(dbContext);
        PaymentMethodRepository = new GenericRepository<PaymentMethod>(dbContext);
        MessageRepository = new GenericRepository<Message>(dbContext);
    }

    public void Complete()
    {
        dbContext.SaveChanges();
    }

    public void CompleteTransaction()
    {
        using (var transaction = dbContext.Database.BeginTransaction())
        {
            try
            {
                dbContext.SaveChanges();
                transaction.Commit();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                Log.Error("CompleteTransactionError", ex);
            }
        }
    }

    public IGenericRepository<User> UserRepository { get; private set; }
    public IGenericRepository<Dealer> DealerRepository { get; private set; }
    public IGenericRepository<Product> ProductRepository { get; private set;}
    public IGenericRepository<Order> OrderRepository { get; private set;}
    public IGenericRepository<OrderPayment> OrderPaymentRepository { get; private set;}
    public IGenericRepository<PaymentMethod> PaymentMethodRepository { get; private set;}
    public IGenericRepository<Message> MessageRepository { get; private set;}



}