using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ViewModels;
using System.Data.Entity;

namespace Vidly.Controllers
{
    public class MovieController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MovieController() {
            _context = new ApplicationDbContext();
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        // GET: Movies/Random

        #region INDEX
        public ActionResult Index()
        {
            List<Movie> movies = _context.Movies.Include(m => m.Genre).ToList();//TODO Resolver este error
            return View(movies);
        }
        #endregion

        #region CREATE
        public ActionResult New() {
            var genre = _context.Genre.ToList();
            MovieFormViewModel viewModel = new MovieFormViewModel
            {
                Genre = genre
            };
            ViewBag.Title = "Creating Movie";
            return View("MovieForm", viewModel);
        }

        [HttpPost]
        public ActionResult SaveOrUpdate(Movie movie)
        {
            if (movie.Id == 0)
            {
                movie.DateAdded = System.DateTime.Now;
                _context.Movies.Add(movie);
            }
            else
            {
                var movieInDb = _context.Movies.Single(m => m.Id == movie.Id);
                movieInDb.Name = movie.Name;
                movieInDb.ReleaseDate = movie.ReleaseDate;
                movieInDb.GenreId = movie.GenreId;
                movieInDb.NumberInStock = movie.NumberInStock;
            }
            _context.SaveChanges();
            return RedirectToAction("Index", "Movie");
        }
        #endregion

        #region EDIT


        public ActionResult Edit(int id)
        {
            Movie movie = _context.Movies.SingleOrDefault(m => m.Id == id);
            if (movie == null)
                return HttpNotFound();

            var viewModel = new MovieFormViewModel
            {
                Genre = _context.Genre.ToList(),
                Movie = movie
            };
            ViewBag.Title = "Editing Movie";
            return View("MovieForm", viewModel);
        }
        #endregion
    }
}