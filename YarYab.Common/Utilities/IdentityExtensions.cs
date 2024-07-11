using System;
using System.Globalization;
using System.Security.Claims;
using System.Security.Principal;
using YarYab.Common.Helper;

namespace YarYab.Common.Utilities
{
    public static class IdentityExtensions
    {
        public static string FindFirstValue(this ClaimsIdentity identity, string claimType)
        {
            return identity?.FindFirst(claimType)?.Value;
        }

        public static string FindFirstValue(this IIdentity identity, string claimType)
        {
            var ClaimsIdentity = identity as ClaimsIdentity;
            return ClaimsIdentity?.FindFirstValue(claimType);
        }

        //public static string GetUserId(this IIdentity identity)
        //{
        //    return identity?.FindFirstValue(ClaimTypes.NameIdentifier);
        //}

        //public static T GetUserId<T>(this IIdentity identity) where T 
        //{
        //    var userId = identity?.GetUserId();
        //    return userId.HasValue()
        //        ? (T)Convert.ChangeType(userId, typeof(T), CultureInfo.InvariantCulture)
        //        : default(T);
        //}

        public static Guid GetUserId(this IIdentity identity)
        {
            var userId = identity?.FindFirstValue(ClaimTypes.NameIdentifier);
            return userId.HasValue() ? Guid.Parse(userId) : Guid.Empty;
        }

        public static string GetUserName(this IIdentity identity)
        {
            return identity?.FindFirstValue(ClaimTypes.Name);
        }
    }
}
