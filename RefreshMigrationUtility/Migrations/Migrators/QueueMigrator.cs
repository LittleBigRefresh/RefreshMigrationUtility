﻿using Refresh.Database;
using Refresh.Database.Models.Relations;
using Refresh.Schema.Realm.Impl;
using RefreshMigrationUtility.Migrations.Dependent;

namespace RefreshMigrationUtility.Migrations.Migrators;

public class QueueMigrator : UserAndLevelDependentMigrator<RealmQueueLevelRelation, QueueLevelRelation>
{
    public QueueMigrator(RealmDatabaseContext realm, GameDatabaseContext ef) : base(realm, ef)
    {}

    protected override QueueLevelRelation Map(GameDatabaseContext ef, RealmQueueLevelRelation old)
    {
        return new QueueLevelRelation
        {
            UserId = old.User.UserId,
            LevelId = old.Level.LevelId,
            Timestamp = old.Timestamp,
        };
    }
}