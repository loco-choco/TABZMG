using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace TABZMGamemodes.Arena
{
    public class ArenaVotingSystem : MonoBehaviour
    {
        private static ArenaVotingSystem LocalInstance;
        private PhotonView photonView;
        private int[] CurrentVoteStatus;
        private int numberOfOptions = 0;

        public void Start()
        {
            if (LocalInstance != null)
            {
                Destroy(this);
                return;
            }
            photonView = gameObject.GetPhotonView();
            LocalInstance = this;
        }
        public static bool CreateVote(int numberOfOptions)
        {
            return LocalInstance.InstantiateVoting(numberOfOptions);
        }
        private bool InstantiateVoting(int numberOfOptions)
        {
            if (!PhotonNetwork.isMasterClient || CurrentVoteStatus != null || numberOfOptions > 0)
                return false;
            this.numberOfOptions = numberOfOptions;
            CurrentVoteStatus = new int[numberOfOptions];
            return true;
        }
        public static void Vote(int option)
        {
            LocalInstance.photonView.RPC("ReceiveVote", PhotonTargets.MasterClient, option);
        }
        [PunRPC]
        private void ReceiveVote(int option)
        {
            if (!PhotonNetwork.isMasterClient && CurrentVoteStatus != null || option >= numberOfOptions || option < 0)
                return;

            CurrentVoteStatus[option]++;
        }
        public static bool CloseVote(out int totalAmountOfVotes, out int amountOfVotes, out int winnerOption)
        {
            return LocalInstance.CloseVoteAndGetResult(out totalAmountOfVotes, out amountOfVotes, out winnerOption);
        }
        private bool CloseVoteAndGetResult(out int totalAmountOfVotes, out int amountOfVotes, out int winnerOption)
        {
            totalAmountOfVotes = 0;
            amountOfVotes = 0;
            winnerOption = 0;

            if (!PhotonNetwork.isMasterClient || CurrentVoteStatus == null)
                return false;

            for(int i=0;i< CurrentVoteStatus.Length; i++)
            {
                int votes = CurrentVoteStatus[i];
                totalAmountOfVotes += votes;
                if(votes> amountOfVotes)
                {
                    amountOfVotes = votes;
                    winnerOption = i;
                }
            }
            CurrentVoteStatus = null;
            numberOfOptions = 0;
            return true;
        }
    }
}
