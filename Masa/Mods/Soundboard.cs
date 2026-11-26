using System;
using System.Collections;
using System.Diagnostics;
using System.IO;
using System.Linq;
using BepInEx;
using Masa.Classes;
using Masa.Menu;
using Photon.Voice.Unity;
using UnityEngine;

namespace Masa.Mods
{
    class Soundboard
    {
        public static void Refresh()
        {
            Buttons.Buttans.RemoveAll(button => button.Category == Buttons.Categories.Soundboard);

            Buttons.Buttans.Add(new Button
            {
                Category = Buttons.Categories.Soundboard,
                Title = "Exit",
                OneShot = true,
                Method = () => Buttons.LoadCategory(Buttons.Categories.Main)
            });

            Buttons.Buttans.Add(new Button
            {
                Category = Buttons.Categories.Soundboard,
                Title = "Open Sounds Folder",
                OneShot = true,
                Method = () => Process.Start("explorer.exe", Path.Combine(Paths.GameRootPath, PluginInfo.Title, "Soundboard"))
            });

            Buttons.Buttans.Add(new Button
            {
                Category = Buttons.Categories.Soundboard,
                Title = "Refresh Sounds",
                OneShot = true,
                Method = () => 
                {
                    Refresh();
                    Buttons.LoadCategory(Buttons.Categories.Soundboard);
                }
            });

            Buttons.Buttans.Add(new Button
            {
                Category = Buttons.Categories.Soundboard,
                Title = "Stop Sound",
                OneShot = true,
                Method = () => StopSound()
            });

            string directory = Path.Combine(Paths.GameRootPath, PluginInfo.Title, "Soundboard");

            if (Directory.Exists(directory))
            {
                foreach (string filePath in Directory.GetFiles(directory)
                        .Where(f => f.EndsWith(".wav", StringComparison.OrdinalIgnoreCase) || f.EndsWith(".mp3", StringComparison.OrdinalIgnoreCase) || f.EndsWith(".ogg", StringComparison.OrdinalIgnoreCase)))
                {
                    string fileName = Path.GetFileName(filePath);
                    string capturedName = fileName;

                    Buttons.Buttans.Add(new Button
                    {
                        Category = Buttons.Categories.Soundboard,
                        Title = capturedName,
                        OneShot = true,
                        Method = () => PlaySound(capturedName)
                    });
                }
            }
        }

        public static AudioSource LocalSource;
        public static void PlaySound(string name)
        {
            if (LocalSource != null)
            {
                LocalSource.Stop();
                GameObject.Destroy(LocalSource.gameObject);
            }

            CoroutineRunner.Coroutine(AssetTools.LoadAudioLocal(name, (AudioClip clip) =>
            {
                LocalSource = Main.PlayOneShot2D(clip);
                if (GorillaTagger.Instance.myRecorder != null)
                {
                    GorillaTagger.Instance.myRecorder.SourceType = Recorder.InputSourceType.AudioClip;
                    GorillaTagger.Instance.myRecorder.AudioClip = clip;
                    GorillaTagger.Instance.myRecorder.RestartRecording(true);

                    CoroutineRunner.Coroutine(NoLoop(clip.length));
                }
            }));

            IEnumerator NoLoop(float duration)
            {
                yield return new WaitForSeconds(duration);
                StopSound();
            }
        }

        public static void StopSound()
        {
            LocalSource.Stop();
            GameObject.Destroy(LocalSource.gameObject);
            if (GorillaTagger.Instance.myRecorder != null)
            {
                GorillaTagger.Instance.myRecorder.SourceType = Recorder.InputSourceType.Microphone;
                GorillaTagger.Instance.myRecorder.AudioClip = null;
                GorillaTagger.Instance.myRecorder.RestartRecording(true);
            }
        }
    }
}
