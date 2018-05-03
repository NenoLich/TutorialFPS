using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace TutorialFPS.Controllers
{
    public class NavMeshController : BaseController
    {
        private NavMeshAgent _agent;
        private Animator _animator;
        private Rigidbody _rigidbody;
        private float _maxRayDistance = 50f;

        private Queue<Vector3> _destinationPoints;

        private void Awake()
        {
            _destinationPoints = new Queue<Vector3>();
            _agent = GameObject.Find("Fred").GetComponent<NavMeshAgent>();
            _animator = _agent.GetComponent<Animator>();
            _rigidbody = _agent.GetComponent<Rigidbody>();
        }

        public void SetDestinationPoint()
        {
            RaycastHit hit;

            if (Physics.Raycast(Camera.main.ViewportPointToRay(new Vector3(0.5F, 0.5F, 0F)),
                out hit, _maxRayDistance, LayerMask.GetMask("Ground")))
            {
                AddDestinationPoint(hit);
            }
        }

        private void AddDestinationPoint(RaycastHit hit)
        {
            _destinationPoints.Enqueue(hit.point);
            Move();
        }

        private void Move()
        {
            if (_agent.remainingDistance > _agent.stoppingDistance)
            {
                _animator.SetBool("Moving", true);
                return;
            }

            _animator.SetBool("Moving", false);
            if (_destinationPoints.Count > 0)
            {
                _agent.SetDestination(_destinationPoints.Dequeue());

            }
        }

        private void Update()
        {
            Move();
            
            if (_agent.isOnOffMeshLink&&_agent.currentOffMeshLinkData.activated)
            {
                _animator.SetTrigger("Jump");
                _agent.ActivateCurrentOffMeshLink(false);
            }
        }
    }
}
