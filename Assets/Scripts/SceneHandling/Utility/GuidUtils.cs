using System.Linq;

namespace SceneHandling.Utility
{
    public static class GuidUtils
    {
        /// <summary>
        ///     GUID of all zeros. Invalid assets have this GUID.
        /// </summary>
        public const string AllZeroGuid = "00000000000000000000000000000000";

        /// <summary>
        ///     Returns if the given <paramref name="guid" /> is valid. A valid GUID is 32 chars of hexadecimals.
        /// </summary>
        public static bool IsValidGuid(this string guid)
        {
            return guid.Length == 32 && guid.ToUpper().All("0123456789ABCDEF".Contains);
        }

        /// <summary>
        ///     If the given GUID is null or whitespace returns <see cref="AllZeroGuid" />. Otherwise returns as-is.
        /// </summary>
        public static string GuardGuidAgainstNullOrWhitespace(this string guid)
        {
            return string.IsNullOrWhiteSpace(guid) ? AllZeroGuid : guid;
        }
    }
}