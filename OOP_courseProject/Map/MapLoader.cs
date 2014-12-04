using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using OOP_courseProject.Enums;
using System.IO;


namespace OOP_courseProject.Map
{
    class MapLoader 
    {
        public static Map LoadMap()
        {
            char[,] map = ReadMapFile();
            MySize size = new MySize(map.GetUpperBound(0) + 1, map.GetUpperBound(1) + 1); 
            Map gameField = new Map(size);
            for (int i = 0; i < size.Height; i++)
            {
                for (int j = 0; j < size.Width; j++)
                {
                    switch (map[i, j])
                    {
                        case 'B':
                            gameField[i, j] = TypeGameElement.Block;
                            break;
                        case 'O':
                            gameField[i, j] = TypeGameElement.Pell;
                            break;
                        case 'P':
                            gameField[i, j] = TypeGameElement.Pacman;
                            break;
                        case 'I':
                            gameField[i, j] = TypeGameElement.Inky;
                            break;
                        case 'L':
                            gameField[i, j] = TypeGameElement.Blynki;
                            break;
                        case 'C':
                            gameField[i, j] = TypeGameElement.Clyde;
                            break;
                        case 'Y':
                            gameField[i, j] = TypeGameElement.Pynki;
                            break;
                        case ' ':
                            gameField[i, j] = TypeGameElement.Empty;
                            break;
                        default:
                            throw new Exception("Карта некорректна!!! Проверьте правильность входно файла с картой!!!");
                    }
                }
            }
            return gameField;
        }
        private static char[,] ReadMapFile()
        {
            char[,] map;
            try
            {
                string[] mapStrings = File.ReadAllLines(@"Map\map.txt");
                map = new char[mapStrings.Length, mapStrings[0].Length];

                for (int j = 0; j < mapStrings[0].Length; j++)
                {
                    for (int i = 0; i < mapStrings.Length; i++)
                    {
                        map[i,j] = mapStrings[i][j];
                    }
                }
                return map;
            }
            catch (FileNotFoundException ex)
            {
                throw new FileNotFoundException("Файл с картой не найден!!!");
            }
            catch (FormatException ex)
            {
                throw new FormatException("Файл с картой имеет неверный формат!!!");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }
    }
}
