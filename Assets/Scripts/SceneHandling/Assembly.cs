using System.Runtime.CompilerServices;
using UnityEngine.Scripting;

[assembly: InternalsVisibleTo("SceneHandling.Editor")]

// Always link to make sure InitializeOnLoad methods don't get stripped away
[assembly: AlwaysLinkAssembly]