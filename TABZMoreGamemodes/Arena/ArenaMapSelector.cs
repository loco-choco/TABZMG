using System.Collections;
using UnityEngine;

namespace TABZMGamemodes.Arena
{
    public class ArenaMapSelector : MonoBehaviour
    {
        public static readonly ArenaMap[] maps = new ArenaMap[]
        {
            new ArenaMap()
            {
                KitSet = ArenaGamemodeData.KitSetOne,
                SpawnPointSet = ArenaGamemodeData.SpawnPointSetOne,
                ArenaPosition = ArenaGamemodeData.ArenaPositionOne,
                ArenaScale = ArenaGamemodeData.ArenaScaleOne
            },
            new ArenaMap()
            {
                KitSet = ArenaGamemodeData.KitSetOne,
                SpawnPointSet = ArenaGamemodeData.SpawnPointSetTwo,
                ArenaPosition = ArenaGamemodeData.ArenaPositionTwo,
                ArenaScale = ArenaGamemodeData.ArenaScaleTwo
            },
        };
        public int CurrentMap { get; private set; } = 0;
        public const int MapToLoadFirst = 1;

        public ArenaGamemode arenaGamemode;
        private PhotonView photonView;

        public void Start()
        {
            photonView = gameObject.GetPhotonView();
            if (!PhotonNetwork.isMasterClient)
                photonView.RPC("RequestMap", PhotonTargets.MasterClient, PhotonNetwork.player.ID);
            else
                ChangeMapTo(MapToLoadFirst);
        }
        public void ChangeMapTo(int mapIndex)
        {
            if (PhotonNetwork.isMasterClient)
                photonView.RPC("ChangeMap", PhotonTargets.All, mapIndex);
        }
        [PunRPC]
        private void RequestMap(int photonPlayerId)
        {
            if (PhotonNetwork.isMasterClient)
                gameObject.GetPhotonView().RPC("ChangeMap", PhotonPlayer.Find(photonPlayerId), CurrentMap);
        }
        [PunRPC]
        private void ChangeMap(int mapIndex)
        {
            if (mapIndex > maps.Length || mapIndex < 0)
                return;

            CurrentMap = mapIndex;
            arenaGamemode.SetNewMap(maps[mapIndex]);
        }
    }
    public class ArenaMap
    {
        public Vector3[] SpawnPointSet;
        public Vector3 ArenaScale;
        public Vector3 ArenaPosition;
        public ArenaKit[] KitSet;
    }
}
