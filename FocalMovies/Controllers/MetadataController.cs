using Focal.Core.DTOS;
using Focal.Core.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace FocalMovies.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MetadataController : ControllerBase
    {
        private readonly IMetaDataService _service;
        public MetadataController(IMetaDataService service) 
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<APIBaseResult<IEnumerable<MetaDataDTO>>>> GetAll()
        {
            try
            {
                return Ok(new APIBaseResult<IEnumerable<MetaDataDTO>>(System.Net.HttpStatusCode.OK) { Data = await _service.GetMetaData() });
            }
            catch (Exception ex)
            {
                return new APIBaseResult<IEnumerable<MetaDataDTO>>(new List<string> { ex.Message }, System.Net.HttpStatusCode.InternalServerError);
            }
        }

        [HttpGet]
        [Route("{movieId}")]
        public async Task<ActionResult<APIBaseResult<IEnumerable<MetaDataDTO>>>> GetById(int movieId)
        {
            try
            {
                var result = await _service.GetById(movieId);
                if (result.Count == 0)
                {
                    return NotFound();
                }
                return Ok(new APIBaseResult<IEnumerable<MetaDataDTO>>(System.Net.HttpStatusCode.OK) { Data = result });
            }
            catch (Exception ex)
            {
                return new APIBaseResult<IEnumerable<MetaDataDTO>>(new List<string> { ex.Message }, System.Net.HttpStatusCode.InternalServerError);
            }
        }

        [HttpPost]
        public async Task<ActionResult<APIBaseResult<MetaDataDTO>>> Add([FromBody] MetaDataDTO data)
        {
            try
            {
                await _service.AddMetaData(data);
                return Ok(new APIBaseResult<MetaDataDTO>(System.Net.HttpStatusCode.OK) { Data = data });
            }
            catch (Exception ex)
            {
                return new APIBaseResult<MetaDataDTO>(new List<string> { ex.Message }, System.Net.HttpStatusCode.InternalServerError);
            }
        }

        [HttpGet]
        [Route("movies/stats")]
        public async Task<ActionResult<APIBaseResult<IEnumerable<MovieStatisticsDTO>>>> stats()
        {
            try
            {
                return Ok(new APIBaseResult<IEnumerable<MovieStatisticsDTO>>(System.Net.HttpStatusCode.OK) { Data = await _service.GetMoviesStatistics()});
            }
            catch (Exception ex)
            {
                return new APIBaseResult<IEnumerable<MovieStatisticsDTO>> (new List<string> { ex.Message }, System.Net.HttpStatusCode.InternalServerError);
            }
        }
    }
}
