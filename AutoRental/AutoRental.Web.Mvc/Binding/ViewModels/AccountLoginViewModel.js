
(function (ar) {
    var AccountLoginViewModel = function (returnUrl) {

        'use strict';

        var self = this;

        self.viewModelHelper = new AutoRental.viewModelHelper();
        self.accountModel = new AutoRental.AccountLoginModel();

        self.login = function (model) {
            var errors = ko.validation.group(model); // populate errors() array
            var modelIsValid = (errors().length === 0);
            self.viewModelHelper.modelIsValid(modelIsValid); // set modelIsValid to true/false
            if (modelIsValid) {
                // action(s) to take if no errors
                var unmappedModel = ko.mapping.toJS(model); // convert model to JSON
                self.viewModelHelper.apiPost("api/account/login", unmappedModel,
                    function (result) {  // success callback
                        if (returnUrl != '' && returnUrl.length > 1)
                            window.location.href = AutoRental.rootPath + returnUrl.substring(1);
                        else
                            window.location.href = AutoRental.rootPath;
                    });
            }
            else
                self.viewModelHelper.modelErrors(errors()); // set modelErrors property to errors()
        }
    }
    ar.AccountLoginViewModel = AccountLoginViewModel;
}(window.AutoRental));
