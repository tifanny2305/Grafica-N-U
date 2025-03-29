using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;

class FigureGame : GameWindow
{
    private Shader shader;
    private Figure figure;
    private InputHandler inputHandler;
    private Vector3 cubePosition = Vector3.Zero;
    private float moveSpeed = 5.0f;

    public FigureGame() : base(GameWindowSettings.Default, NativeWindowSettings.Default)
    {
        Size = new Vector2i(800, 600);
        Title = "Grafica de N-U";
    }

    protected override void OnLoad()
    {
        base.OnLoad();
        GL.ClearColor(0.1f, 0.1f, 0.1f, 1.0f);

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

        // Crear la figura en una posición específica
        figure = new Figure(new Vector3(1.0f, 0.0f, -3.0f));

        inputHandler = new InputHandler();

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

        // Configurar matrices
        Matrix4 model = Matrix4.CreateTranslation(cubePosition);

        Matrix4 view = Matrix4.LookAt(new Vector3(0, 0, 5), Vector3.Zero, Vector3.UnitY);
        Matrix4 projection = Matrix4.CreatePerspectiveFieldOfView(
            MathHelper.DegreesToRadians(45),
            (float)Size.X / Size.Y,
            0.1f, 100.0f);

        GL.UniformMatrix4(GL.GetUniformLocation(shader.Handle, "model"), false, ref model);
        GL.UniformMatrix4(GL.GetUniformLocation(shader.Handle, "view"), false, ref view);
        GL.UniformMatrix4(GL.GetUniformLocation(shader.Handle, "projection"), false, ref projection);

        figure.Draw();
        SwapBuffers();
    }
}