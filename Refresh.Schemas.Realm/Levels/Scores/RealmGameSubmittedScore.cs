using MongoDB.Bson;
using Refresh.Database.Models.Authentication;
using Refresh.Database.Models.Users;

namespace Refresh.Database.Models.Levels.Scores;

#nullable disable

[MapTo("GameSubmittedScore")]
public partial class RealmGameSubmittedScore : IRealmObject // TODO: Rename to GameScore
{
    [PrimaryKey] public ObjectId ScoreId { get; set; } = ObjectId.GenerateNewId();
    
    // ReSharper disable once InconsistentNaming
    [Indexed] public int _Game { get; set; }
    [Ignored] public TokenGame Game
    {
        get => (TokenGame)this._Game;
        set => this._Game = (int)value;
    }
    
    // ReSharper disable once InconsistentNaming
    public int _Platform { get; set; }
    [Ignored] public TokenPlatform Platform
    {
        get => (TokenPlatform)this._Platform;
        set => this._Platform = (int)value;
    }

    public RealmGameLevel Level { get; set; }
    public DateTimeOffset ScoreSubmitted { get; set; }
    
    [Indexed] public int Score { get; set; }
    [Indexed] public byte ScoreType { get; set; }
    
    public IList<RealmGameUser> Players { get; }
}