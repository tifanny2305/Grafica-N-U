using OpenTK.Mathematics;

public class Figure : Mesh
{
    public Figure(Vector3 position) : base(CreateVertices(position), indices) { }

    private static float[] CreateVertices(Vector3 position)
    {
        // En el método CreateVertices() de la clase Figure
        float width = 2.0f;       // Ancho total de la U (aumentado para mejor visibilidad)
        float height = 3.0f;      // Altura de las columnas (aumentado)
        float thickness = 0.5f;   // Grosor en el eje Z (ligeramente aumentado)
        float columnWidth = 0.4f; // Ancho de cada columna (aumentado para proporción)
        float baseHeight = 0.6f;  // Altura de la base (aumentado)

        // Calculamos las posiciones relativas al punto de origen
        return new float[] {
            // Columna izquierda (8 vértices)
            // Frente
            position.X, position.Y, position.Z - thickness/2,          0.0f, 1.0f, 0.0f,
            position.X + columnWidth, position.Y, position.Z - thickness/2, 0.0f, 1.0f, 0.0f,
            position.X + columnWidth, position.Y + height, position.Z - thickness/2, 0.0f, 1.0f, 0.0f,
            position.X, position.Y + height, position.Z - thickness/2, 0.0f, 1.0f, 0.0f,
            // Atrás
            position.X, position.Y, position.Z + thickness/2,          0.0f, 1.0f, 0.0f,
            position.X + columnWidth, position.Y, position.Z + thickness/2, 0.0f, 1.0f, 0.0f,
            position.X + columnWidth, position.Y + height, position.Z + thickness/2, 0.0f, 1.0f, 0.0f,
            position.X, position.Y + height, position.Z + thickness/2, 0.0f, 1.0f, 0.0f,

            // Columna derecha (8 vértices)
            // Frente
            position.X + width - columnWidth, position.Y, position.Z - thickness/2, 0.0f, 1.0f, 0.0f,
            position.X + width, position.Y, position.Z - thickness/2, 0.0f, 1.0f, 0.0f,
            position.X + width, position.Y + height, position.Z - thickness/2, 0.0f, 1.0f, 0.0f,
            position.X + width - columnWidth, position.Y + height, position.Z - thickness/2, 0.0f, 1.0f, 0.0f,
            // Atrás
            position.X + width - columnWidth, position.Y, position.Z + thickness/2, 0.0f, 1.0f, 0.0f,
            position.X + width, position.Y, position.Z + thickness/2, 0.0f, 1.0f, 0.0f,
            position.X + width, position.Y + height, position.Z + thickness/2, 0.0f, 1.0f, 0.0f,
            position.X + width - columnWidth, position.Y + height, position.Z + thickness/2, 0.0f, 1.0f, 0.0f,

            // Base (8 vértices)
            // Frente
            position.X, position.Y, position.Z - thickness/2, 0.0f, 1.0f, 0.0f,
            position.X + width, position.Y, position.Z - thickness/2, 0.0f, 1.0f, 0.0f,
            position.X + width, position.Y + baseHeight, position.Z - thickness/2, 0.0f, 1.0f, 0.0f,
            position.X, position.Y + baseHeight, position.Z - thickness/2, 0.0f, 1.0f, 0.0f,
            // Atrás
            position.X, position.Y, position.Z + thickness/2, 0.0f, 1.0f, 0.0f,
            position.X + width, position.Y, position.Z + thickness/2, 0.0f, 1.0f, 0.0f,
            position.X + width, position.Y + baseHeight, position.Z + thickness/2, 0.0f, 1.0f, 0.0f,
            position.X, position.Y + baseHeight, position.Z + thickness/2, 0.0f, 1.0f, 0.0f
        };
    }

    private static readonly uint[] indices = {
        // Caras columna izquierda
        0, 1, 2, 2, 3, 0,
        4, 5, 6, 6, 7, 4,
        0, 1, 5, 5, 4, 0,
        2, 3, 7, 7, 6, 2,
        0, 3, 7, 7, 4, 0,
        1, 2, 6, 6, 5, 1,

        // Caras columna derecha
        8, 9, 10, 10, 11, 8,
        12, 13, 14, 14, 15, 12,
        8, 9, 13, 13, 12, 8,
        10, 11, 15, 15, 14, 10,
        8, 11, 15, 15, 12, 8,
        9, 10, 14, 14, 13, 9,

        // Caras base
        16, 17, 18, 18, 19, 16,
        20, 21, 22, 22, 23, 20,
        16, 17, 21, 21, 20, 16,
        18, 19, 23, 23, 22, 18,
        16, 19, 23, 23, 20, 16,
        17, 18, 22, 22, 21, 17
    };
}