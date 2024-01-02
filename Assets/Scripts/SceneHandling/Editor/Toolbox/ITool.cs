using System;

namespace SceneHandling.Editor.Toolbox
{
    internal interface ITool
    {
        void Draw(Action closeToolbox);
        float GetHeight();
    }
}