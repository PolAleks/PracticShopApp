namespace OnlineShop.Core.Interfaces.Services
{
    public class UserDataMergeService(ICartService cartService,
                                      IFavoriteService favoriteService,
                                      IComparisonService comparisonService) : IUserDataMergeService
    {
        public async Task MergeAnonymousDataAsync(string sourceUserName, string destinationUserName)
        {
            if (ShouldSkipMerge(sourceUserName, destinationUserName))
                return;

            await cartService.MergeCartAsync(sourceUserName, destinationUserName);
            await favoriteService.MergeFavoriteAsync(sourceUserName, destinationUserName);
            
        }

        private static bool ShouldSkipMerge(string sourceUserName, string destinationUserName)
        {
            return string.IsNullOrEmpty(sourceUserName) ||
                   string.IsNullOrEmpty(destinationUserName) ||
                   sourceUserName == destinationUserName ||
                   !sourceUserName.StartsWith("anonymous_");
        }
    }
}
