﻿using Moq;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace BlazorEcommerceStaticWebApp.Test
{
    public static class QueryableExtensions
    {
        public static IDbSet<T> BuildMockDbSet<T>(this IQueryable<T> source)
            where T : class
        {
            var mock = new Mock<IDbSet<T>>();
            mock.As<IDbAsyncEnumerable<T>>()
                .Setup(x => x.GetAsyncEnumerator())
                .Returns(new TestDbAsyncEnumerator<T>(source.GetEnumerator()));

            mock.As<IQueryable<T>>()
                .Setup(x => x.Provider)
                .Returns(new TestDbAsyncQueryProvider<T>(source.Provider));

            mock.As<IQueryable<T>>()
                .Setup(x => x.Expression)
                .Returns(source.Expression);

            mock.As<IQueryable<T>>()
                .Setup(x => x.ElementType)
                .Returns(source.ElementType);

            mock.As<IQueryable<T>>()
                .Setup(x => x.GetEnumerator())
                .Returns(source.GetEnumerator());

            return mock.Object;
        }
    }
}
