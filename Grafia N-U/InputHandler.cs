using OpenTK.Windowing.GraphicsLibraryFramework;
using OpenTK.Mathematics;

public class InputHandler
{
    public Vector3 HandleMovement(KeyboardState keyboardState, float speed, float deltaTime, Vector3 position)
    {
        Vector3 direction = Vector3.Zero;

        if (keyboardState.IsKeyDown(Keys.W)) direction.Z -= 1;
        if (keyboardState.IsKeyDown(Keys.S)) direction.Z += 1;
        if (keyboardState.IsKeyDown(Keys.A)) direction.X -= 1;
        if (keyboardState.IsKeyDown(Keys.D)) direction.X += 1;
        if (keyboardState.IsKeyDown(Keys.Q)) direction.Y -= 1;
        if (keyboardState.IsKeyDown(Keys.E)) direction.Y += 1;

        if (direction.LengthSquared > 0)
        {
            direction.Normalize();
            position += direction * speed * deltaTime;
        }

        return position;
    }

    public Vector2 HandleMouseMovement(MouseState mouseState, Vector2 rotation, float sensitivity)
    {
        rotation.X -= mouseState.Delta.X * sensitivity;
        rotation.Y -= mouseState.Delta.Y * sensitivity;
        rotation.Y = MathHelper.Clamp(rotation.Y, -89f, 89f); // Limitar inclinación vertical

        return rotation;
    }
}
