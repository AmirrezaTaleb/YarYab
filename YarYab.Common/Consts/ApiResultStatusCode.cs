using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YarYab.Common
{
    public enum ApiResultStatusCode
    {
        [Display(Name = "عملیات با موفقیت انجام شد")]
        Success = 200,

        [Display(Name = "خطایی در سرور رخ داده است")]
        ServerError = 500,

        [Display(Name = "پارامتر های ارسالی معتبر نیستند")]
        BadRequest = 400,

        [Display(Name = "یافت نشد")]
        NotFound = 404,

        [Display(Name = "خطای احراز هویت")]
        UnAuthorized = 401,

        [Display(Name = "خطایی در پردازش رخ داد")]
        LogicError = 5,
        //[Display(Name = "لیست خالی است")]
        //ListEmpty = 4,

        //[Display(Name = "خطایی در پردازش رخ داد")]
        //LogicError = 5,

    }
}
