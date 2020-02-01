using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace CS8NullBasics
{
    static class MessagePopulator
    {
        public static void Populate(Message message)
        {
            message.GetType().InvokeMember("From",
                BindingFlags.Instance | BindingFlags.Public | BindingFlags.SetProperty,
                Type.DefaultBinder, message, new[] { "Jason (set using reflection)" });
        }
    }
}
