using System;
using System.Collections.Generic;
using ExitGames.Client.Photon;
using GorillaLocomotion;
using GorillaLocomotion.Climbing;
using GorillaNetworking;
using Masa.Classes;
using Masa.Menu;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.InputSystem;
using Random = UnityEngine.Random;

namespace Masa.Mods
{
    class Movement
    {
        public static void Fly()
        {
            if (ControllerInputPoller.instance.rightControllerPrimaryButton)
            {
                GTPlayer.Instance.transform.position += GTPlayer.Instance.RightHand.controllerTransform.up * -0.1f;
                GorillaTagger.Instance.rigidbody.linearVelocity = Vector3.zero;
            }
        }

        public static bool TPGunWait = false;
        public static void TeleportGun()
        {
            GunData Gun = Main.DeployGun(false);

            if (Gun.Triggering)
            {
                if (!TPGunWait) GTPlayer.Instance.TeleportTo(Gun.Position, Quaternion.identity, false, true);
                TPGunWait = true;
            }
            else
            {
                TPGunWait = false;
            }
        }

        public static bool NetworkedPlatforms = false;
        public static void ReceivePlatform(EventData Data)
        {
            try
            {
                if (NetworkedPlatforms)
                {
                    object[] arguments = Data.CustomData as object[];
                    if (Data.Code == 69)
                    {
                        GameObject.Destroy(CreatePlatform((Vector3)arguments[0], (Quaternion)arguments[1]), 2f);
                    }
                }
            } catch { }
        }

        private static GameObject leftPlat;
        private static GameObject rightPlat;

        private static GameObject leftStick;
        private static GameObject rightStick;
        public static void Platforms()
        {
            if (ControllerInputPoller.instance.rightGrab && rightPlat == null)
            {
                rightPlat = CreatePlatform(GTPlayer.Instance.RightHand.handFollower.transform.position - new Vector3(0, 0.05f, 0), Quaternion.identity);
            }
            else
            {
                if (rightPlat != null && !ControllerInputPoller.instance.rightGrab) GameObject.Destroy(rightPlat);
            }

            if (ControllerInputPoller.instance.leftGrab && leftPlat == null)
            {
                leftPlat = CreatePlatform(GTPlayer.Instance.LeftHand.handFollower.transform.position - new Vector3(0, 0.05f, 0), Quaternion.identity);
            }
            else
            {
                if (leftPlat != null && !ControllerInputPoller.instance.leftGrab) GameObject.Destroy(leftPlat);
            }
        }

        public static void StickyPlatforms()
        {
            if (ControllerInputPoller.instance.rightGrab && rightPlat == null)
            {
                rightPlat = CreatePlatform(GTPlayer.Instance.RightHand.handFollower.transform.position - new Vector3(0, 0.05f, 0), Quaternion.identity);
                rightStick = StickHand(false);
            }
            else
            {
                if (rightPlat != null && !ControllerInputPoller.instance.rightGrab) GameObject.Destroy(rightPlat);
                if (rightStick != null && !ControllerInputPoller.instance.rightGrab) GameObject.Destroy(rightStick);
            }

            if (ControllerInputPoller.instance.leftGrab && leftPlat == null)
            {
                leftPlat = CreatePlatform(GTPlayer.Instance.LeftHand.handFollower.transform.position - new Vector3(0, 0.05f, 0), Quaternion.identity);
                leftStick = StickHand(true);
            }
            else
            {
                if (leftPlat != null && !ControllerInputPoller.instance.leftGrab) GameObject.Destroy(leftPlat);
                if (leftStick != null && !ControllerInputPoller.instance.leftGrab) GameObject.Destroy(leftStick);
            }
        }

        public static GameObject CreatePlatform(Vector3 position, Quaternion rotation)
        {
            GameObject platform;
            platform = GameObject.CreatePrimitive(PrimitiveType.Cube);
            platform.transform.localScale = new Vector3(0.3f, 0.01f, 0.3f);
            platform.transform.position = position;
            platform.transform.rotation = rotation;
            platform.GetComponent<Renderer>().material.color = Color.black;
            return platform;
        }

        public static GameObject StickHand(bool isleft)
        {
            Vector3[] pos = {
                new Vector3(0, 1f, 0),
                new Vector3(0, -1f, 0),
                new Vector3(1f, 0, 0),
                new Vector3(-1f, 0, 0),
                new Vector3(0, 0, 1f),
                new Vector3(0, 0, -1f)
            };

            Quaternion[] rot = {
                Quaternion.Euler(90, 0, 0),
                Quaternion.Euler(-90, 0, 0),
                Quaternion.Euler(0, -90, 0),
                Quaternion.Euler(0, 90, 0),
                Quaternion.Euler(0, 0, 0),
                Quaternion.Euler(0, 180, 0)
            };

            GameObject thing = new GameObject();
            thing.transform.position = isleft ? GTPlayer.Instance.LeftHand.handFollower.transform.position : GTPlayer.Instance.RightHand.handFollower.transform.position;

            for (int i = 0; i < pos.Length; i++)
            {
                GameObject temp = GameObject.CreatePrimitive(PrimitiveType.Cube);
                temp.transform.parent = thing.transform;
                temp.transform.localPosition = pos[i] / 2f;
                temp.transform.localRotation = rot[i];
                temp.transform.localScale = new Vector3(1.2f, 1.2f, 0.3f);
                temp.GetComponent<Renderer>().enabled = false;
            }

            thing.transform.localScale = Vector3.one * 0.03f;
            return thing;
        }

        public static void RigGun()
        {
            GunData Data = Main.DeployGun(false);
            if (Data.Triggering)
            {
                VRRig.LocalRig.enabled = false;
                VRRig.LocalRig.transform.position = Data.Position + Vector3.up;
            }
            else
            {
                VRRig.LocalRig.enabled = true;
            }
        }

        public static void SetClipping(bool enable)
        {
            foreach (MeshCollider collider in Resources.FindObjectsOfTypeAll<MeshCollider>())
            {
                collider.enabled = enable;
            }
        }

        public static void Noclip()
        {
            SetClipping(!ControllerInputPoller.instance.leftControllerTriggerButton);
        }

        public static float LongarmLength = 1.5f;
        public static void Longarms()
        {
            GTPlayer.Instance.transform.localScale = Vector3.one * LongarmLength;
        }

        public static void MultipiedLongarms() // idk how to do this without breaking everything, so i snatched this from seralyth
        {
            GTPlayer.Instance.GetControllerTransform(true).transform.position = GorillaTagger.Instance.headCollider.transform.position - (GorillaTagger.Instance.headCollider.transform.position - GorillaTagger.Instance.leftHandTransform.position) * LongarmLength;
            GTPlayer.Instance.GetControllerTransform(false).transform.position = GorillaTagger.Instance.headCollider.transform.position - (GorillaTagger.Instance.headCollider.transform.position - GorillaTagger.Instance.rightHandTransform.position) * LongarmLength;
        }

        public static GorillaVelocityTracker LeftVelocity;
        public static GorillaVelocityTracker RightVelocity;

        public static void CreateVelocityTrackers()
        {
            if (LeftVelocity == null) LeftVelocity = GTPlayer.Instance.LeftHand.controllerTransform.gameObject.AddComponent<GorillaVelocityTracker>();
            if (RightVelocity == null) RightVelocity = GTPlayer.Instance.RightHand.controllerTransform.gameObject.AddComponent<GorillaVelocityTracker>();
        }

        public static float PredictionsStrength = 0.05f;
        public static void Predictions()
        {
            CreateVelocityTrackers();

            GTPlayer.Instance.GetControllerTransform(true).transform.position -= LeftVelocity.GetAverageVelocity(true, 0) * PredictionsStrength;
            GTPlayer.Instance.GetControllerTransform(false).transform.position -= LeftVelocity.GetAverageVelocity(true, 0) * PredictionsStrength;
        }

        private static float pitch;
        public static void KeyboardFly()
        {
            //GorillaTagger.Instance.rigidbody.linearVelocity = Vector3.up * 0.076f;
            GorillaTagger.Instance.rigidbody.AddForce(-Physics.gravity * GorillaTagger.Instance.rigidbody.mass);
            GorillaTagger.Instance.rigidbody.linearVelocity = Vector3.zero;

            float speed = Keyboard.current.leftShiftKey.isPressed ? 0.15f : 0.1f;

            if (Keyboard.current.wKey.isPressed) GTPlayer.Instance.transform.position += GTPlayer.Instance.headCollider.transform.forward * speed;
            if (Keyboard.current.sKey.isPressed) GTPlayer.Instance.transform.position -= GTPlayer.Instance.headCollider.transform.forward * speed;
            if (Keyboard.current.dKey.isPressed) GTPlayer.Instance.transform.position += GTPlayer.Instance.headCollider.transform.right * speed;
            if (Keyboard.current.aKey.isPressed) GTPlayer.Instance.transform.position -= GTPlayer.Instance.headCollider.transform.right * speed;
            if (Keyboard.current.spaceKey.isPressed) GTPlayer.Instance.transform.position += GTPlayer.Instance.headCollider.transform.up * speed;
            if (Keyboard.current.leftCtrlKey.isPressed) GTPlayer.Instance.transform.position -= GTPlayer.Instance.headCollider.transform.up * speed;

            if (Mouse.current.rightButton.isPressed) // credits to wherever the heck i found ts
            {
                Vector2 delta = Mouse.current.delta.ReadValue();

                float sensitivity = 0.15f;

                GTPlayer.Instance.transform.Rotate(0f, delta.x * sensitivity, 0f);

                pitch -= delta.y * sensitivity;
                pitch = Mathf.Clamp(pitch, -80f, 80f);

                Camera.main.transform.localRotation = Quaternion.Euler(pitch, 0f, 0f);
            }
        }

        public static void InitMaps()
        {
            foreach (GTZone zone in Enum.GetValues(typeof(GTZone)))
            {
                GTZone capturedZone = zone;

                Buttons.Buttans.Add(new Button
                {
                    Category = Buttons.Categories.Maps,
                    Title = "Teleport to " + zone.ToString(),
                    OneShot = true,
                    Method = () => TeleportToMap(capturedZone)
                });
            }
        }

        public static void TeleportToMap(GTZone zone)
        {
            var controller = PhotonNetworkController.Instance;
            foreach (GorillaNetworkJoinTrigger trigger in PrivateManager.GetField<List<GorillaNetworkJoinTrigger>>(PhotonNetworkController.Instance, "allJoinTriggers"))
            {
                if (trigger.zone == zone)
                {
                    trigger.OnBoxTriggered();
                    ZoneManagement.SetActiveZone(zone);
                    GTPlayer.Instance.TeleportTo(trigger.transform.position, Quaternion.identity, false, true);
                    return;
                }
            }
        }

        public static void NetworkedPlatformGun()
        {
            GunData Data = Main.DeployGun(false);
            if (Data.Triggering)
            {
                GameObject Platform = CreatePlatform(Data.Position, Quaternion.Euler(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360)));
                GameObject.Destroy(Platform, 2f);

                // i think code 69 is the typical one
                PhotonNetwork.RaiseEvent(69, new object[] { Platform.transform.position, Platform.transform.rotation }, new RaiseEventOptions { Receivers = ReceiverGroup.Others }, SendOptions.SendReliable);
            }
        }

        public static void NoGravity()
        {
            GorillaTagger.Instance.rigidbody.AddForce(-Physics.gravity, ForceMode.Acceleration);
        }
    }
}