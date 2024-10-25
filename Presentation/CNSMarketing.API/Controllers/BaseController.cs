using CNSMarketing.Domain.Entity.Common;
using CNSMarketing.Infrastructure.Enums;
using CNSMarketing.Service.Abstraction.Token;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CNSMarketing.API.Controllers
{
    
    public class BaseController : ControllerBase
    {
        private readonly ITokenHandler _tokenHandler;

        public TokenControl tokenControl = null;


        public BaseController(TokenControl tokenInfo)
        {
            this.tokenControl = tokenInfo;
        }

        public BaseController(ITokenHandler tokenHandler)
        {
            _tokenHandler = tokenHandler;
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        public ResultModel<T> GenericResponse<T>(T data)
        {
            var result = new ResultModel<T>
            {
                Data = data,
                ResultCode = GetResultCode(data),
                ResultMessage = GetResultMessage(data)
            };

            return result;
        }

        private int GetResultCode<T>(T data)
        {
            if (data == null)
                return -1;

            // Eğer data bir string ise, "success" kontrolü
            if (data is string strData)
                return strData.Equals("success", StringComparison.OrdinalIgnoreCase) ? 0 : -1;

            // Eğer data bir bool ise
            if (data is bool boolData)
                return boolData ? 0 : -1;

            return 0; // Diğer durumlar için başarılı varsayıyoruz
        }

        private string GetResultMessage<T>(T data)
        {
            if (data == null)
                return "fail";

            if (data is string strData)
                return strData.Equals("success", StringComparison.OrdinalIgnoreCase) ? "success" : "fail";

            if (data is bool boolData)
                return boolData ? "success" : "fail";

            return "success"; // Diğer durumlar için başarılı varsayıyoruz
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        public ResultModel<bool> GetInvalidTokenResponse()
        {
            return new ResultModel<bool>
            {
                ResultCode = -1,
                ResultMessage = "InvalidToken",
                Data = false
            };
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task<TokenResult> TokenControl(HttpContext context)
        {
            // Authorization header'dan Bearer token'ı al
            var accessToken = context.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            // Token'ı doğrula ve kullanıcı bilgilerini al
             tokenControl = await _tokenHandler.GetUserFromTokenAsync(accessToken);

            // Token geçersizse
            if (tokenControl == null)
            {
                return TokenResult.Invalid; // Geçersiz token
            }

            // Kullanıcının token'ının süresini kontrol et
            if (tokenControl.ExpireDate < DateTime.UtcNow)
            {
                return TokenResult.Expired; // Token süresi dolmuş
            }

            // Token geçerli ve süresi dolmamışsa, ikinci adıma geç
            return TokenResult.Ok; // Token geçerli
        }


    }
}
