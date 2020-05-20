using MyGameApplication.Manager;
using MyGameApplication.UI;
using MyGameApplication.Utility;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityStandardAssets.Utility;

namespace MyGameApplication.Maze.NPC.Reactions {
    public class ControlChangeReaction : Reaction {
        private static bool s_CurIsVehicle = false;
        private static Vector3 s_Offset;

        [SerializeField] private GameObject playerToSwitch;
        [SerializeField] private bool isVehicle = false;

        public override async Task React() {
            await Fader.Instance.Fade(1, 1);

            GameObject prePlayer = PlayerControlManager.Instance.CurPlayer;
            if (!s_CurIsVehicle) {
                if (isVehicle) {
                    s_Offset = playerToSwitch.transform.InverseTransformPoint(prePlayer.transform.position);
                }
            }
            else {
                if (!isVehicle) {
                    playerToSwitch.transform.position = prePlayer.transform.TransformPoint(s_Offset);
                    s_Offset = default;
                }
            }
            s_CurIsVehicle = isVehicle;

            PlayerControlManager.Instance.SwitchPlayerControl(playerToSwitch);

            await Fader.Instance.Fade(0, 1);
        }
    }
}
