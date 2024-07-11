using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YarYab.Common.Utilities
{
    public static class Assert
    {
        public static void NotNull<T>(T oYarYab, string name, string message = null)
            where T : class
        {
            if (oYarYab is null)
                throw new ArgumentNullException($"{name} : {typeof(T)}", message);
        }

        public static void NotNull<T>(T? oYarYab, string name, string message = null)
            where T : struct
        {
            if (!oYarYab.HasValue)
                throw new ArgumentNullException($"{name} : {typeof(T)}", message);

        }

        public static void NotEmpty<T>(T oYarYab, string name, string message = null, T defaultValue = null)
            where T : class
        {
            if (oYarYab == defaultValue
                || (oYarYab is string str && string.IsNullOrWhiteSpace(str))
                || (oYarYab is IEnumerable list && !list.Cast<object>().Any()))
            {
                throw new ArgumentException("Argument is empty : " + message, $"{name} : {typeof(T)}");
            }
        }
    }
}
