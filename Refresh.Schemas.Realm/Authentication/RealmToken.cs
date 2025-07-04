using System.Xml.Serialization;
using JetBrains.Annotations;
using MongoDB.Bson;
using Refresh.Database.Models.Users;

namespace Refresh.Database.Models.Authentication;

#nullable disable

[JsonObject(MemberSerialization.OptIn)]
[MapTo("Token")]
public partial class RealmToken : IRealmObject
{
    [PrimaryKey]
    public ObjectId TokenId { get; set; } = ObjectId.GenerateNewId();
    
    // this shouldn't ever be serialized, but just in case let's ignore it
    [XmlIgnore] public string TokenData { get; set; }
    
    // Realm can't store enums, use recommended workaround
    // ReSharper disable once InconsistentNaming (can't fix due to conflict with TokenType)
    public int _TokenType { get; set; }

    public TokenType TokenType
    {
        get => (TokenType)this._TokenType;
        set => this._TokenType = (int)value;
    }
    
    public int _TokenPlatform { get; set; }
    public int _TokenGame { get; set; }
    
    public TokenPlatform TokenPlatform 
    {
        get => (TokenPlatform)this._TokenPlatform;
        set => this._TokenPlatform = (int)value;
    }
    public TokenGame TokenGame 
    {
        get => (TokenGame)this._TokenGame;
        set => this._TokenGame = (int)value;
    }

    public DateTimeOffset ExpiresAt { get; set; }
    public DateTimeOffset LoginDate { get; set; }
    
    public string IpAddress { get; set; }

    public RealmGameUser User { get; set; }
    
    /// <summary>
    /// The digest key to use with this token, determined from the first game request created by this token
    /// </summary>
    [CanBeNull] public string Digest { get; set; }
    public bool IsHmacDigest { get; set; }
}