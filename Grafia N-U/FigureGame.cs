using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using System.Collections.Generic;

class FigureGame : GameWindow
{
    private Shader shader;
    private Figure figure;
    private float rotation = 0.0f;
    private InputHandler inputHandler;
    private Vector3 cubePosition = Vector3.Zero;
    private float moveSpeed = 5.0f;
    private List<Figure> figures = new List<Figure>(); // Lista de figuras

    public FigureGame() : base(GameWindowSettings.Default, NativeWindowSettings.Default)
    {
        Size = new Vector2i(800, 600);
        Title = "Grafica de N-U";
    }

    protected override void OnLoad()
    {
        base.OnLoad();
        GL.ClearColor(0.1f, 0.0f, 0.1f, 5.0f);

        string vertexShaderSource = @"
        #version 330 core
        layout (location = 0) in vec3 aPosition;
        layout (location = 1) in vec3 aColor;
        out vec3 fragColor;
        uniform mat4 model;
        uniform mat4 view;
        uniform mat4 projection;
        void main()
        {
            gl_Position = projection * view * model * vec4(aPosition, 1.0);
            fragColor = aColor;
        }";

        string fragmentShaderSource = @"
        #version 330 core
        in vec3 fragColor;
        out vec4 FragColor;
        void main()
        {
            FragColor = vec4(fragColor, 1.0);
        }";

        shader = new Shader(vertexShaderSource, fragmentShaderSource);
        inputHandler = new InputHandler();

        // Crear múltiples figuras con posiciones variadas
        figures.Add(new Figure(new Vector3(1.0f, 3.0f, -3.0f))); // Figura 1
        figures.Add(new Figure(new Vector3(4.0f, 0.0f, -3.0f))); // Figura 2
        figures.Add(new Figure(new Vector3(-4.0f, 0.0f, -3.0f))); // Figura 3

        // Habilitar prueba de profundidad
        GL.Enable(EnableCap.DepthTest);
    }

    protected override void OnUpdateFrame(FrameEventArgs args)
    {
        base.OnUpdateFrame(args);
        cubePosition = inputHandler.HandleMovement(KeyboardState, moveSpeed, (float)args.Time, cubePosition);
    }

    protected override void OnRenderFrame(FrameEventArgs args)
    {
        base.OnRenderFrame(args);
        GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

        shader.Use();

        Matrix4 view = Matrix4.LookAt(
        new Vector3(cubePosition.X, cubePosition.Y, cubePosition.Z + 10), // Posición cámara
        cubePosition, // Mira hacia el cubo
        Vector3.UnitY);

        //Matrix4 model = Matrix4.CreateRotationY(rotation) * Matrix4.CreateRotationX(rotation);

        Matrix4 projection = Matrix4.CreatePerspectiveFieldOfView(
            MathHelper.DegreesToRadians(45),
            (float)Size.X / Size.Y,
            0.1f, 100.0f);

        GL.UniformMatrix4(GL.GetUniformLocation(shader.Handle, "view"), false, ref view);
        GL.UniformMatrix4(GL.GetUniformLocation(shader.Handle, "projection"), false, ref projection);

        // Dibujar todas las figuras
        foreach (var figure in figures)
        {
            Matrix4 model = Matrix4.CreateTranslation(figure.Position);
            GL.UniformMatrix4(GL.GetUniformLocation(shader.Handle, "model"), false, ref model);
            figure.Draw();
        }

        SwapBuffers();
    }

}