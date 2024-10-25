using System;
using System.Collections.Generic;

namespace MusicStore
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Instantiate the repository
            var repository = new MusicStoreRepository();
            var musicStoreService = new MusicStoreService(repository);

            // Get all albums
            var albums = musicStoreService.GetAlbums();
            // Print all albums
            PrintAlbums(albums);

            Console.WriteLine();
            Console.WriteLine("****************************************************************");

            Console.WriteLine("Search albums by:");
            Console.WriteLine("1. Artist");
            Console.WriteLine("2. Title");
            Console.WriteLine("3. Genre");

            Console.WriteLine("****************************************************************");

            var option = Console.ReadLine();

            switch (option)
            {
                case "1":
                    Console.WriteLine("Enter artist name:");
                    var artist = Console.ReadLine();
                    var albumsByArtist = musicStoreService.SearchAlbumsByArtist(artist);
                    PrintAlbums(albumsByArtist);
                    break;
                case "2":
                    Console.WriteLine("Enter title:");
                    var title = Console.ReadLine();
                    var albumsByTitle = musicStoreService.SearchAlbumsByTitle(title);
                    PrintAlbums(albumsByTitle);
                    break;
                case "3":
                    Console.WriteLine("Enter genre:");
                    var genre = Console.ReadLine();
                    var albumsByGenre = musicStoreService.SearchAlbumsByGenre(genre);
                    PrintAlbums(albumsByGenre);
                    break;
                default:
                    Console.WriteLine("Invalid option");
                    break;
            }
        }

        public static void PrintAlbums(List<Album> albums)
        {
            Console.WriteLine("--------------------------------------------------------------");
            Console.WriteLine("|   ID   |        Title        |       Artist       |   Genre   |");
            Console.WriteLine("--------------------------------------------------------------");
            foreach (var album in albums)
            {
                Console.WriteLine($"| {album.Id,-6} | {album.Title,-19} | {album.Artist,-18} | {album.Genre,-9} |");
            }
            Console.WriteLine("--------------------------------------------------------------");
        }
    }
}