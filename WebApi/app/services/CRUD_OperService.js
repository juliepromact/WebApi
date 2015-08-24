app.service('CRUD_OperService', function ($http) {  
  
    //Create new record  
    this.post = function (ProductNew) {
        var request = $http({  
            method: "post",  
            url: "/api/productNew",  
            data: ProductNew
        });  
        return request;  
    }  
  
    //Get Single Records  
    this.get = function (ProductID) {  
        return $http.get("/api/productNew/" + ProductID);
    }  
  
    //Get All Product  
    this.getAllProduct = function () {  
        return $http.get("/api/productNew");
    }  

    //Update the Record  
    this.put = function (ProductID, ProductNew) {
        var request = $http({
            method: "put",
            url: "/api/productNew/" + ProductID,
            data: ProductNew
        });
        return request;
    }

    //Delete the Record  
    this.delete = function (ProductID) {
        var request = $http({
            method: "delete",
            url: "/api/productNew/" + ProductID
        });
        return request;
    }

    this.addupdate = function (ProductID) {
        var request = $http({
            method: "get",
            // url: "/api/Update/" + ProductID
            url: "/api/EndUserUpdate/" + ProductID
        });
        return request;
    }
});