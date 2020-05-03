using UnityEditor;

namespace MyGameApplication.Maze.NPC.Reactions {
    [CustomEditor(typeof(ExitReaction))]
    public class ExitReactionEditor : ReactionEditor {
        protected override void DrawInspector() {}
    }
}
