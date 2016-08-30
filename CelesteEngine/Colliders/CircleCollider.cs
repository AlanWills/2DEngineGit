using System;
using Microsoft.Xna.Framework;

namespace CelesteEngine
{
    public class CircleCollider : Collider
    {
        #region Properties and Fields

        /// <summary>
        /// The radius of the Circle - corresponds to the X component of the Size property
        /// </summary>
        public float Radius { get { return Size.X; } }

        #endregion

        public CircleCollider(BaseObject parent) :
            base(parent)
        {

        }

        #region Collider Intersection Functions

        protected override bool CheckCollisionWith(RectangleCollider rectangleCollider)
        {
            throw new NotImplementedException();
        }

        protected override bool CheckCollisionWith(CircleCollider circleCollider)
        {
            return (circleCollider.Parent.WorldPosition - Parent.WorldPosition).LengthSquared() <= (circleCollider.Radius * circleCollider.Radius + Radius * Radius);
        }

        public override bool CheckIntersects(Rectangle rectangle)
        {
            throw new NotImplementedException();
        }

        public override bool CheckIntersects(Vector2 point)
        {
            return (point - Parent.WorldPosition).LengthSquared() <= Radius * Radius;
        }

        public override void Update()
        {
            Vector2 empty = new Vector2();
            Parent.UpdateCollider(ref empty, ref size);

            // Want to set the Radius so halve the total size
            size *= 0.5f;
        }

        #endregion
    }
}
