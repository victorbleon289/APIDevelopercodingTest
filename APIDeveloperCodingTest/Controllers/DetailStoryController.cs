using APIDeveloperCodingCore.DTOs.Response;
using APIDeveloperCodingCore.Interfaces;
using APIDeveloperCodingCore.POCO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APIDeveloperCodingTest.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class DetailStoryController : ControllerBase
    {

        private IDetailStoryBussines _detailStoryBussines;

        private IConfiguration _configuration;



        /// <summary>
        /// 
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="detailStoryBussines"></param>
        public DetailStoryController(IConfiguration configuration, IDetailStoryBussines detailStoryBussines)
        {
            _detailStoryBussines = detailStoryBussines;
            _configuration = configuration;
        }

  
        /// <summary>
        /// 
        /// </summary>
        /// <param name="limit"></param>
        /// <returns></returns>
        [HttpGet("{limit}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DetailStoryResponseDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Get(int limit)
        {
            IEnumerable<DetailStoryResponseDto?> detailStoryResponseDto;

            try
            {
                ConfigurationService _configurationService = new ConfigurationService()
                {
                    BaseAddress = _configuration.GetSection("AdminApiConfiguration")?.GetSection("Url").Value.ToString(),
                    EndPoint = _configuration.GetSection("AdminApiConfiguration").GetSection("BestStories").Value.ToString()
                };

                ConfigurationService configurationServiceDetail = new ConfigurationService()
                {
                    BaseAddress = _configuration.GetSection("AdminApiConfiguration").GetSection("Url").Value.ToString(),
                    EndPoint = _configuration.GetSection("AdminApiConfiguration").GetSection("ItemStory").Value.ToString()
                };


                detailStoryResponseDto = await _detailStoryBussines.GetDetailStory<IEnumerable<DetailStoryResponseDto>>(_configurationService, configurationServiceDetail, limit);

                if (detailStoryResponseDto == null)
                    return NotFound();

                return Ok(detailStoryResponseDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


    }
}
