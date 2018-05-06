using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace TutorialFPS
{
    public abstract class BaseGameObject : MonoBehaviour
    {
        #region Fields
        protected int _layer=-1;
        protected Color _color=Color.clear;
        protected Renderer _renderer;
        protected Transform _transform;
        protected Vector3 _position;
        protected Quaternion _rotation;
        protected Vector3 _scale;
        protected GameObject _instanceObject;
        protected Rigidbody _rigidbody;
        protected Collider _collider;
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
            get
            {
                if (_layer == -1 && InstanceObject != null)
                    _layer = InstanceObject.layer;

                return _layer;
            }
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
            get
            {
                _color = Renderer.material.color;
                return _color;
            }
            set
            {
                _color = value;
                if (Renderer == null) return;

                SetColor(transform, _color);
            }
        }
        public Renderer Renderer
        {
            get
            {
                if ( _renderer == null && InstanceObject != null)
                    _renderer = GetComponent<MeshRenderer>()? GetComponent<MeshRenderer>():GetComponentInChildren<MeshRenderer>();

                return _renderer;
            }
        }

        public Transform Transform
        {
            get
            {
                if (_transform == null && InstanceObject != null)
                    _transform = GetComponent<Transform>();

                return _transform;
            }
        }
        /// <summary>
        /// Позиция объекта
        /// </summary>
        public Vector3 Position
        {
            get
            {
                _position = Transform.position;
                return _position;
            }
            set
            {
                _position = value;
                if (InstanceObject != null)
                {
                    Transform.position = _position;
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
                _scale = Transform.localScale;
                return _scale;
            }
            set
            {
                _scale = value;
                if (InstanceObject != null)
                {
                    Transform.localScale = _scale;
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
                _rotation = Transform.rotation;
                return _rotation;
            }
            set
            {
                _rotation = value;
                if (InstanceObject != null)
                {
                    Transform.rotation = _rotation;
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
                if (_rigidbody == null && InstanceObject != null)
                    _rigidbody = GetComponent<Rigidbody>();

                return _rigidbody;
            }
        }

        public Collider Collider
        {
            get
            {
                if (_collider == null && InstanceObject != null)
                    _collider = GetComponent<Collider>() ? GetComponent<Collider>() : GetComponentInChildren<Collider>();

                return _collider;
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
            get
            {
                if (!Renderer)
                    return false;

                return Renderer.enabled;
            }
            set
            {
                _isVisible = value;
                SetVisibility(transform, _isVisible);
            }
        }

        #endregion

        #region UnityFunction
        protected virtual void Awake()
        {
            _instanceObject = gameObject;
            _layer = InstanceObject.layer;
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

        protected virtual void SetVisibility(Transform objTransform, bool visible)
        {
            foreach (var r in GetComponentsInChildren<Collider>(true).Union(GetComponents<Collider>()))
                r.enabled = visible;
            foreach (var r in GetComponentsInChildren<MeshRenderer>(true).Union(GetComponents<MeshRenderer>()))
                r.enabled = visible;
        }

        #endregion

    }
}
