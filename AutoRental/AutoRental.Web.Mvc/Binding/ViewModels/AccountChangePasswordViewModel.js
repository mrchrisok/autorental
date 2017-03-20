(function (ar) {
    var AccountChangePasswordViewModel = function (loginEmail) {

        'use strict';

        var self = this;

        self.viewModelHelper = new AutoRental.viewModelHelper();
        self.passwordModel = new AutoRental.AccountChangePasswordModel();
        self.viewMode = ko.observable("changepw"); // changepw, success

        self.changePassword = function (model) {
            var errors = ko.validation.group(model);
            var modelIsValid = (errors().length === 0);
            self.viewModelHelper.modelIsValid(modelIsValid);
            if (modelIsValid) {
                var unmappedModel = ko.mapping.toJS(model);
                unmappedModel.LoginEmail = loginEmail;
                self.viewModelHelper.apiPost('api/account/changepw', unmappedModel,
                    function (result) {
                        self.viewMode('success');
                    });
            }
            else
                self.viewModelHelper.modelErrors(errors());
        }
    }
    ar.AccountChangePasswordViewModel = AccountChangePasswordViewModel;
}(window.AutoRental));
