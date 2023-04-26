
namespace Fijicare.Interfaces
{
    public interface IPhoto
    {
        System.Threading.Tasks.Task<System.IO.Stream> GetPhoto();
    }
}
