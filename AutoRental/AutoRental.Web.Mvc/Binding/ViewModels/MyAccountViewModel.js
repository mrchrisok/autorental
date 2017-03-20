(function (ar) {
    var MyAccountViewModel = function () {

        var self = this;

        self.viewModelHelper = new AutoRental.viewModelHelper();
        self.viewMode = ko.observable(); // account, success
        self.accountModel = ko.observable();

        self.initialize = function () {
            self.viewModelHelper.apiGet('api/customer/account', null,
                function (result) {
                    self.accountModel(new AutoRental.MyAccountModel(result.AccountId, result.LoginEmail,
                                                                   result.FirstName, result.LastName,
                                                                   result.Address, result.City, result.State,
                                                                   result.ZipCode, result.CreditCard, result.ExpDate));
                    self.viewMode('account');
                });
        }

        self.save = function (model) {
            var errors = ko.validation.group(model, { deep: true });
            var modelIsValid = (errors().length === 0);
            self.viewModelHelper.modelIsValid(modelIsValid);
            if (modelIsValid) {
                var unmappedModel = ko.mapping.toJS(model);
                self.viewModelHelper.apiPost('api/customer/account', unmappedModel,
                    function (result) {
                        self.viewMode('success');
                    });
            }
            else
                self.viewModelHelper.modelErrors(errors());
        }

        self.initialize();
    }
    ar.MyAccountViewModel = MyAccountViewModel;
}(window.AutoRental));
