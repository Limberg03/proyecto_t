using OpenTK.Windowing.GraphicsLibraryFramework;
using OpenTK.Windowing.Desktop;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Windowing.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using opentk.sitios;
using OpenTK.Mathematics;
using System.Drawing;

namespace opentk
{
    class game : GameWindow
    {

        //atributos
        escenario escenario;
       

        public game(string sceneryFileName, GameWindowSettings gameWindowSettings, NativeWindowSettings nativeWindowSettings)
            : base(gameWindowSettings, nativeWindowSettings)
        {
            Console.WriteLine(sceneryFileName);
            escenario = escenario.LoadScene(sceneryFileName);
        }



        public Matrix4 ProjectionMatrix()
        {
            //Inicializar Matriz de Proyeccion
            Matrix4 projection = Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(45.0f), 
                Size.X / Size.Y, 1.0f, 100.0f);

            //posicion de la camara
            Vector3 cameraPosition = new(0.0f, 1.5f, 15.0f);
            Vector3 target = new(0.0f, 0.0f, 0.0f);
            Vector3 up = new(0.0f, 1.0f, 0.0f);

            Matrix4 view = Matrix4.LookAt(cameraPosition, target, up);

            return view * projection;
        }


           //1era      
        protected override void OnLoad()
        {
            GL.ClearColor(Color.SlateGray);

            escenario.OnLoad();
            escenario.SetMatrix(ProjectionMatrix());

            base.OnLoad();
        }


        protected override void OnRenderFrame(FrameEventArgs e)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            GL.Enable(EnableCap.DepthTest);

            escenario.dibujar();

            Context.SwapBuffers();
            base.OnRenderFrame(e);
        }


        protected override void OnResize(ResizeEventArgs e)
        {
            GL.Viewport(0, 0, Size.X, Size.Y);
            base.OnResize(e);
        }



        //-----

        protected override void OnKeyDown(KeyboardKeyEventArgs e)
        {
            KeyboardState inputKey = KeyboardState.GetSnapshot();



            if (this.IsFocused)
            {


              /*  if (inputKey.IsKeyDown(Keys.A))
                {
                    //hacia derecha
                    escenario.getObjeto("televisor").RotateX(0.5f);
                    //escenario.Traslacion(new Vector3(0.1f, 0.0f, 0.0f));
                }
                if (inputKey.IsKeyDown(Keys.P))
                {
                    escenario.getObjeto("televisor").RotateY(0.5f);
                }
                if(inputKey.IsKeyDown(Keys.C))
                {
                    escenario.getObjeto("televisor").RotateZ(0.5f);
                }*/

                //------------------- FACES-ROTAR --------------------------//
                if (inputKey.IsKeyDown(Keys.D5))
                {
                    //hacia derecha

                    escenario.getObjeto("florero").getFace("talloAT").Rotacion(0.1f,0,0,escenario.getObjeto("florero").getFace("talloAT").Origen);
                    //escenario.Traslacion(new Vector3(0.1f, 0.0f, 0.0f));
                }
                if (inputKey.IsKeyDown(Keys.D6))
                {
                    escenario.getObjeto("florero").getFace("talloAT").Rotacion(0,0.1f,0, escenario.getObjeto("florero").getFace("talloAT").Origen);
                }
                if (inputKey.IsKeyDown(Keys.D7))
                {
                    escenario.getObjeto("florero").getFace("talloAT").Rotacion(0,0,0.1f, escenario.getObjeto("florero").getFace("talloAT").Origen);
                }

                //------------------------------------ PARTE - ESCALAR -------------------------------------------------//
                if (inputKey.IsKeyDown(Keys.D8))
                {
                    escenario.getObjeto("florero").getFace("talloAT").Escalar(1.05f, escenario.getObjeto("florero").getFace("talloAT").Origen);
                }
                if (inputKey.IsKeyDown(Keys.D9))
                {
                    escenario.getObjeto("florero").getFace("talloAT").Escalar(0.95f, escenario.getObjeto("florero").getFace("talloAT").Origen);
                }



                //------------------- FACES-TRASLADAR --------------------------//
                if (inputKey.IsKeyDown(Keys.D1))
                {
                    //hacia derecha
                    escenario.getObjeto("florero").getFace("talloAT").Traslacion(0.3f, 0.0f, 0.0f);
                    //escenario.Traslacion(new Vector3(0.1f, 0.0f, 0.0f));
                }
                if (inputKey.IsKeyDown(Keys.D2))
                {
                    escenario.getObjeto("florero").getFace("talloAT").Traslacion(-0.3f, 0.0f, 0.0f);
                }
                if (inputKey.IsKeyDown(Keys.D3))
                {
                    escenario.getObjeto("florero").getFace("talloAT").Traslacion(0.0f, 0.3f, 0.0f);
                }
                if (inputKey.IsKeyDown(Keys.D4))
                {
                    escenario.getObjeto("florero").getFace("talloAT").Traslacion(0.0f, -0.3f, 0.0f);
                }
                



                //------------------- TELEVISOR-ROTAR --------------------------//
                if (inputKey.IsKeyDown(Keys.B))
                {
                    //hacia derecha
                    escenario.getObjeto("florero").Rotacion(0.1f,0,0,escenario.getObjeto("florero").Origen);
                    //escenario.Traslacion(new Vector3(0.1f, 0.0f, 0.0f));
                }
                if (inputKey.IsKeyDown(Keys.N))
                {
                    escenario.getObjeto("florero").Rotacion(0,0.1f,0, escenario.getObjeto("florero").Origen);
                }
                if (inputKey.IsKeyDown(Keys.M))
                {
                    escenario.getObjeto("florero").Rotacion(0,0,0.1f, escenario.getObjeto("florero").Origen);
                }
         



                //------------------- Televisor-TRASLADAR --------------------------//
                if (inputKey.IsKeyDown(Keys.L))
                {
                    //hacia derecha
                    escenario.getObjeto("florero").Traslacion(0.3f, 0.0f, 0.0f);
                    //escenario.Traslacion(new Vector3(0.1f, 0.0f, 0.0f));
                }
                if (inputKey.IsKeyDown(Keys.J))
                {
                    escenario.getObjeto("florero").Traslacion(-0.3f, 0.0f, 0.0f);
                }
                if (inputKey.IsKeyDown(Keys.I))
                {
                    escenario.getObjeto("florero").Traslacion(0.0f, 0.3f, 0.0f);
                }
                if (inputKey.IsKeyDown(Keys.K))
                {
                    escenario.getObjeto("florero").Traslacion(0.0f, -0.3f, 0.0f);
                }


                //------------------- televisor-ESCALAR --------------------------//
                if (inputKey.IsKeyDown(Keys.O))
                {       //hacia adelante
                    escenario.getObjeto("florero").Escalar(1.05f,escenario.getObjeto("florero").Origen);               
                }
                if (inputKey.IsKeyDown(Keys.P))
                {  // hacia atras  
                    escenario.getObjeto("florero").Escalar(0.95f, escenario.getObjeto("florero").Origen);
                }

               




                if (inputKey.IsKeyDown(Keys.D)) ////////////////////////////////////////???
                {
                    //hacia derecha                
                    escenario.Traslacion(0.3f, 0.0f, 0.0f);
                                        
                }

                if (inputKey.IsKeyDown(Keys.A))
                {
                    //hacia izquierda
                    escenario.Traslacion(-0.3f, 0.0f, 0.0f);    
                }

                if (inputKey.IsKeyDown(Keys.W))
                {
                    //hacia arriba
                    escenario.Traslacion(0.0f, 0.3f, 0.0f);
                }

                if (inputKey.IsKeyDown(Keys.S))
                {
                    //hacia abajo
                    escenario.Traslacion(0.0f, -0.3f, 0.0f);
                }


                ///////////////ESCALACION ESCENARIO ////////////////////
                if (inputKey.IsKeyDown(Keys.Q))
                {
                    //mas grande
                    escenario.Escalacion(1.05f);
                }

                if (inputKey.IsKeyDown(Keys.E))
                {
                    //mas pequeño
                    escenario.Escalacion(0.95f);
                }

                // rotar escenario
                if (inputKey.IsKeyDown(Keys.Y))
                {
                    escenario.Rotacion(0,0.06f,0);
                    //escenario.AddObject("television",)
                }

                if (inputKey.IsKeyDown(Keys.X))
                {
                    escenario.Rotacion(0.06f,0,0);
                }

                if (inputKey.IsKeyDown(Keys.Z))
                {
                    escenario.Rotacion(0,0,0.06f);
                }

            }
            base.OnKeyDown(e);
        }

      

    }
}
