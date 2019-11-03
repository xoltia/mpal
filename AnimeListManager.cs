using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MPAL
{
    public class AnimeListManager
    {
        private readonly List<Anime> animes;
        private readonly string listPath;

        public AnimeListManager(string listPath)
        {
            this.listPath = listPath;
            if (!File.Exists(listPath))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(listPath));
                File.WriteAllText(listPath, "[]");
            }
            animes = JsonConvert.DeserializeObject<List<Anime>>(File.ReadAllText(listPath));
        }

        public bool Exists(string animeName) => animes.Any((Anime entry) => entry.Name.ToLower() == animeName.ToLower()); 

        public void Add(Anime anime)
        {
            if (Exists(anime.Name))
            {
                throw new ArgumentException($"Anime with name {anime.Name} already exists.");
            }
            animes.Add(anime);
        }

        public void Update(AnimeUpdate update)
        {
            if (!Exists(update.Name))
            {
                throw new ArgumentException($"Anime with name {update.Name} doesn't exist.");
            }

            update.Merge(animes.Find((Anime entry) => entry.Name.ToLower() == update.Name.ToLower()));
        }

        public void Remove(string name)
        {
            if (!Exists(name))
            {
                throw new ArgumentException($"Anime with name {name} doesn't exist.");
            }

            animes.RemoveAll((Anime entry) => entry.Name.ToLower() == name.ToLower());
        }

        public void Finish(string name)
        {
            Update(new AnimeUpdate
            {
                Name = name,
                Finished = true,
                FinishTime = DateTime.Now
            });
        }

        public void Save() => File.WriteAllText(listPath, JsonConvert.SerializeObject(animes, Formatting.Indented));
    }
}
