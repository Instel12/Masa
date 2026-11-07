using System.Collections;
using GorillaNetworking;
using Masa.Classes;
using Masa.Menu;
using UnityEngine;

namespace Masa.Mods
{
    class Room
    {
        public static void Disconnect()
        {
            NetworkSystem.Instance.ReturnToSinglePlayer();
        }

        public static void Reconnect()
        {
            Disconnect();
            PhotonNetworkController.Instance.AttemptToJoinSpecificRoom(Main.LastRoom, JoinType.Solo);
        }

        public static void JoinRandom()
        {
            IEnumerator wait()
            {
                yield return new WaitForSeconds(3);
                JoinRandom();
            }

            if (NetworkSystem.Instance.InRoom)
            {
                Disconnect();
                CoroutineRunner.Coroutine(wait());
            }
            else
            {
                PhotonNetworkController.Instance.AttemptToJoinPublicRoom(PhotonNetworkController.Instance.currentJoinTrigger);
            }
        }
    }
}
