using OpenTK.Windowing.GraphicsLibraryFramework;
using OpenTK.Mathematics;

public class InputHandler
{
    public Vector3 HandleMovement(KeyboardState keyboardState, float speed, float deltaTime, Vector3 position)
    {
        if (keyboardState.IsKeyDown(Keys.W)) position.Z -= speed * deltaTime;
        if (keyboardState.IsKeyDown(Keys.S)) position.Z += speed * deltaTime;
        if (keyboardState.IsKeyDown(Keys.A)) position.X -= speed * deltaTime;
        if (keyboardState.IsKeyDown(Keys.D)) position.X += speed * deltaTime;
        if (keyboardState.IsKeyDown(Keys.Q)) position.Y -= speed * deltaTime;
        if (keyboardState.IsKeyDown(Keys.E)) position.Y += speed * deltaTime;

        return position;
    }
}