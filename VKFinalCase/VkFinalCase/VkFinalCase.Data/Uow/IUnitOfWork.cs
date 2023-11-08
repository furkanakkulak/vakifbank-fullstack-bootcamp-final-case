using VkFinalCase.Data.Domain;
using VkFinalCase.Data.Repository;

namespace VkFinalCase.Data.Uow;

public interface IUnitOfWork
{
    void Complete();
    void CompleteTransaction();
    
    IGenericRepository<User> UserRepository { get; }
    IGenericRepository<Dealer> DealerRepository { get; }
    IGenericRepository<Product> ProductRepository { get; }
    IGenericRepository<Order> OrderRepository { get; }
    IGenericRepository<OrderPayment> OrderPaymentRepository { get; }
    IGenericRepository<PaymentMethod>PaymentMethodRepository { get; }
    IGenericRepository<Message>MessageRepository { get; }
}