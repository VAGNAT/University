using Microsoft.EntityFrameworkCore;
using Model;

namespace Infrastructure.EF
{
    public class UniversityContext : DbContext
    {
        public DbSet<Course> Courses { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Student> Students { get; set; }

        public UniversityContext(DbContextOptions<UniversityContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Course>().HasData(

                new Course { Id = 1, Name = "Экономика", Description = "Профиль ориентирован на подготовку квалифицированных руководителей и специалистов в области бухгалтерского учета, прикладного экономического анализа и аудита для работы в предприятиях различных форм собственности различного уровня, консалтинговых и аудиторских фирмах, банках, инвестиционных и страховых компаниях." },
                new Course { Id = 2, Name = "Информатика и вычислительная техника", Description = "Профиль ориентирован на подготовку квалифицированных специалистов в организациях любых отраслей экономики и форм собственности в качестве программиста, специалиста по тестированию в области информационных технологий, администратора баз данных, специалиста по технической документации в области информационных технологий; системного аналитика; специалиста по дизайну, графических и пользовательских интерфейсов, системного администратора информационно-коммуникационных систем, специалиста по администрированию сетевых устройств информационно-коммуникационных систем, системным программистом." },
                new Course { Id = 3, Name = "Прикладная информатика", Description = "Профиль ориентирован на подготовку квалифицированных специалистов занимающихся разработкой программных комплексов внедрением и эксплуатацией информационно-коммуникативных технологий различных предметных областях, а также в организациях и на предприятиях различных отраслей экономики и форм собственности в качестве программиста, специалиста по информационным системам, руководителя проектов в области информационных технологий, руководителя разработки программного обеспечения, системного аналитика." });

            modelBuilder.Entity<Group>().HasData(
                new { Id = 1, Name = "EC-18", CourseId = 1 },
                new { Id = 2, Name = "EC-19", CourseId = 1 },
                new { Id = 3, Name = "EC-20", CourseId = 1 },
                new { Id = 4, Name = "PI-18", CourseId = 3 },
                new { Id = 5, Name = "PI-19", CourseId = 3 },
                new { Id = 6, Name = "PI-20", CourseId = 3 },
                new { Id = 7, Name = "IVT-18", CourseId = 2 },
                new { Id = 8, Name = "IVT-19", CourseId = 2 },
                new { Id = 9, Name = "IVT-20", CourseId = 2 }
                );

            modelBuilder.Entity<Student>().HasData(
                new { Id = 1, FirstName = "Brown", LastName = "Yundt", GroupId = 1 },
                new { Id = 2, FirstName = "Alvina", LastName = "Schuppe", GroupId = 1 },
                new { Id = 3, FirstName = "Jaeden", LastName = "Okuneva", GroupId = 1 },
                new { Id = 4, FirstName = "Deangelo", LastName = "Bauch", GroupId = 2 },
                new { Id = 5, FirstName = "Davonte", LastName = "White", GroupId = 2 },
                new { Id = 6, FirstName = "Joy", LastName = "Buckridge", GroupId = 2 },
                new { Id = 7, FirstName = "Madyson", LastName = "D'Amore", GroupId = 3 },
                new { Id = 8, FirstName = "Ibrahim", LastName = "Rosenbaum", GroupId = 3 },
                new { Id = 9, FirstName = "Dessie", LastName = "McGlynn", GroupId = 3 },
                new { Id = 10, FirstName = "Laisha", LastName = "Glover", GroupId = 4 },
                new { Id = 11, FirstName = "Kenyon", LastName = "Prosacco", GroupId = 4 },
                new { Id = 12, FirstName = "Kasandra", LastName = "Green", GroupId = 4 },
                new { Id = 13, FirstName = "Jovani", LastName = "Feil", GroupId = 5 },
                new { Id = 14, FirstName = "Ludwig", LastName = "Streich", GroupId = 5 },
                new { Id = 15, FirstName = "Ethyl", LastName = "Barrows", GroupId = 5 },
                new { Id = 16, FirstName = "Joaquin", LastName = "Moen", GroupId = 6 },
                new { Id = 17, FirstName = "Constance", LastName = "Boyle", GroupId = 6 },
                new { Id = 18, FirstName = "Maximus", LastName = "Lesch", GroupId = 6 },
                new { Id = 19, FirstName = "Osvaldo", LastName = "Daniel", GroupId = 7 },
                new { Id = 20, FirstName = "Kendall", LastName = "Schaefer", GroupId = 7 },
                new { Id = 21, FirstName = "Brooklyn", LastName = "Moore", GroupId = 7 },
                new { Id = 22, FirstName = "Jacques", LastName = "Kulas", GroupId = 8 },
                new { Id = 23, FirstName = "Savion", LastName = "Wolff", GroupId = 8 },
                new { Id = 24, FirstName = "Mark", LastName = "Toy", GroupId = 8 },
                new { Id = 25, FirstName = "Bernadette", LastName = "Spencer", GroupId = 9 },
                new { Id = 26, FirstName = "Viola", LastName = "Kilback", GroupId = 9 },
                new { Id = 27, FirstName = "Jazmyne", LastName = "Zieme", GroupId = 9 });
        }
    }
}