using System.Collections.Generic;

namespace SceneHandling
{
    public interface ISceneDataMapGenerator
    {
        public Dictionary<string, object> GenerateDataMap();
    }
}