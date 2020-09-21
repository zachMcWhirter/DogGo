﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DogGo.Models;
using DogGo.Models.ViewModels;
using DogGo.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DogGo.Controllers
{
    public class OwnersController : Controller
    {

        private readonly IOwnerRepository _ownerRepo;
        private readonly IDogRepository _dogRepo;
        private readonly IWalkerRepository _walkerRepo;
        private readonly INeighborhoodRepository _neighborhoodRepo;

        // ASP.NET will give us an instance of our Walker Repository. This is called "Dependency Injection"
        public OwnersController(
            IOwnerRepository ownerRepository,
            IDogRepository dogRepository,
            IWalkerRepository walkerRepository,
            INeighborhoodRepository neighborhoodRepository)
        {
            _ownerRepo = ownerRepository;
            _dogRepo = dogRepository;
            _walkerRepo = walkerRepository;
            _neighborhoodRepo = neighborhoodRepository;
        }

        // GET: OwnersController
        public ActionResult Index()
        {
            List<Owner> owners = _ownerRepo.GetAllOwners();

            return View(owners);
        }
        // GET: OwnersController/Details/5
        public ActionResult Details(int id)
        {
            //  List the required elements needed to render the view the user should see

            // 1: An Owner object
            Owner owner = _ownerRepo.GetOwnerById(id);
            // 2: A list of Dogs
            List<Dog> dogs = _dogRepo.GetDogsByOwnerId(owner.Id);
            // 3: A list of Walkers
            List<Walker> walkers = _walkerRepo.GetWalkersInNeighborhood(owner.NeighborhoodId);

            ProfileViewModel vm = new ProfileViewModel()
            {
                Owner = owner,
                Dogs = dogs,
                Walkers = walkers
            };

            return View(vm);
        }

        // GET: Owners/Create
        public ActionResult Create()
        {
            List<Neighborhood> neighborhoods = _neighborhoodRepo.GetAll();

            OwnerFormViewModel vm = new OwnerFormViewModel()
            {
                Owner = new Owner(),
                Neighborhoods = neighborhoods
            };

            return View(vm);
        }


        // POST: Owners/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Owner owner)
        {
            try
            {
                _ownerRepo.AddOwner(owner);

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View(owner);
            }
        }

        // GET: Owners/Edit/5
        public ActionResult Edit(int id)
        {
            //  List the required elements needed to render the view the user should see

            Owner owner = _ownerRepo.GetOwnerById(id);
            List<Neighborhood> neighborhoods = _neighborhoodRepo.GetAll();

            OwnerFormViewModel vm = new OwnerFormViewModel()
            {
                Owner = owner,
                Neighborhoods = neighborhoods
            };
                        
            return View(vm);
        }

        // POST: OwnersController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Owner owner)
        {
            try
            {
                _ownerRepo.UpdateOwner(owner);

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                OwnerFormViewModel vm = new OwnerFormViewModel()
                {
                    Owner = owner,
                    Neighborhoods = _neighborhoodRepo.GetAll()
                };
                return View(vm);
            }
        }

        // GET: OwnersController/Delete/5
        public ActionResult Delete(int id)
        {
            Owner owner = _ownerRepo.GetOwnerById(id);

            return View(owner);
        }

        // POST: OwnersController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Owner owner)
        {
            try
            {
                _ownerRepo.DeleteOwner(id);

                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                return View(owner);
            }
        }

    //  Login Method

    //    public ActionResult Login()
    //    {
    //        return View();
    //    }

    //    [HttpPost]
    //    public async Task<ActionResult> Login(LoginViewModel viewModel)
    //    {
    //        Owner owner = _ownerRepo.GetOwnerByEmail(viewModel.Email);

    //        if (owner == null)
    //        {
    //            return Unauthorized();
    //        }

    //        var claims = new List<Claim>
    //{
    //    new Claim(ClaimTypes.NameIdentifier, owner.Id.ToString()),
    //    new Claim(ClaimTypes.Email, owner.Email),
    //    new Claim(ClaimTypes.Role, "DogOwner"),
    //};

    //        var claimsIdentity = new ClaimsIdentity(
    //            claims, CookieAuthenticationDefaults.AuthenticationScheme);

    //        await HttpContext.SignInAsync(
    //            CookieAuthenticationDefaults.AuthenticationScheme,
    //            new ClaimsPrincipal(claimsIdentity));

    //        return RedirectToAction("Index", "Dogs");
    //    }

    }
}
