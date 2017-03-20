(function (ar) {

    'use strict';

    var AccountRegisterModelStep1 = function () {

        var self = this;

        self.FirstName = ko.observable("").extend({
            required: { message: "First name is required" }
        });
        self.LastName = ko.observable("").extend({
            required: { message: "Last name is required" }
        });
        self.Address = ko.observable("").extend({
            required: { message: "Address is required" }
        });
        self.City = ko.observable("").extend({
            required: { message: "City is required" }
        });
        self.State = ko.observable("").extend({
            required: { message: "State is required" }
        });
        self.ZipCode = ko.observable("").extend({
            required: { message: "Zip code is required" }
        });
    }
    ar.AccountRegisterModelStep1 = AccountRegisterModelStep1;
}(window.AutoRental));

(function (ar) {

    'use strict';

    var AccountRegisterModelStep2 = function () {

        var self = this;

        self.LoginEmail = ko.observable("").extend({
            required: { message: "Login email is required" },
            email: { message: "Login is not a valid email" }
        });
        self.Password = ko.observable("").extend({
            required: { message: "Password is required" },
            minLength: { message: "Password must be at least 6 characters", params: 6 }
        });
        self.PasswordConfirm = ko.observable("").extend({
            validation: { validator: AutoRental.mustEqual, message: "Password do not match", params: self.Password }
        });
    }
    ar.AccountRegisterModelStep2 = AccountRegisterModelStep2;
}(window.AutoRental));

(function (ar) {

    'use strict';

    var AccountRegisterModelStep3 = function () {

        var self = this;

        self.CreditCard = ko.observable("").extend({
            required: { message: "Credit card number is required" }
        });
        self.ExpDate = ko.observable("").extend({
            required: { message: "Expiration date is required" }
        });
    }
    ar.AccountRegisterModelStep3 = AccountRegisterModelStep3;
}(window.AutoRental));
