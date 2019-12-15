using System;
using System.Collections.Generic;
using System.Text;


namespace NoteApp
{
    /// <summary>
    /// Класс заметки, хранящий информацию о её названии, тексте, категории, даты создания и даты изменения.
    /// </summary>
    public class Note : ICloneable
    {
        /// <summary>
        /// Поле "Название" заметки, класса "Заметка".
        /// </summary>
        private string _title;

        /// <summary>
        /// Поле "Текст" заметки, класса "Заметка".
        /// </summary>
        private string _text;

        /// <summary>
        /// Поле "Время создания заметки", класса "Заметка".
        /// </summary>
        private DateTime _dateOfCreation;

        /// <summary>
        /// Поле "Дата изменения заметки", класса "Заметка".
        /// </summary>
        private DateTime _dateOfChange;

        /// <summary>
        /// Поле "Категория заметки", класса "Заметка"
        /// </summary>
        private CategoryNote _categoryNote;

        /// <summary>
        /// Конструктор класса "Заметка"
        /// </summary>
        public Note()
        {
            _title = "Без названия";
            _text = "";
            _categoryNote = CategoryNote.Different;
            _dateOfCreation = DateTime.Now;
            _dateOfChange = DateTime.Now;
        }

        /// <summary>
        /// Возвращает и задает текст заметки.
        /// </summary>
        public string Text
        {
            get
            {
                return _text;
            }
            set
            {
                _text = value;
            }
        }

        /// <summary>
        /// Возвращает и задает название заметки.
        /// </summary>
        public string Title
        {
            get
            {
                return _title;
            }

            set
            {
                if (value.Length > 50)
                {
                    throw new ArgumentException("Название заметки должно быть меньше 50 знаков");
                }

                if (value.Length == 0)
                { 
                    throw new ArgumentException("Заголовок не может быть пустым!");
                }

                _title = value;
            }
        }
  
        /// <summary>
        /// Возвращает и задает дату создания заметки.
        /// </summary>
        public DateTime DateOfCreation
        {
            get
            {
                return _dateOfCreation;
            }

            set
            {
                if (value > DateTime.Now)
                {
                    throw new ArgumentException("Дата создания не может быть больше чем текущая!");
                }

               _dateOfCreation = value;
            }
        }

        /// <summary>
        /// Возвращает и задает дату изменения заметки.
        /// </summary>
        public DateTime DateOfChange
        {
            get
            {
                return _dateOfChange;
            }

            set
            {
                if (value < DateOfCreation)
                {
                    throw new ArgumentException("Дата редактирования не может быть меньше чем дата создания!");
                }

                if (value > DateTime.Now)
                {
                    throw new ArgumentException("Дата редактирования не может быть больше чем текущая!");
                }

                _dateOfChange = value;
            }
        }

        /// <summary>
        /// Возвращает и задает перечисление заметки.
        /// </summary>
        public CategoryNote CategoryNote
        {
            get
            {
                return _categoryNote;
            }

            set
            {
                _categoryNote = value;
            }
        }

        public object Clone()
        {
            return new Note
            {
                Title = Title,
                Text = Text,
                CategoryNote = CategoryNote,
                DateOfCreation = DateOfCreation,
                DateOfChange = DateOfChange,
            };
        }
    }
}