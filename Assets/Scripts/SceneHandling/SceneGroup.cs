using System;
using System.Collections.Generic;

namespace SceneHandling
{
    [Serializable]
    public sealed class SceneGroup
    {
        public List<ManagedScene> scenes;
    }
}