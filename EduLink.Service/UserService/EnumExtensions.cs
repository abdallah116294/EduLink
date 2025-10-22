using Google.Apis.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduLink.Service.UserService
{
    public static class EnumExtensions
    {
        public static string GetDisplayName(this Enum enumValue)
        {
            var member = enumValue.GetType().GetMember(enumValue.ToString()).First();
            var attr = member.GetCustomAttribute<DisplayAttribute>();
            return attr?.Name ?? enumValue.ToString();
        }
    }
}
