using UnityEngine;

namespace Assets.Scripts
{
    internal class PoleDrawer : MonoBehaviour
    {
        private Pole Pole { get; set; }

        public GameObject squarePrefab;
        private GameObject[,] squarePrefabClones;

        private Color dufaultColor = Color.white;
        private Color currentColor;
        private Color ColorWhenSelect = Color.yellow;
        private Color ColorWhenClick = Color.red;

        private const int distanceBetween = 11;
        private const int offset = 50;

        // количество выбранных квадратов в данный момент
        private int selectedCeilsCount;
        // индекс выбранной ячейки
        private int FirstSelectedCeilX;
        private int FirstSelectedCeilY;
        private int SecondSelectedCeilX;
        private int SecondSelectedCeilY;


        public PoleDrawer(GameObject squarePrefab)
        {
            this.squarePrefab = squarePrefab;
        }

        /// <summary>
        /// Рисует поле заданного размера
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        public void DrawPole(int width, int height)
        {
            Pole = new Pole(width, height);
            squarePrefabClones = new GameObject[height, width];

            for (int i = 0; i < Pole.poleHeight; i++)
            {
                for (int j = 0; j < Pole.poleWidth; j++)
                {
                    // todo: Нужен метод, который поможет отрисовать поле по центру в определенных границах
                    squarePrefabClones[i, j] = Instantiate(squarePrefab, new Vector2(i * distanceBetween - 105, j * distanceBetween - 30), Quaternion.identity);

                    // находим обработчик событий текущего квадрата
                    var squareEventHandler = squarePrefabClones[i, j].GetComponent<SquareHandler>();
                    // подписываемся на события квадратов
                    squareEventHandler.OnClick += OnCeilsClick;
                    squareEventHandler.OnEnter += OnCeilEnter;
                }
            }
        }

        /// <summary>
        /// Срабатывает, когда навели курсор на квадрат
        /// </summary>
        /// <param name="gameObject"></param>
        /// <exception cref="System.Exception"></exception>
        private void OnCeilEnter(GameObject gameObject)
        {
            var x = gameObject.transform.position.x;
            var y = gameObject.transform.position.y;

            //находим индекс квадрата в массиве
            PositionIndexesInArray((int)x, (int)y, out int xIndex, out int yIndex);

            var currSquare = squarePrefabClones[xIndex, yIndex];

            switch (selectedCeilsCount)
            {
                case 0:
                    PaintCeil(currSquare, ColorWhenSelect);
                    break;

                case 1:
                    PaintCeil(FindAllCeilsInSquare(FirstSelectedCeilX, FirstSelectedCeilY, SecondSelectedCeilX, SecondSelectedCeilY), ColorWhenSelect);
                    break;

                default:
                    throw new System.Exception("Невозможное количество выбранных квадратов");
            }
        }

        /// <summary>
        /// Срабатывает, когда на квадрат нажали
        /// </summary>
        /// <param name="gameObject"></param>
        /// <exception cref="System.Exception"></exception>
        private void OnCeilsClick(GameObject gameObject)
        {
            //позиция квадрата
            var x = gameObject.transform.position.x;
            var y = gameObject.transform.position.y;

            //находим индекс квадрата в массиве
            PositionIndexesInArray((int)x, (int)y, out int xIndex, out int yIndex);

            var currSquare = squarePrefabClones[xIndex, yIndex];

            // сколько выбрано квадратов?
            switch (selectedCeilsCount)
            {
                case 0:
                    PaintCeil(currSquare, ColorWhenClick);
                    FirstSelectedCeilX = xIndex;
                    FirstSelectedCeilY = yIndex;
                    selectedCeilsCount++;
                    break;

                case 1:
                    PaintCeil(FindAllCeilsInSquare(FirstSelectedCeilX, FirstSelectedCeilY, SecondSelectedCeilX, SecondSelectedCeilY), ColorWhenClick);
                    SecondSelectedCeilX = xIndex;
                    SecondSelectedCeilY = yIndex;
                    selectedCeilsCount++;
                    // после второго щелчка обнуляем количество выделенным квадратов
                    selectedCeilsCount = 0;
                    break;

                default:
                    throw new System.Exception("Невозможное количество выбранных квадратов");
            }
        }

        /// <summary>
        /// Находит индекс элемента из массива по входным координатам
        /// </summary>
        /// <param name="x">X координата искомого</param>
        /// <param name="y">Y координата искомого</param>
        /// <param name="xIndex">Возвращаемый индекс</param>
        /// <param name="yIndex">Возвращаемый индекс</param>
        /// <returns></returns>
        private void PositionIndexesInArray(int x, int y, out int xIndex, out int yIndex)
        {
            for (int i = 0; i < Pole.poleHeight; i++)
            {
                for (int j = 0; j < Pole.poleWidth; j++)
                {
                    if (squarePrefabClones[i, j].gameObject.transform.position.x == x &&
                        squarePrefabClones[i, j].gameObject.transform.position.y == y)
                    {
                        xIndex = i;
                        yIndex = j;
                        return;
                    }
                }
            }
            throw new System.Exception("Не найден данный элемент!");
        }

        /// <summary>
        /// Очищает поле
        /// </summary>
        public void ClearPole()
        {
            for (int y = 0; y < Pole.poleHeight; y++)
            {
                for (int x = 0; x < Pole.poleWidth; x++)
                {
                    Destroy(squarePrefabClones[y, x]);
                }
            }
        }

        /// <summary>
        /// Выделяет один квадрат
        /// </summary>
        private void PaintCeil(GameObject ceil, Color color)
        {
            ceil.GetComponent<SpriteRenderer>().color = color;
        }

        private void PaintCeil(GameObject[,] squares, Color color)
        {
            foreach (var ceil in squares)
            {
                ceil.GetComponent<SpriteRenderer>().color = color;
            }
        }

        private GameObject[,] FindAllCeilsInSquare(int x0, int y0, int x1, int y1)
        {
            // приводим к единому виду
            if (x0 > x1)
            {
                var temp = x0;
                x0 = x1;
                x1 = temp;
            }
            if (y0 > y1)
            {
                var temp = y0;
                y0 = y1;
                y1 = temp;
            }
            
            GameObject[,] selectedSquare = new GameObject[x1-x0, y1-y0];

            for (int i = x0; i < x1; i++)
            {
                for (int j = y0; j < y1; j++)
                {
                    selectedSquare[i, j] = squarePrefabClones[i, j];
                }
            }

            return selectedSquare;
        }

    }
}
