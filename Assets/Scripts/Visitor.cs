using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;


public class Visitor: MonoBehaviour
{
        private Spot _mySpot;
        private Rigidbody2D _rigidbody2D;
        private const float Speed = 3f;
        private Coroutine _moveCoroutine;

        private void Start()
        {
                _rigidbody2D = GetComponent<Rigidbody2D>();
        }

        public bool CanGoToSpot(Spot spot)
        {
                if(!CheckChance()) return false;
                MoveToSpot(spot);
                return true;
        }
        
        private void MoveToSpot(Spot spot)
        {
                if (_mySpot != null)
                        _mySpot.IsFree = true;
                _mySpot = spot; 
                if(_moveCoroutine != null)
                        StopCoroutine(_moveCoroutine);
                _moveCoroutine = StartCoroutine(MoveCoroutine());
        }

        private IEnumerator MoveCoroutine()
        {
                var desiredVelocity  = _mySpot.transform.position - transform.position;
                _rigidbody2D.velocity = desiredVelocity.normalized * Speed;
                yield return new WaitUntil(() => CheckPosition(transform.position, _mySpot.transform.position));
                _rigidbody2D.velocity = Vector2.zero;
        }

        private bool CheckPosition(Vector3 a, Vector3 b)
        {
                return Mathf.Abs(a.x - b.x) <= 0.2f && Math.Abs(a.y - b.y) <= 0.2f;
        }

        private bool CheckChance()
        {
                var chance = Random.Range(1, 100);
                return chance >= 45;
        }
}