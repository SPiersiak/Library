using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore;
using Moq;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace TestBooksApi;
public class MockDatabaseFacade : DatabaseFacade
{
    public MockDatabaseFacade(DbContext context) : base(context)
    {
    }

    public override IDbContextTransaction BeginTransaction() =>
        Mock.Of<IDbContextTransaction>();

    public override void CommitTransaction() =>
        Mock.Of<IDbContextTransaction>();
}
