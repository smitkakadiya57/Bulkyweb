using Bulkyweb.Data;
using Bulkyweb.Models;
using Microsoft.AspNetCore.Mvc;

namespace Bulkyweb.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _db;
        public CategoryController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            List<Category> objCategoryList=_db.Categories.ToList();  // retriving all category list
            return View(objCategoryList); // pass to view
        }

        public IActionResult Create()
        {
            return View(); //if you pass nothing then it will create new object with empty values
        }
        [HttpPost] //POST endpoint
		public IActionResult Create(Category obj)
		{
            if(obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name", "The DisplayOrder cannot exactly match the name");
            }
			
			if (ModelState.IsValid)
			{
				_db.Categories.Add(obj); // add category obj to table
				_db.SaveChanges();
				TempData["success"] = "Category Created Successfully";
				return RedirectToAction("Index"); 
			}
           
			return View();
		}

		public IActionResult Edit(int? id)
		{
			if(id== null || id == 0)
			{
				return NotFound();
			}
			Category categoryFromDb = _db.Categories.Find(id);  //work on only primary keys
			
			if(categoryFromDb == null)
			{
				return NotFound();
			}
			return View(categoryFromDb); 
		}
		[HttpPost] 
		public IActionResult Edit(Category obj)
		{
			if (ModelState.IsValid)
			{
				_db.Categories.Update(obj); // Update category obj to table using id populated automatically
				_db.SaveChanges();
				TempData["success"] = "Category Updated Successfully";
				return RedirectToAction("Index");
			}

			return View();
		}

		public IActionResult Delete(int? id)
		{
			if (id == null || id == 0)
			{
				return NotFound();
			}
			Category categoryFromDb = _db.Categories.Find(id);  //work on only primary keys

			if (categoryFromDb == null)
			{
				return NotFound();
			}
			return View(categoryFromDb);
		}
		[HttpPost, ActionName("Delete")]  // to explicitly define end point name
		public IActionResult DeletePOST(int ? id)
		{
			Category obj= _db.Categories.Find(id);
			if (obj == null)
			{
				return NotFound();
			}
			_db.Categories.Remove(obj);
			_db.SaveChanges();
			TempData["success"] = "Category Deleted Successfully";  // use for diplaying notifications
			return RedirectToAction("Index");
		}
	}
}
