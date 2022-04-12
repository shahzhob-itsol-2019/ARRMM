﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ARRMM.Templates
{
    public class EmailTemplateUserRegisteration
    {
        public const string Subject = "Account Activation";
        public const string MessageBody = "Hi #Name </br></br>"
                                        + "Thank you for registering in ItSolution. "
                                        + "Click <a href=\"#Link\">here</a> to activate your account.";
    }
}