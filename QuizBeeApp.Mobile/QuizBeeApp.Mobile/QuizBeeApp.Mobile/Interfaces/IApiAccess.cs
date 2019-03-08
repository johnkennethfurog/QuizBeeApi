using QuizBeeApp.Mobile.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace QuizBeeApp.Mobile.Interfaces
{
    public interface IApiAccess
    {
        string Token { get; }
        string BaseUri { get; set; }

        void SetValidationHeader(string token);
        void ClearValidationHeader();
        #region GET
        Task<Response<T>> GetResopnseAsync<T>(string uriRequest, bool cachedResult = false, int days = 2, CancellationToken cancellationToken = default);
        #endregion

        #region PUT
        Task<Response<T>> PutForGenericResponseAsync<T, Param>(string uriRequest, Param param, CancellationToken cancellationToken = default);
        Task<Response<T>> PutForGenericResponseAsync<T>(string uriRequest, CancellationToken cancellationToken = default);
        #endregion

        #region POST
        Task<Response<T>> PostForResponseAsync<T, Param>(string uriRequest, Param param, CancellationToken cancellationToken = default);
        Task<Response<T>> PostForResponseAsync<T>(string uriRequest, CancellationToken cancellationToken = default);
        Task<Response<T>> PostMultiPartFormAsync<T>(string uriRequest, MultipartFormDataContent content, CancellationToken cancellationToken = default);
        #endregion

        #region DELETE
        //Task<bool> Delete(string uriRequest);
        Task<Response<T>> DeleteForResponseAsync<T>(string uriRequest, CancellationToken cancellationToken = default);
        #endregion
    }
}
