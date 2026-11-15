using TMPro;
using UnityEngine;
using UnityEngine.XR;
using Masa.Menu;
using Masa.Mods;
using UnityEngine.InputSystem;
using System.Linq;

namespace Masa.Classes
{
    class ButtonDriver : MonoBehaviour
    {
        public int Index;
        public TextMeshPro Text;
        public Renderer Renderer;
        public Renderer Star;
        public Main.ButtonFunction Function;

        private Button ThisButton;

        void OnTriggerEnter(Collider other)
        {
            if (other.gameObject == Main.Pointer)
            {
                Press();
            }
        }

        void Press()
        {
            if (Main.Delay < Time.time)
            {
                Main.Delay = Time.time + 0.2f;

                if (Function == Main.ButtonFunction.Mod)
                {
                    if (ControllerInputPoller.instance.leftGrab || Keyboard.current.eKey.isPressed)
                    {
                        ThisButton.Favorite = !ThisButton.Favorite;
                        Main.SaveFavorites();
                    }
                    else
                    {
                        if (ThisButton.ShuffleValues != null)
                        {
                            ThisButton.ShuffleValue++;
                            if (ThisButton.ShuffleValue >= ThisButton.ShuffleValues.Count) ThisButton.ShuffleValue = 0;

                            ThisButton.Note = ThisButton.ShuffleValues.Keys.ElementAt(ThisButton.ShuffleValue);
                            ThisButton.ShuffleAction?.Invoke(ThisButton.ShuffleValues.Values.ElementAt(ThisButton.ShuffleValue).Invoke());
                        }
                        else
                        {
                            if (!ThisButton.OneShot)
                            {
                                ThisButton.Enabled = !ThisButton.Enabled;
                                if (ThisButton.Enabled)
                                {
                                    Notifications.SendNotification($"[<color=green>Enable</color>] {ThisButton.Title}");
                                    ThisButton.EnableMethod?.Invoke();
                                }
                                else
                                {
                                    Notifications.SendNotification($"[<color=red>Disable</color>] {ThisButton.Title}");
                                    ThisButton.DisableMethod?.Invoke();
                                }
                            }
                            else
                            {
                                Notifications.SendNotification($"[<color=blue>One Shot</color>] {ThisButton.Title}");
                                ThisButton.Method?.Invoke();
                                ThisButton.EnableMethod?.Invoke();
                            }
                        }

                        Main.SavePrefs(false);
                    }
                }

                if (Function == Main.ButtonFunction.Disconnect)
                {
                    Room.Disconnect();
                }

                if (Function == Main.ButtonFunction.NextPage)
                {
                    Buttons.NextPage();
                }

                if (Function == Main.ButtonFunction.PreviousPage)
                {
                    Buttons.PreviousPage();
                }

                VRRig.LocalRig.PlayHandTapLocal(66, !Main.LeftHandMenu, 50);
                GorillaTagger.Instance.DoVibration(Main.LeftHandMenu ? XRNode.RightHand : XRNode.LeftHand, 1f, 0.1f);
            }
        }

        void Update()
        {
            if (Function == Main.ButtonFunction.Mod)
            {
                int buttonIndex = (Buttons.Page * 9) + Index;

                if (buttonIndex >= Main.ThisCategory.Count)
                {
                    Star.enabled = false;
                    Renderer.enabled = false;
                    Text.text = "";
                    ThisButton = null;
                    return;
                }

                ThisButton = Buttons.Buttans[Main.ThisCategory[buttonIndex]];
                if (ThisButton.ShuffleValues != null && ThisButton.Note == null) ThisButton.Note = ThisButton.ShuffleValues.Keys.ElementAt(ThisButton.ShuffleValue);

                if (ThisButton == null)
                {
                    Renderer.enabled = false;
                    Text.text = "";
                    Star.enabled = false;
                    return;
                }

                Renderer.enabled = true;
                Star.enabled = ThisButton.Favorite;

                Text.text = ThisButton.Title;
                if (!ThisButton.Note.IsNullOrEmpty()) Text.text += $" [<color=grey>{ThisButton.Note}</color>]";
                Renderer.material.color = ThisButton.Enabled ? Settings.CurrentTheme.Enabled : Settings.CurrentTheme.Disabled;
            }

            if (Mouse.current.leftButton.wasPressedThisFrame)
            {
                Ray ray = Camera.allCameras[1].ScreenPointToRay(Mouse.current.position.ReadValue());
                int layerMask = 1 << 2;
                if (Physics.Raycast(ray, out RaycastHit hit, 1000f, layerMask))
                {
                    if (hit.collider.gameObject == gameObject)
                    {
                        Press();
                    }
                }
            }
        }
    }
}
