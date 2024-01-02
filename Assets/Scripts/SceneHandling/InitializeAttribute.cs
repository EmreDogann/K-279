﻿using UnityEditor;

namespace SceneHandling
{
    /// <summary>Initializes method on editor recompile.</summary>
    /// <remarks>This only works in editor. In build it's a plain attribute and so does nothing.</remarks>
    internal class InitializeInEditorMethodAttribute
#if UNITY_EDITOR
        : InitializeOnLoadMethodAttribute
#else
        : System.Attribute
#endif
    {}

    /// <summary>Initializes class on editor recompile.</summary>
    /// <remarks>This only works in editor. In build it's a plain attribute and so does nothing.</remarks>
    internal class InitializeInEditorAttribute
#if UNITY_EDITOR
        : InitializeOnLoadAttribute
#else
        : System.Attribute
#endif
    {}
}