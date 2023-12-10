using ScriptableObjects.Rooms;

namespace Scene_Loading
{
    public delegate void SceneLoadHandler(RoomType roomToLoad);

    public delegate void SceneUnloadHandler(RoomType roomToUnload);

    public interface ISceneLoadTrigger
    {
        event SceneLoadHandler SceneLoadTriggered;
        event SceneUnloadHandler SceneUnloadTriggered;
    }
}