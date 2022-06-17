using AnimatedThread.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AnimatedThread
{
    class Program
    {
        static Dictionary<string, string> frameSprites = new Dictionary<string, string>();
        static List<Output> output = new List<Output>();
        static IDatabase _database;
        static IDatabase Database
        {
            get
            {
                if (_database == null)
                    _database = new Database();
                return _database;
            }
        }
        static async Task Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            int counter;
            Thread thread = new Thread(GeraOutput);
            thread.Start();
            while (output.Count < 88)
            {
                counter = 0;
                for (int i = 0; i < 9; i++)
                {
                    Console.SetCursorPosition(0, 0);
                    DrawLoopMainThread(i, counter);
                    await Task.Delay(200);
                    counter++;
                }

                Console.WriteLine("-- INICIANDO PROCESSAMENTO --");
                //TODO: Iniciar tarefas abaixo desta linha
                if (!thread.IsAlive)
                    break;
            }

            Console.WriteLine("---- FINALIZADO ---");
            Console.ReadLine();
        }

        private static async void GeraOutput()
        {
            List<Artist> artistas = (List<Artist>)Database.GetArtists();//PegaObjetoArtistas();
            List<Person> pessoas = (List<Person>)Database.GetPeople();//PegaObjetoPessoasInfo();
            List<Song> musicas = (List<Song>)Database.GetSongs();//PegaObjetoMusicas();
            List<ArtistSongs> artistsSongs = Database.GetArtistSongsAsync();

            foreach (var pessoa in pessoas)
            {
                string nomePessoa = pessoa.Name;
                int idadePessoa = pessoa.Age;
                PersonSong personSong = await _database.GetPersonSongsAsync(pessoa.Id);
                int idMusicaFavorita = personSong.FavoriteSongId;
                string musicaFavorita = musicas.Find(m => m.Id == idMusicaFavorita).Name;
                int musicaFavoritaArtistaId = musicas.Find(m => m.Id == idMusicaFavorita).ArtistId;
                string musicaFavoritaArtistaNome = artistas.Find(a => a.Id == musicaFavoritaArtistaId).Name;
                int musicaFavoritaAno = musicas.Find(m => m.Id == idMusicaFavorita).Year;
                int[] outrasMusicasId = personSong.SongsIds;
                List<Song> outrasMusicas = musicas.Where(m => outrasMusicasId.Contains(m.Id)).ToList();
                string[] arrayOutrasMusicas = outrasMusicas.Select(s => s.Name).ToArray();

                output.Add(new Output(
                    nomePessoa,
                    idadePessoa,
                    musicaFavorita,
                    musicaFavoritaArtistaNome,
                    musicaFavoritaAno,
                    arrayOutrasMusicas,
                    artistsSongs.Where(arts => arts.Artist.Name == musicaFavoritaArtistaNome).ToList()
                    )
                );
            }

            var outputSerializado = JsonConvert.SerializeObject(output, Formatting.Indented);
            Console.WriteLine(outputSerializado);
            Console.WriteLine("contagem  " + output.Count);
        }

        static void DrawLoopMainThread(int i, int counter)
        {
            switch (counter % (i + 1))
            {
                case 0:
                    {
                        const string frameName = nameof(Animations.frame8);
                        Console.Write(GetFrameLines(frameName, Animations.frame8));
                        break;
                    };
                case 1:
                    {
                        const string frameName = nameof(Animations.frame7);
                        Console.Write(GetFrameLines(frameName, Animations.frame7));
                        break;
                    };
                case 2:
                    {
                        const string frameName = nameof(Animations.frame6);
                        Console.Write(GetFrameLines(frameName, Animations.frame6));
                        break;
                    };
                case 3:
                    {
                        const string frameName = nameof(Animations.frame5);
                        Console.Write(GetFrameLines(frameName, Animations.frame5));
                        break;
                    };
                case 4:
                    {
                        const string frameName = nameof(Animations.frame4);
                        Console.Write(GetFrameLines(frameName, Animations.frame4));
                        break;
                    };
                case 5:
                    {
                        const string frameName = nameof(Animations.frame3);
                        Console.Write(GetFrameLines(frameName, Animations.frame3));
                        break;
                    };
                case 6:
                    {
                        const string frameName = nameof(Animations.frame2);
                        Console.Write(GetFrameLines(frameName, Animations.frame2));
                        break;
                    };
                case 7:
                    {
                        const string frameName = nameof(Animations.frame1);
                        Console.Write(GetFrameLines(frameName, Animations.frame1));
                        break;
                    }
                case 8:
                    {
                        const string frameName = nameof(Animations.frame0);
                        Console.Write(GetFrameLines(frameName, Animations.frame0));
                        break;
                    }
            }
        }

        static string GetFrameLines(string frameName, string frame)
        {
            if (!frameSprites.ContainsKey(frameName))
            {
                var frameLines = frame.Split("<br>");
                var sb = new StringBuilder();
                foreach (var item in frameLines)
                {
                    sb.AppendLine(item);
                }
                frameSprites.Add(frameName, sb.ToString());
            }
            return frameSprites[frameName];
        }
    }
}
