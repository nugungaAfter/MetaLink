using System.Collections.Generic;
using UnityEngine;
using BackEnd.Tcp;
using BackEnd;
using LitJson;
using UI;

namespace Multiplay
{
    public class MultiplayMatchMakingManager : MonoBehaviour
    {
        [SerializeField] private AlterUI alterUI;
        [SerializeField] private MutiplayMatchMakingTextSetting textSetting;

        private bool isMatchMaking;

        public void Update()
        {
            Backend.Match.Poll();
        }

        public void StartMatch()
        {
            if (isMatchMaking)
                return;

            Backend.Match.OnJoinMatchMakingServer = OnJoinMatchMakingServer;
            Backend.Match.OnLeaveMatchMakingServer = OnLeaveMatchMakingServer;

            bool matchSoket = Backend.Match.JoinMatchMakingServer(out ErrorInfo errorInfo);

            if (matchSoket)
                isMatchMaking = true;
            else {
                alterUI.Enable(errorInfo.Reason);
            }
        }

        public void RequestMatchMaking()
        {
            if (!isMatchMaking)
                return;

            var callback = Backend.Match.GetMatchList();

            if (!callback.IsSuccess()) {
                alterUI.Enable(textSetting.getMatchListFailed);
                return;
            }

            var matchCardList = new List<MultiplayMatchCard>();
            var matchCardListJson = callback.FlattenRows();

            foreach (JsonData matchCardJson in matchCardListJson) {
                var matchCard = new MultiplayMatchCard(matchCardJson);
                matchCardList.Add(matchCard);
                Debug.Log(matchCard.ToString());
            }

            //Backend.Match.RequestMatchMaking(MatchType.Point, MatchModeType.OneOnOne, );
        }


        public void CreateMatchRoom()
        {
            Backend.Match.OnMatchMakingRoomCreate = OnMatchMakingRoomCreate;
            Backend.Match.CreateMatchRoom();
        }

        public void OnJoinMatchMakingServer(JoinChannelEventArgs args)
        {
            if (args.ErrInfo.Category == ErrorCode.Exception) {
                alterUI.Enable(textSetting.matchServerConnectionFailed);
                return;
            }
        }

        public void OnLeaveMatchMakingServer(LeaveChannelEventArgs args)
        {
            if (args.ErrInfo.Category == ErrorCode.Success)
                return;

            if (args.ErrInfo.Detail == ErrorCode.DisconnectFromRemote) {
                alterUI.Enable(textSetting.matchServerConnectionFailed);
                return;
            }

            if (args.ErrInfo.Category == ErrorCode.DisconnectFromRemote && args.ErrInfo.Detail == ErrorCode.Exception) {
                alterUI.Enable(textSetting.noneMatchType);
                return;
            }

            if (args.ErrInfo.Category == ErrorCode.NetworkTimeout) {
                alterUI.Enable(textSetting.connectionTimeout);
                return;
            }
        }

        public void OnMatchMakingRoomCreate(MatchMakingInteractionEventArgs args)
        {

        }
    }
}