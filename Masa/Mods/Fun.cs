using Masa.Classes;
using Masa.Menu;
using UnityEngine;
using GorillaLocomotion;
using Photon.Pun;
using UnityEngine.InputSystem;
using GorillaLocomotion.Swimming;
using System.Diagnostics.Eventing.Reader;

namespace Masa.Mods
{
    class Fun
    {
        public static VRRig CopyingRig = null;
        public static void CopyPlayer()
        {
            GunData Data = Main.DeployGun(true);
            if (Data.Deployed)
            {
                if (Data.Triggering) CopyingRig = Data.Rig;
            }
            else
            {
                CopyingRig = null;
            }

            if (CopyingRig != null)
            {
                SetGhost(true);

                VRRig.LocalRig.enabled = false;

                VRRig.LocalRig.transform.position = CopyingRig.syncPos;
                VRRig.LocalRig.transform.rotation = CopyingRig.syncRotation;

                VRRig.LocalRig.leftHand.rigTarget.transform.position = CopyingRig.leftHand.rigTarget.transform.position;
                VRRig.LocalRig.rightHand.rigTarget.transform.position = CopyingRig.rightHand.rigTarget.transform.position;

                VRRig.LocalRig.leftHand.rigTarget.transform.rotation = CopyingRig.leftHand.rigTarget.transform.rotation;
                VRRig.LocalRig.rightHand.rigTarget.transform.rotation = CopyingRig.rightHand.rigTarget.transform.rotation;

                VRRig.LocalRig.leftIndex.calcT = CopyingRig.leftIndex.calcT;
                VRRig.LocalRig.leftMiddle.calcT = CopyingRig.leftMiddle.calcT;
                VRRig.LocalRig.leftThumb.calcT = CopyingRig.leftThumb.calcT;

                VRRig.LocalRig.leftIndex.LerpFinger(1f, false);
                VRRig.LocalRig.leftMiddle.LerpFinger(1f, false);
                VRRig.LocalRig.leftThumb.LerpFinger(1f, false);
        
                VRRig.LocalRig.rightIndex.calcT = CopyingRig.rightIndex.calcT;
                VRRig.LocalRig.rightMiddle.calcT = CopyingRig.rightMiddle.calcT;
                VRRig.LocalRig.rightThumb.calcT = CopyingRig.rightThumb.calcT;
        
                VRRig.LocalRig.rightIndex.LerpFinger(1f, false);
                VRRig.LocalRig.rightMiddle.LerpFinger(1f, false);
                VRRig.LocalRig.rightThumb.LerpFinger(1f, false);

                VRRig.LocalRig.head.rigTarget.transform.rotation = CopyingRig.head.rigTarget.transform.rotation;
            }
            else
            {
                SetGhost(false);
            }
        }

        public static bool GhostWait;
        public static bool InGhost;
        public static void GhostMonke()
        {
            if (ControllerInputPoller.instance.rightControllerPrimaryButton && !GhostWait)
            {
                GhostWait = true;
                InGhost = !InGhost;
            }
            else if (!ControllerInputPoller.instance.rightControllerPrimaryButton)
            {
                GhostWait = false;
            }

            VRRig.LocalRig.enabled = !InGhost;
        }

        public static void SetGhost(bool Enabled)
        {
            if (GorillaTagger.Instance.offlineVRRig != null) GorillaTagger.Instance.offlineVRRig.enabled = !Enabled;
            if (VRRig.LocalRig != null) VRRig.LocalRig.enabled = !Enabled;
        }

        public static float UnLagTime;
        public static void FakeLag()
        {
            VRRig.LocalRig.enabled = false;
            if (UnLagTime < Time.time)
            {
                VRRig.LocalRig.enabled = true;
                UnLagTime += 0.5f;
            }
        }

        public static void Tracers()
        {
            foreach (VRRig rig in VRRigCache.ActiveRigs)
            {
                if (rig.Creator.ActorNumber != PhotonNetwork.LocalPlayer.ActorNumber)
                {
                    LineRenderer rend = new GameObject().AddComponent<LineRenderer>();
                    rend.SetPosition(0, GTPlayer.Instance.LeftHand.controllerTransform.position);
                    rend.SetPosition(1, rig.headMesh.transform.position);
                    rend.material.shader = Shader.Find("GUI/Text Shader");
                    rend.startWidth = 0.025f;
                    rend.endWidth = 0.025f;
                    rend.startColor = rig.playerColor;
                    rend.endColor = rig.playerColor;
                    GameObject.Destroy(rend, Time.deltaTime); // maybe it's ugly but it woks
                }
            }
        }

        public static void InfectionTracers()
        {
            foreach (VRRig rig in VRRigCache.ActiveRigs)
            {
                if (rig.Creator.ActorNumber != PhotonNetwork.LocalPlayer.ActorNumber)
                {
                    LineRenderer rend = new GameObject().AddComponent<LineRenderer>();
                    rend.SetPosition(0, GTPlayer.Instance.LeftHand.controllerTransform.position);
                    rend.SetPosition(1, rig.headMesh.transform.position);
                    rend.material.shader = Shader.Find("GUI/Text Shader");
                    rend.startWidth = 0.025f;
                    rend.endWidth = 0.025f;
                    Color clr = Main.IsTagged(rig) ? Color.red : Color.green;
                    rend.startColor = clr;
                    rend.endColor = clr;
                    GameObject.Destroy(rend, Time.deltaTime); // maybe it's ugly but it woks
                }
            }
        }


        public static void Punch()
        {
            foreach (VRRig rig in VRRigCache.ActiveRigs)
            {
                if (rig != VRRig.LocalRig)
                {
                    VelocityTracker leftTracker = rig.leftHandTransform.GetComponent<VelocityTracker>();
                    if (leftTracker == null) leftTracker = rig.leftHandTransform.gameObject.AddComponent<VelocityTracker>();

                    VelocityTracker rightTracker = rig.rightHandTransform.GetComponent<VelocityTracker>();
                    if (rightTracker == null) rightTracker = rig.rightHandTransform.gameObject.AddComponent<VelocityTracker>();

                    Vector3 leftVelocity = leftTracker.GetVelocity();
                    Vector3 rightVelocity = rightTracker.GetVelocity();

                    if (Vector3.Distance(rig.leftHandTransform.position, GTPlayer.Instance.headCollider.transform.position) < 0.25f)
                    {
                        GTPlayer.Instance.playerRigidBody.linearVelocity += leftVelocity.normalized * 10f;
                    }

                    if (Vector3.Distance(rig.rightHandTransform.position, GTPlayer.Instance.headCollider.transform.position) < 0.25f)
                    {
                        GTPlayer.Instance.playerRigidBody.linearVelocity += rightVelocity.normalized * 10f;
                    }
                }
            }
        }

        public static void FishHands()
        {
            GTPlayer.Instance.GetControllerTransform(false).transform.position = GorillaTagger.Instance.leftHandTransform.position;
            GTPlayer.Instance.GetControllerTransform(false).transform.rotation = GorillaTagger.Instance.leftHandTransform.rotation;

            GTPlayer.Instance.GetControllerTransform(true).transform.position = GorillaTagger.Instance.rightHandTransform.position;
            GTPlayer.Instance.GetControllerTransform(true).transform.rotation = GorillaTagger.Instance.rightHandTransform.rotation;
        }

        public static void NoFingerMovement()
        {
            VRRig.LocalRig.leftIndex.calcT = 1f;
            VRRig.LocalRig.leftMiddle.calcT = 1f;
            VRRig.LocalRig.leftThumb.calcT = 1f;
            VRRig.LocalRig.rightIndex.calcT = 1f;
            VRRig.LocalRig.rightMiddle.calcT = 1f;
            VRRig.LocalRig.rightThumb.calcT = 1f;
            VRRig.LocalRig.leftIndex.LerpFinger(1f, false);
            VRRig.LocalRig.leftMiddle.LerpFinger(1f, false);
            VRRig.LocalRig.leftThumb.LerpFinger(1f, false);
            VRRig.LocalRig.rightIndex.LerpFinger(1f, false);
            VRRig.LocalRig.rightMiddle.LerpFinger(1f, false);
            VRRig.LocalRig.rightThumb.LerpFinger(1f, false);
        }

        public static void FixHead() => VRRig.LocalRig.head.trackingRotationOffset = Vector3.zero;

        public static float HeadSpinSpeed = 10f;
        public static void SpinHead(string axis)
        {
            if (axis == "x") VRRig.LocalRig.head.trackingRotationOffset.x += HeadSpinSpeed;
            else if (axis == "y") VRRig.LocalRig.head.trackingRotationOffset.y += HeadSpinSpeed;
            else if (axis == "z") VRRig.LocalRig.head.trackingRotationOffset.z += HeadSpinSpeed;
        }

        public static void SpazHead()
        {
            VRRig.LocalRig.head.trackingRotationOffset.x = Random.Range(0, 360);
            VRRig.LocalRig.head.trackingRotationOffset.y = Random.Range(0, 360);
            VRRig.LocalRig.head.trackingRotationOffset.z = Random.Range(0, 360);
        }

        public static float HandTapDelay;
        public static void PlayHandTap(int index)
        {
            if (HandTapDelay < Time.time)
            {
                HandTapDelay = Time.time + 0.15f;
                if (NetworkSystem.Instance.InRoom)
                {
                    GorillaTagger.Instance.myVRRig.SendRPC("RPC_PlayHandTap", RpcTarget.All, index, false, 1f);
                    Safety.FlushRPCs();
                }
                else
                {
                    VRRig.LocalRig.PlayHandTapLocal(index, false, 1f);
                }
            }
        }

        public static void SoundSpam(int index)
        {
            if (NetworkSystem.Instance.InRoom)
            {
                if (ControllerInputPoller.instance.rightGrab || Keyboard.current.eKey.isPressed)
                {
                    PlayHandTap(index);
                }
            }
        }

        public static void RandomSpam()
        {
            SoundSpam(Random.Range(0, GTPlayer.Instance.materialData.Count));
        }

        public static void Slap() // 338
        {
            foreach (VRRig rig in VRRigCache.ActiveRigs)
            {
                if (rig != VRRig.LocalRig)
                {
                    if (Vector3.Distance(rig.headMesh.transform.position, GTPlayer.Instance.LeftHand.controllerTransform.position) < 0.3f &&
                        Vector3.Distance(rig.headMesh.transform.position, GTPlayer.Instance.RightHand.controllerTransform.position) < 0.3f)
                    {
                        PlayHandTap(338);
                    }
                }
            }
        }

        public static void SetupEverySoundSpam()
        {
            for (int i = 0; i < GTPlayer.Instance.materialData.Count; i++)
            {
                int index = i;
                Buttons.Buttans.Add(new Button { Category = Buttons.Categories.EverySound, Title = $"Sound Spam [<color=grey>{GTPlayer.Instance.materialData[index].matName}</color>]", Method = () => SoundSpam(index) });
            }

            Main.RefreshMods();
        }

        //public static float DoorDelay;
        public static void OpenNaNoor(bool opened)
        {
            //if (DoorDelay < Time.time)
            //{
            //    DoorDelay = Time.time + 0.15f;
                GameObject.Find("Environment Objects/LocalObjects_Prefab/CityToBasement/DungeonEntrance/DungeonDoor_Prefab").GetComponent<PhotonView>().RPC("ChangeDoorState", RpcTarget.AllViaServer, opened ? GTDoor.DoorState.Opening : GTDoor.DoorState.Closing);
                Safety.FlushRPCs();
            //}
        }

        public static bool DoorState = false;
        public static void OpenNaNoorSpam()
        {
            //if (DoorDelay < Time.time)
            //{
                DoorState = !DoorState;
                OpenNaNoor(DoorState);
            //}
        }

        public static float SplashDelay = 0f;
        public static void SplashWater(Vector3 position, Quaternion rotaion, float size, bool big)
        {
            if (NetworkSystem.Instance.InRoom)
            {
                if (SplashDelay < Time.time)
                {
                    SplashDelay = Time.time + 0.15f;
                    if (Vector3.Distance(position, GTPlayer.Instance.headCollider.transform.position) > 3f)
                    {
                        VRRig.LocalRig.enabled = false;
                        VRRig.LocalRig.transform.position = position - Vector3.up;
                    }
                    else
                    {
                        VRRig.LocalRig.enabled = true;
                    }

                    //Vector3 splashPosition, Quaternion splashRotation, float splashScale, float boundingRadius, bool bigSplash, bool enteringWater
                    GorillaTagger.Instance.myVRRig.SendRPC("RPC_PlaySplashEffect", RpcTarget.All, position, rotaion, size, size, big, false);
                    Safety.FlushRPCs();
                }
            }
        }

        public static void SplashHands()
        {
            bool big = Random.Range(0, 1) == 1;
            if (ControllerInputPoller.instance.leftGrab) SplashWater(GTPlayer.Instance.LeftHand.controllerTransform.position, GTPlayer.Instance.LeftHand.controllerTransform.rotation, 5, big);
            if (ControllerInputPoller.instance.rightGrab) SplashWater(GTPlayer.Instance.RightHand.controllerTransform.position, GTPlayer.Instance.RightHand.controllerTransform.rotation, 5, big);
        }

        public static void Cum(bool me)
        {
            VRRig rig;

            if (me) rig = VRRig.LocalRig;
            else
            {
                GunData Data = Main.DeployGun(true);
                if (Data.Triggering) rig = Data.Rig;
                else rig = null;
            }

            if (rig != null) SplashWater(rig.transform.position - rig.transform.up * 0.4f + rig.transform.forward * 0.15f, Quaternion.identity, 5, Random.Range(0, 1) == 1);
        }

        public static void SplashGun()
        {
            GunData data = Main.DeployGun(false);
            if (data.Triggering) SplashWater(data.Position, Quaternion.identity, 5, Random.Range(0, 1) == 1);
        }

        public static void Rainbow()
        {
            Main.SetColor(Color.HSVToRGB((Time.time / 5) % 1, 1, 1));
        }
    }
}
