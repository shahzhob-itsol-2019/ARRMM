using ARRMM.Database;
using ARRMM.Entities;
using ARRMM.IServices;
using ARRMM.Models;
using ARRMM.Utilities.Constants;
using ARRMM.Utilities.Enums;
using AutoMapper;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ARRMM.Services
{
    public class ApplicationService : IApplicationService
    {
        private readonly IDbContext dbContext;
        private readonly IMapper mapper;

        private readonly IStatusService _statusService;
        private readonly ICompanyService _companyService;
        private readonly ICountryService _countryServie;
        private readonly IMineralService _mineralService;

        public ApplicationService(
            IDbContext dbContext, 
            IMapper mapper, 
            IStatusService statusService, 
            ICompanyService companyService,
            ICountryService countryService,
            IMineralService mineralService)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;

            _statusService = statusService;
            _companyService = companyService;
            _countryServie = countryService;
            _mineralService = mineralService;
        }

        public async Task<List<ApplicationModel>> GetApplications(string userId = null)
        {
            IQueryable<Application> query = dbContext.Applications
                                                     .Include(x => x.Reconnaissance)
                                                     .Include(m => m.Exploration)
                                                     .Include(m => m.MineralDepositRetention)
                                                     .Include(m => m.LargeMiningLease)
                                                     .Include(m => m.Prospecting)
                                                     .Include(m => m.SmallMiningLease)
                                                     .Include(m => m.LandSurrenderAndTransfer)
                                                     .Include(m => m.Persons)
                                                     .Include(m => m.Minerals);

            if (!string.IsNullOrWhiteSpace(userId))
                query = query.Where(x => x.UserId.Equals(userId));

            var entities = await query.ToListAsync();
            var result = mapper.Map<List<ApplicationModel>>(entities);
            return result;
        }

        public async Task<List<ApplicationMineralModel>> GetAppliedMinerals(string userId = null)
        {
            IQueryable<ApplicationMineral> query = dbContext.ApplicationMinerals
                                                            .Include(x => x.Application)
                                                            .Include(m => m.Mineral);

            if (!string.IsNullOrWhiteSpace(userId))
                query = query.Where(x => x.Application.UserId.Equals(userId));

            var entities = await query.ToListAsync();

            var result = mapper.Map<List<ApplicationMineralModel>>(entities);
            return result;
        }

        public async Task<ReconnaissanceModel> LoadReconnaissanceApplication(string userId, ReconnaissanceModel model = null)
        {
            var countries = await _countryServie.Get();
            var applicionStatuses = await _statusService.Get(StatusType.ApplicionStatus);
            var operationStatuses = await _statusService.Get(StatusType.OperationStatus);
            var mierals = await _mineralService.Get();
            var previousMinerals = await GetAppliedMinerals(userId);

            if (model == null)
                model = new ReconnaissanceModel();
            
            model.PreviousAppliedMinerals = previousMinerals;

            foreach (var country in countries)
                model.Countries.Add(new SelectListItem { Value = country.Id.ToString(), Text = country.Nationality });

            foreach (var status in applicionStatuses)
                model.ApplicionStatuses.Add(new SelectListItem { Value = status.Code, Text = status.Name });

            foreach (var status in operationStatuses)
                model.OperationStatuses.Add(new SelectListItem { Value = status.Code, Text = status.Name });

            foreach (var mieral in mierals)
                model.Minerals.Add(new SelectListItem { Value = mieral.Id.ToString(), Text = mieral.Name });

            return model;
        }

        public async Task<ResponseModel> SubmitReconnaissanceApplication(ReconnaissanceModel model, string userId)
        {
            var status = await _statusService.Get(ApplicationStausCode.None, StatusType.ApplicionStatus);
            if (status == null)
                return new ResponseModel { Success = false, Message = "status is not found in the system." };

            CompanyModel company = null;
            if (model.HasShareCapital)
            {
                company = await _companyService.Add(new CompanyModel
                {
                    Name = model.ApplicantName
                });
            }

            var entity = mapper.Map<Reconnaissance>(model);
            entity.PreviousApplicionStatus = null;
            entity.PreviousOperationStatus = null;
            var application = new Application
            {
                ApplicationNumber = "Reconnaissance-001",
                Type = ApplicationType.Form_A_ReconnaissanceLicenceByCompany,
                ScaleType = ScaleType.LargeScaleMining,
                CreatedAt = DateTime.UtcNow,
                StatusId = status.Id,
                UserId = userId,
                Reconnaissance = entity
            };
            if (model.CompanyControllers != null && model.CompanyControllers.Any())
            {
                if(company == null)
                    company = await _companyService.Add(new CompanyModel
                    {
                        Name = model.ApplicantName
                    });

                foreach (var controller in model.CompanyControllers)
                {
                    application.Persons.Add(new Person
                    {
                        FirstName = controller.FirstName,
                        LastName = controller.LastName,
                        Address = controller.Address,
                        ApplicationId = application.Id,
                        CompanyId = company?.Id,
                        CountryId = controller.CountryId,
                        Type = PersonType.Controller
                    });
                }
            }
            if (model.Directors != null && model.Directors.Any())
            {
                if (company == null)
                    company = await _companyService.Add(new CompanyModel
                    {
                        Name = model.ApplicantName
                    });

                foreach (var director in model.Directors)
                {
                    application.Persons.Add(new Person
                    {
                        FirstName = director.FirstName,
                        LastName = director.LastName,
                        Address = director.Address,
                        ApplicationId = application.Id,
                        CompanyId = company?.Id,
                        CountryId = director.CountryId,
                        Type = PersonType.Director
                    });
                }
            }
            if (model.Officers != null && model.Officers.Any())
            {
                if (company == null)
                    company = await _companyService.Add(new CompanyModel
                    {
                        Name = model.ApplicantName
                    });

                foreach (var officer in model.Officers)
                {
                    application.Persons.Add(new Person
                    {
                        FirstName = officer.FirstName,
                        LastName = officer.LastName,
                        Address = officer.Address,
                        ApplicationId = application.Id,
                        CompanyId = company?.Id,
                        CountryId = officer.CountryId,
                        Type = PersonType.Officer
                    });
                }
            }
            if (model.SelectedMineralIds != null)
            {
                foreach (var mineralId in model.SelectedMineralIds)
                {
                    application.Minerals.Add(new ApplicationMineral
                    {
                        ApplicationId = application.Id,
                        MineralId = mineralId
                    });
                }
            }

            var previousApplicationStatus = await _statusService.Get(model.PreviousApplicationStatusCode, StatusType.ApplicionStatus);
            if (previousApplicationStatus == null)
                return new ResponseModel { Success = false, Message = "status is not found in the system." };
            application.Reconnaissance.PreviousApplicationStatusId = previousApplicationStatus.Id;

            var previousOperationStatus = await _statusService.Get(model.PreviousOperationStatusCode, StatusType.OperationStatus);
            if (previousOperationStatus == null)
                return new ResponseModel { Success = false, Message = "status is not found in the system." };
            application.Reconnaissance.PreviousOperationStatusId = previousOperationStatus.Id;

            var response = await dbContext.Applications.AddAsync(application);
            await dbContext.SaveChangesAsync();
            var result = mapper.Map<ApplicationModel>(application);
            return new ResponseModel { Success = true, Message = "Added successfully.", Data = result };
        }

        public async Task<ExplorationModel> LoadExplorationApplication(string userId, ExplorationModel model = null)
        {
            var countries = await _countryServie.Get();
            var applicionStatuses = await _statusService.Get(StatusType.ApplicionStatus);
            var operationStatuses = await _statusService.Get(StatusType.OperationStatus);
            var mierals = await _mineralService.Get();
            var previousMinerals = await GetAppliedMinerals(userId);

            if (model == null)
                model = new ExplorationModel();

            model.PreviousAppliedMinerals = previousMinerals;

            foreach (var country in countries)
                model.Countries.Add(new SelectListItem { Value = country.Id.ToString(), Text = country.Nationality });

            foreach (var status in applicionStatuses)
                model.ApplicionStatuses.Add(new SelectListItem { Value = status.Code, Text = status.Name });

            foreach (var status in operationStatuses)
                model.OperationStatuses.Add(new SelectListItem { Value = status.Code, Text = status.Name });

            foreach (var mieral in mierals)
                model.Minerals.Add(new SelectListItem { Value = mieral.Id.ToString(), Text = mieral.Name });

            return model;
        }

        public async Task<ResponseModel> SubmitExplorationApplication(ExplorationModel model, string userId)
        {
            var status = await _statusService.Get(ApplicationStausCode.None, StatusType.ApplicionStatus);
            if (status == null)
                return new ResponseModel { Success = false, Message = "status is not found in the system." };

            CompanyModel company = null;
            if (model.IsCompany)
            {
                company = await _companyService.Add(new CompanyModel
                {
                    Name = model.ApplicantName
                });
            }

            var entity = mapper.Map<Exploration>(model);
            entity.PreviousApplicionStatus = null;
            entity.PreviousOperationStatus = null;
            var application = new Application
            {
                ApplicationNumber = "Exploration-001",
                Type = !model.IsCompany ? ApplicationType.Form_B_ExplorationLicenceByIndividual : ApplicationType.Form_C_ExplorationLicenceByCompany,
                ScaleType = ScaleType.LargeScaleMining,
                CreatedAt = DateTime.UtcNow,
                StatusId = status.Id,
                UserId = userId,
                Exploration = entity
            };
            if (model.CompanyControllers != null && model.CompanyControllers.Any())
            {
                if (company == null)
                    company = await _companyService.Add(new CompanyModel
                    {
                        Name = model.ApplicantName
                    });

                foreach (var controller in model.CompanyControllers)
                {
                    application.Persons.Add(new Person
                    {
                        FirstName = controller.FirstName,
                        LastName = controller.LastName,
                        Address = controller.Address,
                        ApplicationId = application.Id,
                        CompanyId = company?.Id,
                        CountryId = controller.CountryId,
                        Type = PersonType.Controller
                    });
                }
            }
            if (model.Directors != null && model.Directors.Any())
            {
                if (company == null)
                    company = await _companyService.Add(new CompanyModel
                    {
                        Name = model.ApplicantName
                    });

                foreach (var director in model.Directors)
                {
                    application.Persons.Add(new Person
                    {
                        FirstName = director.FirstName,
                        LastName = director.LastName,
                        Address = director.Address,
                        ApplicationId = application.Id,
                        CompanyId = company?.Id,
                        CountryId = director.CountryId,
                        Type = PersonType.Director
                    });
                }
            }
            if (model.Officers != null && model.Officers.Any())
            {
                if (company == null)
                    company = await _companyService.Add(new CompanyModel
                    {
                        Name = model.ApplicantName
                    });

                foreach (var officer in model.Officers)
                {
                    application.Persons.Add(new Person
                    {
                        FirstName = officer.FirstName,
                        LastName = officer.LastName,
                        Address = officer.Address,
                        ApplicationId = application.Id,
                        CompanyId = company?.Id,
                        CountryId = officer.CountryId,
                        Type = PersonType.Officer
                    });
                }
            }
            if (model.SelectedMineralIds != null)
            {
                foreach (var mineralId in model.SelectedMineralIds)
                {
                    application.Minerals.Add(new ApplicationMineral
                    {
                        ApplicationId = application.Id,
                        MineralId = mineralId
                    });
                }
            }

            var previousApplicationStatus = await _statusService.Get(model.PreviousApplicationStatusCode, StatusType.ApplicionStatus);
            if (previousApplicationStatus == null)
                return new ResponseModel { Success = false, Message = "status is not found in the system." };
            application.Exploration.PreviousApplicationStatusId = previousApplicationStatus.Id;

            var previousOperationStatus = await _statusService.Get(model.PreviousOperationStatusCode, StatusType.OperationStatus);
            if (previousOperationStatus == null)
                return new ResponseModel { Success = false, Message = "status is not found in the system." };
            application.Exploration.PreviousOperationStatusId = previousOperationStatus.Id;

            var response = await dbContext.Applications.AddAsync(application);
            await dbContext.SaveChangesAsync();
            var result = mapper.Map<ApplicationModel>(application);
            return new ResponseModel { Success = true, Message = "Added successfully.", Data = result };
        }

        public async Task<MineralDepositRetentionModel> LoadMineralDepositRetentionApplication(string userId, MineralDepositRetentionModel model = null)
        {
            var countries = await _countryServie.Get();
            var applicionStatuses = await _statusService.Get(StatusType.ApplicionStatus);
            var operationStatuses = await _statusService.Get(StatusType.OperationStatus);
            var mierals = await _mineralService.Get();
            var previousMinerals = await GetAppliedMinerals(userId);

            if (model == null)
                model = new MineralDepositRetentionModel();

            model.PreviousAppliedMinerals = previousMinerals;

            foreach (var country in countries)
                model.Countries.Add(new SelectListItem { Value = country.Id.ToString(), Text = country.Nationality });

            foreach (var status in applicionStatuses)
                model.ApplicionStatuses.Add(new SelectListItem { Value = status.Code, Text = status.Name });

            foreach (var status in operationStatuses)
                model.OperationStatuses.Add(new SelectListItem { Value = status.Code, Text = status.Name });

            foreach (var mieral in mierals)
                model.Minerals.Add(new SelectListItem { Value = mieral.Id.ToString(), Text = mieral.Name });

            return model;
        }

        public async Task<ResponseModel> SubmitMineralDepositRetentionApplication(MineralDepositRetentionModel model, string userId)
        {
            var status = await _statusService.Get(ApplicationStausCode.None, StatusType.ApplicionStatus);
            if (status == null)
                return new ResponseModel { Success = false, Message = "status is not found in the system." };

            CompanyModel company = null;
            if (model.IsCompany)
            {
                company = await _companyService.Add(new CompanyModel
                {
                    Name = model.ApplicantName
                });
            }

            var entity = mapper.Map<MineralDepositRetention>(model);
            entity.PreviousApplicionStatus = null;
            entity.PreviousOperationStatus = null;
            var application = new Application
            {
                ApplicationNumber = "MineralDepositRetention-001",
                Type = !model.IsCompany ? ApplicationType.Form_D_MineralDepositRetentionLicenceByIndividual : ApplicationType.Form_E_MineralDepositRetentionLicenceByCompany,
                ScaleType = ScaleType.LargeScaleMining,
                CreatedAt = DateTime.UtcNow,
                StatusId = status.Id,
                UserId = userId,
                MineralDepositRetention = entity
            };
            if (model.CompanyControllers != null && model.CompanyControllers.Any())
            {
                if (company == null)
                    company = await _companyService.Add(new CompanyModel
                    {
                        Name = model.ApplicantName
                    });

                foreach (var controller in model.CompanyControllers)
                {
                    application.Persons.Add(new Person
                    {
                        FirstName = controller.FirstName,
                        LastName = controller.LastName,
                        Address = controller.Address,
                        ApplicationId = application.Id,
                        CompanyId = company?.Id,
                        CountryId = controller.CountryId,
                        Type = PersonType.Controller
                    });
                }
            }
            if (model.Directors != null && model.Directors.Any())
            {
                if (company == null)
                    company = await _companyService.Add(new CompanyModel
                    {
                        Name = model.ApplicantName
                    });

                foreach (var director in model.Directors)
                {
                    application.Persons.Add(new Person
                    {
                        FirstName = director.FirstName,
                        LastName = director.LastName,
                        Address = director.Address,
                        ApplicationId = application.Id,
                        CompanyId = company?.Id,
                        CountryId = director.CountryId,
                        Type = PersonType.Director
                    });
                }
            }
            if (model.Officers != null && model.Officers.Any())
            {
                if (company == null)
                    company = await _companyService.Add(new CompanyModel
                    {
                        Name = model.ApplicantName
                    });

                foreach (var officer in model.Officers)
                {
                    application.Persons.Add(new Person
                    {
                        FirstName = officer.FirstName,
                        LastName = officer.LastName,
                        Address = officer.Address,
                        ApplicationId = application.Id,
                        CompanyId = company?.Id,
                        CountryId = officer.CountryId,
                        Type = PersonType.Officer
                    });
                }
            }
            if (model.SelectedMineralIds != null)
            {
                foreach (var mineralId in model.SelectedMineralIds)
                {
                    application.Minerals.Add(new ApplicationMineral
                    {
                        ApplicationId = application.Id,
                        MineralId = mineralId
                    });
                }
            }

            var previousApplicationStatus = await _statusService.Get(model.PreviousApplicationStatusCode, StatusType.ApplicionStatus);
            if (previousApplicationStatus == null)
                return new ResponseModel { Success = false, Message = "status is not found in the system." };
            application.MineralDepositRetention.PreviousApplicationStatusId = previousApplicationStatus.Id;

            var previousOperationStatus = await _statusService.Get(model.PreviousOperationStatusCode, StatusType.OperationStatus);
            if (previousOperationStatus == null)
                return new ResponseModel { Success = false, Message = "status is not found in the system." };
            application.MineralDepositRetention.PreviousOperationStatusId = previousOperationStatus.Id;

            var response = await dbContext.Applications.AddAsync(application);
            await dbContext.SaveChangesAsync();
            var result = mapper.Map<ApplicationModel>(application);
            return new ResponseModel { Success = true, Message = "Added successfully.", Data = result };
        }

        public async Task<LargeMiningLeaseModel> LoadLargeMiningLeaseApplication(string userId, LargeMiningLeaseModel model = null)
        {
            var countries = await _countryServie.Get();
            var applicionStatuses = await _statusService.Get(StatusType.ApplicionStatus);
            var operationStatuses = await _statusService.Get(StatusType.OperationStatus);
            var mierals = await _mineralService.Get();
            var previousMinerals = await GetAppliedMinerals(userId);

            if (model == null)
                model = new LargeMiningLeaseModel();

            model.PreviousAppliedMinerals = previousMinerals;

            foreach (var country in countries)
                model.Countries.Add(new SelectListItem { Value = country.Id.ToString(), Text = country.Nationality });

            foreach (var status in applicionStatuses)
                model.ApplicionStatuses.Add(new SelectListItem { Value = status.Code, Text = status.Name });

            foreach (var status in operationStatuses)
                model.OperationStatuses.Add(new SelectListItem { Value = status.Code, Text = status.Name });

            foreach (var mieral in mierals)
                model.Minerals.Add(new SelectListItem { Value = mieral.Id.ToString(), Text = mieral.Name });

            return model;
        }

        public async Task<ResponseModel> SubmitLargeMiningLeaseApplication(LargeMiningLeaseModel model, string userId)
        {
            var status = await _statusService.Get(ApplicationStausCode.None, StatusType.ApplicionStatus);
            if (status == null)
                return new ResponseModel { Success = false, Message = "status is not found in the system." };

            CompanyModel company = null;
            if (model.HasShareCapital)
            {
                company = await _companyService.Add(new CompanyModel
                {
                    Name = model.ApplicantName
                });
            }

            var entity = mapper.Map<LargeMiningLease>(model);
            entity.PreviousApplicionStatus = null;
            entity.PreviousOperationStatus = null;
            var application = new Application
            {
                ApplicationNumber = "LargeMiningLease-001",
                Type = ApplicationType.Form_F_MiningLeaseByCompany,
                ScaleType = ScaleType.LargeScaleMining,
                CreatedAt = DateTime.UtcNow,
                StatusId = status.Id,
                UserId = userId,
                LargeMiningLease = entity
            };
            if (model.CompanyControllers != null && model.CompanyControllers.Any())
            {
                if (company == null)
                    company = await _companyService.Add(new CompanyModel
                    {
                        Name = model.ApplicantName
                    });

                foreach (var controller in model.CompanyControllers)
                {
                    application.Persons.Add(new Person
                    {
                        FirstName = controller.FirstName,
                        LastName = controller.LastName,
                        Address = controller.Address,
                        ApplicationId = application.Id,
                        CompanyId = company?.Id,
                        CountryId = controller.CountryId,
                        Type = PersonType.Controller
                    });
                }
            }
            if (model.Directors != null && model.Directors.Any())
            {
                if (company == null)
                    company = await _companyService.Add(new CompanyModel
                    {
                        Name = model.ApplicantName
                    });

                foreach (var director in model.Directors)
                {
                    application.Persons.Add(new Person
                    {
                        FirstName = director.FirstName,
                        LastName = director.LastName,
                        Address = director.Address,
                        ApplicationId = application.Id,
                        CompanyId = company?.Id,
                        CountryId = director.CountryId,
                        Type = PersonType.Director
                    });
                }
            }
            if (model.Officers != null && model.Officers.Any())
            {
                if (company == null)
                    company = await _companyService.Add(new CompanyModel
                    {
                        Name = model.ApplicantName
                    });

                foreach (var officer in model.Officers)
                {
                    application.Persons.Add(new Person
                    {
                        FirstName = officer.FirstName,
                        LastName = officer.LastName,
                        Address = officer.Address,
                        ApplicationId = application.Id,
                        CompanyId = company?.Id,
                        CountryId = officer.CountryId,
                        Type = PersonType.Officer
                    });
                }
            }
            if (model.SelectedMineralIds != null)
            {
                foreach (var mineralId in model.SelectedMineralIds)
                {
                    application.Minerals.Add(new ApplicationMineral
                    {
                        ApplicationId = application.Id,
                        MineralId = mineralId
                    });
                }
            }

            var previousApplicationStatus = await _statusService.Get(model.PreviousApplicationStatusCode, StatusType.ApplicionStatus);
            if (previousApplicationStatus == null)
                return new ResponseModel { Success = false, Message = "status is not found in the system." };
            application.LargeMiningLease.PreviousApplicationStatusId = previousApplicationStatus.Id;

            var previousOperationStatus = await _statusService.Get(model.PreviousOperationStatusCode, StatusType.OperationStatus);
            if (previousOperationStatus == null)
                return new ResponseModel { Success = false, Message = "status is not found in the system." };
            application.LargeMiningLease.PreviousOperationStatusId = previousOperationStatus.Id;

            var response = await dbContext.Applications.AddAsync(application);
            await dbContext.SaveChangesAsync();
            var result = mapper.Map<ApplicationModel>(application);
            return new ResponseModel { Success = true, Message = "Added successfully.", Data = result };
        }

        public async Task<ProspectingModel> LoadProspectingApplication(string userId, ProspectingModel model = null)
        {
            var countries = await _countryServie.Get();
            var mierals = await _mineralService.Get();
            var applicantTypes = Enum.GetValues(typeof(ApplicantType));
            var personTypes = Enum.GetValues(typeof(PersonType));

            if (model == null)
                model = new ProspectingModel();

            foreach (var country in countries)
                model.Countries.Add(new SelectListItem { Value = country.Id.ToString(), Text = country.Nationality });

            foreach (var mieral in mierals)
                model.Minerals.Add(new SelectListItem { Value = mieral.Id.ToString(), Text = mieral.Name });

            model.ApplicantTypes.Add(new SelectListItem { Value = "", Text = "Select Application type" });
            foreach (var applicantType in applicantTypes)
                model.ApplicantTypes.Add(new SelectListItem { Value = applicantType.ToString(), Text = applicantType.ToString() });

            foreach (var personType in personTypes)
                model.PersonTypes.Add(new SelectListItem { Value = personType.ToString(), Text = personType.ToString() });

            return model;
        }

        public async Task<ResponseModel> SubmitProspectingApplication(ProspectingModel model, string userId)
        {
            var status = await _statusService.Get(ApplicationStausCode.None, StatusType.ApplicionStatus);
            if (status == null)
                return new ResponseModel { Success = false, Message = "status is not found in the system." };

            CompanyModel company = null;
            if (model.ApplicantType.Equals(ApplicantType.Company))
            {
                company = await _companyService.Add(new CompanyModel
                {
                    Name = model.ApplicantName
                });
            }

            var entity = mapper.Map<Prospecting>(model);
            var application = new Application
            {
                ApplicationNumber = "Prospecting-001",
                Type = ApplicationType.Form_G_ProspectingLicence,
                ScaleType = ScaleType.SmallScaleMining,
                CreatedAt = DateTime.UtcNow,
                StatusId = status.Id,
                UserId = userId,
                Prospecting = entity
            };
            if (model.ApplicantDetails != null && model.ApplicantDetails.Any())
            {
                foreach (var person in model.ApplicantDetails)
                {
                    application.Persons.Add(new Person
                    {
                        FirstName = person.FirstName,
                        LastName = person.LastName,
                        FatherName = person.FatherName,
                        Address = person.Address,
                        NicNumber = person.NicNumber,
                        NicIssueDate = person.NicIssueDate,
                        NicIssuePlace = person.NicIssuePlace,
                        IsLocalDomicile = person.IsLocalDomicile,
                        NameOfTribe = person.NameOfTribe,
                        NameOfDistrict = person.NameOfDistrict,
                        Occupation = person.Occupation,
                        PlaceOfBusiness = person.PlaceOfBusiness,
                        SharesPercentage = person.SharesPercentage,
                        ApplicationId = application.Id,
                        CompanyId = company?.Id,
                        CountryId = person.CountryId,
                        Type = person.Type
                    });
                }
            }
            if (model.SelectedMineralIds != null)
            {
                foreach (var mineralId in model.SelectedMineralIds)
                {
                    application.Minerals.Add(new ApplicationMineral
                    {
                        ApplicationId = application.Id,
                        MineralId = mineralId
                    });
                }
            }

            var response = await dbContext.Applications.AddAsync(application);
            await dbContext.SaveChangesAsync();
            var result = mapper.Map<ApplicationModel>(application);
            return new ResponseModel { Success = true, Message = "Added successfully.", Data = result };
        }

        public async Task<SmallMiningLeaseModel> LoadSmallMiningLeaseApplication(string userId, SmallMiningLeaseModel model = null)
        {
            var countries = await _countryServie.Get();
            var mierals = await _mineralService.Get();
            var applicantTypes = Enum.GetValues(typeof(ApplicantType));
            var personTypes = Enum.GetValues(typeof(PersonType));

            if (model == null)
                model = new SmallMiningLeaseModel();

            foreach (var country in countries)
                model.Countries.Add(new SelectListItem { Value = country.Id.ToString(), Text = country.Nationality });

            foreach (var mieral in mierals)
                model.Minerals.Add(new SelectListItem { Value = mieral.Id.ToString(), Text = mieral.Name });

            model.ApplicantTypes.Add(new SelectListItem { Value = "", Text = "Select Application type" });
            foreach (var applicantType in applicantTypes)
                model.ApplicantTypes.Add(new SelectListItem { Value = applicantType.ToString(), Text = applicantType.ToString() });

            foreach (var personType in personTypes)
                model.PersonTypes.Add(new SelectListItem { Value = personType.ToString(), Text = personType.ToString() });

            return model;
        }

        public async Task<ResponseModel> SubmitSmallMiningLeaseApplication(SmallMiningLeaseModel model, string userId)
        {
            var status = await _statusService.Get(ApplicationStausCode.None, StatusType.ApplicionStatus);
            if (status == null)
                return new ResponseModel { Success = false, Message = "status is not found in the system." };

            CompanyModel company = null;
            if (model.ApplicantType.Equals(ApplicantType.Company))
            {
                company = await _companyService.Add(new CompanyModel
                {
                    Name = model.ApplicantName
                });
            }

            var entity = mapper.Map<SmallMiningLease>(model);
            var application = new Application
            {
                ApplicationNumber = "SmallMiningLease-001",
                Type = ApplicationType.Form_H_MiningLease,
                ScaleType = ScaleType.SmallScaleMining,
                CreatedAt = DateTime.UtcNow,
                StatusId = status.Id,
                UserId = userId,
                SmallMiningLease = entity
            };
            if (model.ApplicantDetails != null && model.ApplicantDetails.Any())
            {
                foreach (var person in model.ApplicantDetails)
                {
                    application.Persons.Add(new Person
                    {
                        FirstName = person.FirstName,
                        LastName = person.LastName,
                        FatherName = person.FatherName,
                        Address = person.Address,
                        NicNumber = person.NicNumber,
                        NicIssueDate = person.NicIssueDate,
                        NicIssuePlace = person.NicIssuePlace,
                        IsLocalDomicile = person.IsLocalDomicile,
                        NameOfTribe = person.NameOfTribe,
                        NameOfDistrict = person.NameOfDistrict,
                        Occupation = person.Occupation,
                        PlaceOfBusiness = person.PlaceOfBusiness,
                        SharesPercentage = person.SharesPercentage,
                        ApplicationId = application.Id,
                        CompanyId = company?.Id,
                        CountryId = person.CountryId,
                        Type = person.Type
                    });
                }
            }
            if (model.SelectedMineralIds != null)
            {
                foreach (var mineralId in model.SelectedMineralIds)
                {
                    application.Minerals.Add(new ApplicationMineral
                    {
                        ApplicationId = application.Id,
                        MineralId = mineralId
                    });
                }
            }

            var response = await dbContext.Applications.AddAsync(application);
            await dbContext.SaveChangesAsync();
            var result = mapper.Map<ApplicationModel>(application);
            return new ResponseModel { Success = true, Message = "Added successfully.", Data = result };
        }

        public async Task<LandSurrenderModel> LoadLandSurrenderApplication(string userId, LandSurrenderModel model = null)
        {
            var mierals = await _mineralService.Get();

            if (model == null)
                model = new LandSurrenderModel();

            foreach (var mieral in mierals)
                model.Minerals.Add(new SelectListItem { Value = mieral.Id.ToString(), Text = mieral.Name });

            return model;
        }

        public async Task<ResponseModel> SubmitLandSurrenderApplication(LandSurrenderModel model, string userId)
        {
            var status = await _statusService.Get(ApplicationStausCode.None, StatusType.ApplicionStatus);
            if (status == null)
                return new ResponseModel { Success = false, Message = "status is not found in the system." };

            var landSurrenderAndTransferModel = mapper.Map<LandSurrenderAndTransferModel>(model);
            var entity = mapper.Map<LandSurrenderAndTransfer>(landSurrenderAndTransferModel);
            var application = new Application
            {
                ApplicationNumber = "LandSurrender-001",
                Type = ApplicationType.Form_J_MineralTitleOrConcessionApplicationForSurrenderOfLand,
                ScaleType = ScaleType.Other,
                CreatedAt = DateTime.UtcNow,
                StatusId = status.Id,
                UserId = userId,
                LandSurrenderAndTransfer = entity
            };
            if (model.SelectedMineralIds != null)
            {
                foreach (var mineralId in model.SelectedMineralIds)
                {
                    application.Minerals.Add(new ApplicationMineral
                    {
                        ApplicationId = application.Id,
                        MineralId = mineralId
                    });
                }
            }

            var response = await dbContext.Applications.AddAsync(application);
            await dbContext.SaveChangesAsync();
            var result = mapper.Map<ApplicationModel>(application);
            return new ResponseModel { Success = true, Message = "Added successfully.", Data = result };
        }

        public async Task<LandTransferModel> LoadLandTransferApplication(string userId, LandTransferModel model = null)
        {
            var applicionStatuses = await _statusService.Get(StatusType.ApplicionStatus);
            var operationStatuses = await _statusService.Get(StatusType.OperationStatus);
            var mierals = await _mineralService.Get();
            var previousMinerals = await GetAppliedMinerals(userId);

            if (model == null)
                model = new LandTransferModel();

            model.PreviousAppliedMinerals = previousMinerals;

            foreach (var status in applicionStatuses)
                model.ApplicionStatuses.Add(new SelectListItem { Value = status.Code, Text = status.Name });

            foreach (var status in operationStatuses)
                model.OperationStatuses.Add(new SelectListItem { Value = status.Code, Text = status.Name });

            foreach (var mieral in mierals)
                model.Minerals.Add(new SelectListItem { Value = mieral.Id.ToString(), Text = mieral.Name });

            return model;
        }

        public async Task<ResponseModel> SubmitLandTransferApplication(LandTransferModel model, string userId)
        {
            var status = await _statusService.Get(ApplicationStausCode.None, StatusType.ApplicionStatus);
            if (status == null)
                return new ResponseModel { Success = false, Message = "status is not found in the system." };

            var landSurrenderAndTransferModel = mapper.Map<LandSurrenderAndTransferModel>(model);
            var entity = mapper.Map<LandSurrenderAndTransfer>(landSurrenderAndTransferModel);
            entity.PreviousApplicionStatus = null;
            entity.PreviousOperationStatus = null;
            var application = new Application
            {
                ApplicationNumber = "LandTransfer-001",
                Type = ApplicationType.Form_K_MineralTitleOrMiningLeaseApplicationForApprovalOfAssignmentOrTransfer,
                ScaleType = ScaleType.Other,
                CreatedAt = DateTime.UtcNow,
                StatusId = status.Id,
                UserId = userId,
                LandSurrenderAndTransfer = entity
            };
            if (model.SelectedMineralIds != null)
            {
                foreach (var mineralId in model.SelectedMineralIds)
                {
                    application.Minerals.Add(new ApplicationMineral
                    {
                        ApplicationId = application.Id,
                        MineralId = mineralId
                    });
                }
            }

            var previousApplicationStatus = await _statusService.Get(model.PreviousApplicationStatusCode, StatusType.ApplicionStatus);
            if (previousApplicationStatus == null)
                return new ResponseModel { Success = false, Message = "status is not found in the system." };
            application.LandSurrenderAndTransfer.PreviousApplicationStatusId = previousApplicationStatus.Id;

            var previousOperationStatus = await _statusService.Get(model.PreviousOperationStatusCode, StatusType.OperationStatus);
            if (previousOperationStatus == null)
                return new ResponseModel { Success = false, Message = "status is not found in the system." };
            application.LandSurrenderAndTransfer.PreviousOperationStatusId = previousOperationStatus.Id;

            var response = await dbContext.Applications.AddAsync(application);
            await dbContext.SaveChangesAsync();
            var result = mapper.Map<ApplicationModel>(application);
            return new ResponseModel { Success = true, Message = "Added successfully.", Data = result };
        }
    }
}
