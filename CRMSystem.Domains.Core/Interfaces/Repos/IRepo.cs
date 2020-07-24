using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CRMSystem.Domains
{
    public interface IRepo<T>
    {
        Task<int> insertAsync(T data);
        Task<int> insertListAsync(List<T> data);
        Task deleteAsync(int ID);

        Task deleteAllAsync(List<T> data);
        Task<int> updateAsync(T data);
        Task<T> getAsync(int ID);
        Task<List<T>> getAllAsync();
       
        // Task<List<T>> getAllByIDAsync(int ID);



    }
}
