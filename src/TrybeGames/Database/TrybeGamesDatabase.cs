namespace TrybeGames;

public class TrybeGamesDatabase
{
    public List<Game> Games = new List<Game>();

    public List<GameStudio> GameStudios = new List<GameStudio>();

    public List<Player> Players = new List<Player>();

    // 4. Crie a funcionalidade de buscar jogos desenvolvidos por um estúdio de jogos
    public List<Game> GetGamesDevelopedBy(GameStudio gameStudio)
    {
        // implementar
        return Games.Where(game => game.DeveloperStudio == gameStudio.Id).ToList();
    }

    // 5. Crie a funcionalidade de buscar jogos jogados por uma pessoa jogadora
    public List<Game> GetGamesPlayedBy(Player player)
    {
        // Implementar
        IEnumerable<Game> games = from game in Games
                                  where game.Players.Contains(player.Id)
                                  select game;
        
        return games.ToList();
    }

    // 6. Crie a funcionalidade de buscar jogos comprados por uma pessoa jogadora
    public List<Game> GetGamesOwnedBy(Player playerEntry)
    {
        // Implementar
        IEnumerable<Game> games = from game in Games
                                  where playerEntry.GamesOwned.Contains(game.Id)
                                  select game;
        return games.ToList();
    }


    // 7. Crie a funcionalidade de buscar todos os jogos junto do nome do estúdio desenvolvedor
    public List<GameWithStudio> GetGamesWithStudio()
    {
        // Implementar
        IEnumerable<GameWithStudio> infos = from game in Games
                                        join studio in GameStudios
                                        on game.DeveloperStudio equals studio.Id
                                        select new GameWithStudio() {
                                            GameName = game.Name,
                                            StudioName = studio.Name,
                                            NumberOfPlayers = game.Players.Count(),
                                        };

        return infos.ToList();                  
    }
    
    // 8. Crie a funcionalidade de buscar todos os diferentes Tipos de jogos dentre os jogos cadastrados
    public List<GameType> GetGameTypes()
    {
        // Implementar
        var gamesDistinct = from game in Games
                            select game.GameType;
        
        return gamesDistinct.ToList();
    }

    // 9. Crie a funcionalidade de buscar todos os estúdios de jogos junto dos seus jogos desenvolvidos com suas pessoas jogadoras
    public List<StudioGamesPlayers> GetStudiosWithGamesAndPlayers()
    {
        // Implementar
        var studiosWithGamesAndPlayers = GameStudios.Select(studio => new StudioGamesPlayers() {
            GameStudioName = studio.Name,
            Games = Games.Where(game => game.DeveloperStudio == studio.Id).ToList().Select(game => new GamePlayer {
                        GameName = game.Name,
                        Players = Players.Where(player => game.Players.Contains(player.Id)).ToList(),
                    }
            ).ToList()
        });
        return studiosWithGamesAndPlayers.ToList();
    }

}
