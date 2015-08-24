app.service('enduserproductService', function ($http) {

    ////Create new record  
    //this.post = function (EndUser) {
    //    var request = $http({
    //        method: "post",
    //        url: "/api/EndUserProduct",
    //        data: EndUser
    //    });
    //    return request;
    //}

    ////Get Single Records  
    //this.get = function (ProductID) {
    //    return $http.get("/api/productNew/" + ProductID);
    //}

    //Get All Product  
    this.getAllProduct = function () {
        return $http.get("/api/EndUserProduct");
    }

    this.addupdate = function (ProductID) {
        var request = $http({
            method: "get",
            url: "/api/EndUserUpdate/" + ProductID
        });
        return request;
    }
});