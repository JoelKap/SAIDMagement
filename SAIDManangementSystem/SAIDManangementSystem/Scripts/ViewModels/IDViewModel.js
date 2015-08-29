function IdViewModel() {
    var self = this;
    baseView.call(self);

    self.url = '/api/Home';
    self.UserDetails = ko.observable();
    self.Id = ko.observable();
    self.GeneratedId = ko.observable();

    self.SAId = ko.observable();
    self.DateConverter = ko.observable();
    self.Gender = ko.observable();
    self.IsCitizen = ko.observable();
    self.SAIdValue = ko.observable();
    self.Age = ko.observable();



    self.verify = function (item) {
        self.GeneratedId('');
        if (validInput()) {
            self.getData('/api/Home/GetUserDetails?id=' + item.SAIdValue(), self.UserDetails, function (data) {
                if (data.statusText == "OK") {
                    setTimeout(function () {
                        toastr["success"]("Retrived successfully!");
                    }, 500);
                    self.SAId(self.UserDetails().SAId);
                    self.DateConverter(self.UserDetails().DateConverter);
                    self.Gender(self.UserDetails().Gender);
                    self.Age(self.UserDetails().Age);
                    self.IsCitizen(self.UserDetails().IsCitizen);
                } else {
                    toastr["error"]("the following error occured" + " " + data.responseText);
                }
            });
        }
    }

    self.generate = function () {
        self.SAId('');
        self.DateConverter('');
        self.Gender('');
        self.IsCitizen('');
        self.SAIdValue('');
        self.Age('');
        self.getData('/api/Home', self.Id, function (data) {
            self.GeneratedId(self.Id());
        });

    }

    function validInput() {
        var isValid = true;
        if ($("#idNumber").val() == "") {
            toastr["error"]("Please type in Id Number");
            isValid = false;
        }

        else if ($('#idNumber').val().length < 13) {
            toastr["error"]("Id Number can't be less then 13 character long!");
            isValid = false;
        }
        return isValid;
    }


}