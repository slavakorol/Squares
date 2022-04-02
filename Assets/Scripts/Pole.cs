namespace Assets.Scripts
{
    internal class Pole
    {
        public int poleWidth { get; set; }
        public int poleHeight {get; set; }

        private const int defaultPoleWidth = 15;
        private const int defaultPoleHeight = 25;

        private Ceil[,] ceils;

        public Pole(int poleWidth = defaultPoleWidth, int poleHeight = defaultPoleHeight)
        {
            this.poleWidth = poleWidth;
            this.poleHeight = poleHeight;

            InitialCeils();
        }

        private void InitialCeils()
        {
            this.ceils = new Ceil[poleWidth, poleHeight];

            //UpdateCeil(0, 0, poleWidth, poleHeight, CeilType.Empty);
        }

        /// <summary>
        /// Изменяет ячейку по позиции
        /// </summary>
        /// <param name="posX"></param>
        /// <param name="posY"></param>
        private void UpdateCeil(int posX, int posY, CeilType newType)
        {
            ceils[posX, posY].UpdateType(newType);
        }

        /// <summary>
        /// Изменяет несколько ячеек через углы прямоугольника
        /// </summary>
        /// <param name="posX1"></param>
        /// <param name="posY1"></param>
        /// <param name="posX2"></param>
        /// <param name="posY2"></param>
        /// <param name="newType"></param>
        private void UpdateCeil(int posX1, int posY1, int posX2, int posY2, CeilType newType)
        {
            // если 1 ближе

            if (posX1 < posX2 && posY1 < posY2 )
            {
                for (int cursorX = posX1; cursorX < posX2; cursorX++)
                {
                    for(int cursorY = posY1; cursorY < posY2; cursorY++)
                    {
                        UpdateCeil(cursorX, cursorY, newType);
                    }
                }
            }
            else
            {
                throw new System.Exception("Неправильная позиция");
            }

            // условие, если 1 дальше

            // условие, если 1 и 2 равны
        }
    }
}
