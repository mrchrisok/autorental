﻿(function (ar) {
    var ReservationModel = function (reservationId, car, rentalDate, returnDate) {

        var self = this;

        self.ReservationId = ko.observable(reservationId);
        self.Car = ko.observable(car);
        self.RentalDate = ko.observable(rentalDate);
        self.ReturnDate = ko.observable(returnDate);

        self.CancelRequest = ko.observable(false);
    }
    ar.ReservationModel = ReservationModel;
}(window.AutoRental));
