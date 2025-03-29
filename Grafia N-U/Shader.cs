using OpenTK.Graphics.OpenGL4;
using System;

public class Shader
{
    public int Handle { get; private set; }

    public Shader(string vertexSource, string fragmentSource)
    {
        int vertexShader = CompileShader(vertexSource, ShaderType.VertexShader);
        int fragmentShader = CompileShader(fragmentSource, ShaderType.FragmentShader);

        Handle = GL.CreateProgram();
        GL.AttachShader(Handle, vertexShader);
        GL.AttachShader(Handle, fragmentShader);
        GL.LinkProgram(Handle);

        GL.DeleteShader(vertexShader);
        GL.DeleteShader(fragmentShader);
    }

    private int CompileShader(string source, ShaderType type)
    {
        int shader = GL.CreateShader(type);
        GL.ShaderSource(shader, source);
        GL.CompileShader(shader);
        return shader;
    }

    public void Use()
    {
        GL.UseProgram(Handle);
    }

    public void Delete()
    {
        GL.DeleteProgram(Handle);
    }
}
