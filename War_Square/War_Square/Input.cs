using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace War_Square
{
    class Input
    {
        private static KeyboardState currentState;
        private static KeyboardState previousState;


        static Input()
        {
            currentState = Keyboard.GetState();
            previousState = Keyboard.GetState();
        }

        public static void Update()
        {
            previousState = currentState;
            currentState = Keyboard.GetState();
        }

        static public bool IsPressed(Keys Key)
        {
            return currentState.IsKeyDown(Key) && !previousState.IsKeyDown(Key);
        }

        static public bool IsDown(Keys Key)
        {
            return currentState.IsKeyDown(Key);
        }
    }
}
