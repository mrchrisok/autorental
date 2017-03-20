
(function (ar) {
    var ReserveCarModel = function () {

        var self = this;

        self.PickupDate = ko.observable("").extend(
            { required: { message: "Pickup date is required" } }
            );
        self.ReturnDate = ko.observable("").extend(
            { required: { message: "Return date is required" } }
            );
    }
    ar.ReserveCarModel = ReserveCarModel;
}(window.AutoRental));
