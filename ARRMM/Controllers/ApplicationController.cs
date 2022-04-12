using ARRMM.Extensions;
using ARRMM.IServices;
using ARRMM.Models;
using ARRMM.Utilities.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ARRMM.Controllers
{
    public class ApplicationController : Controller
    {
        private readonly IApplicationService _applicationService;

        public ApplicationController(IApplicationService applicationService)
        {
            _applicationService = applicationService;
        }
        // GET: ApplicationController
        public async Task<ActionResult> Index()
        {
            var result = await _applicationService.GetApplications();
            return View(result);
        }

        // GET: ApplicationController
        public async Task<ActionResult> ReconnaissanceApplication()
        {
            var userId = User.GetUserId();
            var model = await _applicationService.LoadReconnaissanceApplication(userId);
            return View(model);
        }

        // POST: ApplicationController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ReconnaissanceApplication(ReconnaissanceModel model)
        {
            var userId = User.GetUserId();
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _applicationService.SubmitReconnaissanceApplication(model, userId);
                    if (result.Success)
                        return RedirectToAction(nameof(Index));

                    ModelState.AddModelError("", result.Message);
                }
                return RedirectToAction(nameof(Index));
            }
            catch(Exception ex)
            {
                var viewModel = await _applicationService.LoadReconnaissanceApplication(userId, model);
                return View(viewModel);
            }
        }

        // GET: ApplicationController
        public async Task<ActionResult> ExplorationApplicationIndividual()
        {
            var userId = User.GetUserId();
            var model = await _applicationService.LoadExplorationApplication(userId);
            return View(model);
        }

        // POST: ApplicationController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ExplorationApplicationIndividual(ExplorationModel model)
        {
            var userId = User.GetUserId();
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _applicationService.SubmitExplorationApplication(model, userId);
                    if (result.Success)
                        return RedirectToAction(nameof(Index));

                    ModelState.AddModelError("", result.Message);
                }
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                var viewModel = await _applicationService.LoadExplorationApplication(userId, model);
                return View(viewModel);
            }
        }

        // GET: ApplicationController
        public async Task<ActionResult> ExplorationApplicationCompany()
        {
            var userId = User.GetUserId();
            var model = await _applicationService.LoadExplorationApplication(userId);
            return View(model);
        }

        // POST: ApplicationController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ExplorationApplicationCompany(ExplorationModel model)
        {
            var userId = User.GetUserId();
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _applicationService.SubmitExplorationApplication(model, userId);
                    if (result.Success)
                        return RedirectToAction(nameof(Index));

                    ModelState.AddModelError("", result.Message);
                }
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                var viewModel = await _applicationService.LoadExplorationApplication(userId, model);
                return View(viewModel);
            }
        }

        // GET: ApplicationController
        public async Task<ActionResult> MineralDepositRetentionApplicationIndividual()
        {
            var userId = User.GetUserId();
            var model = await _applicationService.LoadMineralDepositRetentionApplication(userId);
            return View(model);
        }

        // POST: ApplicationController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> MineralDepositRetentionApplicationIndividual(MineralDepositRetentionModel model)
        {
            var userId = User.GetUserId();
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _applicationService.SubmitMineralDepositRetentionApplication(model, userId);
                    if (result.Success)
                        return RedirectToAction(nameof(Index));

                    ModelState.AddModelError("", result.Message);
                }
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                var viewModel = await _applicationService.LoadMineralDepositRetentionApplication(userId, model);
                return View(viewModel);
            }
        }

        // GET: ApplicationController
        public async Task<ActionResult> MineralDepositRetentionApplicationCompany()
        {
            var userId = User.GetUserId();
            var model = await _applicationService.LoadMineralDepositRetentionApplication(userId);
            return View(model);
        }

        // POST: ApplicationController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> MineralDepositRetentionApplicationCompany(MineralDepositRetentionModel model)
        {
            var userId = User.GetUserId();
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _applicationService.SubmitMineralDepositRetentionApplication(model, userId);
                    if (result.Success)
                        return RedirectToAction(nameof(Index));

                    ModelState.AddModelError("", result.Message);
                }
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                var viewModel = await _applicationService.LoadMineralDepositRetentionApplication(userId, model);
                return View(viewModel);
            }
        }

        // GET: ApplicationController
        public async Task<ActionResult> LargeMiningLeaseApplication()
        {
            var userId = User.GetUserId();
            var model = await _applicationService.LoadLargeMiningLeaseApplication(userId);
            return View(model);
        }

        // POST: ApplicationController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> LargeMiningLeaseApplication(LargeMiningLeaseModel model)
        {
            var userId = User.GetUserId();
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _applicationService.SubmitLargeMiningLeaseApplication(model, userId);
                    if (result.Success)
                        return RedirectToAction(nameof(Index));

                    ModelState.AddModelError("", result.Message);
                }
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                var viewModel = await _applicationService.LoadLargeMiningLeaseApplication(userId, model);
                return View(viewModel);
            }
        }

        // GET: ApplicationController
        public async Task<ActionResult> ProspectingApplication()
        {
            var userId = User.GetUserId();
            var model = await _applicationService.LoadProspectingApplication(userId);
            return View(model);
        }

        // POST: ApplicationController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ProspectingApplication(ProspectingModel model)
        {
            var userId = User.GetUserId();
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _applicationService.SubmitProspectingApplication(model, userId);
                    if (result.Success)
                        return RedirectToAction(nameof(Index));

                    ModelState.AddModelError("", result.Message);
                }
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                var viewModel = await _applicationService.LoadProspectingApplication(userId, model);
                return View(viewModel);
            }
        }

        // GET: ApplicationController
        public async Task<ActionResult> SmallMiningLeaseApplication()
        {
            var userId = User.GetUserId();
            var model = await _applicationService.LoadSmallMiningLeaseApplication(userId);
            return View(model);
        }

        // POST: ApplicationController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SmallMiningLeaseApplication(SmallMiningLeaseModel model)
        {
            var userId = User.GetUserId();
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _applicationService.SubmitSmallMiningLeaseApplication(model, userId);
                    if (result.Success)
                        return RedirectToAction(nameof(Index));

                    ModelState.AddModelError("", result.Message);
                }
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                var viewModel = await _applicationService.LoadSmallMiningLeaseApplication(userId, model);
                return View(viewModel);
            }
        }

        // GET: ApplicationController
        public async Task<ActionResult> LandSurrenderApplication()
        {
            var userId = User.GetUserId();
            var model = await _applicationService.LoadLandSurrenderApplication(userId);
            return View(model);
        }

        // POST: ApplicationController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> LandSurrenderApplication(LandSurrenderModel model)
        {
            var userId = User.GetUserId();
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _applicationService.SubmitLandSurrenderApplication(model, userId);
                    if (result.Success)
                        return RedirectToAction(nameof(Index));

                    ModelState.AddModelError("", result.Message);
                }
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                var viewModel = await _applicationService.LoadLandSurrenderApplication(userId, model);
                return View(viewModel);
            }
        }

        // GET: ApplicationController
        public async Task<ActionResult> LandTransferApplication()
        {
            var userId = User.GetUserId();
            var model = await _applicationService.LoadLandTransferApplication(userId);
            return View(model);
        }

        // POST: ApplicationController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> LandTransferApplication(LandTransferModel model)
        {
            var userId = User.GetUserId();
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _applicationService.SubmitLandTransferApplication(model, userId);
                    if (result.Success)
                        return RedirectToAction(nameof(Index));

                    ModelState.AddModelError("", result.Message);
                }
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                var viewModel = await _applicationService.LoadLandTransferApplication(userId, model);
                return View(viewModel);
            }
        }

        // GET: ApplicationController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ApplicationController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ApplicationController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ApplicationController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ApplicationController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ApplicationController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ApplicationController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
