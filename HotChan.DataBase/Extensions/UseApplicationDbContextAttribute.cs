using HotChocolate.Types;
using HotChocolate.Types.Descriptors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace HotChan.DataBase.Extensions
{
	public class UseApplicationDbContextAttribute : ObjectFieldDescriptorAttribute
    {
        public override void OnConfigure(
           IDescriptorContext context,
           IObjectFieldDescriptor descriptor,
           MemberInfo member)
        {
            descriptor.UseDbContext<HotChanContext>();
        }
    }
}
