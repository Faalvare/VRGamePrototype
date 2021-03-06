#if UNITY_STANDALONE_WIN || UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

namespace Autohand.Demo{
    public class SteamVRTeleporterLink : MonoBehaviour{
        public Teleporter teleport;
        public SteamVR_Input_Sources handType;
        public SteamVR_Action_Boolean teleportAction;
        bool teleporting;
        
        private void FixedUpdate() {
            if(!teleporting && teleportAction.GetState(handType)){
                teleporting = true;
                teleport.StartTeleport();
            }
            else if(teleporting && !teleportAction.GetState(handType)){
                teleporting = false;
                teleport.Teleport();
            }
        }
    }
}
#endif
