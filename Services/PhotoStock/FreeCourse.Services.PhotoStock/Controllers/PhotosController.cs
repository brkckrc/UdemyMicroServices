using FreeCourse.Services.PhotoStock.Dtos;
using FreeCourse.Shared.ControllerBases;
using FreeCourse.Shared.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace FreeCourse.Services.PhotoStock.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhotosController : CustomBaseController //Kendi base controllerimizi ekledik.
    {
        [HttpPost]
        public async Task<IActionResult> PhotoSave(IFormFile photo, CancellationToken cancellationToken) //cancellation token: diyelim ki 20 sn sürüyor. işlem sonlanıyorsa foto kaydı da gerçekleştirmesin. örnek tarayıcıyı kapattı işlem devam etmesin. otomatik olarak devreye girecek. asyn işlemi sonlandıracak.
        {
            if (photo!=null && photo.Length>0)
            {
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/photos", photo.FileName);
                using var stream = new FileStream(path, FileMode.Create);
                await photo.CopyToAsync(stream, cancellationToken);//kullanıcı tarayıcıyı kapatır ya da istek kesilirse cancellationToken sayesinde işlem devam etmeyecek.
                
                //http://wwww.photostock.api.com/photos/asfg.jpg şeklinde gelecek
                var returnPath = "photos/" + photo.FileName;

                PhotoDto photoDto = new() { Url = returnPath };
                return CreateActionResultInstance(Response<PhotoDto>.Success(photoDto, 200));
            }
            return CreateActionResultInstance(Response<PhotoDto>.Fail("Photo is Empty", 400));
        }

        //[HttpPost]
        public IActionResult PhotoDelete(string photoUrl)
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/photos", photoUrl);
            if (!System.IO.File.Exists(path))
            {
                return CreateActionResultInstance(Response<NoContent>.Fail("Photo not Found", 404));
            }
            System.IO.File.Delete(path);
            return CreateActionResultInstance(Response<NoContent>.Success(204));
        }

    }
}
