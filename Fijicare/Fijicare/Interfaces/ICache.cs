using System;
namespace Fijicare.Interfaces
{
    public interface ICache
    {
        void Save<TModel>(TModel model, string key);
        bool TryLoad<TModel>(string key, out TModel value);
        void Remove(string key);
        void RemoveAll();
    }
}
