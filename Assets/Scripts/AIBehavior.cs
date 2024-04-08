using System.Collections.Generic;
using UnityEngine;

namespace testTask
{
    public class AIBehavior : MonoBehaviour
    {
        public PatrolMode patrolMode = PatrolMode.Sequential;
        public List<Transform> patrolPoints;
        private int currentPatrolIndex = 0;
        public float moveSpeed = 2.0f;
        private float distanceThreshold = 0.1f;

        private void Start()
        {
            if (patrolPoints.Count == 0)
            {
                Debug.LogWarning("No patrol points assigned!");
                enabled = false; // Wyłącz skrypt, jeśli nie przypisano punktów patrolowych.
                return;
            }

            if (patrolMode == PatrolMode.Random)
            {
                ShufflePatrolPoints();
            }

            MoveToNextPatrolPoint();
        }

        private void Update()
        {
            Vector3 targetPosition = patrolPoints[currentPatrolIndex].position;
            targetPosition.y = transform.position.y; // Zachowaj tę samą pozycję Y.

            if (Vector3.Distance(transform.position, targetPosition) < distanceThreshold)
            {
                if (patrolMode == PatrolMode.Sequential)
                {
                    currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Count;
                }
                else if (patrolMode == PatrolMode.Random)
                {
                    currentPatrolIndex = Random.Range(0, patrolPoints.Count);
                }

                MoveToNextPatrolPoint();
            }

            // Przesuń sztuczną inteligencję w kierunku bieżącego punktu patrolowego
            Vector3 moveDirection = (targetPosition - transform.position).normalized;
            transform.Translate(moveDirection * moveSpeed * Time.deltaTime);
        }

        private void MoveToNextPatrolPoint()
        {
            Vector3 targetPosition = patrolPoints[currentPatrolIndex].position;
            targetPosition.y = transform.position.y; // Zachowaj tę samą pozycję Y.
        }

        private void ShufflePatrolPoints()
        {
            for (int i = 0; i < patrolPoints.Count; i++)
            {
                int randomIndex = Random.Range(i, patrolPoints.Count);
                Transform temp = patrolPoints[i];
                patrolPoints[i] = patrolPoints[randomIndex];
                patrolPoints[randomIndex] = temp;
            }
        }
    }
}