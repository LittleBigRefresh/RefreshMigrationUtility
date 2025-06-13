using System.Collections.Frozen;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;
using Realms;
using Refresh.Database;
using Refresh.Schema.Realm.Impl;

namespace RefreshMigrationUtility.Migrations;

public class SimpleMigrationTask<TOld, TNew> : MigrationTask<TOld, TNew> where TNew : class, new() where TOld : IRealmObject
{
    public SimpleMigrationTask(RealmDatabaseContext realm, GameDatabaseContext ef) : base(realm, ef)
    {
        _oldProperties = typeof(TOld)
            .GetProperties(BindingFlags.Instance | BindingFlags.Public)
            .Where(p => !p.GetCustomAttributes<IgnoredAttribute>().Any())
            .Where(p => p.CanRead && p.CanWrite)
            .ToFrozenSet();

        _newProperties = typeof(TNew)
            .GetProperties(BindingFlags.Instance | BindingFlags.Public)
            .Where(p => !p.GetCustomAttributes<NotMappedAttribute>().Any())
            .ToFrozenSet();

        if (_oldProperties.Count != _newProperties.Count)
        {
            throw new InvalidOperationException("Type is not eligible for automatic mapping");
        }
        
        Dictionary<PropertyInfo, PropertyInfo> newToOld = new();
        
        foreach (PropertyInfo newInfo in _newProperties)
        {
            PropertyInfo oldInfo = _oldProperties.First(i => i.Name == newInfo.Name);
            newToOld[newInfo] = oldInfo;
        }

        _newToOld = newToOld.ToFrozenDictionary();
    }

    private readonly FrozenSet<PropertyInfo> _oldProperties;
    private readonly FrozenSet<PropertyInfo> _newProperties;

    private readonly FrozenDictionary<PropertyInfo, PropertyInfo> _newToOld;

    public override TNew Map(TOld old)
    {
        TNew mapped = new();
        foreach (PropertyInfo newInfo in _newProperties)
        {
            PropertyInfo oldInfo = _newToOld[newInfo];
            newInfo.SetValue(mapped, oldInfo.GetValue(old));
        }

        return mapped;
    }
}