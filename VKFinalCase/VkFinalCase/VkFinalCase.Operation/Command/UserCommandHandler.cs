using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using VkFinalCase.Base.Encryption;
using VkFinalCase.Base.Response;
using VkFinalCase.Data.Context;
using VkFinalCase.Data.Domain;
using VkFinalCase.Operation.Cqrs;
using VkFinalCase.Schema;

namespace VkFinalCase.Operation.Command;

public class UserCommandHandler : 
    IRequestHandler<CreateUserCommand,ApiResponse<UserResponse>>,
    IRequestHandler<UpdateUserCommand,ApiResponse>,
    IRequestHandler<DeleteUserCommand,ApiResponse>
    
{
    private readonly VkDbContext dbContext;
    private readonly IMapper mapper;

    public UserCommandHandler(VkDbContext dbContext,IMapper mapper)
    {
        this.dbContext = dbContext;
        this.mapper = mapper;
    }

    
    public async Task<ApiResponse<UserResponse>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        User mapped = mapper.Map<User>(request.Model);
        mapped.Password = Md5.Create(request.Model.Password.ToUpper());
        mapped.Role = mapped.Role =="admin" || mapped.Role=="dealer" ? mapped.Role : "dealer";

        var entity = await dbContext.Set<User>().AddAsync(mapped,cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);

        var response = mapper.Map<UserResponse>(entity.Entity);
        return new ApiResponse<UserResponse>(response);
    }

    public async Task<ApiResponse> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var entity = await dbContext.Set<User>().FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        if (entity == null)
        {
            return new ApiResponse("Record not found!");
        }

        entity.Username = request.Model.Username;
        entity.Email = request.Model.Email;
        entity.Password = Md5.Create(request.Model.Password.ToUpper());
        entity.Role = request.Model.Role =="admin" || request.Model.Role=="dealer" ? request.Model.Role : "dealer";

        await dbContext.SaveChangesAsync(cancellationToken);
        return new ApiResponse();
    }

    public async Task<ApiResponse> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        var entity = await dbContext.Set<User>().FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        if (entity == null)
        {
            return new ApiResponse("Record not found!");
        }

        entity.IsActive = false;
        await dbContext.SaveChangesAsync(cancellationToken);
        return new ApiResponse();
    }
}