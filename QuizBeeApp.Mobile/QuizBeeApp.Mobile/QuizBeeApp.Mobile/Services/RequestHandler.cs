using QuizBeeApp.Mobile.Interfaces;
using QuizBeeApp.Mobile.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace QuizBeeApp.Mobile.Services
{
    class RequestHandler : IRequestHandler, IDisposable
    {
        private readonly IApiAccess _apiAccess;

        //interfaces to be ignore when generating exceptions
        private readonly List<string> _interfacesToIgnore = new List<string>
        {
           "IRequestCancellation"
        };


        //private const int FIRST_INTERFACES_CALLIN_SERVICE_DERIVED_FROM = 0;

        private object _callingObject = null;

        private CancellationTokenSource _cancellationTokenSource;

        public RequestHandler(IApiAccess apiAccess)
        {
            _apiAccess = apiAccess;

            _cancellationTokenSource = new CancellationTokenSource();
        }

        //it would be better if the type of calling class will be determine without calling init
        public void Init(object callingObject)
        {
            _callingObject = callingObject;
        }

        public void CancelRequest()
        {
            _cancellationTokenSource.Cancel();
        }



        #region DELETE

        public async Task<bool> DeleteAsync(string uriReequest)
        {
            CheckIfInitHasBeenSet();

            try
            {
                var response = await _apiAccess.DeleteForResponseAsync<bool>(uriReequest, _cancellationTokenSource.Token);
                CheckResponseStatus("DELETE", uriReequest, response);
                return response.IsSuccess;
            }
            catch (OperationCanceledException)
            {
                return true;
            }

        }

        #endregion

        #region GET

        public async Task<T> GetAsync<T>(string uriRequest, bool cachedResult = false, int days = 7)
        {
            CheckIfInitHasBeenSet();

            try
            {
                var response = await _apiAccess.GetResopnseAsync<T>(uriRequest, cachedResult, days, _cancellationTokenSource.Token);

                CheckResponseStatus("GET", uriRequest, response);

                return response.Result;

            }
            catch (OperationCanceledException)
            {
                return default;
            }
        }

        #endregion



        #region POST
        public async Task<T> PostAsync<T, Param>(string uriReequest, Param param)
        {
            CheckIfInitHasBeenSet();

            try
            {
                var response = await _apiAccess.PostForResponseAsync<T, Param>(uriReequest, param, _cancellationTokenSource.Token);

                CheckResponseStatus("POST", uriReequest, param, response);

                return response.Result;

            }
            catch (OperationCanceledException)
            {
                return default;
            }
        }

        public async Task<T> PostAsync<T>(string uriReequest, MultipartFormDataContent content)
        {
            CheckIfInitHasBeenSet();

            try
            {

                var response = await _apiAccess.PostMultiPartFormAsync<T>(uriReequest, content, _cancellationTokenSource.Token);

                CheckResponseStatus("POST", uriReequest, response);

                return response.Result;

            }
            catch (OperationCanceledException)
            {
                return default;
            }

        }

        public async Task<T> PostAsync<T>(string uriReequest)
        {
            CheckIfInitHasBeenSet();

            try
            {

                var response = await _apiAccess.PostForResponseAsync<T>(uriReequest, _cancellationTokenSource.Token);

                CheckResponseStatus("POST", uriReequest, response);

                return response.Result;

            }
            catch (OperationCanceledException)
            {
                return default;
            }
        }


        #endregion



        #region PUT

        public async Task<T> PutAsync<T>(string uriReequest)
        {
            CheckIfInitHasBeenSet();

            try
            {
                var response = await _apiAccess.PutForGenericResponseAsync<T>(uriReequest, _cancellationTokenSource.Token);

                CheckResponseStatus("PUT", uriReequest, response);

                return response.Result;

            }
            catch (OperationCanceledException)
            {
                return default;
            }

        }

        public async Task<T> PutAsync<T, Param>(string uriReequest, Param param)
        {
            CheckIfInitHasBeenSet();

            try
            {
                var response = await _apiAccess.PutForGenericResponseAsync<T, Param>(uriReequest, param, _cancellationTokenSource.Token);

                CheckResponseStatus("PUT", uriReequest, response);

                return response.Result;

            }
            catch (OperationCanceledException)
            {
                return default;
            }
        }
        #endregion

        #region ExceptionThrower

        private void CheckResponseStatus(string RequestMethod, string uriRequest, BaseResponse response)
        {
            if (!response.IsSuccess)
                ThrowException(RequestMethod, response.ErrorResult.Message, uriRequest, response.StatusCode);
        }

        private void CheckResponseStatus<Payload>(string RequestMethod, string uriRequest, Payload payload, BaseResponse response)
        {
            if (!response.IsSuccess)
                ThrowException(RequestMethod, response.ErrorResult.Message, uriRequest, response.StatusCode, payload);
        }

        void ThrowException<Payload>(string RequestMethod, string ErrorMessage, string UriRequest, int responseCode, Payload payload)
        {
            var _payload = Newtonsoft.Json.JsonConvert.SerializeObject(payload);

            ThrowException(RequestMethod, ErrorMessage, UriRequest, responseCode, _payload);
        }

        void ThrowException(string RequestMethod, string ErrorMessage, string UriRequest, int responseCode, string payload = "NONE")
        {
            var callingInerface = ExtractCallingServicesInterface();
            switch (callingInerface)
            {
                //case nameof(IAuthenticate):
                //    throw new AuthenticationServiceException(RequestMethod, ErrorMessage, UriRequest, payload, responseCode);
                case nameof(IParticipantService):
                    throw new ParticipantServiceException(RequestMethod, ErrorMessage, UriRequest, payload, responseCode);
                //case nameof(ITeacherService):
                //    throw new TeacherServiceException(RequestMethod, ErrorMessage, UriRequest, payload, responseCode);
                //case nameof(IExperience):
                //    throw new ExperienceServiceException(RequestMethod, ErrorMessage, UriRequest, payload, responseCode);
                //case nameof(ICategory):
                //    throw new CategoryServiceException(RequestMethod, ErrorMessage, UriRequest, payload, responseCode);
                //case nameof(IConfiguration):
                //    throw new ConfigurationServiceException(RequestMethod, ErrorMessage, UriRequest, payload, responseCode);
                //case nameof(INotification):
                //    throw new NotificationServiceException(RequestMethod, ErrorMessage, UriRequest, payload, responseCode);
                //case nameof(IPhoto):
                //    throw new PhotoServiceException(RequestMethod, ErrorMessage, UriRequest, payload, responseCode);
                //case nameof(ISettings):
                //    throw new SettingsServiceException(RequestMethod, ErrorMessage, UriRequest, payload, responseCode);
                //case nameof(IRoleService):
                //    throw new RoleServiceException(RequestMethod, ErrorMessage, UriRequest, payload, responseCode);
                default:
                    throw new Exception(ErrorMessage);
            }
        }

        private string ExtractCallingServicesInterface()
        {

            var callingObjectsInterfaces = _callingObject.GetType().GetInterfaces();//[FIRST_INTERFACES_CALLIN_SERVICE_DERIVED_FROM];

            int interfaceCount = callingObjectsInterfaces.Length;

            for (int ind = 0; ind < interfaceCount; ind++)
            {
                if (!_interfacesToIgnore.Contains(callingObjectsInterfaces[ind].Name))
                    return callingObjectsInterfaces[ind].Name;
            }

            return null;

        }


        void CheckIfInitHasBeenSet()
        {
            if (_callingObject == null)
                throw new NotImplementedException("Init(this);  needs to be call first before using RequestHandler");
        }

        #endregion

        public void Dispose()
        {
            _cancellationTokenSource.Dispose();
        }

        public void AddToken(string token)
        {
            _apiAccess.SetValidationHeader(token);
        }

        public void ClearToken()
        {
            _apiAccess.ClearValidationHeader();
        }
    }
}
