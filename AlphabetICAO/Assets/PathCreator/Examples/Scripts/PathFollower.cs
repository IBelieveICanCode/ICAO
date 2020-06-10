﻿using UnityEngine;

namespace PathCreation.Follower
{
    // Moves along a path at constant speed.
    // Depending on the end of path instruction, will either loop, reverse, or stop at the end of the path.
    public class PathFollower : MonoBehaviour
    {
        Rigidbody2D rb;
        public PathCreator pathCreator;
        public EndOfPathInstruction endOfPathInstruction;
        float distanceTravelled;

        void Start() {
            if (pathCreator != null)
            {
                // Subscribed to the pathUpdated event so that we're notified if the path changes during the game
                pathCreator.pathUpdated += OnPathChanged;
            }
            rb = GetComponent<Rigidbody2D>();
        }

        void Update()
        {
        }

        public void FollowPath(float speed)
        {
            if (pathCreator != null && rb!= null)
            {
                distanceTravelled += speed * Time.deltaTime;
                rb.MovePosition(pathCreator.path.GetPointAtDistance(distanceTravelled, endOfPathInstruction));
                rb.SetRotation(pathCreator.path.GetRotationAtDistance(distanceTravelled, endOfPathInstruction));                   
                //transform.rotation = pathCreator.path.GetRotationAtDistance(distanceTravelled, endOfPathInstruction);
            }
        }
        // If the path changes during the game, update the distance travelled so that the follower's position on the new path
        // is as close as possible to its position on the old path
        void OnPathChanged() {
            distanceTravelled = pathCreator.path.GetClosestDistanceAlongPath(transform.position);
        }

        private void OnDestroy()
        {
            pathCreator.pathUpdated -= OnPathChanged;
        }
    }
}