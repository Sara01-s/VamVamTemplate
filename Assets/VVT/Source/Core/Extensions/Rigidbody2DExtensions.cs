using static Unity.Mathematics.math;
using UnityEngine;

namespace Game {

	public static class Rigidbody2DExtensions {

		 public static void LimitYVelocity(this Rigidbody2D rigidbody2D, float limit) {

			float yVelocity = rigidbody2D.velocity.y;

			if (abs(yVelocity) > limit) {
				yVelocity = sign(yVelocity) * limit;
			}

			rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, yVelocity);
        }

		public static void LimitXVelocity(this Rigidbody2D rigidbody2D, float limit) {

			float xVelocity = rigidbody2D.velocity.x;

			if (abs(xVelocity) > limit) {
				xVelocity = sign(xVelocity) * limit;
			}

			rigidbody2D.velocity = new Vector2(xVelocity, rigidbody2D.velocity.y);
        }

	}
}