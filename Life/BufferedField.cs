namespace Life
{
    public class BufferedField
    {
        #region Fields

        private readonly TorusFoldedField _first;
        private readonly TorusFoldedField _second;
        private bool _flipped = false;

        #endregion

        #region Constructors

        public BufferedField(int width, int height)
        {
            Width = width;
            Height = height;
            _first = new TorusFoldedField(Width, Height);
            _second = new TorusFoldedField(Width, Height);
        }

        #endregion

        #region Properties

        public int Width { get; private set; }
        public int Height { get; private set; }

        /// <summary>
        /// Текущий активный буфер
        /// </summary>
        public TorusFoldedField Front
        { get { return _flipped ? _first : _second; } }

        /// <summary>
        /// Текущий запасной буфер
        /// </summary>
        public TorusFoldedField Back
        { get { return _flipped ? _second : _first; } }

        #endregion

        #region Public members

        /// <summary>
        /// Меняет местами буферы
        /// </summary>
        public void Swap()
        {
            _flipped = !_flipped;
        }

        #endregion

        #region Private members

        #endregion
    }
}
