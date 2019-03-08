using Newtonsoft.Json;
using QuizBeeApp.Mobile.Interfaces;
using QuizBeeApp.Mobile.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace QuizBeeApp.Mobile.Services
{
    public class ApiAccess : IApiAccess
    {
        private readonly HttpClient _client;

        public ApiAccess()
        {
            _client = new HttpClient();
        }

        public string BaseUri
        {
            get { return _client.BaseAddress.AbsoluteUri; }
            set
            {
                _client.BaseAddress = new Uri(value);
            }
        }


        public string Token => _client.DefaultRequestHeaders.GetValues("Authorization").First();


        public void SetValidationHeader(string token)
        {
            _client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
        }

        public void ClearValidationHeader()
        {
            _client.DefaultRequestHeaders.Remove("Authorization");
        }

        async Task<Response<T>> ExtractResonse<T>(HttpResponseMessage httpResponse, CancellationToken cancellationToken)
        {
            var response = new Response<T>
            {
                IsSuccess = httpResponse.IsSuccessStatusCode,
                StatusCode = (int)httpResponse.StatusCode,
                Message = httpResponse.ReasonPhrase
            };

            cancellationToken.ThrowIfCancellationRequested();
            string result = await httpResponse.Content.ReadAsStringAsync();
            if (response.IsSuccess)
                response.Result = JsonConvert.DeserializeObject<T>(result);
            else
                response.ErrorResult = JsonConvert.DeserializeObject<ErrorResult>(result);

            return response;

        }

        private async Task<Response<T>> ExecuteRequest<T>(Func<Task<HttpResponseMessage>> apiRequest, CancellationToken cancellationToken = default)
        {

            Response<T> response;

            cancellationToken.ThrowIfCancellationRequested();
            try
            {
                var httpResponse = await apiRequest();
                response = await ExtractResonse<T>(httpResponse, cancellationToken);

            }
            catch (OperationCanceledException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                response = Response<T>.GetErrorResponse(ex.Message);
            }

            cancellationToken.ThrowIfCancellationRequested();

            return response;
        }

        #region GET


        public async Task<Response<T>> GetResopnseAsync<T>(string uriRequest, bool cachedResult = false, int days = 2, CancellationToken cancellationToken = default)
        {
            return await ExecuteRequest<T>(async () => await _client.GetAsync(uriRequest, cancellationToken));
        }

        #endregion

        #region POST

        public async Task<Response<T>> PostForResponseAsync<T>(string uriRequest, CancellationToken cancellationToken = default)
        {
            return await ExecuteRequest<T>(async () => await _client.PostAsync(uriRequest, new StringContent(""), cancellationToken));
        }


        public async Task<Response<T>> PostMultiPartFormAsync<T>(string uriRequest, MultipartFormDataContent content, CancellationToken cancellationToken = default)
        {
            return await ExecuteRequest<T>(async () => await _client.PostAsync(uriRequest, content, cancellationToken));
        }



        public async Task<Response<T>> PostForResponseAsync<T, Param>(string uriRequest, Param param, CancellationToken cancellationToken = default)
        {
            string json = JsonConvert.SerializeObject(param);
            HttpContent content = new StringContent(json, Encoding.UTF8, "text/json");
            return await ExecuteRequest<T>(async () => await _client.PostAsync(uriRequest, content, cancellationToken));
        }
        #endregion

        #region PUT

        public async Task<Response<T>> PutForGenericResponseAsync<T, Param>(string uriRequest, Param param, CancellationToken cancellationToken = default)
        {

            string json = JsonConvert.SerializeObject(param);
            HttpContent content = new StringContent(json, Encoding.UTF8, "text/json");
            return await ExecuteRequest<T>(async () => await _client.PutAsync(uriRequest, content, cancellationToken));
        }

        public async Task<Response<T>> PutForGenericResponseAsync<T>(string uriRequest, CancellationToken cancellationToken = default)
        {
            HttpContent content = new StringContent("", Encoding.UTF8, "text/json");
            return await ExecuteRequest<T>(async () => await _client.PutAsync(uriRequest, content, cancellationToken));

        }

        #endregion


        #region DELETE
        public async Task<Response<T>> DeleteForResponseAsync<T>(string uriRequest, CancellationToken cancellationToken = default)
        {
            return await ExecuteRequest<T>(async () => await _client.DeleteAsync(uriRequest, cancellationToken));
        }
        #endregion
    }
}
