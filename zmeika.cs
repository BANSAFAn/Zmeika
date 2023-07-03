 internal class Snake : BaseModel
    {
        public Directions _direction;
        public int _lenght;
        public int _width;
        public int _height;
        public GameMode _gameMod;
        public Directions DirectionControl { get; set; }

        public Snake(int lengthSnake, int width, int height, Directions direction, GameMode gameMod)
        {
            if (width <= 0)
                throw new Exception();
            if (height <= 0)
                throw new Exception();
            if (height <= 0)
                throw new Exception();
            if (lengthSnake >= width || lengthSnake >= height)
                throw new Exception("Length of snake not be more size place ");
            _width = width;
            _height = height;
            _direction = direction;
            _gameMod = gameMod;
            _lenght = lengthSnake;

            Draw();

        }

        public bool Move(ref List<Point> frogPoints)
        {
            if (points.Count == 0)
                throw new Exception("Length of snake not be null ");
            int x = points.Last().X;
            int y = points.Last().Y;

            // Запрет разворота на 180
            // Если попытка поворота на 180 градусов
            if ((_direction == Directions.Right && DirectionControl == Directions.Left) ||
                (_direction == Directions.Left && DirectionControl == Directions.Right) ||
                (_direction == Directions.Up && DirectionControl == Directions.Down) ||
                (_direction == Directions.Down && DirectionControl == Directions.Up))
            {
                //_direction;
            }
            else
            {
                _direction = DirectionControl;
            }
            switch (_direction)
            {
                case Directions.Up:
                    y -= 1;
                    break;
                case Directions.Down:
                    y += 1;
                    break;
                case Directions.Left:
                    x -= 1;
                    break;
                case Directions.Right:
                    x += 1;
                    break;
            }
            bool confused = false;
            bool hitTheWall = false;

            // TODO учесть GameMod

            // Проверка. Врежется в стену или в свое тело
            foreach (var point in points)
            {
                confused = (point.X == x && point.Y == y);
                hitTheWall = (x > _width - 1 || x < 1) || (y > _height - 1 || y < 1);
                if (confused || hitTheWall)
                    break;
            }
            // Проверка. Сьест еду. 
            bool frogEated = false;
            for (int i = 0; i < frogPoints.Count; i++)
            {
                // Поела, хвост не удаляется
                if (frogPoints[i].X == x && frogPoints[i].Y == y)
                {
                    frogPoints.Remove(frogPoints[i]);
                    points[0].Model = Models.Tail;
                    points.Last().Model = Models.Body;
                    frogEated = true;
                }

            }
            // Осталась голодной, хвост удаляется
            if (!frogEated)
            {
                points.RemoveAt(0);
                points[0].Model = Models.Tail;
                points.Last().Model = Models.Body;
            }

            // Добавляем голову 
            points.Add(new Point(x, y, Models.Head));

            if (confused || hitTheWall)
            {
                return false;
            }
            else
            {
                return true;
            }

        }

        public override void Draw()
        {
            int startWidth = 1;
            int startHeight = 1;
            points = new List<Point>();

            for (int i = 0; i < _lenght; i++)
            {
                // Add Tail
                if (i == 0)
                    points.Add(new Point(startWidth + i, startHeight, Models.Tail));
                // Add Head
                else if (i == _lenght - 1)
                    points.Add(new Point(startWidth + i, startHeight, Models.Head));
                // Add Body
                else
                    points.Add(new Point(startWidth + i, startHeight, Models.Body));
            }
        }

    }
}




/// by BANSA, мне было скучно йоу)
