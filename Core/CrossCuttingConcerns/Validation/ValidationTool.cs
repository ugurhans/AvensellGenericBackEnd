using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.CrossCuttingConcerns.Validation
{
	public static class ValidationTool
	{
		//tek bir instance olusturulur uygulama boyunca kullanılacagi icin bellekte tutulur
		//validateion classının referansnı tutar IValidator
		public static void Validate(IValidator validator,object entity)
		{//validasyondan gecip gecmedigi kontrolu.
			var context = new ValidationContext<object>(entity);
			var result = validator.Validate(context);
			if (!result.IsValid)
			{
				throw new ValidationException(result.Errors);
			}
		}
	}
}
