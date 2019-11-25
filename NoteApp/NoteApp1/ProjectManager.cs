using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Newtonsoft.Json;


namespace NoteApp
{
    /// <summary>
    /// Класс, реализующий сохранение данных в файл и загрузку данных из файла.
    /// </summary>
    public class ProjectManager
    {
        /// <summary>
        /// Имя файла для сохранения и загрузки.
        /// </summary>
        private const string _name = @"\NoteApp.notes";

        /// <summary>
        /// Путь к папке с документами.
        /// </summary>
        private static readonly string _path =
        Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

        /// <summary>
        /// Полный путь к файлу.
        /// </summary>
        private static readonly string _file = _path + _name;

        /// <summary>
        /// Метод, сохраняющий данные в файл.
        /// </summary>
        public static void SaveToFile(Project data)
        {
            //Создаем экземпляр сериализатора.
            var serializer = new JsonSerializer { Formatting = Formatting.Indented };

            //Открываем поток для записи в файл с указанием пути.
            using (var sw = new StreamWriter(_file))
            using (JsonWriter writer = new JsonTextWriter(sw))
            {
                //Вызываем сериализацию и передаем объект, который хотим сериализовать.
                serializer.Serialize(writer, data);
            }
        }

        /// <summary>
        /// Метод, загружающий данные из файла.
        /// </summary>
        public static Project LoadFromFile()
        {
            //Создаем переменную, в которую поместим результат десериализации.
            Project project = null;

            //Создаем экземпляр сериализатора.
            var serializer = new JsonSerializer { Formatting = Formatting.Indented };

            //Открываем поток для чтения из файла с указанием пути.
            using (var sr = new StreamReader(_file))
            using (JsonReader reader = new JsonTextReader(sr))
            {
                //Вызываем десериализацию и явно преобразуем результат в целевой тип данных
                project = serializer.Deserialize<Project>(reader);
            }

            return project;
        }


    }
}