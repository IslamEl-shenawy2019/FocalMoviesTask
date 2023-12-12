using Focal.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Focal.Core.Interfaces.Repository
{
    public interface IMetaDataRepository
    {
        Task<List<MetaData>> GetMetaData();
        Task<MetaData> AddMetaData(MetaData metaData);
        Task<List<MetaData>> GetMetaData(int movieId);
        Task<List<Stats>> GetStats();

    }
}
