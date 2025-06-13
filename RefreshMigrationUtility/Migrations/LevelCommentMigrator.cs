using Microsoft.EntityFrameworkCore;
using Refresh.Database;
using Refresh.Database.Models.Comments;
using Refresh.Schema.Realm.Impl;
using RefreshMigrationUtility.Migrations.Dependent;

namespace RefreshMigrationUtility.Migrations;

public class LevelCommentMigrator : UserAndLevelDependentMigrator<RealmGameLevelComment, GameLevelComment>
{
    public LevelCommentMigrator(RealmDatabaseContext realm, GameDatabaseContext ef) : base(realm, ef)
    {}
    
    public override void MigrateChunk(RealmDatabaseContext realm, GameDatabaseContext ef)
    {
        IEnumerable<RealmGameLevelComment> chunk = realm.All<RealmGameLevelComment>()
            .AsEnumerable()
            .Skip(Progress)
            .Take(TakeSize);

        DbSet<GameLevelComment> set = ef.Set<GameLevelComment>();

        foreach (RealmGameLevelComment old in chunk)
        {
            // some of these are apparently null in realm so we have to check here
            if (old.Level != null)
            {
                GameLevelComment mapped = Map(ef, old);
                set.Add(mapped);
            }

            Progress++;
        }

        ef.SaveChanges();
    }

    public override GameLevelComment Map(GameDatabaseContext ef, RealmGameLevelComment old)
    {
        return new GameLevelComment
        {
            Level = ef.GameLevels.Find(old.Level.LevelId),
            Timestamp = old.Timestamp,
            SequentialId = old.SequentialId,
            Author = ef.GameUsers.Find(old.Author.UserId),
            Content = old.Content,
        };
    }
}