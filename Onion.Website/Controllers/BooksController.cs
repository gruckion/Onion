using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Onion.Data;
using Onion.Services;
using System;
using System.Threading.Tasks;

namespace Onion.Website.Controllers
{
	public class BooksController : Controller
	{
		private readonly IBookService _bookService;

		public BooksController(IBookService bookService)
		{
			_bookService = bookService ?? throw new ArgumentNullException(nameof(bookService));
		}


		// GET: Books
		public async Task<IActionResult> Index()
		{
			return View(await _bookService.GetAllAsync());
		}

		// GET: Books/Details/5
		public async Task<IActionResult> Details(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var book = await _bookService.GetAsyncById(id.Value);

			if (book == null)
			{
				return NotFound();
			}

			return View(book);
		}

		// GET: Books/Create
		public IActionResult Create()
		{
			return View();
		}

		// POST: Books/Create
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Create([Bind("Ordinal,Name,Description,Author,Id")] Book book)
		{
			if (ModelState.IsValid)
			{
				_bookService.Insert(book);
				return RedirectToAction(nameof(Index));
			}
			return View(book);
		}

		// GET: Books/Edit/5
		public async Task<IActionResult> Edit(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var book = await _bookService.GetAsyncById(id.Value);

			if (book == null)
			{
				return NotFound();
			}

			return View(book);
		}

		// POST: Books/Edit/5
		// To protect from over posting attacks, please enable the specific properties you want to bind to, for 
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Edit(int id, [Bind("Ordinal,Name,Description,Author,Id")] Book book)
		{
			if (id != book.Id)
			{
				return NotFound();
			}

			if (ModelState.IsValid)
			{
				try
				{
					_bookService.Update(book);
				}
				catch (DbUpdateConcurrencyException)
				{
					if (!BookExists(book.Id))
					{
						return NotFound();
					}
					else
					{
						throw;
					}
				}
				return RedirectToAction(nameof(Index));
			}
			return View(book);
		}

		// GET: Books/Delete/5
		public async Task<IActionResult> Delete(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var book = await _bookService.GetAsyncById(id.Value);

			if (book == null)
			{
				return NotFound();
			}

			return View(book);
		}

		// POST: Books/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public IActionResult DeleteConfirmed(int id)
		{
			_bookService.Delete(id);
			return RedirectToAction(nameof(Index));
		}

		private bool BookExists(int id)
		{
			return _bookService.Get(id) != null;
		}
	}
}
