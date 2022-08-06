using HotChan.DataBase.Models.Entities;
using HotChocolate.Data.Filters;

namespace HotChan.DataAccess.Gql.Filters
{
    class UserDataFilter : FilterInputType<User>
    {
        protected override void Configure(
               IFilterInputTypeDescriptor<User> descriptor)
        {
            descriptor.BindFieldsExplicitly();
            descriptor.Field(f => f.Id).Name("UserId");
        }
    }
}
