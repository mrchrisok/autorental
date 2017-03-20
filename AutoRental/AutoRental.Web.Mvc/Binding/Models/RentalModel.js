(function (ar) {
    var RentalModel = function (rentalId, car, dateRented, dateDue, dateReturned) {

        var self = this;

        self.RentalId = ko.observable(rentalId);
        self.Car = ko.observable(car);
        self.DateRented = ko.observable(dateRented);
        self.DateDue = ko.observable(dateDue);
        self.DateReturned = ko.observable(dateReturned);
    }
    ar.RentalModel = RentalModel;
}(window.AutoRental));
