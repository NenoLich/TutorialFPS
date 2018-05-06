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

        public override void OnNotification(Notification notification, Object target, params object[] data)
        {
            base.OnNotification(notification, target, data);

            switch (notification)
            {
                case Notification.UpdateHealth:
                    
                    PlayerModel player = (PlayerModel) target;
                    PlayerView.UpdateHealth((int)player.Health);

                    if (player.Health<=0)
                    {
                        player.Death();
                    }

                    break;
            }
        }
    }
}

