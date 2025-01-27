using AutoMapper;
using DataAccess;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Presentation.Models;
using Service.ServicesContract;
using System.Web;

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
            return View();
        }

        [Route("/MeetingRoom/GetMeetingRoomJsonDataAsync")]
        [HttpPost]
        public async Task<JsonResult> GetMeetingRoomJsonDataAsync([FromBody] MeetingRoomListModel model)
        {
            var result = await _meetingRoomManagementService.GetMeetingRoomsAsync(model.PageIndex, model.PageSize, model.Search,
                model.FormatSortExpression("Id", "Name", "Description"));

            var meetingRoomJsonData = new
            {
                recordsTotal = result.total,
                recordsFiltered = result.totalDisplay,
                data = (from record in result.data
                        select new string[]
                        {
                                $"<img src='{"/" + HttpUtility.HtmlDecode(record.ImageUrl ?? string.Empty)}' alt='Image' width='100' height='70'/>",
                                HttpUtility.HtmlEncode(record.Name),
                                HttpUtility.HtmlDecode(record.Facilities),
                                HttpUtility.HtmlEncode(record.Capacity),
                                HttpUtility.HtmlEncode(record.Color),
                                HttpUtility.HtmlEncode(record.QRCodeData),
                                HttpUtility.HtmlEncode(record.Status),
                                record.Id.ToString()
                        }
                    ).ToArray()
            };

            return Json(meetingRoomJsonData);
        }



        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> MeetingRoomAddAsync(MeetingRoomCreateModel model)
        {
            if (!ModelState.IsValid)
            {
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
                    Message = "The meeting room has been created successfuly!",
                    Type = ResponseTypes.Success
                });

                return RedirectToAction("Index", "MeetingRoom");

            }

            catch (Exception ex)
            {
                TempData.Put("ResponseMessage", new ResponseModel
                {
                    Message = "The meeting room creation has failed!",
                    Type = ResponseTypes.Danger
                });
                _logger.LogError(ex, "The meeting room create failed!");
            }
            return View(model);
        }

        public async Task<IActionResult> MeetingRoomByIdAsync(Guid id)
        {
            var meetingRoom = await _meetingRoomManagementService.GetMeetingRoomByIdAsync(id);

            if (meetingRoom == null)
            {
                return Json(new { success = false, message = "Meeting room not found." });
            }

            return Json(new { success = true, data = meetingRoom });
        }

        [HttpPost, AutoValidateAntiforgeryToken]
        public async Task<IActionResult> UpdateMeetingRoom(MeetingRoomUpdateModel model)
        {
            if (ModelState.IsValid)
            {
                var mettingRoom = await _meetingRoomManagementService.GetMeetingRoomByIdAsync(model.Id);
                model.ImageUrl = await SaveProductImage(model.ImageFile, mettingRoom.ImageUrl);

                mettingRoom = _mapper.Map(model, mettingRoom);

                try
                {
                    await _meetingRoomManagementService.UpdateMeetingRoomAsync(mettingRoom);

                    TempData.Put("ResponseMessage", new ResponseModel
                    {
                        Message = "The metting room has been update successfuly",
                        Type = ResponseTypes.Success
                    });

                    return RedirectToAction("Index", "MeetingRoom");
                }
                catch (Exception ex)
                {
                    TempData.Put("ResponseMessage", new ResponseModel
                    {
                        Message = "The meeting room update has failed!",
                        Type = ResponseTypes.Danger
                    });
                    _logger.LogError(ex, "Ultimatly the meeting room update failed!");
                }
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
