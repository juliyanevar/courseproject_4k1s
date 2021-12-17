using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using QRCoder;
using CourseProject.Models;
using CourseProject.Dto.QRDto;
using Microsoft.AspNetCore.Identity;
using CourseProject.Repository.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace CourseProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class QRController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private IRepositoryWrapper _repositoryWrapper;

        public QRController(UserManager<User> userManager, IRepositoryWrapper repositoryWrapper)
        {
            _userManager = userManager;
            _repositoryWrapper = repositoryWrapper;
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> Create([FromBody] CreateQRDto model)
        {
            var subject = await _repositoryWrapper.Subject.FindFirstByConditionAsync(x => x.Name.Equals(model.SubjectName));
            var date = DateTime.Now;
            var teacher = await _userManager.GetUserAsync(this.User);
            var strPathAndQuery = "https://"+HttpContext.Request.Host+"/ButtonAttendance?subjectId="+subject.Id+"&AuditoriumName="+model.AuditoriumName+"&Date="+date+"&teacher="+teacher.UserName;
            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(strPathAndQuery, QRCodeGenerator.ECCLevel.Q);
            QRCode qrCode = new QRCode(qrCodeData);
            Bitmap qrCodeImage = qrCode.GetGraphic(20);


            var bitmapBytes = BitmapToBytes(qrCodeImage); //Convert bitmap into a byte array
            //Return as file result 
            return File(bitmapBytes, "image/jpeg");
        }


        // This method is for converting bitmap into a byte array
        private static byte[] BitmapToBytes(Bitmap img)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                img.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
                return stream.ToArray();
            }
        }
    }
}
