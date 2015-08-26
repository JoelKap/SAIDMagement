function baseView() {

    var self = this;
    self.model = ko.observable();
  

    //TOASTER NOTIFICATION
    toastr.options = {
        "closeButton": false,
        "debug": false,
        "newestOnTop": false,
        "progressBar": false,
        "positionClass": "toast-top-right",
        "preventDuplicates": false,
        "onclick": null,
        "showDuration": "5000",
        "hideDuration": "5000",
        "timeOut": "5000",
        "extendedTimeOut": "1000",
        "showEasing": "swing",
        "hideEasing": "linear",
        "showMethod": "fadeIn",
        "hideMethod": "fadeOut"
    }


    //GET DATA
    self.getData = function (url, data, callback) {
        var fetchUrl = "";
        if (url != null) {
            fetchUrl = url;
        } else {
            fetchUrl = self.url;
            fetchUrl = "/" + self.url;
        }
        $.ajax({
            url: fetchUrl,
            type: 'GET',
            async: false,
            contentType: 'application/json',
            success: function (returnData) {
                if (data != null) {
                    var jsonData = ko.toJS(ko.mapping.fromJS(returnData));
                    data(jsonData);
                } else { 
                    self.model.remove(function (item) { return true; });
                    var jsonD = ko.toJS(ko.mapping.fromJS(returnData));
                    model(jsonD);
                }
            },
            error: function (e) {
                //catch any error returned
                //toastr["error"]("" + e.responseText);
            },
            complete: function (e) {
                if (callback != undefined) {
                    callback(e);
                }
            },
        });
    };

}