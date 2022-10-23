using System;

namespace TicketManagement.DataAccess.Attributes
{
    internal static class AttributeHandler : object
    {
        public static TAttribute GetMemberAttribute<TAttribute>(Type type, string memberName)
            where TAttribute : Attribute
        {
            if (type == null)
            {
                throw new ArgumentNullException("type");
            }

            if (memberName == null)
            {
                throw new ArgumentNullException("memberName");
            }

            var memberInfo = type.GetMember(memberName)[0];

            return (TAttribute)memberInfo.GetCustomAttributes(typeof(TAttribute), false)[0];
        }
    }
}