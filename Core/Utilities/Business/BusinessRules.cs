using Core.Utilities.Results;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Business
{
	public class BusinessRules
	{
		//params => birden fazla parametre verebilirsin.
		//gönderilen paramterleri bir dizi haline getiriyor
		public static IResult Run(params IResult[] logics)
		{
			//hher bir is kuralini gez
			foreach (var logic in logics)
			{ //gönderilen iskurallarından basarısız olan businessa döner.
				if (!logic.Success)
				{
					return logic;
				}
			}
			return null;
		}
	}
}
