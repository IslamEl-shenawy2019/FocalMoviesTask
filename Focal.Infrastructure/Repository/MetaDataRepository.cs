using Focal.Core.Entities;
using Focal.Core.Interfaces.Repository;
using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Focal.Infrastructure.Repository
{
    public class MetaDataRepository : IMetaDataRepository
    {
        public async Task<MetaData> AddMetaData(MetaData metaData)
        {
            string filePath = "D:\\FocalMovies\\Focal.Infrastructure\\Data\\metadata.csv";

            var allmetaData = ReadMetaDataFromCsv(filePath);

            int lastId = allmetaData.Any() ? allmetaData.Max(m => m.Id) : 0;
            int newId = lastId + 1;

            metaData.Id = newId;
            allmetaData.Add(metaData);

            await WriteMetaDataToCsv(filePath, allmetaData);
            return metaData;
        }

        public async Task<List<MetaData>> GetMetaData()
        {
            string filePath = "D:\\FocalMovies\\Focal.Infrastructure\\Data\\metadata.csv";

            return ReadMetaDataFromCsv(filePath);
        }

        public async Task<List<MetaData>> GetMetaData(int movieId)
        {
            string filePath = "D:\\FocalMovies\\Focal.Infrastructure\\Data\\metadata.csv";

            var metaDataList = ReadMetaDataFromCsv(filePath);
            var movieMetadata = metaDataList
            .Where(meta => meta.MovieId == movieId)
            .OrderBy(meta => meta.Language)  
            .GroupBy(meta => meta.Language)
            .Select(group => group.OrderByDescending(meta => meta.Id).First())
            .Where(meta => IsValidMetadata(meta)) 
            .ToList();

            return movieMetadata;
        }

        public async Task<List<Stats>> GetStats()
        {
            string filePath = "D:\\FocalMovies\\Focal.Infrastructure\\Data\\stats.csv";

            return ReadStatsDataFromCsv(filePath);
        }

        private List<Stats> ReadStatsDataFromCsv(string filePath)
        {
            List<Stats> statsDataList = new List<Stats>();

            using (TextFieldParser parser = new TextFieldParser(filePath))
            {
                parser.TextFieldType = FieldType.Delimited;
                parser.SetDelimiters(",");

                // Skip the header row if present
                if (!parser.EndOfData)
                {
                    parser.ReadLine();
                }

                while (!parser.EndOfData)
                {
                    string[] fields = parser.ReadFields();

                    Stats metaData = new Stats
                    {
                        MovieId = int.Parse(fields[0]),
                        WatchDurationMs = int.Parse(fields[1])
                    };

                    statsDataList.Add(metaData);
                }
            }

            return statsDataList;
        }

        private bool IsValidMetadata(MetaData metaData)
        {
            return metaData.Id > 0
                && metaData.MovieId > 0
                && !string.IsNullOrEmpty(metaData.Title)
                && !string.IsNullOrEmpty(metaData.Language)
                && metaData.Duration != TimeSpan.Zero
                && metaData.ReleaseYear > 0;
        }

        private List<MetaData> ReadMetaDataFromCsv(string filePath)
        {
            List<MetaData> metaDataList = new List<MetaData>();

            using (TextFieldParser parser = new TextFieldParser(filePath))
            {
                parser.TextFieldType = FieldType.Delimited;
                parser.SetDelimiters(",");

                // Skip the header row if present
                if (!parser.EndOfData)
                {
                    parser.ReadLine();
                }

                while (!parser.EndOfData)
                {
                    string[] fields = parser.ReadFields();

                    MetaData metaData = new MetaData
                    {
                        Id = int.Parse(fields[0]),
                        MovieId = int.Parse(fields[1]),
                        Title = fields[2],
                        Language = fields[3],
                        Duration = TimeSpan.Parse(fields[4]),
                        ReleaseYear = int.Parse(fields[5])
                    };

                    metaDataList.Add(metaData);
                }
            }

            return metaDataList;
        }

        private async Task WriteMetaDataToCsv(string filePath, List<MetaData> metaDataList)
        {
            using (StreamWriter sw = new StreamWriter(filePath, false))
            {
                // Write the header row
                sw.WriteLine("Id,MovieId,Title,Language,Duration,ReleaseYear");

                // Write each MetaData object to the file
                foreach (var metaData in metaDataList)
                {
                    sw.WriteLine($"{metaData.Id},{metaData.MovieId},{metaData.Title},{metaData.Language},{metaData.Duration},{metaData.ReleaseYear}");
                }
            }
        }
    }
}
