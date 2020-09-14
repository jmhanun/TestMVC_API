using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MVC_asistentes.Controllers;
using MVC_asistentes.Data;
using MVC_asistentes.Models;

namespace MVCTest
{
    [TestClass]
    public class VariosTest
    {
        private ApplicationDbContext _db;

        [TestInitialize]
        public void InitContext()
        {
            var builder = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase("base_test");
            var context = new ApplicationDbContext(builder.Options);

            var personas = Enumerable.Range(1, 10)
                .Select(i => new Persona { Id = i, Nombre = $"nombre{i}", DNI = $"00000{i}", Sueldo = i });

            var libros = Enumerable.Range(1, 10)
                            .Select(i => new Book { Id = i, Titulo = $"titulo{i}", Autor = $"autor{i}" });

            context.Persona.AddRange(personas);
            context.Book.AddRange(libros);
            int changed = context.SaveChanges();

            _db = context;
        }

        [TestCleanup]
        public void Limpiar()
        {
            _db.Database.EnsureDeleted();
        }

        [TestMethod]
        public void PersonaTest()
        {
            //Arrange
            var controller = new PersonasController(_db);
            var expectedName = "nombre9";

            //Act

            Persona result = controller.GetPersona(9).Result.Value;

            //Assert
            Assert.AreEqual(expectedName, result.Nombre);

        }

        [TestMethod]
        public void BookTest()
        {
            //Arrage
            var controller = new BooksController(_db);
            var expectedTitle = "titulo5";
            //Act
            var result = controller.Details(5).Result as ViewResult;
            Book libro = (Book)result.Model;

            //Assert

            Assert.AreEqual(expectedTitle, libro.Titulo);
        }
    }
}
