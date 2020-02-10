using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Dynamic;
using System.IO;
using System.Linq.Expressions;

namespace Botworx
{
    public partial class Frame : IDynamicMetaObjectProvider
    {
        public Frame()
        {
        }

        private Dictionary<string, object> storage = new
            Dictionary<string, object>();

        public object SetDictionaryEntry(string key, object value)
        {
            if (storage.ContainsKey(key))
                storage[key] = value;
            else
                storage.Add(key, value);
            return value;
        }

        public object GetDictionaryEntry(string key)
        {
            object result = null;
            if (storage.ContainsKey(key))
            {
                result = storage[key];
            }
            return result;
        }

        public object WriteMethodInfo(string methodInfo)
        {
            Console.WriteLine(methodInfo);
            return 42; // because it is the answer to everything
        }

        public override string ToString()
        {
            StringWriter message = new StringWriter();
            foreach (var item in storage)
                message.WriteLine("{0}:\t{1}", item.Key, item.Value);
            return message.ToString();
        }
    }
}
