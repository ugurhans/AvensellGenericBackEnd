using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Results
{
	public class ErrorResult:Result
	{
		//base -> miras aldıgı sınıf -> Result, önce bu method calisir ve aldıgı parametreyi base'e yollar
		public ErrorResult(string message) : base(false, message)
		{

		}
		public ErrorResult() : base(false)
		{

		}
	}

}
