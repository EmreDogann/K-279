using System;
using System.Reflection;
using UnityEngine;

namespace SceneHandling
{
    /// <summary>
    ///     <para>An attribute that specifies a file location relative a location folder. See: <see cref="FilePathAttribute.Location" />.</para>
    ///     <para>
    ///         Using this attribute on a ScriptableObjectSingleton will enable that singleton to persist between Unity Editor sessions (and, if set, be available for
    ///         use in builds as well. See: <see cref="FilePathAttribute.UsageScope" />).
    ///     </para>
    /// </summary>
    // ReSharper disable once RedundantAttributeUsageProperty
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    public sealed class FilePathAttribute : Attribute
    {
        public UsageScope Scope { get; }

        private string Path { get; set; }
        private string FullPath { get; set; }
        private readonly Location _location;

        internal string Filepath
        {
            get
            {
                if (FullPath == null && Path != null)
                {
                    FullPath = CombineFilePath(Path, _location);
                    Path = null;
                }

                return FullPath;
            }
        }

        /// <param name="relativePath">A custom path allowing you to set relative path and name of file. If no extension is provided, then the default will be `.asset`.</param>
        /// <param name="location">Where to store this asset in the project directory. See Also: FilePathAttribute.Location.</param>
        /// <param name="scope">How will this singleton be used? Determines whether or not singleton is saved to disk and loaded.</param>
        public FilePathAttribute(string relativePath,
            Location location = Location.ProjectSettings, UsageScope scope = UsageScope.EditorOnly)
        {
            Path = !string.IsNullOrEmpty(relativePath)
                ? relativePath
                : throw new ArgumentException("Invalid relative path (it is empty)");
            _location = location;
            Scope = scope;
        }

        /// <param name="fileName">The file name, using the class' type name. Will use default extension '.asset'.</param>
        /// <param name="location">Where to store this asset in the project directory. See Also: FilePathAttribute.Location.</param>
        /// <param name="scope">How will this singleton be used? Determines whether or not singleton is saved to disk and loaded.</param>
        public FilePathAttribute(Type fileName,
            Location location = Location.ProjectSettings, UsageScope scope = UsageScope.EditorOnly)
        {
            Path = fileName.Name;
            _location = location;
            Scope = scope;
        }

        public static FilePathAttribute Retrieve(Type type)
        {
            return type.GetCustomAttribute<FilePathAttribute>();
        }

        private static string CombineFilePath(string relativePath, Location location)
        {
            if (relativePath[0] == '/')
            {
                relativePath = relativePath.Substring(1);
            }

            if (!System.IO.Path.HasExtension(relativePath))
            {
                relativePath = $"{relativePath}.asset";
            }

            switch (location)
            {
                case Location.ProjectSettings:
                    return "ProjectSettings/" + relativePath;
                default:
                    Debug.LogError("Unhandled enum: " + location);
                    return relativePath;
            }
        }

        /// <summary>
        ///     Specifies the folder location that Unity uses together with the relative path provided in the FilePathAttribute constructor.
        /// </summary>
        public enum Location
        {
            /// <summary>
            ///     <para>Use this location to save a file relative to the Project Folder. Useful for per-project files (not shared between projects).</para>
            /// </summary>
            ProjectSettings
        }

        /// <summary>
        ///     Used to determine where this singleton is used and should persist.
        /// </summary>
        public enum UsageScope
        {
            /// <summary>
            ///     Will only persist in editor.
            /// </summary>
            EditorOnly,
            /// <summary>
            ///     Will persist in both editor and builds.
            /// </summary>
            EditorAndBuild
        }
    }
}