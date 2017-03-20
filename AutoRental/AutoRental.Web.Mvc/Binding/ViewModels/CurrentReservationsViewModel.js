(function (ar) {
    var CurrentReservationsViewModel = function () {

        'use strict';

        var self = this;

        var initialState = 'reserve';

        self.viewModelHelper = new AutoRental.viewModelHelper();
        self.reservations = ko.observableArray();

        self.initialize = function () {
            self.loadOpenReservations();
        }

        self.loadOpenReservations = function () {
            self.viewModelHelper.apiGet('api/reservation/getopen', null,
                function (result) {
                    self.reservations.removeAll();
                    for (var i = 0; i < result.length; i++) {
                        self.reservations.push(new AutoRental.ReservationModel(
                            result[i].ReservationId, result[i].Car, result[i].RentalDate, result[i].ReturnDate));
                    }
                });
        }

        self.requestCancelReservation = function (reservation) {
            reservation.CancelRequest(true);
        }

        self.undoCancelRequest = function (reservation) {
            reservation.CancelRequest(false);
        }

        self.cancelReservation = function (reservation) {
            self.viewModelHelper.apiPost('api/reservation/cancel', { '': reservation.ReservationId() },
                function (result) {
                    self.reservations.remove(reservation);
                });
        }

        self.initialize();
    }
    ar.CurrentReservationsViewModel = CurrentReservationsViewModel;
}(window.AutoRental));
