using System;
using System.Collections.Generic;
using System.Linq;

namespace CopilotLab
{
    public class MusicStoreService
    {
        private readonly MusicStoreRepository _repository;

        public MusicStoreService(MusicStoreRepository repository)
        {
            _repository = repository;
        }

        public List<Album> GetAlbums()
        {
            return _repository.Albums;
        }

        public Album GetAlbum(int id)
        {
            return _repository.Albums.FirstOrDefault(a => a.Id == id);
        }

        public void AddAlbum(Album album)
        {
            if (_repository.Albums.Any(a => a.Title.Equals(album.Title, StringComparison.OrdinalIgnoreCase)))
            {
                _repository.Albums.Add(album);
            }
            else
            {
                throw new DuplicateAlbumException("Album already exists.");
            }
        }

        public void UpdateAlbum(Album album)
        {
            var existingAlbum = _repository.Albums.FirstOrDefault(a => a.Id == album.Id);
            if (existingAlbum != null)
            {
                existingAlbum.Title = album.Title;
                existingAlbum.Artist = album.Artist;
                existingAlbum.ReleaseDate = album.ReleaseDate;
                existingAlbum.Genre = album.Genre;
            }
        }

        public void DeleteAlbum(int id)
        {
            var album = _repository.Albums.FirstOrDefault(a => a.Id == id);
            if (album != null)
            {
                _repository.Albums.Remove(album);
            }
        }

        public List<Album> SearchAlbumsByTitle(string title)
        {
            return _repository.Albums.Where(a => a.Title.Equals(title, StringComparison.OrdinalIgnoreCase)).ToList();
        }

        public List<Album> SearchAlbumsByArtist(string artist)
        {
            return _repository.Albums.Where(a => a.Artist.Equals(artist, StringComparison.OrdinalIgnoreCase)).ToList();
        }

        public List<Album> SearchAlbumsByGenre(string genre)
        {
            var genreId = _repository.Albums.FirstOrDefault(a => a.Genre.Name.Equals(genre, StringComparison.OrdinalIgnoreCase))?.Genre.Id;
            if (genreId != null)
                return _repository.Albums.Where(a => a.Genre.Id == genreId).ToList();
            return new List<Album>();
        }
    }
}
