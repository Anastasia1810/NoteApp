using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

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

        /// <summary>
        /// Сортировка списка заметок по времени изменения и категории.
        /// </summary>
        public List<Note> SortedNotesCategory(CategoryNote category)
        {
            List<Note> notes = Notes.Where(note => note.CategoryNote == category)
                .OrderByDescending(note => note.DateOfChange).ToList();

            return notes;
        }

        /// <summary>
        /// Сортировка списка заметок по времени изменения
        /// </summary>
        public List<Note> SortedNotes()
        {
            List<Note> notes = Notes.OrderByDescending(note => note.DateOfChange).ToList();

            return notes;
        }

        /// <summary>
        /// Свойство "Текущая заметка"
        /// </summary>
        public Note CurrentNote
        {
            get;
            set;
        }

        /// <summary>
        /// Свойство "Текущая категория"
        /// </summary>
        public string CurrentCategory
        {
            get; set;
        }
    }
}