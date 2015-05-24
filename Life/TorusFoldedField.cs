using System;

namespace Life
{
    /// <summary>
    /// Представляет пространство, свернутое в тор
    /// </summary>
    public class TorusFoldedField
    {
        private Sector[,] _data;

        /// <summary>
        /// Создает новое пространство заданного размера
        /// </summary>
        /// <param name="width">Ширина пространства</param>
        /// <param name="height">Высота пространства</param>
        public TorusFoldedField(int width, int height)
        {
            Width = width;
            Height = height;
            _data = new Sector[Width, Height];
            for (var x = 0; x < Width; x++)
            {
                for (var y = 0; y < Height; y++)
                {
                    _data[x, y] = new Sector();
                }
            }
        }

        /// <summary>
        /// Ширина пространства
        /// </summary>
        public int Width { get; private set; }

        /// <summary>
        /// Высота пространства
        /// </summary>
        public int Height { get; private set; }

        /// <summary>
        /// Возвращает элемент с поверхности тора, с учетом свертки пространства
        /// </summary>
        /// <param name="x">Координата X</param>
        /// <param name="y">Координата Y</param>
        /// <returns>Элемент с поверхности тора</returns>
        public Sector this[int x, int y]
        {
            get
            {
                var _x = GetCircularCoordinate(x, Width);
                var _y = GetCircularCoordinate(y, Height);
                return _data[_x, _y];
            }
        }

        /// <summary>
        /// Позволяет получить реальную координату из координаты закольцованном пространстве
        /// </summary>
        /// <param name="x">Логическая координата в пространстве</param>
        /// <param name="length">Длинна пространства</param>
        /// <returns>Возвращает реальную координату в пространстве</returns>
        private static int GetCircularCoordinate(int x, int length)
        {
            if (length < 1)
                throw new ArgumentException("Размер пространства не может быть меньше 1", "length");
            if (x < 0) return length + x%length;
            if (x >= length) return x%length;
            return x;
        }
    }
}