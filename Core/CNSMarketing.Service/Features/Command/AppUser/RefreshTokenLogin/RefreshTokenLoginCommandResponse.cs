using CNSMarketing.Application.Models.DTOs;

namespace CNSMarketing.Application.Features.Command.AppUser.RefreshTokenLogin
{
    public class RefreshTokenLoginCommandResponse
    {
        public bool IsSuccess { get; set; } // İşlemin başarılı olup olmadığını belirler
        public Token? Token { get; set; }
        public string? Message { get; set; }

    }

    
}
