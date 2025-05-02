using System.ComponentModel.Design;
using System.Net.Http.Headers;
using System.Text;

using APIDeveloperCodingCore.Interfaces;
using APIDeveloperCodingCore.POCO;


namespace APIDeveloperCodingInfrastructure
{
    /// <summary>
    /// 
    /// </summary>
    public class HttpService : IHttpService
    {

        /// <summary>
        /// 
        /// </summary>
        private HttpClient _httpClient;
        

        /// <summary>
        /// 
        /// </summary>
        public HttpService()
        {
            
        }


        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="httpResponseMessage"></param>
        /// <returns></returns>
        private async Task<T> ProcessResponse<T>(HttpResponseMessage httpResponseMessage) where T : ServiceResponse, new()
        {
            T responseModel = new T();

            string strResponse = await httpResponseMessage.Content.ReadAsStringAsync();
            responseModel.Data = strResponse;
            responseModel.CodeStatus = (int)httpResponseMessage.StatusCode;
            
            return responseModel;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="httpRequest"></param>
        /// <returns></returns>
        public async Task<T?> GetAsync<T>(HttpRequest httpRequest) where T : ServiceResponse, new()
        {
            HttpResponseMessage? respuestaServicio = null;
 
            _httpClient = new HttpClient();

            _httpClient.BaseAddress = new Uri(httpRequest.BaseAddress);

            try
            {
                CreateHeaders(httpRequest);

                respuestaServicio = await _httpClient.GetAsync(httpRequest.EndPoint);

                return await ProcessResponse<T>(respuestaServicio);
            }
            catch (Exception ex)
            {
                //logger.LogError("Error: {Error}", ex);
                return null;
            }
        }

        public async Task<T?> PostAsync<T>(HttpRequest httpRequest) where T : ServiceResponse, new()
        {
            HttpResponseMessage? respuestaServicio = null;

            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(httpRequest.BaseAddress);

            try
            {
                StringContent? jsonContent = CreateContent(httpRequest);

                respuestaServicio = await _httpClient.PostAsync(httpRequest.EndPoint, jsonContent);

                return await ProcessResponse<T>(respuestaServicio);
            }
            catch (Exception ex)
            {
                //logger.LogError("Error: {Error}", ex);
                return null;
            }
        }






        private void CreateHeaders(HttpRequest httpRequest)
        {
            if (!String.IsNullOrEmpty(httpRequest.Token))
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", httpRequest.Token);
        }


        private static StringContent? CreateContent(HttpRequest httpRequest)
        {
            if (httpRequest.SerializeData != null)
            {
                using StringContent jsonContent = new(System.Text.Json.JsonSerializer.Serialize(httpRequest.SerializeData),
                       Encoding.UTF8,
                       "application/json");

                return jsonContent;
            }

            return null;
        }


    }
}
