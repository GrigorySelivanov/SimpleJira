using Shared.Constants;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DTOs.UserDTOs
{
    public class LoginRequestDTO
    {
        /// <summary>
        /// Почта пользователя (обяз).
        /// </summary>
        [Required(ErrorMessage = WebConstants.RequiredField)]
        public string Login { get; set; } = null!;

        /// <summary>
        /// Пароль пользователя (обяз).
        /// </summary>
        [Required(ErrorMessage = WebConstants.RequiredField)]
        public string Password { get; set; } = null!;
    }
}
