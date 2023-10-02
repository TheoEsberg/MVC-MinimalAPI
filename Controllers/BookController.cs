using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using MinimalAPI_BookStore.Models;
using MinimalAPI_BookStore.Models.DTOs;
using MVC_MinimalAPI.Models;
using MVC_MinimalAPI.Services;
using Newtonsoft.Json;
using System.Runtime.InteropServices;

namespace MVC_MinimalAPI.Controllers
{
	public class BookController : Controller
	{
		private readonly IBookService _bookService;
        public BookController(IBookService bookService)
        {
            this._bookService = bookService;
        }

        public async Task<IActionResult> BookIndex()
        {
            List<BookDTO> list = new List<BookDTO>();
            var response = await _bookService.GetAllBooks<ResponseDTO>();
            if (response != null && response.IsSuccess) 
            {
                list = JsonConvert.DeserializeObject<List<BookDTO>>(Convert.ToString(response.Result));
            }
            return View(list);
        }

        public async Task<IActionResult> Details(int id)
        { 
            BookDTO bookDTO = new BookDTO();
            var response = await _bookService.GetBookById<ResponseDTO>(id);
            if (response != null && response.IsSuccess)
            {
                BookDTO model = JsonConvert.DeserializeObject<BookDTO>(Convert.ToString(response.Result));
                return View(model);
            }
            return NotFound();
        }

        public async Task<IActionResult> BookCreate()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> BookCreate(BookCreateDTO bookCreateDTO)
        {
            if (ModelState.IsValid) 
            {
                var response = await _bookService.CreateBookAsync<ResponseDTO>(bookCreateDTO);
                if (response != null && response.IsSuccess) 
                {
                    return RedirectToAction(nameof(BookIndex));
                }
            }
            return View(bookCreateDTO);
        }


        public async Task<IActionResult> BookUpdate(int id)
        {
            var response = await _bookService.GetBookById<ResponseDTO>(id);
            if (response != null && response.IsSuccess)
            {
                BookUpdateDTO model = JsonConvert.DeserializeObject<BookUpdateDTO>(Convert.ToString(response.Result));
                return View(model);
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> BookUpdate(BookUpdateDTO bookUpdateDTO)
        {
            if (ModelState.IsValid)
            {
                var response = await _bookService.UpdateBookAsync<ResponseDTO>(bookUpdateDTO);
                if (response != null && response.IsSuccess)
                {
                    return RedirectToAction(nameof(BookIndex));
                }
            }
            return View(bookUpdateDTO);
        }

        
        public async Task<IActionResult> BookDelete(int id)
        {
            var response = await _bookService.GetBookById<ResponseDTO>(id);
            if (response != null && response.IsSuccess)
            {
                BookDTO book = JsonConvert.DeserializeObject<BookDTO>(Convert.ToString(response.Result));
                return View(book);
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> BookDelete(BookDTO book)
        {
            if (ModelState.IsValid)
            {
                var response = await _bookService.DeleteBookAsync<ResponseDTO>(book.Id);
                if (response != null && response.IsSuccess)
                {
                    return RedirectToAction(nameof(BookIndex));
                }
            }
            return NotFound();
        }
    }
}
