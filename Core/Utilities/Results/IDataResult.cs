using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Results
{
	public interface IDataResult<T>:IResult //hangi tip döndürülecek->Generic
	{
		T Data { get; }
	}
}
