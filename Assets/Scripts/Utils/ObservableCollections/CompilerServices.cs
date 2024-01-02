namespace Utils.ObservableCollections
{
    internal static class RuntimeHelpersEx
    {
        internal static bool IsReferenceOrContainsReferences<T>()
        {
#if NETSTANDARD2_0 || NET_STANDARD_2_0 || NET_4_6
            return true;
#else
            return RuntimeHelpers.IsReferenceOrContainsReferences<T>();
#endif
        }
    }
}