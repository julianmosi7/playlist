using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using playlist.Dtos;
using playlist.Services;
using playlistDb;

namespace playlist.Controllers
{
  [Route("[controller]")]
  [ApiController]
  public class ValuesController : ControllerBase
  {
    private readonly ValuesService valuesService;
	public ValuesController(ValuesService valuesService, playlistContext db)
	{
            this.valuesService = valuesService;
	}
	  
    [HttpGet("Playlists")]
    public IEnumerable<PlaylistDto> GetAllPlaylists()
    {
            return valuesService.GetAllPlaylists()
                    .Select(x => new PlaylistDto
                    {
                        PlaylistId = x.PlaylistId,
                        Name = x.Name
                    });
    }

    [HttpGet("PlaylistTracks/{id}")]
    public IEnumerable<TrackDto> GetPlaylistTrack(int id)
    {
            return valuesService.GetPlaylistTrack(id)
                    .Select(x => new TrackDto
                    {
                        TrackId = x.TrackId,
                        Name = x.Name
                    }); 
    }

    [HttpGet("Genres")]
    public IEnumerable<GenreDto> GetAllGenres()
    {
            return valuesService.GetAllGenres()
                    .Select(x => new GenreDto
                    {
                        GenreId = x.GenreId,
                        Name = x.Name
                    });
    }
        
    [HttpGet("Tracks")]
    public IEnumerable<TrackDto> GetAllTracksForGenre([FromQuery] int genreId)
    {
            return valuesService.GetAllTracksForGenre(genreId)
                 .Select(x => new TrackDto
                 {
                     TrackId = x.TrackId,
                     Name = x.Name
                 });
    }

    [HttpPost("Track")]
    public PlaylistTrackDto AddTrack([FromBody] PlaylistTrackDto playlistTrackDto)
    {
            var playlistTrack = new PlaylistTrack
            {
                PlaylistId = playlistTrackDto.PlaylistId,
                TrackId = playlistTrackDto.TrackId
            };
            PlaylistTrack playlistTrackResult = valuesService.AddTrack(playlistTrack);
            return new PlaylistTrackDto
            {
                PlaylistId = playlistTrackResult.PlaylistId,
                TrackId = playlistTrackResult.TrackId
            };  
    }

    [HttpDelete("Track")]
    public PlaylistTrackDto RemoveTrack([FromQuery] int playlistId, int trackId)
    {
        PlaylistTrack playlistTrackResult = valuesService.RemoveTrack(playlistId, trackId);
            return new PlaylistTrackDto
            {
                PlaylistId = playlistTrackResult.PlaylistId,
                TrackId = playlistTrackResult.PlaylistId
            };
    }
  }
}
