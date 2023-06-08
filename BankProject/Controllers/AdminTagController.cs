using BankProject.Data;
using BankProject.Models.Domain;
using BankProject.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;

namespace BankProject.Controllers
{
    public class AdminTagController : Controller
    {
        private readonly BankDbContext bankDbContext;

        public AdminTagController(BankDbContext bankDbContext) 
        {
            this.bankDbContext = bankDbContext;
        }
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        [ActionName("Add")]
        public IActionResult Add(AddTagRequest addTagRequest) 
        {
            //mapping AddTagRequest to Tag domain model
            var tag = new Tag
            {
                Name = addTagRequest.Name,
                DisplayName = addTagRequest.DisplayName
            };
            bankDbContext.Tags.Add(tag);
            bankDbContext.SaveChanges();
            return RedirectToAction("List");
        
        }
        
        
        [HttpGet]
        [ActionName("List")]
       public IActionResult List (){

            //use dbcontext to read text
            var tags = bankDbContext.Tags.ToList();

            return View(tags);

        }

        [HttpGet]
        public IActionResult Edit (Guid id)
        {
            // 1st method
            // var tag = bankDbContext.Tags.Find(id);

            //2nd method
           var tag =  bankDbContext.Tags.FirstOrDefault(x => x.Id == id);
            if(tag != null)
            {
                var editTagRequest = new EditTagRequest
                {
                    Id = tag.Id,
                    Name = tag.Name,
                    DisplayName = tag.DisplayName
                };
                return View(editTagRequest);
            }
            return View(null);
            
        }
        [HttpPost]
        public IActionResult Edit(EditTagRequest editTagRequest)
        {
            var tag = new Tag
            {
                Id = editTagRequest.Id,
                Name = editTagRequest.Name,
                DisplayName = editTagRequest.DisplayName
            };
            var existingTag = bankDbContext.Tags.Find(tag.Id);
            if (existingTag != null)
            {
                existingTag.Name = tag.Name;
                existingTag.DisplayName = tag.DisplayName;
                // save change
                bankDbContext.SaveChanges();
                // Show success notification
                return RedirectToAction("Edit", new { id = editTagRequest.Id });
            }


            // show error notification
            return RedirectToAction("Edit", new { id = editTagRequest.Id });
        }

        [HttpPost]
        public IActionResult Delete(EditTagRequest editTagRequest)
        {             var tag=bankDbContext.Tags.Find(editTagRequest.Id);
            if(tag != null) 
            {
                bankDbContext.Tags.Remove(tag);
                bankDbContext.SaveChanges();
                //Show a success notifivation
                return RedirectToAction("List");
           }
            // show an error notification
            return RedirectToAction("Edit", new { id = editTagRequest.Id });
        }

    }
}
