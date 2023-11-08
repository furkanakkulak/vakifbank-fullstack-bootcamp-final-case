using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using VkFinalCase.Base.Response;
using VkFinalCase.Data.Context;
using VkFinalCase.Data.Domain;
using VkFinalCase.Operation.Cqrs;
using VkFinalCase.Schema;

namespace VkFinalCase.Operation.Query;

public class UserQueryHandler :
    IRequestHandler<GetAllUserQuery, ApiResponse<List<UserResponse>>>,
    IRequestHandler<GetUserByIdQuery, ApiResponse<UserResponse>>
{
    private readonly VkDbContext dbContext;
    private readonly IMapper mapper;

    public UserQueryHandler(VkDbContext dbContext, IMapper mapper)
    {
        this.dbContext = dbContext;
        this.mapper = mapper;
    }


    public async Task<ApiResponse<List<UserResponse>>> Handle(GetAllUserQuery request,
        CancellationToken cancellationToken)
    {
        List<User> list = await dbContext.Set<User>()
            .Include(x => x.Dealer)
            .ToListAsync(cancellationToken);
        
        List<UserResponse> mapped = mapper.Map<List<UserResponse>>(list);
        return new ApiResponse<List<UserResponse>>(mapped);
    }

    public async Task<ApiResponse<UserResponse>> Handle(GetUserByIdQuery request,
        CancellationToken cancellationToken)
    {
        User? entity = await dbContext.Set<User>()
            .Include(x => x.Dealer)
            .FirstOrDefaultAsync(x => x.Id == request.Id,cancellationToken);
        
        if (entity == null)
        {
            return new ApiResponse<UserResponse>("Record not found!");
        }
        
        UserResponse mapped = mapper.Map<UserResponse>(entity);
        return new ApiResponse<UserResponse>(mapped);
    }
}