using AutoMapper;
using VkFinalCase.Data.Domain;
using VkFinalCase.Schema;

namespace VkFinalCase.Operation.Mapper;

public class MapperConfig : Profile
{
    public MapperConfig()
    {
        CreateMap<DealerRequest, Dealer>();
        CreateMap<Dealer, DealerResponse>();

        CreateMap<MessageRequest, Message>();
        CreateMap<Message, MessageResponse>();

        CreateMap<OrderRequest, Order>();
        CreateMap<Order, OrderResponse>();

        CreateMap<OrderPaymentRequest, OrderPayment>();
        CreateMap<OrderPayment, OrderPaymentResponse>();

        CreateMap<PaymentMethodRequest, PaymentMethod>();
        CreateMap<PaymentMethod, PaymentMethodResponse>();

        CreateMap<ProductRequest, Product>();
        CreateMap<Product, ProductResponse>();

        CreateMap<TokenRequest, TokenResponse>();
        CreateMap<TokenResponse, TokenRequest>();

        CreateMap<UserRequest, User>();
        CreateMap<User, UserResponse>();
    }
}
