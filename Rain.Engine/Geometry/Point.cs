using OpenTK.Graphics.OpenGL;

namespace Rain.Engine.Geometry;

/// <summary> A colored point in 3D space that can be rendered by the GPU. </summary>
public struct Point
{
	/// <summary> The length of any array outputed by <c>Point<T>.Array</c>. </summary>
	public const int BufferSize = Color.BufferSize + Vertex.BufferSize + TextureCoordinate.BufferSize;

	/// <summary> The location of the point in 3D space. </summary>
	public Vertex Vertex { get; set; }

	/// <summary> The color of the point. </summary>
	public Color Color { get; set; }

	/// <summary> The point's texture position. </summary>
	public TextureCoordinate TextureCoordinate { get; set; }

	/// <summary> An array representing the Vertex and Color data of this point. </summary>
	public float[] Array 
	{ 
		get 
		{
			var vertexData = new float[BufferSize];

			for (var i = 0; i < Vertex.Array.Length; i++)
				vertexData[i] = Vertex.Array[i];

			for (var i = 0; i < Color.Array.Length; i++)
				vertexData[i + Vertex.BufferSize] = Color.Array[i];

			for (var i = 0; i < TextureCoordinate.Array.Length; i++)
				vertexData[i + Vertex.BufferSize + Color.BufferSize] = TextureCoordinate.Array[i];

			return vertexData;
		}
	}

	/// <summary> Creates a new <c>Point</c> from a <c>Vertex</c>. </summary>
	/// <param name="vertex"> The <c>Vertex</c> to creates a <c>Point</c> from. </param>
	public Point(Vertex vertex)
	{
		Vertex = vertex;
		Color = new(180, 164, 240);
		TextureCoordinate = new(0.0f, 0.0f);
	}

	/// <summary> Creates a new <c>Point</c> from a <c>Vertex</c> and <c>Color</c>. </summary>
	/// <param name="vertex"> The new <c>Point</c>'s <c>Vertex</c>. </param>
	/// <param name="color"> The new <c>Point</c>'s <c>Color</c>. </param>
	public Point(Vertex vertex, Color color)
	{
		Vertex = vertex;
		Color = color;
		TextureCoordinate = new(0.0f, 0.0f);
	}

	/// <summary> Creates a new <c>Point</c> from a <c>Vertex</c> and <c>TextureCoordinate</c>. </summary>
	/// <param name="vertex"> The new <c>Point</c>'s <c>Vertex</c>. </param>
	/// <param name="textureCoordinate"> The new <c>Point</c>'s <c>TextureCoordinate</c>. </param>
	public Point(Vertex vertex, TextureCoordinate textureCoordinate)
	{
		Vertex = vertex;
		Color = new(180, 160, 240);
		TextureCoordinate = textureCoordinate;
	}

	/// <summary> Creates a new <c>Point</c> from a <c>Vertex</c>, <c>Color</c>, and <c>TextureCoordinate</c>. </summary>
	/// <param name="vertex"> The new <c>Point</c>'s <c>Vertex</c>. </param>
	/// <param name="color"> The new <c>Point</c>'s <c>Color</c>. </param>
	/// <param name="textureCoordinate"> The new <c>Point</c>'s <c>TextureCoordinate</c>. </param>
	public Point(Vertex vertex, Color color, TextureCoordinate textureCoordinate)
	{
		Vertex = vertex;
		Color = color;
		TextureCoordinate = textureCoordinate;
	}

	/// <summary> Tells OpenGL how to use the data sent through the Vertex Buffer. </summary>
	public static void SetAttributes(ShaderProgram shaderProgram)
	{
		// This is only defined here for maintanability purposes, usually I would put it somewhere like the ShaderProgram
		// class but it would be easier to make a hard to fix breaking change to the Point class that way. So this is left
		// here in hopes that if the Point class is ever changed in a way that would change the way Vertex Atttributes
		// are defined it would not be missed.
		GL.EnableVertexAttribArray(shaderProgram.GetAttributeHandleByName("vertexPosition"));
		GL.VertexAttribPointer(shaderProgram.GetAttributeHandleByName("vertexPosition"), Vertex.BufferSize, 
							   VertexAttribPointerType.Float, false, BufferSize * sizeof(float), 0);

		GL.EnableVertexAttribArray(shaderProgram.GetAttributeHandleByName("color"));
		GL.VertexAttribPointer(shaderProgram.GetAttributeHandleByName("color"), Color.BufferSize,
							   VertexAttribPointerType.Float, false, BufferSize * sizeof(float), 
							   Vertex.BufferSize * sizeof(float));
		
		GL.EnableVertexAttribArray(shaderProgram.GetAttributeHandleByName("texturePosition"));
		GL.VertexAttribPointer(shaderProgram.GetAttributeHandleByName("texturePosition"), TextureCoordinate.BufferSize,
						 	   VertexAttribPointerType.Float, false, BufferSize * sizeof(float),
							   Vertex.BufferSize * sizeof(float) + Color.BufferSize * sizeof(float));
	}
}