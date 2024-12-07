using CNSMarketing.Domain.Entity.Common;
using CNSMarketing.Infrastructure.Enums;
using CNSMarketing.Application.Abstraction.Token;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CNSMarketing.API.Controllers
{
    
    public class BaseController : ControllerBase
    {
        private readonly ITokenHandler _tokenHandler;

        public TokenInfo tokenInfo = null;


        public BaseController(TokenInfo tokenInfo)
        {
            this.tokenInfo = tokenInfo;
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
            var accessToken = context.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            tokenInfo = await _tokenHandler.GetUserFromTokenAsync(accessToken);

            if (tokenInfo == null)
            {
                return TokenResult.Invalid; 
            }

            if (tokenInfo.ExpireDate < DateTime.UtcNow)
            {
                return TokenResult.Expired;
            }
           
            return TokenResult.Ok; 
        }


    }
}
