namespace PDSI.ResourceHelpr
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
        /// Looks in the assembly and namespace that contains <see cref="RH"/> for a resource named <paramref name="resource"/> and returns it as a <seealso cref="String"/>.
        /// </summary>
        /// <param name="resource">The name of the resource to be retrieved.</param>
        /// <exception cref="ApplicationException">Assembly could not be loaded or <paramref name="resource"/> was not found.</exception>
        public static String GS(String resource)
        {
            var type = typeof(RH);
            return GS(resource, type);
        }

        /// <summary>
        /// Looks in the assembly and namespace that contains <typeparamref name="T"/> for a resource named <paramref name="resource"/> and returns it as a <seealso cref="String"/>.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="resource">The name of the resource to be retrieved.</param>
        /// <exception cref="ApplicationException">Assembly could not be loaded or <paramref name="resource"/> was not found.</exception>
        public static String GS<T>(String resource)
        {
            var type = typeof(T);
            return GS(resource, type);
        }

        private static String GS(String resource, Type type)
        {
            var cachekey = type.FullName + "::" + resource;
            if (!Cache.ContainsKey(cachekey))
            {
                Debug.WriteLine("Cache Miss: " + cachekey, "Resource Helper");
                try
                {
                    var assembly = Assembly.GetAssembly(type);
                    if (assembly == null)
                    {
                        throw new ApplicationException("Could not load the assembly.");
                    }

                    var key = type.Namespace + "." + resource;

                    // ReSharper disable once AssignNullToNotNullAttribute
                    using (var reader = new StreamReader(assembly.GetManifestResourceStream(key)))
                    {
                        Cache[cachekey] = reader.ReadToEnd();
                    }
                }
                catch (Exception ex)
                {
                    throw new ApplicationException("ResourceHelpr.GetString('" + resource + "') failed to load the resource code from " + type.AssemblyQualifiedName, ex);
                }
            }

            return Cache[cachekey];
        }

        /// <summary>
        /// Looks in the assembly and namespace that contains <see cref="RH"/> for a resource named <paramref name="resource"/> and returns it as a <seealso cref="Stream"/>.
        /// </summary>
        /// <param name="resource">The name of the resource to be retrieved.</param>
        /// <exception cref="ApplicationException">Assembly could not be loaded or <paramref name="resource"/> was not found.</exception>
        public static Stream GetResourceStream(String resource)
        {
            var type = typeof(RH);
            return GetResourceStream(type, resource);
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
            return GetResourceStream(type, resource);
        }

        private static Stream GetResourceStream(Type type, String resourceCode)
        {
            var assembly = Assembly.GetAssembly(type);

            if (assembly == null)
            {
                throw new ApplicationException("Could not load the assembly.");
            }

            var key = type.Namespace + "." + resourceCode;

            return assembly.GetManifestResourceStream(key);
        }
    }
}
