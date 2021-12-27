using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake.Renderer
{
    internal class GUIRenderer : IRenderer
    {
        private readonly Snake _snake;
        private readonly Apple _apple;
        private readonly Board _gameBoard;
        public GUIRenderer(Board map, Snake snake, Apple apple)
        {
            this._gameBoard = map;
            this._snake = snake;
            this._apple = apple;
        }

        public void Render()
        {
            
        }
    }
}
