using AutoMapper;
using Focal.Core.DTOS;
using Focal.Core.Entities;
using Focal.Core.Interfaces.Repository;
using Focal.Core.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Focal.Core.Services
{
    public class MetaDataService : IMetaDataService
    {
        private readonly IMetaDataRepository _repository;
        private readonly IMapper _mapper;
        public MetaDataService(IMetaDataRepository repository,IMapper mapper) 
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<MetaDataDTO> AddMetaData(MetaDataDTO metaDataDTO)
        {
            return _mapper.Map<MetaDataDTO>(await _repository.AddMetaData(_mapper.Map<MetaData>(metaDataDTO)));
        }

        public async Task<List<MetaDataDTO>> GetById(int movieId)
        {
            return _mapper.Map<List<MetaDataDTO>>(await _repository.GetMetaData(movieId));
        }

        public async Task<List<MetaDataDTO>> GetMetaData()
        {
            return _mapper.Map<List<MetaDataDTO>>(await _repository.GetMetaData());
        }

        public async Task<IEnumerable<MovieStatisticsDTO>> GetMoviesStatistics()
        {
            var allMovies = await _repository.GetMetaData();
            var allStats = await _repository.GetStats();

            var GroupByQS =
            from stats in allStats
            group stats by stats.MovieId into statsgroub
            orderby statsgroub.Key descending
            select new { key = statsgroub.Key, Watches = statsgroub.OrderByDescending(x => x.WatchDurationMs / 1000) };

            var results =
                from stats in GroupByQS
                from movie in allMovies
                where movie.Id == stats.key
                select new MovieStatisticsDTO()
                {
                    MovieId = movie.Id,
                    Title = movie.Title,
                    ReleaseYear = movie.ReleaseYear,
                    AverageWatchDurationS = stats.Watches.Select(w => w.WatchDurationMs).Average(),
                    Watches = stats.Watches.Count()
                };

            return results;
        }
    }
}
