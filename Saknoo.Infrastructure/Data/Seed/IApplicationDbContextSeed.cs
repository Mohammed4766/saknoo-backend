using System;

namespace Saknoo.Infrastructure.Data.Seed;

public interface IApplicationDbContextSeed
{
Task Seed();
}

