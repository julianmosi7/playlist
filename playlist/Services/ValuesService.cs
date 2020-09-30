using playlistDb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace playlist.Services
{
    public class ValuesService
    {
        private readonly playlistContext db;

        public ValuesService(playlistContext db)
        {
            this.db = db;
        }

        public IEnumerable<Playlist> GetAllPlaylists()
        {
            return db.Playlists.AsEnumerable();
        }

        public IEnumerable<Track> GetPlaylistTrack(int id)
        {
            return db.PlaylistTracks.Where(x => x.PlaylistId == id).Select(x => x.Track).AsEnumerable();
        }

        public IEnumerable<Genre> GetAllGenres()
        {
            return db.Genres.AsEnumerable();
        }

        public IEnumerable<Track> GetAllTracksForGenre(int genreId)
        {
            return db.Tracks.Where(x => x.GenreId == genreId).AsEnumerable();
        }

        public PlaylistTrack AddTrack(PlaylistTrack playlistTrack)
        {
            try
            {
                db.PlaylistTracks.Add(playlistTrack);
                db.SaveChanges();
                return playlistTrack;
            }catch(Exception exc)
            {
                Console.WriteLine($"*******Exception: {exc.Message}");
                return null;
            }
            
        }

        public PlaylistTrack RemoveTrack(int playlistId, int trackId)
        {
            PlaylistTrack trackToRemove = db.PlaylistTracks.Where(x => x.PlaylistId == playlistId && x.TrackId == trackId).FirstOrDefault();
            try
            {
                db.PlaylistTracks.Remove(trackToRemove);
                db.SaveChanges();
                return trackToRemove;
            }catch(Exception exc)
            {
                Console.WriteLine($"*******Exception: {exc.Message}");
                return null;
            }
        }
    }
}
