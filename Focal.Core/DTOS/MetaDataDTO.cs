using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Focal.Core.DTOS
{
    public class MetaDataDTO
    {
        public int Id { get; set; }
        public int MovieId { get; set; }
        public string Title { get; set; }
        public string Language { get; set; }
        public TimeSpan Duration { get; set; }
        public int ReleaseYear { get; set; }

    }
}
