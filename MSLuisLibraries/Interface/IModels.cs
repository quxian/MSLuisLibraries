using MSLuisLibraries.Entities;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace MSLuisLibraries.Interface {
    public interface IModels {
        HttpClient Client { get; set; }
        Uri Uri { get; set; }

        Task<ClosedListEntity[]> GetClosedListEntityAsync();
        Task<ClosedListEntity> GetClosedListEntityByIdAsync(string id);
        Task<bool> UpdateClosedListEntityAsync(string id, ClosedListEntity entity);
        Task<bool> UpdateClosedListSublistAsync(string id, int subId, Sublist subList);
        Task<string> CreateClosedListEntityAsync(ClosedListEntity entity);
        Task<int?> AddClosedListSublistAsync(string id, Sublist subList);
        Task<bool> DeleteClosedListEntityByIdAsync(string id);
    }
}
