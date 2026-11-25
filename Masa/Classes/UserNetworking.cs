using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;

namespace Masa.Classes
{
    class UserNetworking
    {
        public static void RaiseUserEvent(string command, object[] attributes)
        {
            object[] data = new object[attributes.Length + 1];
            data[0] = command;

            for (int i = 0; i < attributes.Length; i++)
            {
                data[i + 1] = attributes[i];
            }

            PhotonNetwork.RaiseEvent(
                PluginInfo.UserChannel,
                data,
                new RaiseEventOptions
                {
                    Receivers = ReceiverGroup.All
                },
                SendOptions.SendReliable
            );
        }
    }
}
