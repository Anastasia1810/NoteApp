using System;
using System.Collections.Generic;
using System.Text;


namespace NoteApp
{
    /// <summary>
    /// Класс, содержащий список всех заметок, созданных в приложении.
    /// </summary>
    public class Project
    {
        /// <summary>
        /// Cписок заметок.
        /// </summary>
        public static List<Note> Notes;

        /// <summary>
        /// Возвращает и задает список всех заметок.
        /// </summary>
        public List<Note> Note
        {
            get
            {
                return Notes;
            }
            set
            {
                Notes = value;
            }
        }

        /// <summary>
        /// Конструктор класса "Проект".
        /// </summary>
        public Project()
        {
            Note = new List<Note>();
        }
        public Project(List<Note> noteList)
        {
            Notes = noteList;
        }

    }
}