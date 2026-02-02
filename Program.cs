// See https://aka.ms/new-console-template for more information
using System.Diagnostics;
using System.IO.Compression;
using System.Numerics;
using System.Reflection.Metadata;
using System.Xml;
using Raylib_cs;
const int FPS = 240;
const int height = 800;
const int width = 800;
const int size = 15;
float dz = 1f;
float angle = 0;
Raylib.InitWindow(height, width, "Hello Raylib");
// Stopwatch stopwatch = new Stopwatch();
// stopwatch.Start();
// long lastUpdateTime = stopwatch.ElapsedMilliseconds;

Coord3D[] vs =
[
    new Coord3D(0.25f,0.25f,0.25f),
    new Coord3D(-0.25f,0.25f,0.25f),
    new Coord3D(-0.25f,-0.25f,0.25f),
    new Coord3D(0.25f,-0.25f,0.25f),

    new Coord3D(0.25f,0.25f,-0.25f),
    new Coord3D(-0.25f,0.25f,-0.25f),
    new Coord3D(-0.25f,-0.25f,-0.25f),
    new Coord3D(0.25f,-0.25f,-0.25f)
];

int[][] fs = [
    [0,1,2,3],
    [4,5,6,7],
    [0,4],
    [1,5],
    [2,6],
    [3,7]
];


while (!Raylib.WindowShouldClose())
{
    // long currentTime = stopwatch.ElapsedMilliseconds;
    // double deltaTime = (currentTime - lastUpdateTime) / 1000.0; 
    // lastUpdateTime = currentTime;


    Raylib.BeginDrawing();
    Raylib.ClearBackground(Color.RayWhite);
    //dz += 1.0f*(1.0f/FPS);
    angle += (float)(Math.PI*(1.0f/FPS));
    foreach(Coord3D v in vs)
    {
        // point(screen(project(translate(rotate_xz(v,angle),dz))));
    }
    foreach(int[] f in fs)
    {
        for(int i = 0; i < f.Length; i++){
            Coord3D a = vs[f[i]];
            Coord3D b = vs[f[(i + 1)%f.Length]];
            line(
                screen(project(translate(rotate_xz(a,angle),dz))),
                screen(project(translate(rotate_xz(b,angle),dz)))
            );
        }
    }
    Raylib.EndDrawing();
    Thread.Sleep(1000/FPS);
}

Raylib.CloseWindow();



static void point(Coord p)
{
    Raylib.DrawRectangleRec(new Rectangle(p.X - size/2,p.Y - size/2,size,size), Color.Black);

}

static Coord screen(Coord p)
{
    float screenX = (p.X + 1.0f) / 2 * width;
    float screenY = (1 - ((p.Y + 1.0f) / 2)) * height;
    return new Coord(screenX, screenY);
}

static Coord project(Coord3D p)
{
    return new Coord(
        p.X/p.Z,
        p.Y/p.Z
    );
}

static Coord3D translate(Coord3D p, float dz)
{
    return new Coord3D(p.X,p.Y,p.Z+dz);
}

static Coord3D rotate_xz(Coord3D p, float angle)
{
    float c = (float)Math.Cos(angle);
    float s = (float)Math.Sin(angle);
    return new Coord3D(p.X*c - p.Z*s, p.Y, p.X*s + p.Z*c);
}

static void line(Coord p1, Coord p2)
{
    Raylib.DrawLineEx(new Vector2(p1.X,p1.Y), new Vector2(p2.X,p2.Y), 3f, Color.Black);
}


struct Coord(float x, float y)
{
    public float X = x;
    public float Y = y;
}
struct Coord3D(float x, float y, float z)
{
    public float X = x;
    public float Y = y;
    public float Z = z;
}
