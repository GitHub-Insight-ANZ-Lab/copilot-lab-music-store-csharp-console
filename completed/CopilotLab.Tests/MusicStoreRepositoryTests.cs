using NUnit.Framework;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CopilotLab.Tests
{
    [TestFixture]
    public class MusicStoreServiceTests
    {
        private Mock<MusicStoreRepository> _mockRepository;
        private MusicStoreService _service;

        [SetUp]
        public void Setup()
        {
            _mockRepository = new Mock<MusicStoreRepository>();
            _service = new MusicStoreService(_mockRepository.Object);
        }

        [Test]
        public void GetAlbums_ShouldReturnAllAlbums()
        {
            // Arrange
            var albums = new List<Album>
            {
                new Album(1, "The Dark Side of the Moon", "Pink Floyd", new DateTime(1973, 3, 1), new Genre(1, "Rock")),
                new Album(2, "Thriller", "Michael Jackson", new DateTime(1982, 11, 30), new Genre(2, "Pop"))
            };
            _mockRepository.Setup(repo => repo.Albums).Returns(albums);

            // Act
            var result = _service.GetAlbums();

            // Assert
            Assert.That(result.Count, Is.EqualTo(2));
        }

        [Test]
        public void GetAlbum_ShouldReturnCorrectAlbum()
        {
            // Arrange
            var album = new Album(1, "The Dark Side of the Moon", "Pink Floyd", new DateTime(1973, 3, 1), new Genre(1, "Rock"));
            _mockRepository.Setup(repo => repo.Albums).Returns(new List<Album> { album });

            // Act
            var result = _service.GetAlbum(1);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Title, Is.EqualTo("The Dark Side of the Moon"));
        }

        [Test]
        public void GetAlbum_ShouldReturnNullForNonExistentAlbum()
        {
            // Arrange
            _mockRepository.Setup(repo => repo.Albums).Returns(new List<Album>());

            // Act
            var result = _service.GetAlbum(999);

            // Assert
            Assert.That(result, Is.Null);
        }

        [Test]
        public void AddAlbum_ShouldAddAlbum_WhenAlbumDoesNotExist()
        {
            // Arrange
            var album = new Album(1, "The Dark Side of the Moon", "Pink Floyd", new DateTime(1973, 3, 1), new Genre(1, "Rock"));
            var albums = new List<Album>();
            _mockRepository.Setup(repo => repo.Albums).Returns(albums);

            // Act
            _service.AddAlbum(album);

            // Assert
            Assert.That(albums.Count, Is.EqualTo(1));
        }

        [Test]
        public void AddAlbum_ShouldThrowDuplicateAlbumException_WhenAlbumAlreadyExists()
        {
            // Arrange
            var album = new Album(1, "The Dark Side of the Moon", "Pink Floyd", new DateTime(1973, 3, 1), new Genre(1, "Rock"));
            var albums = new List<Album> { album };
            _mockRepository.Setup(repo => repo.Albums).Returns(albums);

            // Act & Assert
            Assert.Throws<DuplicateAlbumException>(() => _service.AddAlbum(album));
        }


        [Test]
        public void UpdateAlbum_ShouldUpdateExistingAlbum()
        {
            // Arrange
            var existingAlbum = new Album(1, "The Dark Side of the Moon", "Pink Floyd", new DateTime(1973, 3, 1), new Genre(1, "Rock"));
            var updatedAlbum = new Album(1, "Updated Title", "Updated Artist", new DateTime(1973, 3, 1), new Genre(1, "Rock"));
            var albums = new List<Album> { existingAlbum };
            _mockRepository.Setup(repo => repo.Albums).Returns(albums);

            // Act
            _service.UpdateAlbum(updatedAlbum);

            // Assert
            Assert.That(existingAlbum.Title, Is.EqualTo("Updated Title"));
            Assert.That(existingAlbum.Artist, Is.EqualTo("Updated Artist"));
        }

        [Test]
        public void DeleteAlbum_ShouldRemoveAlbum()
        {
            // Arrange
            var album = new Album(1, "The Dark Side of the Moon", "Pink Floyd", new DateTime(1973, 3, 1), new Genre(1, "Rock"));
            var albums = new List<Album> { album };
            _mockRepository.Setup(repo => repo.Albums).Returns(albums);

            // Act
            _service.DeleteAlbum(1);

            // Assert
            Assert.That(albums.Count, Is.EqualTo(0));
        }

        [Test]
        public void SearchAlbumsByTitle_ShouldReturnCorrectAlbums()
        {
            // Arrange
            var album = new Album(1, "Thriller", "Michael Jackson", new DateTime(1982, 11, 30), new Genre(2, "Pop"));
            var albums = new List<Album> { album };
            _mockRepository.Setup(repo => repo.Albums).Returns(albums);

            // Act
            var result = _service.SearchAlbumsByTitle("Thriller");

            // Assert
            Assert.That(result.Count, Is.EqualTo(1));
            Assert.That(result[0].Title, Is.EqualTo("Thriller"));
        }

        [Test]
        public void SearchAlbumsByArtist_ShouldReturnCorrectAlbums()
        {
            // Arrange
            var album = new Album(1, "The Dark Side of the Moon", "Pink Floyd", new DateTime(1973, 3, 1), new Genre(1, "Rock"));
            var albums = new List<Album> { album };
            _mockRepository.Setup(repo => repo.Albums).Returns(albums);

            // Act
            var result = _service.SearchAlbumsByArtist("Pink Floyd");

            // Assert
            Assert.That(result.Count, Is.EqualTo(1));
            Assert.That(result[0].Artist, Is.EqualTo("Pink Floyd"));
        }

        [Test]
        public void SearchAlbumsByGenre_ShouldReturnCorrectAlbums()
        {
            // Arrange
            var album1 = new Album(1, "The Dark Side of the Moon", "Pink Floyd", new DateTime(1973, 3, 1), new Genre(1, "Rock"));
            var album2 = new Album(2, "Abbey Road", "The Beatles", new DateTime(1969, 9, 26), new Genre(1, "Rock"));
            var albums = new List<Album> { album1, album2 };
            _mockRepository.Setup(repo => repo.Albums).Returns(albums);

            // Act
            var result = _service.SearchAlbumsByGenre("Rock");

            // Assert
            Assert.That(result.Count, Is.EqualTo(2));
            Assert.That(result.All(a => a.Genre.Name == "Rock"));
        }
    }
}
