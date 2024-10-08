using System.Collections.Generic;
namespace CopilotLab
{
    public class MusicStoreRepository
    {
        public virtual List<Album> Albums { get; set; }

        public MusicStoreRepository()
        {
            //read all albums from a json file
            var file = System.IO.File.ReadAllText("albums.json");
            Albums = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Album>>(file);
        }
    }
}
