using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace QuizBeeApp.Mobile.Interfaces
{
    public interface IRequestHandler
    {
        //it would be better if the type of calling class will be determine without calling init
        void Init(object callingObject);
        void AddToken(string token);
        void ClearToken();

        //responsible for cancelling pending request
        void CancelRequest();

        Task<T> PutAsync<T>(string uriReequest);
        Task<T> PutAsync<T, Param>(string uriReequest, Param param);

        Task<T> PostAsync<T, Param>(string uriReequest, Param param);
        Task<T> PostAsync<T>(string uriReequest);
        Task<T> PostAsync<T>(string uriReequest, MultipartFormDataContent content);

        Task<T> GetAsync<T>(string uriRequest, bool cachedResult = false, int days = 7);


        Task<bool> DeleteAsync(string uriReequest);
    }
}
