using Focal.Core.DTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Focal.Core.Interfaces.Services
{
    public interface IMetaDataService
    {
        Task<List<MetaDataDTO>> GetMetaData();
        Task<MetaDataDTO> AddMetaData(MetaDataDTO metaDataDTO);
        Task<IEnumerable<MovieStatisticsDTO>> GetMoviesStatistics();
        Task<List<MetaDataDTO>> GetById(int movieId);
    }
}
