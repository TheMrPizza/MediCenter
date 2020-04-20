using System.Collections.Generic;
using Common;

namespace Server.Services
{
    public interface IService<TModel>
        where TModel : IModel
    {
        List<TModel> GetAll();
        TModel Get(string id);
        TModel Add(TModel model);
        void Update(string id, TModel model);
        void Remove(string id);
        void Remove(TModel model);
    }
}
