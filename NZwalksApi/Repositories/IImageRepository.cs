using NZwalksApi.Models.Domain;

namespace NZwalksApi.Repositories
{
    public interface IImageRepository
    {
        Task<Image> Upload(Image image);
    }
}
