﻿using System;
using Microsoft.Xna.Framework;

namespace CelesteEngine
{
    public class RectangleCollider : Collider
    {
        /// <summary>
        /// The rectangle corresponds to the bounds of this collider.
        /// This is done in this way so that we can modify the Location of the Rectangle.
        /// </summary>
        private Rectangle bounds;
        public Rectangle Bounds
        {
            get { return bounds; }
        }

        public RectangleCollider(BaseObject parent) :
            base(parent)
        {
            // Sets up the bounds
            Vector2 parentWorldPos = parent.WorldPosition;
            Vector2 parentSize = parent.Size;
            bounds = new Rectangle(
                (int)(parentWorldPos.X - parentSize.X * 0.5f), 
                (int)(parentWorldPos.Y - parentSize.Y * 0.5f), 
                (int)parentSize.X, 
                (int)parentSize.Y);
        }

        #region Abstract Collision Functions

        protected override bool CheckCollisionWith(RectangleCollider rectangleCollider)
        {
            return rectangleCollider.Bounds.Intersects(bounds);
        }

        protected override bool CheckCollisionWith(CircleCollider circleCollider)
        {
            throw new NotImplementedException();
        }

        public override bool CheckIntersects(Rectangle rectangle)
        {
            return rectangle.Intersects(Bounds);
        }

        public override bool CheckIntersects(Vector2 point)
        {
            return bounds.Contains(point); ;
        }

        #endregion

        #region Collider Update Functions

        /// <summary>
        /// Update the position of the rectangle collider
        /// </summary>
        public override void Update()
        {
            Vector2 parentWorldPos = Vector2.Zero;

            Parent.UpdateCollider(ref parentWorldPos, ref size);

            // Update the bounds location (top left)
            bounds.Location = new Point(
                (int)(parentWorldPos.X - Size.X * 0.5f),
                (int)(parentWorldPos.Y - Size.Y * 0.5f));

            // Update the bounds size
            bounds.Size = Size.ToPoint();
        }

        #endregion
    }
}
