using System;
namespace Entity.Enum
{
    public enum OrderStates
    {
        WAITINGCARDPAYMENT = 0,
        UNAPPROVED = 1,

        APPROVED = 2,
        PREPARING = 3,
        ONROAD = 4,
        DELIVERED = 5,

        COMPLETED = 6,

        CANCELED = 7,
        DELETED = 8
    }



}

