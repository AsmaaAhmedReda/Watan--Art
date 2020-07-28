using WatanART.BLL.BussinessLayer;
using WatanART.BLL.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WatanART.Controllers.WebApi
{
    public class AlbumGalleryAPIController : ApiController
    {
        AlbumsBLL _AlbumsBLL = new AlbumsBLL();
        AlbumGalleryBLL _AlbumGalleryBLL = new AlbumGalleryBLL();
        Generic _Generic = new Generic();

        //{
        // Language:0
        //}
        [HttpPost]
        [Route("api/AlbumGalleryAPI/GetAllAlbums")]
        public HttpResponseMessage GetAllAlbums([FromBody]BasicVM obj)
        {
            List<AlbumVM> AlbumLST = _AlbumsBLL.SelectALLActive(obj.Language);
            return _Generic.ConvertObjectToJSON(AlbumLST);
        }

        //{
        // AlbumID:1,
        // Language:0
        //}
        [HttpPost]
        [Route("api/AlbumGalleryAPI/GetAllGalleryByAlbumID")]
        public HttpResponseMessage GetAllGalleryByAlbumID([FromBody]AlbumVM obj)
        {
            List<AlbumGalleryVM> AlbumGalleryImageLST = _AlbumGalleryBLL.SelectALL(obj.AlbumID, MediaTypeEnum.Image, obj.Language);
            List<AlbumGalleryVM> AlbumGalleryVideoLST = _AlbumGalleryBLL.SelectALL(obj.AlbumID, MediaTypeEnum.Video, obj.Language);
            return _Generic.ConvertObjectToJSON(AlbumGalleryImageLST, AlbumGalleryVideoLST);
        }

        //{
        // AlbumID:1,
        // Language:0
        //}
        [HttpPost]
        [Route("api/AlbumGalleryAPI/GetAllGalleryImagesByAlbumID")]
        public HttpResponseMessage GetAllGalleryImagesByAlbumID([FromBody]AlbumVM obj)
        {
            List<AlbumGalleryVM> AlbumGalleryImageLST = _AlbumGalleryBLL.SelectALL(obj.AlbumID, MediaTypeEnum.Image, obj.Language);
            return _Generic.ConvertObjectToJSON(AlbumGalleryImageLST);
        }

        //{
        // AlbumID:1,
        // Language:0
        //}
        [HttpPost]
        [Route("api/AlbumGalleryAPI/GetAllGalleryVideosByAlbumID")]
        public HttpResponseMessage GetAllGalleryVideosByAlbumID([FromBody]AlbumVM obj)
        {
            List<AlbumGalleryVM> AlbumGalleryVideoLST = _AlbumGalleryBLL.SelectALL(obj.AlbumID, MediaTypeEnum.Video, obj.Language);
            return _Generic.ConvertObjectToJSON(AlbumGalleryVideoLST);
        }

        //{
        // Language:0
        //}
        [HttpPost]
        [Route("api/AlbumGalleryAPI/GetAllGallery")]
        public HttpResponseMessage GetAllGallery([FromBody]BasicVM obj)
        {
            List<AlbumGalleryVM> AlbumGalleryImageLST = _AlbumGalleryBLL.SelectALL(MediaTypeEnum.Image, obj.Language);
            List<AlbumGalleryVM> AlbumGalleryVideoLST = _AlbumGalleryBLL.SelectALL(MediaTypeEnum.Video, obj.Language);
            return _Generic.ConvertObjectToJSON(AlbumGalleryImageLST, AlbumGalleryVideoLST);
        }

        //{
        // Language:0
        //}
        [HttpPost]
        [Route("api/AlbumGalleryAPI/GetAllGalleryImages")]
        public HttpResponseMessage GetAllGalleryImages([FromBody]BasicVM obj)
        {
            List<AlbumGalleryVM> AlbumGalleryImageLST = _AlbumGalleryBLL.SelectALL(MediaTypeEnum.Image, obj.Language);
            return _Generic.ConvertObjectToJSON(AlbumGalleryImageLST);
        }

        //{
        // Language:0
        //}
        [HttpPost]
        [Route("api/AlbumGalleryAPI/GetAllGalleryVideos")]
        public HttpResponseMessage GetAllGalleryVideos([FromBody]BasicVM obj)
        {
            List<AlbumGalleryVM> AlbumGalleryVideoLST = _AlbumGalleryBLL.SelectALL(MediaTypeEnum.Video, obj.Language);
            return _Generic.ConvertObjectToJSON(AlbumGalleryVideoLST);
        }
    }
}
