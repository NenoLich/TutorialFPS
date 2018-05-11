using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TutorialFPS.Services;
using UnityEngine;
using UnityEngine.UI;

namespace TutorialFPS.Views
{
    public class RadarView : MonoBehaviour
    {
        private Transform _playerPos; // Позиция главного героя
        private readonly float mapScale = 2;
        public Image scanningArea;
        public float scanningSpeed=180;
        public static List<RadarObject> RadObjects = new List<RadarObject>();

        private void Start()
        {
            _playerPos = GameObject.FindGameObjectWithTag("Player").transform;
        }
        public static void RegisterRadarObject(GameObject o, Image i)
        {
            if (RadObjects.Count>0&& RadObjects.First(x => x.Owner == o) != null)
            {
                return;
            }

            Image image = Instantiate(i);
            RadObjects.Add(new RadarObject { Owner = o, Icon = image });
        }
        public static void RemoveRadarObject(GameObject o)
        {
            List<RadarObject> newList = new List<RadarObject>();
            foreach (RadarObject t in RadObjects)
            {
                if (t.Owner == o)
                {
                    Destroy(t.Icon);
                    continue;
                }
                newList.Add(t);
            }
            RadObjects.RemoveRange(0, RadObjects.Count);
            RadObjects.AddRange(newList);
        }
        private void DrawRadarDots()
        {
            foreach (RadarObject radObject in RadObjects)
            {
                Vector3 radarPos = (radObject.Owner.transform.position -
                                    _playerPos.position);
                float distToObject = Vector3.Distance(_playerPos.position,
                                         radObject.Owner.transform.position) * mapScale;
                float deltay = Mathf.Atan2(radarPos.x, radarPos.z) * Mathf.Rad2Deg -
                               270 - _playerPos.eulerAngles.y;
                radarPos.x = distToObject * Mathf.Cos(deltay * Mathf.Deg2Rad) * -1;
                radarPos.z = distToObject * Mathf.Sin(deltay * Mathf.Deg2Rad);
                radObject.Icon.transform.SetParent(transform);
                radObject.Icon.transform.position = new Vector3(radarPos.x,
                                                        radarPos.z, 0) + transform.position;
            }
        }
        private void Update()
        {
            if (Time.frameCount % 3 == 0)
            {
                DrawRadarDots();
            }

            scanningArea.rectTransform.Rotate(Vector3.forward,-scanningSpeed *Time.deltaTime);
        }
    }
}

