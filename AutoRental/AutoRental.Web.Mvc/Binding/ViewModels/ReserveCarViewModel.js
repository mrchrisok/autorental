
(function (ar) {
    var ReserveCarViewModel = function () {

        var self = this;

        var initialState = 'reserve';

        self.viewModelHelper = new AutoRental.viewModelHelper();
        self.viewMode = ko.observable(); // reserve, carlist, success
        self.reservationModel = ko.observable();
        self.cars = ko.observableArray();
        self.reservationNumber = ko.observable();

        var pickupDate = null;
        var returnDate = null;

        self.initialize = function () {
            self.reservationModel(new AutoRental.ReserveCarModel());
            self.viewMode('reserve');
        }

        self.availableCars = function (model) {
            var errors = ko.validation.group(model, { deep: true });
            var modelIsValid = (errors().length === 0);
            self.viewModelHelper.modelIsValid(modelIsValid);
            if (modelIsValid) { //oco: xhr call to get list of available cars
                self.viewModelHelper.apiGet('api/reservation/availablecars',
                    { pickupDate: model.PickupDate(), returnDate: model.ReturnDate() },
                    function (result) {
                        pickupDate = model.PickupDate();
                        returnDate = model.ReturnDate();
                        ko.mapping.fromJS(result, {}, self.cars); // map the returned cars to an array
                        self.viewMode('carlist');
                    });
            }
            else
                self.viewModelHelper.modelErrors(errors());

        }

        self.selectCar = function (car) {
            var model = {
                PickupDate: self.reservationModel().PickupDate(),
                ReturnDate: self.reservationModel().ReturnDate(),
                Car: car.CarId()
            };
            self.viewModelHelper.apiPost('api/reservation/reservecar', model,
                function (result) {
                    self.reservationNumber(result.ReservationId);
                    self.viewMode('success');
                });
        }

        self.reservationDates = function () {
            self.cars([]);
            self.viewMode('reserve');
        }

        self.viewMode.subscribe(function () {
            switch (self.viewMode()) {
                case 'reserve':
                    self.viewModelHelper.pushUrlState('reserve', null, null, 'customer/reserve');
                    break;
                case 'carlist':
                    self.viewModelHelper.pushUrlState('carlist', null, null, 'customer/reserve');
                    break;
            }

            initialState = self.viewModelHelper.handleUrlState(initialState);
        });

        if (Modernizr.history) {
            window.onpopstate = function (arg) {
                if (arg.state != null) {
                    self.viewModelHelper.statePopped = true;
                    self.viewMode(arg.state.Code);
                }
            };
        }

        self.initialize();
    }
    ar.ReserveCarViewModel = ReserveCarViewModel;
}(window.AutoRental));
