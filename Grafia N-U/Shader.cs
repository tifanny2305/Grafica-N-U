using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using System;

public class Shader : IDisposable
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

        GL.GetProgram(Handle, GetProgramParameterName.LinkStatus, out int status);
        if (status == 0)
        {
            string log = GL.GetProgramInfoLog(Handle);
            throw new Exception($"Shader linking error: {log}");
        }

        GL.DeleteShader(vertexShader);
        GL.DeleteShader(fragmentShader);
    }

    private int CompileShader(string source, ShaderType type)
    {
        int shader = GL.CreateShader(type);
        GL.ShaderSource(shader, source);
        GL.CompileShader(shader);

        GL.GetShader(shader, ShaderParameter.CompileStatus, out int status);
        if (status == 0)
        {
            string log = GL.GetShaderInfoLog(shader);
            throw new Exception($"{type} shader compilation error: {log}");
        }

        return shader;
    }

    public void Use()
    {
        GL.UseProgram(Handle);
    }

    public void SetUniform(string name, Matrix4 matrix)
    {
        int location = GL.GetUniformLocation(Handle, name);
        if (location != -1)
            GL.UniformMatrix4(location, false, ref matrix);
    }

    public void Dispose()
    {
        GL.DeleteProgram(Handle);
    }
}
