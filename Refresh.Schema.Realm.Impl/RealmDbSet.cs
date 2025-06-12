using System.Collections;
using System.Linq.Expressions;
using JetBrains.Annotations;
using Realms;

namespace Refresh.Database;

public class RealmDbSet<T> : IQueryable<T> where T : IRealmObject
{
    private readonly Realm _realm;
    private IQueryable<T> Queryable => this._realm.All<T>();

    public RealmDbSet(Realm realm)
    {
        this._realm = realm;
    }

    [MustDisposeResource] public IEnumerator<T> GetEnumerator() => this.Queryable.GetEnumerator();
    [MustDisposeResource] IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();

    public Type ElementType => typeof(T);
    public Expression Expression => this.Queryable.Expression;
    public IQueryProvider Provider => this.Queryable.Provider;
}