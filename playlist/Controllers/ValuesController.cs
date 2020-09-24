using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using playlistDb;

namespace playlist.Controllers
{
  [Route("[controller]")]
  [ApiController]
  public class ValuesController : ControllerBase
  {
	private readonly playlistContext db;
	public ValuesController(playlistContext db)
	{
	  this.db = db;
	}
	  
    [HttpGet("Tracks")]
    public object GetTracks()
    {
      try
      {
        int nr = db.Tracks.Count();
        return new { IsOk = true, Nr = nr };
      }
      catch (Exception exc)
      {
        return new { IsOk = false, Nr = -1, Error = exc.Message };
      }
    }

    [HttpGet("Playlists")]
    public object GetPlaylists()
    {
        
    }

  }
}
