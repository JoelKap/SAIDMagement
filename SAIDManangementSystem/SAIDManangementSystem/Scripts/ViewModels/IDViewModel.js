function IdViewModel() {
    var self = this;
    baseView.call(self);

    self.url = '/api/Home';
    self.UserDetails = ko.observable();
    


    self.SAId = ko.observable();
    self.IdStatus = ko.observable();
    self.DateConverter = ko.observable();
    self.Gender = ko.observable();
    self.IsCitizen = ko.observable();
    self.SAIdValue = ko.observable();
   

    self.verify = function (item) {

        self.getData('/api/Home/GetUserDetails?id=' + item.SAIdValue(), self.UserDetails, function (data) {
            if (data.statusText == "OK") {
                self.SAId(self.UserDetails().SAId);
                self.IdStatus(self.UserDetails().IdStatus);
                self.DateConverter(self.UserDetails().DateConverter);
                self.Gender(self.UserDetails().Gender);
                self.IsCitizen(self.UserDetails().IsCitizen);
            } else {
                toastr["error"]("the following error occured" + " " + data.responseText);
            }
        });
    }
}