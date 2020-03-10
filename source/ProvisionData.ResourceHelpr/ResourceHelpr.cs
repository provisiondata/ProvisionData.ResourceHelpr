namespace ProvisionData.ResourceHelpr
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Reflection;

    /// <summary>
    /// Resource Helper
    /// </summary>
    [DebuggerStepThrough]
    public static class RH // ResourceHelpr
    {
        private static readonly IDictionary<String, String> Cache = new Dictionary<String, String>();

        /// <summary>
        /// Looks in the <see cref="Assembly"/> and namespace that contains <typeparamref name="T"/> for a resource named <paramref name="resource"/> and returns it as a <see cref="String"/>.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="resource">The name of the resource to be retrieved.</param>
        /// <exception cref="ApplicationException">Assembly could not be loaded or <paramref name="resource"/> was not found.</exception>
        public static String GS<T>(String resource)
        {
            var type = typeof(T);
            var assembly = Assembly.GetAssembly(type);
            if (assembly == null)
            {
                throw new ApplicationException("Could not load the assembly.");
            }

            var key = type.Namespace + "." + resource;
            var cachekey = "[" + assembly.FullName + "]::" + key;
            if (!Cache.ContainsKey(cachekey))
            {
                Trace.WriteLine("Cache Miss: " + cachekey, "ResourceHelpr");
                try
                {
                    using (var reader = new StreamReader(assembly.GetManifestResourceStream(key)))
                    {
                        Cache[cachekey] = reader.ReadToEnd();
                    }
                }
                catch (Exception ex)
                {
                    throw new ApplicationException($"Failed to load \"{resource}\" from {assembly.FullName} with the key \"{key}\"", ex);
                }
            }

            return Cache[cachekey];
        }

        /// <summary>
        /// Looks in the assembly and namespace that contains <typeparamref name="T"/> for a resource named <paramref name="resource"/> and returns it as a <seealso cref="Stream"/>.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="resource">The name of the resource to be retrieved.</param>
        /// <exception cref="ApplicationException">Assembly could not be loaded or <paramref name="resource"/> was not found.</exception>
        public static Stream GetResourceStream<T>(String resource)
        {
            var type = typeof(T);
            var assembly = Assembly.GetAssembly(type);
            if (assembly == null)
            {
                throw new ApplicationException("Could not load the assembly.");
            }

            var key = type.Namespace + "." + resource;

            return assembly.GetManifestResourceStream(key);
        }
    }
}
