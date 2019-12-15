using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace NoteApp.UnitTests
{
    [TestFixture]
    public class NoteTest
    {
      
        [Test(Description = "Позитивный тест геттера Title")]
        public void TestTitleGet_CorrectValue()
        {
            var expected = "Новая заметка 1";
            var note = new Note();
            note.Title = expected;
            var actual = note.Title;

            Assert.AreEqual(expected, actual, "Геттер Title возвращает неправильное название");
        }

        [TestCase("", "Должно возникать исключение, если название пустое",
           TestName = "Присвоение Title пустой строки")]
        [TestCase("Название-Название-Название-Название-Название-Название-Название-Название",
           "Должно возникать исключение, если название длиннее 50 символов",
           TestName = "Присвоение Title более 50 символов")]
        public void TestTitleSet_ArgumentExeption(string wrongTitle, string message)
        {
            var note = new Note();

            Assert.Throws<ArgumentException>(
            () => { note.Title = wrongTitle; }, 
            message);
        }

        [Test(Description = "Позитивный тест геттера Text")]
        public void TestTextGet_CorrectValue()
        {
            var expected = "Текст заметки";
            var note = new Note();
            note.Text = expected;
            var actual = note.Text;

            Assert.AreEqual(expected, actual, "Геттер Text возвращает неправильное название");
        }


        [Test(Description = "Позитивный тест геттера DateOfCreation")]
        public void TestDateOfCreationGet_CorrectValue()
        {
            var expected = DateTime.Now;
            var note = new Note();
            var actual = note.DateOfCreation;

            Assert.AreEqual(expected, actual, "Геттер DateOfCreation возвращает неправильное название");
        }

        [Test(Description = "Позитивный тест геттера DateOfChange")]
        public void TestDateOfChangeGet_CorrectValue()
        {
            var dateNow = DateTime.Now;
            var note = new Note();
            note.DateOfChange = dateNow;
            var expected = note.DateOfChange;
            var actual = note.DateOfChange;

            Assert.AreEqual(expected, actual, "Геттер DateOfChange возвращает неправильное название");
        }

        [Test(Description = "Дата создания больше текущей дата")]
        public void TestDateOfCreationSet_LongerCurrentDate()
        {
            var time = DateTime.Now;
            var note = new Note();
            time = time.AddDays(1);
            Assert.Throws<ArgumentException>(
            () => { note.DateOfCreation = time; },
            "Должно возникать исключение, если дата создания больше текущей");
        }

        [Test(Description = "Дата редактирования больше текущей дата")]
        public void TestDateOfChangeSet_LongerCurrentDate()
        {
            var time = DateTime.Now;
            var note = new Note();
            time = time.AddDays(3);
            Assert.Throws<ArgumentException>(
            () => { note.DateOfChange= time; },
            "Должно возникать исключение, если дата редактирования больше текущей");
        }

        [Test(Description = "Дата редактирования меньше даты создания")]
        public void TestDateOfChangeSet_LessCreatedDate()
        {
            var time = DateTime.Now;
            var note = new Note();
            time = time.AddDays(-1);
            Assert.Throws<ArgumentException>(
            () => { note.DateOfChange = time; },
            "Должно возникать исключение, если дата редактирования меньше создания");
        }

        [Test(Description = "Позитивный тест геттера CategoryNote")]
        public void TestCategoryNoteGet_CorrectValue()
        {
            var expected = CategoryNote.Work;
            var note = new Note();
            note.CategoryNote = expected;
            var actual = note.CategoryNote;

            Assert.AreEqual(expected, actual, "Геттер CategoryNote возвращает неправильное название");
        }

        [Test(Description = "Позитивный тест Clone")]
        public void TestClone_CorrectValue()
        {
            var expected = new Note();
            expected.Text = "Text 1";
            expected.Title = "Title 1";
            expected.CategoryNote = CategoryNote.Work;
            expected.DateOfCreation = DateTime.Now;
            Note clone = (Note)expected.Clone();
            var actual = clone;

            Assert.AreEqual(expected.Text, actual.Text, "");
            Assert.AreEqual(expected.Title, actual.Title, "");
            Assert.AreEqual(expected.CategoryNote, actual.CategoryNote, "");
        }
    }
}
