using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace playlist.Dtos
{
    public class PlaylistTrackDto
    {
        public long PlaylistId { get; set; }
        public long TrackId { get; set; }

        public PlaylistDto Playlist { get; set; }
        public TrackDto Track { get; set; }
    }
}
