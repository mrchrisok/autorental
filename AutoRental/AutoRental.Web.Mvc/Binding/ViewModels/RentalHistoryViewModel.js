(function (ar) {
    var RentalHistoryViewModel = function () {

        'use strict';

        var self = this;

        self.viewModelHelper = new AutoRental.viewModelHelper();
        self.rentals = ko.observableArray();

        self.initialize = function () {
            self.loadAllRentals();
        }

        self.loadAllRentals = function () {
            self.viewModelHelper.apiGet('api/rental/getrentalhistory', null,
                function (result) {
                    self.rentals.removeAll();
                    for (var i = 0; i < result.length; i++) {
                        self.rentals.push(new AutoRental.RentalModel(
                            result[i].RentalId, result[i].Car, result[i].DateRented, result[i].ExpectedReturn, result[i].DateReturned));
                    }
                });
        }

        self.initialize();
    }
    ar.RentalHistoryViewModel = RentalHistoryViewModel;
}(window.AutoRental));
