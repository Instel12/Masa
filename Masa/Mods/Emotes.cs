using System.Collections.Generic;
using Masa.Classes;
using Masa.Menu;
using Photon.Voice.Unity;
using UnityEngine;

namespace Masa.Mods
{
    class Emotes
    {
        public class Emote
        {
            public string Title;
            public string Animation;
            public string Audio;
        }

        public static Emote[] emotes = new Emote[]
        {
            new Emote { Title = "Default", Animation = "Dance Moves", Audio = "default" },
            new Emote { Title = "Take The L", Animation = "TakeTheL", Audio = "takethel" },
            new Emote { Title = "Reanimated", Animation = "Reanimated", Audio = "reanimated" },
            new Emote { Title = "Electro Shuffle", Animation = "ElectroShuffle", Audio = "electroshuffle" },
            new Emote { Title = "Orange Justice", Animation = "OrangeJustice", Audio = "oj" },
            new Emote { Title = "Ride The Pony", Animation = "RideThePony", Audio = "ridethepony" },
            new Emote { Title = "Fresh", Animation = "Emote_Fresh", Audio = "fresh" },
            new Emote { Title = "Electro Swing", Animation = "ElectroSwing", Audio = "swing" },
            new Emote { Title = "Floss", Animation = "Emote_FlossDance_CMM", Audio = "floss" },
            new Emote { Title = "Disco Fever", Animation = "DiscoFever", Audio = "discofever" },
            new Emote { Title = "Boogie Down", Animation = "BoogieDownLoop", Audio = "boogiedown" },
            new Emote { Title = "The Robot", Animation = "Emote_RobotDance", Audio = "therobot" },
            new Emote { Title = "Best Mates", Animation = "BestMates", Audio = "bestmates" },
            new Emote { Title = "Paws & Claws", Animation = "Paws&Claws", Audio = "pawsclaws" },
            new Emote { Title = "Get Griddy", Animation = "Get Griddy", Audio = "Emote_Griddles_Music_Loop_01" },
            new Emote { Title = "Pull Up", Animation = "Pull Up", Audio = "Gas_Station_Loop" },
            new Emote { Title = "Popular Vibe", Animation = "Popular Vibe", Audio = "Emote_SpeedDial_Loop" },
            new Emote { Title = "Lucid Dreams", Animation = "Lucid DreamsLoop", Audio = "Emote_KelpLinen_Music_Loop" },
            new Emote { Title = "Empty Out Your Pockets", Animation = "Empty Out Your PocketsLoop", Audio = "eoyp" },
            new Emote { Title = "What You Want", Animation = "WhatYouWant", Audio = "whatyouwant" },
            new Emote { Title = "The Renegade", Animation = "The Renegade", Audio = "Emote_Just_Home_Music_Loop" },
            new Emote { Title = "Jabba Switchway", Animation = "Jabba Switchway Loop", Audio = "Emote_January_Bop_Loop" },
            new Emote { Title = "Infinite Dab", Animation = "InfinidabLoop", Audio = "infinitedab" },
            new Emote { Title = "Celebrate Me", Animation = "Celebrate Me", Audio = "IP_Emote_Cottontail_Loop" },
            new Emote { Title = "Billy Bounce", Animation = "BillyBounce", Audio = "billybounce" },
            new Emote { Title = "Windmill Floss", Animation = "WindmillFloss", Audio = "whirlfloss" },
            new Emote { Title = "Hype", Animation = "Hype", Audio = "hype" },
            new Emote { Title = "Entranced", Animation = "Entranced", Audio = "entranced" },
            new Emote { Title = "Laugh It Up", Animation = "LaughItUp", Audio = "Emote_Laugh_01" },
            new Emote { Title = "Snoop Walk", Animation = "SnoopWalk", Audio = "snoopwalk" },
            new Emote { Title = "Scenario", Animation = "Scenario", Audio = "scenario" },
            new Emote { Title = "Night Out", Animation = "Night Out", Audio = "nightout" },
            new Emote { Title = "Point and Strut", Animation = "pointandstrut", Audio = "pointandstrut" },
            new Emote { Title = "Moongazer", Animation = "moongazer", Audio = "moongazer" },
            new Emote { Title = "Rollie", Animation = "Rollie", Audio = "Emote_Twist_Daytona_Music_Loop_01" },
            new Emote { Title = "Heel Click Breakdown", Animation = "HEEL", Audio = "heelclickbreakdown" },
            new Emote { Title = "Switchstep", Animation = "SwitchStep", Audio = "switchstep" },
            new Emote { Title = "Freestylin'", Animation = "Freestylin'", Audio = "freestylin" },
            new Emote { Title = "Go Mufasa", Animation = "Go Mufasa", Audio = "Emote_Sandwich_Bop_Loop" },
            new Emote { Title = "Jubi Slide", Animation = "jubislide", Audio = "Emote_GoodbyeUpbeat_Loop" },
            new Emote { Title = "Running Man", Animation = "RunningMan", Audio = "Athena_Emote_Music_RunningMan" },
            new Emote { Title = "Zany", Animation = "Zany", Audio = "zany" },
            new Emote { Title = "Pumpernickel", Animation = "pumpernickel2", Audio = "Athena_Emotes_Music_PumpDance" },
            new Emote { Title = "Pony Up", Animation = "RideThePony", Audio = "ponyup" },
            new Emote { Title = "Hula", Animation = "HULA", Audio = "emote_hula_01" },
            new Emote { Title = "Never Gonna'", Animation = "Never Gonna Loop", Audio = "Emote_NeverGonna_Loop_01" },
            new Emote { Title = "Say So", Animation = "Say So", Audio = "Emote_HotPink_Loop_258" },
            new Emote { Title = "Take It Slow", Animation = "Takeitslow", Audio = "takeitslow" },
            new Emote { Title = "Macarena", Animation = "Macarena", Audio = "Emote_Macaroon_Music_Loop_01" },
            new Emote { Title = "Cupid's Arrow", Animation = "cupid", Audio = "cupid" },
            new Emote { Title = "Gangnam Style", Animation = "gangnam", Audio = "gangnam" },
            new Emote { Title = "Real Slim Shady", Animation = "realslimshady", Audio = "slim" },
            new Emote { Title = "Party Hips", Animation = "partyhips", Audio = "partyhips" },
            new Emote { Title = "Out West", Animation = "outwest", Audio = "outwest" },
            new Emote { Title = "My World", Animation = "myworld", Audio = "Myworld" },
            new Emote { Title = "Jake Bug Dance", Animation = "Jake", Audio = "jake" },
            new Emote { Title = "Miku Miku Beam", Animation = "miku", Audio = "miku" },
        };

        public static AssetBundle Bundle;
        public static void Download()
        {
            int buttonIndex = Buttons.GetIndex("Download Emotes");
            Buttons.Buttans[buttonIndex].Method = null;
            Buttons.Buttans[buttonIndex].Title = "Downloading";

            CoroutineRunner.Coroutine(AssetTools.DownloadBundle("https://instel12.github.io/MasaAdmin/fn", (AssetBundle bundle) =>
            {
                Bundle = bundle;
                Buttons.Buttans.Remove(Buttons.Buttans[buttonIndex]);

                foreach (Emote e in emotes)
                {
                    Buttons.Buttans.Add(new Button
                    {
                        Category = Buttons.Categories.Emotes,
                        Title = e.Title,
                        OneShot = true,
                        Method = () =>
                        {
                            HitDatEmote(e.Animation, e.Audio, -1, true);
                        }
                    });
                }

                Notifications.SendNotification("[<color=green>Emotes</color>] Emote bundle downloaded successfully!");
            }));
        }

        #region iiDk's stuff
        public static GameObject LoadAsset(string assetName)
        {
            GameObject gameObject = null;

            gameObject = GameObject.Instantiate<GameObject>(Bundle.LoadAsset<GameObject>(assetName));

            return gameObject;
        }

        public static GameObject audiomgr = null;
        public static void Play2DAudio(AudioClip sound, float volume, bool looping = false)
        {
            if (audiomgr == null)
            {
                audiomgr = new GameObject("2DAudioMgr");
                AudioSource temp = audiomgr.AddComponent<AudioSource>();
                temp.spatialBlend = 0f;
            }
            AudioSource ausrc = audiomgr.GetComponent<AudioSource>();
            ausrc.volume = volume;
            ausrc.loop = looping;
            if (!looping)
                ausrc.PlayOneShot(sound);
            else
            {
                ausrc.clip = sound;
                ausrc.Play();
            }
        }

        public static Dictionary<string, AudioClip> audioPool = new Dictionary<string, AudioClip> { };
        public static AudioClip LoadSoundFromResource(string resourcePath)
        {
            AudioClip sound = null;

            if (!audioPool.ContainsKey(resourcePath))
            {
                sound = Bundle.LoadAsset(resourcePath) as AudioClip;
                audioPool.Add(resourcePath, sound);
            }
            else
                sound = audioPool[resourcePath];

            return sound;
        }

        private static readonly List<GameObject> portedCosmetics = new List<GameObject> { };
        public static void DisableCosmetics()
        {
            try
            {
                VRRig.LocalRig.transform.Find("rig/head/gorillaface").gameObject.layer = LayerMask.NameToLayer("Default");
                foreach (GameObject Cosmetic in VRRig.LocalRig.cosmetics)
                {
                    if (Cosmetic.activeSelf && Cosmetic.transform.parent == VRRig.LocalRig.mainCamera.transform.Find("HeadCosmetics"))
                    {
                        portedCosmetics.Add(Cosmetic);
                        Cosmetic.transform.SetParent(VRRig.LocalRig.headMesh.transform, false);
                        Cosmetic.transform.localPosition += new Vector3(0f, 0.1333f, 0.1f);
                    }
                }
            }
            catch { }
        }

        public static void EnableCosmetics()
        {
            VRRig.LocalRig.transform.Find("rig/head/gorillaface").gameObject.layer = LayerMask.NameToLayer("MirrorOnly");
            foreach (GameObject Cosmetic in portedCosmetics)
            {
                Cosmetic.transform.SetParent(VRRig.LocalRig.mainCamera.transform.Find("HeadCosmetics"), false);
                Cosmetic.transform.localPosition -= new Vector3(0f, 0.1333f, 0.1f);
            }
            portedCosmetics.Clear();
        }

        public static GameObject Kyle;
        public static float emoteTime;

        public static Vector3 archivePosition;

        public static void HitDatEmote(string emoteName, string emoteSound, float animationTime = -1f, bool looping = false)
        {
            HasClosedMenuYet = false;

            if (Kyle != null)
                GameObject.Destroy(Kyle);

            VRRig.LocalRig.enabled = false;
            DisableCosmetics();

            Play2DAudio(LoadSoundFromResource("play"), 0.5f);

            archivePosition = GorillaTagger.Instance.transform.position;
            GorillaLocomotion.GTPlayer.Instance.GetControllerTransform(false).parent.rotation *= Quaternion.Euler(0f, 180f, 0f);

            Kyle = LoadAsset("Rig");
            Kyle.transform.position = VRRig.LocalRig.transform.Find("rig/body_pivot").position - new Vector3(0f, 1.15f, 0f);
            Kyle.transform.rotation = VRRig.LocalRig.transform.Find("rig/body_pivot").rotation;

            Kyle.transform.Find("KyleRobot/RobotKile").gameObject.GetComponent<Renderer>().renderingLayerMask = 0;

            Animator KyleRobot = Kyle.transform.Find("KyleRobot").GetComponent<Animator>();
            KyleRobot.enabled = true;

            AnimationClip Animation = null;
            foreach (AnimationClip Clip in KyleRobot.runtimeAnimatorController.animationClips)
            {
                if (Clip.name == emoteName)
                {
                    Animation = Clip;
                    break;
                }
            }

            Animation.wrapMode = looping ? WrapMode.Loop : WrapMode.Default;
            KyleRobot.Play(Animation.name);

            AudioClip Sound = LoadSoundFromResource(emoteSound);
            Play2DAudio(Sound, 0.5f, looping);

            if (GorillaTagger.Instance.myRecorder != null)
            {
                GorillaTagger.Instance.myRecorder.SourceType = Recorder.InputSourceType.AudioClip;
                GorillaTagger.Instance.myRecorder.AudioClip = Sound;
                GorillaTagger.Instance.myRecorder.RestartRecording(true);
            }

            emoteTime = Time.time + (animationTime > 0f ? animationTime : Animation.length) + (looping ? 999999999999999f : 0);
        }

        public static Vector3 World2Player(Vector3 world) => world - GorillaTagger.Instance.bodyCollider.transform.position + GorillaTagger.Instance.transform.position;
        public static bool HasClosedMenuYet;

        public static void Update()
        {
            if (GorillaLocomotion.GTPlayer.Instance == null)
                return;

            if (!HasClosedMenuYet && !Main.MenuButton) HasClosedMenuYet = true;

            if (Main.MenuButton && HasClosedMenuYet)
            {
                emoteTime = 0f;
                return;
            }

            if (Time.time < emoteTime)
            {
                if (Kyle != null)
                {
                    VRRig.LocalRig.enabled = false;

                    GorillaTagger.Instance.transform.position = World2Player(Kyle.transform.position + (Kyle.transform.forward * 1.5f) + new Vector3(0f, 1.15f, 0f)) + new Vector3(0f, 0.5f, 0f);
                    GorillaTagger.Instance.leftHandTransform.position = GorillaTagger.Instance.bodyCollider.transform.position;
                    GorillaTagger.Instance.rightHandTransform.position = GorillaTagger.Instance.bodyCollider.transform.position;

                    GorillaTagger.Instance.rigidbody.linearVelocity = Vector3.zero;

                    VRRig.LocalRig.transform.position = Kyle.transform.Find("KyleRobot/ROOT/Hips/Spine1/Spine2").transform.position - (Kyle.transform.Find("KyleRobot/ROOT/Hips/Spine1/Spine2").transform.right / 2.5f);
                    VRRig.LocalRig.transform.rotation = Quaternion.Euler(new Vector3(0f, Kyle.transform.Find("KyleRobot/ROOT/Hips/Spine1/Spine2").transform.rotation.eulerAngles.y, 0f));

                    VRRig.LocalRig.leftHand.rigTarget.transform.position = Kyle.transform.Find("KyleRobot/ROOT/Hips/Spine1/Spine2/LeftShoulder/LeftUpperArm/LeftArm/LeftHand").transform.position;
                    VRRig.LocalRig.rightHand.rigTarget.transform.position = Kyle.transform.Find("KyleRobot/ROOT/Hips/Spine1/Spine2/RightShoulder/RightUpperArm/RightArm/RightHand").transform.position;

                    VRRig.LocalRig.leftHand.rigTarget.transform.rotation = Kyle.transform.Find("KyleRobot/ROOT/Hips/Spine1/Spine2/LeftShoulder/LeftUpperArm/LeftArm/LeftHand").transform.rotation * Quaternion.Euler(0, 0, 75);
                    VRRig.LocalRig.rightHand.rigTarget.transform.rotation = Kyle.transform.Find("KyleRobot/ROOT/Hips/Spine1/Spine2/RightShoulder/RightUpperArm/RightArm/RightHand").transform.rotation * Quaternion.Euler(180, 0, -75);

                    VRRig.LocalRig.head.rigTarget.transform.rotation = Kyle.transform.Find("KyleRobot/ROOT/Hips/Spine1/Spine2/Neck/Head").transform.rotation * Quaternion.Euler(0f, 0f, 90f);
                }
            }
            else
            {
                if (Kyle != null)
                {
                    VRRig.LocalRig.enabled = true;
                    EnableCosmetics();

                    GameObject.Destroy(Kyle);

                    if (GorillaTagger.Instance.myRecorder != null)
                    {
                        GorillaTagger.Instance.myRecorder.SourceType = Recorder.InputSourceType.Microphone;
                        GorillaTagger.Instance.myRecorder.AudioClip = null;
                        GorillaTagger.Instance.myRecorder.RestartRecording(true);
                    }

                    GorillaTagger.Instance.transform.position = archivePosition;
                    GorillaLocomotion.GTPlayer.Instance.GetControllerTransform(false).parent.rotation *= Quaternion.Euler(0f, 180f, 0f);
                }

                StopAudio();
            }
        }

        public static void StopAudio()
        {
            if (audiomgr != null)
            {
                AudioSource source = audiomgr.GetComponent<AudioSource>();

                if (source != null)
                {
                    source.Stop();
                    source.clip = null;
                    source.loop = false;
                }
            }
        }
        #endregion
    }
}
