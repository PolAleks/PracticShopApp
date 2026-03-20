namespace OnlineShop.Core.Interfaces.Services
{
    public interface IUserDataMergeService
    {
        Task MergeAnonymousDataAsync(string sourceUserName, string destinationUserName);
    }
}
