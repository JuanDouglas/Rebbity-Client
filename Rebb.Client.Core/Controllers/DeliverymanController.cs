using Rebb.Client.Core.Controllers.Base;
using Rebb.Client.Core.Models.Enums;
using Rebb.Client.Core.Models.Result;
using Rebb.Client.Core.Models.Upload;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Rebb.Client.Core.Controllers
{
    public sealed class DeliverymanController : LoguedController
    {
        public async Task<DeliveryManResult> RegisterAsync(DeliveryManUpload deliveryManUpload)
        {
            Uri uri = new Uri(Host.AbsoluteUri + "/DeliveryMan/Register");
            return await SendJsonObject<DeliveryManResult>(deliveryManUpload, HttpMethod.Put, uri, true);
        }

        public async Task PostDocumentImage(DocumentType type, bool front, int deliveryman_id, byte[] imageBytes, string filename)
        {
            Uri uri = new Uri(Host.AbsoluteUri + $"/DeliveryMan/AddDocumentImage?type={type}&front={front}&deliveryman_id={deliveryman_id}");

            ByteArrayContent content = new ByteArrayContent(imageBytes);
            using MultipartFormDataContent multipartFormData = new MultipartFormDataContent
            {
                { content, "DocumentImage", filename }
            };

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Put, uri)
            {
                Content = multipartFormData
            };

            HttpResponseMessage response = await SendAsync(request);
        }
    }
}