using Castle.DynamicProxy;
using Core.CrossCuttingConcerns.Validation;
using Core.Utilities.Interceptors;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Aspects.Autofac.Validation
{
    //method interception
    //methodun basında sonunda hata verdiginde calisacak yapı. bunu ben belirlerim
    public class ValidationAspect : MethodInterception
    {
        private Type _validatorType;
        public ValidationAspect(Type validatorType)
        {
            //defensive coding
            if (!typeof(IValidator).IsAssignableFrom(validatorType))
            {
                throw new System.Exception("Bu bir doğrulama sınıfı değil");
            }

            _validatorType = validatorType;
        }
        protected override void OnBefore(IInvocation invocation) //invocationda method bilgileri var 
        {
            //burada productvalidator newleniyor.
            var validator = (IValidator)Activator.CreateInstance(_validatorType);//reflection
            //generic argüman yolladım,git onun tipini yakala
            var entityType = _validatorType.BaseType.GetGenericArguments()[0]; //reflection
            //girilen bilgiler entity bilgileriyle uyusuyor mu kontrolu sanirim
            //invocation_>method. methodun argumanlarını gez. oradaki tip product türüyse (yollanan türdeyse) validate et
            //entities eklenen ürünün bilgilerini tutuyor.
            var entities = invocation.Arguments.Where(t => t.GetType() == entityType);//methodun argümanlarını gez.
            foreach (var entity in entities)
            {
                ValidationTool.Validate(validator, entity);
            }
        }
    }
}
