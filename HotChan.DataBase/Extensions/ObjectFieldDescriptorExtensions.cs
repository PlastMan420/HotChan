using HotChocolate.Types;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotChan.DataBase.Extensions
{
	public static class ObjectFieldDescriptorExtensions
	{
        public static IObjectFieldDescriptor UseDbContext<TDbContext>(
           this IObjectFieldDescriptor descriptor)
           where TDbContext : HotChanContext
        {
            return descriptor.UseScopedService<TDbContext>(
                create: s => s.GetRequiredService<IDbContextFactory<TDbContext>>().CreateDbContext(),
                disposeAsync: (s, c) => c.DisposeAsync());
        }
    }
}
