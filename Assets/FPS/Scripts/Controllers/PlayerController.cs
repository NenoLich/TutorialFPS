using System.Collections;
using System.Collections.Generic;
using TutorialFPS.Models;
using TutorialFPS.Services;
using TutorialFPS.Views;
using UnityEngine;

namespace TutorialFPS.Controllers
{
    public class PlayerController : BaseController
    {
        private PlayerView _playerView;
        private PlayerModel _playerModel;

        public PlayerView PlayerView
        {
            get
            {
                if (_playerView==null)
                {
                    _playerView = FindObjectOfType<PlayerView>();
                }
                return _playerView;
            }
        }

        public PlayerModel PlayerModel
        {
            get
            {
                if (_playerModel == null)
                {
                    _playerModel = FindObjectOfType<PlayerModel>();
                }
                return _playerModel;
            }

            set { _playerModel = value; }
        }

        public override void OnNotification(Notification notification, Object target, params object[] data)
        {
            base.OnNotification(notification, target, data);

            switch (notification)
            {
                case Notification.UpdateHealth:

                    PlayerModel = (PlayerModel) target;
                    PlayerView.UpdateHealth(PlayerModel.Health%1f>0? (int)PlayerModel.Health+1 : (int)PlayerModel.Health);

                    if (PlayerModel.Health<=0)
                    {
                        PlayerModel.Death();
                    }

                    break;
            }
        }
    }
}

