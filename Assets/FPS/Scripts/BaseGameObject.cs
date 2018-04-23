using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TutorialFPS
{
    public abstract class BaseGameObject : MonoBehaviour
    {
        #region Fields
        protected int _layer;
        protected Color _color;
        protected Renderer _renderer;
        protected Vector3 _position;
        protected Quaternion _rotation;
        protected Vector3 _scale;
        protected GameObject _instanceObject;
        protected Rigidbody _rigidbody;
        protected string _name;
        protected bool _isVisible;
        #endregion

        #region Property
        /// <summary>
        /// Имя объекта
        /// </summary>
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                InstanceObject.name = _name;
            }
        }
        /// <summary>
        /// Слой объекта
        /// </summary>
        public int Layers
        {
            get { return _layer; }
            set
            {
                _layer = value;
                if (_instanceObject == null) return;

                SetLayer(transform, value);
            }
        }
        /// <summary>
        /// Цвет материала объекта
        /// </summary>
        public Color Color
        {
            get { return _color; }
            set
            {
                _color = value;
                if (_renderer == null) return;

                SetColor(transform, _color);
            }
        }
        public Renderer Renderer
        {
            get
            {
                if (_renderer != null)
                    return _renderer;

                return _renderer = GetComponent<Renderer>();
            }
        }
        /// <summary>
        /// Позиция объекта
        /// </summary>
        public Vector3 Position
        {
            get
            {
                if (InstanceObject != null)
                {
                    _position = transform.position;
                }
                return _position;
            }
            set
            {
                _position = value;
                if (InstanceObject != null)
                {
                    transform.position = _position;
                }
            }
        }
        /// <summary>
        /// Размер объекта
        /// </summary>
        public Vector3 Scale
        {
            get
            {
                if (InstanceObject != null)
                {
                    _scale = transform.localScale;
                }
                return _scale;
            }
            set
            {
                _scale = value;
                if (InstanceObject != null)
                {
                    transform.localScale = _scale;
                }
            }
        }
        /// <summary>
        /// Поворот объекта
        /// </summary>
        public Quaternion Rotation
        {
            get
            {
                if (InstanceObject != null)
                {
                    _rotation = transform.rotation;
                }
                return _rotation;
            }
            set
            {
                _rotation = value;
                if (InstanceObject != null)
                {
                    transform.rotation = _rotation;
                }
            }
        }
        /// <summary>
        /// Получить физическое свойство объекта
        /// </summary>
        public Rigidbody Rigidbody
        {
            get
            {
                if (_rigidbody != null)
                    return _rigidbody;

                return _rigidbody = GetComponent<Rigidbody>();
            }
        }
        /// <summary>
        /// Ссылка на gameObject
        /// </summary>
        public GameObject InstanceObject
        {
            get { return _instanceObject; }
        }

        /// <summary>
        /// Скрывает/показывает объект
        /// </summary>
        public bool IsVisible
        {
            get { return _isVisible; }
            set
            {
                _isVisible = value;

                if (_renderer!=null)
                    _renderer.enabled = _isVisible;
            }
        }

        #endregion

        #region UnityFunction
        protected virtual void Awake()
        {
            _instanceObject = gameObject;
            _name = _instanceObject.name;
        }

        #endregion

        #region PrivateFunctions

        /// <summary>
        /// Выставляет слой себе и всем вложенным объектам независимо от уровня вложенности
        /// </summary>
        /// <param name="obj">Объект</param>
        /// <param name="lvl">Слой</param>
        private void SetLayer(Transform obj, int lvl)
        {
            obj.gameObject.layer = lvl;
            if (obj.childCount <= 0) return;
            foreach (Transform d in obj)
            {
                SetLayer(d, lvl);
            }
        }
        private void SetColor(Transform obj, Color color)
        {
            Renderer rendererComponent = obj.GetComponent<Renderer>();
            if (rendererComponent != null) rendererComponent.material.color = color;

            if (obj.childCount <= 0) return;
            foreach (Transform d in obj)
            {
                SetColor(d, color);
            }
        }
        #endregion

    }
}
