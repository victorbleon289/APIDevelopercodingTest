using APIDeveloperCodingCore.DTOs.Response;
using APIDeveloperCodingCore.Interfaces;
using APIDeveloperCodingCore.POCO;

namespace APIDeveloperCodingBussines
{

    /// <summary>
    /// 
    /// </summary>
    public class DetailStoryBussines : IDetailStoryBussines
    {

        private readonly IHttpService _httpService;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="httpService"></param>
        public DetailStoryBussines(IHttpService httpService)
        {
            _httpService = httpService;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="configurationServiceStory"></param>
        /// <param name="configurationServiceDetail"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        public async Task<T?> GetDetailStory<T>(ConfigurationService configurationServiceStory, ConfigurationService configurationServiceDetail, int limit)
        {
            HttpRequest httpRequest = new HttpRequest()
            {
                BaseAddress = configurationServiceStory.BaseAddress,
                EndPoint = configurationServiceStory.EndPoint
            };

            ServiceResponse? serviceResponse = await _httpService.GetAsync<ServiceResponse>(httpRequest);
            IEnumerable<DetailsResponseDto> listDetailResponse = new List<DetailsResponseDto>();


            if (serviceResponse?.CodeStatus == 200)
            {
                IEnumerable<int>? responseUserMaasAppDto = System.Text.Json.JsonSerializer.Deserialize<IEnumerable<int>>(serviceResponse.Data);

                if(responseUserMaasAppDto == null)
                    return (T?)(object)null;


                foreach (int story in responseUserMaasAppDto)
                {
                    DetailsResponseDto? detailsResponseDto = await GetInformationDetailStory<DetailsResponseDto>(configurationServiceDetail, story);

                    if(detailsResponseDto != null)
                        ((List<DetailsResponseDto>)listDetailResponse).Add(detailsResponseDto);
                }

                IEnumerable<DetailsResponseDto> limitList = listDetailResponse
                                                            .OrderByDescending(detail => detail.Score)
                                                            .Take(limit);

                TimeSpan ts = TimeSpan.FromTicks(1745837458);
                var val = DateTime.SpecifyKind(DateTime.Now.Add(ts), DateTimeKind.Utc);

                IEnumerable<DetailStoryResponseDto> result = from detailList in limitList
                             select new DetailStoryResponseDto()
                             {
                                  CommentCount = detailList.Kids.Count(),
                                  PostedBy = detailList.By,
                                  Score = detailList.Score,
                                 Time = DateTime.SpecifyKind(DateTime.Now.Add(TimeSpan.FromTicks(detailList.Time)), DateTimeKind.Utc).ToString("yyyy-MM-ddThh:mm:ss+00:00"),
                                 Title = detailList.Title,
                                  Uri = detailList.Url
                             };


                return (T)(object)result;
            }

            return (T?)(object)null;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="configurationServiceDetail"></param>
        /// <param name="story"></param>
        /// <returns></returns>
        private async Task<T?> GetInformationDetailStory<T>(ConfigurationService configurationServiceDetail, int story)
        {
            HttpRequest httpRequest = new HttpRequest()
            {
                BaseAddress = configurationServiceDetail.BaseAddress,
                EndPoint = $"{configurationServiceDetail.EndPoint}{story}.json"
            };

            ServiceResponse? serviceResponse = await _httpService.GetAsync<ServiceResponse>(httpRequest);

            if (serviceResponse?.CodeStatus == 200)
            {
                T? response = System.Text.Json.JsonSerializer.Deserialize<T>(serviceResponse.Data);

                if(response != null)
                    return (T?)(object)response;
            }


            return (T?)(object)null;
        }
    }


}
