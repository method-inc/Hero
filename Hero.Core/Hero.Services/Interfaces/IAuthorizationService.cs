namespace Hero.Services.Interfaces
{
    public interface IAuthorizationService
    {
        bool Authorize(string entity);
    }
}