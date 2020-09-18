using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DogGo.Models;
using DogGo.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DogGo.Controllers
{
    public class DogController : Controller
    {
        private readonly IDogRepository _dogRepo;
        private readonly IOwnerRepository _ownerRepo;

        public DogController(IDogRepository dogRepository, IOwnerRepository ownerRepository)
        {
            _dogRepo = dogRepository;
            _ownerRepo = ownerRepository;
        }

        // GET: DogController
        public ActionResult Index()
        {
            List<Dog> dogs = _dogRepo.GetAllDogs();

            return View(dogs);
        }

        // GET: DogController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DogController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Dog dog)
        {
            try
            {
                _dogRepo.AddDog(dog);

                return RedirectToAction(("Index"));
            }
            catch (Exception ex)
            {
                return View(dog);
            }
        }

        /*
        // LOOK AT THIS
        public ActionResult Create()
        {
            // We use a view model because we need the list of Owners in the Create view
            DogFormViewModel vm = new DogFormViewModel()
            {
                dog = new Dog(),
                Owners = _ownerRepo.GetAllOwners(),
            };

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Dog dog)
        {
            try
            {
                // LOOK AT THIS
                //  Let's save a new dog
                //  This new dog may or may not have Notes and/or an ImageUrl
                _dogRepo.AddDog(dog);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                // LOOK AT THIS
                //  When something goes wrong we return to the view
                //  BUT our view expects a DogFormViewModel object...so we'd better give it one
                DogFormViewModel vm = new DogFormViewModel()
                {
                    dog = dog,
                    Owners = _ownerRepo.GetAllOwners(),
                };

                return View(vm);
            }
        }


        */
        
        // GET: DogController/Details/5
        public ActionResult Details(int id)
        {
            Dog dog = _dogRepo.GetDogById(id);

            if (dog == null)
            {
                return NotFound();
            }

            return View(dog);
        }
        
        // GET: DogController/Edit/5
        public ActionResult Edit(int id)
        {
            Dog dog = _dogRepo.GetDogById(id);
            if (dog == null) 
            {
                return NotFound();
            }
            return View(dog);
        }

        // POST: DogController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Dog dog)
        {
            try
            {
                _dogRepo.UpdateDog(dog);
                return RedirectToAction(("Index"));
            }
            catch (Exception ex)
            {
                return View(dog);
            }
        }
        
        // GET: DogController/Delete/5
        public ActionResult Delete(int id)
        {
            Dog dog = _dogRepo.GetDogById(id);

            return View(dog);
        }

        // POST: DogController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Dog dog)
        {
            try
            {
                _dogRepo.DeleteDog(id);

                return RedirectToAction(("Index"));
            }
            catch (Exception ex)
            {
                return View(dog);
            }
        }
        
    }
}
