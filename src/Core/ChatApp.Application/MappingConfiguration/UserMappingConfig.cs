namespace ChatApp.Application.MappingConfiguration;

public class UserMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<AddUserCommandRequest, AppUser>()
            .Ignore(dest => dest.Id)
            .Ignore(dest => dest.ImageUrl)
            .Ignore(dest => dest.LastActive)
            .Ignore(dest => dest.PresenceStatus);
    }
}