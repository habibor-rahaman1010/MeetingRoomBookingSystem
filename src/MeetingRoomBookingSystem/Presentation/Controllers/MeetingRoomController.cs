using AutoMapper;
using DataAccess;
using Domain.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Presentation.Models;
using Service.ServicesContract;

namespace Presentation.Controllers
{
    public class MeetingRoomController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly ILogger<MeetingRoomController> _logger;
        private readonly IMeetingRoomManagementService _meetingRoomManagementService;

        public MeetingRoomController(IMapper mapper,
            IWebHostEnvironment webHostEnvironment,
            IMeetingRoomManagementService meetingRoomManagementService,
            ILogger<MeetingRoomController> logger)
        {
            _mapper = mapper;
            _webHostEnvironment = webHostEnvironment;
            _meetingRoomManagementService = meetingRoomManagementService;
            _logger = logger;
        }

        public IActionResult Index()
        {
            var model = new MeetingRoomCreateModel();
            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> MeetingRoomAddAsync(MeetingRoomCreateModel model)
        {
            if (!ModelState.IsValid)
            {
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    _logger.LogError($"Validation error: {error.ErrorMessage}");
                }
                return BadRequest(ModelState);
            }

            try
            {
                var product = _mapper.Map<MeetingRoom>(model);

                if (model.ImageFile != null)
                {
                    string folder = "meetingRoom/images/";
                    folder += Guid.NewGuid().ToString() + "_" + model.ImageFile.FileName;
                    model.ImageUrl = folder;
                    string serverFolder = Path.Combine(_webHostEnvironment.WebRootPath, folder);

                    await model.ImageFile.CopyToAsync(new FileStream(serverFolder, FileMode.Create));
                    product.ImageUrl = model.ImageUrl;
                }

                await _meetingRoomManagementService.AddMeetingRoomAsync(product);

                TempData.Put("ResponseMessage", new ResponseModel
                {
                    Message = "The product has been created successfuly!",
                    Type = ResponseTypes.Success
                });

                return RedirectToAction("Index", "Dashboard");

            }

            catch (Exception ex)
            {
                TempData.Put("ResponseMessage", new ResponseModel
                {
                    Message = "The product creation has failed!",
                    Type = ResponseTypes.Danger
                });
                _logger.LogError(ex, "Ultimatly the product creation failed!");
            }
            return View(model);
        }

       
        private async Task<string> SaveProductImage(IFormFile productImageFile, string existingImagePath)
        {
            if (productImageFile == null)
            {
                return existingImagePath;
            }

            string folder = "product/images/";
            string folderPath = Path.Combine(_webHostEnvironment.WebRootPath, folder);

            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            string uniqueFileName = Guid.NewGuid().ToString() + "_" + productImageFile.FileName;
            string serverFolder = Path.Combine(folderPath, uniqueFileName);
            string newImagePath = Path.Combine(folder, uniqueFileName);

            using (var fileStream = new FileStream(serverFolder, FileMode.Create))
            {
                await productImageFile.CopyToAsync(fileStream);
            }

            if (!string.IsNullOrEmpty(existingImagePath))
            {
                var oldImagePath = Path.Combine(_webHostEnvironment.WebRootPath, existingImagePath);
                if (System.IO.File.Exists(oldImagePath))
                {
                    System.IO.File.Delete(oldImagePath);
                }
            }

            return newImagePath;
        }

    }
}
